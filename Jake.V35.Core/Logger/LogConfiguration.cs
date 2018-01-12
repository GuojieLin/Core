using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	3/20/2017 11:54:25 PM			//
//			创建日期:	2017				            //
//======================================================//
namespace Jake.V35.Core.Logger
{
    /// <summary>
    /// 增加日志配置
    /// </summary>
    public class LogConfiguration
    {
        /// <summary>
        /// 日志存放根目录
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// 最大文件尺寸
        /// </summary>
        public int MaxFileSize { get; set; }

        /// <summary>
        /// 每次刷新流的大小
        /// </summary>
        public int FlushSize { get; set; }
        /// <summary>
        /// 日志格式
        /// </summary>
        public string DirectoryDatePattern { get; set; }

        /// <summary>
        /// 日志格式
        /// </summary>
        public string AutoFileNameDateFormat { get; set; }
        /// <summary>
        /// 日志对象多久释放一次，防止对一个日志文件少了读写时频繁打开关闭
        /// </summary>
        public TimeSpan LogAutoDisposeTime { get; set; }

        public LogConfiguration()
        {
            BasePath = AppDomain.CurrentDomain.BaseDirectory;
            MaxFileSize = 5242880;
            //至少达到5KB才写入
            FlushSize = 5120;
            DirectoryDatePattern = "yyyyMMdd"; 
            AutoFileNameDateFormat = "yyyyMMddHHmmss";
            //日志为空时至少保留60s才释放
            LogAutoDisposeTime = TimeSpan.FromSeconds(60);
        }
    }
}
