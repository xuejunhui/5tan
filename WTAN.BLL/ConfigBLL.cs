using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.IDAL;
using WTAN.DALFactory;
using WTAN.Model.DModel;
using WTAN.Model.VModel;
using WTAN.CommonUtility;

namespace WTAN.BLL
{
    public class ConfigBLL
    {
        IConfig config = DataAccess.CreateConfigInfo();

        #region 靜態
        private static SysConfigModel _CurrentSysConfigInfo = null;
        /// <summary>
        /// 系統當前的基本配置
        /// </summary>
        public static SysConfigModel CurrentSysConfigInfo {
            get {
                if (_CurrentSysConfigInfo == null)
                {
                    ConfigBLL bll = new ConfigBLL();
                    _CurrentSysConfigInfo = bll.GetSysConfigInfo(WebName.Demo);
                }
                return _CurrentSysConfigInfo;
            }
        }

        public static SysConfigModel CurrentNetWorkSysConfigInfo
        {
            get
            {
                if (_CurrentSysConfigInfo == null)
                {
                    ConfigBLL bll = new ConfigBLL();
                    _CurrentSysConfigInfo = bll.GetSysConfigInfo(WebName.NetWork);
                }
                return _CurrentSysConfigInfo;
            }
        }
        #endregion

        public List<Sys_ConfigTB> GetAdsConfigs(String ap,WebName webName)
        {
            return config.GetConfigInfoList(ap.ToString(), webName);
        }


        public AdsConfigModel GetAdsConfigInfo(String ap ,AdsType adstype,int autokey)
        {
            AdsConfigModel model = new AdsConfigModel(ap, adstype);
            Sys_ConfigTB tb = config.GetConfigInfo(autokey, model.Sys_Type);
            model.BindModelData(tb);
            return model;
        }

        /// <summary>
        /// 获取系统基本配置
        /// </summary>
        /// <returns></returns>
        public SysConfigModel GetSysConfigInfo(WebName webname)
        {
            SysConfigModel model = new SysConfigModel();
            Sys_ConfigTB tb = config.GetConfigInfo(model.Sys_Type, webname);
            model.BindModelData(tb);
            return model;
        }

        /// <summary>
        /// 保存系统基本配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Boolean SaveSysConfigInfo(VModelBase model)
        {
            Sys_ConfigTB tb = new Sys_ConfigTB();
            model.BindToDataModel(tb);
            if (_CurrentSysConfigInfo != null)//更新基本配置時重置
                _CurrentSysConfigInfo = null;
            return config.SaveConfigInfo(tb);
        }

        public Boolean DelSysConfigInfo(String id)
        {
            return config.DelConfigInfo(id);
        }
    }
}
