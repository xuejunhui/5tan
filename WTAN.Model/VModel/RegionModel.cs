using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;

namespace WTAN.Model.VModel
{
    public class RegionModel
    {
        public RegionModel(String Days, String Traffic,CategoryTB Category,String Range)
        {
            this.CurrentCategory = Category;
            this.Days = Days;
            this.Traffic = Traffic;
            this.Range = Range;
        }

        /// <summary>
        /// 當前分類
        /// </summary>
        public CategoryTB CurrentCategory { get; set; }

        /// <summary>
        /// 當前分類的下級分類
        /// </summary>
        public List<CategoryTB> CategoryChilds { get; set; }

        /// <summary>
        /// 交通分類
        /// </summary>
        public List<CategoryTB> TrafficChilds { get; set; }

        /// <summary>
        /// 天數分類
        /// </summary>
        public List<CategoryTB> DaysChilds { get; set; }

        /// <summary>
        /// 当前搜寻的范围
        /// </summary>
        public String Range { get; set; }

        /// <summary>
        /// 篩選條件 天數
        /// </summary>
        public String Days { get; set; }

        /// <summary>
        /// 篩選條件 天數
        /// </summary>
        public String Traffic { get; set; }

        public List<CategoryTB> CategoryNav { get; set; }

        public SearchModel<ContentTB> SearchModel { get; set; }

        public List<ContentTB> RelatedContent { get; set; }

        public List<VisaCenterTB> TopVisas { get; set; }

        public List<VisaCenterTB> TopTickets { get; set; }

        public List<GuideTB> RelatedGuide { get; set; }
    }
}
