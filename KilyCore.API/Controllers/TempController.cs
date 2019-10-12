using KilyCore.Cache;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.Extension.HttpClientFactory;
using KilyCore.Extension.OutSideService;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

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
        /// <param name="Key"></param>
        /// <returns></returns>
        [HttpGet("GetAllSupply")]
        [AllowAnonymous]
        public ObjectResultEx GetAllSupply(SimpleParam<Guid> Param, SimpleParam<int> Key)
        {
            return ObjectResultEx.Instance(Temp.GetAllSupply(Param.Id, Key.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        /// 获取废物处理
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet("RepastDuck")]
        [AllowAnonymous]
        public ObjectResultEx RepastDuck(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(Temp.RepastDuck(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取食材供应
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet("RepastThing")]
        [AllowAnonymous]
        public ObjectResultEx RepastThing(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(Temp.RepastThing(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取周菜谱
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet("RepastWeek")]
        [AllowAnonymous]
        public ObjectResultEx RepastWeek(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(Temp.RepastWeek(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取抽检
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet("RepastCheck")]
        [AllowAnonymous]
        public ObjectResultEx RepastCheck(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(Temp.RepastCheck(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 主营菜品
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet("RepastFoods")]
        [AllowAnonymous]
        public ObjectResultEx RepastFoods(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(Temp.RepastFoods(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 进货台账
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet("GetBuybillPage")]
        [AllowAnonymous]
        public ObjectResultEx GetBuybillPage(Guid CompanyId, string SDate, string EDate)
        {
            return ObjectResultEx.Instance(Temp.GetBuybillPage(CompanyId,SDate,EDate), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 陪餐记录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet("RepastMarket")]
        [AllowAnonymous]
        public ObjectResultEx RepastMarket(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(Temp.RepastMarket(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 商家自查
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet("RepastSelfCheck")]
        [AllowAnonymous]
        public ObjectResultEx RepastSelfCheck(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(Temp.RepastSelfCheck(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        ///产品图片
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet("RepastProduct")]
        [AllowAnonymous]
        public ObjectResultEx RepastProduct(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(Temp.RepastProduct(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 中继API

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

        #endregion 微信公众号

        #region Redis缓存

        /// <summary>
        /// 监测实时数据
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet("GetTempTime")]
        [AllowAnonymous]
        public ObjectResultEx GetTempTime(ResponseEnterpriseEnv Param)
        {
            var data = HttpClientExtension.HttpGetAsync(Param.CheckUrl).Result;
            List<ResponseEnterpriseEnv> env = new List<ResponseEnterpriseEnv>
                {
                    new ResponseEnterpriseEnv{
                        AirEnv = JArray.Parse(data)[2]["DevTempValue"].ToString(),
                        AirHdy = JArray.Parse(data)[2]["DevHumiValue"].ToString(),
                        SoilEnv = JArray.Parse(data)[0]["DevTempValue"].ToString(),
                        SoilHdy = JArray.Parse(data)[0]["DevHumiValue"].ToString(),
                        Light = JArray.Parse(data)[3]["DevHumiValue"].ToString(),
                        CO2 = JArray.Parse(data)[1]["DevHumiValue"].ToString(),
                        Now=DateTime.Now
                    }
                };
            var res = CacheFactory.Cache().GetCache<List<ResponseEnterpriseEnv>>(Param.Flag);
            if (res != null)
            {
                env.AddRange(res);
            }
            CacheFactory.Cache().WriteCache(env, Param.Flag, 24);
            return ObjectResultEx.Instance(env, 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion Redis缓存

        #region 爬虫

        /// <summary>
        /// QS列表
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet("GetProList")]
        [AllowAnonymous]
        public ObjectResultEx GetProList(SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(ProductSearch.GetProList(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// QS详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet("GetProDetail")]
        [AllowAnonymous]
        public ObjectResultEx GetProDetail(SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(ProductSearch.GetProDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 安全标准
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet("GetTargetDb")]
        [AllowAnonymous]
        public ObjectResultEx GetTargetDb(SimpleParam<String> key, SimpleParam<String> value)
        {
            return ObjectResultEx.Instance(ProTarget.GetTargetDb(key.Id, value.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 爬虫
    }
}