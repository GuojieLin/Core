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
    public class LogInfo
    {
        public static object _syncLock = new object();
        public const int MaxSize = 1024 * 1024 * 5;
        public LogInfo(string dictionaryName,string fileName)
        {
            StringBuilder = new StringBuilder(1024);
            DictionaryName = dictionaryName;
            FileName = fileName;
        }
        public string FileName { get; private set;}
        public string DictionaryName { get; private set; }
        public string FullName { get { return Path.Combine(DictionaryName, FileName); } }
        /// <summary>
        /// 当前长度
        /// </summary>
        public long Length { get; private set; }

        /// <summary>
        /// 实际偏移量，即当前实际写下的日志长度，不用读取文件也可以知道实际大小
        /// </summary>
        private int _offset;
        public StringBuilder StringBuilder { get; private set; }

        public void Append(string msg)
        {
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
                this.FileName = fn + "_" + DateTime.Now.ToString(Constants.AutoFileNameFormat) + ".log";
            }
        }

        public void Write()
        {
            int temp = 0;

            lock (this)
            {
                do
                {
                    //总大小减去偏移量大于当前日志
                    if (MaxSize - _offset > StringBuilder.Length)
                    {
                        //可以直接写当前全部内容
                        temp = StringBuilder.Length;
                    }
                    else
                    {
                        temp = MaxSize - _offset;
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
            var dir = Path.GetDirectoryName(DictionaryName);
            Debug.Assert(dir != null);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
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

    }
}
