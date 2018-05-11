﻿using System;
using System.Collections.Generic;
using System.Linq;
using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.Extension.ResultExtension;
using KilyCore.Extension.SessionExtension;
using KilyCore.Extension.Token;
using KilyCore.Extension.ValidateExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.API.Controllers
{
    [Route("api/[controller]")]
    public class SystemController : BaseController
    {
        #region 系统菜单
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetSystemMenu")]
        public ObjectResultEx GetSystemMenu()
        {
            return ObjectResultEx.Instance(SystemService.GetSystemMenu(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取父节菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddSystemParentMenu")]
        public ObjectResultEx AddSystemParentMenu()
        {
            return ObjectResultEx.Instance(SystemService.AddSystemParentMenu(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 菜单分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetMenuPage")]
        public ObjectResultEx GetMenuPage(PageParamList<RequestMenu> pageParam)
        {
            return ObjectResultEx.Instance(SystemService.GetMenuPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("RemoveMenu")]
        public ObjectResultEx RemoveMenu(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(SystemService.RemoveMenu(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 修改新增菜单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("EditMenu")]
        public ObjectResultEx EditMenu(RequestMenu Param)
        {
            return ObjectResultEx.Instance(SystemService.EditMenu(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("GetMenuDetail")]
        public ObjectResultEx GetMenuDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(SystemService.GetMenuDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 角色权限
        /// <summary>
        /// 权限等级
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetRoleLv")]
        public ObjectResultEx GetRoleLv()
        {
            return ObjectResultEx.Instance(SystemService.GetRoleLv(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        [HttpPost("EditRole")]
        public ObjectResultEx EditRole(RequestAuthorRole Param)
        {
            return ObjectResultEx.Instance(SystemService.EditRole(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetAuthorPage")]
        public ObjectResultEx GetAuthorPage(PageParamList<RequestAuthorRole> Param)
        {
            return ObjectResultEx.Instance(SystemService.GetAuthorPage(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 移除权限
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveAuthorRole")]
        public ObjectResultEx RemoveAuthorRole(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(SystemService.RemoveAuthorRole(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 下拉权限列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAuthorRole")]
        public ObjectResultEx GetAuthorRole()
        {
            return ObjectResultEx.Instance(SystemService.GetAuthorRole(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 用户登录退出
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public ObjectResultEx Login(RequestValidate LoginValidate)
        {
            try
            {
                string Code = HttpContext.Session.GetSession<string>("ValidateCode").Trim();
                ResponseAdmin SysAdmin = SystemService.SystemLogin(LoginValidate);
                if (SysAdmin != null && Code.Equals(LoginValidate.ValidateCode.Trim()))
                {
                    CookieInfo cookie = new CookieInfo();
                    VerificationExtension.WriteToken(cookie, SysAdmin);
                    return ObjectResultEx.Instance(new { ResponseCookieInfo.RSAToKen, ResponseCookieInfo.RSAApiKey, ResponseCookieInfo.RSASysKey, SysAdmin }, 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCode")]
        [AllowAnonymous]
        public ObjectResultEx GetCode()
        {
            String Code = ValidateCode.CreateValidateCode();
            HttpContext.Session.SetSession("ValidateCode", Code);
            return ObjectResultEx.Instance(Code, 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        #region 区域树
        /// <summary>
        /// 显示区域树
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetSystemAreaTree")]
        public ObjectResultEx GetSystemAreaTree()
        {
            return ObjectResultEx.Instance(SystemService.GetSystemAreaTree(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 显示完整区域树
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetSystemAreaTrees")]
        public ObjectResultEx GetSystemAreaTrees()
        {
            return ObjectResultEx.Instance(SystemService.GetSystemAreaTrees(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 权限菜单树
        /// <summary>
        /// 权限区域树
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetSystemAdminTree")]
        public ObjectResultEx GetSystemAdminTree()
        {
            return ObjectResultEx.Instance(SystemService.GetSystemAdminTree(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 用户管理
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditAdmin")]
        public ObjectResultEx EditAdmin(RequestAdmin Param)
        {
            return ObjectResultEx.Instance(SystemService.EditAdmin(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取用户分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetAdminPage")]
        public ObjectResultEx GetAdminPage(PageParamList<RequestAdmin> pageParam)
        {
            return ObjectResultEx.Instance(SystemService.GetAdminPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveAdmin")]
        public ObjectResultEx RemoveAdmin(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(SystemService.RemoveAdmin(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetAdminDetail")]
        public ObjectResultEx GetAdminDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(SystemService.GetAdminDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 省市区乡
        /// <summary>
        /// 获取省份
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetProvince")]
        public ObjectResultEx GetProvince()
        {
            return ObjectResultEx.Instance(SystemService.GetProvince(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取城市
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetCity")]
        public ObjectResultEx GetCity(SimlpeParam<int> Param)
        {
            return ObjectResultEx.Instance(SystemService.GetCity(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取区县
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetArea")]
        public ObjectResultEx GetArea(SimlpeParam<int> Param)
        {
            return ObjectResultEx.Instance(SystemService.GetArea(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取乡镇
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("GetTown")]
        public ObjectResultEx GetTown(SimlpeParam<int> param)
        {
            return ObjectResultEx.Instance(SystemService.GetTown(param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 任务调度
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AddJob")]
        public ObjectResultEx AddJob(RequestQuartz Param)
        {
            return ObjectResultEx.Instance(SystemService.AddJob(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 任务分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetJobPage")]
        public ObjectResultEx GetJobPage(PageParamList<RequestQuartz> pageParam)
        {
            return ObjectResultEx.Instance(SystemService.GetJobPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        ///  执行任务
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("ExcuteJob")]
        public ObjectResultEx ExcuteJob(RequestQuartz Param)
        {
            return ObjectResultEx.Instance(SystemService.ExcuteJob(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 停止所有任务
        /// </summary>
        /// <returns></returns>
        [HttpPost("StopJob")]
        public ObjectResultEx StopJob()
        {
            return ObjectResultEx.Instance(SystemService.StopJob(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 恢复暂停任务
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RecoverPauseJob")]
        public ObjectResultEx RecoverPauseJob(RequestQuartz Param)
        {
            return ObjectResultEx.Instance(SystemService.RecoverPauseJob(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 暂停指定任务
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("PauseAppointJob")]
        public ObjectResultEx PauseAppointJob(RequestQuartz Param)
        {
            return ObjectResultEx.Instance(SystemService.PauseAppointJob(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveJob")]
        public ObjectResultEx RemoveJob(RequestQuartz Param)
        {
            return ObjectResultEx.Instance(SystemService.RemoveJob(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 人员归档
        /// <summary>
        /// 人员分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetPresonPage")]
        public ObjectResultEx GetPresonPage(PageParamList<RequestPreson> pageParam)
        {
            return ObjectResultEx.Instance(SystemService.GetPresonPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑人员
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("PresonEdit")]
        public ObjectResultEx PresonEdit(RequestPreson Param)
        {
            return ObjectResultEx.Instance(SystemService.PresonEdit(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除人员
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemovePreson")]
        public ObjectResultEx RemovePreson(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(SystemService.RemovePreson(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetPresonDetail")]
        public ObjectResultEx GetPresonDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(SystemService.GetPresonDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 入住合同
        /// <summary>
        /// 入住合同分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetStayContractPage")]
        public ObjectResultEx GetStayContractPage(PageParamList<RequestStayContract> pageParam)
        {
            return ObjectResultEx.Instance(SystemService.GetStayContractPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}