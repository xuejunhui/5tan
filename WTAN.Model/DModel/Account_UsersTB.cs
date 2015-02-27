using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.CommonUtility;

namespace WTAN.Model.DModel
{
    public class Account_UsersTB : DModelBase
    {
        public Account_UsersTB()
            : base("UserName", "PassWord", "Email", "RoleID")
        {

        }

        #region SetFieldValue
        protected override void SetFieldValue(string name, object value)
        {
            object newValue = value;
            switch (name.ToLower())
            {
                case "username":
                    newValue = value.ToValue("string");
                    break;
                case "password":
                    newValue = value.ToValue("string");
                    break;
                case "email":
                    newValue = value.ToValue("string");
                    break;
                case "roleid":
                    newValue = value.ToValue("int");
                    break;
                default:
                    newValue = value;
                    break;
            }
            base.SetFieldValue(name, newValue);
        }
        #endregion

        #region property UserName
        ///<summary>
        ///UserName
        ///</summary>
        public string UserName
        {
            get { return base["UserName"].ToEmptyTrimString(); }
            set { base["UserName"] = value; }
        }
        #endregion

        #region property PassWord
        ///<summary>
        ///PassWord
        ///</summary>
        public string PassWord
        {
            get { return base["PassWord"].ToEmptyTrimString(); }
            set { base["PassWord"] = value; }
        }
        #endregion

        #region property Email
        ///<summary>
        ///Email
        ///</summary>
        public string Email
        {
            get { return base["Email"].ToEmptyTrimString(); }
            set { base["Email"] = value; }
        }
        #endregion

        #region property RoleID
        ///<summary>
        ///RoleID
        ///</summary>
        public int RoleID
        {
            get { return base["RoleID"].ToInt32Value(); }
            set { base["RoleID"] = value; }
        }
        #endregion
    }
}
