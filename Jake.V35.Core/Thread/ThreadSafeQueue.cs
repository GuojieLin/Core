using System.Collections.Generic;
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
        public new void Enqueue(T item)
        {
            lock (this)
            {
                base.Enqueue(item);
                if (this.Count == 1) Monitor.PulseAll(this);
            }
        }

        public new T Dequeue()
        {
            lock (this)
            {
                while (this.Count == 0)
                    Monitor.Wait(this);
                return base.Dequeue();
            }
        }
    }
}
