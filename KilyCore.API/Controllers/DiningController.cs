using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.DataEntity.RequestMapper.Dining;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.API.Controllers
{
    [Route("api/[controller]")]
    public class DiningController : BaseController
    {
        #region  商家中心
        /// <summary>
        /// 商家分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetMerchantPage")]
        public ObjectResultEx GetMerchantPage(PageParamList<RequestMerchant> pageParam)
        {
            return ObjectResultEx.Instance(DiningService.GetMerchantPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取商家详情
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetMerchantDetail")]
        public ObjectResultEx GetMerchantDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(DiningService.GetMerchantDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 审核商家
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditMerchant")]
        public ObjectResultEx AuditMerchant(RequestAudit Param)
        {
            return ObjectResultEx.Instance(DiningService.AuditMerchant(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 启用账号
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EnableAccount")]
        public ObjectResultEx EnableAccount(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(DiningService.EnableAccount(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}