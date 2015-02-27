using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WTAN.Model.DModel;
using WTAN.BLL;
using WTAN.CommonUtility;

namespace Network.Models
{
    public class NavModel
    {
        public List<NavItem> Navs { get; set; }

        private int _NavsCount = 0;
        public int NavsCount
        {
            get {
                if (_NavsCount == 0)
                    _NavsCount = Navs.Count();
                return _NavsCount;
            }
        }
    }

    public class NavItem
    {
        public String NavName { get; set; }

        public String NavUrl { get; set; }
    }

    public class ViewModel
    {
        private List<ContentTB> _HeardData=null;
        public List<ContentTB> HeardData
        {
            get {
                if (_HeardData == null)
                    _HeardData = CurrentBLL.CBLL.GetContents(WebName.NetWork, 1, 9);
                return _HeardData;
            }
        }

        public ViewModel(String seourl = "",String cseourl="")
        {
            if (seourl.IsNullOrEmpty())
            {
                CurrentHeard = new ContentTB()
                {
                    AutoKey = 0
                };
                return;
            }
            CurrentHeard = CurrentBLL.CBLL.GetContentInfo("SEOURL", seourl);
            if (CurrentHeard == null && seourl.IsNumber())
                CurrentHeard = CurrentBLL.CBLL.GetContentInfo(seourl.ToInt32Value());
            if (IsWebsiteCase || IsNews)
            {
                if (!cseourl.IsNullOrEmpty())//设置当前分类
                {
                    _CurrentCategory = CurrentBLL.CAbll.GetCategoryBySEO(cseourl);
                    if (_CurrentCategory == null && cseourl.IsNumber())
                        _CurrentCategory = CurrentBLL.CAbll.GetCategory(cseourl.ToInt32Value());
                }
                else
                    _CurrentCategory = CurrentBLL.CAbll.GetCategory(CurrentHeard.CategoryID);
            }
        }

        /// <summary>
        /// 是否为案例
        /// </summary>
        public Boolean IsWebsiteCase
        {
            get
            {
                if (CurrentHeard == null)
                    return false;
                return CurrentHeard.SEOURL.ToEmptyTrimString().Equals("WebsiteCase", StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// 是否为新闻
        /// </summary>
        public Boolean IsNews
        {
            get
            {
                if (CurrentHeard == null)
                    return false;
                return CurrentHeard.SEOURL.ToEmptyTrimString().Equals("News", StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// 添加到头面包屑导航中
        /// </summary>
        /// <param name="navname"></param>
        /// <param name="url"></param>
        public void AddNav(String navname, String url)
        {
            if (navname.IsNullOrEmpty() || url.IsNullOrEmpty())
                return;
            this.Nav.Navs.Add(new NavItem
            {
                NavName = navname,
                NavUrl = url
            });
        }

        private NavModel _Nav = null;
        public NavModel Nav
        {
            get
            {
                if (_Nav == null)
                    _Nav = new NavModel()
                    {
                        Navs = new List<NavItem>() {
                            new NavItem(){
                                NavName="网站首页",
                                NavUrl="/"
                            }
                        }
                    };
                return _Nav;
            }
            set {
                if (value != null)
                    _Nav = value;
            }
        }

        private CategoryTB _CurrentCategory = null;
        /// <summary>
        /// 当前所属分类
        /// </summary>
        public CategoryTB CurrentCategory
        {
            get
            {
                if (_CurrentCategory == null)
                    _CurrentCategory = CurrentBLL.CAbll.GetCategory(CurrentHeard.CategoryID);//打开页面时默认列表
                return _CurrentCategory;
            }
        }

        private List<CategoryTB> _CategoryList = null;
        public List<CategoryTB> CategoryList
        {
            get
            {
                if (_CategoryList != null)
                    return _CategoryList;
                if (_CategoryList == null && CurrentHeard != null)
                {
                    _CategoryList = CurrentBLL.CAbll.GetCategorysByParentID(CurrentHeard["CParentID"].ToInt32Value());
                    return _CategoryList;
                }
                else
                    return new List<CategoryTB>();
            }
        }

        /// <summary>
        /// 页面关键字
        /// </summary>
        private String _Keywords = String.Empty;
        public String Keywords { 
            get{
                if(_Keywords.IsNullOrEmpty())
                    _Keywords =  ConfigBLL.CurrentNetWorkSysConfigInfo.Sys_Value.Keyword;
                return _Keywords;
            }
            set
            {
                if (!value.IsNullOrEmpty())
                    _Keywords = value;
            }
        }

        /// <summary>
        /// 网页标题
        /// </summary>
        private String _Title = String.Empty;
        public String Title
        {
            get
            {
                if (_Title.IsNullOrEmpty())
                    _Title = ConfigBLL.CurrentNetWorkSysConfigInfo.Sys_Value.Title;
                return _Title;
            }
            set {
                if (!value.IsNullOrEmpty())
                    _Title = value;
            }
        }

        /// <summary>
        /// 网页描述
        /// </summary>
        private String _Description = String.Empty;
        public String Description {
            get {
                if (_Description.IsNullOrEmpty())
                    _Description = ConfigBLL.CurrentNetWorkSysConfigInfo.Sys_Value.Description;
                return _Description;
            }
            set {
                if (!value.IsNullOrEmpty())
                    _Description = value;
            }
        }

        /// <summary>
        /// 当前头部内容
        /// </summary>
        public ContentTB CurrentHeard { get; set; }
    }
}