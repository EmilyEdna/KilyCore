using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Finance;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.API.Controllers
{
    [Route("api/[controller]")]
    public class EnterpriseWebController : BaseController
    {
        #region 获取全局集团菜单
        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetEnterpriseMenu")]
        public ObjectResultEx GetEnterpriseMenu()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetEnterpriseMenu(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取权限菜单树
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetEnterpriseWebTree")]
        public ObjectResultEx GetEnterpriseWebTree()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetEnterpriseWebTree(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region  企业信息
        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetEnterpriseInfo")]
        public ObjectResultEx GetEnterpriseInfo(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetEnterpriseInfo(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑企业
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditEnterprise")]
        public ObjectResultEx EditEnterprise(RequestEnterprise Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditEnterprise(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 保存合同和缴费凭证
        /// <summary>
        /// 保存合同和缴费凭证
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("SaveContract")]
        public ObjectResultEx SaveContract(RequestStayContract Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.SaveContract(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 人员管理
        /// <summary>
        /// 获取人员管理分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetUserPage")]
        public ObjectResultEx GetUserPage(PageParamList<RequestEnterpriseUser> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetUserPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditUser")]
        public ObjectResultEx EditUser(RequestEnterpriseUser Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditUser(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetUserDetail")]
        public ObjectResultEx GetUserDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetUserDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除子账号
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveUser")]
        public ObjectResultEx RemoveUser(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveUser(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 集团账户权限列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetRoleAuthorList")]
        public ObjectResultEx GetRoleAuthorList()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetRoleAuthorList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 集团账户
        /// <summary>
        /// 新增账户
        /// </summary>
        /// <returns></returns>
        [HttpPost("EditRoleAuthor")]
        public ObjectResultEx EditRoleAuthor(RequestRoleAuthorWeb Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditRoleAuthor(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 账户分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetRoleAuthorPage")]
        public ObjectResultEx GetRoleAuthorPage(PageParamList<RequestRoleAuthorWeb> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetRoleAuthorPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除账户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveRole")]
        public ObjectResultEx RemoveRole(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveRole(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 企业认证
        /// <summary>
        /// 企业认证
        /// </summary>
        /// <returns></returns>
        [HttpPost("EditEnterpriseIdent")]
        public ObjectResultEx EditEnterpriseIdent(RequestEnterpriseIdent Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditEnterpriseIdent(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}