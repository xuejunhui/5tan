using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;

namespace WTAN.Model.VModel
{
    public class SpecialColumnCenterModel
    {

        public SearchModel<SpecialColumnTB> SearchModel { get; set; }

        public List<VisaCenterTB> RelatedVisa { get; set; }
    }
}
