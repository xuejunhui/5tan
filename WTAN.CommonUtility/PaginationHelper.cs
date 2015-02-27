using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WTAN.CommonUtility
{
    /// <summary>
    /// 分頁處理
    /// </summary>
    public class PaginationHelper
    {
        public PaginationHelper(int pageIndex, int dataCount, int pageSize, int buttonCount, String paddingUrlFormat, String formAction = "", Boolean dispalySort = false)
        {
            this._PageIndex = pageIndex;
            this._DataCount = dataCount;
            this._PageSize = pageSize;
            this._ButtonCount = buttonCount;
            this._PaddingUrlFormat = paddingUrlFormat;
            this._FormAction = formAction;
            this._IsDispalySort = dispalySort;
        }

        private Boolean _IsDispalySort;
        public Boolean IsDispalySort
        {
            get { return _IsDispalySort; }
            set { _IsDispalySort = value; }
        }

        private String _FormAction;
        /// <summary>
        /// 排序时提交的表单路径
        /// </summary>
        public String FormAction
        {
            get { return _FormAction; }
            set { _FormAction = value; }
        }

        private String _PaddingUrlFormat;
        /// <summary>
        /// 分頁路徑格式 {0}替代pageindex
        /// </summary>
        public String PaddingUrlFormat
        {
            get { return _PaddingUrlFormat; }
            set { _PaddingUrlFormat = value; }
        }

        private int _ButtonCount = 8;
        /// <summary>
        /// 分頁顯示項個數
        /// </summary>
        public int ButtonCount
        {
            get { return _ButtonCount; }
            set { _ButtonCount = value; }
        }

        private int _PageIndex = 1;
        /// <summary>
        /// 當前頁
        /// </summary>
        public int PageIndex
        {
            get { return _PageIndex < 1 ? 1 : _PageIndex; }
            set { _PageIndex = value; }
        }

        private int _DataCount = 0;
        /// <summary>
        /// 總數量
        /// </summary>
        public int DataCount
        {
            get { return _DataCount < 0 ? 0 : _DataCount; }
            set { _DataCount = value; }
        }

        private int _PageSize = 0;
        /// <summary>
        /// 每頁顯示產品數量
        /// </summary>
        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }

        /// <summary>
        /// 當前總頁數
        /// </summary>
        public int PageCount
        {
            get
            {
                return DataCount / PageSize + (DataCount % PageSize == 0 ? 0 : 1);
            }
        }

        /// <summary>
        /// 分頁
        /// </summary>
        /// <param name="ButtonCount"></param>
        /// <param name="PaddingUrlFormat"></param>
        /// <returns></returns>
        public HtmlString PagingHtml()
        {
            if (ButtonCount < 1)
                ButtonCount = 8;
            if (PageCount < 2)
                return null;
            StringBuilder pager = new StringBuilder();
            pager.Append("<div class=\"lb_menu cl\">");
            string first_page_button = "";//第一页的地址
            string previous_page_button = "";//前一页的地址
            string next_page_button = "";//后一页的地址            
            string last_page_button = "";//最后一页的地址

            if (PageIndex > 1)//说明当前页不是第一页，那么就需要显示第一页的链接和上一页的链接
            {
                first_page_button = "<a href=\"" + string.Format(PaddingUrlFormat, 1) + "\">第一页</a> ";
                previous_page_button = "<a href=\"" + string.Format(PaddingUrlFormat, PageIndex - 1) + "\">上一页</a> ";
            }
            if (PageCount > 1 && PageIndex < PageCount)//说明至少有两页，而且当前页不是最后一页，那么则需要显示最后一页的链接和下一页的链接
            {
                next_page_button = "<a href=\"" + string.Format(PaddingUrlFormat, PageIndex + 1) + "\">下一页</a> ";//<span style=\"padding-left:5px;\"><img src=\"Content/images/ic2.gif\" /></span>
                last_page_button = "<a href=\"" + string.Format(PaddingUrlFormat, PageCount) + "\">最后页</a> ";
            }
            pager.Append(first_page_button).Append(previous_page_button);
            if (PageCount > ButtonCount)//说明当前记录集的页数大于指定显示的按钮个数
            {
                int center_ButtonCount = 0;//数字按钮的中间数 
                if (ButtonCount % 2 == 0)
                    center_ButtonCount = ButtonCount / 2;
                else
                    center_ButtonCount = ButtonCount / 2 + 1;
                if (PageIndex - center_ButtonCount > 0)//说明当前页号前边有大于数字按钮一半长度的数字按钮
                {
                    if (PageIndex != PageCount)
                    {
                        int from_index = PageIndex - center_ButtonCount;//从超出部分开始

                        int count = 0;
                        while (count < center_ButtonCount && from_index < PageIndex)
                        {
                            count++;
                            pager.Append("<a href=\"").Append(string.Format(PaddingUrlFormat, from_index)).Append("\">").Append(from_index).Append("</a> ");
                            from_index++;
                        }
                        pager.Append("<a class=\"hover\" href=\"javascript:void(0);\">").Append(PageIndex).Append("</a> ");
                        count = 0;
                        from_index = PageIndex + 1;
                        while (count < center_ButtonCount && from_index <= PageCount)
                        {
                            count++;
                            pager.Append("<a href=\"").Append(string.Format(PaddingUrlFormat, from_index)).Append("\">").Append(from_index).Append("</a> ");
                            from_index++;
                        }
                    }
                    else
                    {
                        int from_index = PageIndex - ButtonCount + 1;//从超出部分开始

                        int count = 0;
                        while (count < ButtonCount && from_index < PageCount)
                        {
                            count++;
                            pager.Append("<a href=\"").Append(string.Format(PaddingUrlFormat, from_index)).Append("\">").Append(from_index).Append("</a> ");
                            from_index++;
                        }
                        pager.Append("<a class=\"hover\" href=\"javascript:void(0);\">").Append(PageIndex).Append("</a> ");
                    }
                }
                else
                {
                    int from_index = 1;//说明当前页前边没有足够的数字按钮，所以只能从1开始

                    int count = 1;
                    while (count < PageIndex && PageIndex != 1)//从1开始循环到当前页位置
                    {
                        count++;
                        pager.Append("<a href=\"").Append(string.Format(PaddingUrlFormat, from_index)).Append("\">").Append(from_index).Append("</a> ");
                        from_index++;
                    }
                    pager.Append("<a class=\"hover\" href=\"javascript:void(0);\">").Append(PageIndex).Append("</a> ");
                    count = 0;
                    from_index = PageIndex + 1;
                    while (count < ButtonCount - PageIndex && from_index <= PageCount)//循环当前页后半部分
                    {
                        count++;
                        pager.Append("<a href=\"").Append(string.Format(PaddingUrlFormat, from_index)).Append("\">").Append(from_index).Append("</a> ");
                        from_index++;
                    }
                }
            }
            else
            {
                for (int i = 1; i <= PageCount; i++)
                {
                    if (i != PageIndex)
                        pager.Append("<a href=\"").Append(string.Format(PaddingUrlFormat, i)).Append("\">").Append(i).Append("</a> ");
                    else
                        pager.Append("<a class=\"hover\" href=\"javascript:void(0);\">").Append(i).Append("</a> ");
                }
            }
            pager.Append(next_page_button).Append(last_page_button);
            pager.Append("</div>");
            return new HtmlString(pager.ToString());
        }

        public HtmlString AdminPagingHtml(Boolean isAll = true)
        {
            if (ButtonCount < 1)
                ButtonCount = 8;

            StringBuilder pager = new StringBuilder();
            if (isAll)
            {
                pager.Append("<div class=\"mesinfo\">");
                pager.AppendFormat("共<i class=\"blue\">{0}</i>条记录，当前显示第&nbsp;<i class=\"blue\">{1}&nbsp;</i>页", this.DataCount, PageIndex);
                pager.Append("</div>");
            }
            pager.Append("<ul class=\"paginList\">");
            string first_page_button = "";//第一页的地址
            string previous_page_button = "";//前一页的地址
            string next_page_button = "";//后一页的地址            
            string last_page_button = "";//最后一页的地址

            if (PageIndex > 1)//说明当前页不是第一页，那么就需要显示第一页的链接和上一页的链接
            {
                first_page_button = "<li class=\"paginItem\"><a class=\"pagepres\" href=\"" + string.Format(PaddingUrlFormat, 1) + "\">&nbsp;</a></li> ";
                previous_page_button = "<li class=\"paginItem\"><a class=\"pagepre\" href=\"" + string.Format(PaddingUrlFormat, PageIndex - 1) + "\">&nbsp;</a></li>";
            }
            if (PageCount > 1 && PageIndex < PageCount)//说明至少有两页，而且当前页不是最后一页，那么则需要显示最后一页的链接和下一页的链接
            {
                next_page_button = "<li class=\"paginItem\"><a class=\"pagenxt\" href=\"" + string.Format(PaddingUrlFormat, PageIndex + 1) + "\">&nbsp;</a> </li>";//<span style=\"padding-left:5px;\"><img src=\"Content/images/ic2.gif\" /></span>
                last_page_button = "<li class=\"paginItem\"><a class=\"pagenxts\" href=\"" + string.Format(PaddingUrlFormat, PageCount) + "\">&nbsp;</a> </li>";
            }
            pager.Append(first_page_button).Append(previous_page_button);
            if (PageCount > ButtonCount)//说明当前记录集的页数大于指定显示的按钮个数
            {
                int center_ButtonCount = 0;//数字按钮的中间数 
                if (ButtonCount % 2 == 0)
                    center_ButtonCount = ButtonCount / 2;
                else
                    center_ButtonCount = ButtonCount / 2 + 1;
                if (PageIndex - center_ButtonCount > 0)//说明当前页号前边有大于数字按钮一半长度的数字按钮
                {
                    if (PageIndex != PageCount)
                    {
                        int from_index = PageIndex - center_ButtonCount;//从超出部分开始

                        int count = 0;
                        while (count < center_ButtonCount && from_index < PageIndex)
                        {
                            count++;
                            pager.Append("<li class=\"paginItem\"><a class=\"number\" href=\"").Append(string.Format(PaddingUrlFormat, from_index)).Append("\">").Append(from_index).Append("</a> </li>");
                            from_index++;
                        }
                        pager.Append("<li class=\"paginItem current\"><a class=\"number\" href=\"javascript:void(0);\">").Append(PageIndex).Append("</a> </li>");
                        count = 0;
                        from_index = PageIndex + 1;
                        while (count < center_ButtonCount && from_index <= PageCount)
                        {
                            count++;
                            pager.Append("<li class=\"paginItem\"><a class=\"number\" href=\"").Append(string.Format(PaddingUrlFormat, from_index)).Append("\">").Append(from_index).Append("</a> </li>");
                            from_index++;
                        }
                    }
                    else
                    {
                        int from_index = PageIndex - ButtonCount + 1;//从超出部分开始

                        int count = 0;
                        while (count < ButtonCount && from_index < PageCount)
                        {
                            count++;
                            pager.Append("<li class=\"paginItem\"><a class=\"number\" href=\"").Append(string.Format(PaddingUrlFormat, from_index)).Append("\">").Append(from_index).Append("</a> </li>");
                            from_index++;
                        }
                        pager.Append("<li class=\"paginItem current\"><a class=\"number\" href=\"javascript:void(0);\">").Append(PageIndex).Append("</a> </li>");
                    }
                }
                else
                {
                    int from_index = 1;//说明当前页前边没有足够的数字按钮，所以只能从1开始

                    int count = 1;
                    while (count < PageIndex && PageIndex != 1)//从1开始循环到当前页位置
                    {
                        count++;
                        pager.Append("<li class=\"paginItem\"><a class=\"number\" href=\"").Append(string.Format(PaddingUrlFormat, from_index)).Append("\">").Append(from_index).Append("</a> </li>");
                        from_index++;
                    }
                    pager.Append("<li class=\"paginItem current\"><a class=\"number\" href=\"javascript:void(0);\">").Append(PageIndex).Append("</a> </li>");
                    count = 0;
                    from_index = PageIndex + 1;
                    while (count < ButtonCount - PageIndex && from_index <= PageCount)//循环当前页后半部分
                    {
                        count++;
                        pager.Append("<li class=\"paginItem\"><a class=\"number\" href=\"").Append(string.Format(PaddingUrlFormat, from_index)).Append("\">").Append(from_index).Append("</a> </li>");
                        from_index++;
                    }
                }
            }
            else
            {
                for (int i = 1; i <= PageCount; i++)
                {
                    if (i != PageIndex)
                        pager.Append("<li class=\"paginItem\"><a class=\"number\" href=\"").Append(string.Format(PaddingUrlFormat, i)).Append("\">").Append(i).Append("</a> </li>");
                    else
                        pager.Append("<li class=\"paginItem current\"><a class=\"number\" href=\"javascript:void(0);\">").Append(i).Append("</a> </li>");
                }
            }
            pager.Append(next_page_button).Append(last_page_button);
            pager.Append("</ul>");
            return new HtmlString(pager.ToString());
        }
    }
}
