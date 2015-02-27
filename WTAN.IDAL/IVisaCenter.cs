using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;

namespace WTAN.IDAL
{
    public interface IVisaCenter
    {
        /// <summary>
        /// 獲取推薦列表
        /// </summary>
        /// <param name="vtype"></param>
        /// <param name="recommendNum"></param>
        /// <param name="top"></param>
        /// <param name="isAll">是否包含所有內容</param>
        /// <returns></returns>
        List<VisaCenterTB> GetTopVisas(String vtype, int recommendNum, int top, Boolean isAll);

        /// <summary>
        /// 分頁列表
        /// </summary>
        /// <param name="vtype"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="categoryid"></param>
        /// <returns></returns>
        List<VisaCenterTB> GetVisas(String vtype, int pageSize, out int rowCount, int pageIndex, int categoryid);

        /// <summary>
        /// 獲取簽證或票務的推薦
        /// </summary>
        /// <param name="vtype"></param>
        /// <param name="recommendNum"></param>
        /// <returns></returns>
        List<VisaCenterTB> GetTopVisas(String vtype, int recommendNum, int top, int categoryid);

        /// <summary>
        /// 修改置顶状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="topstate"></param>
        /// <returns></returns>
        Boolean UpdateVisaCenterTop(String id, int topstate);

        /// <summary>
        /// 根據多個主鍵 獲得簽證列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<VisaCenterTB> GeVisaList(String id);

        /// <summary>
        /// 刪除簽證
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Boolean DelVisas(String id);

        /// <summary>
        /// 修改簽證狀態
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        Boolean UpdateVisaState(String id, int state);

        /// <summary>
        /// 保存簽證信息
        /// </summary>
        /// <param name="visa"></param>
        /// <returns></returns>
        int SaveVisa(VisaCenterTB visa);

        VisaCenterTB GetVisa(String vname);

        /// <summary>
        /// 獲取單條簽證信息
        /// </summary>
        /// <param name="autokey"></param>
        /// <returns></returns>
        VisaCenterTB GetVisa(int autokey);

        /// <summary>
        /// 簽證列表數據
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="IsEnable"></param>
        /// <param name="Range"></param>
        /// <returns></returns>
        List<VisaCenterTB> GetVisas(String vtype, String keyword, int pageSize, out int rowCount, int pageIndex, String IsEnable, String Range, String TopState);
    }
}
