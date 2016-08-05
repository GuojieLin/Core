using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	4/28/2016 2:03:46 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace Jake.Common.V35.Core.Logging
{
    public enum LogType
    {
        [Value("InfoLogs")] Info,
        [Value("WarningLogs")] Warning,
        [Value("ErrorLogs")] Error
    }

}
