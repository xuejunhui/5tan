using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.CommonUtility;

namespace WTAN.Model.DModel
{
    /// <summary>
    /// 數據處理模型基類
    /// </summary>
    public class DModelBase : RecordItem
    {
        public DModelBase(params string[] names)
            : base(names.Union(new string[] { "AutoKey", "Enable", "RecordVersion" }).ToArray())
        {
            Enable = true;
            AutoKey = 0;
            RecordVersion = 0;
        }

        #region SetFieldValue
        protected override void SetFieldValue(string name, object value)
        {
            object newValue = null;
            switch (name.ToLower())
            {
                case "autokey":
                    newValue = value.ToValue("int");
                    break;
                case "enable":
                    newValue = value.ToBoolValue();
                    break;
                case "recordversion":
                    newValue = value.ToInt64Value();
                    break;
                default:
                    newValue = value;
                    break;
            }
            base.SetFieldValue(name, newValue);
        }
        #endregion

        #region property Enable
        public bool Enable
        {
            get
            {
                return (bool)base["Enable"];
            }
            set
            {
                base["Enable"] = value;
            }
        }
        #endregion //property Enable

        #region property AutoKey
        public int AutoKey
        {
            get
            {
                return (int)base["AutoKey"];
            }
            set
            {
                base["AutoKey"] = value;
            }
        }
        #endregion //property AutoKey

        #region property RecordVersion
        public long RecordVersion
        {
            get
            {
                return (long)base["RecordVersion"];
            }
            set
            {
                base["RecordVersion"] = value;
            }
        }
        #endregion //property AutoKey
    }
}
