using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Jake.Common.V35.Core.Tuple.Interfaces;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	4/26/2016 5:13:37 PM			//
//			创建日期:	2016				            //
//======================================================//

//2016.4.26     .net framework4.0下的Tuple
namespace Jake.Common.V35.Core.Tuple
{

    [Serializable]
    public class Tuple<T1> : IStructuralEquatable, IStructuralComparable, IComparable, ITuple
    {
        private readonly T1 _item;

        public T1 Item1
        {
            get { return this._item; }
        }

        int ITuple.Size
        {
            get { return 1; }
        }

        /// <summary>
        /// 初始化 <see cref="T:System.Tuple`1"/> 类的新实例。
        /// </summary>
        /// <param name="item1">元组的唯一一个分量的值。</param>
        public Tuple(T1 item1)
        {
            this._item = item1;
        }

        /// <summary>
        /// 返回一个值，该值指示当前的 <see cref="T:System.Tuple`1"/> 对象是否与指定对象相等。
        /// </summary>
        /// <returns>
        /// 如果当前实例等于指定对象，则为 true；否则为 false。
        /// </returns>
        /// <param name="obj">与该实例进行比较的对象。</param>
        public override bool Equals(object obj)
        {
            return ((IStructuralEquatable) this).Equals(obj, (IEqualityComparer) EqualityComparer<object>.Default);
        }

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
        {
            if (other == null) return false;
            Tuple<T1> tuple = other as Tuple<T1>;
            if (tuple == null) return false;
            return comparer.Equals((object) this._item, (object) tuple._item);
        }

        int IComparable.CompareTo(object obj)
        {
            return ((IStructuralComparable) this).CompareTo(obj, (IComparer) Comparer<object>.Default);
        }

        int IStructuralComparable.CompareTo(object other, IComparer comparer)
        {
            if (other == null)
                return 1;
            Tuple<T1> tuple = other as Tuple<T1>;
            if (tuple == null)
            {
                throw new ArgumentException("不能和null进行比较");
            }
            return comparer.Compare((object) this._item, (object) tuple._item);
        }

        /// <summary>
        /// 返回当前 <see cref="T:System.Tuple`1"/> 对象的哈希代码。
        /// </summary>
        /// 
        /// <returns>
        /// 32 位带符号整数哈希代码。
        /// </returns>
        public override int GetHashCode()
        {
            return ((IStructuralEquatable) this).GetHashCode((IEqualityComparer) EqualityComparer<object>.Default);
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return comparer.GetHashCode((object) this._item);
        }

        int ITuple.GetHashCode(IEqualityComparer comparer)
        {
            return ((IStructuralEquatable) this).GetHashCode(comparer);
        }

        /// <summary>
        /// 返回一个字符串，该字符串表示此 <see cref="T:System.Tuple`1"/> 实例的值。
        /// </summary>
        /// 
        /// <returns>
        /// 此 <see cref="T:System.Tuple`1"/> 对象的字符串表示形式。
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            return ((ITuple) this).ToString(sb);
        }

