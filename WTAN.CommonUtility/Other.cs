using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace WTAN.CommonUtility
{
    public static class Other
    {
        /// <summary>
        /// 獲取客服端IP
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }

            return AddressIP;
        }
    }
}
