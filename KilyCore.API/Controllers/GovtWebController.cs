﻿using KilyCore.Cache;
using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Cook;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Govt;
using KilyCore.DataEntity.RequestMapper.Repast;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.Extension.ResultExtension;
using KilyCore.Extension.Token;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace KilyCore.API.Controllers
{
    /// <summary>
    /// 政府后台接口
    /// </summary>
    [Route("api/[controller]")]
    public class GovtWebController : BaseController
    {
        #region 获取全局集团菜单

        /// <summary>
        /// 获取全局集团菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetGovtMenu")]
        public ObjectResultEx GetGovtMenu()
        {
            return ObjectResultEx.Instance(GovtWebService.GetGovtMenu(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 获取全局集团菜单

        #region 获取所有商家和企业

        /// <summary>
        /// 获取所有商家和企业
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        [HttpGet("GetAllMerchant")]
        [AllowAnonymous]
        public Object GetAllMerchant(SimpleParam<String> Key)
        {
            return ObjectResultEx.Instance(GovtWebService.GetAllMerchant(Key.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 获取所有商家和企业

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
                var GovtAdmin = GovtWebService.GovtLogin(Param);
                string Code = string.Empty;
                if (!Param.IsApp)
                {
                    Code = CacheFactory.Cache().GetCache<string>("ValidateCode").Trim();
                    if (GovtAdmin != null && Code.ToUpper().Equals(Param.ValidateCode.Trim().ToUpper()))
                    {
                        CookieInfo cookie = new CookieInfo();
                        VerificationExtension.WriteToken(cookie, GovtAdmin);
                        return ObjectResultEx.Instance(new { ResponseCookieInfo.RSAToKen, ResponseCookieInfo.RSAApiKey, ResponseCookieInfo.RSASysKey, GovtAdmin }, 1, RetrunMessge.SUCCESS, HttpCode.Success);
                    }
                    else if (!Code.ToUpper().Equals(Param.ValidateCode.Trim().ToUpper()))
                        return ObjectResultEx.Instance(null, -1, "验证码错误", HttpCode.NoAuth);
                    else
                        return ObjectResultEx.Instance(null, -1, "登录失败或账户冻结", HttpCode.NoAuth);
                }
                else
                {
                    if (GovtAdmin != null)
                    {
                        CookieInfo cookie = new CookieInfo();
                        VerificationExtension.WriteToken(cookie, GovtAdmin);
                        return ObjectResultEx.Instance(new { ResponseCookieInfo.RSAToKen, ResponseCookieInfo.RSAApiKey, ResponseCookieInfo.RSASysKey, GovtAdmin }, 1, RetrunMessge.SUCCESS, HttpCode.Success);
                    }
                    else if (GovtAdmin == null)
                        return ObjectResultEx.Instance(null, -1, "请检查用户名和密码是否正确", HttpCode.NoAuth);
                    else if (!Code.ToUpper().Equals(Param.ValidateCode.Trim().ToUpper()))
                        return ObjectResultEx.Instance(null, -1, "验证码错误", HttpCode.NoAuth);
                    else
                        return ObjectResultEx.Instance(null, -1, "登录失败或账户冻结", HttpCode.NoAuth);
                }
            }
            catch (Exception)
            {
                return ObjectResultEx.Instance(null, -1, "请输入验证码", HttpCode.FAIL);
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

        #endregion 登录退出

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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetCompanyDetail")]
        public ObjectResultEx GetCompanyDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetCompanyDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 商家详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetRepastDetail")]
        public ObjectResultEx GetRepastDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetRepastDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 查询企业
        /// </summary>
        /// <param name="KeyWord"></param>
        /// <returns></returns>
        [HttpGet("GetAllComWithKeyWord")]
        [AllowAnonymous]
        public ObjectResultEx GetAllComWithKeyWord(SimpleParam<string> KeyWord)
        {
            return ObjectResultEx.Instance(GovtWebService.GetAllComWithKeyWord(KeyWord.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 查询商家
        /// </summary>
        /// <param name="KeyWord"></param>
        /// <returns></returns>
        [HttpGet("GetAllMerWithKeyWord")]
        [AllowAnonymous]
        public ObjectResultEx GetAllMerWithKeyWord(SimpleParam<string> KeyWord)
        {
            return ObjectResultEx.Instance(GovtWebService.GetAllMerWithKeyWord(KeyWord.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取所有企业
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet("GetAllCom")]
        public ObjectResultEx GetAllCom(RequestEnterprise Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetAllCom(Param.AreaTree, (int)Param.CompanyType), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取所有商家
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet("GetAllMer")]
        public ObjectResultEx GetAllMer(RequestMerchant Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetAllMer(Param.AreaTree, (int)Param.DiningType), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取所有视频
        /// </summary>
        /// <param name="Param"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        [HttpGet("GetAllVideo")]
        [AllowAnonymous]
        public ObjectResultEx GetAllVideo(SimpleParam<Guid> Param, SimpleParam<int> Type)
        {
            return ObjectResultEx.Instance(GovtWebService.GetAllVideo(Param.Id, Type.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 企业监管

        #region 部门信息

        /// <summary>
        /// 获取或有企业
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllGovt")]
        [AllowAnonymous]
        public ObjectResultEx GetAllGovt()
        {
            return ObjectResultEx.Instance(GovtWebService.GetAllGovt(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

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
        /// <param name="Param"></param>
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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveGovtInfo")]
        public ObjectResultEx RemoveGovtInfo(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.RemoveGovtInfo(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 政府用户详情
        /// </summary>
        /// <param name="Param"></param>
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

        #endregion 部门信息

        #region 管辖区域

        /// <summary>
        /// 获取所属区域的市名称
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetCityName")]
        public ObjectResultEx GetCityName(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetCityName(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetArea")]
        public ObjectResultEx GetArea(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetArea(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 当前登录账号是区县级账号则查询该市下所有乡镇
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetTown")]
        public ObjectResultEx GetTown(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetTown(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 管辖区域

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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetWorkDetail")]
        [AllowAnonymous]
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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetEdibleDetail")]
        [AllowAnonymous]
        public ObjectResultEx GetEdibleDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetEdibleDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 企业产品列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetGoodsPage")]
        public ObjectResultEx GetGoodsPage(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetGoodsPage(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 产品监管

        #region 台账管理

        /// <summary>
        /// 进销台账
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        [HttpGet("GetTickPrint")]
        public ObjectResultEx GetTickPrint(SimpleParam<Dictionary<String, String>> Key)
        {
            return ObjectResultEx.Instance(GovtWebService.GetTickPrint(Key.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 台账管理

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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetBanquetDetail")]
        public ObjectResultEx GetBanquetDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetBanquetDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 现在检测-APP
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("SiteImg")]
        public ObjectResultEx SiteImg(SimpleParam<Guid> Key, SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.SiteImg(Key.Id, Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 审核群宴
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditCookBanquet")]
        public ObjectResultEx EditCookBanquet(SimpleParam<Guid> Key, SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.EditCookBanquet(Key.Id, Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 餐饮监管

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
        /// 预警详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetWaringDetail")]
        public ObjectResultEx GetWaringDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetWaringDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 发布广播
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("ReportWaringRisk")]
        public ObjectResultEx ReportWaringRisk(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.ReportWaringRisk(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveWaringRisk")]
        public ObjectResultEx RemoveWaringRisk(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.RemoveWaringRisk(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 事件分布图
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetRiskCount")]
        [AllowAnonymous]
        public ObjectResultEx GetRiskCount()
        {
            return ObjectResultEx.Instance(GovtWebService.GetRiskCount(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取市名称
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetCity")]
        public ObjectResultEx GetCity(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetCity(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 企业证件到期分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetCardPage")]
        public ObjectResultEx GetCardPage(PageParamList<RequestGovtRiskCompany> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetCardPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 预警提示
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetWarnList")]
        public ObjectResultEx GetWarnList(PageParamList<RequestGovtRiskCompany> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetWarnList(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 证件提醒
        /// </summary>
        /// <param name="Param"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        [HttpPost("ReportCardWaring")]
        public ObjectResultEx ReportCardWaring(SimpleParam<Guid> Param, SimpleParam<String> Key)
        {
            return ObjectResultEx.Instance(GovtWebService.ReportCardWaring(Param.Id, Key.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 预警提醒
        /// </summary>
        /// <param name="Param"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        //[HttpPost("ReportCardWaring")]
        //public ObjectResultEx ReportCardWaring(SimpleParam<Guid> Param, SimpleParam<String> Key, SimpleParam<String> Content)
        //{
        //    return ObjectResultEx.Instance(GovtWebService.ReportCardWaring(Param.Id, Key.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        //}

        #endregion 风险预警

        #region 执法检查

        /// <summary>
        /// 网上执法分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetNetPatrolPage")]
        public ObjectResultEx GetNetPatrolPage(PageParamList<RequestGovtNetPatrol> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetNetPatrolPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 添加网上执法
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditPatrol")]
        public ObjectResultEx EditPatrol(RequestGovtNetPatrol Param)
        {
            return ObjectResultEx.Instance(GovtWebService.EditPatrol(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 删除网上执法
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemovePatrol")]
        public ObjectResultEx RemovePatrol(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.RemovePatrol(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取网上巡检详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetNetPatrolDetail")]
        public ObjectResultEx GetNetPatrolDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetNetPatrolDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 通报批评
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditNetPatrol")]
        public ObjectResultEx EditNetPatrol(RequestGovtMsg Param)
        {
            return ObjectResultEx.Instance(GovtWebService.EditNetPatrol(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取巡查处理内容
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetMsgList")]
        public ObjectResultEx GetMsgList()
        {
            return ObjectResultEx.Instance(GovtWebService.GetMsgList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 巡查记录日志
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetNetPatrolLogs")]
        public ObjectResultEx GetNetPatrolLogs(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetNetPatrolLogs(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 执法检查

        #region 执法类目

        /// <summary>
        /// 执法类目分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetCategoryPage")]
        public ObjectResultEx GetCategoryPage(PageParamList<RequestGovtPatrolCategory> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetCategoryPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 编辑类目
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditCategory")]
        public ObjectResultEx EditCategory(RequestGovtPatrolCategory Param)
        {
            return ObjectResultEx.Instance(GovtWebService.EditCategory(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 类目详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetCategoryDetail")]
        public ObjectResultEx GetCategoryDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetCategoryDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 删除类目
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveCategory")]
        public ObjectResultEx RemoveCategory(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.RemoveCategory(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 题库分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetCategoryAttachPage")]
        public ObjectResultEx GetCategoryAttachPage(PageParamList<RequestGovtPatrolCategoryAttach> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetCategoryAttachPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 编辑题库
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditCategoryAttach")]
        public ObjectResultEx EditCategoryAttach(RequestGovtPatrolCategoryAttach Param)
        {
            return ObjectResultEx.Instance(GovtWebService.EditCategoryAttach(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 题库详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetCategoryAttachDetail")]
        public ObjectResultEx GetCategoryAttachDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetCategoryAttachDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 删除题库
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveCategoryAttach")]
        public ObjectResultEx RemoveCategoryAttach(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.RemoveCategoryAttach(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 执法类目

        #region 移动执法

        /// <summary>
        /// 移动执法分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetMovePatralPage")]
        public ObjectResultEx GetMovePatralPage(PageParamList<RequestGovtMovePatrol> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetMovePatralPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 删除移动执法记录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveMovePatral")]
        public ObjectResultEx RemoveMovePatral(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.RemoveMovePatral(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 编辑移动执法
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditMovePatrol")]
        public ObjectResultEx EditMovePatrol(RequestGovtMovePatrol Param)
        {
            return ObjectResultEx.Instance(GovtWebService.EditMovePatrol(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取移动执法表
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetGovtMovePatrolDetail")]
        [AllowAnonymous]
        public ObjectResultEx GetGovtMovePatrolDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetGovtMovePatrolDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 移动执法

        #region 企业自查模板

        /// <summary>
        /// 获取企业检查分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetTemplateChild")]
        public ObjectResultEx GetTemplateChild(PageParamList<RequestGovtTemplateChild> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetTemplateChild(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取企业检查详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetTemplateChildDetail")]
        public ObjectResultEx GetTemplateChildDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetTemplateChildDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetTemplateContentList")]
        public ObjectResultEx GetTemplateContentList(SimpleParam<String> Key, SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetTemplateContentList(Key.Id, Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 自查模板分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetTemplatePage")]
        public ObjectResultEx GetTemplatePage(PageParamList<RequestGovtTemplate> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetTemplatePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 编辑模板
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditGovtTemplate")]
        public ObjectResultEx EditGovtTemplate(RequestGovtTemplate Param)
        {
            return ObjectResultEx.Instance(GovtWebService.EditGovtTemplate(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveTemplate")]
        public ObjectResultEx RemoveTemplate(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.RemoveTemplate(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetTemplateDetail")]
        public ObjectResultEx GetTemplateDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetTemplateDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 企业自查模板

        #region 应急培训

        #region 培训通知

        /// <summary>
        /// 通知分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetTrainNoticePage")]
        public ObjectResultEx GetTrainNoticePage(PageParamList<RequestGovtTrainNotice> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetTrainNoticePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 编辑通知
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditNotice")]
        public ObjectResultEx EditNotice(RequestGovtTrainNotice Param)
        {
            return ObjectResultEx.Instance(GovtWebService.EditNotice(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveNotice")]
        public ObjectResultEx RemoveNotice(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.RemoveNotice(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 通知详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetTrainNoticeDetail")]
        public ObjectResultEx GetTrainNoticeDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetTrainNoticeDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 推送通知
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("ReportTrainNotice")]
        public ObjectResultEx ReportTrainNotice(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.ReportTrainNotice(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 培训通知

        #region 培训报道

        /// <summary>
        /// 报道分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetTrainReportPage")]
        public ObjectResultEx GetTrainReportPage(PageParamList<RequestGovtTrainReport> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetTrainReportPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 编辑报道
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditTrainReport")]
        public ObjectResultEx EditTrainReport(RequestGovtTrainReport Param)
        {
            return ObjectResultEx.Instance(GovtWebService.EditTrainReport(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 删除报道
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveReport")]
        public ObjectResultEx RemoveReport(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.RemoveReport(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 报道详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetTrainReportDetail")]
        public ObjectResultEx GetTrainReportDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetTrainReportDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 推送报道
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("ReportTrainReport")]
        public ObjectResultEx ReportTrainReport(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.ReportTrainReport(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 培训报道

        #endregion 应急培训

        #region 投诉信息

        /// <summary>
        /// 投诉信息分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetComplainPage")]
        public ObjectResultEx GetComplainPage(PageParamList<RequestGovtComplain> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetComplainPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 删除投诉
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveComplain")]
        public ObjectResultEx RemoveComplain(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.RemoveComplain(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 编辑投诉
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditComplain")]
        [AllowAnonymous]
        public ObjectResultEx EditComplain(RequestGovtComplain Param)
        {
            if (string.IsNullOrEmpty(Param.Center))
            {
                var SessionCode = CacheFactory.Cache().GetCache<string>("PhoneCode").Trim();
                if (SessionCode.Equals(Param.Code))
                    return ObjectResultEx.Instance(GovtWebService.EditComplain(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
                else
                    return ObjectResultEx.Instance("验证码不正确!", 1, RetrunMessge.SUCCESS, HttpCode.Success);
            }
            else
            {
                return ObjectResultEx.Instance(GovtWebService.EditComplain(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
            }
        }

        /// <summary>
        /// 推送投诉
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("ReportComplain")]
        public ObjectResultEx ReportComplain(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.ReportComplain(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 处理投诉
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("ReportComplainInfo")]
        public ObjectResultEx ReportComplainInfo(SimpleParam<Guid> Key, SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.ReportComplainInfo(Key.Id, Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 投诉详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetComplainDetail")]
        public ObjectResultEx GetComplainDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetComplainDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 投诉信息

        #region 数据统计

        #region 新大屏

        /// <summary>
        /// 今日数据
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetNewStayInTodayCount")]
        public ObjectResultEx GetNewStayInTodayCount()
        {
            return ObjectResultEx.Instance(GovtWebService.GetNewStayInTodayCount(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 历史所有企业数据统计
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetNewStayInAllCompanyCount")]
        public ObjectResultEx GetNewStayInAllCompanyCount()
        {
            return ObjectResultEx.Instance(GovtWebService.GetNewStayInAllCompanyCount(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 今日入住
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetTodayNow")]
        public ObjectResultEx GetTodayNow()
        {
            return ObjectResultEx.Instance(GovtWebService.GetTodayNow(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 投诉和风险
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetNewWeekRiskAndComplainCount")]
        public ObjectResultEx GetNewWeekRiskAndComplainCount()
        {
            return ObjectResultEx.Instance(GovtWebService.GetNewWeekRiskAndComplainCount(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 网上巡查统计
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetNewNetCheckCount")]
        public ObjectResultEx GetNewNetCheckCount()
        {
            return ObjectResultEx.Instance(GovtWebService.GetNewNetCheckCount(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取最新视频
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetNewVedioToday")]
        public ObjectResultEx GetNewVedioToday(SimpleParam<Guid?> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetNewVedioToday(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 新大屏

        /// <summary>
        /// 获取产品统计
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetProductRank")]
        public ObjectResultEx GetProductRank()
        {
            return ObjectResultEx.Instance(GovtWebService.GetProductRank(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取人员统计
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetPersonRank")]
        public ObjectResultEx GetPersonRank()
        {
            return ObjectResultEx.Instance(GovtWebService.GetPersonBank(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取执行检查统计
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetLawRank")]
        public ObjectResultEx GetLawRank()
        {
            return ObjectResultEx.Instance(GovtWebService.GetLawRank(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 统计数据
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetCountNum")]
        public ObjectResultEx GetCountNum()
        {
            return ObjectResultEx.Instance(GovtWebService.GetCountNum(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 区域入住分布排行
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAreaRank")]
        public ObjectResultEx GetAreaRank()
        {
            return ObjectResultEx.Instance(GovtWebService.GetAreaRank(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取入驻的企业地图
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAllCityMerchantCount")]
        public ObjectResultEx GetAllCityMerchantCount()
        {
            return ObjectResultEx.Instance(GovtWebService.GetAllCityMerchantCount(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 投诉折线图
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetComplainLine")]
        public ObjectResultEx GetComplainLine()
        {
            return ObjectResultEx.Instance(GovtWebService.GetComplainLine(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 板块占比
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetComDataRatio")]
        public ObjectResultEx GetComDataRatio()
        {
            return ObjectResultEx.Instance(GovtWebService.GetComDataRatio(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 投诉占比
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetComplainDataRatio")]
        public ObjectResultEx GetComplainDataRatio()
        {
            return ObjectResultEx.Instance(GovtWebService.GetComplainDataRatio(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 数据统计

        #region 责任协议

        /// <summary>
        /// 协议分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetAgreePage")]
        public ObjectResultEx GetAgreePage(PageParamList<RequestGovtAgree> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetAgreePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 编辑协议
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditAgree")]
        public ObjectResultEx EditAgree(RequestGovtAgree Param)
        {
            return ObjectResultEx.Instance(GovtWebService.EditAgree(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 协议详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetAgreeDetail")]
        public ObjectResultEx GetAgreeDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.GetAgreeDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 删除协议
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveAgree")]
        public ObjectResultEx RemoveAgree(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtWebService.RemoveAgree(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 责任协议

        #region 操作日志

        /// <summary>
        /// 获取操作日志
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetLogInfos")]
        public ObjectResultEx GetLogInfos()
        {
            return ObjectResultEx.Instance(GovtWebService.GetLogInfos(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 日志列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetHandlerLogPage")]
        public ObjectResultEx GetHandlerLogPage(PageParamList<RequestSystemLogInfo> pageParam)
        {
            return ObjectResultEx.Instance(GovtWebService.GetHandlerLogPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 批量阅读和广播
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        [HttpPost("EditHandlerLog")]
        public ObjectResultEx EditHandlerLog(List<Guid> keys, bool flag) 
        {
            return ObjectResultEx.Instance(GovtWebService.EditHandlerLog(keys, flag), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion 操作日志

        #region 综合

        /// <summary>
        /// 企业综合
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("GetAreaShow")]
        public ObjectResultEx GetAreaShow(SimpleParam<string> name, SimpleParam<string> type)
        {
            return ObjectResultEx.Instance(GovtWebService.GetAreaShow(name.Id, type.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 企业巡查
        /// </summary>
        /// <param name="type"></param>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <returns></returns>
        [HttpGet("GetAreaBill")]
        public ObjectResultEx GetAreaBill(SimpleParam<string> type, SimpleParam<string> name)
        {
            return ObjectResultEx.Instance(GovtWebService.GetAreaBill(type.Id, name.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 所有产品
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        [HttpGet("GetAllPro")]
        public ObjectResultEx GetAllPro(SimpleParam<string> keys)
        {
            return ObjectResultEx.Instance(GovtWebService.GetAllPro(keys.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion 综合
    }
}