using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;
using WTAN.CommonUtility;
using WTAN.IDAL;

namespace WTAN.SQLServerDAL
{
    public class GuideServer : IGuide
    {

        public Boolean UpdateGuideTop(String id, int topstate)
        {
            String sql = "update Guide set MainTop=@MainTop where autokey in ({0})";
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

        public List<GuideTB> GeGuideList(String id)
        {
            String sql = @"select * from guide 
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
            return sql.ExecuteRecords<GuideTB>(list.ToArray());
        }

        public Boolean DelGuides(String id)
        {
            foreach (var item in GeGuideList(id))
            {
                SysFileServer f = new SysFileServer();
                f.DelFileInfo(item.GUID);
            }
            String sql = @"delete guide 
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



        public Boolean UpdateGuideState(String id, int state)
        {
            String sql = "update guide set Enable=@Enable where autokey in ({0})";
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

        public int SaveGuide(GuideTB guide)
        {
            String sql = String.Empty;
            List<String> list = new List<String>() { 
                "KeyWord",guide.KeyWord,
                "Description",guide.Description,
                "GUID", guide.GUID,
                "RelatedGUID",guide.RelatedGUID,
                "GuideName",guide.GuideName,
                "GuideContent",guide.GuideContent,
                "Enable",guide.Enable?"1":"0",
                "AutoKey",guide.AutoKey.ToString(),
                "CategoryID",guide.CategoryID.ToString(),
                "webname",guide.WebName.ToString()
            };
            if (guide.AutoKey > 0)
            {
                sql = @"update Guide set 
                        KeyWord=@KeyWord,
                        Description=@Description, 
                        RelatedGUID=@RelatedGUID, 
                        GuideName=@GuideName,
                        GuideContent=@GuideContent,
                        CategoryID=@CategoryID,
                        webname=@webname,
                        Enable=@Enable where autokey=@AutoKey";
                if (sql.ExecuteNoneQuery(list.ToArray()) > 0)
                    return guide.AutoKey;
            }
            else
            {
                sql = @"insert into Guide(KeyWord, Description, GUID, RelatedGUID, GuideName, GuideContent, CreateTime, Enable, CategoryID,webname) 
                        values(@KeyWord, @Description, @GUID, @RelatedGUID, @GuideName, @GuideContent, getdate(), @Enable, @CategoryID,@webname) 
                        select isnull(SCOPE_IDENTITY(),0) as ID";
                return sql.ExecuteScalarInt(list.ToArray()).ToInt32Value();
            }
            return 0;
        }

        public GuideTB GetGuide(int autokey)
        {
            String sql = "select * from GuideView where autokey=@autokey";
            return sql.ExecuteOneRecord<GuideTB>("autokey", autokey);
        }

        public List<GuideTB> GetGuides(WebName webname, String keyword, int pageSize, out int rowCount, int pageIndex, String IsEnable, String guid, String Range, String TopState)
        {
            String orderby = "g.autokey desc";
            String where = "g.webname=@webname";
            List<String> arr = new List<String>() { 
                "webname",webname.ToString()
            };

            if (!keyword.IsNullOrEmpty())
            {
                where += " (c.title like @keyword or g.Description like @keyword or g.GuideName like @keyword)";
                arr.Add("keyword");
                arr.Add("%" + keyword + "%");
            }

            if (!IsEnable.IsNullOrEmpty())
            {
                where += (where.IsNullOrEmpty() ? " " : " and ") + "g.Enable=@Enable";
                arr.Add("Enable");
                arr.Add(IsEnable);
            }
            if (!guid.IsNullOrEmpty())
            {
                where += (where.IsNullOrEmpty() ? " " : " and ") + "g.RelatedGUID=@RelatedGUID";
                arr.Add("RelatedGUID");
                arr.Add(guid);
            }
            if (!Range.IsNullOrEmpty())
            {
                where += (where.IsNullOrEmpty() ? " " : " and ") + "CategoryName like @Range";
                arr.Add("Range");
                arr.Add(Range + "%");
            }
            if (!TopState.IsNullOrEmpty())
            {
                where += (where.IsNullOrEmpty() ? " " : " and ") + "g.MainTop=@MainTop";
                arr.Add("MainTop");
                arr.Add(TopState);
            }
            return CurrentDataServer.GetPagingData<GuideTB>("GuideView g join Content c on g.RelatedGUID=c.GUID", "g.* ,c.Title,c.Shorttitle", orderby, where, pageSize, out rowCount, pageIndex, arr.ToArray());
        }

        public List<GuideTB> GetGuides(WebName webname, String keyword, int pageSize, out int rowCount, int pageIndex, int categoryid)
        {
            String orderby = "g.autokey desc";
            String where = " g.Enable=1 and g.webname=@webname";
            List<String> arr = new List<String>() { 
                "webname",webname.ToString()
            };

            if (!keyword.IsNullOrEmpty())
            {
                where += " and (c.title like @keyword or g.Description like @keyword or g.GuideName like @keyword)";
                arr.Add("keyword");
                arr.Add("%" + keyword + "%");
            }

            if (categoryid>0)
            {
                where += " and g.CategoryID=@CategoryID";
                arr.Add("CategoryID");
                arr.Add(categoryid.ToString());
            }

            return CurrentDataServer.GetPagingData<GuideTB>("GuideView g join Content c on g.RelatedGUID=c.GUID", "g.* ,c.Title,c.Shorttitle,c.seourl,c.Autokey CAutokey", orderby, where, pageSize, out rowCount, pageIndex, arr.ToArray());
        }

        public List<GuideTB> GetGuides(WebName webname, String keyword, int pageSize, out int rowCount, int pageIndex, String Range)
        {
            String orderby = "g.autokey desc";
            String where = " g.Enable=1 and g.webname=@webname";
            List<String> arr = new List<String>() { 
                "webname",webname.ToString()
            };

            if (!keyword.IsNullOrEmpty())
            {
                where += " and (c.title like @keyword or g.Description like @keyword or g.GuideName like @keyword)";
                arr.Add("keyword");
                arr.Add("%" + keyword + "%");
            }

            if (!Range.IsNullOrEmpty())
            {
                where += " and '>' + CategoryName + '>' like @Range";
                arr.Add("Range");
                arr.Add("%>" + Range + ">%");
            }

            return CurrentDataServer.GetPagingData<GuideTB>("GuideView g join Content c on g.RelatedGUID=c.GUID", "g.* ,c.Title,c.Shorttitle,c.seourl,c.Autokey CAutokey", orderby, where, pageSize, out rowCount, pageIndex, arr.ToArray());
        }


        public List<GuideTB> GetRelatedGuides(WebName webname, String categoryname, int autokey, int topnum, int recommendNum)
        {
            String cachekey = String.Format("GetRelatedGuides{0}_{1}_{2}_{3}", categoryname, autokey, topnum, recommendNum);
            List<GuideTB> result = CacheHelper.ReadServerCache(cachekey) as List<GuideTB>;
            if (result == null)
            {
                String sql = "select top {1} * from (select {0} as sort,* from GuideView where Enable=1 and webname=@webname and autokey<>@autokey {2}) tb order by sort desc,MainTop desc";
                String sort = "case ";
                List<String> list = new List<String>() { 
                    "autokey",
                    autokey.ToString(),
                    "webname",webname.ToString()
                };
                String[] categorys = categoryname.Split('>');
                int index = categorys.Length;
                sort += String.Format(" when CategoryName=@category{0} then {0}", index + 1);
                list.Add(String.Format("category{0}", index + 1));
                list.Add(categoryname);

                sort += String.Format(" when CategoryName like @category{0} then {0}", index);
                list.Add(String.Format("category{0}", index));
                list.Add(categoryname + "%");

                for (int i = index - 1; i > 0; i--)
                {
                    categoryname = categoryname.RemoveEndsWith(categorys[i]).RemoveEndsWith(">");
                    sort += String.Format(" when CategoryName like @category{0} then {0}", i);
                    list.Add(String.Format("category{0}", i));
                    list.Add(categoryname.RemoveEndsWith(categoryname) + "%");
                }
                sort += " else 0 end";
                sql = String.Format(sql, sort, topnum, recommendNum > 0 ? recommendNum < 3 ? " and (MainTop=3 or MainTop=" + recommendNum + ")" : "and MainTop=3" : "");
                result = sql.ExecuteRecords<GuideTB>(list.ToArray());
                CacheHelper.CreateServerCache(cachekey, result);
            }
            return result;

        }

        public List<GuideTB> GetGuides(String guid,int top)
        {
            String cachekey = "GetGuides" + guid + "top" + top;
            List<GuideTB> list = CacheHelper.ReadServerCache(cachekey) as List<GuideTB>;
            if (list == null)
            {
                String sql = String.Format("select {0} * from Guide where RelatedGUID=@guid order by autokey desc", top > 0 ? " top " + top : "");
                list = sql.ExecuteRecords<GuideTB>("guid", guid);
                CacheHelper.CreateServerCache(cachekey, list);
            }
            return list;
        }
    }
}
