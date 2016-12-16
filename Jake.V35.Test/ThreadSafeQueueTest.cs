using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Jake.V35.Core.Logger;
using Jake.V35.Core.Thread;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jake.V35.Test
{
    [TestClass]
    public class ThreadSafeQueueTest
    {

        private ThreadSafeQueue<string> SafeQueue;
        [TestInitialize]
        public void Init()
        {
            SafeQueue = new ThreadSafeQueue<string>();
        }
        [TestMethod]
        public void LoggerInfo10Test()
        {
            SafeQueue.Enqueue("test");
            SafeQueue.Enqueue("test");
            Thread t = new Thread(() =>
            {
                while (true)
                {
                    string test = SafeQueue.Dequeue();
                    Thread.Sleep(1000);
                }

            });
            t.Start();
            while (true)
            {
                int count = 0;
                lock (SafeQueue)
                {
                    Debug.Write("OutA ");
                    count = SafeQueue.Count;
                    Debug.WriteLine("OutB ");
                }
                Debug.WriteLine(count);
            }
        }
        
    }
}
