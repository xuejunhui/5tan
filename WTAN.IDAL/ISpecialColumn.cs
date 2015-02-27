using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;
using WTAN.CommonUtility;

namespace WTAN.IDAL
{
    public interface ISpecialColumn
    {
        /// <summary>
        /// 获取关于我们或签证问题 列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="topnum"></param>
        /// <returns></returns>
        List<SpecialColumnTB> GetSpecialColumns(SpecialColumnType type, int topnum);

        /// <summary>
        /// 根据主键字符串 获取多条信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<SpecialColumnTB> GeSpecialColumnList(String id);

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Boolean DelSpecialColumns(String id);

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        Boolean UpdateSpecialColumnState(String id, int state);

        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="guide"></param>
        /// <returns></returns>
        int SaveSpecialColumn(SpecialColumnTB guide);

        /// <summary>
        /// 根据主键获取单条信息
        /// </summary>
        /// <param name="autokey"></param>
        /// <returns></returns>
        SpecialColumnTB GetSpecialColumn(int autokey);

        /// <summary>
        /// 根据seourl 获取单条信息
        /// </summary>
        /// <param name="seourl"></param>
        /// <returns></returns>
        SpecialColumnTB GetSpecialColumn(String seourl);

        /// <summary>
        /// 获取特色栏位列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="IsEnable"></param>
        /// <returns></returns>
        List<SpecialColumnTB> GetSpecialColumns(SpecialColumnType type, String keyword, int pageSize, out int rowCount, int pageIndex, String IsEnable);
    }
}
