using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTAN.Model.DModel
{
    /// <summary>
    ///  字段存储信息
    /// </summary>
    public class RecordField
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字段值
        /// </summary>
        public object Value { get; set; }
    }

    /// <summary>
    /// 数据类基本模型
    /// </summary>
    public class RecordItem
    {
        public RecordItem()
        {
        }

        public RecordItem(params String[] fields)
        {
            ResetFields(fields);
        }

        public void ResetFields(params string[] fields)
        {
            if (fields != null)
            {
                foreach (var f in fields)
                {
                    AddField(f);
                }
            }
        }

        #region property Fields
        private Dictionary<String, RecordField> _Fields = new Dictionary<String, RecordField>();
        public IEnumerable<RecordField> Fields
        {
            get
            {
                return _Fields.Values;
            }
        }
        #endregion

        /// <summary>
        /// 获取字段信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public RecordField GetField(String name)
        {
            RecordField f = null;
            _Fields.TryGetValue(name.ToUpper(), out f);
            return f;
        }

        /// <summary>
        /// 添加字段
        /// </summary>
        /// <param name="field"></param>
        public void AddField(RecordField field)
        {
            string uName = field.Name.ToUpper();
            var item = GetField(uName);
            if (item == null)
            {
                _Fields.Add(uName, field);
            }
            else
            {
                item.Value = field.Value;
            }
        }

        public void AddField(String name)
        {
            string uName = name.ToUpper();
            var item = GetField(uName);
            if (item == null)
            {
                _Fields.Add(uName, new RecordField { Name = uName });
            }
        }

        public void AddField(String name, object value)
        {
            SetFieldValue(name, value);
        }

        /// <summary>
        /// 设置字段值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        protected virtual void SetFieldValue(String name, object value)
        {
            String uName = name.ToUpper();
            var item = GetField(uName);
            if (item == null)
            {
                _Fields.Add(uName, new RecordField { Name = uName, Value = value });
            }
            else
            {
                item.Value = value;
            }
        }

        /// <summary>
        /// 索引
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object this[String name]
        {
            get
            {
                var field = GetField(name);
                return field == null ? null : field.Value;
            }
            set
            {
                AddField(name, value);
            }
        }
    }
}
