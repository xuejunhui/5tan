using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WTAN.BLL;
using WTAN.Model.VModel;
using WTAN.Model.DModel;
using WTAN.CommonUtility;

namespace Demo.Controllers
{
    [UserSystemAuthorize]
    public class ContentController : Controller
    {
        [HttpPost]
        public ActionResult EditFriendLink(FriendLinkModel model)
        {
            if (ModelState.IsValid)
            {
                FriendLinkBLL bll = new FriendLinkBLL();
                FriendLinkTB tb = new FriendLinkTB();
                model.BindToDataModel(tb);
                model.AutoKey = bll.SaveFriendLink(tb);
                if (model.AutoKey > 0)
                    return Redirect("FriendLink");
                else
                    model.Msg = new Message(MessageState.error, "信息保存时发生异常.");
            }

            return View(model);
        }

        public ActionResult EditFriendLink(int autokey)
        {
            FriendLinkModel model = new FriendLinkModel();
            if (autokey > 0)
            {
                FriendLinkBLL bll = new FriendLinkBLL();
                FriendLinkTB tb = bll.GetFriendLink(autokey);
                model.BindModelData(tb);
            }
            return View(model);
        }

        public ActionResult FriendLink(int PageIndex = 1, String IsEnable = "", String Keyword = "")
        {
            FriendLinkBLL bll = new FriendLinkBLL();
            SearchModel<FriendLinkTB> model = bll.GetFriendLinks( Keyword, 20, PageIndex, IsEnable);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditSpecialColumn(SpecialColumnModel model)
        {
            if (ModelState.IsValid)
            {
                SpecialColumnBLL bll = new SpecialColumnBLL();
                SpecialColumnTB tb = new SpecialColumnTB();
                model.BindToDataModel(tb);
                model.AutoKey = bll.SaveSpecialColumn(tb);
                if (model.AutoKey > 0)
                    return Redirect(model.SCType.ToString());
                else
                    model.Msg = new Message(MessageState.error, "信息保存时发生异常.");
            }
            
            return View(model);
        }

        public ActionResult EditSpecialColumn(SpecialColumnType type,int autokey)
        {
            SpecialColumnModel model = new SpecialColumnModel()
            {
                SCType = type
            };
            if (autokey > 0)
            {
                SpecialColumnBLL bll = new SpecialColumnBLL();
                SpecialColumnTB tb = bll.GetSpecialColumn(autokey);
                model.BindModelData(tb);
            }
            else
            {
                model.GUID = Guid.NewGuid().ToString();
            }
            model.ParentID = type.ToInt32Value();
            model.ViewModel = type.ToString() + "View";
            return View(model);
        }

        public ActionResult Raiders(int PageIndex = 1, String IsEnable = "", String Keyword = "")
        {
            return SpecialColumnManage(SpecialColumnType.Raiders, PageIndex, IsEnable, Keyword);
        }

        public ActionResult VisaQuestion(int PageIndex = 1, String IsEnable = "", String Keyword = "")
        {
            return SpecialColumnManage(SpecialColumnType.VisaQuestion, PageIndex, IsEnable, Keyword);
        }

        public ActionResult AboutUs(int PageIndex = 1, String IsEnable = "", String Keyword = "")
        {
            return SpecialColumnManage(SpecialColumnType.AboutUs, PageIndex, IsEnable, Keyword);
        }

        ActionResult SpecialColumnManage(SpecialColumnType type, int PageIndex, String IsEnable, String Keyword)
        {
            SpecialColumnBLL bll = new SpecialColumnBLL();
            SearchModel<SpecialColumnTB> model = bll.GetSpecialColumns(type, Keyword, 20, PageIndex, IsEnable);
            return View("SpecialColumnManage", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditVisa(VisaModel model)
        {
            if (ModelState.IsValid)
            {
                VisaCenterBLL bll = new VisaCenterBLL();
                VisaCenterTB tb = new VisaCenterTB();
                model.BindToDataModel(tb);
                model.AutoKey = bll.SaveVisaInfo(tb);
                if (model.AutoKey > 0)
                    return Redirect(model.VType);
                else
                    model.Msg = new Message(MessageState.error, "信息保存时发生异常.");
            }
            CategoryBLL cbll = new CategoryBLL();
            model.CategoryDropDownNav = cbll.GetCategoryDropDownNav(model.CategoryID);
            return View(model);
        }

        public ActionResult EditVisa(int autokey, String vtype)
        {
            VisaModel model = new VisaModel();
            if (autokey > 0)
            {
                VisaCenterBLL vbll = new VisaCenterBLL();
                VisaCenterTB tb = vbll.GetVisa(autokey);
                CategoryBLL bll = new CategoryBLL();
                model.BindModelData(tb);
                model.CategoryDropDownNav = bll.GetCategoryDropDownNav(model.CategoryID);
            }
            else
                model.GUID = Guid.NewGuid().ToString();
            model.VType = vtype;
            return View(model);
        }

        public ActionResult Ticket(int PageIndex = 1, String IsEnable = "", String Keyword = "", String Range = "", String TopState = "")
        {
            VisaCenterBLL bll = new VisaCenterBLL();
            SearchModel<VisaCenterTB> model = bll.GetVisas(Keyword.ToEmptyTrimString(), 20, PageIndex, IsEnable, Range, TopState, "Ticket");
            return View("Visa", model);
        }

        public ActionResult Visa(int PageIndex = 1, String IsEnable = "", String Keyword = "", String Range = "", String TopState = "")
        {
            VisaCenterBLL bll = new VisaCenterBLL();
            SearchModel<VisaCenterTB> model = bll.GetVisas(Keyword.ToEmptyTrimString(), 20, PageIndex, IsEnable, Range, TopState);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditGuide(GuideModel model)
        {
            if (ModelState.IsValid)
            {
                GuideBLL bll = new GuideBLL();
                GuideTB tb = new GuideTB();
                model.BindToDataModel(tb);
                model.AutoKey = bll.SaveGuide(tb);
                if (model.AutoKey > 0)
                    return Redirect("Guide?guid=" + model.RelatedGUID + "&webname=" + tb.WebName);
                else
                    model.Msg = new Message(MessageState.error, "信息保存时发生异常.");
            }
            CategoryBLL cbll = new CategoryBLL();
            model.CategoryDropDownNav = cbll.GetCategoryDropDownNav(model.CategoryID);
            return View(model);
        }

        public ActionResult EditGuide(WebName webname, int autokey, String guid = "")
        {
            GuideModel model = null;
            if (autokey > 0)
            {
                GuideBLL gbll = new GuideBLL();
                GuideTB tb = gbll.GetGuide(autokey);
                model = new GuideModel();
                model.BindModelData(tb);

            }
            else if (!guid.IsNullOrEmpty())
            {
                model = new GuideModel()
                {
                    RelatedGUID = guid,
                    GUID = Guid.NewGuid().ToString(),
                    WebName = webname
                };
            }
            else
                return Redirect("/Content/Guide?webname=" + webname);

            if (!model.RelatedGUID.IsNullOrEmpty())
            {
                ContentBLL cbll = new ContentBLL();
                ContentTB ctb = cbll.GetContentInfo(model.RelatedGUID);
                ViewBag.Content = ctb;
                model.CategoryID = model.CategoryID == 0 ? ctb.CategoryID : model.CategoryID;
               
            }
            CategoryBLL bll = new CategoryBLL();
            model.CategoryDropDownNav = bll.GetCategoryDropDownNav(model.CategoryID);
            return View(model);
        }

        /// <summary>
        /// 目的地指南
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActionResult Guide(WebName webname,String guid = "", int PageIndex = 1, String IsEnable = "", String Keyword = "", String Range = "", String TopState = "")
        {
            GuideBLL bll = new GuideBLL();
            ContentBLL cbll = new ContentBLL();
            SearchModel<GuideTB> model = bll.GetGuides(webname, Keyword.ToEmptyTrimString(), 20, PageIndex, IsEnable, guid, Range, TopState);
            if (!guid.IsNullOrEmpty())
                ViewBag.Content = cbll.GetContentInfo(guid);

            return View(model);
        }

        /// <summary>
        /// 旅游管理
        /// </summary>
        /// <returns></returns>
        public ActionResult SummerManage(WebName webname,int PageIndex = 1, String IsEnable = "", String Range = "", String Keyword = "", String TopState = "")
        {
            ContentBLL bll = new ContentBLL();
            SearchModel<ContentTB> model = bll.GetSummerManageModel(webname, PageIndex, Keyword.ToEmptyTrimString(), 15, IsEnable, Range, TopState);
            return View(model);
        }


        public ActionResult EditSummer(int autokey,WebName webname)
        {
            ContentModel model = new ContentModel();
            if (autokey > 0)
            {
                ContentBLL bll = new ContentBLL();
                ContentTB tb = bll.GetContentInfo(autokey);
                model.BindModelData(tb);

                if (tb.CategoryID > 0)
                {
                    #region 获取加载下拉框的数据
                    CategoryBLL cbll = new CategoryBLL();
                    model.CategoryDropDownNav = cbll.GetCategoryDropDownNav(tb.CategoryID);
                    model.CategoryDropDownOfferedType = cbll.GetCategoryDropDownNav(tb.OfferedType);
                    model.CategoryDropDownTraffic = cbll.GetCategoryDropDownNav(tb.Transport);
                    model.CategoryDropDownTravelingDays = cbll.GetCategoryDropDownNav(tb.TravelingDays);
                    #endregion
                }
            }
            else
            {
                model.GUID = Guid.NewGuid().ToString();
                model.WebName = webname;
            }
            if (model.WebName == WebName.NetWork)
                return View("EditNetworkContent", model);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditSummer(ContentModel model)
        {
            #region 获取加载下拉框的数据
            CategoryBLL cbll = new CategoryBLL();
            model.CategoryDropDownNav = cbll.GetCategoryDropDownNav(model.CategoryID);
            if (model.WebName == WebName.Demo)
            {
                model.CategoryDropDownOfferedType = cbll.GetCategoryDropDownNav(model.OfferedType);
                model.CategoryDropDownTraffic = cbll.GetCategoryDropDownNav(model.Transport);
                model.CategoryDropDownTravelingDays = cbll.GetCategoryDropDownNav(model.TravelingDays);
            }
            else if (model.WebName == WebName.NetWork)
            {
                ModelState.Remove("TravelingDays");
                ModelState.Remove("Transport");
                ModelState.Remove("OfferedType");
                ModelState.Remove("MinSignUp");
                ModelState.Remove("XMLContent.ProductTravel");
                ModelState.Remove("XMLContent.ProductPrompt");
            }
            #endregion

            if (ModelState.IsValid)
            {
                if (Request.Files["upImg"].FileName.IsNullOrEmpty())
                {
                    if (model.AutoKey == 0 && model.PreviewIMG.IsNullOrEmpty())
                    {
                        model.Msg = new Message(MessageState.error, "您还未选择上传的图片");
                        return View(model);
                    }
                }
                else
                {
                    String saveName = String.Format("{0:yyyyMMDDHHmmss}{1}", DateTime.Now, UploadHelper.GetFileExtension(Request.Files["upImg"].FileName));
                    UploadInfo upinfo = Request.Files["upImg"].SaveAs(String.Format("/{0}{3}{1}/{2}/", AppSettings.EditUploadDir, "Travel_Img", model.GUID, model.WebName), saveName, AppSettings.UplaodImgType, AppSettings.UplaodImgSize, true, 360, 200);
                    if (upinfo.State == "SUCCESS")
                    {
                        if (!model.PreviewIMG.IsNullOrEmpty())
                            model.PreviewIMG.DelFile();//删除之前文件
                        model.PreviewIMG = upinfo.URL;
                    }
                    else
                    {
                        model.Msg = new Message(MessageState.error, upinfo.State);
                        return View(model);
                    }
                }

                ContentBLL bll = new ContentBLL();
                ContentTB tb = new ContentTB();
                model.BindToDataModel(tb);
                model.AutoKey = bll.SaveContentInfo(tb);

                if (model.AutoKey > 0)
                    return Redirect("SummerManage?webname=" + model.WebName);
                else
                    model.Msg = new Message(MessageState.error, "信息保存时发生异常.");
            }
            if (model.WebName == WebName.NetWork)
                return View("EditNetworkContent", model);
            return View(model);
        }

    }
}
