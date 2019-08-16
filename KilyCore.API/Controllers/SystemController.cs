using System;
using System.Collections.Generic;
using System.Linq;
using KilyCore.Cache;
using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.Extension.ResultExtension;
using KilyCore.Extension.SendMessage;
using KilyCore.Extension.SessionExtension;
using KilyCore.Extension.Token;
using KilyCore.Extension.ValidateExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.API.Controllers
{
    /// <summary>
    /// 系统接口
    /// </summary>
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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveMenu")]
        public ObjectResultEx RemoveMenu(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(SystemService.RemoveMenu(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 修改新增菜单
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditMenu")]
        public ObjectResultEx EditMenu(RequestMenu Param)
        {
            return ObjectResultEx.Instance(SystemService.EditMenu(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetMenuDetail")]
        public ObjectResultEx GetMenuDetail(SimpleParam<Guid> Param)
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
        public ObjectResultEx RemoveAuthorRole(SimpleParam<Guid> Param)
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
        /// <param name="LoginValidate"></param>
        /// <returns></returns>
        [HttpPost("SystemLogin")]
        [AllowAnonymous]
        public ObjectResultEx SystemLogin(RequestValidate LoginValidate)
        {
            try
            {
                ResponseAdmin SysAdmin = SystemService.SystemLogin(LoginValidate);
                string Code = string.Empty;
                if (!LoginValidate.IsApp)
                {
                    Code = CacheFactory.Cache().GetCache<string>("ValidateCode").Trim();
                    if (SysAdmin != null && Code.ToUpper().Equals(LoginValidate.ValidateCode.Trim().ToUpper()))
                    {
                        CookieInfo cookie = new CookieInfo();
                        VerificationExtension.WriteToken(cookie, SysAdmin);
                        return ObjectResultEx.Instance(new { ResponseCookieInfo.RSAToKen, ResponseCookieInfo.RSAApiKey, ResponseCookieInfo.RSASysKey, SysAdmin }, 1, RetrunMessge.SUCCESS, HttpCode.Success);
                    }
                    else if (!Code.ToUpper().Equals(LoginValidate.ValidateCode.Trim().ToUpper()))
                        return ObjectResultEx.Instance(null, -1, "验证码错误", HttpCode.NoAuth);
                    else
                        return ObjectResultEx.Instance(null, -1, "登录失败或账户冻结", HttpCode.NoAuth);
                }
                else
                {
                    if (SysAdmin != null)
                    {
                        CookieInfo cookie = new CookieInfo();
                        VerificationExtension.WriteToken(cookie, SysAdmin);
                        return ObjectResultEx.Instance(new { ResponseCookieInfo.RSAToKen, ResponseCookieInfo.RSAApiKey, ResponseCookieInfo.RSASysKey, SysAdmin }, 1, RetrunMessge.SUCCESS, HttpCode.Success);
                    }
                    else if (!Code.ToUpper().Equals(LoginValidate.ValidateCode.Trim().ToUpper()))
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
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCode")]
        [AllowAnonymous]
        public ObjectResultEx GetCode()
        {
            String Code = ValidateCode.CreateValidateCode();
            CacheFactory.Cache().WriteCaches(Code, "ValidateCode", 2);
            return ObjectResultEx.Instance(Code, 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取手机短信验证码
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet("GetPhoneCode")]
        [AllowAnonymous]
        public ObjectResultEx GetPhoneCode(SimpleParam<String> Param)
        {
            String Code = ValidateCode.CreateCode();
            CacheFactory.Cache().WriteCaches(Code, "PhoneCode", 5);
            String Contents = $"你的手机验证码是：{Code}，请在5分钟内输入，如非本人操作，请忽略此短信。";
            return ObjectResultEx.Instance(PhoneSMS.SendPhoneMsg(Param.Parameter, Contents), 1, RetrunMessge.SUCCESS, HttpCode.Success);
            //return ObjectResultEx.Instance(Code, 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        public ObjectResultEx GetSystemAdminTree(SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(SystemService.GetSystemAdminTree(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 用户管理
        /// <summary>
        /// 中间系统调用
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("InsertAdmin")]
        [AllowAnonymous]
        public ObjectResultEx InsertAdmin(RequestAdmin Param)
        {
            return ObjectResultEx.Instance(SystemService.InsertAdmin(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
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
        public ObjectResultEx RemoveAdmin(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(SystemService.RemoveAdmin(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 启用账户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("OpenAdmin")]
        public ObjectResultEx OpenAdmin(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(SystemService.OpenAdmin(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetAdminDetail")]
        public ObjectResultEx GetAdminDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(SystemService.GetAdminDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取银行卡信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetBankInfo")]
        public ObjectResultEx GetBankInfo()
        {
            return ObjectResultEx.Instance(SystemService.GetBankInfo(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 回收或开启网签
        /// </summary>
        /// <param name="key"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("CG")]
        public ObjectResultEx CG(SimpleParam<Guid> key, SimpleParam<bool> Param)
        {
            return ObjectResultEx.Instance(SystemService.CG(key.Id, Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取可以签到合同的代理商
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetAuthorAdmin")]
        public ObjectResultEx GetAuthorAdmin(SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(SystemService.GetAuthorAdmin(Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 省市区乡
        /// <summary>
        /// 获取省份
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetProvince")]
        [AllowAnonymous]
        public ObjectResultEx GetProvince()
        {
            return ObjectResultEx.Instance(SystemService.GetProvince(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取城市
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetCity")]
        [AllowAnonymous]
        public ObjectResultEx GetCity(SimpleParam<int> Param)
        {
            return ObjectResultEx.Instance(SystemService.GetCity(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取区县
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetArea")]
        [AllowAnonymous]
        public ObjectResultEx GetArea(SimpleParam<int> Param)
        {
            return ObjectResultEx.Instance(SystemService.GetArea(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取乡镇
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetTown")]
        [AllowAnonymous]
        public ObjectResultEx GetTown(SimpleParam<int> Param)
        {
            return ObjectResultEx.Instance(SystemService.GetTown(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        public ObjectResultEx RemovePreson(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(SystemService.RemovePreson(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetPresonDetail")]
        public ObjectResultEx GetPresonDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(SystemService.GetPresonDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 首页人员查询
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetPresonDetailWeb")]
        [AllowAnonymous]
        public ObjectResultEx GetPresonDetailWeb(SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(SystemService.GetPresonDetailWeb(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        /// <summary>
        /// 审核合同
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditContract")]
        public ObjectResultEx AuditContract(RequestAudit Param)
        {
            return ObjectResultEx.Instance(SystemService.AuditContract(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 取审核记录
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetContractRecord")]
        public ObjectResultEx GetContractRecord(PageParamList<RequestAudit> pageParam)
        {
            return ObjectResultEx.Instance(SystemService.GetContractRecord(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveRecord")]
        public ObjectResultEx RemoveRecord(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(SystemService.RemoveRecord(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 确认缴费
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditContract")]
        public ObjectResultEx EditContract(SimpleParam<Guid> Key, SimpleParam<decimal> Param)
        {
            return ObjectResultEx.Instance(SystemService.EditContract(Key.Id, Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 支付宝微信银行支付
        /// <summary>
        /// 支付宝支付
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AliPay")]
        public ObjectResultEx AliPay(SimpleParam<int> Param)
        {
            return ObjectResultEx.Instance(SystemService.AliPay(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 微信支付
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("WxPay")]
        public ObjectResultEx WxPay(SimpleParam<int> Param)
        {
            return ObjectResultEx.Instance(SystemService.WxPay(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 查询支付宝支付
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        [HttpPost("AliQueryPay")]
        public ObjectResultEx AliQueryPay(SimpleParam<String> Key)
        {
            return ObjectResultEx.Instance(SystemService.AliQueryPay(Key.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 更新支付
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditPay")]
        public ObjectResultEx EditPay(RequestStayContract Param)
        {
            return ObjectResultEx.Instance(SystemService.EditPay(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 消息盒子
        /// <summary>
        /// 消息盒子分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetMsgPage")]
        public ObjectResultEx GetMsgPage(PageParamList<Object> pageParam)
        {
            return ObjectResultEx.Instance(SystemService.GetMsgPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 新闻资讯
        /// <summary>
        /// 新闻分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetNewsPage")]
        [AllowAnonymous]
        public ObjectResultEx GetNewsPage(PageParamList<RequestSystemNews> pageParam)
        {
            return ObjectResultEx.Instance(SystemService.GetNewsPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑新闻
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditNews")]
        public ObjectResultEx EditNews(RequestSystemNews Param)
        {
            return ObjectResultEx.Instance(SystemService.EditNews(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 新闻详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetNewsDetail")]
        [AllowAnonymous]
        public ObjectResultEx GetNewsDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(SystemService.GetNewsDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveNews")]
        public ObjectResultEx RemoveNews(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(SystemService.RemoveNews(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 数据报表
        /// <summary>
        /// 二维码统计
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetCodeCountCenter")]
        [AllowAnonymous]
        public ObjectResultEx GetCodeCountCenter()
        {
            return ObjectResultEx.Instance(SystemService.GetCodeCountCenter(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 入住企业统计
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetCompanyCountCenter")]
        [AllowAnonymous]
        public ObjectResultEx GetCompanyCountCenter()
        {
            return ObjectResultEx.Instance(SystemService.GetCompanyCountCenter(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 产品统计
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetProductCountCenter")]
        [AllowAnonymous]
        public ObjectResultEx GetProductCountCenter()
        {
            return ObjectResultEx.Instance(SystemService.GetProductCountCenter(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取合同统计
        /// </summary>
        /// <param name="Range"></param>
        /// <returns></returns>
        [HttpPost("GetContractCountCenter")]
        [AllowAnonymous]
        public ObjectResultEx GetContractCountCenter(RequestRangeDate Range)
        {
            return ObjectResultEx.Instance(SystemService.GetContractCountCenter(Range), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 订单管理
        #region 订单中心
        /// <summary>
        /// 订单分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetOrderPage")]
        public ObjectResultEx GetOrderPage(PageParamList<RequestSystemOrder> pageParam)
        {
            return ObjectResultEx.Instance(SystemService.GetOrderPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 下单
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("OrderEdit")]
        public ObjectResultEx OrderEdit(RequestSystemOrder Param)
        {
            return ObjectResultEx.Instance(SystemService.OrderEdit(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetOrderDetail")]
        public ObjectResultEx GetOrderDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(SystemService.GetOrderDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 订单状态
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("OrderCheck")]
        public ObjectResultEx OrderCheck(RequestSystemOrder Param)
        {
            return ObjectResultEx.Instance(SystemService.OrderCheck(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 订单日志
        /// <summary>
        /// 日志分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetOrderLogPage")]
        public ObjectResultEx GetOrderLogPage(PageParamList<RequestSystemOrderLog> pageParam)
        {
            return ObjectResultEx.Instance(SystemService.GetOrderLogPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditOrderLog")]
        public ObjectResultEx EditOrderLog(RequestSystemOrderLog Param)
        {
            return ObjectResultEx.Instance(SystemService.EditOrderLog(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveLog")]
        public ObjectResultEx RemoveLog(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(SystemService.RemoveLog(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 日志详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetOrderLogDetail")]
        public ObjectResultEx GetOrderLogDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(SystemService.GetOrderLogDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 评分记录
        /// <summary>
        /// 评分记录
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetOrderScorePage")]
        public ObjectResultEx GetOrderScorePage(PageParamList<RequestSystemOrderScore> pageParam)
        {
            return ObjectResultEx.Instance(SystemService.GetOrderScorePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 添加评分
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditOrderScore")]
        public ObjectResultEx EditOrderScore(RequestSystemOrderScore Param)
        {
            return ObjectResultEx.Instance(SystemService.EditOrderScore(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 评分详情
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        [HttpPost("GetOrderScoreDetail")]
        public ObjectResultEx GetOrderScoreDetail(SimpleParam<Guid> Key)
        {
            return ObjectResultEx.Instance(SystemService.GetOrderScoreDetail(Key.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 线下人员
        /// <summary>
        /// 线下人员分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetPersonOffPage")]
        public ObjectResultEx GetPersonOffPage(PageParamList<RequestSystemPersonOff> pageParam)
        {
            return ObjectResultEx.Instance(SystemService.GetPersonOffPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取人员详情
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        [HttpPost("GetOffDetail")]
        public ObjectResultEx GetOffDetail(SimpleParam<Guid> Key)
        {
            return ObjectResultEx.Instance(SystemService.GetOffDetail(Key.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #endregion
    }
}