using System;
using System.IO;
using System.Threading;
using Jake.V35.Core.Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jake.V35.Test
{
    [TestClass]
    public class LogTest
    {
        [TestMethod]
        public void LogConfigTest()
        {
            ILogger logger = FileLoggerFactory.Default.Create("Test.log");
            LogConfiguration logConfiguration = new LogConfiguration();
            Assert.AreEqual(logger.FileName, "Test.log");
            Assert.AreEqual(logger.DirectoryName, Path.Combine(logConfiguration.BasePath, "Logs\\"));
        }
        [TestMethod]
        public void LogConfig2Test()
        {
            LogConfiguration logConfiguration = new LogConfiguration();
            logConfiguration.BasePath = "c:\\";
            FileLoggerFactory factory = new FileLoggerFactory(new FileLoggerProvider(logConfiguration));
            ILogger logger = factory.Create("Test.log");
            Assert.AreEqual(logger.FileName, "Test.log");
            Console.WriteLine(logger.DirectoryName);
            Console.WriteLine(logger.FileName);
            Assert.AreEqual(logger.DirectoryName, Path.Combine(logConfiguration.BasePath, "Logs\\"));
            logger.Configuration = new LogConfiguration() {BasePath = "D:\\"};
            Console.WriteLine(logger.DirectoryName);
            Assert.AreEqual(logger.DirectoryName, "D:\\Logs\\");
            ILogger logger1 = factory.Create("Test2.log");
            Assert.AreEqual(logger1.FileName, "Test2.log");
            Console.WriteLine(logger1.DirectoryName);
            Console.WriteLine(logger1.FileName);
            Assert.AreEqual(logger1.DirectoryName, Path.Combine(logConfiguration.BasePath, "Logs\\"));
        }
        [TestMethod]
        public void LoggerInfo10Test()
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
        [TestMethod]
        public void LoggerInfo100Test()
        {
            var loggerFactory = FileLoggerFactory.Default;
            ILogger logger = loggerFactory.Create("Test100.log");

            string path = Path.Combine(logger.DirectoryName, logger.FileName);
            if(File.Exists(path))  File.Delete(path);
            for (int i = 0; i < 100; i++)
            {
                logger.WriteInfo("测试一下");
            }
        }
        [TestMethod]
        public void LoggerInfo1000Test()
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
        [TestMethod]
        public void LoggerError10Test()
        {
            var loggerFactory = FileLoggerFactory.Default;
            ILogger logger2 = loggerFactory.Create("Test10.log");

            for (int i = 0; i < 10; i++)
            {
                logger2.WriteError("测试2下",new Exception("测试错误"));
            }

        }
        [TestMethod]
        public void LoggerError100Test()
        {
            var loggerFactory = FileLoggerFactory.Default;
            ILogger logger2 = loggerFactory.Create("Test100.log");

            for (int i = 0; i < 100; i++)
            {
                logger2.WriteError("测试2下", new Exception("测试错误"));
            }

        }
        [TestMethod]
        public void LoggerError1000Test()
        {
            var loggerFactory = FileLoggerFactory.Default;
            ILogger logger2 = loggerFactory.Create("Test1000.log");

            for (int i = 0; i < 1000; i++)
            {
                logger2.WriteError("测试2下", new Exception("测试错误"));
            }

        }
        [TestMethod]
        public void LoggerErrorLargeTest()
        {
            var loggerFactory = FileLoggerFactory.Default;
            ILogger logger2 = loggerFactory.Create("Testlarge.log");

            for (int i = 0; i < 100000; i++)
            {
                logger2.WriteError("测试2下".PadLeft(10000,'t'), new Exception("测试错误"));
            }
        }
        [TestMethod]
        public void LoggerMoreTest()
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
        [TestMethod]
        public void LoggerMore2Test()
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

        [TestMethod]
        public void LoggerMoreWrite()
        {
            for (int i = 0; i < 1; i++)
            {
                Thread t = new Thread(() =>
                {
                    while (true)
                    {
                        ILogger logger1 = FileLoggerFactory.Default.Create("TestMore.log");
                        logger1.WriteInfo("test");
                        Thread.Sleep(1);
                    }
                });
                t.IsBackground = true;
                t.Start();
            }
            while(true) Thread.Sleep(1000);
        }
    }
}
