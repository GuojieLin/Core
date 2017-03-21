using System;
using System.Collections.Generic;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	1/5/2016 11:14:57 AM			//
//			创建日期:	2016				            //
//======================================================//

namespace Jake.V35.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class EqualityComparer<TParameter, TResult> : IEqualityComparer<TParameter>
    {
        private readonly Func<TParameter, TResult> _keySelector;

        public EqualityComparer(Func<TParameter, TResult> keySelector)
        {
            this._keySelector = keySelector;
        }

        public bool Equals(TParameter x, TParameter y)
        {
            return EqualityComparer<TResult>.Default.Equals(_keySelector(x), _keySelector(y));
        }

        public int GetHashCode(TParameter obj)
        {
            return EqualityComparer<TResult>.Default.GetHashCode(_keySelector(obj));
        }
    }
}
