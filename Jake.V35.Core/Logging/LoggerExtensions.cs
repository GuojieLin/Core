using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	4/28/2016 2:06:35 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace Jake.Common.V35.Core.Logging
{
    /// <summary>
    /// ILogger extension methods for common scenarios.
    /// </summary>
    public static class LoggerExtensions
    {
        private static string NowTimeString {
            get { return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); }
        }

        private static readonly Func<string, Exception, string> TheMessage =
            (message, error) =>
                string.Format(CultureInfo.CurrentCulture, "{0}: {1}", NowTimeString, (string) message);

        private static readonly Func<string, Exception, string> TheMessageAndError =
            (message, error) =>
                string.Format(CultureInfo.CurrentCulture, "{0}: {1}\r\n{2}", NowTimeString, message, error.Message);

        private static readonly Func<string, Exception, string> TheErrorMessageAndStackTrace =
            (message, error) =>
                string.Format(CultureInfo.CurrentCulture, "{0}: {1}\r\n{2}", NowTimeString, error.Message, error.StackTrace);

        /// <summary>
        /// Writes an informational log message.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        public static void WriteInfo(this ILogger logger, string message)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            logger.WriteCore(LogType.Info, message, null, TheMessage);
        }

        /// <summary>
        /// Writes a warning log message.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void WriteWarning(this ILogger logger, string message, params string[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            logger.WriteCore(LogType.Warning, string.Format(CultureInfo.InvariantCulture, message, args), null, TheMessage);
        }

        /// <summary>
        /// Writes a warning log message.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="error"></param>
        public static void WriteWarning(this ILogger logger, string message, Exception error)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            logger.WriteCore(LogType.Warning, message, error, TheMessageAndError);
        }

        /// <summary>
        /// Writes an error log message.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        public static void WriteError(this ILogger logger, string message)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            logger.WriteCore(LogType.Error, message, null, TheMessage);
        }

        /// <summary>
        /// Writes an error log message.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="error"></param>
        public static void WriteError(this ILogger logger, string message, Exception error)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            logger.WriteCore(LogType.Error, message, error, TheMessageAndError);
        }
    }
}
