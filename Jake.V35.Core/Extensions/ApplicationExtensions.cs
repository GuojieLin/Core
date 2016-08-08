using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	7/18/2016 4:12:52 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace Jake.V35.Core.Extensions
{
    /// <summary>
    /// 获取本地局域网ip地址
    /// </summary>
    public static class ApplicationExtensions
    {
        /// <summary>
        /// A类：10.0.0.0-10.255.255.255
        /// B类：172.16.0.0-172.31.255.255 
        /// C类：192.168.0.0-192.168.255.255
        /// </summary>
        private static readonly string[] InternalNetwork = new[]
        {
            "10.",
            "172.16.", "172.17.", "172.18.", "172.19.", "172.20.", "172.21.", "172.22.", "172.23.", "172.24.", "172.25.",
            "172.26.", "172.27.", "172.28.", "172.29.", "172.30.", "172.33.",
            "192.168."
        };
        public static string GetLocalIp(this AppDomain appDomain)
        {
            IPHostEntry myEntry = Dns.GetHostEntry(Dns.GetHostName());
            var newWork =
                myEntry.AddressList.FirstOrDefault<IPAddress>(
                    e =>
                        e.AddressFamily == AddressFamily.InterNetwork &&
                        InternalNetwork.Any(interNalIp => e.ToString().Contains(interNalIp)));
            return newWork != null ? newWork.ToString() : "";
        }
    }
}
