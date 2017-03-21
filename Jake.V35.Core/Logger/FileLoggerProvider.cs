using System;
using System.Collections.Generic;
using System.IO;
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
        public LogConfiguration Configuration { get; set; }
        public FileLoggerProvider(LogConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// Creates a new FileLogger for the given full name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ILogger Create(string name)
        {
            return InternalCreate(name, this.Configuration);
        }

        /// <summary>
        /// Creates a new FileLogger for the given full name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public ILogger Create(string name,LogConfiguration configuration)
        {
            return InternalCreate(name, configuration);
        }
        /// <summary>
        /// 使用默认跟
        /// </summary>
        /// <param name="useDefaultRoot"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public ILogger Create(bool useDefaultRoot, LogConfiguration configuration, params string[] paths)
        {
            if (paths == null) throw new ArgumentNullException("paths");
            if (!paths.Any()) throw new Exception("至少输入一个路径");
            var logPath = "";
            if (useDefaultRoot) logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.DefaultLogDirectory);
            logPath = paths.Aggregate(logPath, Path.Combine);
            return InternalCreate(logPath, configuration);
        }
        /// <summary>
        /// 使用默认跟
        /// </summary>
        /// <param name="useDefaultRoot"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public ILogger Create(bool useDefaultRoot, params string[] paths)
        {
            if (paths == null) throw new ArgumentNullException("paths");
            if (!paths.Any()) throw new Exception("至少输入一个路径");
            var logPath = "";
            if (useDefaultRoot) logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.DefaultLogDirectory);
            logPath = paths.Aggregate(logPath, Path.Combine);
            return InternalCreate(logPath,this.Configuration);
        }
        private ILogger InternalCreate(string name, LogConfiguration configuration)
        {
            return new FileLogger(name, configuration);
        }
        public void Close()
        {
            //释放日志服务
            FileLogger.Dispose();
        }
    }
}
