using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;

namespace WTAN.Model.VModel
{
    public class TicketCenterModel
    {
        public SearchModel<VisaCenterTB> SearchModel { get; set; }

        public List<VisaCenterTB> RelatedTickets { get; set; }

        public List<ContentTB> RelatedContent { get; set; }

        public CategoryTB CurrentCategory { get; set; }

        public List<CategoryTB> Ranges { get; set; }
    }
}
