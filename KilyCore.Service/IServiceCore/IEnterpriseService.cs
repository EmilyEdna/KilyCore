using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 集团业务逻辑接口
    /// </summary>
    public interface IEnterpriseService :IService
    {
        #region 企业菜单
        PagedResult<ResponseEnterpriseMenu> GetEnterpriseMenuPage(PageParamList<RequestEnterpriseMenu> pageParam);
        String RemoveEnterpriseMenu(Guid Id);
        IList<ResponseEnterpriseMenu> GetEnterpriseParentMenu();
        ResponseEnterpriseMenu GetEnterpriseMenuDetail(Guid Id);
        String EditEnterpriseMenu(RequestEnterpriseMenu Param);
        #endregion
    }
}
