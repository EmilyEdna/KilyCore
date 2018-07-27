using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.DataEntity.RequestMapper.Repast;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.API.Controllers
{
    [Route("api/[controller]")]
    public class RepastWebController : BaseController
    {
        #region 获取全局集团菜单
        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetRepastMenu")]
        public ObjectResultEx GetRepastMenu()
        {
            return ObjectResultEx.Instance(RepastWebService.GetRepastMenu(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取权限菜单树
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetRepastWebTree")]
        public ObjectResultEx GetRepastWebTree()
        {
            return ObjectResultEx.Instance(RepastWebService.GetRepastWebTree(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 基础管理
        #region 商家资料
        /// <summary>
        /// 商家资料分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetMerChantInfo")]
        public ObjectResultEx GetMerchantInfo(PageParamList<RequestMerchant> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetMerchantInfo(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取详细
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetMerchantDetail")]
        public ObjectResultEx GetMerchantDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.GetMerchantDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑商家资料
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditMerchant")]
        public ObjectResultEx EditMerchant(RequestMerchant Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditMerchant(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 保存合同
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("SaveContract")]
        public ObjectResultEx SaveContract(RequestStayContract Param)
        {
            return ObjectResultEx.Instance(RepastWebService.SaveContract(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 商家认证
        /// <summary>
        /// 商家认证
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditMerchantIdent")]
        public ObjectResultEx EditMerchantIdent(RequestRepastIdent Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditMerchantIdent(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 权限角色
        /// <summary>
        /// 新增账户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditRoleAuthor")]
        public ObjectResultEx EditRoleAuthor(RequestRoleAuthorWeb Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditRoleAuthor(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 账户分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetRoleAuthorPage")]
        public ObjectResultEx GetRoleAuthorPage(PageParamList<RequestRoleAuthorWeb> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetRoleAuthorPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除账户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveRole")]
        public ObjectResultEx RemoveRole(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveRole(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 人员管理
        /// <summary>
        /// 用户分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetMerchantUserPage")]
        public ObjectResultEx GetMerchantUserPage(PageParamList<RequestMerchantUser> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetMerchantUserPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑子账户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditUser")]
        public ObjectResultEx EditUser(RequestMerchantUser Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditUser(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取子账户详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetUserDetail")]
        public ObjectResultEx GetUserDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.GetUserDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除子账号
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveUser")]
        public ObjectResultEx RemoveUser(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveUser(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #endregion
    }
}