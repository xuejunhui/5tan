using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.CommonUtility;

namespace WTAN.Model.DModel
{
    public class Sys_ConfigTB : DModelBase
    {
        public Sys_ConfigTB()
            : base("Sys_Type", "Sys_Value","WebName")
        {

        }

        #region SetFieldValue
        protected override void SetFieldValue(string name, object value)
        {
            object newValue = value;
            switch (name.ToLower())
            {
                case "sys_type":
                    newValue = value.ToValue("string");
                    break;
                case "sys_value":
                    newValue = value.ToValue("xml");
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

        #region property Sys_Type
        ///<summary>
        ///Sys_Type
        ///</summary>
        public string Sys_Type
        {
            get { return base["Sys_Type"].ToEmptyTrimString(); }
            set { base["Sys_Type"] = value; }
        }
        #endregion

        #region property Sys_Value
        ///<summary>
        ///Sys_Value
        ///</summary>
        public String Sys_Value
        {
            get { return (String)base["Sys_Value"]; }
            set { base["Sys_Value"] = value; }
        }
        #endregion
    }
}
