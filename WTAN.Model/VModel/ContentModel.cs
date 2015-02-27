using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using WTAN.Model.DModel;
using WTAN.CommonUtility;
using System.Xml.Serialization;
using System.Reflection;
using System.Web.Mvc;

namespace WTAN.Model.VModel
{
    public class ContentModel : VModelBase
    {
        public WebName WebName { get; set; }

        /// <summary>
        /// 参团类型分类
        /// </summary>
        public String CategoryDropDownOfferedType { get; set; }

        /// <summary>
        /// 旅游天数分类
        /// </summary>
        public String CategoryDropDownTravelingDays { get; set; }

        /// <summary>
        /// 交通分类
        /// </summary>
        public String CategoryDropDownTraffic { get; set; }

        /// <summary>
        /// 范围分类
        /// </summary>
        public String CategoryDropDownNav { get; set; }

        ///<summary>
        ///AutoKey
        ///</summary>
        public int AutoKey { get; set; }

        ///<summary>
        ///Title
        ///</summary>
        [Required(ErrorMessage = "旅游标题不能为空.")]
        [StringLength(50, ErrorMessage = "旅游标题不得超过50个字")]
        public string Title { get; set; }

        ///<summary>
        ///Shorttitle
        ///</summary>
        [StringLength(10, ErrorMessage = "旅游短标题不得超过10个字")]
        public string Shorttitle { get; set; }

        ///<summary>
        ///KeyWord
        ///</summary>
        [Required(ErrorMessage = "关键词不能为空.")]
        [StringLength(100, ErrorMessage = "关键词不得超过100个字")]
        public string KeyWord { get; set; }

        ///<summary>
        ///Description
        ///</summary>
        [Required(ErrorMessage = "描述不能为空.")]
        [StringLength(150, ErrorMessage = "描述不得超过150个字")]
        public string Description { get; set; }

        ///<summary>
        ///SEOURL
        ///</summary>
        [RegularExpression(@"[0-9a-zA-Z_]{1,50}", ErrorMessage = "旅游SEOURl径必须为1-50位的数字字母下划线组合.")]
        [Remote("VerificationContentSEOURL", "Ajax", ErrorMessage = "该旅游SEOURL已经存在,请改用其它路径.", AdditionalFields = "AutoKey")]
        public string SEOURL { get; set; }

        ///<summary>
        ///旅游天数
        ///</summary>
        [Required(ErrorMessage = "请选择旅游天数.")]
        public string TravelingDays { get; set; }

        ///<summary>
        ///选择交通
        ///</summary>
        [Required(ErrorMessage = "请选择旅游所用交通工具.")]
        public string Transport { get; set; }

        ///<summary>
        ///参团类型
        ///</summary>
        [Required(ErrorMessage = "请选择参团类型.")]
        public string OfferedType { get; set; }

        ///<summary>
        ///范围分类
        ///</summary>
        [Required(ErrorMessage = "请选择分类范围.")]
        public int CategoryID { get; set; }

        ///<summary>
        ///最少参团人数
        ///</summary>
        [Range(typeof(int), "1", "1000", ErrorMessage = "团人数在{1}和{2}之间")]
        public int MinSignUp { get; set; }

        ///<summary>
        ///Features
        ///</summary>
        [Required(ErrorMessage = "线路特色不能为空.")]
        [StringLength(200, ErrorMessage = "线路特色不得超过200个字")]
        public string Features { get; set; }

        ///<summary>
        ///Price
        ///</summary>
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        [Required(ErrorMessage = "价格不能为空")]
        [Range(typeof(decimal), "1.0", "1000000.0", ErrorMessage = "价格在{1}和{2}之间")]
        public decimal Price { get; set; }

        ///<summary>
        ///预览图片
        ///</summary>
        public string PreviewIMG { get; set; }

        ///<summary>
        ///XMLContent
        ///</summary>
        [Remote("CheckXMLModel", "Ajax")]
        public ContentXML XMLContent { get; set; }

        ///<summary>
        ///GUID
        ///</summary>
        public String GUID { get; set; }

        ///<summary>
        ///CreateTime
        ///</summary>
        public DateTime CreateTime { get; set; }

        ///<summary>
        ///Enable
        ///</summary>
        public bool Enable { get; set; }

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
                    if (info.Name.Equals("XMLContent", StringComparison.OrdinalIgnoreCase))
                    {
                        if (data[info.Name].ToEmptyTrimString().Length == 0)
                            info.SetValue(this, new ContentXML(), null);
                        else
                            info.SetValue(this, data[info.Name].ToEmptyTrimString().XmlDeserialize<ContentXML>(System.Text.Encoding.UTF8), null);
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
                    if (info.Name.Equals("XMLContent", StringComparison.OrdinalIgnoreCase))
                    {
                        data[info.Name] = this.XMLContent.XmlSerialize(System.Text.Encoding.UTF8).Replace("encoding=\"utf-8\"", "");
                        continue;
                    }
                    data[info.Name] = info.GetValue(this, null);
                }
            }
        }

        public void ClearSelectEmpty()
        {
            this.TravelingDays = this.TravelingDays.ToEmptyTrimString();
            this.OfferedType = this.OfferedType.ToEmptyTrimString();
            this.CategoryID = this.CategoryID.ToInt32Value();
            this.Transport = this.Transport.ToEmptyTrimString();
        }
    }

    [Serializable]
    [XmlRoot("ContentXML")]
    public class ContentXML
    {
        /// <summary>
        /// 行程安排
        /// </summary>
        [Required(ErrorMessage = "行程安排不能为空.")]
        public String ProductDetail { get; set; }

        /// <summary>
        /// 报价说明
        /// </summary>
        [Required(ErrorMessage = "报价说明不能为空.")]
        public String ProductTravel { get; set; }

        /// <summary>
        /// 友情提示
        /// </summary>
        [Required(ErrorMessage = "友情提示不能为空.")]
        public String ProductPrompt { get; set; }
    }
}
