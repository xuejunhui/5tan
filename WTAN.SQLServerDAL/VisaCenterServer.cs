using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;
using WTAN.CommonUtility;
using WTAN.IDAL;

namespace WTAN.SQLServerDAL
{
    public class VisaCenterServer : IVisaCenter
    {
        public Boolean UpdateVisaCenterTop(String id, int topstate)
        {
            String sql = "update VisaCenter set MainTop=@MainTop where autokey in ({0})";
            String ids = String.Empty;
            List<String> list = new List<String>() { 
                "MainTop",topstate.ToString()
            };
            for (int i = 0; i < id.Split(',').Count(); i++)
            {
                ids += (ids.IsNullOrEmpty() ? "" : ",") + String.Format("@id{0}", i);
                list.Add(String.Format("id{0}", i));
                list.Add(id.Split(',')[i]);
            }
            sql = String.Format(sql, ids);
            return sql.ExecuteNoneQuery(list.ToArray()) > 0;
        }

        public List<VisaCenterTB> GeVisaList(String id)
        {
            String sql = @"select * from VisaCenter 
                        where autokey in ({0})";//防注入写法
            String ids = String.Empty;
            List<String> list = new List<String>();
            for (int i = 0; i < id.Split(',').Count(); i++)
            {
                ids += (ids.IsNullOrEmpty() ? "" : ",") + String.Format("@id{0}", i);
                list.Add(String.Format("id{0}", i));
                list.Add(id.Split(',')[i]);
            }
            sql = String.Format(sql, ids);
            return sql.ExecuteRecords<VisaCenterTB>(list.ToArray());
        }

        public Boolean DelVisas(String id)
        {
            foreach (var item in GeVisaList(id))
            {
                SysFileServer f = new SysFileServer();
                f.DelFileInfo(item.GUID);
            }
            String sql = @"delete VisaCenter 
                        where autokey in ({0})";//防注入写法
            String ids = String.Empty;
            List<String> list = new List<String>();
            for (int i = 0; i < id.Split(',').Count(); i++)
            {
                ids += (ids.IsNullOrEmpty() ? "" : ",") + String.Format("@id{0}", i);
                list.Add(String.Format("id{0}", i));
                list.Add(id.Split(',')[i]);
            }
            sql = String.Format(sql, ids);
            return sql.ExecuteNoneQuery(list.ToArray()) > 0;
        }

        public Boolean UpdateVisaState(String id, int state)
        {
            String sql = "update VisaCenter set Enable=@Enable where autokey in ({0})";
            String ids = String.Empty;
            List<String> list = new List<String>() { 
                "Enable",state.ToString()
            };
            for (int i = 0; i < id.Split(',').Count(); i++)
            {
                ids += (ids.IsNullOrEmpty() ? "" : ",") + String.Format("@id{0}", i);
                list.Add(String.Format("id{0}", i));
                list.Add(id.Split(',')[i]);
            }
            sql = String.Format(sql, ids);
            return sql.ExecuteNoneQuery(list.ToArray()) > 0;
        }

        public int SaveVisa(VisaCenterTB visa)
        {
            String sql = String.Empty;
            List<String> list = new List<String>() { 
                "KeyWord",visa.KeyWord,
                "Description",visa.Description,
                "GUID", visa.GUID,
                "VName",visa.VName,
                "VContent",visa.VContent,
                "Price",visa.Price.ToString(),
                "Enable",visa.Enable?"1":"0",
                "AutoKey",visa.AutoKey.ToString(),
                "VType",visa.VType,
                "CategoryID",visa.CategoryID.ToString()
            };
            if (visa.AutoKey > 0)
            {
                sql = @"update VisaCenter set 
                        KeyWord=@KeyWord,
                        Description=@Description, 
                        GUID=@GUID, 
                        VName=@VName,
                        VContent=@VContent,
                        Price=@Price,
                        CategoryID=@CategoryID,
                        VType=@VType,
                        Enable=@Enable where autokey=@AutoKey";
                if (sql.ExecuteNoneQuery(list.ToArray()) > 0)
                    return visa.AutoKey;
            }
            else
            {
                sql = @"insert into VisaCenter(KeyWord, Description, GUID, VName, VContent, CreateTime, Enable, CategoryID, Price, VType, MainTop) 
                        values(@KeyWord, @Description, @GUID, @VName, @VContent, getdate(), @Enable, @CategoryID, @Price,@VType, 0) 
                        select isnull(SCOPE_IDENTITY(),0) as ID";
                return sql.ExecuteScalarInt(list.ToArray()).ToInt32Value();
            }
            return 0;
        }

