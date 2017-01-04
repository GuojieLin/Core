using System;
using System.Threading;
using Jake.V35.Core.Async.Interfaces;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	12/19/2015 8:54:05 PM			//
//			创建日期:	2015				            //
//======================================================//
//2015.12.19    
//2015.1.17 添加异常捕获机制
//2016.1.19 实现ContinueWithAsync,当异步处理完成后执行
//2016.4.26 FuncAsync增加5,6,7,8个参数重载
namespace Jake.V35.Core.Async
{
    public class FuncAsync<TResult> : Operator, IFuncOperationAsync<TResult>
    {
        private TResult _result;
        public bool IsComplted { get; private set; }
        private readonly AutoResetEvent _waitSignal =  new AutoResetEvent(false);
        public AutoResetEvent WaitSignal
        {
            get { return _waitSignal; }
        }
        public TResult Result
        {
            get
            {
                if (!Middle.IsCompleted || _result == null || !IsComplted)
                {
                    _result = GetResult();
                }
                return _result;
            }
        }

        private readonly Func<TResult> _func;
        public FuncAsync()
        {
        }
        public FuncAsync(Func<TResult> func)
        {
            this._func = func;
        }
        public override IAsyncResult Invoke()
        {
            var result = _func.BeginInvoke(CompletedCallBack, null);
            SetAsyncResult(result);
            return result;
        }

        public override void CompletedCallBack(IAsyncResult ar)
        {
            try
            {

                var result = _func.EndInvoke(ar);
                SetResult(result);
            }
            catch (Exception exception)
            {
                this.CatchException(exception);
                SetResult(default(TResult));
            }
            ContinueAsync();
        }

        public virtual TResult GetResult()
        {
            Wait();
            _waitSignal.WaitOne();
            return this._result;
        }

        public void SetResult(TResult result)
        {
            _result = result;
            _waitSignal.Set();
            IsComplted = true;
        }

    }
    public class FuncAsync<T1, TResult> : FuncAsync<TResult>
    {
        public T1 Parameter1 { get; protected set; }
        private readonly Func<T1, TResult> _func;

        public FuncAsync(Func<T1, TResult> action, T1 parameter1)
            : this(parameter1)
        {
            this._func = action;
        }
        protected FuncAsync(T1 parameter1)
            : base()
        {
            this.Parameter1 = parameter1;
        }

        public override IAsyncResult Invoke()
        {
            var result = _func.BeginInvoke(Parameter1, CompletedCallBack, null);
            SetAsyncResult(result);
            return result;
        }
        public override void CompletedCallBack(IAsyncResult ar)
        {
            try
            {
                var result = _func.EndInvoke(ar);
                SetResult(result);
            }
            catch (Exception exception)
            {
                CatchException(exception);
                SetResult(default(TResult));
            }
            ContinueAsync();
        }
    }
    public class FuncAsync<T1, T2, TResult> : FuncAsync<T1, TResult>
    {
        private readonly Func<T1, T2, TResult> _func;
        public T2 Parameter2 { get; protected set; }
        public FuncAsync(Func<T1, T2, TResult> func, T1 parameter1, T2 parameter2)
            : this(parameter1, parameter2)
        {
            this._func = func;
        }
        public FuncAsync(T1 parameter1, T2 parameter2)
            : base(parameter1)
        {
            this.Parameter2 = parameter2;
        }

        public override IAsyncResult Invoke()
        {
            var result = _func.BeginInvoke(Parameter1, Parameter2, CompletedCallBack, null);
            SetAsyncResult(result);
            return result;
        }
        public override void CompletedCallBack(IAsyncResult ar)
        {
            try
            {
                var result = _func.EndInvoke(ar);
                SetResult(result);
            }
            catch (Exception exception)
            {
                this.CatchException(exception);
                SetResult(default(TResult));
            }
            ContinueAsync();
        }
    }

    public class FuncAsync<T1, T2, T3, TResult> : FuncAsync<T1, T2, TResult>
    {
        private readonly Func<T1, T2, T3, TResult> _func;
        public T3 Parameter3 { get; protected set; }

        public FuncAsync(Func<T1, T2, T3, TResult> func, T1 parameter1, T2 parameter2, T3 parameter3)
            : this(parameter1, parameter2, parameter3)
        {
            this._func = func; 
        }

        public FuncAsync(T1 parameter1, T2 parameter2, T3 parameter3)
            : base(parameter1, parameter2)
        {
            this.Parameter3 = parameter3;
        }

        public override IAsyncResult Invoke()
        {
            var result = _func.BeginInvoke(Parameter1, Parameter2, Parameter3, CompletedCallBack, null);
            SetAsyncResult(result);
            return result;
        }

        public override void CompletedCallBack(IAsyncResult ar)
        {
            try
            {
                var result = _func.EndInvoke(ar);
                SetResult(result);
            }
            catch (Exception exception)
            {
                this.CatchException(exception);
                SetResult(default(TResult));
            }
            ContinueAsync();
        }
    }

    public class FuncAsync<T1, T2, T3,T4, TResult> : FuncAsync<T1, T2,T3, TResult>
    {
        private readonly Func<T1, T2, T3, T4, TResult> _func;
        public T4 Parameter4 { get; protected set; }

