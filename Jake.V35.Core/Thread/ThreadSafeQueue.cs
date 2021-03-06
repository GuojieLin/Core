﻿using System.Collections.Generic;
using System.Threading;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	8/9/2016 4:27:16 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace Jake.V35.Core.Thread
{
    /// <summary>
    /// 当队列最后一个被取出时若不等待，则可能会取出null的情况
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ThreadSafeQueue<T> : Queue<T>
    {
        public ThreadSafeQueue()
        {
        }
        public ThreadSafeQueue(int capacity)
            : base(capacity)
        {
        }

        public ThreadSafeQueue(IEnumerable<T> iEnumerable)
            : base(iEnumerable)
        {
        }
        public new void Enqueue(T item)
        {
            lock (this)
            {
                base.Enqueue(item);  
                //发送消息给所有等待对象
                if (this.Count == 1) Monitor.PulseAll(this);
            }
        }

        public new T Dequeue()
        {
            lock (this)
            {
                //不能用if,否则有并发问题，导致队列是空
                while (this.Count == 0)
                    //这里会释放锁
                    Monitor.Wait(this);
                return base.Dequeue();
            }
        }
    }
}
