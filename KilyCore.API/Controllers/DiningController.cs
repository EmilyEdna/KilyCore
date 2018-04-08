using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.DataEntity.RequestMapper.Dining;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.API.Controllers
{
    [Route("api/[controller]")]
    public class DiningController : BaseController
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
            return ObjectResultEx.Instance(DiningService.GetMerchantPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取商家详情
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetMerchantDetail")]
        public ObjectResultEx GetMerchantDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(DiningService.GetMerchantDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 审核商家
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditMerchant")]
        public ObjectResultEx AuditMerchant(RequestAudit Param)
        {
            return ObjectResultEx.Instance(DiningService.AuditMerchant(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 餐饮菜单
        /// <summary>
        /// 餐饮菜单分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetDiningMenuPage")]
        public ObjectResultEx GetDiningMenuPage(PageParamList<RequestDiningMenu> pageParam)
        {
            return ObjectResultEx.Instance(DiningService.GetDiningMenuPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditDiningMenu")]
        public ObjectResultEx EditDiningMenu(RequestDiningMenu Param)
        {
            return ObjectResultEx.Instance(DiningService.EditDiningMenu(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveMenu")]
        public ObjectResultEx RemoveMenu(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(DiningService.RemoveMenu(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 菜单详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetDiningMenuDetail")]
        public ObjectResultEx GetDiningMenuDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(DiningService.GetDiningMenuDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 显示父节菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddDiningParentMenu")]
        public ObjectResultEx AddDiningParentMenu() {
            return ObjectResultEx.Instance(DiningService.AddDiningParentMenu(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
            return ObjectResultEx.Instance(DiningService.GetDiningTree(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
            return ObjectResultEx.Instance(DiningService.EditRole(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 角色分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetAuthorPage")]
        public ObjectResultEx GetAuthorPage(PageParamList<RequestAuthorRole> pageParam)
        {
            return ObjectResultEx.Instance(DiningService.GetAuthorPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveAuthorRole")]
        public ObjectResultEx RemoveAuthorRole(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(DiningService.RemoveAuthorRole(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}