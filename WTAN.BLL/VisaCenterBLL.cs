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
    public class VisaCenterBLL
    {
        IVisaCenter VisaCenter = DALFactory.DataAccess.CreateVisaCenter();


        public SearchModel<VisaCenterTB> GetTickets(int pageIndex, int categoryid)
        {
            SearchModel<VisaCenterTB> search = new SearchModel<VisaCenterTB>()
            {
                PageIndex = pageIndex,
                PageSize = 10,
                VType = "Ticket"
            };
            int rowcount = 0;
            categoryid = categoryid == (int)DropDownState.TicketCategory ? 0 : categoryid;
            search.DataList = VisaCenter.GetVisas(search.VType, search.PageSize, out rowcount, pageIndex, categoryid);
            search.RowCount = rowcount;
            search.Pagination = new PaginationHelper(pageIndex, rowcount, search.PageSize, 8, URLUtility.TicketCenterUrlFormart(categoryid));
            return search;
        }

        public List<VisaCenterTB> GetTopVisas(String vtype, int recommendNum, int top, Boolean isAll)
        {
            return VisaCenter.GetTopVisas(vtype, recommendNum, top, isAll);
        }

        public List<VisaCenterTB> GetTopVisas(String vtype, int recommendNum,int top = 10,int category=0)
        {
            return VisaCenter.GetTopVisas(vtype, recommendNum, top, category);
        }

        public Boolean DelVisas(String id)
        {
            return VisaCenter.DelVisas(id);
        }

        public Boolean UpdateVisaCenterTop(String id, int topstate)
        {
            return VisaCenter.UpdateVisaCenterTop(id, topstate);
        }

        public Boolean UpdateVisaState(String id, int state)
        {
            return VisaCenter.UpdateVisaState(id, state);
        }

        public int SaveVisaInfo(VisaCenterTB tb)
        {
            int result = VisaCenter.SaveVisa(tb);
            if (result > 0)
            {
                SysFileBLL bll = new SysFileBLL();
                bll.ChanageFileOfficial(tb.GUID);
            }
            return result;
        }

        public VisaCenterTB GetVisa(int autokey)
        {
            return VisaCenter.GetVisa(autokey);
        }

        public VisaCenterTB GetVisa(String vname)
        {
            return VisaCenter.GetVisa(vname);
        }

        public SearchModel<VisaCenterTB> GetVisas(String keyword, int pagesize, int pageindex, String IsEnable, String Range, String TopState, String vtype = "Visa")
        {
            SearchModel<VisaCenterTB> search = new SearchModel<VisaCenterTB>()
            {
                Keyword = keyword,
                PageIndex = pageindex,
                PageSize = pagesize,
                IsEnable = IsEnable,
                Range = Range,
                VType = vtype,
                TopState = TopState
            };
            int rowcount = 0;
            String formarturl = "/Conent/" + vtype + "?PageIndex={0}";
            if (!keyword.IsNullOrEmpty())
                formarturl += "&Keyword=" + keyword;
            if (!IsEnable.IsNullOrEmpty())
                formarturl += "&IsEnable=" + IsEnable;
            if (!Range.IsNullOrEmpty())
                formarturl += "&Range=" + Range;
            if (!TopState.IsNullOrEmpty())
                formarturl += "&TopState=" + TopState;

            search.DataList = VisaCenter.GetVisas(vtype, keyword, pagesize, out rowcount, pageindex, IsEnable, Range, TopState);
            search.RowCount = rowcount;
            search.Pagination = new PaginationHelper(pageindex, rowcount, pagesize, 8, formarturl);
            return search;
        }
    }
}
