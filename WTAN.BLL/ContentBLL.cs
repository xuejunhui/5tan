using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.IDAL;
using WTAN.Model.VModel;
using WTAN.Model.DModel;
using WTAN.CommonUtility;

namespace WTAN.BLL
{
    public class ContentBLL
    {
        IContent Content = DALFactory.DataAccess.CreateContent();

        public List<ContentTB> GetContents(WebName webname, int recommendNum, int maxtop)
        {
            return Content.GetContents(webname, recommendNum, maxtop);
        }

        public List<ContentTB> GetOfferedTypeTopContent(String offeredType, int topnum, int recommendNum, Boolean isAll)
        {
            return Content.GetOfferedTypeTopContent(offeredType, topnum, recommendNum, isAll);
        }

        public Boolean UpdateContentTop(String id, int topstate)
        {
            return Content.UpdateContentTop(id, topstate);
        }

        public Boolean UpdateContentState(String id, int state)
        {
            return Content.UpdateContentState(id, state);
        }

        public Boolean DelContentInfo(String id)
        {
            return Content.DelContentInfo(id);
        }

        public int SaveContentInfo(ContentTB tb)
        {
            int result = Content.SaveContentInfo(tb);
            if (result > 0)
            {
                SysFileBLL bll = new SysFileBLL();
                bll.ChanageFileOfficial(tb.GUID);
            }
            return result;
        }

        /// <summary>
        /// 检测Seo路径是否已经在表中存在
        /// </summary>
        /// <param name="seourl"></param>
        /// <param name="autokey"></param>
        /// <returns></returns>
        public Boolean IsExistsContentSEOURL(String seourl, int autokey)
        {
            ContentTB c = GetContentInfo("SEOURL", seourl);
            return c != null && c.AutoKey != autokey;
        }

        public ContentTB GetContentInfo(int autokey)
        {
            return GetContentInfo("autokey", autokey.ToEmptyTrimString());
        }

        public ContentTB GetContentInfo(String guid)
        {
            return GetContentInfo("GUID", guid.ToEmptyTrimString());
        }

        public ContentTB GetContentInfo(String colname, String colvalue)
        {
            return Content.GetContentInfo(colname, colvalue);
        }

        /// <summary>
        /// 获得旅游管理列表数据
        /// </summary>
        /// <param name="pageindex">当前页</param>
        /// <param name="keyword">搜索关键词</param>
        /// <param name="pagesize">每页显示数据条数</param>
        /// <returns></returns>
        public SearchModel<ContentTB> GetSummerManageModel(WebName webname,int pageindex, String keyword, int pagesize, String IsEnable, String Range, String TopState)
        {
            SearchModel<ContentTB> search = new SearchModel<ContentTB>()
            {
                Keyword = keyword,
                PageIndex = pageindex,
                PageSize = pagesize,
                IsEnable = IsEnable,
                Range = Range,
                TopState = TopState,
                WebName = webname
            };
            int rowcount = 0;
            String formarturl = "/Content/SummerManage?PageIndex={0}";
            if (!keyword.IsNullOrEmpty())
                formarturl += "&Keyword=" + keyword;
            if (!IsEnable.IsNullOrEmpty())
                formarturl += "&IsEnable=" + IsEnable;
            if (!Range.IsNullOrEmpty())
                formarturl += "&Range=" + Range;
            if (!TopState.IsNullOrEmpty())
                formarturl += "&TopState=" + TopState;
            formarturl += "webname=" + webname.ToString();

            search.DataList = Content.GetContents(webname, keyword, pagesize, out rowcount, pageindex, IsEnable, Range, TopState);
            search.RowCount = rowcount;
            search.Pagination = new PaginationHelper(pageindex, rowcount, pagesize, 8, formarturl);
            return search;
        }


        public SearchModel<RecordItem> GetAllContentSearchModel(WebName webname, int pageindex, String keyword, int pagesize, String ctype)
        {
            SearchModel<RecordItem> search = new SearchModel<RecordItem>()
            {
                Keyword = keyword,
                Range = ctype
            };
            int rowcount = 0;
            search.DataList = Content.GetSearchAllContent(webname, keyword, pageindex, pagesize, out rowcount, ctype, URLUtility.SearchUrlFormart(ctype, keyword));
            search.RowCount = rowcount;
            search.Pagination = new PaginationHelper(pageindex, rowcount, pagesize, 8, URLUtility.SearchUrlFormart(ctype, keyword));
            return search;
        }

        public SearchModel<ContentTB> GetRegionSearchModel(WebName webname,String days, String traffic, String Range,int PageIndex,int PageSize,String Formarturl)
        {
            SearchModel<ContentTB> search = new SearchModel<ContentTB>()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            int rowcount = 0;
            search.DataList = Content.GetContents(webname,PageSize, out rowcount, PageIndex, Range, days, traffic);
            search.RowCount = rowcount;
            search.Pagination = new PaginationHelper(PageIndex, rowcount, PageSize, 8, Formarturl);
            return search;
        }

        /// <summary>
        /// 获取相关列表
        /// </summary>
        /// <param name="categoryname"></param>
        /// <param name="autokey"></param>
        /// <param name="topnum"></param>
        /// <param name="recommendNum">置頂 3全局置頂 2列表置頂 1首頁置頂 0不置頂</param>
        /// <returns></returns>
        public List<ContentTB> GetRelatedContent(WebName webname,String categoryname, int autokey, int topnum = 10, int recommendNum = 0, Boolean isAll = true)
        {
            return Content.GetRelatedContent(webname,categoryname, autokey, topnum, recommendNum, isAll);
        }
    }
}
