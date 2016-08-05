using System;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	12/19/2015 8:48:02 PM			//
//			创建日期:	2015				            //
//======================================================//

//2015.1.17 添加异常捕获机制
//          移除无用代码            
//2016.1.19 实现ContinueWithAsync,当异步处理完成后执行
//2016.4.26 ActionAsync增加5,6,7,8个参数重载
namespace Jake.Common.V35.Core.Async
{
    public class ActionAsync : Operator
    {
        private readonly Action _action;
        protected ActionAsync()
        {
        }
        public ActionAsync(Action action)
            : this()
        {
            this._action = action;
        }
        public override IAsyncResult Invoke()
        {
            var middle = _action.BeginInvoke(CompletedCallBack, null);
            SetAsyncResult(middle);
            return middle;
        }
        public override void CompletedCallBack(IAsyncResult ar)
        {
            try
            {
                _action.EndInvoke(ar);
            }
            catch (Exception exception)
            {
                this.CatchException(exception);
            }
            ContinueAsync();
        }
    }

    public class ActionAsync<T> : ActionAsync
    {
        public T Result;
        private readonly Action<T> _action;
        protected readonly T Parameter1;
        public ActionAsync()
        {
        }
        public ActionAsync(T parameter)
        {
            this.Parameter1 = parameter;
        }
        public ActionAsync(Action<T> action, T parameter)
        {
            this._action = action;
            this.Parameter1 = parameter;
        }
        public override IAsyncResult Invoke()
        {
            var result = _action.BeginInvoke(Parameter1, CompletedCallBack, null);
            SetAsyncResult(result);
            return result;
        }

        public override void CompletedCallBack(IAsyncResult ar)
        {
            try
            {
                _action.EndInvoke(ar);
            }
            catch (Exception exception)
            {
                this.CatchException(exception);
            }
            ContinueAsync();
        }
    }
    public class ActionAsync<T1, T2> : ActionAsync<T1>
    {
        protected readonly T2 Parameter2;
        private readonly Action<T1, T2> _action;

        public ActionAsync(Action<T1, T2> action, T1 parameter1, T2 parameter2)
            : this(parameter1, parameter2)
        {
            this._action = action;
        }
        protected ActionAsync(T1 parameter1, T2 parameter2)
            : this(parameter1)
        {
            this.Parameter2 = parameter2;
        }
        protected ActionAsync(T1 parameter1)
            : base(parameter1)
        {
        }

        public override IAsyncResult Invoke()
        {
            var result = _action.BeginInvoke(Parameter1, Parameter2, CompletedCallBack, null);
            SetAsyncResult(result);
            return result;
        }

        public override void CompletedCallBack(IAsyncResult ar)
        {
            try
            {
                _action.EndInvoke(ar);

            }
            catch (Exception exception)
            {
                this.CatchException(exception);
            }
            ContinueAsync();
        }
    }
    public class ActionAsync<T1, T2, T3> : ActionAsync<T1, T2>
    {
        private readonly Action<T1, T2, T3> _action;
        protected T3 Parameter3;
        public ActionAsync(Action<T1, T2, T3> action, T1 parameter1, T2 parameter2, T3 parameter3)
            : this(parameter1, parameter2, parameter3)
        {
            this._action = action;
        }

        public ActionAsync(T1 parameter1, T2 parameter2, T3 parameter3)
            : base(parameter1, parameter2)
        {
            this.Parameter3 = parameter3;
        }

        public override IAsyncResult Invoke()
        {
            var result = _action.BeginInvoke(Parameter1, Parameter2, Parameter3, CompletedCallBack, null);
            SetAsyncResult(result);
            return result;
        }
        public override void CompletedCallBack(IAsyncResult ar)
        {
            try
            {
                _action.EndInvoke(ar);
            }
            catch (Exception exception)
            {
                this.CatchException(exception);
            }
            ContinueAsync();
        }
    }
    public class ActionAsync<T1, T2, T3,T4> : ActionAsync<T1, T2,T3>
    {
        private readonly Action<T1, T2, T3,T4> _action;
        protected T4 Parameter4;
        public ActionAsync(Action<T1, T2, T3,T4> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4)
            : this(parameter1, parameter2, parameter3, parameter4)
        {
            this._action = action;
        }

        public ActionAsync(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4)
            : base(parameter1, parameter2, parameter3)
        {
            this.Parameter4 = parameter4;
        }

