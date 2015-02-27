using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//整站枚舉
namespace WTAN.CommonUtility
{
    public enum WebName
    {
        NetWork,
        Demo
    }

    /// <summary>
    /// VisaQuestion 签证常见问题
    /// AboutUs 关于我们
    /// </summary>
    public enum SpecialColumnType
    {
        VisaQuestion = 2,
        AboutUs = 4,
        Raiders = 7
    }

    /// <summary>
    /// 文本编辑器所在页面类型
    /// Travel 旅游
    /// Special 特色
    /// Guide 指南
    /// Visa 签证
    /// </summary>
    public enum UploadPage
    {
        Travel,
        Special,
        Guide,
        Visa,
        Network
    }

    public enum UploadType
    {
        File,
        Img
    }

    /// <summary>
    /// 下拉框状态
    /// TravelingDays 旅游天数
    /// Transport 交通工具
    /// CategoryID 范围分类
    /// OfferedType 组团类型
    /// TicketCategory 票务预定
    /// </summary>
    public enum DropDownState
    {
        TravelingDays = 48,
        Transport = 58,
        CategoryID = 4,
        OfferedType = 5,
        VisaCategory = 31,
        TicketCategory = 8,
        NetworkCategory = 9
    }

    /// <summary>
    /// 登陸狀態
    /// Success 成功
    /// UserNameErr 用戶名錯誤
    /// PassWordErr 密碼錯誤
    /// Lock 帳戶被鎖定
    /// </summary>
    public enum LoginState
    { 
        Success,
        UserNameErr,
        PassWordErr,
        Lock

    }

    /// <summary>
    /// 提示信息状态
    /// </summary>
    public enum MessageState
    {
        success,
        error,
        info,
        block
    }

    /// <summary>
    /// 广告位位置
    /// </summary>
    /// MainAdsSlide 首页幻灯
    /// MainAdsBanner1 首页Banner1
    /// MainAdsBanner2 首页Banner2
    /// AdsMainContact 首頁右側聯繫我們圖片
    public enum AdsPos
    {
        AdsMainSlide,
        AdsMainBanner1,
        AdsMainBanner2,
        AdsMainContact,
        VisaCenterYaZhou,
        VisaCenterFeiZhou,
        VisaCenterOuZhou,
        VisaCenterMeiZhou,
        VisaCenterTaiPingYang,
        MainTopScroll
    }

    /// <summary>
    /// NetWork 網站廣告位
    /// </summary>
    public enum AdsNetWorkPos
    { 
        MainSlideTxt
    }

    /// <summary>
    /// 广告类型
    /// Text 纯文本广告
    /// Image 图片广告
    /// Script 脚本广告
    /// </summary>
    public enum AdsType
    { 
        Text,
        Image,
        Script,
        Slide,
        Link
    }

    public static class EnumCommon
    {
        public static String GetAdsNetWorkName(this AdsNetWorkPos ap)
        {
            String result = String.Empty;
            switch (ap)
            {
                case AdsNetWorkPos.MainSlideTxt: result = "首页幻灯文字"; break;
            }

            return result;
        }

        public static AdsType GetAdsNetWorkType(this AdsNetWorkPos ap)
        {
            AdsType t = AdsType.Text;
            switch (ap)
            {
                case AdsNetWorkPos.MainSlideTxt: t = AdsType.Link; break;
            }

            return t;
        }

        /// <summary>
        /// 获取广告位名称
        /// </summary>
        /// <param name="ap"></param>
        /// <returns></returns>
        public static String GetAdsPosName(this AdsPos ap)
        {
            String result = String.Empty;
            switch (ap)
            {
                case AdsPos.AdsMainSlide: result = "首页幻灯广告"; break;
                case AdsPos.AdsMainBanner1: result = "首页Banner上"; break;
                case AdsPos.AdsMainBanner2: result = "首页Banner下"; break;
                case AdsPos.AdsMainContact: result = "首页右侧联系图片"; break;
                case AdsPos.VisaCenterFeiZhou: result = "签证中心非洲"; break;
                case AdsPos.VisaCenterMeiZhou: result = "签证中心美洲"; break;
                case AdsPos.VisaCenterOuZhou: result = "签证中心欧洲"; break;
                case AdsPos.VisaCenterTaiPingYang: result = "签证中心太平洋"; break;
                case AdsPos.VisaCenterYaZhou: result = "签证中心亚洲"; break;
                case AdsPos.MainTopScroll: result = "首页头部滚动"; break;
                default: result = ap.ToString(); break;
            }
            return result;
        }

