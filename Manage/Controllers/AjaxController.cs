using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WTAN.BLL;
using WTAN.Model.VModel;
using WTAN.CommonUtility;
using WTAN.Model.DModel;

namespace Demo.Controllers
{
    public class AjaxController : Controller
    {
        public void ClearCache()
        {
            CacheHelper.ClearServerCache();
        }

        [UserSystemAuthorize]
        public JsonResult VerifySpecialColumn(String id, int state)
        {
            if (!id.IsNullOrEmpty())
            {
                SpecialColumnBLL bll = new SpecialColumnBLL();
                return Json(bll.UpdateSpecialColumnState(id, state), JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [UserSystemAuthorize]
        public JsonResult VerifyVisa(String id, int state)
        {
            if (!id.IsNullOrEmpty())
            {
                VisaCenterBLL bll = new VisaCenterBLL();
                return Json(bll.UpdateVisaState(id, state), JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [UserSystemAuthorize]
        public JsonResult VerifyGuide(String id, int state)
        {
            if (!id.IsNullOrEmpty())
            {
                GuideBLL bll = new GuideBLL();
                return Json(bll.UpdateGuideState(id, state), JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [UserSystemAuthorize]
        public JsonResult SetTopGuide(String id, int state)
        {
            if (!id.IsNullOrEmpty())
            {
                GuideBLL bll = new GuideBLL();
                return Json(bll.UpdateGuideTop(id, state), JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [UserSystemAuthorize]
        public JsonResult SetTopVisaCenter(String id, int state)
        { 
            if (!id.IsNullOrEmpty())
            {
                VisaCenterBLL bll = new VisaCenterBLL();
                return Json(bll.UpdateVisaCenterTop(id, state), JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [UserSystemAuthorize]
        public JsonResult SetTopContent(String id, int state)
        {
            if (!id.IsNullOrEmpty())
            {
                ContentBLL bll = new ContentBLL();
                return Json(bll.UpdateContentTop(id, state), JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [UserSystemAuthorize]
        public JsonResult VerifyFriendLink(String id, int state)
        {
            if (!id.IsNullOrEmpty())
            {
                FriendLinkBLL bll = new FriendLinkBLL();
                return Json(bll.UpdateFriendLinkState(id, state), JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [UserSystemAuthorize]
        public JsonResult VerifyContent(String id,int state)
        {
            if (!id.IsNullOrEmpty())
            {
                ContentBLL bll = new ContentBLL();
                return Json(bll.UpdateContentState(id, state), JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [UserSystemAuthorize]
        public ActionResult LoadSelectCategoryDropDownBox(DropDownState state, String value, String name)
        {
            CategoryBLL bll = new CategoryBLL();
            List<CategoryTB> list = bll.GetCategorysByParentID((int)state);
            ViewBag.Name = name;
            ViewBag.Value = value;
            return PartialView(list);
        }

        /// <summary>
        /// 系统分类下拉框
        /// </summary>
        /// <param name="parentid">第一个框的autokey</param>
        /// <returns></returns>
        [UserSystemAuthorize]
        public ActionResult DropDownBox(String parentid, String value, DropDownState dname)
        {
            CategoryBLL bll = new CategoryBLL();
            int pid = 0;
            if (parentid.IsNumber())
            {
                pid = parentid.ToInt32Value();
            }
            else
            {
                CategoryTB c = bll.GetCategory(parentid);
                pid = c.AutoKey;
            }
            CategoryDropDownBox model = new CategoryDropDownBox()
            {
                Value = value,
                DropDownData = bll.GetCategorysByParentID(pid),
                CategoryIDName = dname,
                ParentID = pid
            };
            return PartialView(model);
        }

        /// <summary>
        /// 用于xml字段模型的前端验证
        /// </summary>
        /// <param name="ContentXML"></param>
        /// <returns></returns>
        [UserSystemAuthorize]
        public JsonResult CheckXMLModel(ConfigXml model)
        {

            return Json(ModelState.IsValid, JsonRequestBehavior.AllowGet);
        }

        [UserSystemAuthorize]
        [HttpPost]
        public JsonResult DelFriendLinkInfo(String id)
        {
            if (id.IsNullOrEmpty())
                return Json(false, JsonRequestBehavior.AllowGet);
            FriendLinkBLL bll = new FriendLinkBLL();
            Boolean result = bll.DelFriendLinks(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [UserSystemAuthorize]
        public JsonResult DelSpecialColumnInfo(String id)
        {
            if (id.IsNullOrEmpty())
                return Json(false, JsonRequestBehavior.AllowGet);
            SpecialColumnBLL bll = new SpecialColumnBLL();
            Boolean result = bll.DelSpecialColumns(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [UserSystemAuthorize]
        public JsonResult DelVisaInfo(String id)
        {
            if (id.IsNullOrEmpty())
                return Json(false, JsonRequestBehavior.AllowGet);
            VisaCenterBLL bll = new VisaCenterBLL();
            Boolean result = bll.DelVisas(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [UserSystemAuthorize]
        public JsonResult DelContentInfo(String id)
        {
            if (id.IsNullOrEmpty())
                return Json(false, JsonRequestBehavior.AllowGet);
            ContentBLL bll = new ContentBLL();
            Boolean result = bll.DelContentInfo(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [UserSystemAuthorize]
        public JsonResult DelGuideInfo(String id)
        {
            if (id.IsNullOrEmpty())
                return Json(false, JsonRequestBehavior.AllowGet);
            GuideBLL bll = new GuideBLL();
            Boolean result = bll.DelGuides(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [UserSystemAuthorize]
        public JsonResult DelAdsInfo(String id)
        {
            if (!id.IsNullOrEmpty())
            {
                ConfigBLL bll = new ConfigBLL();
                return Json(bll.DelSysConfigInfo(id), JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [UserSystemAuthorize]
        public ActionResult DelCategory(String id)
        {
            if (id.IsNullOrEmpty())
                return Json(false, JsonRequestBehavior.AllowGet);
            CategoryBLL bll = new CategoryBLL();
            return Json(bll.DelCategory(id), JsonRequestBehavior.AllowGet);
        }

        [UserSystemAuthorize]
        public JsonResult VerificationVisaName(String VName, int AutoKey)
        {
            VisaCenterBLL bll = new VisaCenterBLL();
            VisaCenterTB v = bll.GetVisa(VName);
            return Json(!(v != null && v.AutoKey != AutoKey), JsonRequestBehavior.AllowGet);
        }

        [UserSystemAuthorize]
        public JsonResult VerificationCategoryName(String CategoryName, int AutoKey)
        {
            CategoryBLL bll = new CategoryBLL();
            CategoryTB c = bll.GetCategory(CategoryName);
            return Json(!(c != null && c.AutoKey != AutoKey), JsonRequestBehavior.AllowGet);
        }

        [UserSystemAuthorize]
        public JsonResult VerificationCategorySEOURL(String SEOURL, int AutoKey)
        {
            CategoryBLL bll = new CategoryBLL();
            CategoryTB c = bll.GetCategoryBySEO(SEOURL);
            return Json(!(c != null && c.AutoKey != AutoKey), JsonRequestBehavior.AllowGet);
        }

        [UserSystemAuthorize]
        public JsonResult VerificationContentSEOURL(String SEOURL, int AutoKey)
        {
            if (SEOURL.IsNullOrEmpty())
                return Json(true, JsonRequestBehavior.AllowGet);
            ContentBLL bll = new ContentBLL();
            return Json(!bll.IsExistsContentSEOURL(SEOURL, AutoKey), JsonRequestBehavior.AllowGet);
        }

        [UserSystemAuthorize]
        public JsonResult VerificationSpecialColumnSEOURL(String SEOURL, int AutoKey)
        {
            if (SEOURL.IsNullOrEmpty())
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            SpecialColumnBLL bll = new SpecialColumnBLL();
            SpecialColumnTB tb = bll.GetSpecialColumn(SEOURL);
            return Json(!(tb != null && tb.AutoKey != AutoKey), JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerificationFriendLinkName(String LinkName, int AutoKey)
        {
            FriendLinkBLL bll = new FriendLinkBLL();
            FriendLinkTB tb = bll.GetFriendLinkByName(LinkName);
            return Json(!(tb != null && tb.AutoKey != AutoKey), JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult VerificationFriendLinkUrl(String LinkUrl, int AutoKey)
        {
            FriendLinkBLL bll = new FriendLinkBLL();
            FriendLinkTB tb = bll.GetFriendLinkByUrl(LinkUrl);
            return Json(!(tb != null && tb.AutoKey != AutoKey), JsonRequestBehavior.AllowGet);
        }

    }
}
