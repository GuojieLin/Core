using System.Collections;
using System.Text;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	4/26/2016 5:24:45 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace Jake.V35.Core.Tuple.Interfaces
{
    public interface ITuple
    {
        int Size { get; }

        string ToString(StringBuilder sb);

        int GetHashCode(IEqualityComparer comparer);
    }
}
