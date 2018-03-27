using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Finance;
using KilyCore.DataEntity.ResponseMapper.Finance;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 财务业务逻辑接口
    /// </summary>
    public interface IFinanceService: IService
    {
        #region 加盟缴费
        PagedResult<ResponseAdminAttach> GetJoinPayPage(PageParamList<RequestAdminAttach> pageParam);
        String StartUse(Guid Id);
        String BlockUp(Guid Id);
        String Archive(RequestAdminAttach param);
        #endregion
    }
}
