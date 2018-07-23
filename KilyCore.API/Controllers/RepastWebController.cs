using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.DataEntity.RequestMapper.Repast;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.API.Controllers
{
    [Route("api/[controller]")]
    public class RepastWebController : BaseController
    {
        #region 获取全局集团菜单
        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetRepastMenu")]
        public ObjectResultEx GetRepastMenu()
        {
            return ObjectResultEx.Instance(RepastWebService.GetRepastMenu(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 基础管理
        #region 商家资料
        /// <summary>
        /// 商家资料分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetMerChantInfo")]
        public ObjectResultEx GetMerchantInfo(PageParamList<RequestMerchant> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetMerchantInfo(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取详细
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetMerchantDetail")]
        public ObjectResultEx GetMerchantDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.GetMerchantDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑商家资料
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditMerchant")]
        public ObjectResultEx EditMerchant(RequestMerchant Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditMerchant(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #endregion
    }
}