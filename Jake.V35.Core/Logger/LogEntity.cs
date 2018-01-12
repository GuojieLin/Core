using System;
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
        private int _syncIndex = 0;
        public LogConfiguration Configuration { get; private set; }
        public StreamWriter StreamWriter { get; private set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; private set; }
        //单个日志文件最大5M
        public LogEntity(LogConfiguration configuration,string dictionaryName, string fileName)
        {
            StringBuilder = new StringBuilder(1024);
            Configuration = configuration;
            DictionaryName = dictionaryName;
            FileName = fileName;
            CreateTime = DateTime.Now;
        }
        public string FileName { get; private set; }
        public string DictionaryName { get; private set; }
        public string FullName { get { return Path.Combine(DictionaryName, FileName); } }
        public bool IsDispose { get; private set; }
        private bool _IsDisposing;
        /// <summary>
        /// 实际偏移量，即当前实际写下的日志长度，不用读取文件也可以知道实际大小
        /// </summary>
        private int _Offset;

        private bool _Reset = true;
        private int _FlushCount;
        public StringBuilder StringBuilder { get; private set; }
        public int Length { get { return StringBuilder.Length; } }

        public void Append(string msg)
        {
            if(IsDispose) throw new Exception("当前对象已释放");
            lock (this)
            {
                StringBuilder.Append(msg);
            }
        }

        private void GetNewName()
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

        public void Write()
        {
            if (IsDispose) throw new Exception("当前对象已释放");
            int temp = 0;
            do
            {
                if (StringBuilder.Length <= 0) return;
                //总大小减去偏移量大于当前日志
                if (Configuration.MaxFileSize - _Offset > StringBuilder.Length)
                {
                    //可以直接写当前全部内容
                    temp = StringBuilder.Length;
                }
                else
                {
                    temp = Configuration.MaxFileSize - _Offset;
                }
                if (temp <= 0)
                {
                    string oldName = this.FileName;
                    GetNewName();
                    Debug.Assert(oldName != FileName);
                    //恢复偏移量
                    _Offset = 0;
                    _FlushCount = 0;
                    //新文件需要重置为true
                    _Reset = true;
                }
            } while (temp <= 0);
            EnsureDirectory();
            if (_Reset)
            {
                if (StreamWriter != null)
                {
                    //先释放原来的
                    StreamWriter.Flush();
                    StreamWriter.Close();
                    StreamWriter.Dispose();
                }
                //正在停止则不在处理后续
                if (_IsDisposing) return;
                StreamWriter = new StreamWriter(FullName, true, Encoding.UTF8);
                _Reset = false;
            }
            //尽量一次性写入，减少频繁文件开关
            StreamWriter.Write(StringBuilder.ToString(0, temp));
            //判断是否写入
            lock (this)
            {
                StringBuilder.Remove(0, temp);
            }
            //当前偏移量达到最大，则更换日志名称
            _Offset += temp;
            if (_Offset/Configuration.FlushSize > _FlushCount)
            {
                //判断是否达到写入大小
                StreamWriter.Flush();
                _FlushCount = _Offset / Configuration.FlushSize;
            }
        }

        private void EnsureDirectory()
        {
            var dir = Path.GetDirectoryName(DictionaryName);
            Debug.Assert(dir != null);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir); 
        }
        public void Dispose()
        {
            if(IsDispose) throw new Exception("当前对象已释放");
            Dispose(true);
        }
        private void Dispose(bool dispose)
        {
            if (!dispose) return;
            _IsDisposing = true;
            this.Write();
            //流写入硬盘
            StreamWriter.Flush();
            StreamWriter.Close();
            StreamWriter.Dispose();
            IsDispose = true;
        }
    }
}
