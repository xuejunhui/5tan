using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;
using System.Reflection;

namespace WTAN.Model.VModel
{
    public class VModelBase
    {
        /// <summary>
        /// 向页面输出提示
        /// </summary>
        public Message Msg { get; set; }

         /// <summary>
        /// 将数据模型中的数据绑定到视图模型中
        /// </summary>
        /// <param name="data"></param>
        public virtual void BindModelData(DModelBase data)
        {
            if (data == null)
                return;
            PropertyInfo[] pi = this.GetType().GetProperties();
            foreach (PropertyInfo info in pi)
            {
                if (data.Fields.Where(s => s.Name.Equals(info.Name, StringComparison.OrdinalIgnoreCase)).Count() > 0)
                {
                    this.SetValue(data[info.Name], info);
                }
            }
        }

        /// <summary>
        /// 将视图模型中的数据绑定到数据模型中
        /// </summary>
        /// <param name="data"></param>
        public virtual void BindToDataModel(DModelBase data)
        {
            if (data == null)
                return;
            PropertyInfo[] pi = this.GetType().GetProperties();
            foreach (PropertyInfo info in pi)
            {
                if (data.Fields.Where(s => s.Name.Equals(info.Name, StringComparison.OrdinalIgnoreCase)).Count() > 0)
                {
                    data[info.Name] = info.GetValue(this, null);
                }
            }
        }

        /// <summary>
        /// 给属性赋值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="info"></param>
        public void SetValue(Object value, PropertyInfo info)
        {
            
            if (value == null || value.ToString().Length == 0)
            {
                switch (info.PropertyType.Name.ToLower())
                {
                    case "int32": info.SetValue(this, 0, null); break;
                    case "string": info.SetValue(this, "", null); break;
                    case "boolean": info.SetValue(this, false, null); break;
                    case "double": info.SetValue(this, 0, null); break;
                }
                return;
            }

            switch (info.PropertyType.Name.ToLower())
            {
                case "int32":
                    try
                    {
                        info.SetValue(this, Convert.ToInt32(value), null);
                    }
                    catch
                    {
                        info.SetValue(this, 0, null);
                    }
                    break;
                case "string":
                    info.SetValue(this, Convert.ToString(value), null);
                    break;
                case "datetime":
                    try
                    {
                        info.SetValue(this, Convert.ToDateTime(value), null);
                    }
                    catch
                    {
                        return;
                    }
                    break;
                case "boolean":
                    try
                    {
                        info.SetValue(this, Convert.ToBoolean(value), null);
                    }
                    catch
                    {
                        info.SetValue(this, false, null);
                    }
                    break;
                case "double":
                    try
                    {
                        info.SetValue(this, Convert.ToDouble(value), null);
                    }
                    catch
                    {
                        info.SetValue(this, 0, null);
                    }
                    break;
                case "decimal":
                    try
                    {
                        info.SetValue(this, Convert.ToDecimal(value), null);
                    }
                    catch
                    {
                        info.SetValue(this, 0, null);
                    }
                    break;
            }
        }
    }
}
