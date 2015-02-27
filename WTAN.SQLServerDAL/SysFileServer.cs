using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.IDAL;
using WTAN.Model.DModel;
using WTAN.CommonUtility;

namespace WTAN.SQLServerDAL
{
    public class SysFileServer : ISysFile
    {

        public List<Sys_FilesTB> GetFilesList(String guid, int top, Boolean isAll)
        {
            String cachekey = String.Format("GetFilesList_{0}_{1}_{2}", guid, top, isAll);
            List<Sys_FilesTB> result = CacheHelper.ReadServerCache(cachekey) as List<Sys_FilesTB>;
            if (result == null)
            {
                String sql = String.Format("select {0} * from Sys_Files where RelatedGUID=@RelatedGUID and UploadType like '%_Img' {1} order by Sort asc,autokey desc",
                    top > 0 ? "top " + top : "",
                    isAll ? "" : " and Sort=0");
                result = sql.ExecuteRecords<Sys_FilesTB>("RelatedGUID", guid);

            }
            return result;
        }


        /// <summary>
        ///  获取对应内容的对应资源
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <param name="guid"></param>
        /// <param name="UploadType"></param>
        /// <param name="sumcount"></param>
        /// <returns></returns>
        public List<Sys_FilesTB> GetFilesList(int start, int size, String guid, String uploadType, out int sumcount)
        {
            String sql = String.Format("select top {0} * from (select ROW_NUMBER() over (order by autokey) as RowNumber,* from Sys_Files where RelatedGUID=@RelatedGUID and UploadType=@UploadType) A where RowNumber > {1}",
                size, start);
            List<Sys_FilesTB> list = sql.ExecuteRecords<Sys_FilesTB>("UploadType", uploadType, "RelatedGUID", guid);
            if (list != null && list.Count() > 0)
            {
                sql = "select count(0) from Sys_Files where RelatedGUID=@RelatedGUID and UploadType=@UploadType";
                sumcount = sql.ExecuteScalarInt("UploadType", uploadType, "RelatedGUID", guid).ToInt32Value();
            }
            else
                sumcount = 0;
            return list;
        }

        public Boolean UpdateFileType(String key, String value, int autokey)
        {
            String sql = String.Format("update Sys_Files set {0}=@{0} where autokey=@autokey", key);
            return sql.ExecuteNoneQuery(key, value, "autokey", autokey) > 0;
        }

        public Sys_FilesTB GetFileInfo(int autokey)
        {
            String sql = "select * from Sys_Files where autokey=@autokey";
            return sql.ExecuteOneRecord<Sys_FilesTB>("autokey", autokey.ToString());
        }

        public Boolean UpdateFileState(String relatedGUID, Boolean Enable)
        {
            String sql = "update Sys_Files set Enable=@Enable where relatedGUID=@relatedGUID";
            return sql.ExecuteNoneQuery("Enable", Enable ? "1" : "0", "relatedGUID", relatedGUID) > 0;
        }

        public List<Sys_FilesTB> GetFiles(String relatedGUID)
        {
            String sql = "select * from Sys_Files where relatedGUID=@relatedGUID";
            return sql.ExecuteRecords<Sys_FilesTB>("relatedGUID", relatedGUID.ToString());
        }

        public Boolean DelFileInfo(String relatedGUID)
        {
            foreach (var Sys_FilesTB in GetFiles(relatedGUID))
            {
                Sys_FilesTB.FileURL.DelFile();
            }
            String sql = "delete Sys_Files where relatedGUID=@relatedGUID";
            return sql.ExecuteNoneQuery("relatedGUID", relatedGUID.ToString()) > 0;
        }

        public Boolean DelFileInfo(int autokey)
        {
            String sql = "delete Sys_Files where autokey=@autokey";
            return sql.ExecuteNoneQuery("autokey",autokey.ToString()) > 0;
        }

        public int SaveFileInfo(Sys_FilesTB file)
        {
            String sql = @"insert into Sys_Files(FileName, FileURL, FilePath, FileSize, FileType, UploadType, Sort, RelatedGUID, Enable) 
                            values(@FileName, @FileURL, @FilePath, @FileSize, @FileType, @UploadType, @Sort, @RelatedGUID, 0)
                            select isnull(SCOPE_IDENTITY(),0) as ID";
            List<String> list = new List<String>() { 
                "FileName",file.FileName,
                "FileURL",file.FileURL,
                "FilePath",file.FilePath,
                "FileSize",file.FileSize.ToString(),
                "FileType",file.FileType,
                "UploadType",file.UploadType,
                "Sort",file.Sort.ToString(),
                "RelatedGUID",file.RelatedGUID
            };
            return sql.ExecuteScalarInt(list.ToArray()).ToInt32Value();
        }
    }
}
