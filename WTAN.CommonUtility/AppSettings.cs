using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace WTAN.CommonUtility
{
    /// <summary>
    /// 配置獲取
    /// </summary>
    public static class AppSettings
    {
        private static String _ManageDomain;
        public static String ManageDomain
        {
            get{
                _ManageDomain = GetAppSettingValue("ManageDomain");
                if (_ManageDomain.IsNullOrEmpty())
                    return "http://Manage.5tan.net";
               return _ManageDomain ;   
            }
        }

        public static String WaterMarkText = GetAppSettingValue("WaterMarkText");

        /// <summary>
        /// 允许上传文件大小
        /// </summary>
        public static int UploadFileSize = GetAppSettingValue("UploadFileSize").ToInt32Value();

        /// <summary>
        /// 允许上传图片的最大大小
        /// </summary>
        public static int UplaodImgSize = GetAppSettingValue("UploadImgSize").ToInt32Value();

        /// <summary>
        /// 编辑器文件上传位置
        /// </summary>
        public static String EditUploadDir = GetAppSettingValue("EditUploadDir");

        /// <summary>
        /// 允许上传的图片类型
        /// </summary>
        public static String[] UplaodFileType
        {
            get
            {
                return GetAppSettingValue("UplaodFileType").ToUpper().Split('|');
            }
        }

        /// <summary>
        /// 允许上传的图片类型
        /// </summary>
        public static String[] UplaodImgType
        {
            get
            {
                return GetAppSettingValue("UplaodImgType").ToUpper().Split('|');
            }
        }

        /// <summary>
        /// 广告图片上传位置
        /// </summary>
        public static String AdsUploadDir = GetAppSettingValue("AdsUploadDir"); 

        /// <summary>
        /// 資料庫連接
        /// </summary>
        public static String DefaultConnectionString = GetAppSettingValue("DefaultConnectionString");

        /// <summary>
        /// 是否開啟緩存
        /// </summary>
        public static Boolean IsStartCache = GetAppSettingValue("IsStartCache").ToBoolValue();

        /// <summary>
        /// 緩存時長
        /// </summary>
        public static int CacheTime = GetAppSettingValue("CacheTime").ToInt32Value();

        /// <summary>
        /// 程序集引用
        /// </summary>
        public static readonly string path = GetAppSettingValue("DAL");

        /// <summary>
        /// 獲取配置信息
        /// </summary>
        /// <param name="keyName">Webconfig appsetting 中的key</param>
        /// <returns>Value</returns>
        public static String GetAppSettingValue(String keyName)
        {
            try
            {
                return ConfigurationManager.AppSettings[keyName];
            }
            catch (Exception ex)
            {
                ex.AddLog("AppSettings", "GetAppSettingValue");
                return String.Empty;
            }
        }
    }
}
