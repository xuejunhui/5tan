using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WTAN.Model.DModel;
using WTAN.BLL;
using WTAN.CommonUtility;

namespace Network.Models
{
    public class GuideViewModel : ContentViewModel
    {

        public GuideViewModel(String seourl, String cseourl, int autokey)
            : base(seourl, cseourl)
        {
            if (autokey > 0)
                Guide = CurrentBLL.GBLL.GetGuide(autokey);
            if (Guide != null && Guide.AutoKey > 0)
            {
                this.Title = Guide.GuideName + " | " + this.Title;
                this.Keywords = Guide.KeyWord + " " + this.Keywords;
                this.Description = Guide.Description;
                base.AddNav(Guide.GuideName, URLUtility.NetWordGuideUrl(base.CurrentHeard.AutoKey, base.CurrentHeard.SEOURL, Guide.AutoKey));
            }
        } 

        public GuideViewModel(String seourl, int autokey)
            : base(seourl)
        {
            if (autokey > 0)
                Guide = CurrentBLL.GBLL.GetGuide(autokey);
            if (Guide != null && Guide.AutoKey > 0)
            {
                this.Title = Guide.GuideName + " | " + this.Title;
                this.Keywords = Guide.KeyWord + " " + this.Keywords;
                this.Description = Guide.Description;
                base.AddNav(Guide.GuideName, URLUtility.NetWordGuideUrl(base.CurrentHeard.AutoKey, base.CurrentHeard.SEOURL, Guide.AutoKey));
            }
        }

        public GuideTB Guide { get; set; }
    }
}