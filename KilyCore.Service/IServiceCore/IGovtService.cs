using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Govt;
using KilyCore.DataEntity.ResponseMapper.Govt;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;

#region << 版 本 注 释 >>

/*----------------------------------------------------------------
* 类 名 称 ：IGovtService
* 类 描 述 ：
* 命名空间 ：KilyCore.Service.IServiceCore
* 机器名称 ：EMILY
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/6 14:33:45
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/

#endregion << 版 本 注 释 >>

namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 政府监管业务逻辑接口
    /// </summary>
    public interface IGovtService : IService
    {
        #region 政府监管

        IList<ResponseGovtMenu> AddGovtParentMenu();

        PagedResult<ResponseGovtMenu> GetGovtMenuPage(PageParamList<RequestGovtMenu> pageParam);

        ResponseGovtMenu GetGovtMenuDetail(Guid Id);

        String RemoveGovtMenu(Guid Id);

        String EditGovtMenu(RequestGovtMenu Param);

        #endregion 政府监管

        #region 权限菜单树

        IList<ResponseParentTree> GetGovtTree();

        #endregion 权限菜单树

        #region 角色权限

        PagedResult<ResponseGovtRoleAuthor> GetAuthorPage(PageParamList<RequestGovtRoleAuthor> pageParam);

        String RemoveAuthor(Guid Id);

        String EditAuthor(RequestGovtRoleAuthor Param);

        #endregion 角色权限

        #region 政府账号

        PagedResult<ResponseGovtInfo> GetInfoPage(PageParamList<RequestGovtInfo> pageParam);

        String EditInfo(RequestGovtInfo Param);

        String PushInfo(RequestGovtInfo Param);

        #endregion 政府账号
    }
}