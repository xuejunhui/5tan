using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;

namespace WTAN.Model.VModel
{
    public class VisaQuestionDetailModel : SpecialColumnDetailModel
    {
        public VisaQuestionDetailModel(SpecialColumnTB s)
            : base(s)
        { 
            
        }

        public List<SpecialColumnTB> VisaQuestion { get; set; }

        public List<VisaCenterTB> RelatedVisa { get; set; }
    }

    public class SpecialColumnDetailModel
    {
        public SpecialColumnDetailModel(SpecialColumnTB s)
        {
            this.SpecialColumn = s;
        }

        public SpecialColumnTB SpecialColumn { get; set; }
    }
}
