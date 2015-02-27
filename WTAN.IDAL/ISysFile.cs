using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;

namespace WTAN.IDAL
{
    public interface ISysFile
    {
        List<Sys_FilesTB> GetFilesList(String guid,int top, Boolean isAll);

        /// <summary>
        /// 修改文件狀態
        /// </summary>
        /// <param name="relatedGUID"></param>
        /// <param name="Enable"></param>
        /// <returns></returns>
        Boolean UpdateFileState(String relatedGUID, Boolean Enable);

        /// <summary>
        /// 获得文件列表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <param name="guid"></param>
        /// <param name="UploadType"></param>
        /// <param name="sumcount"></param>
        /// <returns></returns>
        List<Sys_FilesTB> GetFilesList(int start, int size, String guid, String uploadType, out int sumcount);

        /// <summary>
        /// 修改文件的某一属性
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="autokey"></param>
        /// <returns></returns>
        Boolean UpdateFileType(String key, String value, int autokey);

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="autokey"></param>
        /// <returns></returns>
        Sys_FilesTB GetFileInfo(int autokey);

        /// <summary>
        /// 根据关联GUID进行批量删除
        /// </summary>
        /// <param name="relatedGUID"></param>
        /// <returns></returns>
        Boolean DelFileInfo(String relatedGUID);

        /// <summary>
        /// 删除文件信息
        /// </summary>
        /// <param name="autokey"></param>
        /// <returns></returns>
        Boolean DelFileInfo(int autokey);

        /// <summary>
        /// 保存上传文件的信息到资料库
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        int SaveFileInfo(Sys_FilesTB file);
    }
}
