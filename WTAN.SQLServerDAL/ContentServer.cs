using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.IDAL;
using WTAN.Model.DModel;
using WTAN.CommonUtility;

namespace WTAN.SQLServerDAL
{
    public class ContentServer : IContent
    {
        public Boolean UpdateContentTop(String id, int topstate)
        {
            String sql = "update Content set MainTop=@MainTop where autokey in ({0})";
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

        public Boolean UpdateContentState(String id, int state)
        {
            String sql = "update Content set Enable=@Enable where autokey in ({0})";
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

        public List<ContentTB> GetContentList(String id)
        {
            String sql = @"select * from Content 
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
            return sql.ExecuteRecords<ContentTB>(list.ToArray());
        }

        public Boolean DelContentInfo(String id)
        {
            foreach (var item in GetContentList(id))
            {
                SysFileServer f = new SysFileServer();
                f.DelFileInfo(item.GUID);
                item.PreviewIMG.DelFile();
            }
            String sql = @"delete Content 
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

        public int SaveContentInfo(ContentTB tb)
        {
            String sql = String.Empty;
            List<String> list = new List<string>() { 
                "Title", tb.Title,
                "Shorttitle", tb.Shorttitle,
                "KeyWord",tb.KeyWord.ToEmptyTrimString(),
                "Description",tb.Description.ToEmptyTrimString(),
                "SEOURL",tb.SEOURL.ToEmptyTrimString(),
                "TravelingDays",tb.TravelingDays.ToEmptyTrimString(),
                "Transport",tb.Transport.ToEmptyTrimString(),
                "OfferedType",tb.OfferedType.ToEmptyTrimString(),
                "CategoryID",tb.CategoryID.ToString(),
                "MinSignUp",tb.MinSignUp.ToString(),
                "Features",tb.Features.ToEmptyTrimString(),
                "Price",tb.Price.ToString(),
                "PreviewIMG",tb.PreviewIMG.ToEmptyTrimString(),
                "XMLContent",tb.XMLContent,
                "GUID",tb.GUID.ToEmptyTrimString(),
                "Enable",tb.Enable?"1":"0",
                "autokey",tb.AutoKey.ToString(),
                "webname",tb.WebName.ToString()
            };
            if (tb.AutoKey == 0)
            {
                sql = @"insert into Content(Title, Shorttitle, KeyWord, Description, SEOURL, TravelingDays, Transport, OfferedType, CategoryID, MinSignUp, Features, Price, PreviewIMG, XMLContent, GUID, CreateTime, Enable,webname)
                        values(@Title, @Shorttitle, @KeyWord, @Description, @SEOURL, @TravelingDays, @Transport, @OfferedType, @CategoryID, @MinSignUp, @Features, @Price, @PreviewIMG, @XMLContent, @GUID, getdate(), @Enable,@webname)
                        select isnull(SCOPE_IDENTITY(),0) as ID";
                return sql.ExecuteScalarInt(list.ToArray()).ToInt32Value();
            }
            else
            {
                sql = @"Update Content set Title=@Title,
                        Shorttitle=@Shorttitle,
                        KeyWord=@KeyWord,
                        Description=@Description, 
                        SEOURL=@SEOURL,
                        TravelingDays=@TravelingDays,
                        Transport=@Transport,
                        OfferedType=@OfferedType, 
                        CategoryID=@CategoryID, 
                        MinSignUp=@MinSignUp, 
                        Features=@Features,
                        Price=@Price, 
                        PreviewIMG=@PreviewIMG,
                        XMLContent=@XMLContent, 
                        GUID=@GUID, 
                        Enable=@Enable,
                        webname=@webname
                        where autokey=@autokey";
                return sql.ExecuteNoneQuery(list.ToArray()) > 0 ? tb.AutoKey : 0;
            }
        }

        /// <summary>
        /// 根据某一字段 获取一条数据
        /// </summary>
        /// <param name="colname"></param>
        /// <param name="colvalue"></param>
        /// <returns></returns>
        public ContentTB GetContentInfo(String colname,String colvalue)
        {
            String sql = String.Format("select * from ContentView where {0}=@{0}", colname);
            return sql.ExecuteOneRecord<ContentTB>(colname, colvalue);
        }

        /// <summary>
        /// 获取旅游列表 分页数据
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public List<ContentTB> GetContents(WebName webname , String keyword, int pageSize, out int rowCount, int pageIndex, String IsEnable, String Range, String TopState)
        {
            String orderby = "autokey desc";
            String where = "webname=@webname";
            List<String> arr = new List<String>() { 
                "webname",webname.ToString()
            };

            if (!keyword.IsNullOrEmpty())
            {
                where += " (title like @keyword or Description like @keyword)";
                arr.Add("keyword");
                arr.Add("%" + keyword + "%");
            }

            if (!IsEnable.IsNullOrEmpty())
            {
                where += (where.IsNullOrEmpty() ? " " : " and ") + "Enable=@Enable";
                arr.Add("Enable");
                arr.Add(IsEnable);
            }
            if (!Range.IsNullOrEmpty())
            {
                where += (where.IsNullOrEmpty() ? " " : " and ") + "CategoryName like @Range";
                arr.Add("Range");
                arr.Add(Range + "%");
            }
            if (!TopState.IsNullOrEmpty())
            {
                where += (where.IsNullOrEmpty() ? " " : " and ") + "MainTop=@MainTop";
                arr.Add("MainTop");
                arr.Add(TopState);
            }

            return CurrentDataServer.GetPagingData<ContentTB>("ContentView", "*", orderby, where, pageSize, out rowCount, pageIndex, arr.ToArray());
        }

        public List<ContentTB> GetContents(WebName webname, int pageSize, out int rowCount, int pageIndex, String Range, String Days, String Traffic)
        {
            String orderby = "autokey desc";
            String where = "Enable=1 and webname=@webname";
            List<String> arr = new List<String>() { 
                "webname",webname.ToString()
            };
            if (!Range.IsNullOrEmpty())
            {
                where += " and '>'+CategoryName+'>' like @Range";
                arr.Add("Range");
                arr.Add("%>" + Range + ">%");
            }
            if (!Days.IsNullOrEmpty())
            {
                where += " and TravelingDays=@Days";
                arr.Add("Days");
                arr.Add(Days);
            }
            if (!Traffic.IsNullOrEmpty())
            {
                where += " and Transport=@Traffic";
                arr.Add("Traffic");
                arr.Add(Traffic);
            }

            return CurrentDataServer.GetPagingData<ContentTB>("ContentView", "*", orderby, where, pageSize, out rowCount, pageIndex, arr.ToArray());
        }

        public List<ContentTB> GetOfferedTypeTopContent(String offeredType, int topnum, int recommendNum, Boolean isAll)
        {
            String cachekey = String.Format("GetOfferedTypeTopContent_{0}_{1}_{2}_{3}", offeredType, topnum, recommendNum, isAll);
            List<ContentTB> result = CacheHelper.ReadServerCache(cachekey) as List<ContentTB>;
            String sort = "";
            if (result == null)
            {
                String sql = "select {0} * from contentview where Enable=1 and OfferedType=@OfferedType ";
                if (!isAll)
                {
                    sql += " and (MainTop=3 or MainTop=" + recommendNum + ")";
                }
                List<String> list = new List<String> { 
                    "OfferedType",offeredType
                };
                if (3 > recommendNum && recommendNum > 0)
                {
                    sort += " case when MainTop=" + recommendNum + " then 0 when MainTop=3 then 1 else 2 end sort,";
                    sql = String.Format("select top {1} * from ( " + sql + ") tb order by sort asc,MainTop desc,autokey desc", sort, topnum);
                }
                else
                {
                    sql = String.Format(sql, " top " + topnum);
                }
                result = sql.ExecuteRecords<ContentTB>(list.ToArray());
                CacheHelper.CreateServerCache(cachekey, result);
            }
            return result;
        }

        public List<ContentTB> GetRelatedContent(WebName webname, String categoryname, int autokey, int topnum, int recommendNum, Boolean isAll)
        {
            String cachekey = String.Format("GetRelatedContent{0}_{1}_{2}_{3}_{4}", categoryname, autokey, topnum, recommendNum, isAll);
            List<ContentTB> result = CacheHelper.ReadServerCache(cachekey) as List<ContentTB>;
            if (result == null)
            {
                String sql = "select top {1} * from (select {0} as sort,* from contentview where Enable=1 and webname=@webname and autokey<>@autokey {2} {3}) tb order by sort desc,MainTop desc";
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
                list.Add(categoryname + ">%");

                for (int i = index - 1; i >0; i--)
                {
                    categoryname = categoryname.RemoveEndsWith(categorys[i]).RemoveEndsWith(">");
                    sort += String.Format(" when CategoryName like @category{0} then {0}", i);
                    list.Add(String.Format("category{0}", i));
                    list.Add(categoryname + ">%");
                }
                sort += " else 0 end";
                sql = String.Format(sql, sort, topnum,
                    recommendNum > 0 ? recommendNum < 3 ? " and (MainTop=3 or MainTop=" + recommendNum + ")" : "and MainTop=3" : "",
                    isAll ? "" : " and CategoryName like @category" + index);
                result = sql.ExecuteRecords<ContentTB>(list.ToArray());
                CacheHelper.CreateServerCache(cachekey, result);
            }
            return result;

        }

        public List<RecordItem> GetSearchAllContent(WebName webname,String keyword, int pageindex,int pagesize, out int rowcount, String ctype,String urlformart)
        {
            //通过视图中sort 10分 范围内的权重进行 匹配度排序
            int index = 6;
            String orderby = "dbo.GetImportanceNumber(title,[description],KeyWord,CategoryName,@keyword)*sort/5";
            String where = "Enable=1 and webname=@webname";
            List<String> arr = new List<String>()
            { 
                "keyword", keyword,
                "webname", webname.ToString()
            };
            String[] keywrds = keyword.SplitKeyWord();

            if (keywrds.Length > 1)
            {
                where += " and (";
                foreach (String k in keywrds)
                {
                    orderby += String.Format(" + dbo.GetImportanceNumber(title,[description],KeyWord,CategoryName,@keyword{0})*sort/{0}", index);
                    where += String.Format("{1} title like @linkkeyowrd{0} or [description] like @linkkeyowrd{0} or KeyWord like @linkkeyowrd{0} or CategoryName like @linkkeyowrd", index, index == 6 ? "" : " or ");
                    arr.Add("keyword" + index);
                    arr.Add(k);
                    arr.Add("linkkeyowrd" + index);
                    arr.Add("%" + k + "%");
                    index++;
                }
                where += ") ";
            }
            else {
                where += " and (title like @linkkeyowrd or [description] like @linkkeyowrd or KeyWord like @linkkeyowrd or CategoryName like @linkkeyowrd)";
                arr.Add("linkkeyowrd");
                arr.Add("%" + keyword + "%");
            }
            if (!ctype.IsNullOrEmpty() && !ctype.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                where += " and ctype=@ctype";
                arr.Add("ctype");
                arr.Add(ctype);
            }
            orderby += " desc, sort desc";

            return CurrentDataServer.GetPagingData<RecordItem>("AllContentView", "*", orderby, where, pagesize, out rowcount, pageindex, arr.ToArray());

        }

        public List<ContentTB> GetContents(WebName webname, int recommendNum, int maxtop)
        {
            String cachekey = String.Format("GetContents{0}_{1}_{2}", webname.ToString(), recommendNum, maxtop);
            List<ContentTB> result = CacheHelper.ReadServerCache(cachekey) as List<ContentTB>;
            if (result == null)
            {
                String sql = String.Format("select top {0} * from contentview where Enable=1 and MainTop={1} and webname='{2}'", maxtop, recommendNum, webname.ToString());
                result = sql.ExecuteRecords<ContentTB>();
                CacheHelper.CreateServerCache(cachekey, result);
            }
            return result;
        }
    }
}
