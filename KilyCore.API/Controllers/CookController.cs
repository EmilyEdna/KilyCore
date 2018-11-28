using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.DataEntity.RequestMapper.Cook;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.API.Controllers
{
    [Route("api/[controller]")]
    public class CookController : BaseController
    {
        #region 厨师菜单
        /// <summary>
        /// 父级菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddCookParentMenu")]
        public ObjectResultEx AddCookParentMenu()
        {
            return ObjectResultEx.Instance(CookService.AddCookParentMenu(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("GetCookMenuDetail")]
        public ObjectResultEx GetCookMenuDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(CookService.GetCookMenuDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 厨师菜单分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetCookMenuPage")]
        public ObjectResultEx GetCookMenuPage(PageParamList<RequestCookMenu> pageParam)
        {
            return ObjectResultEx.Instance(CookService.GetCookMenuPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="Id"></param>
        [HttpPost("RemoveCookMenu")]
        public ObjectResultEx RemoveCookMenu(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(CookService.RemoveCookMenu(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("EditCookMenu")]
        public ObjectResultEx EditCookMenu(RequestCookMenu Param)
        {
            return ObjectResultEx.Instance(CookService.EditCookMenu(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 权限菜单树
        /// <summary>
        /// 获取权限菜单树
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetCookTree")]
        public ObjectResultEx GetCookTree()
        {
            return ObjectResultEx.Instance(CookService.GetCookTree(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 厨师角色
        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("GetCookAuthorPage")]
        public ObjectResultEx GetCookAuthorPage(PageParamList<RequestCookRole> pageParam)
        {
            return ObjectResultEx.Instance(CookService.GetCookAuthorPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("EditRole")]
        public ObjectResultEx EditRole(RequestCookRole Param)
        {
            return ObjectResultEx.Instance(CookService.EditRole(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveAuthorRole")]
        public ObjectResultEx RemoveAuthorRole(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(CookService.RemoveAuthorRole(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 厨师信息
        /// <summary>
        /// 厨师信息
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetCookInfoPage")]
        public ObjectResultEx GetCookInfoPage(PageParamList<RequestCookInfo> pageParam)
        {
            return ObjectResultEx.Instance(CookService.GetCookInfoPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 厨师详情
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetCookInfoDetail")]
        public ObjectResultEx GetCookInfoDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(CookService.GetCookInfoDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 审核厨师信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditCookInfo")]
        public ObjectResultEx AuditCookInfo(RequestAudit Param)
        {
            return ObjectResultEx.Instance(CookService.AuditCookInfo(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 服务管理
        /// <summary>
        /// 厨师开通的服务分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetCookServicePage")]
        public ObjectResultEx GetCookServicePage(PageParamList<RequestCookInfo> pageParam)
        {
            return ObjectResultEx.Instance(CookService.GetCookServicePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 启用账号
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("StartUse")]
        public ObjectResultEx StartUse(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(CookService.StartUse(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 停用账号
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("BlockUp")]
        public ObjectResultEx BlockUp(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(CookService.BlockUp(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 确认缴费
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("CheckPayment")]
        public ObjectResultEx CheckPayment(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(CookService.CheckPayment(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}