using System;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.API.Controllers
{
    [Route("api/[controller]")]
    public class EnterpriseController : BaseController
    {
        #region 集团菜单
        /// <summary>
        /// 修改新增菜单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("EditEnterpriseMenu")]
        public ObjectResultEx EditEnterpriseMenu(RequestEnterpriseMenu Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.EditEnterpriseMenu(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("GetEnterpriseMenuDetail")]
        public ObjectResultEx GetEnterpriseMenuDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.GetEnterpriseMenuDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取父节菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetEnterpriseParentMenu")]
        public ObjectResultEx GetEnterpriseParentMenu()
        {
            return ObjectResultEx.Instance(EnterpriseService.GetEnterpriseParentMenu(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 企业菜单分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetEnterpriseMenuPage")]
        public ObjectResultEx GetEnterpriseMenuPage(PageParamList<RequestEnterpriseMenu> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseService.GetEnterpriseMenuPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除企业菜单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("RemoveEnterpriseMenu")]
        public ObjectResultEx RemoveEnterpriseMenu(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.RemoveEnterpriseMenu(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 角色权限菜单
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetEnterpriseTree")]
        public ObjectResultEx GetEnterpriseTree()
        {
            return ObjectResultEx.Instance(EnterpriseService.GetEnterpriseTree(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 集团角色
        /// <summary>
        /// 集团角色分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetCompanyRoleAuthorPage")]
        public ObjectResultEx GetCompanyRoleAuthorPage(PageParamList<RequestEnterpriseRoleAuthor> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseService.GetCompanyRoleAuthorPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑集团角色菜单
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditEnterpriseRoleAuthor")]
        public ObjectResultEx EditEnterpriseRoleAuthor(RequestEnterpriseRoleAuthor Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.EditEnterpriseRoleAuthor(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 角色列表分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetRoleAuthorPage")]
        public ObjectResultEx GetRoleAuthorPage(PageParamList<RequestEnterpriseRoleAuthor> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseService.GetRoleAuthorPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveEnterpriseRoleAuthor")]
        public ObjectResultEx RemoveEnterpriseRoleAuthor(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.RemoveEnterpriseRoleAuthor(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetRoleAuthorList")]
        public ObjectResultEx GetRoleAuthorList() {
            return ObjectResultEx.Instance(EnterpriseService.GetRoleAuthorList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("DistributionRole")]
        public ObjectResultEx DistributionRole(RequestEnterpriseRoleAuthor Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.DistributionRole(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}