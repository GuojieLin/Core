using System;
using System.Collections.Generic;
using System.Linq;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	12/26/2015 11:10:56 AM			//
//			创建日期:	2015				            //
//======================================================//
//2015.12.26 加入异常构造函数
namespace Jake.V35.Core
{
    public class DealResult
    {
        private static readonly DealResult _success;
        public bool Succeeded { get; set; }

        public static DealResult Success
        {
            get { return _success; }
        }

        static DealResult()
        {
            _success = new DealResult(true);
        }

        protected DealResult(bool success)
        {
            this.Succeeded = success;
            this.Errors = new string[0];
        }

        public DealResult(IEnumerable<string> errors)
        {
            if (errors == null)
            {
                errors = new string[] { };
            }
            this.Succeeded = false;
            this.Errors = errors;

        }
        public DealResult(IEnumerable<Exception> errors)
        {
            if (errors == null)
            {
                errors = new Exception[] { };
            }
            this.Succeeded = false;
            this.Errors = errors.SelectMany(e => new[] { e.Message, e.StackTrace });
        }
        public DealResult(params string[] errors)
            : this((IEnumerable<string>)errors)
        {
        }
        public DealResult(params Exception[] exceptions)
            : this((IEnumerable<Exception>)exceptions)
        {
        }

        public static DealResult Failed(params string[] errors)
        {
            return new DealResult(errors);
        }
        public static DealResult Failed(params Exception[] exceptions)
        {
            return new DealResult(exceptions);
        }
        // Properties
        public IEnumerable<string> Errors { get; private set; }

        public string Error
        {
            get { return string.Join(Environment.NewLine, Errors.SkipWhile(string.IsNullOrEmpty).ToArray()); }
        }
        /// <summary>
        /// 可用于链式添加错误
        /// 如：DealResult.Failed("错误1").AddError("错误2")
        /// 不对原对象产生修改
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public DealResult AddError(params string[] error)
        {
            var errors = new List<string>(this.Errors);
            errors.AddRange(error);
            //重新创建对象,以防对静态的成功变量产生影响
            DealResult result = new DealResult(errors);
            return result;
        }
        /// <summary>
        /// 可用于链式添加错误
        /// DealResult result = new DealResult.Failed("错误1")
        /// 如：DealResult.Failed("错误2").AddError(result);
        /// result.AddError(DealResult.Failed("错误3"));
        /// 不对原对象产生修改
        /// </summary>
        /// <param name="preResult"></param>
        /// <returns></returns>
        public DealResult AddError(DealResult preResult)
        {
            var errors = new List<string>(this.Errors);
            errors.AddRange(preResult.Errors);
            //重新创建对象,以防对静态的成功变量产生影响
            DealResult result = new DealResult(errors);
            return result;
        }
    }
}
