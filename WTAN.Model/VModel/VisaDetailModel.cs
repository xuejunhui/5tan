using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;

namespace WTAN.Model.VModel
{
    public class VisaDetailModel
    {
        public VisaDetailModel(VisaCenterTB v)
        {
            this.VisaDetail = v;
        }

        public VisaCenterTB VisaDetail { get; set; }

        public List<SpecialColumnTB> VisaQuestion { get; set; }

        public List<VisaCenterTB> RelatedVisa { get; set; }
    }
}
