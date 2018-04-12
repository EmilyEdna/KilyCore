using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.DataEntity.RequestMapper.Dining;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Finance;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public ObjectResultEx WatchCertificate(SimlpeParam<Guid> Key, SimlpeParam<string> Value) {
            return ObjectResultEx.Instance(FinanceService.WatchCertificate(Key.Id, Value.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}