        public FuncAsync(Func<T1, T2, T3,T4, TResult> func, T1 parameter1, T2 parameter2, T3 parameter3,T4 parameter4)
            : this(parameter1, parameter2, parameter3, parameter4)
        {
            this._func = func;
        }

        public FuncAsync(T1 parameter1, T2 parameter2, T3 parameter3,T4 parameter4)
            : base(parameter1, parameter2, parameter3)
        {
            this.Parameter4 = parameter4;
        }

        public override IAsyncResult Invoke()
        {
            var result = _func.BeginInvoke(Parameter1, Parameter2, Parameter3,Parameter4, CompletedCallBack, null);
            SetAsyncResult(result);
            return result;
        }

        public override void CompletedCallBack(IAsyncResult ar)
        {
            try
            {
                var result = _func.EndInvoke(ar);
                SetResult(result);
            }
            catch (Exception exception)
            {
                this.CatchException(exception);
                SetResult(default(TResult));
            }
            ContinueAsync();
        }
    }
    public class FuncAsync<T1, T2, T3, T4,T5, TResult> : FuncAsync<T1, T2, T3,T4, TResult>
    {
        private readonly Func<T1, T2, T3, T4,T5, TResult> _func;
        public T5 Parameter5 { get; protected set; }

        public FuncAsync(Func<T1, T2, T3, T4, T5, TResult> func, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5)
            : this(parameter1, parameter2, parameter3, parameter4, parameter5)
        {
            this._func = func;
        }

        public FuncAsync(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5)
            : base(parameter1, parameter2, parameter3,parameter4)
        {
            this.Parameter5 = parameter5;
        }

        public override IAsyncResult Invoke()
        {
            var result = _func.BeginInvoke(Parameter1, Parameter2, Parameter3, Parameter4,Parameter5, CompletedCallBack, null);
            SetAsyncResult(result);
            return result;
        }

        public override void CompletedCallBack(IAsyncResult ar)
        {
            try
            {
                var result = _func.EndInvoke(ar);
                SetResult(result);
            }
            catch (Exception exception)
            {
                this.CatchException(exception);
                SetResult(default(TResult));
            }
            ContinueAsync();
        }
    }
    public class FuncAsync<T1, T2, T3, T4, T5, T6, TResult> : FuncAsync<T1, T2, T3, T4, T5, TResult>
    {
        private readonly Func<T1, T2, T3, T4,T5,T6, TResult> _func;
        public T6 Parameter6 { get; protected set; }

        public FuncAsync(Func<T1, T2, T3, T4, T5, T6, TResult> func, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6)
            : this(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6)
        {
            this._func = func;
        }

        public FuncAsync(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6)
            : base(parameter1, parameter2, parameter3, parameter4, parameter5)
        {
            this.Parameter6 = parameter6;
        }

        public override IAsyncResult Invoke()
        {
            var result = _func.BeginInvoke(Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6,
                CompletedCallBack, null);
            SetAsyncResult(result);
            return result;
        }

        public override void CompletedCallBack(IAsyncResult ar)
        {
            try
            {
                var result = _func.EndInvoke(ar);
                SetResult(result);
            }
            catch (Exception exception)
            {
                this.CatchException(exception);
                SetResult(default(TResult));
            }
            ContinueAsync();
        }
    }

    public class FuncAsync<T1, T2, T3, T4, T5, T6, T7, TResult> : FuncAsync<T1, T2, T3, T4, T5, T6, TResult>
    {
        private readonly Func<T1, T2, T3, T4,T5,T6, T7,  TResult> _func;
        public T7 Parameter7 { get; protected set; }

        public FuncAsync(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, T1 parameter1, T2 parameter2, T3 parameter3,
            T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7)
            : this(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7)
        {
            this._func = func;
        }

        public FuncAsync(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7)
            : base(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6)
        {
            this.Parameter7 = parameter7;
        }

        public override IAsyncResult Invoke()
        {
            var result = _func.BeginInvoke(Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6,
                Parameter7, CompletedCallBack, null);
            SetAsyncResult(result);
            return result;
        }

        public override void CompletedCallBack(IAsyncResult ar)
        {
            try
            {
                var result = _func.EndInvoke(ar);
                SetResult(result);
            }
            catch (Exception exception)
            {
                this.CatchException(exception);
                SetResult(default(TResult));
            }
            ContinueAsync();
        }
    }

    public class FuncAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult> : FuncAsync<T1, T2, T3, T4, T5, T6,T7, TResult>
    {
        private readonly Func<T1, T2, T3, T4,T5,T6, T7,T8,  TResult> _func;
        public T8 Parameter8 { get; protected set; }

        public FuncAsync(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, T1 parameter1, T2 parameter2, T3 parameter3,
            T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8)
            : this(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8)
        {
            this._func = func;
        }

        public FuncAsync(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8)
            : base(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7)
        {
            this.Parameter8 = parameter8;
        }

        public override IAsyncResult Invoke()
        {
            var result = _func.BeginInvoke(Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6,Parameter7,
                Parameter8, CompletedCallBack, null);
            SetAsyncResult(result);
            return result;
        }

        public override void CompletedCallBack(IAsyncResult ar)
        {
            try
            {
                var result = _func.EndInvoke(ar);
                SetResult(result);
            }
            catch (Exception exception)
            {
                this.CatchException(exception);
                SetResult(default(TResult));
            }
            ContinueAsync();
        }
    }

}
