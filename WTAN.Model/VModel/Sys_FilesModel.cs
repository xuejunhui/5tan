using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using WTAN.Model.DModel;
using WTAN.CommonUtility;

namespace WTAN.Model.VModel
{
    public class Sys_FilesModel : VModelBase
    {
        ///<summary>
        ///AutoKey
        ///</summary>
        public int AutoKey { get; set; }

        ///<summary>
        ///FileName
        ///</summary>
        public string FileName { get; set; }

        ///<summary>
        ///FileURL
        ///</summary>
        public string FileURL { get; set; }

        ///<summary>
        ///FilePath
        ///</summary>
        public string FilePath { get; set; }

        ///<summary>
        ///FileSize
        ///</summary>
        public int FileSize { get; set; }

        ///<summary>
        ///FileType
        ///</summary>
        public string FileType { get; set; }

        ///<summary>
        ///UploadType
        ///</summary>
        public string UploadType { get; set; }

        ///<summary>
        ///Sort
        ///</summary>
        public int Sort { get; set; }

        ///<summary>
        ///RelatedGUID
        ///</summary>
        public String RelatedGUID { get; set; }

        ///<summary>
        ///Enable
        ///</summary>
        public bool Enable { get; set; }
    }
}
