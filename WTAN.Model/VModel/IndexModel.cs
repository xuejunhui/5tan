using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;

namespace WTAN.Model.VModel
{
    public class IndexModel
    {
        /// <summary>
        /// 散客拼團
        /// </summary>
        public List<ContentTB> FitFightGroups { get; set; }

        /// <summary>
        /// 獨立組團
        /// </summary>
        public List<ContentTB> IndependentGroups { get; set; }

        public List<VisaCenterTB> TicketTopList { get; set; }

        public List<VisaCenterTB> VisaTopList { get; set; }

        public List<SpecialColumnTB> RaidersNewList { get; set; }

        public List<FriendLinkTB> FriendLinks { get; set; }

        public List<GuideTB> GuidesTopList { get; set; }
    }
}
