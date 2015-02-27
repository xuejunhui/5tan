using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;
using WTAN.CommonUtility;

namespace WTAN.Model.VModel
{
    public class SearchModel<T> where T : RecordItem, new()
    {
        public int CategoryID { get; set; }

        public WebName WebName { get; set; }

        public String VType { get; set; }

        public SpecialColumnType SCType { get; set; }

        public String Guid { get; set; }

        /// <summary>
        /// 置顶状态
        /// </summary>
        public String TopState { get; set; }

        /// <summary>
        /// 旅游范围
        /// </summary>
        public String Range { get; set; }

        /// <summary>
        /// 是否審核
        /// </summary>
        public String IsEnable { get; set; }

        /// <summary>
        /// 搜索关键字
        /// </summary>
        public String Keyword { get; set; }

        /// <summary>
        /// 查询到的数据列表
        /// </summary>
        public List<T> DataList { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页显示数据条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总数据条数
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// 分页类
        /// </summary>
        public PaginationHelper Pagination { get; set; }
    }
}
