using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;

namespace WTAN.IDAL
{
    public interface IAdminMenus
    {
        /// <summary>
        /// 获取管理员菜单
        /// </summary>
        /// <returns></returns>
        AdminMenus GetAdminMenus();

        String GetEditorConfig();
    }
}
