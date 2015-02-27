using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.CommonUtility;
using WTAN.Model.DModel;

namespace WTAN.IDAL
{
    public interface IGuide
    {

        List<GuideTB> GetGuides(WebName webname, String keyword, int pageSize, out int rowCount, int pageIndex, int categoryid);

        /// <summary>
        /// 推荐置顶
        /// </summary>
        /// <param name="id"></param>
        /// <param name="topstate"></param>
        /// <returns></returns>
        Boolean UpdateGuideTop(String id, int topstate);

        /// <summary>
        /// 前端显示列表数据
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="Range"></param>
        /// <returns></returns>
        List<GuideTB> GetGuides(WebName webname, String keyword, int pageSize, out int rowCount, int pageIndex, String Range);

        /// <summary>
        /// 获取相关指南Top 10
        /// </summary>
        /// <param name="categoryname"></param>
        /// <param name="autokey"></param>
        /// <returns></returns>
        List<GuideTB> GetRelatedGuides(WebName webname, String categoryname, int autokey, int topnum, int recommendNum);

        /// <summary>
        /// 修改指南狀態
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        Boolean UpdateGuideState(String id, int state);

        /// <summary>
        /// 刪除指南
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Boolean DelGuides(String id);

        /// <summary>
        /// 保存目的地指南信息
        /// </summary>
        /// <param name="guide"></param>
        /// <returns></returns>
        int SaveGuide(GuideTB guide);

        /// <summary>
        /// 获取单条目的地指南
        /// </summary>
        /// <param name="autokey"></param>
        /// <returns></returns>
        GuideTB GetGuide(int autokey);

        /// <summary>
        /// 根據關聯guid獲得 目的地列表
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        List<GuideTB> GetGuides(String guid,int top);

        /// <summary>
        /// 获取目的地指南列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="IsEnable"></param>
        /// <returns></returns>
        List<GuideTB> GetGuides(WebName webname, String keyword, int pageSize, out int rowCount, int pageIndex, String IsEnable, String guid, String Range, String TopState);
    }
}
