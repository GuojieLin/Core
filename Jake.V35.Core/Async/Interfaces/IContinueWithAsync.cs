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
        Operator ContinueWithAsync(Action action);
        Operator ContinueWithAsync<TParameter>(Action<TParameter> action, TParameter parameter);
        Operator ContinueWithAsync<TParameter1, TParameter2>(Action<TParameter1, TParameter2> action,
            TParameter1 parameter1, TParameter2 parameter2);
        Operator ContinueWithAsync<TParameter1, TParameter2, TParameter3>
            (Action<TParameter1, TParameter2, TParameter3> action,
                TParameter1 parameter1,
                TParameter2 parameter2,
                TParameter3 parameter3);
        Operator ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4>(
            Action<TParameter1, TParameter2, TParameter3, TParameter4> action,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4);
        Operator ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>(
            Action<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5> action,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4,
            TParameter5 parameter5);
        Operator ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>(
            Action<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6> action,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4,
            TParameter5 parameter5,
            TParameter6 parameter6);
        Operator ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>(
            Action<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7> action,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4,
            TParameter5 parameter5,
            TParameter6 parameter6,
            TParameter7 parameter7);
        Operator ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6,TParameter7, TParameter8>(
            Action<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8> action,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4,
            TParameter5 parameter5,
            TParameter6 parameter6,
            TParameter7 parameter7,
            TParameter8 parameter8);
        Operator ContinueWithAsync<TResult>(Func<TResult> func);
        Operator ContinueWithAsync<TParameter, TResult>(Func<TParameter, TResult> func,
            TParameter parameter);
        Operator ContinueWithAsync<TParameter1, TParameter2, TResult>
            (Func<TParameter1, TParameter2, TResult> func,
                TParameter1 parameter1,
                TParameter2 parameter2);
        Operator ContinueWithAsync<TParameter1, TParameter2, TParameter3, TResult>(
            Func<TParameter1, TParameter2, TParameter3, TResult> func,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3);
        Operator ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TResult>(
            Func<TParameter1, TParameter2, TParameter3, TParameter4, TResult> func,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4);
        Operator ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TResult>(
            Func<TParameter1, TParameter2, TParameter3, TParameter4,TParameter5, TResult> func,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4,
            TParameter5 parameter5);
        Operator ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5,TParameter6, TResult>(
            Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5,TParameter6, TResult> func,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4,
            TParameter5 parameter5,
            TParameter6 parameter6);
        Operator ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6,TParameter7, TResult>(
            Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6,TParameter7, TResult> func,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4,
            TParameter5 parameter5,
            TParameter6 parameter6,
            TParameter7 parameter7);
        Operator ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7,TParameter8, TResult>(
            Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TResult> func,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4,
            TParameter5 parameter5,
            TParameter6 parameter6,
            TParameter7 parameter7,
            TParameter8 parameter8);
    }
}
