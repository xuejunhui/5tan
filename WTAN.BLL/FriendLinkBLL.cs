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
    public class FriendLinkBLL
    {
        IFriendLink FriendLink = DALFactory.DataAccess.CreateFriendLink();

        public List<FriendLinkTB> GetFriendLinks(WebName webname)
        {
            return FriendLink.GetFriendLinks(webname);
        }

        public Boolean DelFriendLinks(String id)
        {
            return FriendLink.DelFriendLink(id);
        }

        public Boolean UpdateFriendLinkState(String id, int state)
        {
            return FriendLink.UpdateFriendLinkState(id, state);
        }

        public int SaveFriendLink(FriendLinkTB tb)
        {
            return FriendLink.SaveFriendLink(tb);
        } 

        public FriendLinkTB GetFriendLink(int autokey)
        {
            return FriendLink.GetFriendLink(autokey);
        }

        public FriendLinkTB GetFriendLinkByName(String linkname)
        {
            return FriendLink.GetFriendLink("LinkName", linkname);
        }

        public FriendLinkTB GetFriendLinkByUrl(String linkurl)
        {
            return FriendLink.GetFriendLink("LinkUrl", linkurl);
        }

        public SearchModel<FriendLinkTB> GetFriendLinks(String keyword, int pagesize, int pageindex, String IsEnable)
        {
            SearchModel<FriendLinkTB> search = new SearchModel<FriendLinkTB>()
            {
                Keyword = keyword,
                PageIndex = pageindex,
                PageSize = pagesize,
                IsEnable = IsEnable
            };
            int rowcount = 0;
            String formarturl = "/Conent/FriendLink?PageIndex={0}";
            if (!keyword.IsNullOrEmpty())
                formarturl += "&Keyword=" + keyword;
            if (!IsEnable.IsNullOrEmpty())
                formarturl += "&IsEnable=" + IsEnable;

            search.DataList = FriendLink.GetFriendLinks(keyword, pagesize, out rowcount, pageindex, IsEnable);
            search.RowCount = rowcount;
            search.Pagination = new PaginationHelper(pageindex, rowcount, pagesize, 8, formarturl);
            return search;
        }
    }
}
