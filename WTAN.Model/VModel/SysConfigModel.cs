using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Reflection;
using WTAN.CommonUtility;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WTAN.Model.VModel
{
    /// <summary>
    /// 网站基本配置类
    /// </summary>
    public class SysConfigModel : Sys_ConfigModel<SysConfigXML>
    {
        public SysConfigModel() {
            this.Sys_Type = "SysConfig";
        }

        public WebName WebName { get; set; }
    }

    [Serializable]
    [XmlRoot("SysConfigXML")]
    public class SysConfigXML : ConfigXml
    {
        /// <summary>
        /// 網站名
        /// </summary>
        [Required(ErrorMessage = "请输入网站名称.")]
        [StringLength(50, ErrorMessage = "网站名称不能超过50个字.")]
        public String WebName { get; set; }

        /// <summary>
        /// 網站域名
        /// </summary>
        [Required(ErrorMessage = "请输入网站域名.")]
        [StringLength(50, ErrorMessage = "域名长度不得超过50个字符.")]
        public String DomainUrl { get; set; }

        /// <summary>
        /// 網站備案號
        /// </summary>
        public String BeiAn { get; set; }

        /// <summary>
        /// 網站標題
        /// </summary>
        [Required(ErrorMessage = "请输入网站标题.")]
        [StringLength(50, ErrorMessage = "网站标题长度不能超过50个字.")]
        public String Title { get; set; }

        /// <summary>
        /// 網站關鍵字
        /// </summary>
        [Required(ErrorMessage = "请输入网站关键字.")]
        [StringLength(100, ErrorMessage = "网站关键字长度不得超过50個字.")]
        public String Keyword { get; set; }

        /// <summary>
        /// 網站簡介
        /// </summary>
        [Required(ErrorMessage = "请输入网站简介.")]
        [StringLength(150, ErrorMessage = "网站描述不得超过150个字.")]
        public String Description { get; set; }

        [Required(ErrorMessage = "请输入在线联系QQ号码.")]
        [StringLength(12, ErrorMessage = "请输入正确的qq号码")]
        public String QQ { get; set; }

        [Required(ErrorMessage = "请输入联系人手机号码.")]
        public String Phone { get; set; }

        [Required(ErrorMessage = "请输入联系人固定电话.")]
        public String Telephone { get; set; }

        [Required(ErrorMessage = "请输入联系人名字.")]
        public String ConcactName { get; set; }

        [Required(ErrorMessage = "请输入旅行社地址.")]
        public String Address { get; set; }

        [Required(ErrorMessage = "请输入许可证编号.")]
        public String LicenseNumber { get; set; }

        [Required(ErrorMessage = "请输入注册号.")]
        public String RegistrationNumber { get; set; }

        /// <summary>
        /// 版權所有
        /// </summary>
        public String Copyright { get; set; }
    }
}