        //public static AdsType GetAdsType(this AdsPos ap)
        //{
        //    AdsType t= AdsType.Text;
        //    switch (ap)
        //    {
        //        case AdsPos.AdsMainSlide: t = AdsType.Slide; break;
        //        case AdsPos.AdsMainBanner1: t = AdsType.Image; break;
        //        case AdsPos.AdsMainBanner2: t = AdsType.Image; break;
        //        case AdsPos.AdsMainContact: t = AdsType.Image; break;
        //        case AdsPos.VisaCenterFeiZhou: t = AdsType.Link; break;
        //        case AdsPos.VisaCenterMeiZhou: t = AdsType.Link; break;
        //        case AdsPos.VisaCenterOuZhou: t = AdsType.Link; break;
        //        case AdsPos.VisaCenterTaiPingYang: t = AdsType.Link; break;
        //        case AdsPos.VisaCenterYaZhou: t = AdsType.Link; break;
        //        case AdsPos.MainTopScroll: t = AdsType.Link; break;
        //    }
        //    return t;
        //}

        public static AdsType GetAdsType(this String ap)
        {
            AdsType t = AdsType.Text;
            if (ap.Equals(AdsPos.AdsMainSlide.ToString(), StringComparison.OrdinalIgnoreCase))
                return t = AdsType.Slide;
            if (ap.Equals(AdsPos.AdsMainBanner1.ToString(), StringComparison.OrdinalIgnoreCase))
                return t = AdsType.Image;
            if (ap.Equals(AdsPos.AdsMainBanner2.ToString(), StringComparison.OrdinalIgnoreCase))
                return t = AdsType.Image;
            if (ap.Equals(AdsPos.AdsMainContact.ToString(), StringComparison.OrdinalIgnoreCase))
                return t = AdsType.Image;
            if (ap.Equals(AdsPos.VisaCenterFeiZhou.ToString(), StringComparison.OrdinalIgnoreCase))
                return t = AdsType.Link;
            if (ap.Equals(AdsPos.VisaCenterMeiZhou.ToString(), StringComparison.OrdinalIgnoreCase))
                return t = AdsType.Link;
            if (ap.Equals(AdsPos.VisaCenterOuZhou.ToString(), StringComparison.OrdinalIgnoreCase))
                return t = AdsType.Link;
            if (ap.Equals(AdsPos.VisaCenterTaiPingYang.ToString(), StringComparison.OrdinalIgnoreCase))
                return t = AdsType.Link;
            if (ap.Equals(AdsPos.VisaCenterYaZhou.ToString(), StringComparison.OrdinalIgnoreCase))
                return t = AdsType.Link;
            if (ap.Equals(AdsPos.MainTopScroll.ToString(), StringComparison.OrdinalIgnoreCase))
                return t = AdsType.Link;

            if (ap.Equals(AdsNetWorkPos.MainSlideTxt.ToString(), StringComparison.OrdinalIgnoreCase))
                return t = AdsType.Link;
            
            return t;
        }

        public static WebName ToWebName(this String ap)
        {
            WebName p;
            Enum.TryParse<WebName>(ap, out p);
            return p;
        }

        public static AdsPos ToAdsPos(this String ap)
        {
            AdsPos p;
            Enum.TryParse<AdsPos>(ap, out p);
            return p;
        }

        public static AdsNetWorkPos ToAdsNetWorkPos(this String ap)
        {
            AdsNetWorkPos p;
            Enum.TryParse<AdsNetWorkPos>(ap, out p);
            return p;
        }

    }
}
