using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using WTAN.CommonUtility;

namespace WTAN.Model.VModel
{
    //Guide
    public class GuideModel : VModelBase
    {
        public WebName WebName { get; set; }

        public String CategoryDropDownNav { get; set; }

        ///<summary>
        ///AutoKey
        ///</summary>
        public int AutoKey { get; set; }

        ///<summary>
        ///KeyWord
        ///</summary>
        [StringLength(50, ErrorMessage = "关键字不得超过100个字")]
        public string KeyWord { get; set; }

        ///<summary>
        ///Description
        ///</summary>
        [StringLength(200, ErrorMessage = "描述不得超过150个字")]
        public string Description { get; set; }

        ///<summary>
        ///RelatedGUID
        ///</summary>
        public String RelatedGUID { get; set; }

        public String GUID { get; set; }

        ///<summary>
        ///GuideName
        ///</summary>
        [Required(ErrorMessage = "指南名称不能为空.")]
        [StringLength(50, ErrorMessage = "指南名称不得超过50个字")]
        public string GuideName { get; set; }

        ///<summary>
        ///GuideContent
        ///</summary>
        [Required(ErrorMessage = "指南内容不能为空.")]
        public string GuideContent { get; set; }

        ///<summary>
        ///CreateTime
        ///</summary>
        public DateTime CreateTime { get; set; }

        ///<summary>
        ///Enable
        ///</summary>
        public bool Enable { get; set; }

        [Required(ErrorMessage = "请选择分类范围.")]
        public int CategoryID { get; set; }
    }
}
