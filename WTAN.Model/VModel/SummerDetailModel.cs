using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.CommonUtility;
using WTAN.Model.DModel;

namespace WTAN.Model.VModel
{
    public class SummerDetailModel
    {
        public SummerDetailModel(ContentTB Content)
        {
            this.Content = Content;
            ContentModel model = new ContentModel();
            model.BindModelData(this.Content);
            this.ContentModel = model;
        }
        
        public ContentTB Content { get; set; }

        public ContentModel ContentModel { get; set; }

        public List<CategoryTB> CategoryNav { get; set; }

        /// <summary>
        /// 目的地指南
        /// </summary>
        public List<GuideTB> Guides { get; set; }

        public List<ContentTB> RelatedContent { get; set; }
    }
}
