using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Jake.V35.Core.Async;
using Jake.V35.Core.Logger;

namespace Jake.V35.Console.Test
{
    public class a : IEquatable<a>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public a(int id,string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^ this.Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as a;
            if (other == null) return false;
            return Equals(other);
        }

        public override string ToString()
        {
            return this.Id + "," + this.Name;
        }

        public bool Equals(a other)
        {
            return this.Id == other.Id && this.Name == other.Name;
        }
    }
    class Program
    {
        static void Main(string[] args)

        {
            //new Thread(AsyncTest2) {IsBackground = true}.Start();
            
            //AsyncTest2();
            //AsyncTest3();
            //System.Console.ReadKey();
            //a a1 = new a(1,"abc");
            //a a2 = new a(2,"abc");
            //a a3= new a(2,"123");
            //a a4=  new a(1, "abc");
            //HashSet<a> ha = new HashSet<a>();
            //new Thread(() =>
            //{
            //    while(true)
            //    for (int i = 0; i < 1000; i ++)
            //    {
            //        ha.Add(new a(i, "abc"));
            //    }
            //}).Start();
            //new Thread(() =>
            //{
            //    while (true)
            //        for (int i = 0; i < 1000; i++)
            //        {
            //            ha.Add(new a(i, "abc"));
            //        }
            //}).Start();
            //while (true)
            //{
            //    Thread.Sleep(1000);
            //}
            //var r1 = ha.Add(a1);
            //var r2 = ha.Add(a2);
            //var r3 = ha.Add(a3);
            //var r4 = ha.Add(a4);
            //var r = r1;
            //r1 = r2 ;
            //r2= r3;
            //r3= r4;
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            LoggerInfo10Test();
            stopwatch.Stop();
            stopwatch.Stop();
            System.Console.WriteLine("LoggerInfo10Test：" + stopwatch.ElapsedMilliseconds + "s");
            stopwatch.Start();
            LoggerInfo100Test();
            stopwatch.Stop();
            System.Console.WriteLine("LoggerInfo100Test：" + stopwatch.ElapsedMilliseconds +"ms");
            stopwatch.Reset();
            stopwatch.Start();
            LoggerInfo1000Test();
            stopwatch.Stop();
            System.Console.WriteLine("LoggerInfo1000Test：" + stopwatch.ElapsedMilliseconds +"ms");
            stopwatch.Reset();
            stopwatch.Start();
            LoggerError10Test();
            stopwatch.Stop();
            System.Console.WriteLine("LoggerError10Test：" + stopwatch.ElapsedMilliseconds +"ms");
            stopwatch.Reset();
            stopwatch.Start();
            LoggerError100Test();
            stopwatch.Stop();
            System.Console.WriteLine("LoggerError100Test：" + stopwatch.ElapsedMilliseconds +"ms");
            stopwatch.Reset();
            stopwatch.Start();
            LoggerError1000Test();
            stopwatch.Stop();
            System.Console.WriteLine("LoggerError1000Test：" + stopwatch.ElapsedMilliseconds +"ms");
            stopwatch.Reset();
            stopwatch.Start();
            LoggerErrorLargeTest();
            stopwatch.Stop();
            System.Console.WriteLine("LoggerErrorLargeTest：" + stopwatch.ElapsedMilliseconds +"ms");
            stopwatch.Reset();
            stopwatch.Start();
            LoggerMoreTest();
            stopwatch.Stop();
            System.Console.WriteLine("LoggerMoreTest：" + stopwatch.ElapsedMilliseconds + "ms");
            stopwatch.Start();
            stopwatch.Reset();
            LoggerMore2Test();
            stopwatch.Stop();
            System.Console.WriteLine("LoggerMore2Test：" + stopwatch.ElapsedMilliseconds +"ms");
            stopwatch.Reset();
            stopwatch.Start();
            Logger100000Test();
            stopwatch.Stop();
            System.Console.WriteLine("Logger100000Test：" + stopwatch.ElapsedMilliseconds +"ms");
            stopwatch.Reset();
            stopwatch.Start();
            Logger1000000Test();
            stopwatch.Stop();
            System.Console.WriteLine("Logger1000000Test：" + stopwatch.ElapsedMilliseconds +"ms");
            stopwatch.Reset();
            stopwatch.Start();
            LoggerMore1000Test();
            stopwatch.Stop();
            System.Console.WriteLine("LoggerMore1000Test：" + stopwatch.ElapsedMilliseconds +"ms");
            stopwatch.Reset();
            System.Console.ReadKey();
        }

        private static void AsyncTest1()
        {
            ILogger logger = FileLoggerFactory.Default.Create(true, "AsyncTest1.txt");
            List<Operator> operators  = new List<Operator>();
            for (int i = 0; i < 100; i ++)
            {
                int j = i;
                var @operator = Asynchronous.Invoke(() => { logger.WriteInfo(j.ToString()); });
                operators.Add(@operator);
            }
            while (true)
            {
                Thread.Sleep(1000);
            }
            //operators.ForEach(o => o.Invoke());
        }
        private static void AsyncTest2()
        {
            ILogger logger = FileLoggerFactory.Default.Create(true, "AsyncTest2.txt");
            for (int i = 100; i < 200; i++)
            {
                int j = i;
                Asynchronous.Invoke(() => { logger.WriteInfo(j.ToString()); });
            }
        }

        private static void AsyncTest3()
        {
            ILogger logger = FileLoggerFactory.Default.Create(true, "AsyncTest3.txt");
            for (int i = 200; i < 250; i++)
            {
                int j = i;
                Asynchronous.Invoke(() => { logger.WriteInfo(j.ToString()); })
                    .ContinueWithAsync(() =>
                    {
                        logger.WriteInfo((j * 2).ToString());
                    });
            }
        }
        public static void Logger1000000Test()
        {
            var loggerFactory = FileLoggerFactory.Default;
            ILogger logger = loggerFactory.Create("Test1000000.log");

            string path = Path.Combine(logger.DirectoryName, logger.FileName);
            if (File.Exists(path)) File.Delete(path);
            for (int i = 0; i < 1000000; i++)
            {
                logger.WriteInfo("测试一下");
            }
        }
        public static void Logger100000Test()
        {
            var loggerFactory = FileLoggerFactory.Default;
            ILogger logger = loggerFactory.Create("Test100000.log");

            string path = Path.Combine(logger.DirectoryName, logger.FileName);
            if (File.Exists(path)) File.Delete(path);
            for (int i = 0; i < 100000; i++)
            {
                logger.WriteInfo("测试一下");
            }
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
        public static void LoggerMore1000Test()
        {
            var loggerFactory = FileLoggerFactory.Default;
            for (int i = 0; i < 1000; i++)
            {
                ILogger logger = loggerFactory.Create("TestMore" + i + "Test.log");

                string path = Path.Combine(logger.DirectoryName, logger.FileName);
                if (File.Exists(path)) File.Delete(path);
                logger.WriteInfo("测试多文件日志".PadLeft(10000, 't'));
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
