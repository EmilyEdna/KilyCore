using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;

/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 系统业务逻辑接口
    /// </summary>
    public interface ISystemService : IService
    {
        #region 用户登陆

        ResponseAdmin SystemLogin(RequestValidate LoginValidate);

        #endregion 用户登陆

        #region 系统菜单

        IList<ResponseMenu> GetSystemMenu();

        IList<ResponseMenu> AddSystemParentMenu();

        PagedResult<ResponseMenu> GetMenuPage(PageParamList<RequestMenu> pageParam);

        ResponseMenu GetMenuDetail(Guid Id);

        String RemoveMenu(Guid Id);

        String EditMenu(RequestMenu Param);

        #endregion 系统菜单

        #region 角色权限

        IList<ResponseRoleLv> GetRoleLv();

        String EditRole(RequestAuthorRole Param);

        PagedResult<ResponseAuthorRole> GetAuthorPage(PageParamList<RequestAuthorRole> pageParam);

        String RemoveAuthorRole(Guid Id);

        IList<ResponseAuthorRole> GetAuthorRole();

        #endregion 角色权限

        #region 区域树

        IList<ResponseTree> GetSystemAreaTree();

        IList<ResponseTree> GetSystemAreaTrees();

        #endregion 区域树

        #region 权限菜单树

        IList<ResponseParentTree> GetSystemAdminTree(String Key);

        #endregion 权限菜单树

        #region 省市区

        IList<ResponseProvince> GetProvince();

        IList<ResponseCity> GetCity(int Pid);

        IList<ResponseArea> GetArea(int Cid);

        IList<ResponseTown> GetTown(int Aid);

        String GetAreaWithChinese(string Param);

        #endregion 省市区

        #region 用户管理

        String InsertAdmin(RequestAdmin Param);

        String EditAdmin(RequestAdmin Param);

        PagedResult<ResponseAdmin> GetAdminPage(PageParamList<RequestAdmin> pageParam);

        String RemoveAdmin(Guid Id);

        String OpenAdmin(Guid Id);

        ResponseAdmin GetAdminDetail(Guid Id);

        IList<ResponseAdmin> GetBankInfo();

        String CG(Guid Id, bool Param);

        IList<ResponseAdmin> GetAuthorAdmin(string TypePath);

        #endregion 用户管理

        #region 任务调度

        String AddJob(RequestQuartz Param);

        PagedResult<ResponseQuartz> GetJobPage(PageParamList<RequestQuartz> pageParam);

        String ExcuteJob(RequestQuartz Param);

        String StopJob();

        String RecoverPauseJob(RequestQuartz Param);

        String PauseAppointJob(RequestQuartz Param);

        String RemoveJob(RequestQuartz Param);

        #endregion 任务调度

        #region 人员归档

        PagedResult<ResponsePreson> GetPresonPage(PageParamList<RequestPreson> pageParam);

        String PresonEdit(RequestPreson Param);

        String RemovePreson(Guid Id,bool Key);

        ResponsePreson GetPresonDetail(Guid Id);

        PagedResult<ResponsePreson> GetPresonDetailWeb(String key, int pageSize, int pageIndex);

        #endregion 人员归档

        #region 服务网点
        PagedResult<ResponseSystemNetService> GetNetServicePage(PageParamList<RequestSystemNetService> pageParam);

        ResponseSystemNetService GetNetServiceDetail(Guid Id);

        string IsOpen(Guid Id, bool key);

        string EditNetService(RequestSystemNetService param);

        PagedResult<ResponseSystemNetService> GetNetServiceWeb(String key, int pageSize, int pageIndex);

        #endregion

        #region 入住合同

        PagedResult<ResponseStayContract> GetStayContractPage(PageParamList<RequestStayContract> pageParam);

        String EditContract(Guid Id, decimal Money);

        String AuditContract(RequestAudit Param);

        PagedResult<ResponseAudit> GetContractRecord(PageParamList<RequestAudit> pageParam);

        String RemoveRecord(Guid Id);

        #endregion 入住合同

        #region 支付宝微信银行支付

        String AliPay(int Money);

        String WxPay(int Money);

        String AliQueryPay(String TradeNo);

        String EditPay(RequestStayContract Param);

        #endregion 支付宝微信银行支付

        #region 消息盒子

        PagedResult<ResponseSystemMessage> GetMsgPage(PageParamList<Object> pageParam);

        #endregion 消息盒子

        #region 新闻资讯

        PagedResult<ResponseSystemNews> GetNewsPage(PageParamList<RequestSystemNews> pageParam);

        String EditNews(RequestSystemNews Param);

        ResponseSystemNews GetNewsDetail(Guid Id);

        String RemoveNews(Guid Id);

        #endregion 新闻资讯

        #region 数据报表

        IList<ResponseSystemCodeCount> GetCodeCountCenter();

        IList<ResponseSystemCompanyCount> GetCompanyCountCenter();

        IList<ResponseSystemProductCount> GetProductCountCenter();

        ResponseSystemContractTotalCount GetContractCountCenter(RequestRangeDate Range);

        #endregion 数据报表

        #region 订单管理

        #region 订单中心

        PagedResult<ResponseSystemOrder> GetOrderPage(PageParamList<RequestSystemOrder> pageParam);

        String OrderEdit(RequestSystemOrder Param);

        ResponseSystemOrder GetOrderDetail(Guid Id);

        String OrderCheck(RequestSystemOrder Param);

        #endregion 订单中心

        #region 订单日志

        PagedResult<ResponseSystemOrderLog> GetOrderLogPage(PageParamList<RequestSystemOrderLog> pageParam);

        String EditOrderLog(RequestSystemOrderLog Param);

        String RemoveLog(Guid Id);

        ResponseSystemOrderLog GetOrderLogDetail(Guid Id);

        #endregion 订单日志

        #region 评分记录

        PagedResult<ResponseSystemOrderScore> GetOrderScorePage(PageParamList<RequestSystemOrderScore> pageParam);

        String EditOrderScore(RequestSystemOrderScore Param);

        ResponseSystemOrderScore GetOrderScoreDetail(Guid Id);

        #endregion 评分记录

        #region 线下人员

        PagedResult<ResponseSystemPersonOff> GetPersonOffPage(PageParamList<RequestSystemPersonOff> pageParam);

        ResponseSystemPersonOff GetOffDetail(Guid Id);

        #endregion 线下人员

        #endregion 订单管理
    }
}