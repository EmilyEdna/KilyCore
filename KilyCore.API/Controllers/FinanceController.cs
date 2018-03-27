using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.DataEntity.RequestMapper.Finance;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
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
        public ObjectResultEx Archive(RequestAdminAttach Param) {
            return ObjectResultEx.Instance(FinanceService.Archive(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}