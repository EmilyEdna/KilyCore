using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Cook;
using KilyCore.DataEntity.ResponseMapper.Cook;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ICookService
* 类 描 述 ：
* 命名空间 ：KilyCore.Service.IServiceCore
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/24 14:25:12
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 乡村厨师业务逻辑接口
    /// </summary>
    public interface ICookService : IService
    {
        #region 厨师菜单
        IList<ResponseCookMenu> AddCookParentMenu();
        ResponseCookMenu GetCookMenuDetail(Guid Id);
        PagedResult<ResponseCookMenu> GetCookMenuPage(PageParamList<RequestCookMenu> pageParam);
        String RemoveCookMenu(Guid Id);
        String EditCookMenu(RequestCookMenu Param);
        #endregion

        #region 权限菜单树
        IList<ResponseParentTree> GetCookTree();
        #endregion

        #region 厨师角色
        PagedResult<ResponseCookRole> GetCookAuthorPage(PageParamList<RequestCookRole> pageParam);
        String EditRole(RequestCookRole Param);
        String RemoveAuthorRole(Guid Id);
        #endregion
    }
}
