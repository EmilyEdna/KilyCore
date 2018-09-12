using System;
using KilyCore.DataEntity.RequestMapper.Govt;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.API.Controllers
{
    [Route("api/[controller]")]
    public class GovtController : BaseController
    {
        #region 政府监管
        /// <summary>
        /// 获取父级菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddGovtParentMenu")]
        public ObjectResultEx AddGovtParentMenu()
        {
            return ObjectResultEx.Instance(GovtService.AddGovtParentMenu(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 政府菜单分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetGovtMenuPage")]
        public ObjectResultEx GetGovtMenuPage(PageParamList<RequestGovtMenu> pageParam)
        {
            return ObjectResultEx.Instance(GovtService.GetGovtMenuPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取政府菜单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("GetGovtMenuDetail")]
        public ObjectResultEx GetGovtMenuDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtService.GetGovtMenuDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除政府菜单
        /// </summary>
        /// <param name="Id"></param>
        [HttpPost("RemoveGovtMenu")]
        public ObjectResultEx RemoveGovtMenu(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtService.RemoveGovtMenu(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 新增政府菜单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("EditGovtMenu")]
        public ObjectResultEx EditGovtMenu(RequestGovtMenu Param)
        {
            return ObjectResultEx.Instance(GovtService.EditGovtMenu(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 权限菜单树
        /// <summary>
        /// 获取权限菜单树
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetGovtTree")]
        public ObjectResultEx GetGovtTree()
        {
            return ObjectResultEx.Instance(GovtService.GetGovtTree(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 角色权限
        /// <summary>
        /// 权限分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetAuthorPage")]
        public ObjectResultEx GetAuthorPage(PageParamList<RequestGovtRoleAuthor> pageParam)
        {
            return ObjectResultEx.Instance(GovtService.GetAuthorPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveAuthor")]
        public ObjectResultEx RemoveAuthor(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(GovtService.RemoveAuthor(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑权限
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditAuthor")]
        public ObjectResultEx EditAuthor(RequestGovtRoleAuthor Param)
        {
            return ObjectResultEx.Instance(GovtService.EditAuthor(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 政府账号
        /// <summary>
        /// 账号分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetInfoPage")]
        public ObjectResultEx GetInfoPage(PageParamList<RequestGovtInfo> pageParam)
        {
            return ObjectResultEx.Instance(GovtService.GetInfoPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑账号
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditInfo")]
        public ObjectResultEx EditInfo(RequestGovtInfo Param)
        {
            return ObjectResultEx.Instance(GovtService.EditInfo(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}