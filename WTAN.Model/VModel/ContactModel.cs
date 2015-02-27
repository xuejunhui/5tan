using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;

namespace WTAN.Model.VModel
{
    public class ContactModel
    {
        public SpecialColumnTB CurrentContact { get; set; }

        public List<SpecialColumnTB> RelatedContact { get; set; }

        public List<ContentTB> RelatedContent { get; set; }
    }
}
