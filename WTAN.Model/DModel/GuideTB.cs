using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.CommonUtility;

namespace WTAN.Model.DModel
{
    //Guide
    public class GuideTB : DModelBase
    {
        public GuideTB()
            : base("KeyWord", "Description", "RelatedGUID", "GuideName", "GuideContent", "CreateTime", "GUID", "CategoryID", "WebName")
        {

        }

        //public string MappingTableName { get { return "Guide"; } }

        #region SetFieldValue
        protected override void SetFieldValue(string name, object value)
        {
            object newValue = value;
            switch (name.ToLower())
            {
                case "keyword":
                    newValue = value.ToValue("string");
                    break;
                case "description":
                    newValue = value.ToValue("string");
                    break;
                case "relatedguid":
                    newValue = value.ToValue("string");
                    break;
                case "guidename":
                    newValue = value.ToValue("string");
                    break;
                case "guidecontent":
                    newValue = value.ToValue("string");
                    break;
                case "createtime":
                    newValue = value.ToValue("datetime");
                    break;
                case "guid":
                    newValue = value.ToValue("string");
                    break;
                case "categoryid":
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

        #region property RelatedGUID
        ///<summary>
        ///RelatedGUID
        ///</summary>
        public String RelatedGUID
        {
            get { return (String)base["RelatedGUID"]; }
            set { base["RelatedGUID"] = value; }
        }
        #endregion

        #region property CategoryID
        ///<summary>
        ///CategoryID
        ///</summary>
        public int CategoryID
        {
            get { return base["CategoryID"].ToInt32Value(); }
            set { base["CategoryID"] = value; }
        }
        #endregion

        #region property GUID
        ///<summary>
        ///RelatedGUID
        ///</summary>
        public String GUID
        {
            get { return (String)base["GUID"]; }
            set { base["GUID"] = value; }
        }
        #endregion

        #region property GuideName
        ///<summary>
        ///GuideName
        ///</summary>
        public string GuideName
        {
            get { return base["GuideName"].ToEmptyTrimString(); }
            set { base["GuideName"] = value; }
        }
        #endregion

        #region property GuideContent
        ///<summary>
        ///GuideContent
        ///</summary>
        public string GuideContent
        {
            get { return base["GuideContent"].ToEmptyTrimString(); }
            set { base["GuideContent"] = value; }
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
