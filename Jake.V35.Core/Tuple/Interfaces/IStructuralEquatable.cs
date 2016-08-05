using System.Collections;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	4/26/2016 5:20:06 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace Jake.Common.V35.Core.Tuple.Interfaces
{

    /// <summary>
    /// 定义方法以支持对象的结构相等性比较。
    /// </summary>
    public interface IStructuralEquatable
    {
        /// <summary>
        /// 确定某个对象与当前实例在结构上是否相等。
        /// </summary>
        /// 
        /// <returns>
        /// 如果两个对象相等，则为 true；否则为 false。
        /// </returns>
        /// <param name="other">要与当前实例进行比较的对象。</param><param name="comparer">一个可确定当前实例与 <paramref name="other"/> 是否相等的对象。</param>
        bool Equals(object other, IEqualityComparer comparer);

        /// <summary>
        /// 返回当前实例的哈希代码。
        /// </summary>
        /// 
        /// <returns>
        /// 当前实例的哈希代码。
        /// </returns>
        /// <param name="comparer">一个计算当前对象的哈希代码的对象。</param>
        int GetHashCode(IEqualityComparer comparer);
    }
}
