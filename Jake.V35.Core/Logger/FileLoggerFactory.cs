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
    /// Provides an ILoggerFactory based on FileSystem.
    /// </summary>
    public class FileLoggerFactory : ILoggerFactory
    {
        private static readonly Dictionary<string, ILogger> LoggerContainer = new Dictionary<string, ILogger>(StringComparer.OrdinalIgnoreCase);

        static FileLoggerFactory()
        {
            Default = new FileLoggerFactory();
        }
        /// <summary>
        /// Provides a default ILoggerFactory based on System.Diagnostics.TraceSorce.
        /// </summary>
        public static ILoggerFactory Default { get; set; }
        /// <summary>
        /// Creates a new FileLogger for the given full name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ILogger Create(string name)
        {
            lock (LoggerContainer)
            {
                var logger = Find(name);
                if (logger == null)
                {
                    logger = new FileLogger(name);
                    LoggerContainer.Add(name, logger);
                }
                return logger;
            }
        }
        /// <summary>
        /// 使用默认跟
        /// </summary>
        /// <param name="useDefaultRoot"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public ILogger Create(bool useDefaultRoot, params string[] names)
        {
            string newName = string.Join("_", names);
            lock (LoggerContainer)
            {
                var logger = Find(newName);
                if (logger == null)
                {
                    logger = new FileLogger(useDefaultRoot, names);
                    LoggerContainer.Add(newName, logger);
                }
                return logger;
            }
        }

        public ILogger Find(string name)
        {
            lock (LoggerContainer)
            {
                ILogger logger;
                return LoggerContainer.TryGetValue(name, out logger) ? logger : null;
            }
        }

        public ILogger Find(params string[] names)
        {
            lock (LoggerContainer)
            {
                ILogger logger;
                return LoggerContainer.TryGetValue(string.Join("_", names), out logger) ? logger : null;
            }
        }
    }
}
