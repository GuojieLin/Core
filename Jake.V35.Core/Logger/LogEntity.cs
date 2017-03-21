﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	12/27/2016 6:53:33 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace Jake.V35.Core.Logger
{
    internal class LogEntity:IDisposable
    {
        public static object _syncLock = new object();
        private int _syncIndex = 0;
        public LogConfiguration Configuration { get; private set; }
        //单个日志文件最大5M
        public LogEntity(LogConfiguration configuration,string dictionaryName, string fileName)
        {
            StringBuilder = new StringBuilder(1024);
            Configuration = configuration;
            DictionaryName = dictionaryName;
            FileName = fileName;
        }
        public string FileName { get; private set; }
        public string DictionaryName { get; private set; }
        public string FullName { get { return Path.Combine(DictionaryName, FileName); } }
        private bool _isDispose=false;
        /// <summary>
        /// 实际偏移量，即当前实际写下的日志长度，不用读取文件也可以知道实际大小
        /// </summary>
        private int _offset;
        public StringBuilder StringBuilder { get; private set; }
        public int Length { get { return StringBuilder.Length; } }

        public void Append(string msg)
        {
            if(_isDispose) throw new Exception("当前对象已释放");
            lock (this)
            {
                StringBuilder.Append(msg);
            }
        }

        private void GetNewName()
        {
            lock (_syncLock)
            {
                var fn = Path.GetFileNameWithoutExtension(FileName);
                Debug.Assert(fn != null);
                int index = fn.LastIndexOf("_", StringComparison.OrdinalIgnoreCase);
                if (index > 0)
                {
                    fn = fn.Substring(0, index);
                }
                Interlocked.Increment(ref _syncIndex);
                this.FileName = fn + "_" + _syncIndex + ".log";
            }
        }
        public void Write()
        {
            if (_isDispose) throw new Exception("当前对象已释放");
            int temp = 0;
            lock (this)
            {
                do
                {
                    //总大小减去偏移量大于当前日志
                    if (Configuration.MaxFileSize - _offset > StringBuilder.Length)
                    {
                        //可以直接写当前全部内容
                        temp = StringBuilder.Length;
                    }
                    else
                    {
                        temp = Configuration.MaxFileSize - _offset;
                    }
                    if (temp <= 0)
                    {
                        string oldName = this.FileName;
                        GetNewName();
                        Debug.Assert(oldName != FileName);
                        //恢复偏移量
                        Interlocked.Exchange(ref _offset, 0);
                    }
                } while (temp <= 0);
            }
            EnsureDirectory();
            using (var logStreamWriter = new StreamWriter(FullName, true, Encoding.UTF8))
            {
                logStreamWriter.Write(StringBuilder.ToString(0, temp));
                logStreamWriter.Flush();
            }
            lock (this)
            {
                StringBuilder.Remove(0, temp);
            }
            //当前偏移量达到最大，则更换日志名称
            Interlocked.Add(ref _offset, temp);
        }
        private void EnsureDirectory()
        {
            var dir = Path.GetDirectoryName(DictionaryName);
            Debug.Assert(dir != null);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir); 
        }
        public void Dispose()
        {
            if(!_isDispose) throw new Exception("当前对象已释放");
            Dispose(true);
        }
        private void Dispose(bool dispose = true)
        {
            if (!dispose) return;
            this.Write();
            _isDispose = true;
        }
    }
}
