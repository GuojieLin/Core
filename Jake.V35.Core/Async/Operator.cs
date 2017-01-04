using System;
using Jake.V35.Core.Async.Interfaces;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	12/19/2015 1:19:39 PM			//
//			创建日期:	2015				            //
//======================================================//
//2015.12.19    Operate基类,IFunc异步操作接口
//              实现了基本操作,处理开始调用及处理完成回调事件
//              实现Operation类
//2016.1.7      添加异常机制,异常保存在Exception
//2016.4.26     添加异步操作完继续操作5，6，7，8个参数的重载
                
namespace Jake.V35.Core.Async
{

    /// <summary>
    /// 异步操作
    /// </summary>
    public abstract class Operator : IOperationAsync, IContinueWithAsync
    {
        public IAsyncResult Middle;
        public readonly string Id;
        public Exception Exception { get; private set; }
        public Operator Previous { get; set; }
        public Operator Next { get; set; }
        protected Operator()
        {
            Id = Guid.NewGuid().ToString();
        }

        public abstract IAsyncResult Invoke();
        protected void SetAsyncResult(IAsyncResult result)
        {
            this.Middle = result;
        }

        public virtual void Wait()
        {
            if (!Middle.IsCompleted) Middle.AsyncWaitHandle.WaitOne();
        }

        public virtual void CompletedCallBack(IAsyncResult ar)
        {
        }

        public void CatchException(Exception exception)
        {
            this.Exception = exception;
        }

        protected Operator ContinueAsync()
        {
            if (Next != null) Next.Invoke();
            return Next;
        }
        public virtual ActionAsync ContinueWithAsync(Action action)
        {
            Next = new ActionAsync(action);
            Next.Previous = this;
            return (ActionAsync) Next;
        }

        public virtual ActionAsync<Operator> ContinueWithAsync(Action<Operator> action)
        {
            Next = new ActionAsync<Operator>(action, this);
            Next.Previous = this;
            return (ActionAsync<Operator>)Next;
        }

        public virtual ActionAsync<Operator, TParameter> ContinueWithAsync<TParameter>(Action<Operator, TParameter> action, TParameter parameter)
        {
            Next = new ActionAsync<Operator, TParameter>(action, this, parameter);
            Next.Previous = this;
            return (ActionAsync<Operator, TParameter>)Next;
        }

        public virtual ActionAsync<Operator, TParameter1, TParameter2> ContinueWithAsync<TParameter1, TParameter2>(
            Action<Operator, TParameter1, TParameter2> action, TParameter1 parameter1, TParameter2 parameter2)
        {
            Next = new ActionAsync<Operator, TParameter1, TParameter2>(action, this, parameter1, parameter2);
            Next.Previous = this;
            return (ActionAsync<Operator, TParameter1, TParameter2>)Next;
        }

        public virtual ActionAsync<Operator, TParameter1, TParameter2, TParameter3> ContinueWithAsync<TParameter1, TParameter2, TParameter3>
            (Action<Operator, TParameter1, TParameter2, TParameter3> action,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3)
        {
            Next = new ActionAsync<Operator, TParameter1, TParameter2, TParameter3>(action, this,parameter1, parameter2, parameter3);
            Next.Previous = this;
            return (ActionAsync<Operator, TParameter1, TParameter2, TParameter3>)Next;
        }
        public virtual ActionAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4>
            ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4>(Action<Operator, TParameter1, TParameter2, TParameter3, TParameter4> action, 
            TParameter1 parameter1, 
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4)
        {
            Next = new ActionAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4>
                (action,this, parameter1, parameter2, parameter3,parameter4);
            Next.Previous = this;
            return (ActionAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4>)Next;
        }

        public virtual ActionAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>
            ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>(Action<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5> action,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4,
            TParameter5 parameter5)
        {
            Next = new ActionAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>
                (action, this,parameter1, parameter2, parameter3, parameter4, parameter5);
            Next.Previous = this;
            return (ActionAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>)Next;
        }

