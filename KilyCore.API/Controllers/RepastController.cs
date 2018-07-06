using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.DataEntity.RequestMapper.Repast;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.API.Controllers
{
    [Route("api/[controller]")]
    public class RepastController : BaseController
    {
        #region  商家资料
        /// <summary>
        /// 商家分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetMerchantPage")]
        public ObjectResultEx GetMerchantPage(PageParamList<RequestMerchant> pageParam)
        {
            return ObjectResultEx.Instance(RepastService.GetMerchantPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取商家详情
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetMerchantDetail")]
        public ObjectResultEx GetMerchantDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastService.GetMerchantDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 审核商家
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditMerchant")]
        public ObjectResultEx AuditMerchant(RequestAudit Param)
        {
            return ObjectResultEx.Instance(RepastService.AuditMerchant(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 餐饮菜单
        /// <summary>
        /// 餐饮菜单分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetDiningMenuPage")]
        public ObjectResultEx GetDiningMenuPage(PageParamList<RequestRepastMenu> pageParam)
        {
            return ObjectResultEx.Instance(RepastService.GetDiningMenuPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditDiningMenu")]
        public ObjectResultEx EditDiningMenu(RequestRepastMenu Param)
        {
            return ObjectResultEx.Instance(RepastService.EditDiningMenu(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveMenu")]
        public ObjectResultEx RemoveMenu(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastService.RemoveMenu(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 菜单详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetDiningMenuDetail")]
        public ObjectResultEx GetDiningMenuDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastService.GetDiningMenuDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 显示父节菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddDiningParentMenu")]
        public ObjectResultEx AddDiningParentMenu()
        {
            return ObjectResultEx.Instance(RepastService.AddDiningParentMenu(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 权限菜单树
        /// <summary>
        /// 权限菜单树
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetDiningTree")]
        public ObjectResultEx GetDiningTree()
        {
            return ObjectResultEx.Instance(RepastService.GetDiningTree(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 角色管理
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        [HttpPost("EditRole")]
        public ObjectResultEx EditRole(RequestAuthorRole Param)
        {
            return ObjectResultEx.Instance(RepastService.EditRole(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 角色分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetAuthorPage")]
        public ObjectResultEx GetAuthorPage(PageParamList<RequestAuthorRole> pageParam)
        {
            return ObjectResultEx.Instance(RepastService.GetAuthorPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveAuthorRole")]
        public ObjectResultEx RemoveAuthorRole(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastService.RemoveAuthorRole(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 认证审核
        /// <summary>
        /// 商家认证分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetDiningIdentPage")]
        public ObjectResultEx GetDiningIdentPage(PageParamList<RequestDiningIdent> pageParam)
        {
            return ObjectResultEx.Instance(RepastService.GetDiningIdentPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取认证审核详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetDiningIdentDetail")]
        public ObjectResultEx GetDiningIdentDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastService.GetDiningIdentDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 认证审核
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditIdent")]
        public ObjectResultEx AuditIdent(RequestAudit Param)
        {
            return ObjectResultEx.Instance(RepastService.AuditIdent(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 认证缴费
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditPayment")]
        public ObjectResultEx AuditPayment(RequestPayment Param) {
            return ObjectResultEx.Instance(RepastService.AuditPayment(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}