using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;

namespace WTAN.Model.VModel
{
    public class GuideCenterModel
    {
        public List<CategoryTB> Ranges { get; set; }

        public SearchModel<GuideTB> SearchModel { get; set; }

        public CategoryTB CurrentCategory { get; set; }

        public List<CategoryTB> CategoryNav { get; set; }

        public List<GuideTB> RelatedGuides { get; set; }

        public List<ContentTB> RelatedContent { get; set; }
    }
}
