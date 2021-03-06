﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	8/1/2016 5:46:32 PM			    //
//			创建日期:	2016				            //
//======================================================//
namespace Jake.V35.Core.Logger
{
    public class EmptyLogger : ILogger
    {
        public string FileName { get; set; }
        public string DirectoryName { get; set; }
        public string Id { get; private set; }
        public LogConfiguration Configuration { get; set; }
        public EmptyLogger()
        {
            Configuration = new LogConfiguration();
        }
        bool ILogger.WriteCore(LogType logType, string content, Exception exception,
            Func<string, Exception, string> formatter)
        {
            return true;
        }
    }
}
