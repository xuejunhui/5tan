using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.IDAL;
using WTAN.Model.DModel;
using WTAN.CommonUtility;
using WTAN.Model.VModel;

namespace WTAN.BLL
{
    public class SpecialColumnBLL
    {
        ISpecialColumn SpecialColumn = DALFactory.DataAccess.CreateSpecialColumn();

        public List<SpecialColumnTB> GetSpecialColumns(SpecialColumnType type, int topnum)
        {
            return SpecialColumn.GetSpecialColumns(type, topnum);
        }

        public Boolean DelSpecialColumns(String id)
        {
            return SpecialColumn.DelSpecialColumns(id);
        }

        public Boolean UpdateSpecialColumnState(String id, int state)
        {
            return SpecialColumn.UpdateSpecialColumnState(id, state);
        }

        public int SaveSpecialColumn(SpecialColumnTB tb)
        {
            int result = SpecialColumn.SaveSpecialColumn(tb);
            if (result > 0)
            {
                SysFileBLL bll = new SysFileBLL();
                bll.ChanageFileOfficial(tb.GUID);
            }
            return result;
        }

        public SpecialColumnTB GetSpecialColumn(String seourl)
        {
            return SpecialColumn.GetSpecialColumn(seourl);
        }

        public SpecialColumnTB GetSpecialColumn(int autokey)
        {
            return SpecialColumn.GetSpecialColumn(autokey);
        }

        public SearchModel<SpecialColumnTB> GetSpecialColumns(SpecialColumnType type, String keyword, int pagesize, int pageindex, String IsEnable)
        {
            SearchModel<SpecialColumnTB> search = new SearchModel<SpecialColumnTB>()
            {
                Keyword = keyword,
                PageIndex = pageindex,
                PageSize = pagesize,
                IsEnable = IsEnable,
                SCType = type
            };
            int rowcount = 0;
            String formarturl = "/Conent/" + type.ToString() + "?PageIndex={0}";
            if (!keyword.IsNullOrEmpty())
                formarturl += "&Keyword=" + keyword;
            if (!IsEnable.IsNullOrEmpty())
                formarturl += "&IsEnable=" + IsEnable;


            search.DataList = SpecialColumn.GetSpecialColumns(type, keyword, pagesize, out rowcount, pageindex, IsEnable);
            search.RowCount = rowcount;
            search.Pagination = new PaginationHelper(pageindex, rowcount, pagesize, 8, formarturl);
            return search;
        }

        public SearchModel<SpecialColumnTB> GetSpecialColumns(SpecialColumnType type, String keyword, int pageindex, String formarturl)
        {
            int pagesize = 10;
            SearchModel<SpecialColumnTB> search = new SearchModel<SpecialColumnTB>()
            {
                Keyword = keyword,
                PageIndex = pageindex,
                PageSize = pagesize,
                SCType = type
            };
            int rowcount = 0;

            search.DataList = SpecialColumn.GetSpecialColumns(type, keyword, pagesize, out rowcount, pageindex, "1");
            search.RowCount = rowcount;
            search.Pagination = new PaginationHelper(pageindex, rowcount, pagesize, 8, formarturl);
            return search;
        }
    }
}
