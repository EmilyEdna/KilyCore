using System;
using KilyCore.DataEntity.RequestMapper.Repast;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.API.Controllers
{
    /// <summary>
    /// 中继系统接口
    /// </summary>
    [Route("api/[controller]")]
    public class TempController : BaseController
    {
        #region 中继API
        /// <summary>
        /// 获取所有人员
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet("GetAllUser")]
        [AllowAnonymous]
        public ObjectResultEx GetAllUser(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(Temp.GetAllUser(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取所有供应商
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet("GetAllSupply")]
        [AllowAnonymous]
        public ObjectResultEx GetAllSupply(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(Temp.GetAllSupply(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}