using System;
using System.Collections.Generic;
using System.Linq;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	12/19/2015 12:38:59 PM			//
//			创建日期:	2015				            //
//======================================================//
//通用的异步操作方法
//2015.12.19    添加Action、Action<T>、Func<TResult>、Func<T,TResult>操作方法
//              实现等待全部操作完成
//2016.01.19    异步执行完成后继续执行操作
//2016.04.26    添加ActionAsync和FuncAsync的5，6，7，8个参数重置
namespace Jake.V35.Core.Async
{
    /// <summary>
    /// 异步执行
    /// </summary>
    public class Asynchronous
    {
        /// <summary> 
        ///为空则表示全部操作都需要等待
        ///由于可能包过其他线程的操作,会等待所有异步操作都结束才会返回
        ///可能会导致程序卡很久
        ///因此必须指定操作
        /// </summary>
        /// <param name="operators"></param>
        public static void WaitAll(IEnumerable<Operator> @operators)
        {
            foreach (var @operator in @operators)
            {
                @operator.Wait();
            }
        }
        #region Action
        /// <summary>
        /// 异步调用方法
        /// 自动从线程池获取一个空闲线程去去执行操作
        /// </summary>
        /// <param name="action"></param>
        public static Operator Invoke(Action action)
        {
            Operator @operator = new ActionAsync(action);
            @operator.Invoke();
            return @operator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static Operator Invoke<T>(Action<T> action, T parameter)
        {
            Operator @operator = new ActionAsync<T>(action, parameter);
            @operator.Invoke();
            return @operator;
        }

        public static Operator Invoke<T1, T2>(Action<T1, T2> action, T1 parameter1, T2 parameter2)
        {
            Operator @operator = new ActionAsync<T1, T2>(action, parameter1, parameter2);
            @operator.Invoke();
            return @operator;
        }

        public static Operator Invoke<T1, T2, T3>(Action<T1, T2, T3> action, T1 parameter1, T2 parameter2, T3 parameter3)
        {
            Operator @operator = new ActionAsync<T1, T2, T3>(action, parameter1, parameter2, parameter3);
            @operator.Invoke();
            return @operator;
        }

        public static Operator Invoke<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 parameter1, T2 parameter2,
            T3 parameter3, T4 parameter4)
        {
            Operator @operator = new ActionAsync<T1, T2, T3, T4>(action, parameter1, parameter2, parameter3, parameter4);
            @operator.Invoke();
            return @operator;
        }

        public static Operator Invoke<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action, T1 parameter1, T2 parameter2,
            T3 parameter3, T4 parameter4, T5 parameter5)
        {
            Operator @operator = new ActionAsync<T1, T2, T3, T4, T5>(action, parameter1, parameter2, parameter3, parameter4, parameter5);
            @operator.Invoke();
            return @operator;
        }

