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
        public ObjectResultEx GetAllSupply(SimpleParam<Guid> Param, SimpleParam<int> Parameter)
        {
            return ObjectResultEx.Instance(Temp.GetAllSupply(Param.Id, Parameter.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取商家留样
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet("GetAllSample")]
        [AllowAnonymous]
        public ObjectResultEx GetAllSample(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(Temp.GetAllSample(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取商家留样
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet("RepastDuck")]
        [AllowAnonymous]
        public ObjectResultEx RepastDuck(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(Temp.RepastDuck(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 微信公众号
        /// <summary>
        /// 微信活动注册接口
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("WeChatRegist")]
        [AllowAnonymous]
        public ObjectResultEx WeChatRegist(RequestEnterprise Param)
        {
            return ObjectResultEx.Instance(Temp.WeChatRegist(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取邀请码
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        [HttpGet("GetInviteCode")]
        [AllowAnonymous]
        public ObjectResultEx GetInviteCode(SimpleParam<string> Key)
        {
            return ObjectResultEx.Instance(Temp.GetInviteCode(Key.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}