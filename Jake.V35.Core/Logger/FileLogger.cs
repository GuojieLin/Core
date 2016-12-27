using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Text;
using Jake.V35.Core.Extensions;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	4/29/2016 16:30:46 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace Jake.V35.Core.Logger
{
    /// <summary>
    /// 2016.08.5 删除键和获取文件大小是对dictionary加锁
    /// </summary>
    internal class FileLogger : ILogger,IDisposable
    {
        private static readonly Dictionary<string, LogInfo> WriteLogDirectory;
        private static readonly Dictionary<string, LogInfo> EmergencyWriteLogDirectory;

        //private static readonly Dictionary<string, IList<string>> WriteLogDirectory;
        //private static readonly Dictionary<string, IList<string>> EmergencyWriteLogDirectory;
        //private static readonly Dictionary<string, object> FileLocks;
        public static ILogger Empty = new EmptyLogger();
        private static AutoResetEvent WriteAutoResetEvent { get; set; }
        private static AutoResetEvent EmergencyWriteAutoResetEvent { get; set; }
        private static bool _start = true;
        private static bool _isDispose = false;
        /// <summary>
        /// 一般日志
        /// </summary>
        private static readonly System.Threading.Thread _writeThread;
        /// <summary>
        /// 错误日志另外处理，保证能实时记录
        /// </summary>
        private static readonly System.Threading.Thread _emergencyWriteThread;
        /// <summary>
        /// 路径
        /// </summary>
        public string DirectoryName { get; set; }

        /// <summary>
        /// FileLogger的别名
        /// </summary>
        public string FileName { get; set; }

        static FileLogger()
        {
            //FileLocks = new Dictionary<string, object>();
            //WriteLogDirectory = new Dictionary<string, IList<string>>();
            //EmergencyWriteLogDirectory = new Dictionary<string, IList<string>>();
            WriteLogDirectory = new Dictionary<string, LogInfo>();
            EmergencyWriteLogDirectory = new Dictionary<string, LogInfo>();
            WriteAutoResetEvent = new AutoResetEvent(false);
            EmergencyWriteAutoResetEvent = new AutoResetEvent(false);
            _writeThread = new System.Threading.Thread(StartWriter);
            _emergencyWriteThread = new System.Threading.Thread(StartWriter);

            _writeThread.IsBackground = true;
            _emergencyWriteThread.IsBackground = true;

            _writeThread.Start(new object[] { WriteLogDirectory, WriteAutoResetEvent });
            _emergencyWriteThread.Start(new object[] { EmergencyWriteLogDirectory, EmergencyWriteAutoResetEvent });
        }
        /// <summary>
        /// 使用一个明确的文件路径,若只有文件名则用默认
        /// </summary>
        /// <param name="fileName"></param>
        public FileLogger(string fileName)
        {
            Init(fileName);
        }
        public FileLogger(bool userDefaultRoot, params string[] paths)
        {
            if (paths == null) throw new ArgumentNullException("paths");
            if (!paths.Any()) throw new Exception("至少输入一个路径");
            var logPath = "";
            if (userDefaultRoot) logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.DefaultLogDirectory);
            logPath = paths.Aggregate(logPath, Path.Combine);
            Init(logPath);
        }
        
        private void Init(string logPath)
        {
            if (Path.HasExtension(logPath))
            {
                FileName = Path.GetFileName(logPath);
                DirectoryName = Path.GetDirectoryName(logPath);
                if (String.IsNullOrEmpty(DirectoryName))
                {
                    DirectoryName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.DefaultLogDirectory);
                }
            }
            else
            {
                FileName = DateTime.Now.ToString(Constants.AutoFileNameFormat) + Constants.LogEntensionName;
                DirectoryName = logPath;
            }
        }

        private static void StartWriter(object parmameter)
        {
            if (_isDispose) throw new Exception("日志服务已经释放");
            var paras = (object[]) parmameter;
            IDictionary<string,LogInfo> dictionary = (IDictionary<string, LogInfo>)paras[0];
            AutoResetEvent autoResetEvent = (AutoResetEvent)paras[1];
            while (_start)
            {
                try
                {
                    autoResetEvent.WaitOne();
                    bool hasLog = false;
                    Monitor.Enter(dictionary);
                    string[] fileNames = dictionary.Keys.ToArray();
                    Monitor.Exit(dictionary);
                    foreach (var key in fileNames)
                    {
                        LogInfo logObjects;
                        lock (dictionary)
                        {
                            logObjects = dictionary[key];
                            if (logObjects.StringBuilder.Length <= 0)
                            {
                                dictionary.Remove(key);
                                System.Threading.Thread.Sleep(1);
                                continue;
                            }
                        }
                        hasLog = true;
                        try
                        {
                            logObjects.Write();
                        }
                        catch (Exception exception)
                        {
                            //由于磁盘空间满等原因写不进去
                            ILogger logger = FileLoggerFactory.Default.Create("LogError.log");
                            logger.WriteWarning("日志写入出错：", exception);
                        }
                    }
                    if (!hasLog)
                    {
                        autoResetEvent.Reset();
                    }
                    System.Threading.Thread.Sleep(1);
                }
                catch (Exception exception)
                {
                    ILogger logger = FileLoggerFactory.Default.Create("LogError.log");
                    logger.WriteWarning("日志写入出错:", exception);
                }
            }
        }
        public bool WriteCore(LogType logType, string content, Exception exception, Func<string, Exception, string> formatter)
        {
            try
            {
                if(_isDispose) throw new Exception("日志服务已经释放");
                string msg = formatter(content, exception);
                string directoryName = String.Format("{0}\\{1}\\{2}\\", DirectoryName.TrimEnd('\\'),
                    DateTime.Now.ToString(Constants.LogDirectoryDateFormat), logType.GetValue());
                if (logType == LogType.Error)
                {
                    Log(directoryName, FileName, msg, EmergencyWriteLogDirectory);
                    EmergencyWriteAutoResetEvent.Set();
                }
                else
                {
                    Log(directoryName, FileName,msg, WriteLogDirectory);
                    WriteAutoResetEvent.Set();
                }
                return true;

            }
            catch (Exception ex)
            {
                ILogger logger = FileLoggerFactory.Default.Create("LogError.log");
                logger.WriteError("日志写入出错:", ex);
                return false;
            }
        }


        private void Log(string dictionaryName,string fileName, string msg, IDictionary<string, LogInfo /*IList<string>*/> dictionary)
        {
            //首先缓存 队列查找是否存在该文件,存在则直接插入尾部,可减少文件读写次数
            string fullName = Path.Combine(dictionaryName, this.FileName);
            lock (dictionary)
            {
                LogInfo logInfo;
                if (!dictionary.TryGetValue(fullName, out logInfo))
                {
                    logInfo = new LogInfo(dictionaryName, fileName);
                    dictionary.Add(fullName, logInfo);
                } 
                logInfo.Append(msg);
            }
            
        }
        

        void IDisposable.Dispose()
        {
            Dispose();
        }
        public static void Dispose()
        {
            while (WriteLogDirectory.Count > 0 || EmergencyWriteLogDirectory.Count > 0)
            {
                //等待日志写完
                System.Threading.Thread.Sleep(10);
            }
            _start = false;
            _isDispose = true;
            EmergencyWriteAutoResetEvent = null;
            WriteAutoResetEvent = null;
        }

    }
}
