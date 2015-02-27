using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.IDAL;
using WTAN.Model.DModel;
using WTAN.Model.VModel;
using WTAN.CommonUtility;

namespace WTAN.SQLServerDAL
{
    public class AccountServer : IAccount_Users
    {
        public Boolean ChangePassword(ChangePasswordModel cp,int autokey)
        {
            return CurrentDataServer.ExecuteNoneQuery("update Account_Users set PassWord=@PassWord where PassWord=@OldPassWord and Autokey=@Autokey",
                "PassWord", cp.Password.ToMD5(),
                "OldPassWord", cp.OldPassword.ToMD5(),
                "Autokey", autokey.ToString()) > 0;
        }

        public Account_UsersTB GetAccountUserInfo(String userName, String passWord)
        {
            return CurrentDataServer.GetInfo<Account_UsersTB>("*", 
                "userName=@userName and passWord=@passWord",
                "userName", userName,
                "passWord", passWord);
        }

        public Account_UsersTB GetAccountUserInfo(String userName)
        {
            return CurrentDataServer.GetInfo<Account_UsersTB>("*",
                "userName=@userName",
                "userName", userName);
        }
    }
}
