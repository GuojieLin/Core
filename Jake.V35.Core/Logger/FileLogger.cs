using System;
using System.Collections;
using System.Collections.Generic;
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
    internal class FileLogger : ILogger
    {
        //private static readonly Dictionary<string, StringBuilder> WriteLogDirectory;
        //private static readonly Dictionary<string, StringBuilder> EmergencyWriteLogDirectory;

        private static readonly Dictionary<string, IList<string>> WriteLogDirectory;
        private static readonly Dictionary<string, IList<string>> EmergencyWriteLogDirectory;
        //private static readonly Dictionary<string, object> FileLocks;
        public static ILogger Empty = new EmptyLogger();

        /// <summary>
        /// FileLogger的别名
        /// </summary>
        public string FileName { get; set; }
        private static AutoResetEvent _writeAutoResetEvent { get; set; }
        private static AutoResetEvent _emergencyWriteAutoResetEvent { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string DirectoryName { get; set; }
        
        private static bool _start = true;
        /// <summary>
        /// 一般日志
        /// </summary>
        private static System.Threading.Thread _writeThread;
        /// <summary>
        /// 错误日志另外处理，保证能实时记录
        /// </summary>
        private static System.Threading.Thread _emergencyWriteThread;

        static FileLogger()
        {
            //FileLocks = new Dictionary<string, object>();
            WriteLogDirectory = new Dictionary<string, IList<string>>();
            EmergencyWriteLogDirectory = new Dictionary<string, IList<string>>();
            //WriteLogDirectory = new Dictionary<string, StringBuilder>();
            //EmergencyWriteLogDirectory = new Dictionary<string, StringBuilder>();
            _writeAutoResetEvent = new AutoResetEvent(false);
            _emergencyWriteAutoResetEvent = new AutoResetEvent(false);
            _writeThread = new System.Threading.Thread(StartWriter);
            _emergencyWriteThread = new System.Threading.Thread(StartWriter);

            _writeThread.IsBackground = true;
            _emergencyWriteThread.IsBackground = true;

            _writeThread.Start(new object[] { WriteLogDirectory, _writeAutoResetEvent });
            _emergencyWriteThread.Start(new object[] { EmergencyWriteLogDirectory, _emergencyWriteAutoResetEvent });
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
            var paras = (object[]) parmameter;
            IDictionary<string,IList<string>> dictionary = (IDictionary<string, IList<string>>)paras[0];
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
                        hasLog = true;
                        IList<string> logObjects;
                        lock (dictionary)
                        {
                            logObjects = dictionary[key];
                            lock (logObjects)
                            {
                                if (!logObjects.Any())
                                {
                                    dictionary.Remove(key);
                                    continue;
                                }
                            }
                        }
                        var dir = Path.GetDirectoryName(key);
                        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                        lock (logObjects)
                        {
                            string content = logObjects.Aggregate("", (str1, str2) => str1 + str2);
                            using (var logStreamWriter = new StreamWriter(key, true))
                            {
                                logStreamWriter.Write(content);
                                logStreamWriter.Flush();
                            }
                            logObjects.Clear();
                        }
                    }
                    if (!hasLog)
                    {
                        autoResetEvent.Reset();
                    }
                }
                catch (Exception exception)
                {
                    ILogger logger = FileLoggerFactory.Default.Create("LogError.log");
                    logger.WriteError("日志写入出错:", exception);
                }
            }
        }
        public bool WriteCore(LogType logType, string content, Exception exception, Func<string, Exception, string> formatter)
        {
            try
            {
                string msg = formatter(content, exception);
                string directoryName = String.Format("{0}\\{1}\\{2}\\", DirectoryName.TrimEnd('\\'),
                    DateTime.Now.ToString(Constants.LogDirectoryDateFormat), logType.GetValue());
                string fileName = Path.Combine(directoryName, this.FileName);
                //LogObject logObject = new LogObject
                //{
                //    Content = formatter(content, exception),
                //    FileName = this.FileName,
                //    DirectoryName = String.Format("{0}\\{1}\\{2}\\", DirectoryName.TrimEnd('\\'),
                //        DateTime.Now.ToString(Constants.LogDirectoryDateFormat), logType.GetValue())
                //};
                //string msg = formatter(content, exception);

                //if (GetFileSize(logType, logObject.FullName) + Encoding.UTF8.GetBytes(content).Length/1024 > 1024)
                //{
                //    var fn = Path.GetFileNameWithoutExtension(logObject.FileName);
                //    int index = fn.IndexOf("_", StringComparison.Ordinal);
                //    if (index > 0)
                //    {
                //        fn = fn.Substring(0, index);
                //    }
                //    logObject.FileName = fn + "_" + DateTime.Now.ToString(Constants.AutoFileNameFormat) + ".log";
                //}
                if (logType == LogType.Error)
                {
                    //
                    Log(fileName,msg, EmergencyWriteLogDirectory);
                    _emergencyWriteAutoResetEvent.Set();
                }
                else
                {
                    Log(fileName,msg, WriteLogDirectory);
                    _writeAutoResetEvent.Set();
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

        private void Log(string fileName, string msg, IDictionary<string, IList<string>> dictionary)
        {
            //首先缓存 队列查找是否存在该文件,存在则直接插入尾部,可减少文件读写次数
            IList<string> buffer;
            lock (dictionary)
            {
                if (!dictionary.TryGetValue(fileName, out buffer))
                {
                    buffer = new List<string>();
                    dictionary.Add(fileName, buffer);
                }
            }
            lock (buffer)
            {
                buffer.Add(msg);
            }
        }
        
        //private void Log(string fileName, string msg,IDictionary<string, StringBuilder> dictionary)
        //{
        //        //首先缓存 队列查找是否存在该文件,存在则直接插入尾部,可减少文件读写次数
        //    lock (dictionary)
        //    {
        //        StringBuilder buffer;
        //        if (dictionary.TryGetValue(fileName, out buffer))
        //        {
        //            buffer.AppendLine(msg);
        //        }
        //        else
        //        {
        //            dictionary.Add(fileName, new StringBuilder().AppendLine(msg));
        //        }
        //        AddLock(fileName);
        //    }
        //}
        //private static object AddLock(string fileName)
        //{
        //    object o;
        //    lock (FileLocks)
        //    {
        //        if (!FileLocks.TryGetValue(fileName,out o))
        //        {
        //            o = new object();
        //            FileLocks.Add(fileName, o);
        //        }
        //    }
        //    return o;
        //}

        private static long GetFileSize(LogType logType,string fileName)
        {
            IDictionary dictionary = logType == LogType.Error ? EmergencyWriteLogDirectory : WriteLogDirectory;
            long strRe = 0;
            if (File.Exists(fileName))
            {
                lock (dictionary)
                {
                    using (var fileStream = new FileStream(fileName, FileMode.Open))
                    {
                        strRe = fileStream.Length/1024;
                    }
                }

            }
            return strRe;
        }
    }
}
