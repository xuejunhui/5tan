using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WTAN.BLL;
using WTAN.CommonUtility;
using WTAN.Model.DModel;
using WTAN.Model.VModel;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult ApplicationFriends(ApplicationFriendsModel model)
        {
            if (ModelState.IsValid)
            {
                model.Note = model.Email + Environment.NewLine + model.Note;
                FriendLinkBLL bll = new FriendLinkBLL();
                FriendLinkTB tb = new FriendLinkTB();
                model.BindToDataModel(tb);
                tb.AutoKey = 0;
                tb.Enable = false;
                if (bll.SaveFriendLink(tb) > 0)
                {
                    model = new ApplicationFriendsModel();
                    return RedirectToAction("ApplicationFriends", new { isSucc = true });//不得已 跳转一下 防止某些浏览器能F5刷新提交
                }
                else
                    model.Msg = new Message(MessageState.error, "您的申请未能成功提交,请联系网页右侧客服!");
            }
            return View(model);
        }

        public ActionResult ApplicationFriends(Boolean isSucc = false)
        {
            ApplicationFriendsModel model = new ApplicationFriendsModel();
            if (isSucc)
                model.Msg = new Message(MessageState.success, "您的申请已经成功提交到网站管理员,我们将在1-5个工作日内进行处理.请您耐心等候!");
            return View(model);
        }

        public ActionResult RaiderCenter(int pageindex = 0)
        {
            SpecialColumnBLL bll = new SpecialColumnBLL();
            ContentBLL cbll = new ContentBLL();
            GuideBLL gbll = new GuideBLL();
            RaiderCenterModel model = new RaiderCenterModel();

            model.SearchModel = bll.GetSpecialColumns(SpecialColumnType.Raiders, "", pageindex, URLUtility.RaiderCenterUrlFormart());
            model.RelatedContent = cbll.GetRelatedContent(WebName.Demo, "", 0, 5, 2, true);
            model.RelatedGuide = gbll.GetRelatedGuides(WebName.Demo,"", 0, 6, 2);

            return View(model);
        }

        public ActionResult RaiderDetail(int autokey = 0, String seourl = "")
        {
            ContactModel model = new ContactModel();
            SpecialColumnBLL bll = new SpecialColumnBLL();
            ContentBLL cbll = new ContentBLL();
            if (autokey == 0 && seourl.IsNullOrEmpty())
            {
                return Redirect(URLUtility.page404());
            }
            else if (!seourl.IsNullOrEmpty())
            {
                SpecialColumnTB currspec = bll.GetSpecialColumn(seourl);

                if (currspec == null || currspec.AutoKey == 0)
                    return Redirect(URLUtility.page404());

                model.CurrentContact = currspec;
            }
            else if (autokey > 0)
            {
                SpecialColumnTB currspec = bll.GetSpecialColumn(autokey);

                if (currspec != null && !currspec.SEOURL.IsNullOrEmpty())
                    return RedirectPermanent(URLUtility.ContactUrl(0, currspec.SEOURL));
                //seourl如果為數字 將會被global 識別到autokey
                if (currspec == null || currspec.AutoKey == 0)//autokey 可能為seourl
                    currspec = bll.GetSpecialColumn(autokey.ToString());
                if (currspec == null || currspec.AutoKey == 0)
                    return Redirect(URLUtility.page404());

                model.CurrentContact = currspec;
            }
            model.RelatedContent = cbll.GetRelatedContent(WebName.Demo, "", 0, 5, 2, true);
            model.RelatedContact = bll.GetSpecialColumns(SpecialColumnType.Raiders, 10);
            return View(model);
        }

        public ActionResult TicketDetail(int autokey = 0)
        {
            VisaDetailModel model = null;
            VisaCenterBLL bll = new VisaCenterBLL();
            if(autokey==0)
                return Redirect(URLUtility.page404());
            else if (autokey > 0)
            {
                VisaCenterTB ticket = bll.GetVisa(autokey);
                if (ticket == null || ticket.AutoKey==0)
                    return Redirect(URLUtility.page404());
                model = new VisaDetailModel(ticket);
            }
            model.RelatedVisa = bll.GetTopVisas("Ticket", 2);
            return View(model);
        }

        public ActionResult TicketCenter(int pageindex = 1, int categoryid = 0)
        {
            CategoryBLL cbll = new CategoryBLL();
            VisaCenterBLL bll = new VisaCenterBLL();
            TicketCenterModel model = new TicketCenterModel();
            ContentBLL ctbll = new ContentBLL();

            categoryid = categoryid == 0 ? (int)DropDownState.TicketCategory : categoryid;
            model.CurrentCategory = cbll.GetCategory(categoryid);
            if (model.CurrentCategory == null || model.CurrentCategory.AutoKey == 0)
                return RedirectPermanent(URLUtility.TicketCenterUrl());

            model.SearchModel = bll.GetTickets(pageindex, categoryid);
            model.RelatedContent = ctbll.GetRelatedContent(WebName.Demo, "", 0, 10, 2);
            model.RelatedTickets = bll.GetTopVisas("Ticket", 2, 10, categoryid == (int)DropDownState.TicketCategory ? 0 : categoryid);
            model.Ranges = cbll.GetCategorysByParentID((int)DropDownState.TicketCategory);
            return View(model);
        }

        public ActionResult Contact(int autokey = 0, String seourl = "")
        {
            ContactModel model = new ContactModel();
            SpecialColumnBLL bll = new SpecialColumnBLL();
            if (autokey == 0 && seourl.IsNullOrEmpty())
            {
                return Redirect(URLUtility.page404());
            }
            else if (!seourl.IsNullOrEmpty())
            {
                SpecialColumnTB currspec = bll.GetSpecialColumn(seourl);

                if (currspec == null || currspec.AutoKey == 0)
                    return Redirect(URLUtility.page404());

                model.CurrentContact = currspec;
            }
            else if (autokey > 0)
            {
                SpecialColumnTB currspec = bll.GetSpecialColumn(autokey);

                if (currspec != null && !currspec.SEOURL.IsNullOrEmpty())
                    return RedirectPermanent(URLUtility.ContactUrl(0, currspec.SEOURL));
                //seourl如果為數字 將會被global 識別到autokey
                if (currspec == null || currspec.AutoKey == 0)//autokey 可能為seourl
                    currspec = bll.GetSpecialColumn(autokey.ToString());
                if (currspec == null || currspec.AutoKey == 0)
                    return Redirect(URLUtility.page404());

                model.CurrentContact = currspec;
            }
            model.RelatedContact = bll.GetSpecialColumns(SpecialColumnType.AboutUs, 0);

            return View(model);
        }

        public ActionResult LoadRelatedImg(String guid, int top = 5, Boolean IsAll = true)
        {
            SysFileBLL bll = new SysFileBLL();
            List<Sys_FilesTB> model = bll.GetFilesList(guid, top, IsAll);
            return PartialView(model);
        }

        public ActionResult GuideCenter(int pageindex = 1, String range = "", int categoryid = 0)
        {
            CategoryBLL cbll = new CategoryBLL();
            GuideBLL bll = new GuideBLL();
            GuideCenterModel model = new GuideCenterModel();
            ContentBLL ctbll = new ContentBLL();

            categoryid = categoryid == 0 ? (int)DropDownState.CategoryID : categoryid;
            model.CurrentCategory = cbll.GetCategory(categoryid);

            if (model.CurrentCategory == null || model.CurrentCategory.AutoKey == 0)
                return RedirectPermanent(URLUtility.GuideCenterUrl());

            model.CategoryNav = cbll.GetCategoryNav(categoryid);
            model.Ranges = cbll.GetCategorysByParentID(categoryid);
            range = range.IsNullOrEmpty() && categoryid != (int)DropDownState.CategoryID ? model.CurrentCategory.CategoryName : range;
            model.SearchModel = bll.GetGuides(WebName.Demo, "", pageindex, range, 10, categoryid);
            model.RelatedGuides = bll.GetRelatedGuides(WebName.Demo, model.CurrentCategory["CategoryNav"].ToEmptyTrimString(), 0, 5, 2);
            model.RelatedContent = ctbll.GetRelatedContent(WebName.Demo, model.CurrentCategory["CategoryNav"].ToEmptyTrimString(), 0, 10, 2);
            return View(model);
        }

        public ActionResult VisaQuestionDetail(String seourl, SpecialColumnTB question = null)
        {
            SpecialColumnBLL bll = new SpecialColumnBLL();
            VisaCenterBLL vbll = new VisaCenterBLL();
            if (question == null || question.AutoKey == 0)
            {
                question = bll.GetSpecialColumn(seourl);
                if (question == null || question.AutoKey == 0)
                    return Redirect(URLUtility.page404());
            }
            VisaQuestionDetailModel model = new VisaQuestionDetailModel(question);
            model.VisaQuestion = bll.GetSpecialColumns(SpecialColumnType.VisaQuestion, 10);
            model.RelatedVisa = vbll.GetTopVisas("Visa", 0);
            return View("VisaQuestionDetail", model);
        }

        public ActionResult VisaQuestionDetailA(int autokey)
        {
            SpecialColumnBLL bll = new SpecialColumnBLL();
            SpecialColumnTB question = bll.GetSpecialColumn(autokey);
            if (question == null || question.AutoKey == 0)
                return Redirect(URLUtility.page404());
            else if (!question.SEOURL.IsNullOrEmpty())
                return RedirectPermanent(URLUtility.VisaQuestionDetailUrl(autokey, question.SEOURL));
            else
                return VisaQuestionDetail("", question);
        }

        public ActionResult VisaQuestion(int pageindex = 1,String keyword="")
        {
            SpecialColumnCenterModel model = new SpecialColumnCenterModel();
            SpecialColumnBLL bll = new SpecialColumnBLL();
            VisaCenterBLL vbll = new VisaCenterBLL();
            String formarturl = URLUtility.VisaQuestionUrlFormart(keyword);
            model.SearchModel = bll.GetSpecialColumns(SpecialColumnType.VisaQuestion, keyword, pageindex, formarturl);
            model.RelatedVisa = vbll.GetTopVisas("Visa", 0);
            return View(model);
        }

        public ActionResult VisaDetail(int autokey)
        {
            VisaCenterBLL bll = new VisaCenterBLL();
            VisaCenterTB visa = bll.GetVisa(autokey);
            SpecialColumnBLL sbll = new SpecialColumnBLL();
            if (visa == null || visa.AutoKey == 0)
                return Redirect(URLUtility.page404());

            VisaDetailModel model = new VisaDetailModel(visa);
            model.VisaQuestion = sbll.GetSpecialColumns(SpecialColumnType.VisaQuestion, 10);
            model.RelatedVisa = bll.GetTopVisas("Visa", 0, 10, visa.CategoryID);
            return View(model);
        }

        public ActionResult VisaCenter()
        {
            CategoryBLL cbll = new CategoryBLL();
            VisaCenterBLL bll = new VisaCenterBLL();
            SpecialColumnBLL sbll = new SpecialColumnBLL();
            VisaCenterModel model = new VisaCenterModel(cbll.GetCategory((int)DropDownState.VisaCategory));
            if (model.VisaCenter == null || model.VisaCenter.AutoKey == 0)
                return Redirect(URLUtility.page404());
            model.VisaList = bll.GetTopVisas("Visa", 0, 0);
            model.VisaCategory = cbll.GetCategorysByParentID((int)DropDownState.VisaCategory);
            model.VisaQuestion = sbll.GetSpecialColumns(SpecialColumnType.VisaQuestion, 10);
            return View(model);
        }

        public ActionResult GuideDetail(int autokey)
        {
            GuideBLL bll = new GuideBLL();
            ContentBLL cobll = new ContentBLL();
            CategoryBLL cbll = new CategoryBLL();
            GuideTB g = bll.GetGuide(autokey);
            if(g==null || g.AutoKey==0)
                return Redirect(URLUtility.page404());
            GuideDetailModel model = new GuideDetailModel(g);
            model.CategoryNav = cbll.GetCategoryNav(g.CategoryID);
            model.RelatedContent = cobll.GetRelatedContent(WebName.Demo, g["CategoryName"].ToEmptyTrimString(), 0, 6);
            model.RelatedGuides = bll.GetRelatedGuides(WebName.Demo, g["CategoryName"].ToEmptyTrimString(), g.AutoKey);
            return View(model);
        }

        

        public ActionResult DetailA(int autokey)
        {
            ContentBLL bll = new ContentBLL();
            CategoryBLL cbll = new CategoryBLL();
            ContentTB c = bll.GetContentInfo(autokey);
            
            if (c == null || c.AutoKey == 0)
                return Redirect(URLUtility.page404());
            if (!c.SEOURL.IsNullOrEmpty())
                return Redirect(URLUtility.SummerDetailUrl(autokey, c.SEOURL));

            return Detail("", c);
        }

        public ActionResult Detail(String seourl, ContentTB c = null)
        {
            ContentBLL bll = new ContentBLL();
            GuideBLL gbll = new GuideBLL();
            CategoryBLL cbll = new CategoryBLL();
            if (c == null || c.AutoKey == 0)
            {
                c = bll.GetContentInfo("SEOURL", seourl);
                if (c == null || c.AutoKey == 0)
                    return Redirect(URLUtility.page404());
            }
            
            SummerDetailModel model = new SummerDetailModel(c);
            model.CategoryNav = cbll.GetCategoryNav(c.CategoryID);
            model.Guides = gbll.GetGuides(c.GUID);
            model.RelatedContent = bll.GetRelatedContent(WebName.Demo, c["CategoryName"].ToEmptyTrimString(), c.AutoKey);
            return View("Detail", model);
        }

        public ActionResult PreviewPhoto()
        {
            return View();
        }

        /// <summary>
        /// 旅遊區域列表視圖
        /// </summary>
        /// <returns></returns>
        public ActionResult Region(String categoryURL, String days = "", String traffic = "", int pageIndex = 1, String range = "", CategoryTB category = null)
        {
            CategoryBLL bll = new CategoryBLL();
            ContentBLL cbll = new ContentBLL();
            VisaCenterBLL vbll = new VisaCenterBLL();
            GuideBLL gbll = new GuideBLL();
            if (category == null || category.AutoKey == 0)
            {
                category = bll.GetCategoryBySEO(categoryURL);
                if (category == null || category.AutoKey == 0)
                    return Redirect(URLUtility.page404());
            }

            RegionModel model = new RegionModel(days, traffic, category, range);
            bll.BindRegionModel(model);
            range = range == "" ? category.CategoryName : range;
            model.SearchModel = cbll.GetRegionSearchModel(WebName.Demo, days, traffic, range, pageIndex, 20, URLUtility.CategoryFormartUrl(category.AutoKey, categoryURL, days, traffic, range));
            model.RelatedContent = cbll.GetRelatedContent(WebName.Demo, category["CategoryNav"].ToEmptyTrimString(), 0, 10, 2);
            model.TopTickets = vbll.GetTopVisas("Ticket", 2);
            model.TopVisas = vbll.GetTopVisas("Visa", 2);
            model.RelatedGuide = gbll.GetRelatedGuides(WebName.Demo, category["CategoryNav"].ToEmptyTrimString(), 0);
            return View("Region", model);
        }

        public ActionResult RegionByID(int categoryid, String days = "", String traffic = "", int pageIndex = 1, String range = "")
        {
            CategoryBLL bll = new CategoryBLL();
            ContentBLL cbll = new ContentBLL();
            CategoryTB category = bll.GetCategory(categoryid);
            if (category == null || category.AutoKey == 0)
                return Redirect(URLUtility.page404());
            else if (!category.SEOURL.IsNullOrEmpty())
                return RedirectPermanent(URLUtility.CategoryUrl(categoryid, category.SEOURL));//地址重定向

            return Region("", days, traffic, pageIndex, range, category);
        }

        public ActionResult Index()
        {
            IndexModel model = new IndexModel();
            ContentBLL bll = new ContentBLL();
            VisaCenterBLL vbll = new VisaCenterBLL();
            SpecialColumnBLL sbll = new SpecialColumnBLL();
            FriendLinkBLL fbll = new FriendLinkBLL();
            GuideBLL gbll = new GuideBLL();

            model.IndependentGroups = bll.GetOfferedTypeTopContent("独立组团", 10, 2, true);
            model.FitFightGroups = bll.GetOfferedTypeTopContent("散客拼团", 10, 2, true);
            model.TicketTopList = vbll.GetTopVisas("Ticket", 2, 10, true);
            model.VisaTopList = vbll.GetTopVisas("Visa", 2, 10, true);
            model.RaidersNewList = sbll.GetSpecialColumns(SpecialColumnType.Raiders, 10);
            model.FriendLinks = fbll.GetFriendLinks(WebName.Demo);
            model.GuidesTopList = gbll.GetRelatedGuides(WebName.Demo,"", 0, 8, 1);

            return View(model);
        }

        public ActionResult Page404()
        {
            return PartialView();
        }

        public ActionResult Map()
        {
            return View();
        }

        public ActionResult Search(String keyword = "", String ctype = "All", int pageindex = 1)
        {
            ContentBLL bll = new ContentBLL();
            SearchModel<RecordItem> model = null;
            if (keyword.IsNullOrEmpty())
            {
                model = new SearchModel<RecordItem>()
                {
                    Range = ctype
                };
            }
            else
                model = bll.GetAllContentSearchModel(WebName.Demo, pageindex, keyword, 20, ctype);
            if (model.RowCount > 0 && pageindex > 1 && model.DataList.Count() == 0)
                return RedirectPermanent(URLUtility.SearchUrl(ctype, keyword));
            return PartialView(model);
        }

    }
}
