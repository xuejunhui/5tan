using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;
using WTAN.Model.VModel;

namespace WTAN.IDAL
{
    public interface ICategory
    {
        /// <summary>
        /// 根据分类名称获得分类列表
        /// </summary>
        /// <param name="categoryname"></param>
        /// <returns></returns>
        List<CategoryTB> GetCategorysByCategoryName(String categoryname);

        /// <summary>
        /// 刪除分類
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Boolean DelCategory(String id);

        /// <summary>
        /// 獲得下一層分類 分頁
        /// </summary>
        /// <param name="parentid"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<CategoryTB> GetCategorysByParent(int parentid, int pageindex, int pagesize, out int rowcount, String keyword = "");

        /// <summary>
        /// 获得分类下一岑的分类数据
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        List<CategoryTB> GetCategorysByParent(int parentid, String keyword = "");

        /// <summary>
        /// 获得分类导航数据
        /// </summary>
        /// <param name="autokey"></param>
        /// <returns></returns>
        List<CategoryTB> GetCategoryNav(int autokey, String categoryname = "");

        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<CategoryTB> GetCategorys(String id);

        /// <summary>
        /// 根據分類名稱獲得某個分類
        /// </summary>
        /// <param name="cname"></param>
        /// <returns></returns>
        CategoryTB GetCategoryInfo(String cname, String colname = "CategoryName");

        /// <summary>
        /// 根据主键获得分类信息
        /// </summary>
        /// <param name="autokey"></param>
        /// <returns></returns>
        CategoryTB GetCategoryInfo(int autokey);

        /// <summary>
        /// 保存分類信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int SaveCategoryInfo(CategoryModel model);
    }
}
