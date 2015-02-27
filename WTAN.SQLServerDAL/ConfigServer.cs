using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.IDAL;
using WTAN.Model.DModel;
using WTAN.Model.VModel;
using WTAN.CommonUtility;

namespace WTAN.SQLServerDAL
{
    public class ConfigServer : IConfig
    {
        public List<Sys_ConfigTB> GetConfigInfoList(String configType,WebName webName)
        {
            String cachekey = "GetConfigInfoList" + configType;
            List<Sys_ConfigTB> result = CacheHelper.ReadServerCache(cachekey) as List<Sys_ConfigTB>;
            if (result == null)
            {
                String sql = "select * from Sys_Config where Sys_Type=@Sys_Type and Enable=1 and webName=@webName";
                result = sql.ExecuteRecords<Sys_ConfigTB>("Sys_Type", configType, "webName", webName.ToString());
                CacheHelper.CreateServerCache(cachekey, result);
            }
            return result;
        }

        public Sys_ConfigTB GetConfigInfo(int autokey, String Sys_Type = "")
        {
            String cachekey = "GetConfigInfo" + autokey;
            Sys_ConfigTB result = CacheHelper.ReadServerCache(cachekey) as Sys_ConfigTB;
            if (result == null)
            {
                String sql = "select top 1 * from Sys_Config where autokey=@autokey and Enable=1";
                result = sql.ExecuteOneRecord<Sys_ConfigTB>("autokey", autokey);
                if (result == null && Sys_Type != "")
                    result = new Sys_ConfigTB() { Sys_Type = Sys_Type };
                CacheHelper.CreateServerCache(cachekey, result);
            }
            return result;
        }

        public Sys_ConfigTB GetConfigInfo(String configType,WebName webName)
        {
            String cachekey = "GetConfigInfo" + configType;
            Sys_ConfigTB result = CacheHelper.ReadServerCache(cachekey) as Sys_ConfigTB;
            if (result == null)
            {
                String sql = "select top 1 * from Sys_Config where Sys_Type=@Sys_Type and Enable=1 and webname=@webname";
                result = sql.ExecuteOneRecord<Sys_ConfigTB>("Sys_Type", configType, "webname", webName.ToString());
                if (result == null)
                    result = new Sys_ConfigTB() { Sys_Type = configType };
                CacheHelper.CreateServerCache(cachekey, result);
            }
            return result;
        }

        public Boolean SaveConfigInfo(Sys_ConfigTB config)
        {
            String sql = String.Empty;
            List<String> p = new List<String>() { 
                "Sys_Type",config.Sys_Type,
                "AutoKey",config.AutoKey.ToString(),
                "Sys_Value",config.Sys_Value,
                "webname",config.WebName.ToString()
            };

            if (config.AutoKey > 0)
                sql = "Update Sys_Config set Sys_Value=@Sys_Value where AutoKey=@AutoKey";
            else
                sql = "insert into Sys_Config(Sys_Type,Sys_Value,Enable,WebName) values(@Sys_Type,@Sys_Value,1,@webname)";
            if (sql.ExecuteNoneQuery(p.ToArray()) > 0)
            {
                CacheHelper.ClearServerCache("GetConfigInfo" + config.Sys_Type);
                return true;
            }
            return false;
        }

        public Boolean DelConfigInfo(String id)
        {
            try
            {
                String qsql = "select * from Sys_Config where autokey in ({0})";
                String sql = "delete Sys_Config where autokey in ({0})";
                String ids = String.Empty;
                List<String> list = new List<String>();
                for (int i = 0; i < id.Split(',').Count(); i++)
                {
                    ids += (ids.IsNullOrEmpty() ? "" : ",") + String.Format("@id{0}", i);
                    list.Add(String.Format("id{0}", i));
                    list.Add(id.Split(',')[i]);
                }
                sql = String.Format(sql, ids);
                qsql = String.Format(qsql, ids);
                foreach (var item in qsql.ExecuteRecords<Sys_ConfigTB>(list.ToArray()))
                {
                    AdsConfigXml ads = item.Sys_Value.XmlDeserialize<AdsConfigXml>(System.Text.Encoding.UTF8);
                    if (ads.AdsType == AdsType.Image || ads.AdsType == AdsType.Slide)
                        ads.LinkContent.DelFile();//刪除圖片文件
                }
                return CurrentDataServer.ExecuteNoneQuery(sql, list.ToArray()) > 0;
            }
            catch (Exception ex)
            {
                ex.AddLog("AccountServer", "DelConfigInfo");
                return false;
            }
        }
    }
}
