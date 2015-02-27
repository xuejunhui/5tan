using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WTAN.Model.VModel
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "请输入原始密码.")]
        public String OldPassword { get; set; }

        [Required(ErrorMessage = "请输入新密码.")]
        public String Password { get; set; }

        [Compare("Password", ErrorMessage = "两次输入的密码不一致.")]
        public String ConfirmPassword { get; set; }

        public Message Msg { get; set; }
    }
}
