﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Cook;
using KilyCore.DataEntity.RequestMapper.System;
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
    public class CookWebController : BaseController
    {
        #region 登录注册退出
        /// <summary>
        /// 厨师注册
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RegistCookAccount")]
        [AllowAnonymous]
        public ObjectResultEx RegistCookAccount(SimpleParam<RequestCookInfo> Param)
        {
            return ObjectResultEx.Instance(CookWebService.RegistCookAccount(Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 厨师登录
        /// </summary>
        /// <param name="LoginValidate"></param>
        /// <returns></returns>
        [HttpPost("CookLogin")]
        [AllowAnonymous]
        public ObjectResultEx CookLogin(RequestValidate LoginValidate)
        {
            try
            {
                string Code = HttpContext.Session.GetSession<string>("ValidateCode").Trim();
                var CookAdmin = CookWebService.CookLogin(LoginValidate);
                if (CookAdmin != null && Code.Equals(LoginValidate.ValidateCode.Trim()))
                {
                    CookieInfo cookie = new CookieInfo();
                    VerificationExtension.WriteToken(cookie, CookAdmin);
                    return ObjectResultEx.Instance(new { ResponseCookieInfo.RSAToKen, ResponseCookieInfo.RSAApiKey, ResponseCookieInfo.RSASysKey, CookAdmin }, 1, RetrunMessge.SUCCESS, HttpCode.Success);
                }
                else
                    return ObjectResultEx.Instance(null, -1, "登录失败", HttpCode.NoAuth);
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
        #endregion

        #region 全局菜单
        [HttpPost("GetCookMenu")]
        public ObjectResultEx GetCookMenu()
        {
            return ObjectResultEx.Instance(CookWebService.GetCookMenu(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 账号管理
        /// <summary>
        /// 账号分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetCookVipPage")]
        public ObjectResultEx GetCookVipPage(PageParamList<RequestCookInfo> pageParam)
        {
            return ObjectResultEx.Instance(CookWebService.GetCookVipPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 账号详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetCookVipDetail")]
        public ObjectResultEx GetCookVipDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(CookWebService.GetCookVipDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑账号
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditCookVip")]
        public ObjectResultEx EditCookVip(RequestCookInfo Param)
        {
            return ObjectResultEx.Instance(CookWebService.EditCookVip(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 开通服务
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("OpenService")]
        public ObjectResultEx OpenService(RequestStayContract Param)
        {
            return ObjectResultEx.Instance(CookWebService.OpenService(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 厨师信息
        /// <summary>
        /// 厨师详情
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetCookInfoDetail")]
        public ObjectResultEx GetCookInfoDetail()
        {
            return ObjectResultEx.Instance(CookWebService.GetCookInfoDetail(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑厨师信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditCookInfo")]
        public ObjectResultEx EditCookInfo(RequestCookInfo Param)
        {
            return ObjectResultEx.Instance(CookWebService.EditCookInfo(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 办宴报备
        /// <summary>
        /// 报备列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetBanquetPage")]
        public ObjectResultEx GetBanquetPage(PageParamList<RequestCookBanquet> pageParam)
        {
            return ObjectResultEx.Instance(CookWebService.GetBanquetPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 新增报备记录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditBanquet")]
        public ObjectResultEx EditBanquet(RequestCookBanquet Param)
        {
            return ObjectResultEx.Instance(CookWebService.EditBanquet(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 查看详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetBanquetDetail")]
        public ObjectResultEx GetBanquetDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(CookWebService.GetBanquetDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除报备
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveBanquet")]
        public ObjectResultEx RemoveBanquet(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(CookWebService.RemoveBanquet(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 帮厨管理
        /// <summary>
        /// 帮厨分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetHelperPage")]
        public ObjectResultEx GetHelperPage(PageParamList<RequestCookHelper> pageParam)
        {
            return ObjectResultEx.Instance(CookWebService.GetHelperPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑帮厨
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditHelper")]
        public ObjectResultEx EditHelper(RequestCookHelper Param)
        {
            return ObjectResultEx.Instance(CookWebService.EditHelper(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除帮厨
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveHelper")]
        public ObjectResultEx RemoveHelper(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(CookWebService.RemoveHelper(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 帮厨列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetHelperList")]
        public ObjectResultEx GetHelperList() {
            return ObjectResultEx.Instance(CookWebService.GetHelperList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 食材信息
        /// <summary>
        /// 食材分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetFoodPage")]
        public ObjectResultEx GetFoodPage(PageParamList<RequestCookFood> pageParam)
        {
            return ObjectResultEx.Instance(CookWebService.GetFoodPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除食材
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveFood")]
        public ObjectResultEx RemoveFood(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(CookWebService.RemoveFood(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑食材
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditFood")]
        public ObjectResultEx EditFood(RequestCookFood Param)
        {
            return ObjectResultEx.Instance(CookWebService.EditFood(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 食材列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetFoodList")]
        public ObjectResultEx GetFoodList()
        {
            return ObjectResultEx.Instance(CookWebService.GetFoodList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 责任协议
        /// <summary>
        /// 协议分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetAgreePage")]
        public ObjectResultEx GetAgreePage(PageParamList<RequestCookAgree> pageParam)
        {
            return ObjectResultEx.Instance(CookWebService.GetAgreePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑协议
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditAgree")]
        public ObjectResultEx EditAgree(RequestCookAgree Param)
        {
            return ObjectResultEx.Instance(CookWebService.EditAgree(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 协议详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetAgreeDetail")]
        public ObjectResultEx GetAgreeDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(CookWebService.GetAgreeDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除协议
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveAgree")]
        public ObjectResultEx RemoveAgree(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(CookWebService.RemoveAgree(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}