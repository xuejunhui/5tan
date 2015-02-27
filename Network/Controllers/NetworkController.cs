using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WTAN.CommonUtility;
using WTAN.BLL;
using WTAN.Model.DModel;
using WTAN.Model.VModel;
using Network.Models;

namespace Network.Controllers
{
    public class NetworkController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.ShowLinks = true;
            ViewModel model = new ViewModel();
            return View(model);
        }

        public ActionResult Content(String seourl)
        {
            ContentViewModel model = new ContentViewModel(seourl);
            if (model.CurrentHeard.AutoKey == 0)
                return Redirect(URLUtility.page404());
            if (model.IsWebsiteCase || model.IsNews)
                return RedirectPermanent(URLUtility.NetWordCategoryUrl(model.CurrentHeard.AutoKey, model.CurrentHeard.SEOURL, model.CurrentCategory.AutoKey, model.CurrentCategory.SEOURL));
            return View(model);
        }

        public ActionResult WebsiteCase(String cseourl, int pageindex = 1)
        {
            ContentViewModel model = new ContentViewModel("WebsiteCase", cseourl, pageindex);
            if (model.CurrentHeard.AutoKey == 0)
                return Redirect(URLUtility.page404());
            return View(model);
        }

        public ActionResult News(String cseourl, int pageindex = 1)
        {
            ContentViewModel model = new ContentViewModel("News", cseourl, pageindex);
            if (model.CurrentHeard.AutoKey == 0)
                return Redirect(URLUtility.page404());
            return View(model);
        }

        public ActionResult Guide(String seourl, int autokey)
        {
            GuideViewModel model = new GuideViewModel(seourl, autokey);
            if(model.CurrentHeard.AutoKey==0|| model.Guide==null || model.Guide.AutoKey==0)
                return Redirect(URLUtility.page404());
            return View(model);
        }

        public ActionResult GuideCase(String seourl, String cseourl, int autokey)
        {
            GuideViewModel model = new GuideViewModel(seourl, cseourl, autokey);
            if (model.CurrentHeard.AutoKey == 0 || model.Guide == null || model.Guide.AutoKey == 0)
                return Redirect(URLUtility.page404());
            return View("Guide", model);
        }

        public ActionResult Page404()
        {
            return PartialView();
        }
    }
}
