using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.CommonUtility;
using WTAN.Model.DModel;

namespace WTAN.IDAL
{
    public interface IContent
    {
        /// <summary>
        /// 獲取某一推薦的頭幾條數據
        /// </summary>
        /// <param name="webname"></param>
        /// <param name="recommendNum"></param>
        /// <param name="maxtop"></param>
        /// <returns></returns>
        List<ContentTB> GetContents(WebName webname, int recommendNum, int maxtop);

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="rowcount"></param>
        /// <param name="ctype"></param>
        /// <param name="urlformart"></param>
        /// <returns></returns>
        List<RecordItem> GetSearchAllContent(WebName webname,String keyword, int pageindex, int pagesize, out int rowcount, String ctype, String urlformart);

        /// <summary>
        /// 獲取獨立組團或散客拼團的列表
        /// </summary>
        /// <param name="offeredType"></param>
        /// <param name="topnum"></param>
        /// <param name="recommendNum"></param>
        /// <param name="isAll"></param>
        /// <returns></returns>
        List<ContentTB> GetOfferedTypeTopContent(String offeredType, int topnum, int recommendNum, Boolean isAll);

        /// <summary>
        /// 获取相关的旅游信息
        /// </summary>
        /// <param name="categoryname"></param>
        /// <returns></returns>
        List<ContentTB> GetRelatedContent(WebName webname, String categoryname, int autokey, int topnum, int recommendNum, Boolean isAll);

        /// <summary>
        /// 修改内容置顶状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="topstate"></param>
        /// <returns></returns>
        Boolean UpdateContentTop(String id, int topstate);

        /// <summary>
        /// 修改内容状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        Boolean UpdateContentState(String id, int state);

        /// <summary>
        /// 根据主键获取内容列表 多个主键用,分割
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<ContentTB> GetContentList(String id);

        /// <summary>
        /// 根据主键删除内容 多个主键用,分割
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Boolean DelContentInfo(String id);

        /// <summary>
        /// 保存内容
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        int SaveContentInfo(ContentTB tb);

        /// <summary>
        /// 获取搜索分页数据
        /// </summary>
        /// <param name="keyword">搜索关键字</param>
        /// <param name="pageSize">每页显示数据条数</param>
        /// <param name="rowCount">数据总条数</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        List<ContentTB> GetContents(WebName webname, String keyword, int pageSize, out int rowCount, int pageIndex, String IsEnable, String Range, String TopState);

        /// <summary>
        /// 前端分类页面显示
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="Range"></param>
        /// <param name="Days"></param>
        /// <param name="Traffic"></param>
        /// <returns></returns>
        List<ContentTB> GetContents(WebName webname, int pageSize, out int rowCount, int pageIndex, String Range, String Days, String Traffic);

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="colname">字段名</param>
        /// <param name="colvalue">字段值</param>
        /// <returns></returns>
        ContentTB GetContentInfo(String colname, String colvalue);
    }
}