        public VisaCenterTB GetVisa(String vname)
        {
            String sql = "select top 1 * from VisaCenter where vname=@vname";
            return sql.ExecuteOneRecord<VisaCenterTB>("vname", vname);
        }

        public VisaCenterTB GetVisa(int autokey)
        {
            String sql = "select * from VisaCenter where autokey=@autokey";
            return sql.ExecuteOneRecord<VisaCenterTB>("autokey", autokey);
        }

        public List<VisaCenterTB> GetVisas(String vtype, String keyword, int pageSize, out int rowCount, int pageIndex, String IsEnable, String Range, String TopState)
        {
            String orderby = "autokey desc";
            String where = " VType=@vtype";
            List<String> arr = new List<String>() { 
                "vtype",vtype
            };

            if (!keyword.IsNullOrEmpty())
            {
                where += " and (Description like @keyword or VName like @keyword)";
                arr.Add("keyword");
                arr.Add("%" + keyword + "%");
            }

            if (!IsEnable.IsNullOrEmpty())
            {
                where += " and Enable=@Enable";
                arr.Add("Enable");
                arr.Add(IsEnable);
            }
            if (!Range.IsNullOrEmpty())
            {
                where += " and CategoryName like @Range";
                arr.Add("Range");
                arr.Add(Range + "%");
            }
            if (!TopState.IsNullOrEmpty())
            {
                where += (where.IsNullOrEmpty() ? " " : " and ") + "MainTop=@MainTop";
                arr.Add("MainTop");
                arr.Add(TopState);
            }
            return CurrentDataServer.GetPagingData<VisaCenterTB>("VisaCenterView", "*", orderby, where, pageSize, out rowCount, pageIndex, arr.ToArray());
        }

        public List<VisaCenterTB> GetVisas(String vtype, int pageSize, out int rowCount, int pageIndex, int categoryid)
        {
            String orderby = "autokey desc";
            String where = " VType=@vtype and Enable=1" + (categoryid > 0 ? " and CategoryID=" + categoryid : "");
            List<String> arr = new List<String>() { 
                "vtype",vtype
            };

            return CurrentDataServer.GetPagingData<VisaCenterTB>("VisaCenterView", "*", orderby, where, pageSize, out rowCount, pageIndex, arr.ToArray());
        }

        public List<VisaCenterTB> GetTopVisas(String vtype, int recommendNum, int top, Boolean isAll)
        {
            String cachekey = String.Format("GetTopVisas{0}_{1}_{2}_{3}", vtype, recommendNum, top, isAll ? "true" : "false");
            List<VisaCenterTB> result = CacheHelper.ReadServerCache(cachekey) as List<VisaCenterTB>;
            String sort = "";
            if (result == null)
            {
                String sql = "select {0} * from VisaCenterView where Enable=1 and VType=@vtype";
                if(!isAll)
                {
                    sql += " and (MainTop=3 or MainTop=" + recommendNum + ")";
                }
                List<String> list = new List<String> { 
                    "vtype",vtype
                };
                if (3 > recommendNum && recommendNum > 0)
                {
                    sort += " case when MainTop=" + recommendNum + " then 0 when MainTop=3 then 1 else 2 end sort,";
                    sql = String.Format("select top {1} * from ( " + sql + ") tb order by sort asc,MainTop desc,autokey desc", sort, top);
                }
                else
                {
                    sql = String.Format(sql, " top " + top);
                }
                result = sql.ExecuteRecords<VisaCenterTB>(list.ToArray());
                CacheHelper.CreateServerCache(cachekey, result);
            }
            return result;
        }

        public List<VisaCenterTB> GetTopVisas(String vtype, int recommendNum, int top,int categoryid)
        {
            String cachekey = String.Format("GetTopVisas{0}_{1}_{2}_{3}", vtype, recommendNum, top, categoryid);
            List<VisaCenterTB> result = CacheHelper.ReadServerCache(cachekey) as List<VisaCenterTB>;
            if (result == null)
            {
                String sql = "select {1} * from VisaCenterView where Enable=1 and VType=@vtype {0} {2} order by MainTop desc,autokey desc";
                sql = String.Format(sql, recommendNum > 0 ? recommendNum < 3 ? " and (MainTop=3 or MainTop=" + recommendNum + ")" : "and MainTop=3" : "", top > 0 ? " top " + top : "", categoryid > 0 ? " and CategoryID=" + categoryid : "");
                result = sql.ExecuteRecords<VisaCenterTB>("vtype", vtype);
                CacheHelper.CreateServerCache(cachekey, result);
            }
            return result;
        }
    }
}
