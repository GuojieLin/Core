﻿using System;
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
        public static void WaitAll(params Operator[] @operators)
        {
            foreach (var @operator in @operators)
            {
                @operator.Wait();
            }
        }
        #region Action
        
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
        public static ActionAsync<T> Invoke<T>(Action<T> action, T parameter)
        {
            ActionAsync<T> @operator = new ActionAsync<T>(action, parameter);
            @operator.Invoke();
            return @operator;
        }

        public static ActionAsync<T1, T2> Invoke<T1, T2>(Action<T1, T2> action, T1 parameter1, T2 parameter2)
        {
            ActionAsync<T1, T2> @operator = new ActionAsync<T1, T2>(action, parameter1, parameter2);
            @operator.Invoke();
            return @operator;
        }

        public static ActionAsync<T1, T2, T3> Invoke<T1, T2, T3>(Action<T1, T2, T3> action, T1 parameter1, T2 parameter2, T3 parameter3)
        {
            ActionAsync<T1, T2, T3> @operator = new ActionAsync<T1, T2, T3>(action, parameter1, parameter2, parameter3);
            @operator.Invoke();

            return @operator;
        }


        public static ActionAsync<T1, T2, T3, T4> Invoke<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 parameter1, T2 parameter2,
            T3 parameter3, T4 parameter4)
        {
            ActionAsync<T1, T2, T3, T4> @operator = new ActionAsync<T1, T2, T3, T4>(action, parameter1, parameter2, parameter3, parameter4);
            @operator.Invoke();
            return @operator;
        }

        public static ActionAsync<T1, T2, T3, T4, T5> Invoke<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action, T1 parameter1, T2 parameter2,
            T3 parameter3, T4 parameter4, T5 parameter5)
        {
            ActionAsync<T1, T2, T3, T4, T5> @operator = new ActionAsync<T1, T2, T3, T4, T5>(action, parameter1, parameter2, parameter3, parameter4, parameter5);
            @operator.Invoke();
            return @operator;
        }
        public static ActionAsync<T1, T2, T3, T4, T5, T6> Invoke<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action, T1 parameter1, T2 parameter2,
            T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6)
        {
            ActionAsync<T1, T2, T3, T4, T5, T6> @operator = new ActionAsync<T1, T2, T3, T4, T5, T6>(action, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
            @operator.Invoke();

            return @operator;
        }
        public static ActionAsync<T1, T2, T3, T4, T5, T6, T7> Invoke<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action, T1 parameter1, T2 parameter2,
    T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7)
        {
            ActionAsync<T1, T2, T3, T4, T5, T6, T7> @operator = new ActionAsync<T1, T2, T3, T4, T5, T6, T7>(action, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7);
            @operator.Invoke();

            return @operator;
        }

        public static ActionAsync<T1, T2, T3, T4, T5, T6, T7, T8> Invoke<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 parameter1, T2 parameter2,
            T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8)
        {
            ActionAsync<T1, T2, T3, T4, T5, T6, T7, T8> @operator = new ActionAsync<T1, T2, T3, T4, T5, T6, T7, T8>(action, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8);
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
        public static IEnumerable<ActionAsync<TParameter>> Foreach<TParameter>(IEnumerable<TParameter> items, Action<TParameter> action)
        {
            return items.Select(t => Invoke(action, t)).ToList();
        }

        #endregion
        #region Func



        /// <summary>
        /// 异步调用方法
        /// 自动从线程池获取一个空闲线程去去执行操作
        /// </summary>
        /// <param name="func"></param>
        public static FuncAsync<TResult> Invoke<TResult>(Func<TResult> func)
        {

            FuncAsync<TResult> @operator = new FuncAsync<TResult>(func);
            @operator.Invoke();
            return @operator;
        }
        public static FuncAsync<TParameter, TResult> Invoke<TParameter, TResult>(Func<TParameter, TResult> func, TParameter parameter)
        {
            TParameter param = parameter;
            FuncAsync<TParameter, TResult> @operator = new FuncAsync<TParameter, TResult>(func, param);
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
        public static FuncAsync<T1, T2, TResult> Invoke<T1, T2, TResult>(Func<T1, T2, TResult> func, T1 parameter1, T2 parameter2)
        {
            FuncAsync<T1, T2, TResult> @operator = new FuncAsync<T1, T2, TResult>(func, parameter1, parameter2);

            @operator.Invoke();
            return @operator;
        }

        public static FuncAsync<T1, T2, T3, TResult> Invoke<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, T1 parameter1, T2 parameter2,
            T3 parameter3)
        {
            FuncAsync<T1, T2, T3, TResult> @operator = new FuncAsync<T1, T2, T3, TResult>(func, parameter1, parameter2, parameter3);
            @operator.Invoke();

            return @operator;
        }

        public static FuncAsync<T1, T2, T3, T4, TResult> Invoke<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func, T1 parameter1,
            T2 parameter2, T3 parameter3, T4 parameter4)
        {
            FuncAsync<T1, T2, T3, T4, TResult> @operator = new FuncAsync<T1, T2, T3, T4, TResult>(func, parameter1, parameter2, parameter3,
                parameter4);
            @operator.Invoke();

            return @operator;
        }

        public static FuncAsync<T1, T2, T3, T4, T5, TResult> Invoke<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> func, T1 parameter1,
            T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5)
        {
            FuncAsync<T1, T2, T3, T4,T5, TResult> @operator = new FuncAsync<T1, T2, T3, T4, T5, TResult>(func, parameter1, parameter2, parameter3,
                parameter4, parameter5);

            @operator.Invoke();
            return @operator;
        }

        public static FuncAsync<T1, T2, T3, T4, T5, T6, TResult> Invoke<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> func, T1 parameter1,
            T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6)
        {
            FuncAsync<T1, T2, T3, T4, T5, T6, TResult> @operator = new FuncAsync<T1, T2, T3, T4, T5, T6, TResult>(func, parameter1, parameter2, parameter3,
                parameter4, parameter5, parameter6);

            @operator.Invoke();
            return @operator;
        }
        public static FuncAsync<T1, T2, T3, T4, T5, T6, T7, TResult> Invoke<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, T1 parameter1,
            T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7)
        {
            FuncAsync<T1, T2, T3, T4, T5, T6, T7, TResult> @operator = new FuncAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(func, parameter1, parameter2, parameter3,
                parameter4, parameter5, parameter6, parameter7);

            @operator.Invoke();
            return @operator;
        }
        public static FuncAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Invoke<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, T1 parameter1,
            T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8)
        {
            FuncAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult> @operator = new FuncAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(func, parameter1, parameter2, parameter3,
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
        public static IEnumerable<FuncAsync<TParameter, TResult>> Foreach<TParameter, TResult>(IEnumerable<TParameter> items, Func<TParameter, TResult> func)
        {
            return items.Select(parameter => Invoke(func, parameter)).ToList();
        }

        #endregion
        #region ContinueWithAction
        /// <summary>
        /// 等待所有执行完成后继续执行
        /// </summary>
        /// <param name="action"></param>
        /// <param name="operators"></param>
        /// <returns></returns>
        public static ActionAsync ContinueWithAsync(IEnumerable<Operator> operators, Action action)
        {
            ActionAsync next = Invoke(WaitAll, operators).ContinueWithAsync(action);
            return next;
        }
        public static ActionAsync<Operator> ContinueWithAsync(IEnumerable<Operator> operators, Action<Operator> action)
        {
            ActionAsync<Operator> next = Invoke(WaitAll, operators).ContinueWithAsync(action);
            return next;
        }
        public static ActionAsync<Operator, TParameter> ContinueWithAsync<TParameter>(IEnumerable<Operator> operators, Action<Operator, TParameter> action, TParameter parameter)
        {
            ActionAsync<Operator,TParameter> next = Invoke(WaitAll, operators).ContinueWithAsync(action, parameter);
            return next;
        }
        public static ActionAsync<Operator, TParameter1, TParameter2> ContinueWithAsync<TParameter1, TParameter2>(IEnumerable<Operator> operators, Action<Operator, TParameter1, TParameter2> action, TParameter1 parameter1, TParameter2 parameter2)
        {
            ActionAsync<Operator,TParameter1,TParameter2>  next = Invoke(WaitAll, operators)
                .ContinueWithAsync(action, parameter1, parameter2);
            return next;
        }
        public static ActionAsync<Operator, TParameter1, TParameter2, TParameter3> ContinueWithAsync<TParameter1, TParameter2, TParameter3>
            (IEnumerable<Operator> operators, Action<Operator, TParameter1, TParameter2, TParameter3> action, 
            TParameter1 parameter1, 
            TParameter2 parameter2,
            TParameter3 parameter3)
        {
            ActionAsync<Operator, TParameter1, TParameter2, TParameter3> next = Invoke(WaitAll, operators)
                .ContinueWithAsync(action, parameter1, parameter2, parameter3);
            return next;
        }
        public static ActionAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4> ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4>
            (IEnumerable<Operator> operators,
            Action<Operator, TParameter1, TParameter2, TParameter3, TParameter4> action,
            TParameter1 parameter1,
            TParameter2 parameter2, 
            TParameter3 parameter3, 
            TParameter4 parameter4)
        {
            ActionAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4>  next = Invoke(WaitAll, operators).ContinueWithAsync(action, parameter1, parameter2, parameter3, parameter4);
            
            return next;
        }
        #endregion

        #region ContinueWithFunc
        /// <summary>
        /// 等待所有执行完成后继续执行
        /// </summary>
        /// <param name="func"></param>
        /// <param name="operators"></param>
        /// <returns></returns>
        public static FuncAsync<TResult> ContinueWithAsync<TResult>(IEnumerable<Operator> operators, Func<TResult> func)
        {
            FuncAsync<TResult> next = Invoke(WaitAll, operators).ContinueWithAsync(func);
            
            return next;
        }
        public static FuncAsync<Operator, TResult> ContinueWithAsync<TResult>(IEnumerable<Operator> operators, Func<Operator, TResult> func)
        {
            FuncAsync<Operator, TResult> next = Invoke(WaitAll, operators).ContinueWithAsync(func);
            return next;
        }
        public static FuncAsync<Operator, TParameter, TResult> ContinueWithAsync<TParameter, TResult>(IEnumerable<Operator> operators,
            Func<Operator,TParameter, TResult> func, TParameter parameter)
        {
            FuncAsync<Operator, TParameter, TResult>  next = Invoke(WaitAll, operators).ContinueWithAsync(func, parameter);
            
            return next;
        }

        public static FuncAsync<Operator, TParameter1, TParameter2, TResult> ContinueWithAsync
            <TParameter1, TParameter2, TResult>
            (IEnumerable<Operator> operators,
                Func<Operator, TParameter1, TParameter2, TResult> func,
                TParameter1 parameter1,
                TParameter2 parameter2)
        {
            FuncAsync<Operator, TParameter1, TParameter2, TResult> next =
                Invoke(WaitAll, operators)
                    .ContinueWithAsync(func, parameter1, parameter2);
            return next;
        }

        public static FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TResult> ContinueWithAsync<TParameter1, TParameter2, TParameter3, TResult>
            (IEnumerable<Operator> operators,
            Func<Operator, TParameter1, TParameter2, TParameter3, TResult> func,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3)
        {
            FuncAsync<Operator, TParameter1, TParameter2,TParameter3, TResult>  next = Invoke(WaitAll, operators).ContinueWithAsync(func, parameter1, parameter2, parameter3);
            
            return next;
        }

        public static FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TResult> ContinueWithAsync
            <TParameter1, TParameter2, TParameter3, TParameter4, TResult>
            (IEnumerable<Operator> operators,
                Func<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TResult> func,
                TParameter1 parameter1,
                TParameter2 parameter2,
                TParameter3 parameter3,
                TParameter4 parameter4)
        {

            FuncAsync<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TResult> next = 
                Invoke(WaitAll,operators)
                .ContinueWithAsync(func, parameter1, parameter2, parameter3, parameter4);
            return next;
            //TODO:BUG CotinueWithAsync之后无法调用Wait等待
        }

        #endregion
    }
}
