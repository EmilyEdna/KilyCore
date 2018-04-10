using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Dining;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Dining;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 点餐后台业务逻辑接口
    /// </summary>
    public interface IDiningService :IService
    {
        #region 商家资料
        PagedResult<ResponseMerchant> GetMerchantPage(PageParamList<RequestMerchant> pageParam);
        ResponseMerchant GetMerchantDetail(Guid Id);
        String AuditMerchant(RequestAudit Param);
        #endregion
        #region 餐饮菜单
        PagedResult<ResponseDiningMenu> GetDiningMenuPage(PageParamList<RequestDiningMenu> pageParam);
        String EditDiningMenu(RequestDiningMenu Param);
        String RemoveMenu(Guid Id);
        ResponseDiningMenu GetDiningMenuDetail(Guid Id);
        IList<ResponseDiningMenu> AddDiningParentMenu();
        #endregion
        #region 餐饮权限菜单树
        IList<ResponseParentTree> GetDiningTree();
        #endregion
        #region 餐饮权限
        String EditRole(RequestAuthorRole Param);
        PagedResult<ResponseAuthorRole> GetAuthorPage(PageParamList<RequestAuthorRole> pageParam);
        String RemoveAuthorRole(Guid Id);
        #endregion
        #region 认证审核
        PagedResult<ResponseDiningIdent> GetDiningIdentPage(PageParamList<RequestDiningIdent> pageParam);
        ResponseDiningIdent GetDiningIdentDetail(Guid Id);
        String AuditIdent(RequestAudit Param);
        String AuditPayment(RequestPayment Param);
        #endregion
    }
}
