using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.CommonUtility;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace WTAN.Model.VModel
{
    public class AdsConfigModel : Sys_ConfigModel<AdsConfigXml>
    {
        public AdsConfigModel()
        { 
        
        }

        public WebName WebName { get; set; }

        public AdsConfigModel(String pos,AdsType adstype)
        {
            this.Sys_Type = pos;
            this.Sys_Value = new AdsConfigXml() { AdsType = adstype };
        }
    }

    [Serializable]
    [XmlRoot("AdsConfigXml")]
    public class AdsConfigXml : ConfigXml
    {
        public AdsType AdsType { get; set; }

        [StringLength(100, ErrorMessage = "该文本框不能超过100个字符.")]
        public String Link { get; set; }

        [Required(ErrorMessage = "请输广告名称.")]
        [StringLength(50, ErrorMessage = "广告名称不能超过50个字.")]
        public String LinkName { get; set; }

        [StringLength(200, ErrorMessage = "该文本框不能输入超过200个字符.")]
        public String LinkContent { get; set; }

        [StringLength(200, ErrorMessage = "该文本框不能输入超过200个字符.")]
        public String LinkDesc { get; set; }
    }
}
