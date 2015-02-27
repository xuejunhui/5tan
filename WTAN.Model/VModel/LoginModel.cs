using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using WTAN.Model.DModel;
using System.Web;

namespace WTAN.Model.VModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "请输入您的用户名.")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "请输入您的密码.")]
        public String PassWord { get; set; }

        public Boolean IsRememberPassword { get; set; }

        public static Account_UsersTB CurrentUser { get {
            return HttpContext.Current.Session["LoginUserInfo"] as Account_UsersTB;
        } }
    }
}
