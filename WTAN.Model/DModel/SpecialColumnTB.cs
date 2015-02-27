using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.CommonUtility;

namespace WTAN.Model.DModel
{
    public class SpecialColumnTB : DModelBase
    {
        public SpecialColumnTB()
            : base("SC_Name", "SEOURL", "KeyWord", "Description", "SC_Content", "ViewModel", "GUID", "ParentID", "WebName")
        {

        }

        //public string MappingTableName { get { return "SpecialColumn"; } }

        #region SetFieldValue
        protected override void SetFieldValue(string name, object value)
        {
            object newValue = value;
            switch (name.ToLower())
            {
                case "sc_name":
                    newValue = value.ToValue("string");
                    break;
                case "seourl":
                    newValue = value.ToValue("string");
                    break;
                case "keyword":
                    newValue = value.ToValue("string");
                    break;
                case "description":
                    newValue = value.ToValue("string");
                    break;
                case "sc_content":
                    newValue = value.ToValue("string");
                    break;
                case "viewmodel":
                    newValue = value.ToValue("string");
                    break;
                case "guid":
                    newValue = value.ToValue("string");
                    break;
                case "parentid":
                    newValue = value.ToValue("int");
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

        #region property SC_Name
        ///<summary>
        ///SC_Name
        ///</summary>
        public string SC_Name
        {
            get { return base["SC_Name"].ToEmptyTrimString(); }
            set { base["SC_Name"] = value; }
        }
        #endregion

        #region property SEOURL
        ///<summary>
        ///SEOURL
        ///</summary>
        public string SEOURL
        {
            get { return base["SEOURL"].ToEmptyTrimString(); }
            set { base["SEOURL"] = value; }
        }
        #endregion

        #region property KeyWord
        ///<summary>
        ///KeyWord
        ///</summary>
        public string KeyWord
        {
            get { return base["KeyWord"].ToEmptyTrimString(); }
            set { base["KeyWord"] = value; }
        }
        #endregion

        #region property Description
        ///<summary>
        ///Description
        ///</summary>
        public string Description
        {
            get { return base["Description"].ToEmptyTrimString(); }
            set { base["Description"] = value; }
        }
        #endregion

        #region property SC_Content
        ///<summary>
        ///SC_Content
        ///</summary>
        public string SC_Content
        {
            get { return base["SC_Content"].ToEmptyTrimString(); }
            set { base["SC_Content"] = value; }
        }
        #endregion

        #region property ViewModel
        ///<summary>
        ///專題的顯示視圖模型
        ///</summary>
        public string ViewModel
        {
            get { return base["ViewModel"].ToEmptyTrimString(); }
            set { base["ViewModel"] = value; }
        }
        #endregion

        #region property GUID
        ///<summary>
        ///關聯標識
        ///</summary>
        public String GUID
        {
            get { return (String)base["GUID"]; }
            set { base["GUID"] = value; }
        }
        #endregion

        #region property ParentID
        ///<summary>
        ///ParentID
        ///</summary>
        public int ParentID
        {
            get { return base["ParentID"].ToInt32Value(); }
            set { base["ParentID"] = value; }
        }
        #endregion
    }
}
