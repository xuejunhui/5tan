using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.CommonUtility;

namespace WTAN.Model.DModel
{
    //Category
    public class CategoryTB : DModelBase
    {
        public CategoryTB()
            : base("CategoryName", "KeyWord", "Description", "SEOURL", "ParentID", "WebName")
        {

        }


        #region SetFieldValue
        protected override void SetFieldValue(string name, object value)
        {
            object newValue = value;
            switch (name.ToLower())
            {
                case "categoryname":
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

        #region property CategoryName
        ///<summary>
        ///分類名稱
        ///</summary>
        public string CategoryName
        {
            get { return base["CategoryName"].ToEmptyTrimString(); }
            set { base["CategoryName"] = value; }
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
        ///分类描述
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

        #region property ParentID
        ///<summary>
        ///父節點ID
        ///</summary>
        public int ParentID
        {
            get { return base["ParentID"].ToInt32Value(); }
            set { base["ParentID"] = value; }
        }
        #endregion


    }
}
