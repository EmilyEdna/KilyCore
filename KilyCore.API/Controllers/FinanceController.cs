using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        #region 认证缴费
        /// <summary>
        /// 认证缴费
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetIdentPayPage")]
        public ObjectResultEx GetIdentPayPage(PageParamList<RequestEnterpriseIdent> pageParam)
        {
            return ObjectResultEx.Instance(FinanceService.GetIdentPayPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 是否通过终审
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        [HttpPost("AuditIndetPay")]
        public ObjectResultEx AuditIndetPay(SimlpeParam<Guid> Key, SimlpeParam<bool> Value)
        {
            return ObjectResultEx.Instance(FinanceService.AuditIndetPay(Key.Id, Value.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}