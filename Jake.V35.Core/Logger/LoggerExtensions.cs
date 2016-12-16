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
namespace Jake.V35.Core.Logger
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
                string.Format(CultureInfo.CurrentCulture, "{0}: {1}\r\n", NowTimeString, (string) message);

        private static readonly Func<string, Exception, string> TheMessageAndError =
            (message, error) =>
                string.Format(CultureInfo.CurrentCulture, "{0}: {1}\r\n{2}\r\n", NowTimeString, message, error.Message);

        private static readonly Func<string, Exception, string> TheErrorMessageAndStackTrace =
            (message, error) =>
                string.Format(CultureInfo.CurrentCulture, "{0}: {1} {2}\r\n{3}\r\n", NowTimeString, message,
                    error.Message, error.StackTrace);

        /// <summary>
        /// Writes an debug log message.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        public static void WriteDebug(this ILogger logger, string message)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            Debug.WriteLine(message);
        }
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

            logger.WriteInfo(message, null);
        }
        public static void WriteInfo(this ILogger logger, string message,Exception exception)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            if (exception == null) logger.WriteCore(LogType.Info, message, null, TheMessage);
            else logger.WriteCore(LogType.Info, message, exception, TheMessageAndError);
        }

        /// <summary>
        /// Writes a warning log message.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        public static void WriteWarning(this ILogger logger, string message)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            logger.WriteWarning(message, null);
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
            if (error == null) logger.WriteCore(LogType.Warning, message, null, TheMessage);
            else logger.WriteCore(LogType.Warning, message, error, TheMessageAndError);
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
            logger.WriteError(message, null);
        }

        /// <summary>
        /// Writes an error log message.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="error"></param>
        public static void WriteError(this ILogger logger, Exception error)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            logger.WriteError("", error);
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

            if (error == null) logger.WriteCore(LogType.Error, message, null, TheMessage);
            else logger.WriteCore(LogType.Error, message, error, TheErrorMessageAndStackTrace);
        }
    }
}
