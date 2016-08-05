using System.Collections;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	4/26/2016 5:20:35 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace Jake.V35.Core.Tuple.Interfaces
{
    public interface IStructuralComparable
    {
        /// <summary>
        /// 确定当前集合对象在排序顺序中的位置是位于另一个对象之前、之后还是与其位置相同。
        /// </summary>
        /// 
        /// <returns>
        /// 一个指示当前集合对象与 <paramref name="other"/> 的关系的整数，如下表所示。返回值说明-1当前实例位于 <paramref name="other"/> 之前。0当前实例与 <paramref name="other"/> 位于同一位置。1当前实例位于 <paramref name="other"/> 之后。
        /// </returns>
        /// <param name="other">要与当前实例进行比较的对象。</param><param name="comparer">一个将当前集合对象的成员与 <paramref name="other"/> 的对应成员进行比较的对象。</param>
        int CompareTo(object other, IComparer comparer);
    }
}
