using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.DataEntity.RequestMapper.Repast;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Authorization;
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
        /// 下拉字典类型
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetDictionaryList")]
        public ObjectResultEx GetDictionaryList()
        {
            return ObjectResultEx.Instance(RepastWebService.GetDictionaryList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        [HttpPost("GetMerchantInfoPage")]
        public ObjectResultEx GetMerchantInfoPage(PageParamList<RequestMerchant> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetMerchantInfoPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取详细
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetMerchantDetail")]
        public ObjectResultEx GetMerchantDetail(SimpleParam<Guid> Param)
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
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditMerchantAccount")]
        public ObjectResultEx EditMerchantAccount(RequestMerchant Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditMerchantAccount(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 修改区域
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditMerchantArea")]
        public ObjectResultEx EditMerchantArea(RequestMerchant Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditMerchantArea(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取合同审核记录
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetContractAudit")]
        public ObjectResultEx GetContractAudit(PageParamList<RequestAudit> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetContractAudit(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        public ObjectResultEx RemoveRole(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveRole(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取权限角色
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetRoleAuthorList")]
        public ObjectResultEx GetRoleAuthorList()
        {
            return ObjectResultEx.Instance(RepastWebService.GetRoleAuthorList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        public ObjectResultEx GetUserDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.GetUserDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除子账号
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveUser")]
        public ObjectResultEx RemoveUser(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveUser(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 人员列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetMerchantList")]
        public ObjectResultEx GetMerchantList()
        {
            return ObjectResultEx.Instance(RepastWebService.GetMerchantList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 集团账户
        /// <summary>
        /// 集团账户列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetChildInfoPage")]
        public ObjectResultEx GetChildInfoPage(PageParamList<RequestMerchant> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetChildInfoPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑子账户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditChildInfo")]
        public ObjectResultEx EditChildInfo(RequestMerchant Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditChildInfo(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 餐饮字典
        /// <summary>
        /// 字典分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetDicPage")]
        public ObjectResultEx GetDicPage(PageParamList<RequestRepastDictionary> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetDicPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除码表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveDic")]
        public ObjectResultEx RemoveDic(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveDic(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 字典详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetDicDetail")]
        public ObjectResultEx GetDicDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.GetDicDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑字典
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditDic")]
        public ObjectResultEx EditDic(RequestRepastDictionary Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditDic(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 升级续费
        /// <summary>
        /// 查看版本信息
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetLvPage")]
        public ObjectResultEx GetLvPage(PageParamList<RequestRepastLevelUp> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetLvPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑续费
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditContinued")]
        public ObjectResultEx EditContinued(RequestRepastContinued Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditContinued(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑升级
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditUpLevel")]
        public ObjectResultEx EditUpLevel(RequestRepastUpLevel Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditUpLevel(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 续费记录
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetContinuedPage")]
        public ObjectResultEx GetContinuedPage(PageParamList<RequestRepastContinued> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetContinuedPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 升级记录
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetUpLevelPage")]
        public ObjectResultEx GetUpLevelPage(PageParamList<RequestRepastUpLevel> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetUpLevelPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 审核审计续费
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditContinuedAndLevel")]
        public ObjectResultEx AuditContinuedAndLevel(SimpleParam<Guid> Key, SimpleParam<bool> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.AuditContinuedAndLevel(Key.Id, Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #endregion

        #region 功能管理
        #region 供应商
        /// <summary>
        /// 供应商列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetSupplierPage")]
        public ObjectResultEx GetSupplierPage(PageParamList<RequestRepastSupplier> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetSupplierPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除供应商
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveSupplier")]
        public ObjectResultEx RemoveSupplier(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveSupplier(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑供应商
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditSupplier")]
        public ObjectResultEx EditSupplier(RequestRepastSupplier Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditSupplier(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 供应商列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetSupplierList")]
        public ObjectResultEx GetSupplierList()
        {
            return ObjectResultEx.Instance(RepastWebService.GetSupplierList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 进货台账
        /// <summary>
        /// 进货台账分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetBuybillPage")]
        public ObjectResultEx GetBuybillPage(PageParamList<RequestRepastBuybill> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetBuybillPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除进货台账
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveBuybill")]
        public ObjectResultEx RemoveBuybill(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveBuybill(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 进货台账详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetBuybillDetail")]
        public ObjectResultEx GetBuybillDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.GetBuybillDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑进货台账
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditBuybill")]
        public ObjectResultEx EditBuybill(RequestRepastBuybill Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditBuybill(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 销售台账
        /// <summary>
        /// 销售台账分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetSellbillPage")]
        public ObjectResultEx GetSellbillPage(PageParamList<RequestRepastSellbill> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetSellbillPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除销售台账
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveSellbill")]
        public ObjectResultEx RemoveSellbill(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveSellbill(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 销售台账详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetSellbillDetail")]
        public ObjectResultEx GetSellbillDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.GetSellbillDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑销售台账
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditSellbill")]
        public ObjectResultEx EditSellbill(RequestRepastSellbill Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditSellbill(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 实时监控
        /// <summary>
        /// 视频分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetVideoPage")]
        public ObjectResultEx GetVideoPage(PageParamList<RequestRepastVideo> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetVideoPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑视频
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditVideo")]
        public ObjectResultEx EditVideo(RequestRepastVideo Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditVideo(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除视频
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveVideo")]
        public ObjectResultEx RemoveVideo(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveVideo(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #endregion

        #region 菜品管理
        /// <summary>
        /// 菜品分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetDishPage")]
        public ObjectResultEx GetDishPage(PageParamList<RequestRepastDish> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetDishPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 菜品详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetDishDetail")]
        public ObjectResultEx GetDishDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.GetDishDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除菜品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveDish")]
        public ObjectResultEx RemoveDish(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveDish(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑菜品
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditDish")]
        public ObjectResultEx EditDish(RequestRepastDish Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditDish(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 菜品列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetDishList")]
        public ObjectResultEx GetDishList()
        {
            return ObjectResultEx.Instance(RepastWebService.GetDishList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 溯源追踪
        #region 原料溯源
        /// <summary>
        /// 原料溯源分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetStuffPage")]
        public ObjectResultEx GetStuffPage(PageParamList<RequestRepastStuff> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetStuffPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除溯源信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveStuff")]
        public ObjectResultEx RemoveStuff(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveStuff(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑溯源信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditStuff")]
        public ObjectResultEx EditStuff(RequestRepastStuff Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditStuff(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取溯源详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetStuffDetail")]
        public ObjectResultEx GetStuffDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.GetStuffDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 仓库原料列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetInStorageList")]
        public ObjectResultEx GetInStorageList(SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.GetInStorageList(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 留样管理
        /// <summary>
        /// 留样分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetSamplePage")]
        public ObjectResultEx GetSamplePage(PageParamList<RequestRepastSample> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetSamplePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑留样
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditSample")]
        public ObjectResultEx EditSample(RequestRepastSample Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditSample(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除留样
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveSample")]
        public ObjectResultEx RemoveSample(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveSample(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 抽样管理
        /// <summary>
        /// 抽样分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetDrawPage")]
        public ObjectResultEx GetDrawPage(PageParamList<RequestRepastDraw> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetDrawPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑抽样
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditDraw")]
        public ObjectResultEx EditDraw(RequestRepastDraw Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditDraw(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除抽样
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveDraw")]
        public ObjectResultEx RemoveDraw(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveDraw(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 废物处理
        /// <summary>
        /// 废物分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetDuckPage")]
        public ObjectResultEx GetDuckPage(PageParamList<RequestRepastDuck> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetDuckPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑废物
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditDuck")]
        public ObjectResultEx EditDuck(RequestRepastDuck Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditDuck(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除废物
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveDuck")]
        public ObjectResultEx RemoveDuck(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveDuck(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 消毒管理
        /// <summary>
        /// 消毒分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetDisinfectPage")]
        public ObjectResultEx GetDisinfectPage(PageParamList<RequestRepastDisinfect> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetDisinfectPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑消毒
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditDisinfect")]
        public ObjectResultEx EditDisinfect(RequestRepastDisinfect Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditDisinfect(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除消毒
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveDisinfect")]
        public ObjectResultEx RemoveDisinfect(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveDisinfect(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取消毒详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetDisinfectDetail")]
        public ObjectResultEx GetDisinfectDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.GetDisinfectDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 添加剂管理
        /// <summary>
        /// 添加剂分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetAdditivePage")]
        public ObjectResultEx GetAdditivePage(PageParamList<RequestRepastAdditive> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetAdditivePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑添加剂
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditAdditive")]
        public ObjectResultEx EditAdditive(RequestRepastAdditive Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditAdditive(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除添加剂
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveAdditive")]
        public ObjectResultEx RemoveAdditive(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveAdditive(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取添加剂详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetAdditiveDetail")]
        public ObjectResultEx GetAdditiveDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.GetAdditiveDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #endregion

        #region 仓库管理

        #region 原料仓库-入库
        /// <summary>
        /// 原料入库分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetInStoragePage")]
        public ObjectResultEx GetInStoragePage(PageParamList<RequestRepastInStorage> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetInStoragePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑原料入库
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditInStorage")]
        public ObjectResultEx EditInStorage(RequestRepastInStorage Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditInStorage(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除原料入库
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveInStorage")]
        public ObjectResultEx RemoveInStorage(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveInStorage(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 原料入库详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetInStorageDetail")]
        public ObjectResultEx GetInStorageDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.GetInStorageDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 原料仓库-出库
        /// <summary>
        /// 出库分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetOutStoragePage")]
        public ObjectResultEx GetOutStoragePage(PageParamList<RequestRepastOutStorage> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetOutStoragePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑出库
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditOutStorage")]
        public ObjectResultEx EditOutStorage(RequestRepastOutStorage Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditOutStorage(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除出库
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveOutStorage")]
        public ObjectResultEx RemoveOutStorage(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveOutStorage(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 物品仓库-入库
        /// <summary>
        /// 物品入库分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetInStockPage")]
        public ObjectResultEx GetInStockPage(PageParamList<RequestRepastArticleInStock> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetInStockPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除物品入库
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveInStock")]
        public ObjectResultEx RemoveInStock(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveInStock(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑物品入库
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditInStock")]
        public ObjectResultEx EditInStock(RequestRepastArticleInStock Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditInStock(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取物品入库详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetInStockDetail")]
        public ObjectResultEx GetInStockDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.GetInStockDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 物品仓库-出库
        /// <summary>
        /// 物品出库分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetOutStockPage")]
        public ObjectResultEx GetOutStockPage(PageParamList<RequestRepastArticleOutStock> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetOutStockPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除物品出库
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveOutStock")]
        public ObjectResultEx RemoveOutStock(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveOutStock(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑物品出库
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditOutStock")]
        public ObjectResultEx EditOutStock(RequestRepastArticleOutStock Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditOutStock(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 库存报表
        /// <summary>
        /// 原料报表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetStorageReportPage")]
        public ObjectResultEx GetStorageReportPage(PageParamList<RequestRepastStockReport> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetStorageReportPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 物品报表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetStockReportPage")]
        public ObjectResultEx GetStockReportPage(PageParamList<RequestRepastStockReport> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetStockReportPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 名称管理
        /// <summary>
        /// 名称分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetNamesPage")]
        public ObjectResultEx GetNamesPage(PageParamList<RequestRepastTypeName> pageParam)
        {
            return ObjectResultEx.Instance(RepastWebService.GetNamesPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑名称
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditNames")]
        public ObjectResultEx EditNames(RequestRepastTypeName Param)
        {
            return ObjectResultEx.Instance(RepastWebService.EditNames(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除名称
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveNames")]
        public ObjectResultEx RemoveNames(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveNames(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 名称列表
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        [HttpPost("GetNamesList")]
        public ObjectResultEx GetNamesList(SimpleParam<int> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.GetNamesList(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #endregion

        #region 支付模块
        /// <summary>
        /// 版本续费和升级使用支付宝支付
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        [HttpPost("AliPay")]
        public ObjectResultEx AliPay(SimpleParam<int> Key, SimpleParam<int?> Value)
        {
            return ObjectResultEx.Instance(RepastWebService.AliPay(Key.Id, Value.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 版本续费和升级使用微信支付
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        [HttpPost("WxPay")]
        public ObjectResultEx WxPay(SimpleParam<int> Key, SimpleParam<int?> Value)
        {
            return ObjectResultEx.Instance(RepastWebService.WxPay(Key.Id, Value.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 导出Excel
        /// <summary>
        /// 食材入库Excel导出
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("ExportStuffInStockFile")]
        [AllowAnonymous]
        public ObjectResultEx ExportStuffInStockFile(SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.ExportStuffInStockFile(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 食材出库Excel导出
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("ExportStuffOutStockFile")]
        [AllowAnonymous]
        public ObjectResultEx ExportStuffOutStockFile(SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.ExportStuffOutStockFile(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 物品入库Excel导出
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("ExportGoodsInStockFile")]
        [AllowAnonymous]
        public ObjectResultEx ExportGoodsInStockFile(SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.ExportGoodsInStockFile(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 物品出库Excel导出
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("ExportGoodsOutStockFile")]
        [AllowAnonymous]
        public ObjectResultEx ExportGoodsOutStockFile(SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.ExportGoodsOutStockFile(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 数据统计
        /// <summary>
        /// 数据统计
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetDataCount")]
        public ObjectResultEx GetDataCount()
        {
            return ObjectResultEx.Instance(RepastWebService.GetDataCount(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}