        public ActionAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>
            ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>(Action<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6> action,
            TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4,
            TParameter5 parameter5, TParameter6 parameter6)
        {
            Next = new ActionAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>
                (action,this, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
            Next.Previous = this;
            return (ActionAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>)Next;
        }

        public ActionAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>
            ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>(
            Action<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7> action, TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4,
            TParameter5 parameter5, TParameter6 parameter6, TParameter7 parameter7)
        {
            Next = new ActionAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>
                (action,this, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7);
            Next.Previous = this;
            return (ActionAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>)Next;
        }

        //public Operator ContinueWithAsync
        //    <TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>(
        //    Action<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8> action, TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4,
        //    TParameter5 parameter5, TParameter6 parameter6, TParameter7 parameter7, TParameter8 parameter8)
        //{
        //    Next = new ActionAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>
        //        (action, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8);
        //    Next.Previous = this;
        //    return Next;
        //}

        public virtual FuncAsync<TResult> ContinueWithAsync<TResult>(Func<TResult> func)
        {
            Next = new FuncAsync<TResult>(func);
            Next.Previous = this;
            return (FuncAsync<TResult>)Next;
        }
        public virtual FuncAsync<Operator, TResult> ContinueWithAsync<TResult>(Func<Operator, TResult> func)
        {
            Next = new FuncAsync<Operator, TResult>(func, this);
            Next.Previous = this;
            return (FuncAsync<Operator, TResult>)Next;
        }

        /// <summary>
        /// 异步调用方法
        /// 自动从线程池获取一个空闲线程去去执行操作
        /// </summary>
        /// <param name="func"></param>
        /// <param name="parameter"></param>
        public virtual FuncAsync<Operator, TParameter, TResult> ContinueWithAsync<TParameter, TResult>(Func<Operator, TParameter, TResult> func,
            TParameter parameter)
        {
            Next = new FuncAsync<Operator, TParameter, TResult>(func,this, parameter);
            Next.Previous = this;
            return (FuncAsync<Operator, TParameter, TResult>)Next;
        }

        /// <summary>
        /// 异步调用方法
        /// 自动从线程池获取一个空闲线程去执行操作
        /// </summary>
        /// <param name="func"></param>
        /// <param name="parameter1"></param>
        /// <param name="parameter2"></param>
        public virtual FuncAsync<Operator, TParameter1, TParameter2, TResult> ContinueWithAsync<TParameter1, TParameter2, TResult>
            (Func<Operator, TParameter1, TParameter2, TResult> func,
            TParameter1 parameter1, 
            TParameter2 parameter2)
        {
            Next = new FuncAsync<Operator, TParameter1, TParameter2, TResult>(func, this,parameter1, parameter2);
            Next.Previous = this;
            return (FuncAsync<Operator, TParameter1, TParameter2, TResult>)Next;
        }

        public FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TResult> ContinueWithAsync<TParameter1, TParameter2, TParameter3, TResult>(Func<Operator, TParameter1, TParameter2, TParameter3, TResult> func, 
            TParameter1 parameter1, 
            TParameter2 parameter2, 
            TParameter3 parameter3)
        {
            Next = new FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TResult>
                (func,this, parameter1, parameter2, parameter3);
            Next.Previous = this;
            return (FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TResult>)Next;
        }

        public virtual FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TResult> ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TResult>(Func<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TResult> func,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4)
        {
            Next = new FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TResult>
                (func, this,parameter1, parameter2, parameter3, parameter4);
            Next.Previous = this;
            return (FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TResult>)Next;
        }

        public FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TResult>
            ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TResult>(Func<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TResult> func,
            TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4,
            TParameter5 parameter5)
        {
            Next = new FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TResult>
                (func, this,parameter1, parameter2, parameter3, parameter4, parameter5);
            Next.Previous = this;
            return (FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TResult>)Next;
        }

        public FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TResult>
            ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TResult>(
            Func<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TResult> func, TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4,
            TParameter5 parameter5, TParameter6 parameter6)
        {
            Next = new FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TResult>
                (func,this, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
            Next.Previous = this;
            return (FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TResult>)Next;
        }

        public FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TResult>
            ContinueWithAsync
            <TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TResult>(Func<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TResult> func,
                TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4,
                TParameter5 parameter5, TParameter6 parameter6, TParameter7 parameter7)
        {
            Next = new FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TResult>
                (func, this,parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7);
            Next.Previous = this;
            return (FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TResult>)Next;
        }

        //public Operator ContinueWithAsync
        //    <TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TResult>(
        //    Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TResult> func, TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4,
        //    TParameter5 parameter5, TParameter6 parameter6, TParameter7 parameter7, TParameter8 parameter8)
        //{
        //    Next = new FuncAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7,TParameter8, TResult>
        //        (func, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8);
        //    Next.Previous = this;
        //    return Next;
        //}
    }
}
