using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WTAN.CommonUtility
{
    public static class LogHelper
    {
        public static void AddLog(this string logText)
        {
            string filePath = HttpContext.Current == null ? HttpRuntime.AppDomainAppPath + "log" : HttpContext.Current.Server.MapPath("\\log");
            string fileName = string.Format("{0}\\{1}.mlg", filePath, DateTime.Now.Date.ToString("yyyyMMdd"));
            if (!System.IO.Directory.Exists(filePath))
            {
                System.IO.Directory.CreateDirectory(filePath);
            }

            using (var sw = System.IO.File.Exists(fileName) ? System.IO.File.AppendText(fileName) : System.IO.File.CreateText(fileName))
            {
                sw.WriteLine(string.Format("{0}\t{1}", DateTime.Now.ToString("HH:mm:ss.fff"), logText));
                sw.Close();
            }
        }

        public static void AddLog(this Exception ex, String className, String functionName)
        {
            string log = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("[{0}:{1}]\t", className, functionName);
            sb.AppendFormat("Error:{0}", ex.Message);
            log = sb.ToString();
            log.AddLog();
        }
    }
}
