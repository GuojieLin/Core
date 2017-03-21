using System;
using System.Collections.Generic;
using System.Linq;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	1/5/2016 11:16:46 AM			//
//			创建日期:	2016				            //
//======================================================//
namespace Jake.V35.Core.Extensions
{
    public static class DistinctExtensions
    {
        /// <summary>
        /// Linq Distinct实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T, TResult>(this IEnumerable<T> source, Func<T, TResult> keySelector)
        {
            return source.Distinct(new EqualityComparer<T, TResult>(keySelector));
        }
    }
}
