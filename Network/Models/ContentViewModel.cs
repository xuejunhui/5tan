using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WTAN.BLL;
using WTAN.Model.DModel;
using WTAN.CommonUtility;
using WTAN.Model.VModel;

namespace Network.Models
{

    public class ContentViewModel : ViewModel
    {
        public ContentViewModel(String seourl, String categoryseourl = "",int pageindex=1)
            : base(seourl, categoryseourl)
        {
            if (base.CurrentHeard != null && base.CurrentHeard.AutoKey != 0)
            {
                this.Title = base.CurrentHeard.Title + "-" + this.Title;
                this.Keywords = base.CurrentHeard.KeyWord + " " + this.Keywords;
                this.Description = base.CurrentHeard.Description + "." + this.Description;
                base.AddNav(base.CurrentHeard.Shorttitle, URLUtility.NetWrokContentUrl(base.CurrentHeard.AutoKey, base.CurrentHeard.SEOURL));
                if (base.IsWebsiteCase || base.IsNews)//網站案例或新聞用列表方式展示
                {
                    this.Title = base.CurrentCategory.CategoryName + "|" + this.Title;
                    this.Keywords = base.CurrentCategory.KeyWord + " " + this.Keywords;
                    this.Description = base.CurrentCategory.Description + this.Description;
                }

                if (IsWebsiteCase || IsNews)
                    _SearchGuides = CurrentBLL.GBLL.GetGuides(WebName.NetWork, "", 10, pageindex, CurrentCategory.AutoKey, URLUtility.NetWordCategoryUrl(CurrentHeard.AutoKey, CurrentHeard.SEOURL, CurrentCategory.AutoKey, CurrentCategory.SEOURL) + "?pageindex={0}");
               
            }
            else
                base.CurrentHeard = new ContentTB();
        }


       private SearchModel<GuideTB> _SearchGuides = null;
       public SearchModel<GuideTB> SearchGuides {
           get { return _SearchGuides; }
       }
       private List<GuideTB> _Guides = null;
       public List<GuideTB> Guides {
        get{
            if (_Guides != null)
                return _Guides;
            else if (base.CurrentHeard.AutoKey > 0 && _Guides == null)
            {
                _Guides = CurrentBLL.GBLL.GetGuides(base.CurrentHeard.GUID);
                return _Guides;
            }
            else
                return new List<GuideTB>();
        }
       }
    }
}