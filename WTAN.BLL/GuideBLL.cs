using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.IDAL;
using WTAN.Model.DModel;
using WTAN.Model.VModel;
using WTAN.CommonUtility;

namespace WTAN.BLL
{
    public class GuideBLL
    {
        IGuide Guide = DALFactory.DataAccess.CreateGuide();

        public Boolean UpdateGuideTop(String id, int topstate)
        {
            return Guide.UpdateGuideTop(id, topstate);
        }

        public SearchModel<GuideTB> GetGuides(WebName webname, String keyword, int pagesize, int pageindex, int categoryid, String formarturl)
        {
            SearchModel<GuideTB> search = new SearchModel<GuideTB>()
            {
                Keyword = keyword,
                PageIndex = pageindex,
                CategoryID = categoryid
            };
            int rowcount = 0;
            search.DataList = Guide.GetGuides(webname, keyword, pagesize, out rowcount, pageindex, categoryid);
            search.RowCount = rowcount;
            search.Pagination = new PaginationHelper(pageindex, rowcount, pagesize, 8, formarturl);
            return search;
        }

        public SearchModel<GuideTB> GetGuides(WebName webname, String keyword, int pageindex, String range, int pagesize, int categoryid)
        {
            SearchModel<GuideTB> search = new SearchModel<GuideTB>()
            {
                Keyword = keyword,
                PageIndex = pageindex,
                Range = range
            };
            int rowcount = 0;
            String formarturl = URLUtility.GuideCenterUrlFormart(categoryid, range);

            search.DataList = Guide.GetGuides(webname,keyword, pagesize, out rowcount, pageindex, range);
            search.RowCount = rowcount;
            search.Pagination = new PaginationHelper(pageindex, rowcount, pagesize, 8, formarturl);
            return search;
        }

        public List<GuideTB> GetRelatedGuides(WebName webname, String categoryname, int autokey, int topnum = 10, int recommendNum = 0)
        {
            return Guide.GetRelatedGuides(webname, categoryname, autokey, topnum, recommendNum);
        }

        public List<GuideTB> GetGuides(String guid,int top=0)
        {
            return Guide.GetGuides(guid, top);
        }

        public Boolean DelGuides(String id)
        {
            return Guide.DelGuides(id);
        }

        public Boolean UpdateGuideState(String id, int state)
        {
            return Guide.UpdateGuideState(id, state);
        }

        public int SaveGuide(GuideTB tb)
        {
            int result = Guide.SaveGuide(tb);
            if (result > 0)
            {
                SysFileBLL bll = new SysFileBLL();
                bll.ChanageFileOfficial(tb.GUID);
            }
            return result;
        }

        public GuideTB GetGuide(int autokey)
        {
            return Guide.GetGuide(autokey);
        }

        public SearchModel<GuideTB> GetGuides(WebName webname,String keyword, int pagesize, int pageindex, String IsEnable, String guid, String Range, String TopState)
        {
            SearchModel<GuideTB> search = new SearchModel<GuideTB>()
            {
                Keyword = keyword,
                PageIndex = pageindex,
                PageSize = pagesize,
                IsEnable = IsEnable,
                Guid = guid,
                Range = Range,
                TopState = TopState,
                WebName=webname
            };
            int rowcount = 0;
            String formarturl = "/Conent/Guide?PageIndex={0}";
            if (!keyword.IsNullOrEmpty())
                formarturl += "&Keyword=" + keyword;
            if (!IsEnable.IsNullOrEmpty())
                formarturl += "&IsEnable=" + IsEnable;
            if (!guid.IsNullOrEmpty())
                formarturl += "&Guid=" + guid;
            if (!Range.IsNullOrEmpty())
                formarturl += "&Range=" + Range;
            if(!TopState.IsNullOrEmpty())
                formarturl += "&TopState=" + TopState;
            formarturl += "&webname=" + webname.ToString();

            search.DataList = Guide.GetGuides(webname,keyword, pagesize, out rowcount, pageindex, IsEnable, guid, Range, TopState);
            search.RowCount = rowcount;
            search.Pagination = new PaginationHelper(pageindex, rowcount, pagesize, 8, formarturl);
            return search;
        }
    }
}
