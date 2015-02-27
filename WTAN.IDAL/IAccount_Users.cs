using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;
using WTAN.Model.VModel;

namespace WTAN.IDAL
{
    public interface IAccount_Users
    {
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="autokey"></param>
        /// <returns></returns>
        Boolean ChangePassword(ChangePasswordModel cp, int autokey);

        /// <summary>
        /// 獲取用戶信息
        /// </summary>
        /// <param name="userName">用戶名</param>
        /// <param name="passWord">MD5加密后密碼</param>
        /// <returns>Account_UsersTB</returns>
        Account_UsersTB GetAccountUserInfo(String userName, String passWord);

        /// <summary>
        /// 獲取用戶信息
        /// </summary>
        /// <param name="userName">用戶名</param>
        /// <returns>Account_UsersTB</returns>
        Account_UsersTB GetAccountUserInfo(String userName);
    }
}
