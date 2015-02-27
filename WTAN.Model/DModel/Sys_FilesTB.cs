using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.CommonUtility;

namespace WTAN.Model.DModel
{
    public class Sys_FilesTB : DModelBase
    {
        public Sys_FilesTB() 
            : base("FileName", "FileURL", "FilePath", "FileSize", "FileType", "UploadType", "Sort", "RelatedGUID")
        {

        }

        //public string MappingTableName { get { return "Sys_Files"; } }

        #region SetFieldValue
        protected override void SetFieldValue(string name, object value)
        {
            object newValue = value;
            switch (name.ToLower())
            {
                case "filename":
                    newValue = value.ToValue("string");
                    break;
                case "fileurl":
                    newValue = value.ToValue("string");
                    break;
                case "filepath":
                    newValue = value.ToValue("string");
                    break;
                case "filesize":
                    newValue = value.ToValue("int");
                    break;
                case "filetype":
                    newValue = value.ToValue("string");
                    break;
                case "uploadtype":
                    newValue = value.ToValue("string");
                    break;
                case "sort":
                    newValue = value.ToValue("int");
                    break;
                case "relatedguid":
                    newValue = value.ToValue("string");
                    break;
                default:
                    newValue = value;
                    break;
            }
            base.SetFieldValue(name, newValue);
        }
        #endregion

        #region property FileName
        ///<summary>
        ///文件名
        ///</summary>
        public string FileName
        {
            get { return base["FileName"].ToEmptyTrimString(); }
            set { base["FileName"] = value; }
        }
        #endregion

        #region property FileURL
        ///<summary>
        ///瀏覽文件路徑
        ///</summary>
        public string FileURL
        {
            get { return base["FileURL"].ToEmptyTrimString(); }
            set { base["FileURL"] = value; }
        }
        #endregion

        #region property FilePath
        ///<summary>
        ///FilePath
        ///</summary>
        public string FilePath
        {
            get { return base["FilePath"].ToEmptyTrimString(); }
            set { base["FilePath"] = value; }
        }
        #endregion

        #region property FileSize
        ///<summary>
        ///FileSize
        ///</summary>
        public int FileSize
        {
            get { return base["FileSize"].ToInt32Value(); }
            set { base["FileSize"] = value; }
        }
        #endregion

        #region property FileType
        ///<summary>
        ///文件類型（IMG|File）
        ///</summary>
        public string FileType
        {
            get { return base["FileType"].ToEmptyTrimString(); }
            set { base["FileType"] = value; }
        }
        #endregion

        #region property UploadType
        ///<summary>
        ///上傳類型 圖片|普通資源
        ///</summary>
        public string UploadType
        {
            get { return base["UploadType"].ToEmptyTrimString(); }
            set { base["UploadType"] = value; }
        }
        #endregion

        #region property Sort
        ///<summary>
        ///排序 0默認圖片1圖片上傳圖片 2遠程圖片
        ///</summary>
        public int Sort
        {
            get { return base["Sort"].ToInt32Value(); }
            set { base["Sort"] = value; }
        }
        #endregion

        #region property RelatedGUID
        ///<summary>
        ///關聯標識
        ///</summary>
        public String RelatedGUID
        {
            get { return (String)base["RelatedGUID"]; }
            set { base["RelatedGUID"] = value; }
        }
        #endregion


    }
}
