using System;
using KilyCore.DataEntity.RequestMapper.Dining;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Finance;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.API.Controllers
{
    [Route("api/[controller]")]
    public class FinanceController : BaseController
    {
        #region 加盟缴费
        /// <summary>
        /// 加盟缴费分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetJoinPayPage")]
        public ObjectResultEx GetJoinPayPage(PageParamList<RequestAdminAttach> pageParam)
        {
            return ObjectResultEx.Instance(FinanceService.GetJoinPayPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("StartUse")]
        public ObjectResultEx StartUse(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(FinanceService.StartUse(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 停用
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("BlockUp")]
        public ObjectResultEx BlockUp(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(FinanceService.BlockUp(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 加盟归档
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("Archive")]
        public ObjectResultEx Archive(RequestAdminAttach Param)
        {
            return ObjectResultEx.Instance(FinanceService.Archive(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="receive"></param>
        /// <returns></returns>
        [HttpPost("SendEmail")]
        public ObjectResultEx SendEmail(RequestEMail Param)
        {
            return ObjectResultEx.Instance(FinanceService.SendEmail(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 企业认证
        /// <summary>
        /// 企业认证
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("IdentEnterprisePay")]
        public ObjectResultEx IdentEnterprisePay(PageParamList<RequestEnterpriseIdent> pageParam)
        {
            return ObjectResultEx.Instance(FinanceService.IdentEnterprisePay(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 是否通过终审
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        [HttpPost("AuditIndetEnterprisePay")]
        public ObjectResultEx AuditIndetEnterprisePay(SimlpeParam<Guid> Key, SimlpeParam<bool> Value)
        {
            return ObjectResultEx.Instance(FinanceService.AuditIndetEnterprisePay(Key.Id, Value.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 餐饮认证
        /// <summary>
        /// 餐饮认证
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("IdentFoodPay")]
        public ObjectResultEx IdentFoodPay(PageParamList<RequestDiningIdent> pageParam)
        {
            return ObjectResultEx.Instance(FinanceService.IdentFoodPay(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 是否通过终审
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        [HttpPost("AuditIndetFoodPay")]
        public ObjectResultEx AuditIndetFoodPay(SimlpeParam<Guid> Key, SimlpeParam<bool> Value)
        {
            return ObjectResultEx.Instance(FinanceService.AuditIndetFoodPay(Key.Id, Value.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 缴费凭证
        /// <summary>
        /// 查看缴费凭证
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("WatchCertificate")]
        public ObjectResultEx WatchCertificate(SimlpeParam<Guid> Key, SimlpeParam<string> Value)
        {
            return ObjectResultEx.Instance(FinanceService.WatchCertificate(Key.Id, Value.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 餐饮合同
        /// <summary>
        /// 餐饮合同分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetContractPage")]
        public ObjectResultEx GetContractPage(PageParamList<RequestContract> pageParam)
        {
            return ObjectResultEx.Instance(FinanceService.GetContractPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 餐饮缴费
        /// <summary>
        /// 餐饮缴费分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetDiningPayPage")]
        public ObjectResultEx GetDiningPayPage(PageParamList<RequestDiningPay> pageParam)
        {
            return ObjectResultEx.Instance(FinanceService.GetDiningPayPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 权限列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetDiningRoles")]
        public ObjectResultEx GetDiningRoles()
        {
            return ObjectResultEx.Instance(FinanceService.GetDiningRoles(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 更新餐饮权限
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditDiningRole")]
        public ObjectResultEx EditDiningRole(RequestDiningInfo Param)
        {
            return ObjectResultEx.Instance(FinanceService.EditDiningRole(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}