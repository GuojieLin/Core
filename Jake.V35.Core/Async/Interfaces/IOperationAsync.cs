using System;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	4/26/2016 2:41:44 PM			//
//			创建日期:	2016				            //
//======================================================//


//2015.12.19    IFunc异步操作接口
namespace Jake.V35.Core.Async.Interfaces
{

    public interface IOperationAsync
    {
        IAsyncResult Invoke();
        void Wait();
        void CompletedCallBack(IAsyncResult ar);
        void CatchException(Exception exception);
    }

}
