//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	4/26/2016 2:36:27 PM			//
//			创建日期:	2016				            //
//======================================================//

//2016.4.26     Func增加5,6,7,8个参数
namespace Jake.Common.V35.Core.Async
{
    public delegate TResult Func<T1, T2, T3, T4, T5, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

    public delegate TResult Func<T1, T2, T3, T4, T5, T6, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

    public delegate TResult Func<T1, T2, T3, T4, T5, T6, T7, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);

    public delegate TResult Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
        T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);
}
