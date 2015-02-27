using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using WTAN.CommonUtility;
using WTAN.IDAL;

namespace WTAN.DALFactory
{
    public class DataAccess
    {
        //不使用缓存
        private static object CreateObjectNoCache(string AssemblyPath, string classNamespace)
        {
            try
            {
                object objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
                return objType;
            }
            catch (System.Exception ex)
            {
                ex.AddLog("BLL.DataAccess", "CreateObjectNoCache");
                return null;
            }

        }
        //使用缓存
        private static object CreateObject(string AssemblyPath, string classNamespace)
        {
            object objType =  CacheHelper.ReadServerCache(classNamespace);
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
                    CacheHelper.CreateServerCache(classNamespace, objType);// 写入缓存
                }
                catch(System.Exception ex)
                {
                    ex.AddLog("BLL.DataAccess", "CreateObject");
                }
            }
            return objType;
        }

        #region CreateAccount_Users
        public static IAccount_Users CreateAccount_Users()
        {
            //方式1			
            //return (Maticsoft.IDAL.IAccount_Users)Assembly.Load(AssemblyPath).CreateInstance(AssemblyPath+".Account_Users");

            //方式2 			
            string classNamespace = AppSettings.path + ".AccountServer";
            object objType = CreateObject(AppSettings.path, classNamespace);
            return (IAccount_Users)objType;
        }
        #endregion

        #region CreateAdminMenus
        public static IAdminMenus CreateAdminMenus()
        {
            string classNamespace = AppSettings.path + ".AdminMenuServer";
            object objType = CreateObject(AppSettings.path, classNamespace);
            return (IAdminMenus)objType;
        }
        #endregion

        #region CreateConfig
        public static IConfig CreateConfigInfo()
        {
            string classNamespace = AppSettings.path + ".ConfigServer";
            object objType = CreateObject(AppSettings.path, classNamespace);
            return (IConfig)objType;
        }
        #endregion

        #region CreateCategory
        public static ICategory CreateCategory()
        {
            string classNamespace = AppSettings.path + ".CategoryServer";
            object objType = CreateObject(AppSettings.path, classNamespace);
            return (ICategory)objType;
        }
        #endregion

        #region CreateContent
        public static IContent CreateContent()
        {
            string classNamespace = AppSettings.path + ".ContentServer";
            object objType = CreateObject(AppSettings.path, classNamespace);
            return (IContent)objType;
        }
        #endregion

        #region CreateSysFile
        public static ISysFile CreateSysFile()
        {
            string classNamespace = AppSettings.path + ".SysFileServer";
            object objType = CreateObject(AppSettings.path, classNamespace);
            return (ISysFile)objType;
        }
        #endregion

        #region CreateGuide
        public static IGuide CreateGuide()
        {
            string classNamespace = AppSettings.path + ".GuideServer";
            object objType = CreateObject(AppSettings.path, classNamespace);
            return (IGuide)objType;
        }
        #endregion

        #region CreateVisaCenter
        public static IVisaCenter CreateVisaCenter()
        {
            string classNamespace = AppSettings.path + ".VisaCenterServer";
            object objType = CreateObject(AppSettings.path, classNamespace);
            return (IVisaCenter)objType;
        }
        #endregion

        #region CreateSpecialColumn
        public static ISpecialColumn CreateSpecialColumn()
        {
            string classNamespace = AppSettings.path + ".SpecialColumnServer";
            object objType = CreateObject(AppSettings.path, classNamespace);
            return (ISpecialColumn)objType;
        }
        #endregion

        #region CreateFriendLink
        public static IFriendLink CreateFriendLink()
        {
            string classNamespace = AppSettings.path + ".FriendLinkServer";
            object objType = CreateObject(AppSettings.path, classNamespace);
            return (IFriendLink)objType;
        }
        #endregion
    }
}
