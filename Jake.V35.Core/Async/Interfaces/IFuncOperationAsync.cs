//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	4/26/2016 2:42:20 PM			//
//			创建日期:	2016				            //
//======================================================//


//2015.12.19    添加异步操作接口
namespace Jake.Common.V35.Core.Async.Interfaces
{
    public interface IFuncOperationAsync<T>
    {
        void SetResult(T result);
        T GetResult();
    }
}
