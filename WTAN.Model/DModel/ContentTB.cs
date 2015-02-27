using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.CommonUtility;

namespace WTAN.Model.DModel
{
    public class ContentTB : DModelBase
    {
        public ContentTB()
            : base("Title", "Shorttitle", "KeyWord", "Description", "SEOURL", "TravelingDays", "Transport", "OfferedType", "CategoryID", "MinSignUp", "Features", "Price", "PreviewIMG", "XMLContent", "GUID", "CreateTime", "WebName")
        {

        }

        //public string MappingTableName { get { return "Content"; } }

        #region SetFieldValue
        protected override void SetFieldValue(string name, object value)
        {
            object newValue = value;
            switch (name.ToLower())
            {
                case "title":
                    newValue = value.ToValue("string");
                    break;
                case "shorttitle":
                    newValue = value.ToValue("string");
                    break;
                case "keyword":
                    newValue = value.ToValue("string");
                    break;
                case "description":
                    newValue = value.ToValue("string");
                    break;
                case "seourl":
                    newValue = value.ToValue("string");
                    break;
                case "travelingdays":
                    newValue = value.ToValue("string");
                    break;
                case "transport":
                    newValue = value.ToValue("string");
                    break;
                case "offeredtype":
                    newValue = value.ToValue("string");
                    break;
                case "categoryid":
                    newValue = value.ToValue("int");
                    break;
                case "minsignup":
                    newValue = value.ToValue("int");
                    break;
                case "features":
                    newValue = value.ToValue("string");
                    break;
                case "price":
                    newValue = value.ToValue("decimal");
                    break;
                case "previewimg":
                    newValue = value.ToValue("string");
                    break;
                case "xmlcontent":
                    newValue = value.ToValue("string");
                    break;
                case "guid":
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

        #region property Title
        ///<summary>
        ///Title
        ///</summary>
        public string Title
        {
            get { return base["Title"].ToEmptyTrimString(); }
            set { base["Title"] = value; }
        }
        #endregion

        #region property Shorttitle
        ///<summary>
        ///短標題
        ///</summary>
        public string Shorttitle
        {
            get { return base["Shorttitle"].ToEmptyTrimString(); }
            set { base["Shorttitle"] = value; }
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

        #region property TravelingDays
        ///<summary>
        ///旅游天数
        ///</summary>
        public string TravelingDays
        {
            get { return base["TravelingDays"].ToEmptyTrimString(); }
            set { base["TravelingDays"] = value; }
        }
        #endregion

        #region property Transport
        ///<summary>
        ///交通工具
        ///</summary>
        public string Transport
        {
            get { return base["Transport"].ToEmptyTrimString(); }
            set { base["Transport"] = value; }
        }
        #endregion

        #region property OfferedType
        ///<summary>
        ///OfferedType
        ///</summary>
        public string OfferedType
        {
            get { return base["OfferedType"].ToEmptyTrimString(); }
            set { base["OfferedType"] = value; }
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

        #region property MinSignUp
        ///<summary>
        ///提前报名数
        ///</summary>
        public int MinSignUp
        {
            get { return base["MinSignUp"].ToInt32Value(); }
            set { base["MinSignUp"] = value; }
        }
        #endregion

        #region property Features
        ///<summary>
        ///线路特色
        ///</summary>
        public string Features
        {
            get { return base["Features"].ToEmptyTrimString(); }
            set { base["Features"] = value; }
        }
        #endregion

        #region property Price
        ///<summary>
        ///Price
        ///</summary>
        public decimal Price
        {
            get { return base["Price"].ToDecimal(); }
            set { base["Price"] = value; }
        }
        #endregion

        #region property PreviewIMG
        ///<summary>
        ///预览图片
        ///</summary>
        public string PreviewIMG
        {
            get { return base["PreviewIMG"].ToEmptyTrimString(); }
            set { base["PreviewIMG"] = value; }
        }
        #endregion

        #region property XMLContent
        ///<summary>
        ///网页内容（用xml保存方便以后拓展）
        ///</summary>
        public String XMLContent
        {
            get { return (String)base["XMLContent"]; }
            set { base["XMLContent"] = value; }
        }
        #endregion

        #region property GUID
        ///<summary>
        ///標識
        ///</summary>
        public String GUID
        {
            get { return (String)base["GUID"]; }
            set { base["GUID"] = value; }
        }
        #endregion

        #region property CreateTime
        ///<summary>
        ///文件上傳時間
        ///</summary>
        public DateTime CreateTime
        {
            get { return base["CreateTime"].ToDateTimeValue(); }
            set { base["CreateTime"] = value; }
        }
        #endregion
    }
}
