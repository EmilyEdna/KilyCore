using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 集团业务逻辑接口
    /// </summary>
    public interface IEnterpriseService : IService
    {
        #region 集团菜单
        PagedResult<ResponseEnterpriseMenu> GetEnterpriseMenuPage(PageParamList<RequestEnterpriseMenu> pageParam);
        String RemoveEnterpriseMenu(Guid Id);
        IList<ResponseEnterpriseMenu> GetEnterpriseParentMenu();
        ResponseEnterpriseMenu GetEnterpriseMenuDetail(Guid Id);
        String EditEnterpriseMenu(RequestEnterpriseMenu Param);
        IList<ResponseParentTree> GetEnterpriseTree();
        #endregion

        #region 集团角色
        PagedResult<ResponseEnterpriseRoleAuthor> GetCompanyRoleAuthorPage(PageParamList<RequestEnterpriseRoleAuthor> pageParam);
        String EditEnterpriseRoleAuthor(RequestEnterpriseRoleAuthor Param);
        #endregion
    }
}
