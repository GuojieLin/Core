using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
    /// Provides an ILoggerFactory based on FileSystem.
    /// </summary>
    public class FileLoggerFactory : ILoggerProvider
    {
        private readonly FileLoggerProvider _fileLoggerProvider;
        static FileLoggerFactory()
        {
            Default = new FileLoggerFactory();
        }

        public FileLoggerFactory()
        {
            _fileLoggerProvider = new FileLoggerProvider();
        }

        public FileLoggerFactory(FileLoggerProvider fileLoggerProvider)
        {
            _fileLoggerProvider = fileLoggerProvider;
        }
        /// <summary>
        /// Provides a default ILoggerFactory based on System.Diagnostics.TraceSorce.
        /// </summary>
        public static ILoggerProvider Default { get; set; }
        /// <summary>
        /// Creates a new FileLogger for the given full name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ILogger Create(string name)
        {
            return _fileLoggerProvider.Create(name);
        }
        /// <summary>
        /// 使用默认跟
        /// </summary>
        /// <param name="useDefaultRoot"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public ILogger Create(bool useDefaultRoot, params string[] names)
        {
            return _fileLoggerProvider.Create(useDefaultRoot,names);
        }
    }
}