        public static Operator Invoke<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action, T1 parameter1, T2 parameter2,
            T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6)
        {
            Operator @operator = new ActionAsync<T1, T2, T3, T4, T5, T6>(action, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
            @operator.Invoke();
            return @operator;
        }
        public static Operator Invoke<T1, T2, T3, T4, T5, T6,T7>(Action<T1, T2, T3, T4, T5, T6, T7> action, T1 parameter1, T2 parameter2,
            T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7)
        {
            Operator @operator = new ActionAsync<T1, T2, T3, T4, T5, T6, T7>(action, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7);
            @operator.Invoke();
            return @operator;
        }
        public static Operator Invoke<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 parameter1, T2 parameter2,
            T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8)
        {
            Operator @operator = new ActionAsync<T1, T2, T3, T4, T5, T6, T7, T8>(action, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8);
            @operator.Invoke();
            return @operator;
        }
        /// <summary>
        /// 遍历集合异步操作
        /// </summary>
        /// <typeparam name="TParameter"></typeparam>
        /// <param name="items"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<Operator> Foreach<TParameter>(IEnumerable<TParameter> items, Action<TParameter> action)
        {
            return items.Select(t => Invoke(action, t)).ToList();
            //不能用yield 否则如果不出发,就不会调用方法
            //foreach (var item in items)
            //{
            //    if (stop) yield break;
            //    var i = item;
            //    yield return Invoke(action, i);
            //}
        }

        public static IEnumerable<Operator> For(int amount, Action action)
        {
            List<Operator> operators = new List<Operator>();
            for (int i = 0; i < amount; i++)
            {
                operators.Add(Invoke(action));
            }
            return operators;
        }
        #endregion
        #region Func

        /// <summary>
        /// 异步调用方法
        /// 自动从线程池获取一个空闲线程去去执行操作
        /// </summary>
        /// <param name="func"></param>
        public static Operator Invoke<TResult>(Func<TResult> func)
        {

            Operator @operator = new FuncAsync<TResult>(func);
            @operator.Invoke();
            return @operator;
        }

        /// <summary>
        /// 异步调用方法
        /// 自动从线程池获取一个空闲线程去去执行操作
        /// </summary>
        /// <param name="func"></param>
        /// <param name="parameter"></param>
        public static Operator Invoke<TParameter, TResult>(Func<TParameter, TResult> func, TParameter parameter)
        {
            TParameter param = parameter;
            Operator @operator = new FuncAsync<TParameter, TResult>(func, param);
            @operator.Invoke();
            return @operator;
        }

        /// <summary>
        /// 异步调用方法
        /// 自动从线程池获取一个空闲线程去执行操作
        /// </summary>
        /// <param name="func"></param>
        /// <param name="parameter1"></param>
        /// <param name="parameter2"></param>
        public static Operator Invoke<T1, T2, TResult>(Func<T1, T2, TResult> func, T1 parameter1, T2 parameter2)
        {
            Operator @operator = new FuncAsync<T1, T2, TResult>(func, parameter1, parameter2);
            @operator.Invoke();
            return @operator;
        }

        public static Operator Invoke<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, T1 parameter1, T2 parameter2,
            T3 parameter3)
        {
            Operator @operator = new FuncAsync<T1, T2, T3, TResult>(func, parameter1, parameter2, parameter3);
            @operator.Invoke();
            return @operator;
        }

        public static Operator Invoke<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func, T1 parameter1,
            T2 parameter2, T3 parameter3, T4 parameter4)
        {
            Operator @operator = new FuncAsync<T1, T2, T3, T4, TResult>(func, parameter1, parameter2, parameter3,
                parameter4);
            @operator.Invoke();
            return @operator;
        }

        public static Operator Invoke<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> func, T1 parameter1,
            T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5)
        {
            Operator @operator = new FuncAsync<T1, T2, T3, T4,T5, TResult>(func, parameter1, parameter2, parameter3,
                parameter4, parameter5);
            @operator.Invoke();
            return @operator;
        }

        public static Operator Invoke<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> func, T1 parameter1,
            T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6)
        {
            Operator @operator = new FuncAsync<T1, T2, T3, T4, T5, T6, TResult>(func, parameter1, parameter2, parameter3,
                parameter4, parameter5, parameter6);
            @operator.Invoke();
            return @operator;
        }
        public static Operator Invoke<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, T1 parameter1,
            T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7)
        {
            Operator @operator = new FuncAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(func, parameter1, parameter2, parameter3,
                parameter4, parameter5, parameter6, parameter7);
            @operator.Invoke();
            return @operator;
        }
        public static Operator Invoke<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, T1 parameter1,
            T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8)
        {
            Operator @operator = new FuncAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(func, parameter1, parameter2, parameter3,
                parameter4, parameter5, parameter6, parameter7, parameter8);
            @operator.Invoke();
            return @operator;
        }
        /// <summary>
        /// 遍历集合异步操作
        /// </summary>
        /// <typeparam name="TParameter"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="items"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IEnumerable<Operator> Foreach<TParameter, TResult>(IEnumerable<TParameter> items, Func<TParameter, TResult> func)
        {
            return items.Select(parameter => Invoke(func, parameter)).ToList();
            //不能用yield 否则如果不出发,就不会调用方法
            //foreach (var item in items)
            //{
            //    if(stop) yield break;
            //    var i = item;
            //    yield return Invoke(func, i);
            //}
        }

        public static IEnumerable<Operator> For<TParameter, TResult>(int start, int end, Func<TParameter, TResult> func, TParameter parameter)
        {
            for (; start < end; start++)
            {
                yield return Invoke(func, parameter);
            }
        }
        public static IEnumerable<Operator> For<TResult>(int start, int end, Func<TResult> func)
        {
            for (; start < end; start++)
            {
                yield return Invoke(func);
            }
        }
        #endregion
        #region ContinueWithAction
        /// <summary>
        /// 等待所有执行完成后继续执行
        /// </summary>
        /// <param name="action"></param>
        /// <param name="operators"></param>
        /// <returns></returns>
        public static Operator ContinueWithAsync(IEnumerable<Operator>operators, Action action)
        {
            return Invoke(WaitAll, operators)
                .ContinueWithAsync(action);
        }
        public static Operator ContinueWithAsync<TParameter>(IEnumerable<Operator> operators, Action<TParameter> action, TParameter parameter)
        {
            return Invoke(WaitAll, operators)
                .ContinueWithAsync(action, parameter);
        }
        public static Operator ContinueWithAsync<TParameter1, TParameter2>(IEnumerable<Operator> operators, Action<TParameter1, TParameter2> action, TParameter1 parameter1, TParameter2 parameter2)
        {
            return Invoke(WaitAll, operators)
                .ContinueWithAsync(action, parameter1, parameter2);
        }
        public static Operator ContinueWithAsync<TParameter1, TParameter2, TParameter3>
            (IEnumerable<Operator> operators, Action<TParameter1, TParameter2, TParameter3> action, 
            TParameter1 parameter1, 
            TParameter2 parameter2,
            TParameter3 parameter3)
        {
            return Invoke(WaitAll, operators)
                .ContinueWithAsync(action, parameter1, parameter2, parameter3);
        }
        public static Operator ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4>
            (IEnumerable<Operator> operators, 
            Action<TParameter1, TParameter2, TParameter3, TParameter4> action,
            TParameter1 parameter1,
            TParameter2 parameter2, 
            TParameter3 parameter3, 
            TParameter4 parameter4)
        {
            return Invoke(WaitAll, operators)
                .ContinueWithAsync(action, parameter1, parameter2, parameter3, parameter4);
        }
        #endregion

        #region ContinueWithFunc
        /// <summary>
        /// 等待所有执行完成后继续执行
        /// </summary>
        /// <param name="func"></param>
        /// <param name="operators"></param>
        /// <returns></returns>
        public static Operator ContinueWithAsync<TResult>(IEnumerable<Operator> operators,Func<TResult> func)
        {
            return Invoke(WaitAll, operators)
                .ContinueWithAsync(func);
        }
        public static Operator ContinueWithAsync<TParameter, TResult>(IEnumerable<Operator> operators, 
            Func<TParameter, TResult> func, TParameter parameter)
        {
            return Invoke(WaitAll, operators)
                .ContinueWithAsync(func, parameter);
        }
        public static Operator ContinueWithAsync<TParameter1, TParameter2, TResult>
            (IEnumerable<Operator> operators, 
            Func<TParameter1, TParameter2, TResult> func, 
            TParameter1 parameter1,
            TParameter2 parameter2)
        {
            return Invoke(WaitAll, operators)
                .ContinueWithAsync(func, parameter1, parameter2);
        }
        public static Operator ContinueWithAsync<TParameter1, TParameter2, TParameter3, TResult>
            (IEnumerable<Operator> operators, 
            Func<TParameter1, TParameter2, TParameter3, TResult> func,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3)
        {
            return Invoke(WaitAll, operators)
                .ContinueWithAsync(func, parameter1, parameter2, parameter3);
        }
        public static Operator ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TResult>
            (IEnumerable<Operator> operators, 
            Func<TParameter1, TParameter2, TParameter3, TParameter4, TResult> func,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4)
        {
            return Invoke(WaitAll, operators)
                .ContinueWithAsync(func, parameter1, parameter2, parameter3, parameter4);
            //TODO:BUG CotinueWithAsync之后无法调用Wait等待
        }
        #endregion
    }
}
