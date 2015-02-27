using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using WTAN.CommonUtility;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WTAN.Model.VModel
{
    public class SpecialColumnModel : VModelBase
    {
        public SpecialColumnType SCType { get; set; }

        ///<summary>
        ///AutoKey
        ///</summary>
        public int AutoKey { get; set; }

        ///<summary>
        ///SC_Name
        ///</summary>
        [Required(ErrorMessage = "标题名称不能为空.")]
        [StringLength(50, ErrorMessage = "标题名称不得超过50个字")]
        public string SC_Name { get; set; }

        ///<summary>
        ///SEOURL
        ///</summary>
        [Remote("VerificationSpecialColumnSEOURL", "Ajax", ErrorMessage = "该路径已经存在,请改用其它路径.", AdditionalFields = "AutoKey")]
        public string SEOURL { get; set; }

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
        ///SC_Content
        ///</summary>
        public string SC_Content { get; set; }

        ///<summary>
        ///ViewModel
        ///</summary>
        public string ViewModel { get; set; }

        ///<summary>
        ///GUID
        ///</summary>
        public string GUID { get; set; }

        ///<summary>
        ///ParentID
        ///</summary>
        public int ParentID { get; set; }

        ///<summary>
        ///Enable
        ///</summary>
        public bool Enable { get; set; }
    }
}
