﻿using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Repast;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Mvc;
using System;

/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.API.Controllers
{
    /// <summary>
    /// 系统财务接口
    /// </summary>
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
        public ObjectResultEx StartUse(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(FinanceService.StartUse(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 停用
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("BlockUp")]
        public ObjectResultEx BlockUp(SimpleParam<Guid> Param)
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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("SendEmail")]
        public ObjectResultEx SendEmail(RequestEMail Param)
        {
            return ObjectResultEx.Instance(FinanceService.SendEmail(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 加盟缴费

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
        public ObjectResultEx AuditIndetEnterprisePay(SimpleParam<Guid> Key, SimpleParam<bool> Value)
        {
            return ObjectResultEx.Instance(FinanceService.AuditIndetEnterprisePay(Key.Id, Value.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 企业认证

        #region 餐饮认证

        /// <summary>
        /// 餐饮认证
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("IdentFoodPay")]
        public ObjectResultEx IdentFoodPay(PageParamList<RequestRepastIdent> pageParam)
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
        public ObjectResultEx AuditIndetFoodPay(SimpleParam<Guid> Key, SimpleParam<bool> Value)
        {
            return ObjectResultEx.Instance(FinanceService.AuditIndetFoodPay(Key.Id, Value.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 餐饮认证

        #region 缴费凭证

        /// <summary>
        /// 查看缴费凭证
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        [HttpPost("WatchCertificate")]
        public ObjectResultEx WatchCertificate(SimpleParam<Guid> Key, SimpleParam<string> Value)
        {
            return ObjectResultEx.Instance(FinanceService.WatchCertificate(Key.Id, Value.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 缴费凭证

        #region 物码缴费

        /// <summary>
        /// 物码缴费分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetTagAuditPage")]
        public ObjectResultEx GetTagAuditPage(PageParamList<RequestEnterpriseApply> pageParam)
        {
            return ObjectResultEx.Instance(FinanceService.GetTagAuditPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 审核物码缴费
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditCode")]
        public ObjectResultEx AuditCode(RequestAudit Param)
        {
            return ObjectResultEx.Instance(FinanceService.AuditCode(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 物码缴费
    }
}