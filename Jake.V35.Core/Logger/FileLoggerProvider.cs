using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	8/26/2016 8:25:32 AM			//
//			创建日期:	2016				            //
//======================================================//
namespace Jake.V35.Core.Logger
{
    public class FileLoggerProvider : ILoggerProvider
    {
        /// <summary>
        /// Creates a new FileLogger for the given full name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ILogger Create(string name)
        {
            var logger = new FileLogger(name);
            return logger;
        }
        /// <summary>
        /// 使用默认跟
        /// </summary>
        /// <param name="useDefaultRoot"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public ILogger Create(bool useDefaultRoot, params string[] names)
        {
            var logger = new FileLogger(useDefaultRoot, names);
            return logger;
        }
    }
}
