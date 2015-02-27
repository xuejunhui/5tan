using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;
using WTAN.CommonUtility;

namespace WTAN.IDAL
{
    public interface IConfig
    {
        /// <summary>
        /// 獲取網站配置列表
        /// </summary>
        /// <param name="configType"></param>
        /// <returns></returns>
        List<Sys_ConfigTB> GetConfigInfoList(String configType,WebName webName);

        /// <summary>
        /// 获取网站的某一配置
        /// </summary>
        /// <param name="configType"></param>
        /// <returns></returns>
        Sys_ConfigTB GetConfigInfo(String configType, WebName webName);

        /// <summary>
        /// 获取网站某一配置
        /// </summary>
        /// <param name="autokey"></param>
        /// <param name="Sys_Type"></param>
        /// <returns></returns>
        Sys_ConfigTB GetConfigInfo(int autokey, String Sys_Type = "");

        /// <summary>
        /// 保存网站的某一配置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        Boolean SaveConfigInfo(Sys_ConfigTB config);

        /// <summary>
        /// 刪除系統某一配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Boolean DelConfigInfo(String id);
        
    }
}
