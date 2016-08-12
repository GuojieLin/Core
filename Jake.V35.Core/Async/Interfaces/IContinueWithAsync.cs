using System;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	4/26/2016 2:40:32 PM			//
//			创建日期:	2016				            //
//======================================================//

//2016.01.19    实现IContinueWithAsync结果异步处理完成后继续某异步操作
namespace Jake.V35.Core.Async.Interfaces
{
    public interface IContinueWithAsync
    {
        Operator Previous { get; set; }
        Operator Next { get; set; }
        ActionAsync ContinueWithAsync(Action action);
        ActionAsync<Operator> ContinueWithAsync(Action<Operator> action);
        ActionAsync<Operator, TParameter> ContinueWithAsync<TParameter>(Action<Operator, TParameter> action, TParameter parameter);
        ActionAsync<Operator, TParameter1, TParameter2> ContinueWithAsync<TParameter1, TParameter2>(Action<Operator, TParameter1, TParameter2> action,
            TParameter1 parameter1, TParameter2 parameter2);
        ActionAsync<Operator, TParameter1, TParameter2, TParameter3> ContinueWithAsync<TParameter1, TParameter2, TParameter3>
            (Action<Operator,TParameter1, TParameter2, TParameter3> action,
                TParameter1 parameter1,
                TParameter2 parameter2,
                TParameter3 parameter3);
        ActionAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4> 
            ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4>(
            Action<Operator,TParameter1, TParameter2, TParameter3, TParameter4> action,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4);
        ActionAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>
            ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>(
            Action<Operator,TParameter1, TParameter2, TParameter3, TParameter4, TParameter5> action,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4,
            TParameter5 parameter5);
        ActionAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>
            ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>(
            Action<Operator,TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6> action,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4,
            TParameter5 parameter5,
            TParameter6 parameter6);
        ActionAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>
            ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>(
            Action<Operator,TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7> action,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4,
            TParameter5 parameter5,
            TParameter6 parameter6,
            TParameter7 parameter7);
        FuncAsync<TResult> ContinueWithAsync<TResult>(Func<TResult> func);
        FuncAsync<Operator, TResult> ContinueWithAsync<TResult>(Func<Operator, TResult> func);
        FuncAsync<Operator, TParameter, TResult> ContinueWithAsync<TParameter, TResult>(Func<Operator, TParameter, TResult> func,
            TParameter parameter);
        FuncAsync<Operator, TParameter1, TParameter2, TResult> ContinueWithAsync<TParameter1, TParameter2, TResult>
            (Func<Operator, TParameter1, TParameter2, TResult> func,
                TParameter1 parameter1,
                TParameter2 parameter2);
        FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TResult>
            ContinueWithAsync<TParameter1, TParameter2, TParameter3, TResult>(
            Func<Operator, TParameter1, TParameter2, TParameter3, TResult> func,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3);
        FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TResult> 
            ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TResult>(
            Func<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TResult> func,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4);
        FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TResult>
            ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TResult>(
            Func<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TResult> func,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4,
            TParameter5 parameter5);
        FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TResult>
            ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TResult>(
            Func<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TResult> func,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4,
            TParameter5 parameter5,
            TParameter6 parameter6);
        FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TResult>
            ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TResult>(
            Func<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TResult> func,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4,
            TParameter5 parameter5,
            TParameter6 parameter6,
            TParameter7 parameter7);
    }
}
