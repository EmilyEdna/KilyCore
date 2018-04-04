using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Dining;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Dining;
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
        #region 点餐商家中心
        String EnableAccount(Guid Id);
        PagedResult<ResponseMerchant> GetMerchantPage(PageParamList<RequestMerchant> pageParam);
        ResponseMerchant GetMerchantDetail(Guid Id);
        #endregion
        #region 资料审核
        String AuditMerchant(RequestAudit Param);
        #endregion
    }
}
