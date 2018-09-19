using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Cook;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Govt;
using KilyCore.DataEntity.RequestMapper.Repast;
using KilyCore.Extension.ResultExtension;
using KilyCore.Extension.SessionExtension;
using KilyCore.Extension.Token;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.API.Controllers
{
    [Route("api/[controller]")]
    public class GovtWebController : BaseController
    {
        #region 获取全局集团菜单
        [HttpPost("GetGovtMenu")]
        public ObjectResultEx GetGovtMenu()
        {
            return ObjectResultEx.Instance(GovtWebService.GetGovtMenu(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 登录退出
        /// <summary>
        /// 监管登录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GovtLogin")]
        [AllowAnonymous]
        public ObjectResultEx GovtLogin(RequestGovtInfo Param)
        {
            try
            {
                string Code = HttpContext.Session.GetSession<string>("ValidateCode").Trim();
                var GovtAdmin = GovtWebService.GovtLogin(Param);
                if (GovtAdmin != null && Code.Equals(Param.ValidateCode.Trim()))
                {
                    CookieInfo cookie = new CookieInfo();
                    VerificationExtension.WriteToken(cookie, GovtAdmin);
                    return ObjectResultEx.Instance(new { ResponseCookieInfo.RSAToKen, ResponseCookieInfo.RSAApiKey, ResponseCookieInfo.RSASysKey, GovtAdmin }, 1, RetrunMessge.SUCCESS, HttpCode.Success);
                }
                else
                    return ObjectResultEx.Instance(null, -1, "登录失败或账户冻结", HttpCode.NoAuth);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 安全退出
        /// </summary>
        /// <returns></returns>
        [HttpPost("LoginOut")]
        public ObjectResultEx LoginOut()
        {
            return ObjectResultEx.Instance(VerificationExtension.LoginOut(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 第一次登录更新密码
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditPwd")]
        public ObjectResultEx EditPwd(RequestGovtInfo Param)
        {
            return ObjectResultEx.Instance(GovtWebService.EditPwd(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 企业监管
        /// <summary>
        /// 监管企业分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetCompanyPage")]
        public ObjectResultEx GetCompanyPage(PageParamList<RequestEnterprise> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetCompanyPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 监管餐饮分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetMerchantPage")]
        public ObjectResultEx GetMerchantPage(PageParamList<RequestMerchant> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetMerchantPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 企业详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetCompanyDetail")]
        public ObjectResultEx GetCompanyDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetCompanyDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 商家详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetRepastDetail")]
        public ObjectResultEx GetRepastDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetRepastDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 部门信息
        /// <summary>
        /// 机构分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetInsPage")]
        public ObjectResultEx GetInsPage(PageParamList<RequestGovtInstitution> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetInsPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 机构列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetInsList")]
        public ObjectResultEx GetInsList()
        {
            return ObjectResultEx.Instance(GovtWebService.GetInsList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除机构
        /// </summary>
        /// <returns></returns>
        [HttpPost("RemoveIns")]
        public ObjectResultEx RemoveIns(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.RemoveIns(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取机构详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetInsDetail")]
        public ObjectResultEx GetInsDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetInsDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑机构
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditIns")]
        public ObjectResultEx EditIns(RequestGovtInstitution Param)
        {
            return ObjectResultEx.Instance(GovtWebService.EditIns(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 政府用户分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetGovtInfoPage")]
        public ObjectResultEx GetGovtInfoPage(PageParamList<RequestGovtInfo> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetGovtInfoPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除政府用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveGovtInfo")]
        public ObjectResultEx RemoveGovtInfo(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.RemoveGovtInfo(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 政府用户详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetGovtInfoDetail")]
        public ObjectResultEx GetGovtInfoDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetGovtInfoDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑政府用户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditUser")]
        public ObjectResultEx EditUser(RequestGovtInfo Param)
        {
            return ObjectResultEx.Instance(GovtWebService.EditUser(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 管辖区域
        /// <summary>
        /// 获取分配的区域
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetDistributArea")]
        public ObjectResultEx GetDistributArea()
        {
            return ObjectResultEx.Instance(GovtWebService.GetDistributArea(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 当前登录账号是市级账号则查询该市下所有区县
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetArea")]
        public ObjectResultEx GetArea(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetArea(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 当前登录账号是区县级账号则查询该市下所有乡镇
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetTown")]
        public ObjectResultEx GetTown(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetTown(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 产品监管
        /// <summary>
        /// 加工产品分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetWorkPage")]
        public ObjectResultEx GetWorkPage(PageParamList<RequestEnterpriseGoods> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetWorkPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 加工产品详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetWorkDetail")]
        public ObjectResultEx GetWorkDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetWorkDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 食用品分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetEdiblePage")]
        public ObjectResultEx GetEdiblePage(PageParamList<RequestEnterpriseGoods> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetEdiblePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 食用品详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetEdibleDetail")]
        public ObjectResultEx GetEdibleDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetEdibleDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 餐饮监管
        /// <summary>
        /// 群宴报备分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetBanquetPage")]
        public ObjectResultEx GetBanquetPage(PageParamList<RequestCookBanquet> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetBanquetPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 群宴报备详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetBanquetDetail")]
        public ObjectResultEx GetBanquetDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetBanquetDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 审核群宴
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("EditCookBanquet")]
        public ObjectResultEx EditCookBanquet(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.EditCookBanquet(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 风险预警
        /// <summary>
        /// 预警信息分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetWaringRiskPage")]
        public ObjectResultEx GetWaringRiskPage(PageParamList<RequestGovtRisk> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetWaringRiskPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑预警信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditWaringRisk")]
        public ObjectResultEx EditWaringRisk(RequestGovtRisk Param)
        {
            return ObjectResultEx.Instance(GovtWebService.EditWaringRisk(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 发布广播
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("ReportWaringRisk")]
        public ObjectResultEx ReportWaringRisk(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.ReportWaringRisk(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveWaringRisk")]
        public ObjectResultEx RemoveWaringRisk(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.RemoveWaringRisk(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}