        public override IAsyncResult Invoke()
        {
            var result = _action.BeginInvoke(Parameter1, Parameter2, Parameter3, Parameter4, CompletedCallBack, null);
            SetAsyncResult(result);
            return result;
        }

        public override void CompletedCallBack(IAsyncResult ar)
        {
            try
            {
                _action.EndInvoke(ar);
            }
            catch (Exception exception)
            {
                this.CatchException(exception);
            }
            ContinueAsync();
        }
    }
    public class ActionAsync<T1, T2, T3, T4,T5> : ActionAsync<T1, T2, T3, T4>
    {
        private readonly Action<T1, T2, T3,T4,T5> _action;
        protected T5 Parameter5;
        public ActionAsync(Action<T1, T2, T3, T4, T5> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5)
            : this(parameter1, parameter2, parameter3, parameter4, parameter5)
        {
            this._action = action;
        }

        public ActionAsync(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5)
            : base(parameter1, parameter2, parameter3, parameter4)
        {
            this.Parameter5 = parameter5;
        }

        public override IAsyncResult Invoke()
        {
            var result = _action.BeginInvoke(Parameter1, Parameter2, Parameter3, Parameter4,Parameter5, CompletedCallBack, null);
            SetAsyncResult(result);
            return result;
        }

        public override void CompletedCallBack(IAsyncResult ar)
        {
            try
            {
                _action.EndInvoke(ar);
            }
            catch (Exception exception)
            {
                this.CatchException(exception);
            }
            ContinueAsync();
        }
    }
    public class ActionAsync<T1, T2, T3, T4, T5, T6> : ActionAsync<T1, T2, T3, T4, T5>
    {
        private readonly Action<T1, T2, T3,T4,T5,T6> _action;
        protected T6 Parameter6;
        public ActionAsync(Action<T1, T2, T3, T4, T5, T6> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6)
            : this(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6)
        {
            this._action = action;
        }

        public ActionAsync(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6)
            : base(parameter1, parameter2, parameter3, parameter4, parameter5)
        {
            this.Parameter6 = parameter6;
        }

        public override IAsyncResult Invoke()
        {
            var result = _action.BeginInvoke(Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6, CompletedCallBack, null);
            SetAsyncResult(result);
            return result;
        }

        public override void CompletedCallBack(IAsyncResult ar)
        {
            try
            {
                _action.EndInvoke(ar);
            }
            catch (Exception exception)
            {
                this.CatchException(exception);
            }
            ContinueAsync();
        }
    }

    public class ActionAsync<T1, T2, T3, T4, T5, T6, T7> : ActionAsync<T1, T2, T3, T4, T5, T6>
    {
        private readonly Action<T1, T2, T3, T4, T5, T6, T7> _action;
        protected T7 Parameter7;

        public ActionAsync(Action<T1, T2, T3, T4, T5, T6, T7> action, T1 parameter1, T2 parameter2, T3 parameter3,
            T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7)
            : this(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7)
        {
            this._action = action;
        }

        public ActionAsync(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6,
            T7 parameter7)
            : base(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6)
        {
            this.Parameter7 = parameter7;
        }

        public override IAsyncResult Invoke()
        {
            var result = _action.BeginInvoke(Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6,
                Parameter7, CompletedCallBack, null);
            SetAsyncResult(result);
            return result;
        }

        public override void CompletedCallBack(IAsyncResult ar)
        {
            try
            {
                _action.EndInvoke(ar);
            }
            catch (Exception exception)
            {
                this.CatchException(exception);
            }
            ContinueAsync();
        }
    }


    public class ActionAsync<T1, T2, T3, T4, T5, T6, T7, T8> : ActionAsync<T1, T2, T3, T4, T5, T6, T7>
    {
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8> _action;
        protected T8 Parameter8;

        public ActionAsync(Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 parameter1, T2 parameter2, T3 parameter3,
            T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8)
            : this(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8)
        {
            this._action = action;
        }

        public ActionAsync(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6,
            T7 parameter7, T8 parameter8)
            : base(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7)
        {
            this.Parameter8 = parameter8;
        }

        public override IAsyncResult Invoke()
        {
            var result = _action.BeginInvoke(Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6,
                Parameter7,
                Parameter8, CompletedCallBack, null);
            SetAsyncResult(result);
            return result;
        }

        public override void CompletedCallBack(IAsyncResult ar)
        {
            try
            {
                _action.EndInvoke(ar);
            }
            catch (Exception exception)
            {
                this.CatchException(exception);
            }
            ContinueAsync();
        }
    }
}
