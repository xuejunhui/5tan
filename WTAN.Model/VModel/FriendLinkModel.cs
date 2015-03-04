using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.CommonUtility;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WTAN.Model.VModel
{
    public class ApplicationFriendsModel : FriendLinkModel
    {
        [Required(ErrorMessage = "邮箱不能为空.")]
        [StringLength(50, ErrorMessage = "邮箱不得超过50个字")]
        [RegularExpression(@"[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}", ErrorMessage = "请填写符合规范的邮箱地址.")]
        public String Email { get; set; }
    }

    public class FriendLinkModel : VModelBase
    {
        ///<summary>
        ///AutoKey
        ///</summary>
        public int AutoKey { get; set; }

        ///<summary>
        ///LinkName
        ///</summary>
        [Required(ErrorMessage = "友链名称不能为空.")]
        [StringLength(50, ErrorMessage = "友链名称不得超过50个字")]
        [Remote("VerificationFriendLinkName", "Ajax", ErrorMessage = "该友链名称已经存在.", AdditionalFields = "AutoKey")]
        public string LinkName { get; set; }

        ///<summary>
        ///LinkUrl
        ///</summary>
        [Required(ErrorMessage = "友链地址不能为空.")]
        [StringLength(50, ErrorMessage = "友链地址不得超过50个字")]
        [Remote("VerificationFriendLinkUrl", "Ajax", ErrorMessage = "该友链地址已经存在.", AdditionalFields = "AutoKey")]
        public string LinkUrl { get; set; }

        ///<summary>
        ///Note
        ///</summary>
        [Required(ErrorMessage = "友链备注不能为空.")]
        [StringLength(100, ErrorMessage = "备注不得超过100个字")]
        public string Note { get; set; }

        ///<summary>
        ///CreateTime
        ///</summary>
        public DateTime CreateTime { get; set; }

        public String WebName { get; set; }

        ///<summary>
        ///Enable
        ///</summary>
        public bool Enable { get; set; }
    }
}
