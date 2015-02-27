using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;
using WTAN.CommonUtility;

namespace WTAN.Model.VModel
{
    public class CategoryDropDownBox
    {
        /// <summary>
        /// 第一下拉框所属分类的parentid
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// 当前选中的分类
        /// </summary>
        public String Value { get; set; }

        /// <summary>
        /// 分类所在页面提交时Name属性
        /// </summary>
        public DropDownState CategoryIDName { get; set; }

        public List<CategoryTB> DropDownData { get; set; }
    }
}
