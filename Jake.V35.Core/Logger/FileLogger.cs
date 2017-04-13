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
        private static readonly Dictionary<string, LogEntity> WriteLogDirectory;
        private static readonly Dictionary<string, LogEntity> EmergencyWriteLogDirectory;
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

        /// <summary>
        /// FileLogger的别名
        /// </summary>
        public string FullName
        {
            get { return Path.Combine(this.DirectoryName, this.FileName); }
        }

        private LogConfiguration _configuration;

        public LogConfiguration Configuration
        {
            get { return _configuration; }
            set
            {
                _configuration = value;
                //不为空的时候需要更新配置
                OnConfigurationChanged();
            }
        }

        private void OnConfigurationChanged()
        {
            Init(this.FileName);
        }

        static FileLogger()
        {
            //FileLocks = new Dictionary<string, object>();
            //WriteLogDirectory = new Dictionary<string, IList<string>>();
            //EmergencyWriteLogDirectory = new Dictionary<string, IList<string>>();
            WriteLogDirectory = new Dictionary<string, LogEntity>();
            EmergencyWriteLogDirectory = new Dictionary<string, LogEntity>();
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
        /// <param name="configuration">配置</param>
        /// <param name="fileName"></param>
        /// <param name="directoryName"></param>
        public FileLogger(string fileName, string directoryName, LogConfiguration configuration)
        {
            _configuration = configuration;
            FileName = fileName;
            DirectoryName = directoryName;
        }

        public FileLogger(string logPath, LogConfiguration configuration)
        {
            _configuration = configuration;
            Init(logPath);
        }

        /// <summary>
        /// 重新初始化日志目录
        /// </summary>
        /// <param name="logPath"></param>
        private void Init(string logPath)
        {
            if (Path.HasExtension(logPath))
            {
                FileName = Path.GetFileName(logPath);
                DirectoryName = Path.GetDirectoryName(logPath);
                if (String.IsNullOrEmpty(DirectoryName))
                {
                    DirectoryName = Path.Combine(_configuration.BasePath, Constants.DefaultLogDirectory);
                }
            }
            else
            {
                FileName = DateTime.Now.ToString(_configuration.AutoFileNameDateFormat) + Constants.LogEntensionName;
                DirectoryName = logPath;
            }
        }

        private static void StartWriter(object parmameter)
        {
            if (_isDispose) throw new Exception("日志服务已经释放");
            var paras = (object[]) parmameter;
            IDictionary<string,LogEntity> dictionary = (IDictionary<string, LogEntity>)paras[0];
            AutoResetEvent autoResetEvent = (AutoResetEvent)paras[1];
            while (_start)
            {
                try
                {
                    autoResetEvent.WaitOne(10000);
                    bool hasLog = false;
                    Monitor.Enter(dictionary);
                    string[] fileNames = dictionary.Keys.ToArray();
                    Monitor.Exit(dictionary);
                    foreach (var key in fileNames)
                    {
                        LogEntity logEntity;
                        lock (dictionary)
                        {
                            logEntity = dictionary[key];
                            if (logEntity.Length<= 0)
                            {
                                //移除对象
                                dictionary.Remove(key);
                                System.Threading.Thread.Sleep(1);
                                continue;
                            }
                        }
                        hasLog = true;
                        try
                        {
                            logEntity.Write();
                        }
                        catch (Exception exception)
                        {
                            //由于磁盘空间满等原因写不进去
                            ILogger logger = FileLoggerFactory.Default.Create("LogError.log");
                            logger.WriteWarning("日志写入出错：", exception);
                        }
                    }
                    if (hasLog)
                    {
                        autoResetEvent.Set();
                    }
                }
                catch (Exception exception)
                {
                    ILogger logger = FileLoggerFactory.Default.Create("LogError.log");
                    logger.WriteWarning("日志写入出错:", exception);
                }
                finally
                {
                    System.Threading.Thread.Sleep(1);
                }
            }
        }
        bool ILogger.WriteCore(LogType logType, string content, Exception exception, Func<string, Exception, string> formatter)
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
        private void Log(string dictionaryName,string fileName, string msg, IDictionary<string, LogEntity /*IList<string>*/> dictionary)
        {
            //首先缓存 队列查找是否存在该文件,存在则直接插入尾部,可减少文件读写次数
            string fullName = Path.Combine(dictionaryName, this.FileName);
            lock (dictionary)
            {
                LogEntity logInfo;
                if (!dictionary.TryGetValue(fullName, out logInfo))
                {
                    logInfo = new LogEntity(Configuration, dictionaryName, fileName);
                    dictionary.Add(fullName, logInfo);
                } 
                logInfo.Append(msg);
            }
            
        }
        
        void IDisposable.Dispose()
        {
            Dispose(true);
        }

        public static void Dispose(bool isDispose = true)
        {
            if (isDispose)
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
}
