using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.CommonUtility;
using WTAN.Model.DModel;

namespace WTAN.IDAL
{
    public interface IFriendLink
    {
        List<FriendLinkTB> GetFriendLinks(WebName webname);

        /// <summary>
        /// 删除友链
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Boolean DelFriendLink(String id);

        /// <summary>
        /// 审核友链
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        Boolean UpdateFriendLinkState(String id, int state);

        /// <summary>
        /// 保存友链
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        int SaveFriendLink(FriendLinkTB f);

        /// <summary>
        /// 根据主键获取友链
        /// </summary>
        /// <param name="autokey"></param>
        /// <returns></returns>
        FriendLinkTB GetFriendLink(int autokey);

        /// <summary>
        /// 根据某一字段获取友链
        /// </summary>
        /// <param name="colname"></param>
        /// <param name="colvalue"></param>
        /// <returns></returns>
        FriendLinkTB GetFriendLink(String colname, String colvalue);

        /// <summary>
        /// 友链分页列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="IsEnable"></param>
        /// <returns></returns>
        List<FriendLinkTB> GetFriendLinks(String keyword, int pageSize, out int rowCount, int pageIndex, String IsEnable);
    }
}
