using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.CommonUtility;
using WTAN.IDAL;
using WTAN.Model.DModel;
using WTAN.Model.VModel;

namespace WTAN.SQLServerDAL
{
    public class CategoryServer : ICategory
    {

        public Boolean DelCategory(String id)
        {

            String sql = @"delete category 
                        from category c
                        where autokey in ({0}) and (select count(0) from Category c1 where c1.parentid=c.autokey)=0";//防注入写法
            String ids = String.Empty;
            List<String> list = new List<String>();
            for (int i = 0; i < id.Split(',').Count(); i++)
            {
                ids += (ids.IsNullOrEmpty() ? "" : ",") + String.Format("@id{0}", i);
                list.Add(String.Format("id{0}", i));
                list.Add(id.Split(',')[i]);
            }
            sql = String.Format(sql, ids);
            return sql.ExecuteNoneQuery(list.ToArray()) > 0;
        }

        public List<CategoryTB> GetCategorysByParent(int parentid, String keyword = "")
        {
            String cachekey = "GetCategorysByParent" + parentid + keyword;
            List<CategoryTB> result = CacheHelper.ReadServerCache(cachekey) as List<CategoryTB>;
            if (result == null)
            {
                String sql = "select *,(select count(0) from Category c1 where c1.parentid=c.autokey) childscount from Category c where ParentID=@ParentID";
                List<String> list = new List<string>(){ 
                    "ParentID", parentid.ToString()
                };
                if (!keyword.IsNullOrEmpty())
                {
                    sql += " and (CategoryName like @keyword or SEOURL like @keyword)";
                    list.Add("keyword");
                    list.Add("%" + keyword + "%");
                }
                result = sql.ExecuteRecords<CategoryTB>(list.ToArray());
                CacheHelper.CreateServerCache(cachekey, result);
            }
            return result;
        }

        public List<CategoryTB> GetCategorysByCategoryName(String categoryname)
        {
            String cachekey = "GetCategorysByCategoryName" + categoryname;
            List<CategoryTB> result = CacheHelper.ReadServerCache(cachekey) as List<CategoryTB>;
            if (result == null)
            {
                String sql = "select c.*,(select count(0) from Category c1 where c1.parentid=c.autokey) childscount from Category c,category c1 where c.autokey=c1.parentid and c1.categoryname=@categoryname";
                List<String> list = new List<string>(){ 
                    "categoryname", categoryname
                };
                result = sql.ExecuteRecords<CategoryTB>(list.ToArray());
                CacheHelper.CreateServerCache(cachekey, result);
            }
            return result;
        }

        public List<CategoryTB> GetCategorysByParent(int parentid,int pageindex,int pagesize,out int rowcount, String keyword = "")
        {
            
            String where = "ParentID=@ParentID";
            List<String> list = new List<string>(){ 
                "ParentID", parentid.ToString()
            };
            if (!keyword.IsNullOrEmpty())
            {
                where += " and (CategoryName like @keyword or SEOURL like @keyword)";
                list.Add("keyword");
                list.Add("%" + keyword + "%");
            }
            return CurrentDataServer.GetPagingData<CategoryTB>("category c", "*,(select count(0) from Category c1 where c1.parentid=c.autokey) childscount", "autokey asc", where, pagesize, out rowcount, pageindex, list.ToArray());
        }

        public List<CategoryTB> GetCategoryNav(int autokey, String categoryname = "")
        {
            String cachekey = "GetCategoryNav" + autokey;
            List<CategoryTB> result = CacheHelper.ReadServerCache(cachekey) as List<CategoryTB>;
            if (result == null)
            {
                String sql = "exec GetCategoryAllParentChilds @autokey,@categoryname";
                result = sql.ExecuteRecords<CategoryTB>("autokey", autokey, "categoryname", categoryname);
                CacheHelper.CreateServerCache(cachekey, result);
            }
            return result;
        }

        public List<CategoryTB> GetCategorys(String id)
        {
            String cachekey = String.Format("GetCategory{0}", id);
            List<CategoryTB> result = CacheHelper.ReadServerCache(cachekey) as List<CategoryTB>;
            if (result == null)
            {
                String sql = "select *,(select count(0) from Category c1 where c1.parentid=c.autokey) childscount from category c where autokey in ({0})";//防注入写法
                String ids = String.Empty;
                List<String> list = new List<String>();
                for (int i = 0; i < id.Split(',').Count(); i++)
                {
                    ids += (ids.IsNullOrEmpty() ? "" : ",") + String.Format("@id{0}", i);
                    list.Add(String.Format("id{0}", i));
                    list.Add(id.Split(',')[i]);
                }
                sql = String.Format(sql, ids);
                result = sql.ExecuteRecords<CategoryTB>(list.ToArray());
                CacheHelper.CreateServerCache(cachekey, result);
            }

            return result;
        }

        public CategoryTB GetCategoryInfo(int autokey)
        {
            String cachekey = String.Format("GetCategoryInfo{0}", autokey);
            CategoryTB result = CacheHelper.ReadServerCache(cachekey) as CategoryTB;
            if (result == null)
            {
                String sql = "select top 1 *,dbo.CategoryParentsName(autokey) as CategoryNav from category where autokey=@autokey";
                result = sql.ExecuteOneRecord<CategoryTB>("autokey", autokey);
                CacheHelper.CreateServerCache(cachekey, result);
            }
            return result;
        }

        public CategoryTB GetCategoryInfo(String cname, String colname = "CategoryName")
        {
            String cachekey = String.Format("GetCategoryInfo{0}", cname);
            CategoryTB result = CacheHelper.ReadServerCache(cachekey) as CategoryTB;
            if (result == null)
            {
                String sql = String.Format("select top 1 *,dbo.CategoryParentsName(autokey) as CategoryNav from category where {0}=@{0}", colname);
                result = sql.ExecuteOneRecord<CategoryTB>(colname, cname);
                CacheHelper.CreateServerCache(cachekey, result);
            }
            return result;
        }

        public int SaveCategoryInfo(CategoryModel model)
        {
            String sql = String.Empty;
            List<String> list = new List<String>() { 
                "CategoryName",model.CategoryName,
                "Description",model.Description.ToEmptyTrimString(),
                "Enable",model.Enable?"1":"0",
                "KeyWord",model.KeyWord.ToEmptyTrimString(),
                "ParentID",model.ParentID.ToString(),
                "SEOURL",model.SEOURL,
                "Autokey",model.AutoKey.ToString()
            };
            try
            {
                if (model.AutoKey > 0)
                {
                    sql = @"update category set 
                    CategoryName=@CategoryName,
                    Description=@Description,
                    Enable=@Enable,
                    KeyWord=@KeyWord,
                    ParentID=@ParentID,
                    SEOURL=@SEOURL where Autokey=@Autokey";
                    if (sql.ExecuteNoneQuery(list.ToArray()) > 0)
                    {
                        String cachekey = String.Format("GetCategoryInfo{0}", model.CategoryName);
                        CacheHelper.ClearServerCache(cachekey);
                        return model.AutoKey;
                    }
                }
                else
                {
                    sql = @"insert into category(CategoryName, KeyWord, Description, SEOURL, ParentID, Enable)
                    values(@CategoryName, @KeyWord, @Description, @SEOURL, @ParentID, @Enable) 
                    select isnull(SCOPE_IDENTITY(),0) as ID";
                    return sql.ExecuteScalarInt(list.ToArray()).ToInt32Value();

                }
            }
            catch (Exception ex)
            {
                ex.AddLog("CategoryServer", "SaveCategoryInfo");
            }
            return 0;
        }
    }
}
