using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.CommonUtility;

namespace WTAN.Model.DModel
{
    public class VisaCenterTB : DModelBase
    {
        public VisaCenterTB()
            : base("KeyWord", "Description", "CategoryID", "GUID", "Price", "VName", "VContent", "CreateTime", "VType", "MainTop")
        {

        }

        //public string MappingTableName { get { return "VisaCenter"; } }

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
                case "categoryid":
                    newValue = value.ToValue("int");
                    break;
                case "guid":
                    newValue = value.ToValue("string");
                    break;
                case "price":
                    newValue = value.ToValue("decimal");
                    break;
                case "vname":
                    newValue = value.ToValue("string");
                    break;
                case "vcontent":
                    newValue = value.ToValue("string");
                    break;
                case "createtime":
                    newValue = value.ToValue("datetime");
                    break;
                case "maintop":
                    newValue = value.ToValue("int");
                    break;
                case "vtype":
                    newValue = value.ToValue("string");
                    break;
                default:
                    newValue = value;
                    break;
            }
            base.SetFieldValue(name, newValue);
        }
        #endregion

        #region property MainTop
        ///<summary>
        ///CategoryID
        ///</summary>
        public int MainTop
        {
            get { return base["MainTop"].ToInt32Value(); }
            set { base["MainTop"] = value; }
        }
        #endregion

        #region property VType
        ///<summary>
        ///GUID
        ///</summary>
        public String VType
        {
            get { return (String)base["VType"]; }
            set { base["VType"] = value; }
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
        ///GUID
        ///</summary>
        public String GUID
        {
            get { return (String)base["GUID"]; }
            set { base["GUID"] = value; }
        }
        #endregion

        #region property Price
        ///<summary>
        ///Price
        ///</summary>
        public decimal Price
        {
            get { return (decimal)base["Price"]; }
            set { base["Price"] = value; }
        }
        #endregion

        #region property VName
        ///<summary>
        ///VName
        ///</summary>
        public string VName
        {
            get { return base["VName"].ToEmptyTrimString(); }
            set { base["VName"] = value; }
        }
        #endregion

        #region property VContent
        ///<summary>
        ///VContent
        ///</summary>
        public string VContent
        {
            get { return base["VContent"].ToEmptyTrimString(); }
            set { base["VContent"] = value; }
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
