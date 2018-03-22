using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.DataEntity.RequestMapper.Company;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.API.Controllers
{
    [Route("api/[controller]")]
    public class CompanyController : BaseController
    {
        #region  资料审核
        /// <summary>
        /// 企业分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetCompanyPage")]
        public ObjectResultEx GetCompanyPage(PageParamList<RequestCompany> pageParam)
        {
            return ObjectResultEx.Instance(CompanyService.GetCompanyPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取企业详情
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetCompanyDetail")]
        public ObjectResultEx GetCompanyDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(CompanyService.GetCompanyDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 审核企业
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditCompany")]
        public ObjectResultEx AuditCompany(RequestAudit Param)
        {
            return ObjectResultEx.Instance(CompanyService.AuditCompany(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 认证审核
        /// <summary>
        /// 企业认证分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetCompanyIdentPage")]
        public ObjectResultEx GetCompanyIdentPage(PageParamList<RequestCompanyIdent> pageParam)
        {
            return ObjectResultEx.Instance(CompanyService.GetCompanyIdentPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取认证详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetCompanyIdentDetail")]
        public ObjectResultEx GetCompanyIdentDetail(RequestCompanyIdent Param)
        {
            return ObjectResultEx.Instance(CompanyService.GetCompanyIdentDetail(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 认证审核
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditIdent")]
        public ObjectResultEx AuditIdent(RequestAudit Param)
        {
            return ObjectResultEx.Instance(CompanyService.AuditIdent(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}