        string ITuple.ToString(StringBuilder sb)
        {
            sb.Append((object) this._item);
            sb.Append(")");
            return sb.ToString();
        }
    }

    [Serializable]
    public class Tuple<T1, T2> : IStructuralEquatable, IStructuralComparable, IComparable, ITuple
    {
        private readonly T1 m_Item1;
        private readonly T2 m_Item2;

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`2"/> 对象的第一个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`2"/> 对象的第一个分量的值。
        /// </returns>
        public T1 Item1
        {
            get
            {
                return this.m_Item1;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`2"/> 对象的第二个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`2"/> 对象的第二个分量的值。
        /// </returns>
        public T2 Item2
        {
            get
            {
                return this.m_Item2;
            }
        }

        int ITuple.Size
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// 初始化 <see cref="T:System.Tuple`2"/> 类的新实例。
        /// </summary>
        /// <param name="item1">此元组的第一个组件的值。</param><param name="item2">此元组的第二个组件的值。</param>
        public Tuple(T1 item1, T2 item2)
        {
            this.m_Item1 = item1;
            this.m_Item2 = item2;
        }

        /// <summary>
        /// 返回一个值，该值指示当前的 <see cref="T:System.Tuple`2"/> 对象是否与指定对象相等。
        /// </summary>
        /// 
        /// <returns>
        /// 如果当前实例等于指定对象，则为 true；否则为 false。
        /// </returns>
        /// <param name="obj">与该实例进行比较的对象。</param>
        public override bool Equals(object obj)
        {
            return ((IStructuralEquatable)this).Equals(obj, (IEqualityComparer)EqualityComparer<object>.Default);
        }

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
        {
            if (other == null)
                return false;
            Tuple<T1, T2> tuple = other as Tuple<T1, T2>;
            if (tuple == null || !comparer.Equals((object)this.m_Item1, (object)tuple.m_Item1))
                return false;
            return comparer.Equals((object)this.m_Item2, (object)tuple.m_Item2);
        }

        int IComparable.CompareTo(object obj)
        {
            return ((IStructuralComparable)this).CompareTo(obj, (IComparer)Comparer<object>.Default);
        }

        int IStructuralComparable.CompareTo(object other, IComparer comparer)
        {
            if (other == null)
                return 1;
            Tuple<T1, T2> tuple = other as Tuple<T1, T2>;
            if (tuple == null)
            {
                throw new ArgumentException("不能和null进行比较");
            }
            int num = comparer.Compare((object)this.m_Item1, (object)tuple.m_Item1);
            if (num != 0)
                return num;
            return comparer.Compare((object)this.m_Item2, (object)tuple.m_Item2);
        }

        /// <summary>
        /// 返回当前 <see cref="T:System.Tuple`2"/> 对象的哈希代码。
        /// </summary>
        /// 
        /// <returns>
        /// 32 位带符号整数哈希代码。
        /// </returns>
        public override int GetHashCode()
        {
            return ((IStructuralEquatable)this).GetHashCode((IEqualityComparer)EqualityComparer<object>.Default);
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return Tuple.CombineHashCodes(comparer.GetHashCode((object)this.m_Item1), comparer.GetHashCode((object)this.m_Item2));
        }

        int ITuple.GetHashCode(IEqualityComparer comparer)
        {
            return ((IStructuralEquatable)this).GetHashCode(comparer);
        }

        /// <summary>
        /// 返回一个字符串，该字符串表示此 <see cref="T:System.Tuple`2"/> 实例的值。
        /// </summary>
        /// 
        /// <returns>
        /// 此 <see cref="T:System.Tuple`2"/> 对象的字符串表示形式。
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            return ((ITuple)this).ToString(sb);
        }

        string ITuple.ToString(StringBuilder sb)
        {
            sb.Append((object)this.m_Item1);
            sb.Append(", ");
            sb.Append((object)this.m_Item2);
            sb.Append(")");
            return sb.ToString();
        }
    }

    [Serializable]
    public class Tuple<T1, T2, T3> : IStructuralEquatable, IStructuralComparable, IComparable, ITuple
    {
        private readonly T1 m_Item1;
        private readonly T2 m_Item2;
        private readonly T3 m_Item3;

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`3"/> 对象的第一个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`3"/> 对象的第一个分量的值。
        /// </returns>
        
        public T1 Item1
        {
            
            get
            {
                return this.m_Item1;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`3"/> 对象的第二个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`3"/> 对象的第二个分量的值。
        /// </returns>
        
        public T2 Item2
        {
            
            get
            {
                return this.m_Item2;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`3"/> 对象的第三个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`3"/> 对象的第三个分量的值。
        /// </returns>
        
        public T3 Item3
        {
            
            get
            {
                return this.m_Item3;
            }
        }

        int ITuple.Size
        {
            get
            {
                return 3;
            }
        }

        /// <summary>
        /// 初始化 <see cref="T:System.Tuple`3"/> 类的新实例。
        /// </summary>
        /// <param name="item1">此元组的第一个组件的值。</param><param name="item2">此元组的第二个组件的值。</param><param name="item3">此元组的第三个组件的值。</param>
        
        public Tuple(T1 item1, T2 item2, T3 item3)
        {
            this.m_Item1 = item1;
            this.m_Item2 = item2;
            this.m_Item3 = item3;
        }

        /// <summary>
        /// 返回一个值，该值指示当前的 <see cref="T:System.Tuple`3"/> 对象是否与指定对象相等。
        /// </summary>
        /// 
        /// <returns>
        /// 如果当前实例等于指定对象，则为 true；否则为 false。
        /// </returns>
        /// <param name="obj">与该实例进行比较的对象。</param>
        
        public override bool Equals(object obj)
        {
            return ((IStructuralEquatable)this).Equals(obj, (IEqualityComparer)EqualityComparer<object>.Default);
        }

        
        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
        {
            if (other == null)
                return false;
            Tuple<T1, T2, T3> tuple = other as Tuple<T1, T2, T3>;
            if (tuple == null || !comparer.Equals((object)this.m_Item1, (object)tuple.m_Item1) || !comparer.Equals((object)this.m_Item2, (object)tuple.m_Item2))
                return false;
            return comparer.Equals((object)this.m_Item3, (object)tuple.m_Item3);
        }

        
        int IComparable.CompareTo(object obj)
        {
            return ((IStructuralComparable)this).CompareTo(obj, (IComparer)Comparer<object>.Default);
        }

        
        int IStructuralComparable.CompareTo(object other, IComparer comparer)
        {
            if (other == null)
                return 1;
            Tuple<T1, T2, T3> tuple = other as Tuple<T1, T2, T3>;
            if (tuple == null)
            {
                throw new ArgumentException("不能和null进行比较");
            }
            int num1 = comparer.Compare((object)this.m_Item1, (object)tuple.m_Item1);
            if (num1 != 0)
                return num1;
            int num2 = comparer.Compare((object)this.m_Item2, (object)tuple.m_Item2);
            if (num2 != 0)
                return num2;
            return comparer.Compare((object)this.m_Item3, (object)tuple.m_Item3);
        }

        /// <summary>
        /// 返回当前的 <see cref="T:System.Tuple`3"/> 对象的哈希代码。
        /// </summary>
        /// 
        /// <returns>
        /// 32 位带符号整数哈希代码。
        /// </returns>
        
        public override int GetHashCode()
        {
            return ((IStructuralEquatable)this).GetHashCode((IEqualityComparer)EqualityComparer<object>.Default);
        }

        
        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return Tuple.CombineHashCodes(comparer.GetHashCode((object)this.m_Item1), comparer.GetHashCode((object)this.m_Item2), comparer.GetHashCode((object)this.m_Item3));
        }

        int ITuple.GetHashCode(IEqualityComparer comparer)
        {
            return ((IStructuralEquatable)this).GetHashCode(comparer);
        }

        /// <summary>
        /// 返回一个字符串，该字符串表示此 <see cref="T:System.Tuple`3"/> 实例的值。
        /// </summary>
        /// 
        /// <returns>
        /// 此 <see cref="T:System.Tuple`3"/> 对象的字符串表示形式。
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            return ((ITuple)this).ToString(sb);
        }

        string ITuple.ToString(StringBuilder sb)
        {
            sb.Append((object)this.m_Item1);
            sb.Append(", ");
            sb.Append((object)this.m_Item2);
            sb.Append(", ");
            sb.Append((object)this.m_Item3);
            sb.Append(")");
            return sb.ToString();
        }
    }

    [Serializable]
    public class Tuple<T1, T2, T3, T4> : IStructuralEquatable, IStructuralComparable, IComparable, ITuple
    {
        private readonly T1 m_Item1;
        private readonly T2 m_Item2;
        private readonly T3 m_Item3;
        private readonly T4 m_Item4;

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`4"/> 对象的第一个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`4"/> 对象的第一个分量的值。
        /// </returns>
        public T1 Item1
        {
            get
            {
                return this.m_Item1;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`4"/> 对象的第二个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`4"/> 对象的第二个分量的值。
        /// </returns>
        public T2 Item2
        {
            get
            {
                return this.m_Item2;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`4"/> 对象的第三个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`4"/> 对象的第三个分量的值。
        /// </returns>
        public T3 Item3
        {
            get
            {
                return this.m_Item3;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`4"/> 对象的第四个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`4"/> 对象的第四个分量的值。
        /// </returns>
        public T4 Item4
        {
            get
            {
                return this.m_Item4;
            }
        }

        int ITuple.Size
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// 初始化 <see cref="T:System.Tuple`4"/> 类的新实例。
        /// </summary>
        /// <param name="item1">此元组的第一个组件的值。</param><param name="item2">此元组的第二个组件的值。</param><param name="item3">此元组的第三个组件的值。</param><param name="item4">此元组的第四个组件的值</param>
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            this.m_Item1 = item1;
            this.m_Item2 = item2;
            this.m_Item3 = item3;
            this.m_Item4 = item4;
        }

        /// <summary>
        /// 返回一个值，该值指示当前的 <see cref="T:System.Tuple`4"/> 对象是否与指定对象相等。
        /// </summary>
        /// 
        /// <returns>
        /// 如果当前实例等于指定对象，则为 true；否则为 false。
        /// </returns>
        /// <param name="obj">与该实例进行比较的对象。</param>
        public override bool Equals(object obj)
        {
            return ((IStructuralEquatable)this).Equals(obj, (IEqualityComparer)EqualityComparer<object>.Default);
        }

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
        {
            if (other == null)
                return false;
            Tuple<T1, T2, T3, T4> tuple = other as Tuple<T1, T2, T3, T4>;
            if (tuple == null || !comparer.Equals((object)this.m_Item1, (object)tuple.m_Item1) || (!comparer.Equals((object)this.m_Item2, (object)tuple.m_Item2) || !comparer.Equals((object)this.m_Item3, (object)tuple.m_Item3)))
                return false;
            return comparer.Equals((object)this.m_Item4, (object)tuple.m_Item4);
        }

        int IComparable.CompareTo(object obj)
        {
            return ((IStructuralComparable)this).CompareTo(obj, (IComparer)Comparer<object>.Default);
        }

        int IStructuralComparable.CompareTo(object other, IComparer comparer)
        {
            if (other == null)
                return 1;
            Tuple<T1, T2, T3, T4> tuple = other as Tuple<T1, T2, T3, T4>;
            if (tuple == null)
            {
                throw new ArgumentException("不能和null进行比较");
            }
            int num1 = comparer.Compare((object)this.m_Item1, (object)tuple.m_Item1);
            if (num1 != 0)
                return num1;
            int num2 = comparer.Compare((object)this.m_Item2, (object)tuple.m_Item2);
            if (num2 != 0)
                return num2;
            int num3 = comparer.Compare((object)this.m_Item3, (object)tuple.m_Item3);
            if (num3 != 0)
                return num3;
            return comparer.Compare((object)this.m_Item4, (object)tuple.m_Item4);
        }

        /// <summary>
        /// 返回当前 <see cref="T:System.Tuple`4"/> 对象的哈希代码。
        /// </summary>
        /// 
        /// <returns>
        /// 32 位带符号整数哈希代码。
        /// </returns>
        public override int GetHashCode()
        {
            return ((IStructuralEquatable)this).GetHashCode((IEqualityComparer)EqualityComparer<object>.Default);
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return Tuple.CombineHashCodes(comparer.GetHashCode((object)this.m_Item1), comparer.GetHashCode((object)this.m_Item2), comparer.GetHashCode((object)this.m_Item3), comparer.GetHashCode((object)this.m_Item4));
        }

        int ITuple.GetHashCode(IEqualityComparer comparer)
        {
            return ((IStructuralEquatable)this).GetHashCode(comparer);
        }

        /// <summary>
        /// 返回一个字符串，该字符串表示此 <see cref="T:System.Tuple`4"/> 实例的值。
        /// </summary>
        /// 
        /// <returns>
        /// 此 <see cref="T:System.Tuple`4"/> 对象的字符串表示形式。
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            return ((ITuple)this).ToString(sb);
        }

        string ITuple.ToString(StringBuilder sb)
        {
            sb.Append((object)this.m_Item1);
            sb.Append(", ");
            sb.Append((object)this.m_Item2);
            sb.Append(", ");
            sb.Append((object)this.m_Item3);
            sb.Append(", ");
            sb.Append((object)this.m_Item4);
            sb.Append(")");
            return sb.ToString();
        }
    }

    /// <summary>
    /// 表示 5 元组，即五元组。
    /// </summary>
    /// <typeparam name="T1">此元组的第一个组件的类型。</typeparam><typeparam name="T2">此元组的第二个组件的类型。</typeparam><typeparam name="T3">此元组的第三个组件的类型。</typeparam><typeparam name="T4">此元组的第四个组件的类型。</typeparam><typeparam name="T5">元组的第五个分量的类型。</typeparam><filterpriority>2</filterpriority>
    
    [Serializable]
    public class Tuple<T1, T2, T3, T4, T5> : IStructuralEquatable, IStructuralComparable, IComparable, ITuple
    {
        private readonly T1 m_Item1;
        private readonly T2 m_Item2;
        private readonly T3 m_Item3;
        private readonly T4 m_Item4;
        private readonly T5 m_Item5;

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`5"/> 对象的第一个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`5"/> 对象的第一个分量的值。
        /// </returns>
        
        public T1 Item1
        {
            
            get
            {
                return this.m_Item1;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`5"/> 对象的第二个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`5"/> 对象的第二个分量的值。
        /// </returns>
        
        public T2 Item2
        {
            
            get
            {
                return this.m_Item2;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`5"/> 对象的第三个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`5"/> 对象的第三个分量的值。
        /// </returns>
        
        public T3 Item3
        {
            
            get
            {
                return this.m_Item3;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`5"/> 对象的第四个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`5"/> 对象的第四个分量的值。
        /// </returns>
        
        public T4 Item4
        {
            
            get
            {
                return this.m_Item4;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`5"/> 对象的第五个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`5"/> 对象的第五个分量的值。
        /// </returns>
        
        public T5 Item5
        {
            
            get
            {
                return this.m_Item5;
            }
        }

        int ITuple.Size
        {
            get
            {
                return 5;
            }
        }

        /// <summary>
        /// 初始化 <see cref="T:System.Tuple`5"/> 类的新实例。
        /// </summary>
        /// <param name="item1">此元组的第一个组件的值。</param><param name="item2">此元组的第二个组件的值。</param><param name="item3">此元组的第三个组件的值。</param><param name="item4">此元组的第四个组件的值</param><param name="item5">元组的第五个分量的值。</param>
        
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
        {
            this.m_Item1 = item1;
            this.m_Item2 = item2;
            this.m_Item3 = item3;
            this.m_Item4 = item4;
            this.m_Item5 = item5;
        }

        /// <summary>
        /// 返回一个值，该值指示当前的 <see cref="T:System.Tuple`5"/> 对象是否与指定对象相等。
        /// </summary>
        /// 
        /// <returns>
        /// 如果当前实例等于指定对象，则为 true；否则为 false。
        /// </returns>
        /// <param name="obj">与该实例进行比较的对象。</param>
        
        public override bool Equals(object obj)
        {
            return ((IStructuralEquatable)this).Equals(obj, (IEqualityComparer)EqualityComparer<object>.Default);
        }

        
        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
        {
            if (other == null)
                return false;
            Tuple<T1, T2, T3, T4, T5> tuple = other as Tuple<T1, T2, T3, T4, T5>;
            if (tuple == null || !comparer.Equals((object)this.m_Item1, (object)tuple.m_Item1) || (!comparer.Equals((object)this.m_Item2, (object)tuple.m_Item2) || !comparer.Equals((object)this.m_Item3, (object)tuple.m_Item3)) || !comparer.Equals((object)this.m_Item4, (object)tuple.m_Item4))
                return false;
            return comparer.Equals((object)this.m_Item5, (object)tuple.m_Item5);
        }

        
        int IComparable.CompareTo(object obj)
        {
            return ((IStructuralComparable)this).CompareTo(obj, (IComparer)Comparer<object>.Default);
        }

        
        int IStructuralComparable.CompareTo(object other, IComparer comparer)
        {
            if (other == null)
                return 1;
            Tuple<T1, T2, T3, T4, T5> tuple = other as Tuple<T1, T2, T3, T4, T5>;
            if (tuple == null)
            {
                throw new ArgumentException("不能和null进行比较");
            }
            int num1 = comparer.Compare((object)this.m_Item1, (object)tuple.m_Item1);
            if (num1 != 0)
                return num1;
            int num2 = comparer.Compare((object)this.m_Item2, (object)tuple.m_Item2);
            if (num2 != 0)
                return num2;
            int num3 = comparer.Compare((object)this.m_Item3, (object)tuple.m_Item3);
            if (num3 != 0)
                return num3;
            int num4 = comparer.Compare((object)this.m_Item4, (object)tuple.m_Item4);
            if (num4 != 0)
                return num4;
            return comparer.Compare((object)this.m_Item5, (object)tuple.m_Item5);
        }

        /// <summary>
        /// 返回当前的 <see cref="T:System.Tuple`5"/> 对象的哈希代码。
        /// </summary>
        /// 
        /// <returns>
        /// 32 位带符号整数哈希代码。
        /// </returns>
        
        public override int GetHashCode()
        {
            return ((IStructuralEquatable)this).GetHashCode((IEqualityComparer)EqualityComparer<object>.Default);
        }

        
        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return Tuple.CombineHashCodes(comparer.GetHashCode((object)this.m_Item1), comparer.GetHashCode((object)this.m_Item2), comparer.GetHashCode((object)this.m_Item3), comparer.GetHashCode((object)this.m_Item4), comparer.GetHashCode((object)this.m_Item5));
        }

        int ITuple.GetHashCode(IEqualityComparer comparer)
        {
            return ((IStructuralEquatable)this).GetHashCode(comparer);
        }

        /// <summary>
        /// 返回一个字符串，该字符串表示此 <see cref="T:System.Tuple`5"/> 实例的值。
        /// </summary>
        /// 
        /// <returns>
        /// 此 <see cref="T:System.Tuple`5"/> 对象的字符串表示形式。
        /// </returns>
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            return ((ITuple)this).ToString(sb);
        }

        string ITuple.ToString(StringBuilder sb)
        {
            sb.Append((object)this.m_Item1);
            sb.Append(", ");
            sb.Append((object)this.m_Item2);
            sb.Append(", ");
            sb.Append((object)this.m_Item3);
            sb.Append(", ");
            sb.Append((object)this.m_Item4);
            sb.Append(", ");
            sb.Append((object)this.m_Item5);
            sb.Append(")");
            return sb.ToString();
        }
    }

    /// <summary>
    /// 表示 6 元组，即六元组。
    /// </summary>
    /// <typeparam name="T1">此元组的第一个组件的类型。</typeparam><typeparam name="T2">此元组的第二个组件的类型。</typeparam><typeparam name="T3">此元组的第三个组件的类型。</typeparam><typeparam name="T4">此元组的第四个组件的类型。</typeparam><typeparam name="T5">元组的第五个分量的类型。</typeparam><typeparam name="T6">元组的第六个分量的类型。</typeparam><filterpriority>2</filterpriority>
    
    [Serializable]
    public class Tuple<T1, T2, T3, T4, T5, T6> : IStructuralEquatable, IStructuralComparable, IComparable, ITuple
    {
        private readonly T1 m_Item1;
        private readonly T2 m_Item2;
        private readonly T3 m_Item3;
        private readonly T4 m_Item4;
        private readonly T5 m_Item5;
        private readonly T6 m_Item6;

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`6"/> 对象的第一个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`6"/> 对象的第一个分量的值。
        /// </returns>
        
        public T1 Item1
        {
            
            get
            {
                return this.m_Item1;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`6"/> 对象的第二个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`6"/> 对象的第二个分量的值。
        /// </returns>
        
        public T2 Item2
        {
            
            get
            {
                return this.m_Item2;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`6"/> 对象的第三个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`6"/> 对象的第三个分量的值。
        /// </returns>
        
        public T3 Item3
        {
            
            get
            {
                return this.m_Item3;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`6"/> 对象的第四个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`6"/> 对象的第四个分量的值。
        /// </returns>
        
        public T4 Item4
        {
            
            get
            {
                return this.m_Item4;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`6"/> 对象的第五个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`6"/> 对象的第五个分量的值。
        /// </returns>
        
        public T5 Item5
        {
            
            get
            {
                return this.m_Item5;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`6"/> 对象的第六个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`6"/> 对象的第六个分量的值。
        /// </returns>
        
        public T6 Item6
        {
            
            get
            {
                return this.m_Item6;
            }
        }

        int ITuple.Size
        {
            get
            {
                return 6;
            }
        }

        /// <summary>
        /// 初始化 <see cref="T:System.Tuple`6"/> 类的新实例。
        /// </summary>
        /// <param name="item1">此元组的第一个组件的值。</param><param name="item2">此元组的第二个组件的值。</param><param name="item3">此元组的第三个组件的值。</param><param name="item4">此元组的第四个组件的值</param><param name="item5">元组的第五个分量的值。</param><param name="item6">元组的第六个分量的值。</param>
        
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
        {
            this.m_Item1 = item1;
            this.m_Item2 = item2;
            this.m_Item3 = item3;
            this.m_Item4 = item4;
            this.m_Item5 = item5;
            this.m_Item6 = item6;
        }

        /// <summary>
        /// 返回一个值，该值指示当前的 <see cref="T:System.Tuple`6"/> 对象是否与指定对象相等。
        /// </summary>
        /// 
        /// <returns>
        /// 如果当前实例等于指定对象，则为 true；否则为 false。
        /// </returns>
        /// <param name="obj">与该实例进行比较的对象。</param>
        
        public override bool Equals(object obj)
        {
            return ((IStructuralEquatable)this).Equals(obj, (IEqualityComparer)EqualityComparer<object>.Default);
        }

        
        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
        {
            if (other == null)
                return false;
            Tuple<T1, T2, T3, T4, T5, T6> tuple = other as Tuple<T1, T2, T3, T4, T5, T6>;
            if (tuple == null || !comparer.Equals((object)this.m_Item1, (object)tuple.m_Item1) || (!comparer.Equals((object)this.m_Item2, (object)tuple.m_Item2) || !comparer.Equals((object)this.m_Item3, (object)tuple.m_Item3)) || (!comparer.Equals((object)this.m_Item4, (object)tuple.m_Item4) || !comparer.Equals((object)this.m_Item5, (object)tuple.m_Item5)))
                return false;
            return comparer.Equals((object)this.m_Item6, (object)tuple.m_Item6);
        }

        
        int IComparable.CompareTo(object obj)
        {
            return ((IStructuralComparable)this).CompareTo(obj, (IComparer)Comparer<object>.Default);
        }

        
        int IStructuralComparable.CompareTo(object other, IComparer comparer)
        {
            if (other == null)
                return 1;
            Tuple<T1, T2, T3, T4, T5, T6> tuple = other as Tuple<T1, T2, T3, T4, T5, T6>;
            if (tuple == null)
            {
                throw new ArgumentException("不能和null进行比较");
            }
            int num1 = comparer.Compare((object)this.m_Item1, (object)tuple.m_Item1);
            if (num1 != 0)
                return num1;
            int num2 = comparer.Compare((object)this.m_Item2, (object)tuple.m_Item2);
            if (num2 != 0)
                return num2;
            int num3 = comparer.Compare((object)this.m_Item3, (object)tuple.m_Item3);
            if (num3 != 0)
                return num3;
            int num4 = comparer.Compare((object)this.m_Item4, (object)tuple.m_Item4);
            if (num4 != 0)
                return num4;
            int num5 = comparer.Compare((object)this.m_Item5, (object)tuple.m_Item5);
            if (num5 != 0)
                return num5;
            return comparer.Compare((object)this.m_Item6, (object)tuple.m_Item6);
        }

        /// <summary>
        /// 返回当前的 <see cref="T:System.Tuple`6"/> 对象的哈希代码。
        /// </summary>
        /// 
        /// <returns>
        /// 32 位带符号整数哈希代码。
        /// </returns>
        
        public override int GetHashCode()
        {
            return ((IStructuralEquatable)this).GetHashCode((IEqualityComparer)EqualityComparer<object>.Default);
        }

        
        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return Tuple.CombineHashCodes(comparer.GetHashCode((object)this.m_Item1), comparer.GetHashCode((object)this.m_Item2), comparer.GetHashCode((object)this.m_Item3), comparer.GetHashCode((object)this.m_Item4), comparer.GetHashCode((object)this.m_Item5), comparer.GetHashCode((object)this.m_Item6));
        }

        int ITuple.GetHashCode(IEqualityComparer comparer)
        {
            return ((IStructuralEquatable)this).GetHashCode(comparer);
        }

        /// <summary>
        /// 返回一个字符串，该字符串表示此 <see cref="T:System.Tuple`6"/> 实例的值。
        /// </summary>
        /// 
        /// <returns>
        /// 此 <see cref="T:System.Tuple`6"/> 对象的字符串表示形式。
        /// </returns>
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            return ((ITuple)this).ToString(sb);
        }

        string ITuple.ToString(StringBuilder sb)
        {
            sb.Append((object)this.m_Item1);
            sb.Append(", ");
            sb.Append((object)this.m_Item2);
            sb.Append(", ");
            sb.Append((object)this.m_Item3);
            sb.Append(", ");
            sb.Append((object)this.m_Item4);
            sb.Append(", ");
            sb.Append((object)this.m_Item5);
            sb.Append(", ");
            sb.Append((object)this.m_Item6);
            sb.Append(")");
            return sb.ToString();
        }
    }

    [Serializable]
    public class Tuple<T1, T2, T3, T4, T5, T6, T7> : IStructuralEquatable, IStructuralComparable, IComparable, ITuple
    {
        private readonly T1 m_Item1;
        private readonly T2 m_Item2;
        private readonly T3 m_Item3;
        private readonly T4 m_Item4;
        private readonly T5 m_Item5;
        private readonly T6 m_Item6;
        private readonly T7 m_Item7;

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`7"/> 对象的第一个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`7"/> 对象的第一个分量的值。
        /// </returns>
        
        public T1 Item1
        {
            
            get
            {
                return this.m_Item1;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`7"/> 对象的第二个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`7"/> 对象的第二个分量的值。
        /// </returns>
        
        public T2 Item2
        {
            
            get
            {
                return this.m_Item2;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`7"/> 对象的第三个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`7"/> 对象的第三个分量的值。
        /// </returns>
        
        public T3 Item3
        {
            
            get
            {
                return this.m_Item3;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`7"/> 对象的第四个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`7"/> 对象的第四个分量的值。
        /// </returns>
        
        public T4 Item4
        {
            
            get
            {
                return this.m_Item4;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`7"/> 对象的第五个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`7"/> 对象的第五个分量的值。
        /// </returns>
        
        public T5 Item5
        {
            
            get
            {
                return this.m_Item5;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`7"/> 对象的第六个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`7"/> 对象的第六个分量的值。
        /// </returns>
        
        public T6 Item6
        {
            
            get
            {
                return this.m_Item6;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`7"/> 对象的第七个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`7"/> 对象的第七个分量的值。
        /// </returns>
        
        public T7 Item7
        {
            
            get
            {
                return this.m_Item7;
            }
        }

        int ITuple.Size
        {
            get
            {
                return 7;
            }
        }

        /// <summary>
        /// 初始化 <see cref="T:System.Tuple`7"/> 类的新实例。
        /// </summary>
        /// <param name="item1">此元组的第一个组件的值。</param><param name="item2">此元组的第二个组件的值。</param><param name="item3">此元组的第三个组件的值。</param><param name="item4">此元组的第四个组件的值</param><param name="item5">元组的第五个分量的值。</param><param name="item6">元组的第六个分量的值。</param><param name="item7">元组的第七个分量的值。</param>
        
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
        {
            this.m_Item1 = item1;
            this.m_Item2 = item2;
            this.m_Item3 = item3;
            this.m_Item4 = item4;
            this.m_Item5 = item5;
            this.m_Item6 = item6;
            this.m_Item7 = item7;
        }

        /// <summary>
        /// 返回一个值，该值指示当前的 <see cref="T:System.Tuple`7"/> 对象是否与指定对象相等。
        /// </summary>
        /// 
        /// <returns>
        /// 如果当前实例等于指定对象，则为 true；否则为 false。
        /// </returns>
        /// <param name="obj">与该实例进行比较的对象。</param>
        
        public override bool Equals(object obj)
        {
            return ((IStructuralEquatable)this).Equals(obj, (IEqualityComparer)EqualityComparer<object>.Default);
        }

        
        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
        {
            if (other == null)
                return false;
            Tuple<T1, T2, T3, T4, T5, T6, T7> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7>;
            if (tuple == null || !comparer.Equals((object)this.m_Item1, (object)tuple.m_Item1) || (!comparer.Equals((object)this.m_Item2, (object)tuple.m_Item2) || !comparer.Equals((object)this.m_Item3, (object)tuple.m_Item3)) || (!comparer.Equals((object)this.m_Item4, (object)tuple.m_Item4) || !comparer.Equals((object)this.m_Item5, (object)tuple.m_Item5) || !comparer.Equals((object)this.m_Item6, (object)tuple.m_Item6)))
                return false;
            return comparer.Equals((object)this.m_Item7, (object)tuple.m_Item7);
        }

        
        int IComparable.CompareTo(object obj)
        {
            return ((IStructuralComparable)this).CompareTo(obj, (IComparer)Comparer<object>.Default);
        }

        
        int IStructuralComparable.CompareTo(object other, IComparer comparer)
        {
            if (other == null)
                return 1;
            Tuple<T1, T2, T3, T4, T5, T6, T7> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7>;
            if (tuple == null)
            {
                throw new ArgumentException("不能和null进行比较");
            }
            int num1 = comparer.Compare((object)this.m_Item1, (object)tuple.m_Item1);
            if (num1 != 0)
                return num1;
            int num2 = comparer.Compare((object)this.m_Item2, (object)tuple.m_Item2);
            if (num2 != 0)
                return num2;
            int num3 = comparer.Compare((object)this.m_Item3, (object)tuple.m_Item3);
            if (num3 != 0)
                return num3;
            int num4 = comparer.Compare((object)this.m_Item4, (object)tuple.m_Item4);
            if (num4 != 0)
                return num4;
            int num5 = comparer.Compare((object)this.m_Item5, (object)tuple.m_Item5);
            if (num5 != 0)
                return num5;
            int num6 = comparer.Compare((object)this.m_Item6, (object)tuple.m_Item6);
            if (num6 != 0)
                return num6;
            return comparer.Compare((object)this.m_Item7, (object)tuple.m_Item7);
        }

        /// <summary>
        /// 返回当前的 <see cref="T:System.Tuple`7"/> 对象的哈希代码。
        /// </summary>
        /// 
        /// <returns>
        /// 32 位带符号整数哈希代码。
        /// </returns>
        
        public override int GetHashCode()
        {
            return ((IStructuralEquatable)this).GetHashCode((IEqualityComparer)EqualityComparer<object>.Default);
        }

        
        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return Tuple.CombineHashCodes(comparer.GetHashCode((object)this.m_Item1), comparer.GetHashCode((object)this.m_Item2), comparer.GetHashCode((object)this.m_Item3), comparer.GetHashCode((object)this.m_Item4), comparer.GetHashCode((object)this.m_Item5), comparer.GetHashCode((object)this.m_Item6), comparer.GetHashCode((object)this.m_Item7));
        }

        int ITuple.GetHashCode(IEqualityComparer comparer)
        {
            return ((IStructuralEquatable)this).GetHashCode(comparer);
        }

        /// <summary>
        /// 返回一个字符串，该字符串表示此 <see cref="T:System.Tuple`7"/> 实例的值。
        /// </summary>
        /// 
        /// <returns>
        /// 此 <see cref="T:System.Tuple`7"/> 对象的字符串表示形式。
        /// </returns>
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            return ((ITuple)this).ToString(sb);
        }

        string ITuple.ToString(StringBuilder sb)
        {
            sb.Append((object)this.m_Item1);
            sb.Append(", ");
            sb.Append((object)this.m_Item2);
            sb.Append(", ");
            sb.Append((object)this.m_Item3);
            sb.Append(", ");
            sb.Append((object)this.m_Item4);
            sb.Append(", ");
            sb.Append((object)this.m_Item5);
            sb.Append(", ");
            sb.Append((object)this.m_Item6);
            sb.Append(", ");
            sb.Append((object)this.m_Item7);
            sb.Append(")");
            return sb.ToString();
        }
    }
    [Serializable]
    public class Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> : IStructuralEquatable, IStructuralComparable, IComparable, ITuple
    {
        private readonly T1 m_Item1;
        private readonly T2 m_Item2;
        private readonly T3 m_Item3;
        private readonly T4 m_Item4;
        private readonly T5 m_Item5;
        private readonly T6 m_Item6;
        private readonly T7 m_Item7;
        private readonly TRest m_Rest;

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`8"/> 对象的第一个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`8"/> 对象的第一个分量的值。
        /// </returns>
        
        public T1 Item1
        {
            
            get
            {
                return this.m_Item1;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`8"/> 对象的第二个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`8"/> 对象的第二个分量的值。
        /// </returns>
        
        public T2 Item2
        {
            
            get
            {
                return this.m_Item2;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`8"/> 对象的第三个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`8"/> 对象的第三个分量的值。
        /// </returns>
        
        public T3 Item3
        {
            
            get
            {
                return this.m_Item3;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`8"/> 对象的第四个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`8"/> 对象的第四个分量的值。
        /// </returns>
        
        public T4 Item4
        {
            
            get
            {
                return this.m_Item4;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`8"/> 对象的第五个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`8"/> 对象的第五个分量的值。
        /// </returns>
        
        public T5 Item5
        {
            
            get
            {
                return this.m_Item5;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`8"/> 对象的第六个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`8"/> 对象的第六个分量的值。
        /// </returns>
        
        public T6 Item6
        {
            
            get
            {
                return this.m_Item6;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`8"/> 对象的第七个分量的值。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`8"/> 对象的第七个分量的值。
        /// </returns>
        
        public T7 Item7
        {
            
            get
            {
                return this.m_Item7;
            }
        }

        /// <summary>
        /// 获取当前 <see cref="T:System.Tuple`8"/> 对象的剩余分量。
        /// </summary>
        /// 
        /// <returns>
        /// 当前 <see cref="T:System.Tuple`8"/> 对象的剩余分量的值。
        /// </returns>
        
        public TRest Rest
        {
            
            get
            {
                return this.m_Rest;
            }
        }

        int ITuple.Size
        {
            get
            {
                return 7 + ((ITuple)(object)this.m_Rest).Size;
            }
        }

        /// <summary>
        /// 初始化 <see cref="T:System.Tuple`8"/> 类的新实例。
        /// </summary>
        /// <param name="item1">此元组的第一个组件的值。</param><param name="item2">此元组的第二个组件的值。</param><param name="item3">此元组的第三个组件的值。</param><param name="item4">此元组的第四个组件的值</param><param name="item5">元组的第五个分量的值。</param><param name="item6">元组的第六个分量的值。</param><param name="item7">元组的第七个分量的值。</param><param name="rest">任何常规 Tuple 对象，其中包含元组的剩余分量的值。</param><exception cref="T:System.ArgumentException"><paramref name="rest"/> 不是泛型 Tuple 对象。</exception>
        
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest)
        {
            if (!((object)rest is ITuple))
                throw new ArgumentException("最后一个参数必须为元祖");
            this.m_Item1 = item1;
            this.m_Item2 = item2;
            this.m_Item3 = item3;
            this.m_Item4 = item4;
            this.m_Item5 = item5;
            this.m_Item6 = item6;
            this.m_Item7 = item7;
            this.m_Rest = rest;
        }

        /// <summary>
        /// 返回一个值，该值指示当前的 <see cref="T:System.Tuple`8"/> 对象是否与指定对象相等。
        /// </summary>
        /// 
        /// <returns>
        /// 如果当前实例等于指定对象，则为 true；否则为 false。
        /// </returns>
        /// <param name="obj">与该实例进行比较的对象。</param>
        
        public override bool Equals(object obj)
        {
            return ((IStructuralEquatable)this).Equals(obj, (IEqualityComparer)EqualityComparer<object>.Default);
        }

        
        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
        {
            if (other == null)
                return false;
            Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>;
            if (tuple == null || !comparer.Equals((object)this.m_Item1, (object)tuple.m_Item1) || (!comparer.Equals((object)this.m_Item2, (object)tuple.m_Item2) || !comparer.Equals((object)this.m_Item3, (object)tuple.m_Item3)) || (!comparer.Equals((object)this.m_Item4, (object)tuple.m_Item4) || !comparer.Equals((object)this.m_Item5, (object)tuple.m_Item5) || (!comparer.Equals((object)this.m_Item6, (object)tuple.m_Item6) || !comparer.Equals((object)this.m_Item7, (object)tuple.m_Item7))))
                return false;
            return comparer.Equals((object)this.m_Rest, (object)tuple.m_Rest);
        }

        
        int IComparable.CompareTo(object obj)
        {
            return ((IStructuralComparable)this).CompareTo(obj, (IComparer)Comparer<object>.Default);
        }

        
        int IStructuralComparable.CompareTo(object other, IComparer comparer)
        {
            if (other == null)
                return 1;
            Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>;
            if (tuple == null)
            {
                throw new ArgumentException("不能和null进行比较");
            }
            int num1 = comparer.Compare((object)this.m_Item1, (object)tuple.m_Item1);
            if (num1 != 0)
                return num1;
            int num2 = comparer.Compare((object)this.m_Item2, (object)tuple.m_Item2);
            if (num2 != 0)
                return num2;
            int num3 = comparer.Compare((object)this.m_Item3, (object)tuple.m_Item3);
            if (num3 != 0)
                return num3;
            int num4 = comparer.Compare((object)this.m_Item4, (object)tuple.m_Item4);
            if (num4 != 0)
                return num4;
            int num5 = comparer.Compare((object)this.m_Item5, (object)tuple.m_Item5);
            if (num5 != 0)
                return num5;
            int num6 = comparer.Compare((object)this.m_Item6, (object)tuple.m_Item6);
            if (num6 != 0)
                return num6;
            int num7 = comparer.Compare((object)this.m_Item7, (object)tuple.m_Item7);
            if (num7 != 0)
                return num7;
            return comparer.Compare((object)this.m_Rest, (object)tuple.m_Rest);
        }

        /// <summary>
        /// 计算当前 <see cref="T:System.Tuple`8"/> 对象的哈希代码。
        /// </summary>
        /// 
        /// <returns>
        /// 32 位带符号整数哈希代码。
        /// </returns>
        
        public override int GetHashCode()
        {
            return ((IStructuralEquatable)this).GetHashCode((IEqualityComparer)EqualityComparer<object>.Default);
        }

        
        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            ITuple tuple = (ITuple)(object)this.m_Rest;
            if (tuple.Size >= 8)
                return tuple.GetHashCode(comparer);
            switch (8 - tuple.Size)
            {
                case 1:
                    return Tuple.CombineHashCodes(comparer.GetHashCode((object)this.m_Item7), tuple.GetHashCode(comparer));
                case 2:
                    return Tuple.CombineHashCodes(comparer.GetHashCode((object)this.m_Item6), comparer.GetHashCode((object)this.m_Item7), tuple.GetHashCode(comparer));
                case 3:
                    return Tuple.CombineHashCodes(comparer.GetHashCode((object)this.m_Item5), comparer.GetHashCode((object)this.m_Item6), comparer.GetHashCode((object)this.m_Item7), tuple.GetHashCode(comparer));
                case 4:
                    return Tuple.CombineHashCodes(comparer.GetHashCode((object)this.m_Item4), comparer.GetHashCode((object)this.m_Item5), comparer.GetHashCode((object)this.m_Item6), comparer.GetHashCode((object)this.m_Item7), tuple.GetHashCode(comparer));
                case 5:
                    return Tuple.CombineHashCodes(comparer.GetHashCode((object)this.m_Item3), comparer.GetHashCode((object)this.m_Item4), comparer.GetHashCode((object)this.m_Item5), comparer.GetHashCode((object)this.m_Item6), comparer.GetHashCode((object)this.m_Item7), tuple.GetHashCode(comparer));
                case 6:
                    return Tuple.CombineHashCodes(comparer.GetHashCode((object)this.m_Item2), comparer.GetHashCode((object)this.m_Item3), comparer.GetHashCode((object)this.m_Item4), comparer.GetHashCode((object)this.m_Item5), comparer.GetHashCode((object)this.m_Item6), comparer.GetHashCode((object)this.m_Item7), tuple.GetHashCode(comparer));
                case 7:
                    return Tuple.CombineHashCodes(comparer.GetHashCode((object)this.m_Item1), comparer.GetHashCode((object)this.m_Item2), comparer.GetHashCode((object)this.m_Item3), comparer.GetHashCode((object)this.m_Item4), comparer.GetHashCode((object)this.m_Item5), comparer.GetHashCode((object)this.m_Item6), comparer.GetHashCode((object)this.m_Item7), tuple.GetHashCode(comparer));
                default:
                    return -1;
            }
        }

        int ITuple.GetHashCode(IEqualityComparer comparer)
        {
            return ((IStructuralEquatable)this).GetHashCode(comparer);
        }

        /// <summary>
        /// 返回一个字符串，该字符串表示此 <see cref="T:System.Tuple`8"/> 实例的值。
        /// </summary>
        /// 
        /// <returns>
        /// 此 <see cref="T:System.Tuple`8"/> 对象的字符串表示形式。
        /// </returns>
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            return ((ITuple)this).ToString(sb);
        }

        string ITuple.ToString(StringBuilder sb)
        {
            sb.Append((object)this.m_Item1);
            sb.Append(", ");
            sb.Append((object)this.m_Item2);
            sb.Append(", ");
            sb.Append((object)this.m_Item3);
            sb.Append(", ");
            sb.Append((object)this.m_Item4);
            sb.Append(", ");
            sb.Append((object)this.m_Item5);
            sb.Append(", ");
            sb.Append((object)this.m_Item6);
            sb.Append(", ");
            sb.Append((object)this.m_Item7);
            sb.Append(", ");
            return ((ITuple)(object)this.m_Rest).ToString(sb);
        }
    }
    public static class Tuple
    {
        /// <summary>
        /// 创建新的 1 元组，即单一实例。
        /// </summary>
        /// 
        /// <returns>
        /// 值为 (<paramref name="item1"/>) 的元组。
        /// </returns>
        /// <param name="item1">元组仅有的分量的值。</param><typeparam name="T1">元组的唯一一个分量的类型。</typeparam>
        public static Tuple<T1> Create<T1>(T1 item1)
        {
            return new Tuple<T1>(item1);
        }

        /// <summary>
        /// 创建新的 2 元组，即二元组。
        /// </summary>
        /// 
        /// <returns>
        /// 值为 (<paramref name="item1"/>, <paramref name="item2"/>) 的 2 元组。
        /// </returns>
        /// <param name="item1">此元组的第一个分量的值。</param><param name="item2">此元组的第二个分量的值。</param><typeparam name="T1">此元组的第一个分量的类型。</typeparam><typeparam name="T2">元组的第二个分量的类型。</typeparam>
        public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
        {
            return new Tuple<T1, T2>(item1, item2);
        }

        /// <summary>
        /// 创建新的 3 元组，即三元组。
        /// </summary>
        /// 
        /// <returns>
        /// 值为 (<paramref name="item1"/>, <paramref name="item2"/>, <paramref name="item3"/>) 的 3 元组。
        /// </returns>
        /// <param name="item1">此元组的第一个分量的值。</param><param name="item2">此元组的第二个分量的值。</param><param name="item3">此元组的第三个分量的值。</param><typeparam name="T1">此元组的第一个分量的类型。</typeparam><typeparam name="T2">元组的第二个分量的类型。</typeparam><typeparam name="T3">元组的第三个分量的类型。</typeparam>
        public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
        {
            return new Tuple<T1, T2, T3>(item1, item2, item3);
        }

        /// <summary>
        /// 创建新的 4 元组，即四元组。
        /// </summary>
        /// 
        /// <returns>
        /// 值为 (<paramref name="item1"/>, <paramref name="item2"/>, <paramref name="item3"/>, <paramref name="item4"/>) 的 4 元组。
        /// </returns>
        /// <param name="item1">此元组的第一个分量的值。</param><param name="item2">此元组的第二个分量的值。</param><param name="item3">此元组的第三个分量的值。</param><param name="item4">此元组的第四个分量的值。</param><typeparam name="T1">此元组的第一个分量的类型。</typeparam><typeparam name="T2">元组的第二个分量的类型。</typeparam><typeparam name="T3">元组的第三个分量的类型。</typeparam><typeparam name="T4">此元组的第四个分量的类型。</typeparam>
        public static Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            return new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4);
        }

        /// <summary>
        /// 创建新的 5 元组，即五元组。
        /// </summary>
        /// 
        /// <returns>
        /// 值为 (<paramref name="item1"/>, <paramref name="item2"/>, <paramref name="item3"/>, <paramref name="item4"/>, <paramref name="item5"/>) 的 5 元组。
        /// </returns>
        /// <param name="item1">此元组的第一个分量的值。</param><param name="item2">此元组的第二个分量的值。</param><param name="item3">此元组的第三个分量的值。</param><param name="item4">此元组的第四个分量的值。</param><param name="item5">此元组的第五个分量的值。</param><typeparam name="T1">此元组的第一个分量的类型。</typeparam><typeparam name="T2">元组的第二个分量的类型。</typeparam><typeparam name="T3">元组的第三个分量的类型。</typeparam><typeparam name="T4">此元组的第四个分量的类型。</typeparam><typeparam name="T5">此元组的第五个分量的类型。</typeparam>
        public static Tuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
        {
            return new Tuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
        }

        /// <summary>
        /// 创建新的 6 元组，即六元组。
        /// </summary>
        /// 
        /// <returns>
        /// 值为 (<paramref name="item1"/>, <paramref name="item2"/>, <paramref name="item3"/>, <paramref name="item4"/>, <paramref name="item5"/>, <paramref name="item6"/>) 的 6 元组。
        /// </returns>
        /// <param name="item1">此元组的第一个分量的值。</param><param name="item2">此元组的第二个分量的值。</param><param name="item3">此元组的第三个分量的值。</param><param name="item4">此元组的第四个分量的值。</param><param name="item5">此元组的第五个分量的值。</param><param name="item6">此元组的第六个分量的值。</param><typeparam name="T1">此元组的第一个分量的类型。</typeparam><typeparam name="T2">元组的第二个分量的类型。</typeparam><typeparam name="T3">元组的第三个分量的类型。</typeparam><typeparam name="T4">此元组的第四个分量的类型。</typeparam><typeparam name="T5">此元组的第五个分量的类型。</typeparam><typeparam name="T6">此元组的第六个分量的类型。</typeparam>
        public static Tuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
        }

        /// <summary>
        /// 创建新的 7 元组，即七元组。
        /// </summary>
        /// 
        /// <returns>
        /// 值为 (<paramref name="item1"/>, <paramref name="item2"/>, <paramref name="item3"/>, <paramref name="item4"/>, <paramref name="item5"/>, <paramref name="item6"/>, <paramref name="item7"/>) 的 7 元组。
        /// </returns>
        /// <param name="item1">此元组的第一个分量的值。</param><param name="item2">此元组的第二个分量的值。</param><param name="item3">此元组的第三个分量的值。</param><param name="item4">此元组的第四个分量的值。</param><param name="item5">此元组的第五个分量的值。</param><param name="item6">此元组的第六个分量的值。</param><param name="item7">元组的第七个分量的值。</param><typeparam name="T1">此元组的第一个分量的类型。</typeparam><typeparam name="T2">元组的第二个分量的类型。</typeparam><typeparam name="T3">元组的第三个分量的类型。</typeparam><typeparam name="T4">此元组的第四个分量的类型。</typeparam><typeparam name="T5">此元组的第五个分量的类型。</typeparam><typeparam name="T6">此元组的第六个分量的类型。</typeparam><typeparam name="T7">元组的第七个分量的类型。</typeparam>
        public static Tuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
        }

        /// <summary>
        /// 创建新的 8 元组，即八元组。
        /// </summary>
        /// 
        /// <returns>
        /// 值为 (<paramref name="item1"/>, <paramref name="item2"/>, <paramref name="item3"/>, <paramref name="item4"/>, <paramref name="item5"/>, <paramref name="item6"/>, <paramref name="item7"/>, <paramref name="item8"/>) 的 8 元祖（八元组）。
        /// </returns>
        /// <param name="item1">此元组的第一个分量的值。</param><param name="item2">此元组的第二个分量的值。</param><param name="item3">此元组的第三个分量的值。</param><param name="item4">此元组的第四个分量的值。</param><param name="item5">此元组的第五个分量的值。</param><param name="item6">此元组的第六个分量的值。</param><param name="item7">元组的第七个分量的值。</param><param name="item8">元组的第八个分量的值。</param><typeparam name="T1">此元组的第一个分量的类型。</typeparam><typeparam name="T2">元组的第二个分量的类型。</typeparam><typeparam name="T3">元组的第三个分量的类型。</typeparam><typeparam name="T4">此元组的第四个分量的类型。</typeparam><typeparam name="T5">此元组的第五个分量的类型。</typeparam><typeparam name="T6">此元组的第六个分量的类型。</typeparam><typeparam name="T7">元组的第七个分量的类型。</typeparam><typeparam name="T8">元组的第八个分量的类型。</typeparam>
        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>(item1, item2, item3, item4, item5, item6, item7, new Tuple<T8>(item8));
        }


        internal static int CombineHashCodes(int h1, int h2)
        {
            return (((h1 << 5) + h1) ^ h2);
        }

        internal static int CombineHashCodes(int h1, int h2, int h3)
        {
            return CombineHashCodes(CombineHashCodes(h1, h2), h3);
        }

        internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
        {
            return CombineHashCodes(CombineHashCodes(h1, h2), CombineHashCodes(h3, h4));
        }

        internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
        {
            return CombineHashCodes(CombineHashCodes(h1, h2, h3, h4), h5);
        }

        internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6)
        {
            return CombineHashCodes(CombineHashCodes(h1, h2, h3, h4), CombineHashCodes(h5, h6));
        }

        internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
        {
            return CombineHashCodes(CombineHashCodes(h1, h2, h3, h4), CombineHashCodes(h5, h6, h7));
        }

        internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
        {
            return CombineHashCodes(CombineHashCodes(h1, h2, h3, h4), CombineHashCodes(h5, h6, h7, h8));
        } 
    }
}
