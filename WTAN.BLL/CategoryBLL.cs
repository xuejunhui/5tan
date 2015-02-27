using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.IDAL;
using WTAN.Model.DModel;
using WTAN.Model.VModel;
using WTAN.CommonUtility;

namespace WTAN.BLL
{
    public class CategoryBLL
    {
        ICategory Category = DALFactory.DataAccess.CreateCategory();

        public void BindRegionModel(RegionModel model)
        {
            model.CategoryChilds = Category.GetCategorysByParent(model.CurrentCategory.AutoKey);
            model.DaysChilds = Category.GetCategorysByParent((int)DropDownState.TravelingDays);
            model.TrafficChilds = Category.GetCategorysByParent((int)DropDownState.Transport);
            model.CategoryNav = Category.GetCategoryNav(model.CurrentCategory.AutoKey);
        }

        public Boolean DelCategory(String id)
        {
            return Category.DelCategory(id);
        }
        /// <summary>
        /// 未分頁
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="menuid"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public CategoryViewData GetCategoryListViewData(int parent, int menuid, String keyword = "")
        {
            MenusBLL bll = new MenusBLL();
            CategoryViewData model = new CategoryViewData();
            model.Menu = bll.GetMenuInfo(menuid);
            model.Data = Category.GetCategorysByParent(parent, keyword);
            model.Nav = GetCategoryNav(parent);
            return model;
        }

        /// <summary>
        /// 分頁
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="menuid"></param>
        /// <param name="paddingformat"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public CategoryViewData GetCategoryListViewData(int parent,int pageindex,int pagesize, int menuid,String paddingformat, String keyword = "")
        {
            MenusBLL bll = new MenusBLL();
            CategoryViewData model = new CategoryViewData();
            int rowcount = 0;
            model.Menu = bll.GetMenuInfo(menuid);
            model.Data = Category.GetCategorysByParent(parent, pageindex, pagesize, out rowcount, keyword);
            model.Pagination = new PaginationHelper(pageindex, rowcount, pagesize, 8, paddingformat);
            model.Nav = GetCategoryNav(parent);
            return model;
        }

        /// <summary>
        /// 获取分类下拉框加载所需数据 返回值为主键
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public String GetCategoryDropDownNav(int parent)
        {
            String result = String.Empty;
            foreach (var item in GetCategoryNav(parent))
            {
                result += (result.IsNullOrEmpty() ? "" : "|") + item.AutoKey;
            }
            return result;
        }

        /// <summary>
        /// 获取分类下拉框加载所需数据 返回值为分类名称
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public String GetCategoryDropDownNav(String categoryname)
        {
            String result = String.Empty;
            foreach (var item in GetCategoryNav(0, categoryname))
            {
                result += (result.IsNullOrEmpty() ? "" : "|") + item.CategoryName;
            }
            return result;
        }

        public List<CategoryTB> GetCategoryNav(int parent, String categoryname = "")
        {
            return Category.GetCategoryNav(parent, categoryname);
        }

        /// <summary>
        /// 获取分类列表视图数据
        /// </summary>
        /// <param name="menuid"></param>
        /// <returns></returns>
        public CategoryViewData GetCategoryListViewData(int menuid)
        {
            MenusBLL bll = new MenusBLL();
            ChildItem menu = bll.GetAdminMenus().MenuItems.Where(s => s.ID == 2).FirstOrDefault().ChildItems.Where(s => s.ID == menuid).FirstOrDefault();
            List<CategoryTB> list = Category.GetCategorys(menu.CategoryIDS);
            return new CategoryViewData()
            {
                Data = list,
                Menu = menu
            };
        }

        public List<CategoryTB> GetCategorysByParentID(int parentid)
        {
            return Category.GetCategorysByParent(parentid);
        }

        public List<CategoryTB> GetCategorysByCategoryName(String category)
        {
            return Category.GetCategorysByCategoryName(category);
        }

        public List<CategoryTB> GetCategorys(String id)
        {
            return Category.GetCategorys(id);
        }

        /// <summary>
        /// 分类名称是否已经存在
        /// </summary>
        /// <param name="categoryname"></param>
        /// <param name="autokey"></param>
        /// <returns></returns>
        public Boolean IsExistsCategory(String categoryname, int autokey)
        {
            CategoryTB c = GetCategory(categoryname);
            return c.AutoKey != 0 && c.AutoKey != autokey;
        }

        public Boolean IsExistsCategoryBySEO(String seourl, int autokey)
        {
            CategoryTB c = GetCategoryBySEO(seourl);
            return c.AutoKey != 0 && c.AutoKey != autokey;
        }

        public CategoryTB GetCategory(int autokey)
        {
            return Category.GetCategoryInfo(autokey);
        }

        public CategoryTB GetCategory(String categoryname)
        {
            return Category.GetCategoryInfo(categoryname);
        }

        public CategoryTB GetCategoryBySEO(String seourl)
        {
            return Category.GetCategoryInfo(seourl, "SEOURL");
        }

        /// <summary>
        /// 保存分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveCategory(CategoryModel model)
        {
            return Category.SaveCategoryInfo(model);
        }
    }
}
