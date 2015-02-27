using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
//[assembly: PreApplicationStartMethod(typeof(WTAN.BLL.PreApplicationStartCode), "PreStart")]

namespace WTAN.BLL
{
    public class PreApplicationStartCode
    {
        private static bool hasLoaded;

        public static void PreStart()
        {
            if (!hasLoaded)
            {
                hasLoaded = true;
                //注意这里的动态注册，此静态方法在Microsoft.Web.Infrastructure.DynamicModuleHelper
                DynamicModuleUtility.RegisterModule(typeof(StopAppDomainRestartOnFolderDeleteModule));
            }
        }
    }

    public class StopAppDomainRestartOnFolderDeleteModule : IHttpModule 
    {
        private static bool DisableFCNs = false;
        public void Init(HttpApplication context)
        {
            if (DisableFCNs) return;
            PropertyInfo p = typeof(HttpRuntime).GetProperty("FileChangesMonitor", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            object o = p.GetValue(null, null);
            FieldInfo f = o.GetType().GetField("_dirMonSubdirs", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
            object monitor = f.GetValue(o);
            MethodInfo m = monitor.GetType().GetMethod("StopMonitoring", BindingFlags.Instance | BindingFlags.NonPublic);
            m.Invoke(monitor, new object[] { });
            DisableFCNs = true;
        }
        public void Dispose() { } 
    }
}
