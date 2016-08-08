using System;
using System.Collections.Generic;

namespace Jake.V35.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// IEnumerable可以直接foreach遍历
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="action"></param>
        public static void ForEach<TEntity>(this IEnumerable<TEntity> entities, Action<TEntity> action)
        {
            foreach (var entity in entities)
            {
                action.Invoke(entity);
            }
        }
    }
}
