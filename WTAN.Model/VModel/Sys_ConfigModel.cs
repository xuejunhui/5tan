using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using WTAN.Model.DModel;
using System.Reflection;
using WTAN.CommonUtility;

namespace WTAN.Model.VModel
{
    public class Sys_ConfigModel<T> : VModelBase where T : ConfigXml, new()
    {
        ///<summary>
        ///AutoKey
        ///</summary>
        public int AutoKey { get; set; }

        ///<summary>
        ///Sys_Type
        ///</summary>
        public string Sys_Type { get; set; }

        ///<summary>
        ///Enable
        ///</summary>
        public bool Enable { get; set; }

        ///<summary>
        ///Sys_Value
        ///</summary>
        [Remote("CheckXMLModel", "Ajax")]
        public T Sys_Value { get; set; }

        /// <summary>
        /// 将数据绑定到视图模型
        /// </summary>
        /// <param name="data"></param>
        public override void BindModelData(DModelBase data)
        {
            PropertyInfo[] pi = this.GetType().GetProperties();
            foreach (PropertyInfo info in pi)
            {
                if (data.Fields.Where(s => s.Name.Equals(info.Name, StringComparison.OrdinalIgnoreCase)).Count() > 0)
                {
                    if (info.Name.Equals("Sys_Value", StringComparison.OrdinalIgnoreCase))
                    {
                        if (data[info.Name].ToEmptyTrimString().Length == 0)
                            info.SetValue(this, new T(), null);
                        else
                            info.SetValue(this, data[info.Name].ToEmptyTrimString().XmlDeserialize<T>(System.Text.Encoding.UTF8), null);
                        continue;
                    }
                    this.SetValue(data[info.Name], info);
                }
            }
        }

        /// <summary>
        /// 将视图模型中数据绑定到数据模型
        /// </summary>
        /// <param name="data"></param>
        public override void BindToDataModel(DModelBase data)
        {
            PropertyInfo[] pi = this.GetType().GetProperties();
            foreach (PropertyInfo info in pi)
            {
                if (data.Fields.Where(s => s.Name.Equals(info.Name, StringComparison.OrdinalIgnoreCase)).Count() > 0)
                {
                    if (info.Name.Equals("Sys_Value", StringComparison.OrdinalIgnoreCase))
                    {
                        data[info.Name] = this.Sys_Value.XmlSerialize(System.Text.Encoding.UTF8).Replace("encoding=\"utf-8\"", "");
                        continue;
                    }
                    data[info.Name] = info.GetValue(this, null);
                }
            }
        }
    }
}
