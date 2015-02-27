using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using WTAN.Model.DModel;

namespace WTAN.Model.VModel
{
    public class VisaCenterModel
    {
        public VisaCenterModel(CategoryTB c)
        {
            if (c != null && c.AutoKey > 0)
            {
                this.VisaCenter = new CategoryModel();
                this.VisaCenter.BindModelData(c);
            }
        }

        public CategoryModel VisaCenter { get; set; }

        public List<VisaCenterTB> VisaList { get; set; }

        public List<CategoryTB> VisaCategory { get; set; }

        public List<SpecialColumnTB> VisaQuestion { get; set; }

    }

    public class VisaModel : VModelBase
    {
        public String CategoryDropDownNav { get; set; }

        public int MainTop { get; set; }

        public String VType { get; set; }

        ///<summary>
        ///AutoKey
        ///</summary>
        public int AutoKey { get; set; }

        ///<summary>
        ///KeyWord
        ///</summary>
        [StringLength(50, ErrorMessage = "关键字不得超过50个字")]
        public string KeyWord { get; set; }

        ///<summary>
        ///Description
        ///</summary>
        [StringLength(150, ErrorMessage = "分类描述不得超过150个字")]
        public string Description { get; set; }

        ///<summary>
        ///CategoryID
        ///</summary>
        [Required(ErrorMessage = "请选择签证分类.")]
        public int CategoryID { get; set; }

        ///<summary>
        ///GUID
        ///</summary>
        public String GUID { get; set; }

        ///<summary>
        ///Price
        ///</summary>
        [Required(ErrorMessage = "价格不能为空.")]
        public decimal Price { get; set; }

        ///<summary>
        ///VName
        ///</summary>
        [Required(ErrorMessage = "签证名称不能为空.")]
        [StringLength(50, ErrorMessage = "签证名称不得超过50个字")]
        [Remote("VerificationVisaName", "Ajax", ErrorMessage = "该签证名称已经存在,请改用其它名称.", AdditionalFields = "AutoKey")]
        public string VName { get; set; }

        ///<summary>
        ///VContent
        ///</summary>
         [Required(ErrorMessage = "签证内容不能为空.")]
        public string VContent { get; set; }

        ///<summary>
        ///CreateTime
        ///</summary>
        public DateTime CreateTime { get; set; }

        ///<summary>
        ///Enable
        ///</summary>
        public bool Enable { get; set; }
    }
}
