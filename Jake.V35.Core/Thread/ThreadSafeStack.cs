using System.Collections.Generic;
using System.Threading;

namespace Jake.V35.Core.Thread
{
    public class ThreadSafeStack<T> : Stack<T>
    {
        public ThreadSafeStack()
        {
        }
        public ThreadSafeStack(int capacity)
            : base(capacity)
        {
        }

        public ThreadSafeStack(IEnumerable<T> iEnumerable)
            : base(iEnumerable)
        {
        }

        public new void Push(T item)
        {
            lock (this)
            {
                base.Push(item);
                if (this.Count == 1)
                {
                    //This means we have gone from empty stack to stack with 1 item.
                    //So, wake Pop().
                    Monitor.PulseAll(this);
                }
            }
        }

        public new T Pop()
        {
            lock (this)
            {
                //不能用if,否则有并发问题，导致队列是空
                while (this.Count == 0)
                {
                    //Stack is empty. Wait until Pulse is received from Push().
                    Monitor.Wait(this);
                }
                T item = base.Pop();
                return item;
            }
        }
    }
}
