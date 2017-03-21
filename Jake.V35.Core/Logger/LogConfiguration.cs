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
        /// 日志格式
        /// </summary>
        public string DatePattern { get; set; }

        /// <summary>
        /// 日志格式
        /// </summary>
        public string AutoFileNameDateFormat { get; set; }

        public LogConfiguration()
        {
            BasePath = AppDomain.CurrentDomain.BaseDirectory;
            MaxFileSize = 5242880;
            DatePattern = "yyyyMMdd"; 
            AutoFileNameDateFormat = "yyyyMMddHHmmss";
        }
    }
}
