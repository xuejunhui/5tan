using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace WTAN.BLL
{
    public class UserSystemAuthorize : AuthorizeAttribute
    {
        /// <summary>
        /// 是否啟用登陸驗證
        /// </summary>
        private Boolean _IsStart = true;

        public Boolean IsStart {
            get {
                return _IsStart;
            }
            set
            {
                _IsStart = value;
            }
        }
        public UserSystemAuthorize()
            : base()
        {
        }

        /// <summary>
        /// 验证用户登录
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            if (UsersBLL.IsLoginSuccess || !_IsStart)//登陸成功
            {
               //權限驗證
            }
            else
            {
                // 导航到登录页面
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}
