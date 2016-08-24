using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Jake.V35.Core.Logger;

namespace Jake.V35.Console.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            LoggerInfo10Test();
            stopwatch.Stop(); 
            System.Console.WriteLine("LoggerInfo10Test：" + stopwatch.ElapsedMilliseconds / 1000 + "s");
            stopwatch.Start();
            LoggerInfo100Test();
            stopwatch.Stop();
            System.Console.WriteLine("LoggerInfo100Test：" + stopwatch.ElapsedMilliseconds / 1000 + "s");
            stopwatch.Start();
            LoggerInfo1000Test();
            stopwatch.Stop();
            System.Console.WriteLine("LoggerInfo1000Test：" + stopwatch.ElapsedMilliseconds / 1000 + "s");
            stopwatch.Start();
            LoggerError10Test();
            stopwatch.Stop();
            System.Console.WriteLine("LoggerError10Test：" + stopwatch.ElapsedMilliseconds / 1000 + "s");
            stopwatch.Start();
            LoggerError100Test();
            stopwatch.Stop();
            System.Console.WriteLine("LoggerError100Test：" + stopwatch.ElapsedMilliseconds / 1000 + "s");
            stopwatch.Start();
            LoggerError1000Test();
            stopwatch.Stop();
            System.Console.WriteLine("LoggerError1000Test：" + stopwatch.ElapsedMilliseconds / 1000 + "s");
            stopwatch.Start();
            LoggerErrorLargeTest();
            stopwatch.Stop();
            System.Console.WriteLine("LoggerErrorLargeTest：" + stopwatch.ElapsedMilliseconds / 1000 + "s");
            stopwatch.Start();
            LoggerMoreTest();
            stopwatch.Stop();
            stopwatch.Start();
            System.Console.WriteLine("LoggerMoreTest：" + stopwatch.ElapsedMilliseconds / 1000 + "s");
            LoggerMore2Test();
            stopwatch.Stop();
            System.Console.WriteLine("LoggerMoreTest：" + stopwatch.ElapsedMilliseconds / 1000 + "s");
            System.Console.ReadKey();
        }

        public static void LoggerInfo10Test()
        {
            var loggerFactory = FileLoggerFactory.Default;
            ILogger logger = loggerFactory.Create("Test10.log");

            string path = Path.Combine(logger.DirectoryName, logger.FileName);
            if (File.Exists(path)) File.Delete(path);
            for (int i = 0; i < 10; i++)
            {
                logger.WriteInfo("测试一下");
            }
        }
        public static  void LoggerInfo100Test()
        {
            var loggerFactory = FileLoggerFactory.Default;
            ILogger logger = loggerFactory.Create("Test100.log");

            string path = Path.Combine(logger.DirectoryName, logger.FileName);
            if (File.Exists(path)) File.Delete(path);
            for (int i = 0; i < 100; i++)
            {
                logger.WriteInfo("测试一下");
            }
        }
        public static void LoggerInfo1000Test()
        {
            var loggerFactory = FileLoggerFactory.Default;
            ILogger logger = loggerFactory.Create("Test1000.log");

            string path = Path.Combine(logger.DirectoryName, logger.FileName);
            if (File.Exists(path)) File.Delete(path);
            for (int i = 0; i < 1000; i++)
            {
                logger.WriteInfo("测试一下");
            }
        }
        public static void LoggerError10Test()
        {
            var loggerFactory = FileLoggerFactory.Default;
            ILogger logger2 = loggerFactory.Create("Test10.log");

            for (int i = 0; i < 10; i++)
            {
                logger2.WriteError("测试2下", new Exception("测试错误"));
            }

        }
        public static void LoggerError100Test()
        {
            var loggerFactory = FileLoggerFactory.Default;
            ILogger logger2 = loggerFactory.Create("Test100.log");

            for (int i = 0; i < 100; i++)
            {
                logger2.WriteError("测试2下", new Exception("测试错误"));
            }

        }
        public static void LoggerError1000Test()
        {
            var loggerFactory = FileLoggerFactory.Default;
            ILogger logger2 = loggerFactory.Create("Test1000.log");

            for (int i = 0; i < 1000; i++)
            {
                logger2.WriteError("测试2下", new Exception("测试错误"));
            }

        }
        public static void LoggerErrorLargeTest()
        {
            var loggerFactory = FileLoggerFactory.Default;
            ILogger logger2 = loggerFactory.Create("Testlarge.log");

            for (int i = 0; i < 1000; i++)
            {
                logger2.WriteError("测试2下".PadLeft(10000, 't'), new Exception("测试错误"));
            }

        }
        public static void LoggerMoreTest()
        {
            var loggerFactory = FileLoggerFactory.Default;
            ILogger logger = loggerFactory.Create("TestMore.log");

            string path = Path.Combine(logger.DirectoryName, logger.FileName);
            if (File.Exists(path)) File.Delete(path);
            for (int i = 0; i < 100; i++)
            {
                logger.WriteInfo("测试一下");
                logger.WriteError("测试一下");
            }
        }
        public static void LoggerMore2Test()
        {
            var loggerFactory = FileLoggerFactory.Default;
            ILogger logger1 = loggerFactory.Create("TestMore1.log");
            ILogger logger2 = loggerFactory.Create("TestMore2.log");

            string path = Path.Combine(logger1.DirectoryName, logger1.FileName);
            if (File.Exists(path)) File.Delete(path);
            path = Path.Combine(logger2.DirectoryName, logger2.FileName);
            if (File.Exists(path)) File.Delete(path);
            for (int i = 0; i < 100; i++)
            {
                logger1.WriteInfo("测试more1下");
                logger2.WriteInfo("测试more2下");
            }
        }
    }
}
