using System;
using System.IO;
using Jake.V35.Core.Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jake.V35.Test
{
    [TestClass]
    public class LogTest
    {
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
    }
}
