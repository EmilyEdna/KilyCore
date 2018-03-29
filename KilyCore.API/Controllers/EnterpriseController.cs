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
        #region 企业菜单
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
        #endregion

    }
}