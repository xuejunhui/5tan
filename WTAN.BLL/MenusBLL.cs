using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.IDAL;
using WTAN.Model.DModel;
using WTAN.CommonUtility;

namespace WTAN.BLL
{
    public class MenusBLL
    {
        IAdminMenus MenuDB = DALFactory.DataAccess.CreateAdminMenus();

        public String EditorConfigString()
        {
            return MenuDB.GetEditorConfig();
        }

        public ChildItem GetMenuInfo(int menuid, int parentid = 2)
        {
            try
            {
                return GetAdminMenus().MenuItems.Where(s => s.ID == parentid).FirstOrDefault().ChildItems.Where(s => s.ID == menuid).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ex.AddLog("MenusBLL", "GetMenuInfo");
                return new ChildItem();
            }
        }
        public AdminMenus GetAdminMenus()
        {
            return MenuDB.GetAdminMenus();
        }
    }
}
