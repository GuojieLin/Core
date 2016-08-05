using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using CCBFC.Common.Extensions;
using CCBFC.Common.Helpers;
using Jake.Common.V35.Core.Extensions;
using Jake.Common.V35.Core.Logging;

namespace CCBFC.Common
{
    public class LogEntity
    {
        private static readonly Dictionary<string,StringBuilder> LogDirectory;
        private static readonly object FileLock;
        public string DirectoryPath { get; private set; }
        public string FileName { get; private set; }
        private static bool _start = true;
        /// <summary>
        /// 每写入10000次清理一次无用key
        /// </summary>
        private const int ResetCount = 10000;

        private static int _currentCount = 0;
        public LogEntity(bool userDefaultRoot, params string[] paths)
        {
            if (paths == null) throw new ArgumentNullException("paths");
            if (!paths.Any()) throw new Exception("至少输入一个路径");
            var logPath = "";
            if (userDefaultRoot) logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            logPath = paths.Aggregate(logPath, Path.Combine);
            Init(logPath); ;
        }

        private void Init(string logPath)
        {
            if (Path.HasExtension(logPath))
            {
                FileName = Path.GetFileName(logPath);
                DirectoryPath = Path.GetDirectoryName(logPath);
            }
            else
            {
                FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".log";
                DirectoryPath = logPath;
            }
        }

        static LogEntity()
        {
            FileLock = new object();
            var writeThread = new Thread(StartWriter);
            LogDirectory = new Dictionary<string, StringBuilder>();
            writeThread.IsBackground = true;
            writeThread.Start();
        }

        //private static string ConvertToLogFormat(string content)
        //{
        //    return string.Format("{0}: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), content);
        //}

        public static void Log(string fileName, string content)
        {
            lock (LogDirectory)
            {
                StringBuilder buffer;
                if (LogDirectory.TryGetValue(fileName, out buffer))
                {
                    buffer.AppendLine(content);
                }
                else
                {
                    LogDirectory.Add(fileName, new StringBuilder().AppendLine(content));
                }
            }
        }

        public static void ErrorLog(string fileName, string content)
        {
            Log(fileName, content);
        }
        public static void ErrorLog(string fileName, params Exception[] exceptions)
        {
            foreach (var exception in exceptions)
            {
                ErrorLog(fileName, exception.Message);
                ErrorLog(fileName, exception.StackTrace);
            }
        }
        private void Log(string fileName,string msg, LogType type)
        {
            fileName = CommonHelper.PathCombine(DirectoryPath, DateTime.Now.ToString("yyyyMMdd"),
                type.GetValue(), fileName);

            if (GetFileSize(fileName) > 1024)
            {
                FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".log";
                fileName = CommonHelper.PathCombine(DirectoryPath, DateTime.Now.ToString("yyyyMMdd"),
                    type.ToString(), FileName);
            }
            //首先缓存 队列查找是否存在该文件,存在则直接插入尾部,可减少文件读写次数
            Log(fileName,msg);
        }
        public void AddLog(params string[] msgs)
        {
            foreach (var msg in msgs)
            {
                Log(FileName, msg, LogType.InfoLogs);
            }
        }

        public void AddLogToFile(string fileName,params string[] msgs)
        {
            foreach (var msg in msgs)
            {
                Log(fileName, msg, LogType.InfoLogs);
            }

        }
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="msgs"></param>
        public void AddWarningLog(params string[] msgs)
        {
            foreach (var msg in msgs)
            {
                Log(FileName,msg, LogType.WarningLogs);
            }
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="errors"></param>
        public void AddErrorLog(params string[] errors)
        {
            foreach (var error in errors)
            {
                Log(FileName,error, LogType.ErrorLogs);
            }
        }

        public void AddErrorLog(params Exception[] exceptions)
        {
            foreach (var exception in exceptions)
            {
                Log(FileName,exception.Message, LogType.ErrorLogs);
                Log(FileName,exception.StackTrace, LogType.ErrorLogs);
            }
        }

        private static void StartWriter()
        {
            while (_start)
            {
                bool hasLog =false;
                Monitor.Enter(LogDirectory);
                List<string> fileNames = LogDirectory.Keys.ToList();
                Monitor.Exit(LogDirectory);
                //TODO:可以用异步方式写日志
                foreach (var key in fileNames)
                {
                    hasLog = true; 
                    Monitor.Enter(LogDirectory);
                    StringBuilder builder = LogDirectory[key];
                    Monitor.Exit(LogDirectory);
                    if (builder.Length <= 0) continue;
                    Monitor.Enter(FileLock);
                    var dir = Path.GetDirectoryName(key);
                    if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                    using (var logStreamWriter = new FgStreamWriter(key, true))
                    {
                        logStreamWriter.Write(builder.ToString());
                        logStreamWriter.Flush();
                    }
                    builder.Remove(0, builder.Length);
                    _currentCount++;
                    Monitor.Exit(FileLock);
                }
                if (!hasLog)
                {
                    Thread.Sleep(100);
                }
                else if (_currentCount > ResetCount)
                {
                    Monitor.Enter(LogDirectory);
                    List<string> keys = LogDirectory.Keys.ToList();
                    Monitor.Exit(LogDirectory);
                    foreach (var key in keys)
                    {
                        lock (LogDirectory)
                        {
                            if (LogDirectory[key].Length <= 0)
                                LogDirectory.Remove(key);
                        }

                    }
                }
            }
        }

        private static long GetFileSize(string fileName)
        {
            long strRe = 0;
            if (File.Exists(fileName))
            {
                lock (FileLock)
                {
                    using (var fileStream = new FileStream(fileName, FileMode.Open))
                    {
                        strRe = fileStream.Length/1024;
                    }
                }
            }
            return strRe;
        }

        private static void CopyToBak(string sFileName)
        {
            int fileCount = 0;
            string sBakName;
            Monitor.Enter(FileLock);
            do
            {
                fileCount++;
                sBakName = sFileName + "." + fileCount + ".BAK";
            } while (File.Exists(sBakName));

            File.Copy(sFileName, sBakName);
            File.Delete(sFileName);
            Monitor.Exit(FileLock);
        }
    }
    }