using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	8/1/2016 4:54:57 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace Jake.V35.Core.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取标签Name
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="messageType"></param>
        /// <returns></returns>
        public static string GetAttributeValue<TAttribute>(TAttribute messageType)
        {
            var value = messageType.GetType().GetCustomAttributes(typeof(TAttribute), true);
            if (!value.Any())
                return null;
            return ((TAttribute)value[0]).ToString();
        }
        public static string GetValue(this Enum em)
        {
            Type type = em.GetType();
            FieldInfo fieldInfo = type.GetField(em.ToString());
            if (fieldInfo == null)
                return string.Empty;
            ValueAttribute[] attrs = (ValueAttribute[])fieldInfo.GetCustomAttributes(typeof(ValueAttribute), false);
            return attrs.Length > 0 ? attrs[0].ToString() : "";
        }

    }
}
