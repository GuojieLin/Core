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
        public static void Invoke(Operator @operator)
        {
            @operator.Invoke();
        }
        #region Action
        /// <summary>
        /// 异步调用方法
        /// 自动从线程池获取一个空闲线程去去执行操作
        /// </summary>
        /// <param name="action"></param>
        public static Operator Create(Action action)
        {
            Operator @operator = new ActionAsync(action);
            return @operator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static ActionAsync<T> Create<T>(Action<T> action, T parameter)
        {
            ActionAsync<T> @operator = new ActionAsync<T>(action, parameter);
            
            return @operator;
        }

        public static ActionAsync<T1, T2> Create<T1, T2>(Action<T1, T2> action, T1 parameter1, T2 parameter2)
        {
            ActionAsync<T1, T2> @operator = new ActionAsync<T1, T2>(action, parameter1, parameter2);
            
            return @operator;
        }
        public static ActionAsync<T1, T2, T3> Create<T1, T2, T3>(Action<T1, T2, T3> action, T1 parameter1, T2 parameter2, T3 parameter3)
        {
            ActionAsync<T1, T2, T3> @operator = new ActionAsync<T1, T2, T3>(action, parameter1, parameter2, parameter3);
            
            return @operator;
        }

        public static ActionAsync<T1, T2, T3, T4> Create<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 parameter1, T2 parameter2,
            T3 parameter3, T4 parameter4)
        {
            ActionAsync<T1, T2, T3, T4> @operator = new ActionAsync<T1, T2, T3, T4>(action, parameter1, parameter2, parameter3, parameter4);
            
            return @operator;
        }
        public static ActionAsync<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action, T1 parameter1, T2 parameter2,
            T3 parameter3, T4 parameter4, T5 parameter5)
        {
            ActionAsync<T1, T2, T3, T4, T5> @operator = new ActionAsync<T1, T2, T3, T4, T5>(action, parameter1, parameter2, parameter3, parameter4, parameter5);
            
            return @operator;
        }

        public static ActionAsync<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action, T1 parameter1, T2 parameter2,
            T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6)
        {
            ActionAsync<T1, T2, T3, T4, T5, T6> @operator = new ActionAsync<T1, T2, T3, T4, T5, T6>(action, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
            
            return @operator;
        }
        public static ActionAsync<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action, T1 parameter1, T2 parameter2,
            T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7)
        {
            ActionAsync<T1, T2, T3, T4, T5, T6, T7> @operator = new ActionAsync<T1, T2, T3, T4, T5, T6, T7>(action, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7);
            
            return @operator;
        }
        public static ActionAsync<T1, T2, T3, T4, T5, T6, T7, T8> Create<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 parameter1, T2 parameter2,
            T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8)
        {
            ActionAsync<T1, T2, T3, T4, T5, T6, T7, T8> @operator = new ActionAsync<T1, T2, T3, T4, T5, T6, T7, T8>(action, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8);
            
            return @operator;
        }
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
        public static FuncAsync<TResult> Create<TResult>(Func<TResult> func)
        {

            FuncAsync<TResult> @operator = new FuncAsync<TResult>(func);
            
            return @operator;
        }

        /// <summary>
        /// 异步调用方法
        /// 自动从线程池获取一个空闲线程去去执行操作
        /// </summary>
        /// <param name="func"></param>
        /// <param name="parameter"></param>
        public static FuncAsync<TParameter, TResult> Create<TParameter, TResult>(Func<TParameter, TResult> func, TParameter parameter)
        {
            TParameter param = parameter;
            FuncAsync<TParameter, TResult> @operator = new FuncAsync<TParameter, TResult>(func, param);
            
            return @operator;
        }
        /// <summary>
        /// 异步调用方法
        /// 自动从线程池获取一个空闲线程去执行操作
        /// </summary>
        /// <param name="func"></param>
        /// <param name="parameter1"></param>
        /// <param name="parameter2"></param>
        public static FuncAsync<T1, T2, TResult> Create<T1, T2, TResult>(Func<T1, T2, TResult> func, T1 parameter1, T2 parameter2)
        {
            FuncAsync<T1, T2, TResult> @operator = new FuncAsync<T1, T2, TResult>(func, parameter1, parameter2);
            
            return @operator;
        }

        public static FuncAsync<T1, T2, T3, TResult> Create<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, T1 parameter1, T2 parameter2,
            T3 parameter3)
        {
            FuncAsync<T1, T2, T3, TResult> @operator = new FuncAsync<T1, T2, T3, TResult>(func, parameter1, parameter2, parameter3);
            
            return @operator;
        }

        public static FuncAsync<T1, T2, T3, T4, TResult> Create<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func, T1 parameter1,
            T2 parameter2, T3 parameter3, T4 parameter4)
        {
            FuncAsync<T1, T2, T3, T4, TResult> @operator = new FuncAsync<T1, T2, T3, T4, TResult>(func, parameter1, parameter2, parameter3,
                parameter4);
            
            return @operator;
        }

        public static FuncAsync<T1, T2, T3, T4, T5, TResult> Create<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> func, T1 parameter1,
            T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5)
        {
            FuncAsync<T1, T2, T3, T4,T5, TResult> @operator = new FuncAsync<T1, T2, T3, T4, T5, TResult>(func, parameter1, parameter2, parameter3,
                parameter4, parameter5);
            
            return @operator;
        }

        public static FuncAsync<T1, T2, T3, T4, T5, T6, TResult> Create<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> func, T1 parameter1,
            T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6)
        {
            FuncAsync<T1, T2, T3, T4, T5, T6, TResult> @operator = new FuncAsync<T1, T2, T3, T4, T5, T6, TResult>(func, parameter1, parameter2, parameter3,
                parameter4, parameter5, parameter6);
            
            return @operator;
        }
        public static FuncAsync<T1, T2, T3, T4, T5, T6, T7, TResult> Create<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, T1 parameter1,
            T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7)
        {
            FuncAsync<T1, T2, T3, T4, T5, T6, T7, TResult> @operator = new FuncAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(func, parameter1, parameter2, parameter3,
                parameter4, parameter5, parameter6, parameter7);
            
            return @operator;
        }
        public static FuncAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, T1 parameter1,
            T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8)
        {
            FuncAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult> @operator = new FuncAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(func, parameter1, parameter2, parameter3,
                parameter4, parameter5, parameter6, parameter7, parameter8);
            
            return @operator;
        }



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
            //不能用yield 否则如果不出发,就不会调用方法
            //foreach (var item in items)
            //{
            //    if(stop) yield break;
            //    var i = item;
            //    yield return Create(func, i);
            //}
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
            Operator @operator = Create(WaitAll, operators);
            Operator next = @operator.ContinueWithAsync(action);
            @operator.Invoke();
            return next;
        }
        public static Operator ContinueWithAsync(IEnumerable<Operator> operators, Action<Operator> action)
        {
            Operator @operator = Create(WaitAll, operators);
            Operator next = @operator.ContinueWithAsync(action);
            @operator.Invoke();
            return next;
        }
        public static Operator ContinueWithAsync<TParameter>(IEnumerable<Operator> operators, Action<Operator,TParameter> action, TParameter parameter)
        {
            Operator @operator = Create(WaitAll, operators);
            Operator next = @operator.ContinueWithAsync(action, parameter);
            @operator.Invoke();
            return next;
        }
        public static Operator ContinueWithAsync<TParameter1, TParameter2>(IEnumerable<Operator> operators, Action<Operator, TParameter1, TParameter2> action, TParameter1 parameter1, TParameter2 parameter2)
        {
            Operator @operator = Create(WaitAll, operators);
            Operator next = @operator.ContinueWithAsync(action, parameter1, parameter2);
            @operator.Invoke();
            return next;
        }
        public static Operator ContinueWithAsync<TParameter1, TParameter2, TParameter3>
            (IEnumerable<Operator> operators, Action<Operator, TParameter1, TParameter2, TParameter3> action, 
            TParameter1 parameter1, 
            TParameter2 parameter2,
            TParameter3 parameter3)
        {
            Operator @operator = Create(WaitAll, operators);
            Operator next = @operator.ContinueWithAsync(action, parameter1, parameter2, parameter3);
            @operator.Invoke();
            return next;
        }
        public static Operator ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4>
            (IEnumerable<Operator> operators,
            Action<Operator, TParameter1, TParameter2, TParameter3, TParameter4> action,
            TParameter1 parameter1,
            TParameter2 parameter2, 
            TParameter3 parameter3, 
            TParameter4 parameter4)
        {
            Operator @operator = Create(WaitAll, operators);
            Operator next = @operator.ContinueWithAsync(action, parameter1, parameter2, parameter3, parameter4);
            @operator.Invoke();
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
        public static Operator ContinueWithAsync<TResult>(IEnumerable<Operator> operators,Func<TResult> func)
        {
            Operator @operator = Create(WaitAll, operators);
            Operator next = @operator.ContinueWithAsync(func);
            @operator.Invoke();
            return next;
        }
        public static Operator ContinueWithAsync<TResult>(IEnumerable<Operator> operators, Func<Operator,TResult> func)
        {
            Operator @operator = Create(WaitAll, operators);
            Operator next = @operator.ContinueWithAsync(func);
            @operator.Invoke();
            return next;
        }
        public static Operator ContinueWithAsync<TParameter, TResult>(IEnumerable<Operator> operators,
            Func<Operator,TParameter, TResult> func, TParameter parameter)
        {
            Operator @operator = Create(WaitAll, operators);
            Operator next = @operator.ContinueWithAsync(func, parameter);
            @operator.Invoke();
            return next;
        }
        public static Operator ContinueWithAsync<TParameter1, TParameter2, TResult>
            (IEnumerable<Operator> operators,
            Func<Operator, TParameter1, TParameter2, TResult> func, 
            TParameter1 parameter1,
            TParameter2 parameter2)
        {
            Operator @operator = Create(WaitAll, operators);
            Operator next = @operator.ContinueWithAsync(func, parameter1, parameter2);
            @operator.Invoke();
            return next;
        }
        public static Operator ContinueWithAsync<TParameter1, TParameter2, TParameter3, TResult>
            (IEnumerable<Operator> operators,
            Func<Operator, TParameter1, TParameter2, TParameter3, TResult> func,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3)
        {
            Operator @operator = Create(WaitAll, operators);
            Operator next = @operator.ContinueWithAsync(func, parameter1, parameter2, parameter3);
            @operator.Invoke();
            return next;
        }
        public static Operator ContinueWithAsync<TParameter1, TParameter2, TParameter3, TParameter4, TResult>
            (IEnumerable<Operator> operators,
            Func<Operator, TParameter1, TParameter2, TParameter3, TParameter4, TResult> func,
            TParameter1 parameter1,
            TParameter2 parameter2,
            TParameter3 parameter3,
            TParameter4 parameter4)
        {
            Operator @operator = Create(WaitAll, operators);
            Operator next = @operator.ContinueWithAsync(func, parameter1, parameter2, parameter3, parameter4);
            @operator.Invoke();
            return next;
            //TODO:BUG CotinueWithAsync之后无法调用Wait等待
        }
        #endregion
    }
}
