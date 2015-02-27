using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;
using WTAN.CommonUtility;
using WTAN.SQLServerDAL.Resources;
using WTAN.IDAL;

namespace WTAN.SQLServerDAL
{
    public class AdminMenuServer : IAdminMenus
    {
        public String GetEditorConfig()
        {
            return Resource.EditorConfig;
        }

        public AdminMenus GetAdminMenus()
        {
            try
            {
                String cacheKey = "GetAdminMenu";
                AdminMenus menu = CacheHelper.ReadServerCache(cacheKey) as AdminMenus;
                if (menu == null)
                {
                   menu = Resources.Resource.AdminMenus.XmlDeserialize<AdminMenus>(Encoding.UTF8);
                   CacheHelper.CreateServerCache(cacheKey, menu);
                }
                return menu;
                
            }
            catch (Exception ex)
            {
                ex.AddLog("Menus", "GetAdminMenu");
                return new AdminMenus();
            }
        } 
    }
}
