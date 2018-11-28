using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Repast;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Repast;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 餐饮业务逻辑接口
    /// </summary>
    public interface IRepastService : IService
    {
        #region 商家资料
        PagedResult<ResponseMerchant> GetMerchantPage(PageParamList<RequestMerchant> pageParam);
        ResponseMerchant GetMerchantDetail(Guid Id);
        String AuditMerchant(RequestAudit Param);
        #endregion
        #region 餐饮菜单
        PagedResult<ResponseRepastMenu> GetDiningMenuPage(PageParamList<RequestRepastMenu> pageParam);
        String EditDiningMenu(RequestRepastMenu Param);
        String RemoveMenu(Guid Id);
        ResponseRepastMenu GetDiningMenuDetail(Guid Id);
        IList<ResponseRepastMenu> AddDiningParentMenu();
        #endregion
        #region 餐饮权限菜单树
        IList<ResponseParentTree> GetDiningTree();
        #endregion
        #region 餐饮权限
        String EditRole(RequestRepastRoleAuthor Param);
        PagedResult<ResponseRepastRoleAuthor> GetMerchantAuthorPage(PageParamList<RequestRepastRoleAuthor> pageParam);
        String RemoveAuthorRole(Guid Id);
        PagedResult<ResponseRepastRoleAuthor> GetRoleAuthorPage(PageParamList<RequestRepastRoleAuthor> pageParam);
        IList<ResponseRepastRoleAuthor> GetRoleAuthorList();
        String DistributionRole(RequestRepastRoleAuthor Param);
        #endregion
        #region 认证审核
        PagedResult<ResponseRepastIdent> GetDiningIdentPage(PageParamList<RequestRepastIdent> pageParam);
        ResponseRepastIdent GetDiningIdentDetail(Guid Id);
        String AuditIdent(RequestAudit Param);
        String AuditPayment(RequestPayment Param);
        #endregion
        #region 登录注册
        String RegistRepastAccount(RequestMerchant Param);
        Object MerchantLogin(RequestValidate LoginValidate);
        #endregion
    }
}
