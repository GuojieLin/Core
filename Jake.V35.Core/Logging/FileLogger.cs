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
namespace Jake.V35.Core.Logging
{
    
    internal class FileLogger : ILogger
    {
        private static readonly Dictionary<string, StringBuilder> WriteLogDirectory;
        private static readonly Dictionary<string, StringBuilder> EmergencyWriteLogDirectory;
        private static readonly Dictionary<string, object> FileLocks;
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
        /// 每写入10000次清理一次无用key
        /// </summary>
        private const int ResetCount = 10000;

        private static int _currentCount = 0;
        /// <summary>
        /// 一般日志
        /// </summary>
        private static Thread _writeThread;
        /// <summary>
        /// 错误日志另外处理，保证能实时记录
        /// </summary>
        private static Thread _emergencyWriteThread;

        static FileLogger()
        {
            FileLocks = new Dictionary<string, object>();
            WriteLogDirectory = new Dictionary<string, StringBuilder>();
            EmergencyWriteLogDirectory = new Dictionary<string, StringBuilder>();
            _writeAutoResetEvent = new AutoResetEvent(false);
            _emergencyWriteAutoResetEvent = new AutoResetEvent(false);
            _writeThread = new Thread(StartWriter);
            _emergencyWriteThread = new Thread(StartWriter);

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
            IDictionary<string, StringBuilder> dictionary = (IDictionary<string, StringBuilder>)paras[0];
            AutoResetEvent autoResetEvent = (AutoResetEvent)paras[1];
            while (_start)
            {
                autoResetEvent.WaitOne();
                bool hasLog = false;
                Monitor.Enter(dictionary);
                string[] fileNames = dictionary.Keys.ToArray();
                Monitor.Exit(dictionary);
                foreach (var key in fileNames)
                {
                    hasLog = true;
                    Monitor.Enter(dictionary);
                    StringBuilder builder = dictionary[key];
                    Monitor.Exit(dictionary);
                    if (builder.Length <= 0) continue;
                    object o = AddLock(key);
                    lock (o)
                    {
                        var dir = Path.GetDirectoryName(key);
                        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                        using (var logStreamWriter = new StreamWriter(key, true))
                        {
                            logStreamWriter.Write(builder.ToString());
                            logStreamWriter.Flush();
                        }
                        builder.Remove(0, builder.Length);
                        _currentCount++;
                    }
                }
                if (!hasLog)
                {
                    autoResetEvent.Reset();
                }
                else if (_currentCount > ResetCount)
                {
                    Monitor.Enter(dictionary);
                    List<string> keys = dictionary.Keys.ToList();
                    Monitor.Exit(dictionary);
                    foreach (var key in keys)
                    {
                        lock (dictionary)
                        {
                            if (dictionary[key].Length <= 0)
                            {
                                dictionary.Remove(key);
                                lock (FileLocks)
                                {
                                    if (FileLocks.ContainsKey(key))
                                        FileLocks.Remove(key);
                                }
                            }
                        }
                    }
                }
            }
        }
        public bool WriteCore(LogType logType, string content, Exception exception, Func<string, Exception, string> formatter)
        {
            string msg = formatter(content, exception);
            string path = String.Format("{0}\\{1}\\{2}\\{3}", DirectoryName.TrimEnd('\\'),
                 DateTime.Now.ToString(Constants.LogDirectoryDateFormat), logType.GetValue(), FileName);

            if (GetFileSize(path) + Encoding.UTF8.GetBytes(content).Length / 1024 > 1024)
            {
                var fn = Path.GetFileNameWithoutExtension(path);
                int index = fn.IndexOf("_", StringComparison.Ordinal);
                if (index > 0)
                {
                    fn = fn.Substring(0, index);
                }
                FileName = fn + "_" + DateTime.Now.ToString(Constants.AutoFileNameFormat) + ".log";
                path = String.Format("{0}\\{1}\\{2}\\{3}", DirectoryName.TrimEnd('\\'),
                    DateTime.Now.ToString(Constants.LogDirectoryDateFormat), logType.GetValue(), FileName);
            }
            if (logType == LogType.Error)
            {
                //
                Log(path, msg, WriteLogDirectory);
                _writeAutoResetEvent.Set();
            }
            else
            {
                Log(path, msg, EmergencyWriteLogDirectory);
                _emergencyWriteAutoResetEvent.Set();
            }
            return true;
        }
        private void Log(string fileName, string msg,IDictionary<string, StringBuilder> dictionary)
        {
                //首先缓存 队列查找是否存在该文件,存在则直接插入尾部,可减少文件读写次数
            lock (dictionary)
            {
                StringBuilder buffer;
                if (dictionary.TryGetValue(fileName, out buffer))
                {
                    buffer.AppendLine(msg);
                }
                else
                {
                    dictionary.Add(fileName, new StringBuilder().AppendLine(msg));
                }
            }
            AddLock(fileName);
        }

        private static object AddLock(string fileName)
        {
            object o;
            lock (FileLocks)
            {
                if (!FileLocks.TryGetValue(fileName,out o))
                {
                    o = new object();
                    FileLocks.Add(fileName, o);
                }
            }
            return o;
        }

        private static long GetFileSize(string fileName)
        {
            long strRe = 0;
            if (File.Exists(fileName))
            {
                AddLock(fileName);
                lock (FileLocks[fileName])
                {
                    using (var fileStream = new FileStream(fileName, FileMode.Open))
                    {
                        strRe = fileStream.Length / 1024;
                    }
                }
            }
            return strRe;
        }
    }
}
