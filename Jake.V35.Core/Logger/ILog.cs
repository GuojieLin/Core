using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	4/28/2016 2:03:46 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace Jake.V35.Core.Logger
{
    /// <summary>
    /// A generic interface for logging.
    /// </summary>
    public interface ILogger
    {
        LogConfiguration Configuration { get; set; }
        string FileName { get; set; }
        string DirectoryName { get; set; }
        /// <summary>
        /// Aggregates most logging patterns to a single method.  
        /// This must be compatible with the Func representation in the OWIN environment.
        /// To check IsEnabled call WriteCore with only TraceEventType and check the return value, no event will be written.
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="content"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        /// <returns></returns>
        bool WriteCore(LogType logType, string content, Exception exception, Func<string, Exception, string> formatter);
    }
}
