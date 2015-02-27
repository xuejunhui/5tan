using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WTAN.Model.VModel;
using WTAN.BLL;
using WTAN.CommonUtility;

namespace Demo.Controllers
{
    public class AccountController : Controller
    {
        [UserSystemAuthorize(IsStart = false)]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [UserSystemAuthorize(IsStart = false)]
        public ActionResult Login(LoginModel model, String ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                UsersBLL bll = new UsersBLL();
                LoginState result = bll.Login(model.UserName, model.PassWord);
                if (result == LoginState.Success)//登陆成功
                {
                    #region 登陆后跳转到登陆前的地址
                    //是否为本网站的url
                    //Boolean isRightUrl = Url.IsLocalUrl(ReturnUrl) && ReturnUrl.Length > 1 && ReturnUrl.StartsWith("/") && !ReturnUrl.StartsWith("//") && !ReturnUrl.StartsWith("/\\");
                    //if (isRightUrl)
                    //{
                    //    return Redirect(ReturnUrl);
                    //}
                    //else
                    //{
                    //    return Redirect(URLUtility.AdminUrl);
                    //}
                    #endregion
                    return Redirect(URLUtility.AdminUrl);
                }
                //登录失败
                switch (result)
                {
                    case LoginState.UserNameErr:
                        ModelState.AddModelError("UserName", "您输入的用户名不存在.");
                        break;
                    case LoginState.PassWordErr:
                        ModelState.AddModelError("UserName", "您输入的密码错误.");
                        break;
                    case LoginState.Lock:
                        ModelState.AddModelError("UserName", "您的账号已经被锁,请联系管理员.");
                        break;
                }

            }
            return View(model);
        }

        

        [UserSystemAuthorize]
        public ActionResult Main()
        {
            return View();
        }

        [UserSystemAuthorize]
        public ActionResult Head()
        {
            return View();
        }

        [UserSystemAuthorize]
        public ActionResult Left(int id = 0)
        {
            MenusBLL bll = new MenusBLL();
            if (id == 0)
                return View(bll.GetAdminMenus().MenuItems);
            return View(bll.GetAdminMenus().MenuItems.Where(s => s.ID == id).ToList());
        }

        
    }
}
