using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;

namespace WTAN.Model.VModel
{
    public class GuideDetailModel
    {
        public GuideDetailModel(GuideTB g)
        {
            this.Guide = g;
        }

        public GuideTB Guide { get; set; }

        public List<CategoryTB> CategoryNav { get; set; }

        public List<ContentTB> RelatedContent { get; set; }

        public List<GuideTB> RelatedGuides { get; set; }
    }
}
