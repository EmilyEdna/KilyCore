using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Finance;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 企业前台后也基础管理业务逻辑接口
    /// </summary>
    public interface IEnterpriseWebService : IService
    {
        #region 获取全局集团菜单
        IList<ResponseEnterpriseMenu> GetEnterpriseMenu();
        #endregion
        #region 企业信息
        ResponseEnterprise GetEnterpriseInfo(Guid Id);
        String EditEnterprise(RequestEnterprise param);
        #endregion
        #region 保存合同和缴费凭证
        String SaveContract(RequestStayContract Param);
        #endregion
        #region 人员管理
        PagedResult<ResponseEnterpriseUser> GetUserPage(PageParamList<RequestEnterpriseUser> pageParam);
        String EditUser(RequestEnterpriseUser Param);
        String RemoveUser(Guid Id);
        ResponseEnterpriseUser GetUserDetail(Guid Id);
        #endregion
    }
}
