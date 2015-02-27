using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WTAN.BLL;
using WTAN.CommonUtility;
using WTAN.Model.VModel;
using WTAN.Model.DModel;
using System.Text.RegularExpressions;

namespace Demo.Controllers
{
    [UserSystemAuthorize]
    public class ManageController : Controller
    {
        public ActionResult SignOut()
        {
            UploadHelper.DeleteEmptyFolder();
            UsersBLL.ClearSession();
            return Redirect("/Login");
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (LoginModel.CurrentUser.PassWord.Equals(model.OldPassword.ToMD5()))
                {
                    UsersBLL bll = new UsersBLL();
                    if (bll.ChangePassword(model, LoginModel.CurrentUser.AutoKey))
                    {
                        LoginModel.CurrentUser.PassWord = model.Password.ToMD5();
                        model.Msg = new Message(MessageState.success, "您已成功修改了密码.");
                    }
                    else
                    {
                        model.Msg = new Message(MessageState.error, "密码修改失败,请重试.");
                    }
                }
                else
                {
                    model.Msg = new Message(MessageState.error, "原始密码错误.");
                }
            }
            return View(model);
        }

        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordModel());
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 在線天氣 用js去直接請求愣是失敗 先這樣頂著用
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public String Weather(String city = "无锡")
        {
            WebRequestHelp request = new WebRequestHelp();
            return request.GetHttpRequestStringByNUll_Get("http://api.map.baidu.com/telematics/v3/weather?location=" + city + "&output=json&ak=198E22C56f2796c7c57aae686b61feec", System.Text.Encoding.UTF8);
        }

        public ActionResult FunIndex(int id = 0)
        {
            MenusBLL bll = new MenusBLL();
            if (id == 0)
                return View(bll.GetAdminMenus().MenuItems);
            return View(bll.GetAdminMenus().MenuItems.Where(s => s.ID == id).ToList());
        }

        

        #region 系統分類
        public ActionResult EditCategory(int parentid,int autokey,int menuid)
        {
            CategoryBLL bll = new CategoryBLL();
            CategoryModel model = new CategoryModel()
            {
                ParentID = parentid,
                AutoKey = autokey
            };
            if (autokey > 0)
            {
                CategoryTB c = bll.GetCategory(autokey);
                model.BindModelData(c);
            }
            MenusBLL mbll = new MenusBLL();
            ViewBag.Menu = mbll.GetMenuInfo(menuid);
            ViewBag.Nav = bll.GetCategoryNav(parentid);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditCategory(int menuid, CategoryModel model)
        {
            CategoryBLL bll = new CategoryBLL();
            MenusBLL mbll = new MenusBLL();
            if (ModelState.IsValid)
            {
                if (bll.SaveCategory(model) > 0)
                {
                    if (model.ParentID == 0)
                        return Redirect("Category/" + menuid);
                    else
                        return Redirect(String.Format("CategoryListByParentID?parentid={0}&id={1}", model.ParentID, menuid));
                }
                else
                    model.Msg = new Message(MessageState.error, "分类信息保存失败.");
            }
            ViewBag.Menu = mbll.GetMenuInfo(menuid);
            ViewBag.Nav = bll.GetCategoryNav(model.ParentID);
            return View(model);
        }

        public ActionResult Category(int id)
        {
            CategoryBLL bll = new CategoryBLL();
            CategoryViewData model = bll.GetCategoryListViewData(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult CategoryListByParentID(int parentid, int id, int pageindex = 1, String Keyword = "")
        {
            CategoryBLL bll = new CategoryBLL();
            String formarturl = "/manage/CategoryListByParentID?pageindex={0}&id=" + id + "&parentid=" + parentid;
            if (!Keyword.IsNullOrEmpty())
                formarturl += "&keyword=" + Keyword;
            CategoryViewData model = bll.GetCategoryListViewData(parentid, pageindex, 15, id, formarturl, Keyword);
            model.CurrentCategoryID = parentid;
            model.Keyword = Keyword;
            return View(model);
        }

        /// <summary>
        /// 国内城市更新
        /// </summary>
        public void UpdateDomesticCategory()
        {
            WebRequestHelp request = new WebRequestHelp();
            String c = request.GetHttpRequestStringByNUll_Get("http://webservice.webxml.com.cn/WebServices/WeatherWS.asmx/getRegionProvince", System.Text.Encoding.UTF8);
            String rex = "<string>([\\d\\D]*?)</string>";
            MatchCollection mh = Regex.Matches(c, rex, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            CategoryBLL bll = new CategoryBLL();

            foreach (Match m in mh)//遍历所有匹配项
            {
                String cname = m.Groups[1].Value.Split(',')[0];
                String code = m.Groups[1].Value.Split(',')[1];
                int parent = 1;
                CategoryTB ca = bll.GetCategory(cname);
                if (ca == null || ca.AutoKey == 0)
                {
                    CategoryModel cmodel = new CategoryModel()
                    {
                        CategoryName = cname,
                        Enable = true,
                        ParentID = parent,
                        SEOURL = code,
                        Description = "",
                        KeyWord = ""
                    };
                    parent = bll.SaveCategory(cmodel);
                }
                else
                {
                    parent = ca.AutoKey;
                }
                c = request.GetHttpRequestStringByNUll_Get("http://webservice.webxml.com.cn/WebServices/WeatherWS.asmx/getSupportCityString?theRegionCode=" + code, System.Text.Encoding.UTF8);
                MatchCollection mmh = Regex.Matches(c, rex, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                foreach (Match mm in mmh)
                {
                    cname = mm.Groups[1].Value.Split(',')[0];
                    code = mm.Groups[1].Value.Split(',')[1];
                    ca = bll.GetCategory(cname);
                    if (ca == null || ca.AutoKey == 0)
                    {
                        CategoryModel cmodel = new CategoryModel()
                        {
                            CategoryName = cname,
                            Enable = true,
                            ParentID = parent,
                            SEOURL = code,
                            Description = "",
                            KeyWord = ""
                        };
                        bll.SaveCategory(cmodel);
                    }
                }
            }
        }
        #endregion

        #region 系統配置
        /// <summary>
        /// 廣告
        /// </summary>
        /// <returns></returns>
        public ActionResult Ads()
        {
            return View();
        }

        public ActionResult AdsNetWork()
        {
            return View();
        }

        /// <summary>
        /// 系統基本配置
        /// </summary>
        /// <returns></returns>
        public ActionResult SysConfig(WebName id = WebName.Demo)
        {
            ConfigBLL bll = new ConfigBLL();
            SysConfigModel model = bll.GetSysConfigInfo(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult SysConfig(SysConfigModel model)
        {
            if (ModelState.IsValid)
            {
                ConfigBLL bll = new ConfigBLL();

                if (bll.SaveSysConfigInfo(model))
                    model.Msg = new Message(MessageState.success, "网站基本配置保存完成.");
                else
                    model.Msg = new Message(MessageState.error, "网站基本配置保存失败,请重新尝试.");
            }
            return View(model);
        }

        public ActionResult AdsManage(String id, WebName webname)
        {
            ConfigBLL bll = new ConfigBLL();
            List<Sys_ConfigTB> model = bll.GetAdsConfigs(id, webname);
            ViewBag.PosName = id.ToAdsPos().GetAdsPosName();
            ViewBag.AdsPos = id;
            ViewBag.WebName = webname;
            return View(model);
        }

        public ActionResult EditAds(int autokey, String adspos, WebName webname)
        {
            AdsConfigModel model = null;
            if (autokey == 0)
            {
                model = new AdsConfigModel(adspos, adspos.GetAdsType());
                model.WebName = webname;
            }
            else
            {
                ConfigBLL bll = new ConfigBLL();
                model = bll.GetAdsConfigInfo(adspos, adspos.GetAdsType(), autokey);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditAds(AdsConfigModel model)
        {
            if (ModelState.IsValid)
            {
                ConfigBLL bll = new ConfigBLL();
                if (model.Sys_Value.AdsType == AdsType.Image || model.Sys_Value.AdsType == AdsType.Slide)
                {
                    if (Request.Files["upImg"].FileName.IsNullOrEmpty())
                    {
                        if (model.AutoKey == 0)
                        {
                            model.Msg = new Message(MessageState.error, "您还未选择上传的图片");
                            return View(model);
                        }
                    }
                    else
                    {
                        String saveName = String.Format("{0}{1:yyyyMMDDHHmmss}{2}", model.Sys_Type.ToString(), DateTime.Now, UploadHelper.GetFileExtension(Request.Files["upImg"].FileName));
                        UploadInfo upinfo = Request.Files["upImg"].SaveAs(AppSettings.AdsUploadDir, saveName, AppSettings.UplaodImgType, AppSettings.UplaodImgSize, true, 200, 100);
                        if (upinfo.State == "SUCCESS")
                        {
                            if (!model.Sys_Value.LinkContent.IsNullOrEmpty())
                                model.Sys_Value.LinkContent.DelFile();//删除之前文件
                            model.Sys_Value.LinkContent = upinfo.URL;
                        }
                        else
                        {
                            model.Msg = new Message(MessageState.error, upinfo.State);
                            return View(model);
                        }
                    }
                }
                if (bll.SaveSysConfigInfo(model))
                {
                    if (model.AutoKey == 0)
                    {
                        return Redirect("/Manage/AdsManage/" + model.Sys_Type + "?webname=" + model.WebName.ToString());
                    }
                    model.Msg = new Message(MessageState.success, model.Sys_Type.ToAdsPos().GetAdsPosName() + "保存完成.");
                }
                else
                    model.Msg = new Message(MessageState.error, model.Sys_Type.ToAdsPos().GetAdsPosName() + "保存失败,请重新尝试.");
            }
            return View(model);
        }

        
        #endregion
    }
}
