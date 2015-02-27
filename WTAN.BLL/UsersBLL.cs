using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.IDAL;
using WTAN.Model.DModel;
using WTAN.CommonUtility;
using System.Web;
using WTAN.Model.VModel;

namespace WTAN.BLL
{
    public class UsersBLL
    {
        IAccount_Users UserDB = DALFactory.DataAccess.CreateAccount_Users();

        /// <summary>
        /// 用戶是否已經登陸成功
        /// </summary>
        public static Boolean IsLoginSuccess {
            get {
                return HttpContext.Current.Session["UserID"].ToInt32Value() > 0;
            }
        }

        /// <summary>
        /// 用戶登陸
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public LoginState Login(String userName, String passWord)
        {
            Account_UsersTB user = UserDB.GetAccountUserInfo(userName);

            if (user == null || user.AutoKey == 0)
                return LoginState.UserNameErr;
            else if (!user.Enable)
                return LoginState.Lock;
            else if (user.PassWord != passWord.ToMD5())
                return LoginState.PassWordErr;

            this.SaveToSession(user);
            return LoginState.Success;
        }

        /// <summary>
        /// 将用户信息存储到session中
        /// </summary>
        /// <param name="user"></param>
        private void SaveToSession(Account_UsersTB user)
        {
            HttpContext.Current.Session.Timeout = 30;
            HttpContext.Current.Session["LoginUserInfo"] = user;
            HttpContext.Current.Session["UserID"] = user.AutoKey;
        }

        public static  void ClearSession()
        {
            HttpContext.Current.Session.Clear();
        }

        /// <summary>
        /// 獲取用戶信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public Account_UsersTB GetAccountUserInfo(String userName, String passWord)
        {
            return UserDB.GetAccountUserInfo(userName, passWord.ToMD5());
        }

        public Boolean ChangePassword(ChangePasswordModel cp, int autokey)
        {
            return UserDB.ChangePassword(cp, autokey);
        }
    }
}
