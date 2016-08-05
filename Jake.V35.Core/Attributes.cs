using System;
using System.IO;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using Jake.V35.Core.Logging;

namespace Jake.V35.Core
{
    [AttributeUsage(AttributeTargets.All,AllowMultiple = false,Inherited = false)]
    public class ValueAttribute : Attribute
    {
        public string RealValue { get; set; }

        public ValueAttribute(string value)
        {
            RealValue = value;
        }

        public override string ToString()
        {
            return RealValue;
        }
    }
    /// <summary>
    /// 自动日志写入
    /// </summary>
    public class AutoLogAttribute : ProxyAttribute
    {
        public override MarshalByRefObject CreateInstance(Type serverType)
        {
            AopLogProxy realProxy = new AopLogProxy(serverType);
            return realProxy.GetTransparentProxy() as MarshalByRefObject;
        }
    }

    public class AopLogProxy : RealProxy
    {
        public static readonly string LoadLog = Path.Combine("LoadLogs", DateTime.Now.ToString("yyyyMMddHHmmss") + ".load");
        public static readonly string CoreLog = Path.Combine("CoreLogs", DateTime.Now.ToString("yyyyMMddHHmmss") + ".load");
        private static readonly ILogger Logger = FileLoggerFactory.Default.Create(true, LoadLog);
        public AopLogProxy(Type serverType)
            : base(serverType)
        {
        }
        public override IMessage Invoke(IMessage msg)
        {
            //消息拦截之后，就会执行这里的方法。
            IConstructionCallMessage constructCallMsg = msg as IConstructionCallMessage;
            if (constructCallMsg !=null) // 如果是构造函数，按原来的方式返回即可。
            {
                IConstructionReturnMessage constructionReturnMessage = this.InitializeServerObject((IConstructionCallMessage)msg);
                if (constructionReturnMessage != null)
                {
                    RealProxy.SetStubData(this, constructionReturnMessage.ReturnValue);
                    return constructionReturnMessage;
                }
            }
            IMethodCallMessage callMsg = msg as IMethodCallMessage;
            if (callMsg != null) //如果是方法调用（属性也是方法调用的一种）
            {
                object[] args = callMsg.Args;
                IMessage message;
                try
                {
                    if (callMsg.MethodName.StartsWith("set_") && args.Length == 1)
                    {
                        //这里检测到是set方法
                        //在这里切入日志记录
                        if (!args[0].GetType().IsByRef || 
                            args[0].GetType().MemberType == typeof(string).MemberType)
                        {
                            Logger.WriteInfo(string.Format("加载{0}成功,值：{1}",
                                callMsg.MethodName.Substring(callMsg.MethodName.IndexOf("_", StringComparison.Ordinal) + 1), args[0]));
                        }
                    }
                    object o = callMsg.MethodBase.Invoke(GetUnwrappedServer(), args);
                    message = new ReturnMessage(o, args, args.Length, callMsg.LogicalCallContext, callMsg);
                }
                catch (Exception e)
                {
                    message = new ReturnMessage(e, callMsg);
                }
                return message;
            }
            return msg;
        }
    }
}
