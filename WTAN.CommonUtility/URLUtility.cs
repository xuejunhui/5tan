using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WTAN.CommonUtility
{
    public class URLUtility
    {

        public static String NetWordCategoryUrl(int pautokey, String pseourl, int autokey, String cseourl)
        {
            String url = "/";
            if (!pseourl.IsNullOrEmpty())
                url += pseourl;
            else if (pautokey > 0)
                url += pautokey;
            else
                return page404();
            url += "/";
            if (!cseourl.IsNullOrEmpty())
                url += cseourl;
            else if (autokey > 0)
                url += autokey;
            else
                return page404();

            return url;
        }

        public static String NetWordGuideUrl(int pautokey, String pseourl, int cautokey, String cseourl, int autokey)
        {
            String url = "/";
            if (!pseourl.IsNullOrEmpty())
                url += pseourl;
            else if (pautokey > 0)
                url += pautokey;
            else
                return page404();
            url += "/";
            if (!cseourl.IsNullOrEmpty())
                url += cseourl;
            else if (cautokey > 0)
                url += cautokey;
            else
                return page404();
            url += "/";
            if (autokey > 0)
                url += autokey + ".html";
            else
                return page404();

            return url;
        }

        public static String NetWordGuideUrl(int pautokey, String pseourl, int autokey)
        {
            String url = "/";
            if (!pseourl.IsNullOrEmpty())
                url += pseourl;
            else if (pautokey > 0)
                url += pautokey;
            else
                return page404();
            url += "/";
            if (autokey > 0)
                url += autokey + ".html";
            else
                return page404();

            return url;
        }

        public static String NetWrokContentUrl(int autokey, String seourl)
        {
            if (!seourl.IsNullOrEmpty())
                return "/" + seourl + ".html";
            else if (autokey > 0)
                return "/" + autokey + ".html";
            return page404();
        }

        #region Demo Url
        public static String SearchCategoryDetailUrl(String ctype, int categoryid)
        {
            if (ctype.Equals("Province", StringComparison.OrdinalIgnoreCase) || ctype.Equals("Domestic", StringComparison.OrdinalIgnoreCase) || ctype.Equals("Overseas", StringComparison.OrdinalIgnoreCase))
            {
                return CategoryUrl(categoryid,"");
            }
            else if (ctype.Equals("Guide", StringComparison.OrdinalIgnoreCase))
            {
                return GuideCenterUrl(categoryid);
            }
            else if (ctype.Equals("Question", StringComparison.OrdinalIgnoreCase))
            {
                return VisaQuestionUrl();
            }
            else if (ctype.Equals("Raiders", StringComparison.OrdinalIgnoreCase))
            {
                return RaiderCenterUrl();
            }
            else if (ctype.Equals("Visa", StringComparison.OrdinalIgnoreCase))
            {
                return VisaCenterUrl();
            }
            else if (ctype.Equals("Ticket", StringComparison.OrdinalIgnoreCase))
            {
                return TicketCenterUrl();
            }
            else
            {
                return page404();
            }
        }

        public static String SearchDetailUrl(String ctype,String seourl,int autokey)
        {
            if (ctype.Equals("Province", StringComparison.OrdinalIgnoreCase) || ctype.Equals("Domestic", StringComparison.OrdinalIgnoreCase) || ctype.Equals("Overseas", StringComparison.OrdinalIgnoreCase))
            {
                return SummerDetailUrl(autokey, seourl);
            }
            else if (ctype.Equals("Guide", StringComparison.OrdinalIgnoreCase))
            {
                return GuideDetailUrl(autokey);
            }
            else if (ctype.Equals("Question", StringComparison.OrdinalIgnoreCase))
            {
                return VisaQuestionDetailUrl(autokey,seourl);
            }
            else if (ctype.Equals("Raiders", StringComparison.OrdinalIgnoreCase))
            {
                return RaiderDetailUrl(autokey, seourl);
            }
            else if (ctype.Equals("Visa", StringComparison.OrdinalIgnoreCase))
            {
                return VisaDetailUrl(autokey);
            }
            else if (ctype.Equals("Ticket", StringComparison.OrdinalIgnoreCase))
            {
                return TicketDetailUrl(autokey);
            }
            else
            {
                return page404();
            }
        }

        public static String SearchUrl(String ctype = "", String keyword = "")
        {
            if (ctype.IsNullOrEmpty() && keyword.IsNullOrEmpty())
                return "/Search";
            return String.Format("/Search?ctype={0}&keyword={1}", ctype, keyword.UrlEncode());
        }

        public static String SearchUrlFormart(String ctype = "", String keyword = "")
        {
            if (ctype.IsNullOrEmpty() && keyword.IsNullOrEmpty())
                return "/Search?pageindex={0}";
            return String.Format("/Search?ctype={0}&keyword={1}", ctype, keyword.UrlEncode()) + "&pageindex={0}";
        }

        public static String ApplicationFriends()
        {
            return "/ApplicationFriends";
        }

        public static String MapUrl()
        {
            return "/Map";
        }

        public static String RaiderDetailUrl(int autokey, String seourl)
        {
            String url = String.Format("/RaiderDetail/{0}.html", seourl.IsNullOrEmpty() ? autokey.ToString() : seourl);
            return url;
        }

        public static String RaiderCenterUrlFormart()
        {
            return "/RaiderCenter?pageindex={0}";
        }

        public static String RaiderCenterUrl()
        {
            return "/RaiderCenter";
        }

        public static String TicketDetailUrl(int autokey)
        {
            return String.Format("/TicketCenter/{0}.html", autokey);
        }

        public static String TicketCenterUrl()
        {
            return "/TicketCenter";
        }

        public static String TicketCenterUrl(int pageindex, int categoryid)
        {
            return String.Format("/TicketCenter?pageindex={0}&categoryid={1}", pageindex, categoryid);
        }

        public static String TicketCenterUrlFormart(int categoryid)
        {
            return "/TicketCenter?pageindex={0}&categoryid=" + categoryid;
        }

        public static String ContactUrl(int autokey, String seourl)
        {
            String url = String.Format("/Contact/{0}.html", seourl.IsNullOrEmpty() ? autokey.ToString() : seourl);
            return url;

        }

        public static String GuideCenterUrlFormart(int categoryid, String range )
        {
            String url = GuideCenterUrl() + "?pageindex={0}";
            if (categoryid > 0)
                url += "&categoryid=" + categoryid;
            if (!range.IsNullOrEmpty())
                url += "&range=" + range;
            return url;
        }

        public static String GuideCenterUrl(int categoryid, String range = "")
        {
            String url = String.Format("/GuideCenter?pageindex=1&categoryid={0}&range={1}", categoryid, range);
            return url;
        }

        public static String GuideCenterUrl()
        {
            return "/GuideCenter";
        }

        public static String VisaQuestionUrl()
        {
            return "/VisaQuestion";
        }

        public static String VisaQuestionUrl(String keyword,int pageindex)
        {
            if (keyword.IsNullOrEmpty() && pageindex == 1)
                return VisaQuestionUrl();

            return String.Format("/VisaQuestion?pageindex={0}&keyword={1}", keyword.UrlEncode(), pageindex);
        }

        public static String VisaQuestionUrlFormart(String keyword)
        {
            if (keyword.IsNullOrEmpty())
                return VisaQuestionUrl() + "?pageindex={0}";
            return VisaQuestionUrl() + "?pageindex={0}&keyword=" + keyword.UrlEncode();
        }

        public static String VisaQuestionDetailUrl(int autokey, String seourl)
        {
            String url = "/VisaQuestionDetail{0}/{1}.html";
            if (!seourl.IsNullOrEmpty())
                return String.Format(url, "", seourl);
            else if (autokey > 0)
                return String.Format(url, "A", autokey);
            return page404();
        }

        public static String GuideDetailUrl(int autokey)
        {
            return String.Format("/GuideDetail/{0}.html", autokey);
        }

        public static String SummerDetailUrl(int autokey, String seourl)
        {
            String url = "/TravelDetail{0}/{1}.html";
            if (!seourl.IsNullOrEmpty())
                return String.Format(url, "", seourl);
            else if (autokey > 0)
                return string.Format(url, "A", autokey);
            return page404();
        }

        public static String VisaDetailUrl(int autokey)
        {
            return String.Format("/VisaCenter/{0}.html", autokey);
        }

        /// <summary>
        /// 簽證中心
        /// </summary>
        /// <returns></returns>
        public static String VisaCenterUrl()
        {
            return "/VisaCenter";
        }

        /// <summary>
        /// 分類
        /// </summary>
        /// <param name="autokey"></param>
        /// <param name="seourl"></param>
        /// <returns></returns>
        public static String CategoryUrl(int autokey, String seourl)
        {
            if (!seourl.IsNullOrEmpty())
                return String.Format("/CategoryU/{0}", seourl);
            else if (autokey > 0)
                return String.Format("/CategoryA/{0}", autokey);
            else
                return page404();
        }

        public static String CategoryUrl(int autokey, String seourl, String days, String traffic, String range)
        {
            String url = String.Empty;
            if (!days.IsNullOrEmpty())
                url += (url.IsNullOrEmpty() ? "?" : "&") + "days=" + days.UrlEncode();
            if (!traffic.IsNullOrEmpty())
                url += (url.IsNullOrEmpty() ? "?" : "&") + "traffic=" + traffic.UrlEncode();
            if (!range.IsNullOrEmpty())
                url += (url.IsNullOrEmpty() ? "?" : "&") + "range=" + range.UrlEncode();
            url = CategoryUrl(autokey, seourl) + url;
            return url;
        }

        public static String CategoryFormartUrl(int autokey, String seourl, String days, String traffic, String range)
        {
            String url = CategoryUrl(autokey, seourl) + "?pageIndex={0}";
            if (!days.IsNullOrEmpty())
                url += "&days=" + days.UrlEncode();
            if (!traffic.IsNullOrEmpty())
                url += "&traffic=" + traffic.UrlEncode();
            if (!range.IsNullOrEmpty())
                url += "&range=" + range.UrlEncode();
            return url;
        }
        #endregion

        /// <summary>
        /// 404page
        /// </summary>
        /// <returns></returns>
        public static String page404()
        {
            return "/Page404";
        }

        public static String AdminUrl {
            get {
                return "/";
            }
        }
    }
}
