using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.IDAL;
using WTAN.Model.DModel;
using WTAN.CommonUtility;

namespace WTAN.BLL
{
    public class SysFileBLL
    {
        ISysFile SysFile = DALFactory.DataAccess.CreateSysFile();

        public List<Sys_FilesTB> GetFilesList(String guid, int top, Boolean isAll)
        {
            return SysFile.GetFilesList(guid, top, isAll);
        }

        /// <summary>
        /// 修改文件為正式的有效關聯文件
        /// </summary>
        /// <param name="rguid"></param>
        /// <returns></returns>
        public Boolean ChanageFileOfficial(String rguid)
        {
            return SysFile.UpdateFileState(rguid, true);
        }

        public List<Sys_FilesTB> GetFilesList(int start, int size, String guid, String uploadType, out int sumcount)
        {
            return SysFile.GetFilesList(start, size, guid, uploadType, out sumcount);
        }

        public Boolean UploadFileSortInfo(String value, int autokey)
        {
            return SysFile.UpdateFileType("Sort", value, autokey);
        }

        /// <summary>
        /// 删除关联的GUID 文件信息不删除文件
        /// </summary>
        /// <param name="relatedguid"></param>
        /// <returns></returns>
        public Boolean DelFileInfo(String relatedguid)
        {
            return SysFile.DelFileInfo(relatedguid);
        }
          
        /// <summary>
        /// 删除一条文件信息 并删除物理文件
        /// </summary>
        /// <param name="autokey"></param>
        /// <returns></returns>
        public Boolean DelFileInfo(int autokey)
        {
            Sys_FilesTB tb = GetFileInfo(autokey);
            if (SysFile.DelFileInfo(autokey))
            {
                tb.FileURL.DelFile();
                return true;
            }
            return false;
        }

        public Sys_FilesTB GetFileInfo(int autokey)
        {
            return SysFile.GetFileInfo(autokey);
        }

        public int SaveFileInfo(Sys_FilesTB file)
        {
            return SysFile.SaveFileInfo(file);
        }
    }
}
