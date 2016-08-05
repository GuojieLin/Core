using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	4/28/2016 2:05:46 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace Jake.Common.V35.Core.Logging
{
    /// <summary>
    /// Used to create logger instances of the given name.
    /// </summary>
    public interface ILoggerFactory : ICreateLogger, IFindLogger
    {

    }

    public interface ICreateLogger
    {
        /// <summary>
        /// Creates a new ILogger instance of the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ILogger Create(string name);

        /// <summary>
        /// Creates a new ILogger instance of the given name.
        /// </summary>
        /// <param name="useDefaultRoot">用默认文件路径</param>
        /// <param name="names"></param>
        /// <returns></returns>
        ILogger Create(bool useDefaultRoot, params string[] names);
    }
    public interface IFindLogger
    {
        /// <summary>
        /// Creates a new ILogger instance of the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ILogger Find(string name);
        /// <summary>
        /// Creates a new ILogger instance of the given name.
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        ILogger Find(params string[] names);
    }
}
