using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.IDAL;
using WTAN.Model.DModel;
using WTAN.CommonUtility;

namespace WTAN.SQLServerDAL
{
    public class SpecialColumnServer : ISpecialColumn
    {
        public List<SpecialColumnTB> GeSpecialColumnList(String id)
        {
            String sql = @"select * from SpecialColumn 
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
            return sql.ExecuteRecords<SpecialColumnTB>(list.ToArray());
        }

        public Boolean DelSpecialColumns(String id)
        {
            foreach (var item in GeSpecialColumnList(id))
            {
                SysFileServer f = new SysFileServer();
                f.DelFileInfo(item.GUID.ToEmptyTrimString());
            }
            String sql = @"delete SpecialColumn 
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

        public Boolean UpdateSpecialColumnState(String id, int state)
        {
            String sql = "update SpecialColumn set Enable=@Enable where autokey in ({0})";
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

        public int SaveSpecialColumn(SpecialColumnTB guide)
        {
            String sql = String.Empty;
            List<String> list = new List<String>() { 
                "KeyWord",guide.KeyWord,
                "Description",guide.Description,
                "GUID", guide.GUID,
                "SC_Name",guide.SC_Name,
                "SEOURL",guide.SEOURL,
                "SC_Content",guide.SC_Content,
                "Enable",guide.Enable?"1":"0",
                "AutoKey",guide.AutoKey.ToString(),
                "ParentID",guide.ParentID.ToString(),
                "ViewModel",guide.ViewModel.ToString()
            };
            if (guide.AutoKey > 0)
            {
                sql = @"update SpecialColumn set 
                        KeyWord=@KeyWord,
                        Description=@Description, 
                        SC_Name=@SC_Name, 
                        SC_Content=@SC_Content,
                        SEOURL=@SEOURL,
                        ParentID=@ParentID,
                        Enable=@Enable where autokey=@AutoKey";
                if (sql.ExecuteNoneQuery(list.ToArray()) > 0)
                    return guide.AutoKey;
            }
            else
            {
                sql = @"insert into SpecialColumn(KeyWord, Description, GUID, SC_Name, SC_Content, SEOURL, CreateTime, Enable, ParentID, ViewModel) 
                        values(@KeyWord, @Description, @GUID, @SC_Name, @SC_Content, @SEOURL, getdate(), @Enable, @ParentID, @ViewModel) 
                        select isnull(SCOPE_IDENTITY(),0) as ID";
                return sql.ExecuteScalarInt(list.ToArray()).ToInt32Value();
            }
            return 0;
        }

        public SpecialColumnTB GetSpecialColumn(int autokey)
        {
            String sql = "select * from SpecialColumn where autokey=@autokey";
            return sql.ExecuteOneRecord<SpecialColumnTB>("autokey", autokey);
        }

        public SpecialColumnTB GetSpecialColumn(String seourl)
        {
            String sql = "select * from SpecialColumn where SEOURL=@SEOURL";
            return sql.ExecuteOneRecord<SpecialColumnTB>("SEOURL", seourl.ToEmptyTrimString());
        }

        public List<SpecialColumnTB> GetSpecialColumns(SpecialColumnType type, String keyword, int pageSize, out int rowCount, int pageIndex, String IsEnable)
        {
            String orderby = "autokey desc";
            String where = "parentid=@parentid";
            List<String> arr = new List<String>() { 
                "parentid",((int)type).ToString()
            };

            if (!keyword.IsNullOrEmpty())
            {
                where += " and (SC_Name like @keyword or Description like @keyword)";
                arr.Add("keyword");
                arr.Add("%" + keyword + "%");
            }

            if (!IsEnable.IsNullOrEmpty())
            {
                where += " and Enable=@Enable";
                arr.Add("Enable");
                arr.Add(IsEnable);
            }

            return CurrentDataServer.GetPagingData<SpecialColumnTB>("SpecialColumn", "*", orderby, where, pageSize, out rowCount, pageIndex, arr.ToArray());
        }

        public List<SpecialColumnTB> GetSpecialColumns(SpecialColumnType type, int topnum)
        {
            String cachekey = String.Format("GetSpecialColumns{0}_{1}", type, topnum);
            List<SpecialColumnTB> result = CacheHelper.ReadServerCache(cachekey) as List<SpecialColumnTB>;
            if (result == null)
            {
                String sql = String.Format("select {0} * from SpecialColumn where Enable=1 and parentid=@parentid order by autokey desc", topnum > 0 ? " top " + topnum : "");
                result = sql.ExecuteRecords<SpecialColumnTB>("parentid", ((int)type).ToString());
                CacheHelper.CreateServerCache(cachekey, result);
            }
            return result;
        }
    }
}
