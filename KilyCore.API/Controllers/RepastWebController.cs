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
        public ObjectResultEx RemoveDic(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveDic(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 字典详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetDicDetail")]
        public ObjectResultEx GetDicDetail(SimlpeParam<Guid> Param)
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
        public ObjectResultEx AuditContinuedAndLevel(SimlpeParam<Guid> Key, SimlpeParam<bool> Param)
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
        public ObjectResultEx RemoveSupplier(SimlpeParam<Guid> Param)
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
        public ObjectResultEx RemoveBuybill(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveBuybill(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 进货台账详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetBuybillDetail")]
        public ObjectResultEx GetBuybillDetail(SimlpeParam<Guid> Param)
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
        public ObjectResultEx RemoveSellbill(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveSellbill(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 销售台账详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetSellbillDetail")]
        public ObjectResultEx GetSellbillDetail(SimlpeParam<Guid> Param)
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
        public ObjectResultEx GetDishDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.GetDishDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除菜品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveDish")]
        public ObjectResultEx RemoveDish(SimlpeParam<Guid> Param)
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
        public ObjectResultEx RemoveInStorage(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveInStorage(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 原料入库详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetInStorageDetail")]
        public ObjectResultEx GetInStorageDetail(SimlpeParam<Guid> Param)
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
        public ObjectResultEx RemoveOutStorage(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastWebService.RemoveOutStorage(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        public ObjectResultEx AliPay(SimlpeParam<int> Key, SimlpeParam<int?> Value)
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
        public ObjectResultEx WxPay(SimlpeParam<int> Key, SimlpeParam<int?> Value)
        {
            return ObjectResultEx.Instance(RepastWebService.WxPay(Key.Id, Value.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}