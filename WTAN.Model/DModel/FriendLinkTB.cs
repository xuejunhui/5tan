using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.CommonUtility;

namespace WTAN.Model.DModel
{
    public class FriendLinkTB : DModelBase
    {
        public FriendLinkTB()
            : base("LinkName", "LinkUrl", "Note", "CreateTime", "WebName")
        {

        }

        #region SetFieldValue
        protected override void SetFieldValue(string name, object value)
        {
            object newValue = value;
            switch (name.ToLower())
            {
                case "linkname":
                    newValue = value.ToValue("string");
                    break;
                case "linkurl":
                    newValue = value.ToValue("string");
                    break;
                case "note":
                    newValue = value.ToValue("string");
                    break;
                case "createtime":
                    newValue = value.ToValue("datetime");
                    break;
                case "webname":
                    newValue = value.ToValue("string");
                    break;
                default:
                    newValue = value;
                    break;
            }
            base.SetFieldValue(name, newValue);
        }
        #endregion

        #region property WebName
        ///<summary>
        ///WebName
        ///</summary>
        public WebName WebName
        {
            get { return base["WebName"].ToEmptyTrimString().ToWebName(); }
            set { base["WebName"] = value; }
        }
        #endregion

        #region property LinkName
        ///<summary>
        ///LinkName
        ///</summary>
        public string LinkName
        {
            get { return base["LinkName"].ToEmptyTrimString(); }
            set { base["LinkName"] = value; }
        }
        #endregion

        #region property LinkUrl
        ///<summary>
        ///LinkUrl
        ///</summary>
        public string LinkUrl
        {
            get { return base["LinkUrl"].ToEmptyTrimString(); }
            set { base["LinkUrl"] = value; }
        }
        #endregion

        #region property Note
        ///<summary>
        ///Note
        ///</summary>
        public string Note
        {
            get { return base["Note"].ToEmptyTrimString(); }
            set { base["Note"] = value; }
        }
        #endregion

        #region property CreateTime
        ///<summary>
        ///CreateTime
        ///</summary>
        public DateTime CreateTime
        {
            get { return base["CreateTime"].ToDateTimeValue(); }
            set { base["CreateTime"] = value; }
        }
        #endregion
    }
}
