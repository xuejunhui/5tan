using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using WTAN.Model.DModel;
using WTAN.CommonUtility;

namespace WTAN.Model.VModel
{
    public class CategoryViewData
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int RowCount { get; set; }

        public PaginationHelper Pagination { get; set; }

        /// <summary>
        /// 列表数据
        /// </summary>
        public List<CategoryTB> Data { get; set; }
        /// <summary>
        /// 当前菜单栏
        /// </summary>
        public ChildItem Menu { get; set; }
        /// <summary>
        /// 分类导航数据
        /// </summary>
        public List<CategoryTB> Nav { get; set; }

        public String Keyword { get; set; }

        public int CurrentCategoryID { get; set; }
    }

    public class CategoryModel : VModelBase
    {
        ///Autokey
        ///</summary>
        public int AutoKey { get; set; }

        ///<summary>
        ///CategoryName
        ///</summary>
        [Required(ErrorMessage = "分类名称不能为空.")]
        [StringLength(50, ErrorMessage = "分类名称不得超过50个字")]
        [Remote("VerificationCategoryName", "Ajax", ErrorMessage = "该分类名称已经存在,请改用其它名称.", AdditionalFields = "AutoKey")]
        public string CategoryName { get; set; }

        ///<summary>
        ///KeyWord
        ///</summary>
        [StringLength(50, ErrorMessage = "分类关键字不得超过50个字")]
        public string KeyWord { get; set; }

        ///<summary>
        ///Description
        ///</summary>
        [StringLength(200, ErrorMessage = "分类描述不得超过200个字")]
        public string Description { get; set; }

        ///<summary>
        ///SEOURL
        ///</summary>
        [Required(ErrorMessage = "分类SEO不能为空.")]
        [RegularExpression(@"[0-9a-zA-Z_]{1,50}", ErrorMessage = "分类路径必须为1-50位的数字字母下划线组合.")]
        [Remote("VerificationCategorySEOURL", "Ajax", ErrorMessage = "该分类SEOURL已经存在,请改用其它路径.", AdditionalFields = "AutoKey")]
        public string SEOURL { get; set; }

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
