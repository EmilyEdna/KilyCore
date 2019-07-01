﻿using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Function;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.RequestMapper.Govt;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.Function;
using KilyCore.DataEntity.ResponseMapper.Govt;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.Model.Enterprise;
using KilyCore.EntityFrameWork.Model.Govt;
using KilyCore.EntityFrameWork.Model.System;
using KilyCore.EntityFrameWork.ModelEnum;
using KilyCore.Extension.AttributeExtension;
using KilyCore.Extension.AutoMapperExtension;
using KilyCore.Extension.PayCore.AliPay;
using KilyCore.Extension.PayCore.WxPay;
using KilyCore.Repositories.BaseRepository;
using KilyCore.Service.ConstMessage;
using KilyCore.Service.IServiceCore;
using KilyCore.Service.QueryExtend;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using KilyCore.Extension.UtilExtension;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
namespace KilyCore.Service.ServiceCore
{
    public class EnterpriseWebService : Repository, IEnterpriseWebService
    {
        #region 下拉关联列表
        /// <summary>
        /// 返回子公司Id列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IQueryable<Guid> GetChildIdList(Guid Id)
        {
            return Kily.Set<EnterpriseInfo>().Where(t => t.CompanyId == Id).Select(t => t.Id).AsQueryable();
        }
        /// <summary>
        /// 厂商列表
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public IList<ResponseEnterpriseSeller> GetSellerList(int Type)
        {
            IQueryable<EnterpriseSeller> queryable = Kily.Set<EnterpriseSeller>().Where(t => t.IsDelete == false).AsNoTracking();
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            if ((SellerEnum)Type == SellerEnum.Supplier)
                queryable = queryable.Where(t => t.SellerType == SellerEnum.Supplier);
            if ((SellerEnum)Type == SellerEnum.Sale)
                queryable = queryable.Where(t => t.SellerType == SellerEnum.Sale);
            if ((SellerEnum)Type == SellerEnum.Production)
                queryable = queryable.Where(t => t.SellerType == SellerEnum.Production);
            var data = queryable.Select(t => new ResponseEnterpriseSeller()
            {
                Id = t.Id,
                SupplierName = t.SupplierName
            }).ToList();
            return data;
        }
        /// <summary>
        /// 下拉字典类型
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseDictionary> GetDictionaryList(string Param)
        {
            IQueryable<EnterpriseDictionary> queryable = Kily.Set<EnterpriseDictionary>().Where(t => t.IsDelete == false);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Where(t => t.DicType == Param).GroupBy(t => t.DicType)
                .Select(t => new ResponseEnterpriseDictionary()
                {
                    DicType = t.Key.ToString(),
                }).AsNoTracking().ToList();
            data.ForEach(t =>
            {
                t.DictionaryList = queryable
                .Where(x => x.DicType == t.DicType)
                .Select(x => new ResponseEnterpriseDictionary()
                {
                    Id = x.Id,
                    DicName = x.DicName,
                    DicValue = x.DicValue,
                    Remark = x.Remark
                }).AsNoTracking().ToList();
            });
            return data;

        }
        /// <summary>
        /// 获取入住企业的经销商
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public IList<ResponseEnterpriseSeller> GetSellerInEnterprise(string Param)
        {
            IQueryable<EnterpriseSeller> Seller = Kily.Set<EnterpriseSeller>().Where(t => t.IsDelete == false).Where(t => t.SellerType == SellerEnum.Sale);
            if (!string.IsNullOrEmpty(Param))
                Seller = Seller.Where(t => t.SupplierName.Contains(Param));
            var data = Seller.Select(t => new ResponseEnterpriseSeller()
            {
                Id = t.Id,
                SupplierName = t.SupplierName,
                Address = t.Address,
                LinkPhone = t.LinkPhone
            }).ToList();
            return data;
        }
        #endregion

        #region 获取全局集团菜单
        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseMenu> GetEnterpriseMenu()
        {
            IQueryable<EnterpriseMenu> queryable = Kily.Set<EnterpriseMenu>().Where(t => t.Level == MenuEnum.LevelOne).Where(t => t.IsDelete == false).AsNoTracking().AsQueryable().OrderBy(t => t.CreateTime);
            if (CompanyInfo() != null)
            {
                EnterpriseRoleAuthor Author = null;
                EnterpriseRoleAuthorWeb AuthorWeb = null;
                String RolePath = String.Empty;
                if (CompanyInfo().CompanyId == null)
                {
                    Author = Kily.Set<EnterpriseRoleAuthor>().Where(t => t.IsDelete == false).Where(t => t.Id == CompanyInfo().EnterpriseRoleId).AsNoTracking().FirstOrDefault();
                    RolePath = Author.AuthorMenuPath;
                }
                else
                {
                    AuthorWeb = Kily.Set<EnterpriseRoleAuthorWeb>().Where(t => t.IsDelete == false).Where(t => t.Id == CompanyInfo().EnterpriseRoleId).AsNoTracking().FirstOrDefault();
                    RolePath = AuthorWeb.AuthorMenuPath;
                }
                queryable = queryable.Where(t => RolePath.Contains(t.Id.ToString())).AsNoTracking();
                var data = queryable.OrderBy(t => t.CreateTime).Select(t => new ResponseEnterpriseMenu()
                {
                    Id = t.Id,
                    MenuId = t.MenuId,
                    ParentId = t.ParentId,
                    MenuAddress = t.MenuAddress,
                    MenuName = t.MenuName,
                    HasChildrenNode = t.HasChildrenNode,
                    MenuIcon = t.MenuIcon,
                    MenuChildren = Kily.Set<EnterpriseMenu>()
                    .Where(x => x.ParentId == t.MenuId)
                    .Where(x => x.Level != MenuEnum.LevelOne)
                    .Where(x => x.IsDelete == false)
                    .Where(x => RolePath.Contains(x.Id.ToString()))
                    .OrderBy(x => x.CreateTime).Select(x => new ResponseEnterpriseMenu()
                    {
                        Id = x.Id,
                        MenuId = x.MenuId,
                        ParentId = x.ParentId,
                        MenuAddress = x.MenuAddress,
                        MenuName = x.MenuName,
                        HasChildrenNode = x.HasChildrenNode,
                        MenuIcon = x.MenuIcon
                    }).ToList()
                }).ToList();
                return data;
            }
            else
            {
                EnterpriseRoleAuthorWeb AuthorWeb = Kily.Set<EnterpriseRoleAuthorWeb>().Where(t => t.IsDelete == false).
                    Where(t => t.Id == CompanyUser().RoleAuthorType).AsNoTracking().FirstOrDefault();
                queryable = queryable.Where(t => AuthorWeb.AuthorMenuPath.Contains(t.Id.ToString())).AsNoTracking();
                var data = queryable.OrderBy(t => t.CreateTime).Select(t => new ResponseEnterpriseMenu()
                {
                    Id = t.Id,
                    MenuId = t.MenuId,
                    ParentId = t.ParentId,
                    MenuAddress = t.MenuAddress,
                    MenuName = t.MenuName,
                    HasChildrenNode = t.HasChildrenNode,
                    MenuIcon = t.MenuIcon,
                    MenuChildren = Kily.Set<EnterpriseMenu>()
                  .Where(x => x.ParentId == t.MenuId)
                  .Where(x => x.Level != MenuEnum.LevelOne)
                  .Where(x => x.IsDelete == false)
                  .Where(x => AuthorWeb.AuthorMenuPath.Contains(x.Id.ToString()))
                  .OrderBy(x => x.CreateTime).Select(x => new ResponseEnterpriseMenu()
                  {
                      Id = x.Id,
                      MenuId = x.MenuId,
                      ParentId = x.ParentId,
                      MenuAddress = x.MenuAddress,
                      MenuName = x.MenuName,
                      HasChildrenNode = x.HasChildrenNode,
                      MenuIcon = x.MenuIcon
                  }).ToList()
                }).ToList();
                return data;
            }
        }
        /// <summary>
        /// 获取权限菜单树
        /// </summary>
        /// <returns></returns>
        public IList<ResponseParentTree> GetEnterpriseWebTree(String Key)
        {
            IQueryable<EnterpriseMenu> query = Kily.Set<EnterpriseMenu>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(Key))
                query = query.Where(t => Key.Contains(t.Id.ToString()));
            if (CompanyInfo() != null)
            {
                if (CompanyInfo().CompanyId == null)
                {
                    IQueryable<EnterpriseRoleAuthor> queryables = Kily.Set<EnterpriseRoleAuthor>().Where(t => t.IsDelete == false);
                    queryables = queryables.Where(t => t.Id == CompanyInfo().EnterpriseRoleId).AsNoTracking();
                    EnterpriseRoleAuthor Author = queryables.FirstOrDefault();
                    IQueryable<ResponseParentTree> queryable = Kily.Set<EnterpriseMenu>().Where(t => t.IsDelete == false)
                         .Where(t => t.Level == MenuEnum.LevelOne)
                         .Where(t => Author.AuthorMenuPath.Contains(t.Id.ToString()))
                         .AsNoTracking().Select(t => new ResponseParentTree()
                         {
                             Id = t.Id,
                             Text = t.MenuName,
                             Color = "black",
                             BackClolor = "white",
                             State = string.IsNullOrEmpty(Key) ? null : (query.Where(x => x.Id == t.Id).AsNoTracking().FirstOrDefault() != null ? new States { Checked = true } : null),
                             SelectedIcon = "fa fa-refresh fa-spin",
                             Nodes = Kily.Set<EnterpriseMenu>().Where(x => t.IsDelete == false)
                             .Where(x => x.Level != MenuEnum.LevelOne)
                             .Where(x => x.ParentId == t.MenuId)
                             .Where(x => Author.AuthorMenuPath.Contains(x.Id.ToString()))
                             .AsNoTracking()
                             .Select(x => new ResponseChildTree()
                             {
                                 Id = x.Id,
                                 Text = x.MenuName,
                                 Color = "black",
                                 State = string.IsNullOrEmpty(Key) ? null : (query.Where(p => p.Id == x.Id).AsNoTracking().FirstOrDefault() != null ? new States { Checked = true } : null),
                                 BackClolor = "white",
                                 SelectedIcon = "fa fa-refresh fa-spin",
                             }).AsQueryable()
                         }).AsQueryable();
                    var data = queryable.ToList();
                    return data;
                }
                else
                {
                    IQueryable<EnterpriseRoleAuthorWeb> queryables = Kily.Set<EnterpriseRoleAuthorWeb>().Where(t => t.IsDelete == false);
                    queryables = queryables.Where(t => t.Id == CompanyInfo().EnterpriseRoleId).AsNoTracking();
                    EnterpriseRoleAuthorWeb Author = queryables.FirstOrDefault();
                    IQueryable<ResponseParentTree> queryable = Kily.Set<EnterpriseMenu>().Where(t => t.IsDelete == false)
                        .Where(t => t.Level == MenuEnum.LevelOne)
                         .Where(t => Author.AuthorMenuPath.Contains(t.Id.ToString()))
                         .AsNoTracking().Select(t => new ResponseParentTree()
                         {
                             Id = t.Id,
                             Text = t.MenuName,
                             Color = "black",
                             BackClolor = "white",
                             State = string.IsNullOrEmpty(Key) ? null : (query.Where(x => x.Id == t.Id).AsNoTracking().FirstOrDefault() != null ? new States { Checked = true } : null),
                             SelectedIcon = "fa fa-refresh fa-spin",
                             Nodes = Kily.Set<EnterpriseMenu>().Where(x => t.IsDelete == false)
                             .Where(x => x.Level != MenuEnum.LevelOne)
                             .Where(x => x.ParentId == t.MenuId)
                             .Where(x => Author.AuthorMenuPath.Contains(x.Id.ToString()))
                             .AsNoTracking()
                             .Select(x => new ResponseChildTree()
                             {
                                 Id = x.Id,
                                 Text = x.MenuName,
                                 Color = "black",
                                 State = string.IsNullOrEmpty(Key) ? null : (query.Where(p => p.Id == x.Id).AsNoTracking().FirstOrDefault() != null ? new States { Checked = true } : null),
                                 BackClolor = "white",
                                 SelectedIcon = "fa fa-refresh fa-spin",
                             }).AsQueryable()
                         }).AsQueryable();
                    var data = queryable.ToList();
                    return data;
                }
            }
            else
            {
                IQueryable<EnterpriseRoleAuthorWeb> queryables = Kily.Set<EnterpriseRoleAuthorWeb>().Where(t => t.IsDelete == false);
                queryables = queryables.Where(t => t.Id == CompanyUser().RoleAuthorType).AsNoTracking();
                EnterpriseRoleAuthorWeb Author = queryables.FirstOrDefault();
                IQueryable<ResponseParentTree> queryable = Kily.Set<EnterpriseMenu>().Where(t => t.IsDelete == false)
                     .Where(t => t.Level == MenuEnum.LevelOne)
                     .Where(t => Author.AuthorMenuPath.Contains(t.Id.ToString()))
                     .AsNoTracking().Select(t => new ResponseParentTree()
                     {
                         Id = t.Id,
                         Text = t.MenuName,
                         Color = "black",
                         State = string.IsNullOrEmpty(Key) ? null : (query.Where(x => x.Id == t.Id).AsNoTracking().FirstOrDefault() != null ? new States { Checked = true } : null),
                         BackClolor = "white",
                         SelectedIcon = "fa fa-refresh fa-spin",
                         Nodes = Kily.Set<EnterpriseMenu>().Where(x => t.IsDelete == false)
                         .Where(x => x.Level != MenuEnum.LevelOne)
                         .Where(x => x.ParentId == t.MenuId)
                         .Where(x => Author.AuthorMenuPath.Contains(x.Id.ToString()))
                         .AsNoTracking()
                         .Select(x => new ResponseChildTree()
                         {
                             Id = x.Id,
                             Text = x.MenuName,
                             Color = "black",
                             State = string.IsNullOrEmpty(Key) ? null : (query.Where(p => p.Id == x.Id).AsNoTracking().FirstOrDefault() != null ? new States { Checked = true } : null),
                             BackClolor = "white",
                             SelectedIcon = "fa fa-refresh fa-spin",
                         }).AsQueryable()
                     }).AsQueryable();
                var data = queryable.ToList();
                return data;
            }
        }
        #endregion

        #region 编辑不需要权限的
        #region 企业资料
        /// <summary>
        /// 更新企业资料
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveEnterprise(RequestEnterprise Param)
        {
            Param.AuditType = AuditEnum.WaitAduit;
            EnterpriseInfo data = Kily.Set<EnterpriseInfo>().Where(t => t.Id == Param.Id).FirstOrDefault();
            Param.EnterpriseRoleId = data.EnterpriseRoleId;
            Param.CompanyId = data.CompanyId;
            EnterpriseInfo Info = Param.MapToEntity<EnterpriseInfo>();
            if (Update<EnterpriseInfo, RequestEnterprise>(Info, Param))
                return ServiceMessage.UPDATESUCCESS;
            else
                return ServiceMessage.UPDATEFAIL;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveCompanyAccount(RequestEnterprise Param)
        {
            EnterpriseInfo info = Kily.Set<EnterpriseInfo>().Where(t => t.Id == Param.Id).FirstOrDefault();
            info.PassWord = Param.PassWord;
            info.CompanyAccount = Param.CompanyAccount;
            if (NormalUtil.CheckStringChineseUn(info.CompanyAccount))
                return "账号不能包含中文和特殊字符";
            IList<String> Fields = new List<String> { "CompanyAccount", "PassWord" };
            return UpdateField(info, null, Fields) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }
        /// <summary>
        /// 修改区域
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveCompanyArea(RequestEnterprise Param)
        {
            EnterpriseInfo info = Kily.Set<EnterpriseInfo>().Where(t => t.Id == Param.Id).FirstOrDefault();
            info.TypePath = Param.TypePath;
            return UpdateField(info, "TypePath") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }
        #endregion

        #region 人员管理
        /// <summary>
        /// 编辑子账户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveUser(RequestEnterpriseUser Param)
        {
            EnterpriseUser User = Param.MapToObj<RequestEnterpriseUser, EnterpriseUser>();
            if (NormalUtil.CheckStringChineseUn(User.Account))
                return "账号不能包含中文和特殊字符";
            var Users = Kily.Set<EnterpriseUser>().Where(t => t.Account.Equals(Param.Account)).AsNoTracking().FirstOrDefault();
            if (Users != null) return "该账号已经存在!";
            if (CompanyInfo() != null)
                User.TypePath = CompanyInfo().TypePath;
            else
                User.TypePath = CompanyUser().TypePath;
            if (Param.Id != Guid.Empty)
            {
                if (Update<EnterpriseUser, RequestEnterpriseUser>(User, Param))
                    return ServiceMessage.UPDATESUCCESS;
                else
                    return ServiceMessage.UPDATEFAIL;
            }
            else
            {
                if (Insert<EnterpriseUser>(User))
                    return ServiceMessage.INSERTSUCCESS;
                else
                    return ServiceMessage.INSERTFAIL;
            }
        }
        #endregion

        #region 权限角色
        /// <summary>
        /// 新增账户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveRoleAuthor(RequestRoleAuthorWeb Param)
        {
            EnterpriseRoleAuthorWeb Author = Param.MapToEntity<EnterpriseRoleAuthorWeb>();
            if (Param.Id == Guid.Empty)
                return Insert<EnterpriseRoleAuthorWeb>(Author) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return Update<EnterpriseRoleAuthorWeb, RequestRoleAuthorWeb>(Author, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }
        #endregion

        #region 集团账户
        /// <summary>
        /// 编辑子账户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveChildInfo(RequestEnterprise Param)
        {
            if (CompanyInfo() != null)
            {
                if (CompanyInfo().CompanyId != null)
                    return "无权限创建，仅限总公司使用!";
                EnterpriseInfo info = Param.MapToEntity<EnterpriseInfo>();
                var infos = Kily.Set<EnterpriseInfo>().Where(t => t.CompanyAccount.Equals(Param.CompanyAccount)).AsNoTracking().FirstOrDefault();
                if (infos != null) return "改账号已经存在!";
                info.AuditType = AuditEnum.WaitAduit;
                info.CompanyType = CompanyEnum.Other;
                info.NatureAgent = 1;
                info.TagCodeNum = 0;
                return Insert(info) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
            return "无权限创建!";
        }
        #endregion

        #region 企业字典
        /// <summary>
        /// 编辑字典
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveDic(RequestEnterpriseDictionary Param)
        {
            EnterpriseDictionary dictionary = Param.MapToEntity<EnterpriseDictionary>();
            if (Param.Id != Guid.Empty)
            {
                return Update<EnterpriseDictionary, RequestEnterpriseDictionary>(dictionary, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            }
            else
            {
                return Insert<EnterpriseDictionary>(dictionary) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            }
        }
        #endregion

        #region 升级续费
        /// <summary>
        /// 编辑续费
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveContinued(RequestEnterpriseContinued Param)
        {
            EnterpriseContinued continued = Param.MapToEntity<EnterpriseContinued>();
            continued.AuditType = AuditEnum.WaitAduit;
            return Insert<EnterpriseContinued>(continued) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 编辑升级
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveUpLevel(RequestEnterpriseUpLevel Param)
        {
            EnterpriseUpLevel level = Param.MapToEntity<EnterpriseUpLevel>();
            level.AuditType = AuditEnum.WaitAduit;
            return Insert<EnterpriseUpLevel>(level) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        #endregion

        #region  内部文件
        /// <summary>
        /// 编辑文件
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveFile(RequestEnterpriseInsideFile Param)
        {
            EnterpriseInsideFile insideFile = Param.MapToEntity<EnterpriseInsideFile>();
            if (Param.Id == Guid.Empty)
                return Insert<EnterpriseInsideFile>(insideFile) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return ServiceMessage.HANDLESUCCESS;
        }
        #endregion

        #region 设备管理
        /// <summary>
        /// 编辑设备
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveDevice(RequestEnterpriseDevice Param)
        {
            EnterpriseDevice device = Param.MapToEntity<EnterpriseDevice>();
            return Insert<EnterpriseDevice>(device) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 编辑设备清洗
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveDeviceClean(RequestEnterpriseDeviceClean Param)
        {
            EnterpriseDeviceClean Clean = Param.MapToObj<RequestEnterpriseDeviceClean, EnterpriseDeviceClean>();
            return Insert<EnterpriseDeviceClean>(Clean) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 编辑设备维护
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveDeviceFix(RequestEnterpriseDeviceFix Param)
        {
            EnterpriseDeviceFix Fix = Param.MapToObj<RequestEnterpriseDeviceFix, EnterpriseDeviceFix>();
            return Insert<EnterpriseDeviceFix>(Fix) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        #endregion

        #region 设施管理
        /// <summary>
        /// 编辑设施
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveFac(RequestEnterpriseFacilities Param)
        {
            EnterpriseFacilities facilities = Param.MapToEntity<EnterpriseFacilities>();
            return Insert(facilities) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 编辑设施附加
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveFacAttach(RequestEnterpriseFacilitiesAttach Param)
        {
            EnterpriseFacilitiesAttach attach = Param.MapToEntity<EnterpriseFacilitiesAttach>();
            return Insert<EnterpriseFacilitiesAttach>(attach) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        #endregion

        #region 关键点控制
        /// <summary>
        /// 编辑指标
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveTarget(RequestEnterpriseTarget Param)
        {
            EnterpriseTarget target = Param.MapToEntity<EnterpriseTarget>();
            return Insert<EnterpriseTarget>(target) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        #endregion

        #region 产品召回
        /// <summary>
        /// 编辑召回
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveRecover(RequestEnterpriseRecover Param)
        {
            EnterpriseRecover recover = Param.MapToEntity<EnterpriseRecover>();
            if (recover.Id == Guid.Empty)
                return Insert(recover) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
            {
                EnterpriseRecover data = Kily.Set<EnterpriseRecover>().Where(t => t.Id == Param.Id).AsNoTracking().FirstOrDefault();
                if (!string.IsNullOrEmpty(data.RecoverNum) && !string.IsNullOrEmpty(data.HandleWays))
                    return "无权限更改";
                data.HandleTime = Param.HandleTime;
                data.HandleUser = Param.HandleUser;
                data.HandleWays = Param.HandleWays;
                data.RecoverNum = Param.RecoverNum;
                List<String> Fields = new List<String> { "HandleTime", "HandleUser", "HandleWays", "RecoverNum" };
                return UpdateField(recover, null, Fields) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            }
        }
        #endregion
        #endregion

        #region 删除不需要权限的
        #region 人员管理
        /// <summary>
        /// 删除子账号
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteUser(Guid Id)
        {
            if (Delete(ExpressionExtension.GetExpression<EnterpriseUser>("Id", Id, ExpressionEnum.Equals)))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }
        #endregion

        #region 权限角色
        /// <summary>
        /// 删除账户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteRole(Guid Id)
        {
            if (Remove<EnterpriseRoleAuthorWeb>(t => t.Id == Id))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }
        #endregion

        #region 企业字典
        /// <summary>
        /// 删除码表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteDic(Guid Id)
        {
            return Remove<EnterpriseDictionary>(ExpressionExtension.GetExpression<EnterpriseDictionary>("Id", Id, ExpressionEnum.Equals)) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        #endregion

        #region 内部文件
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteFile(Guid Id)
        {
            return Remove<EnterpriseInsideFile>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        #endregion

        #region 厂商管理
        /// <summary>
        /// 删除厂商
        /// </summary>
        /// <param name="Id"></param>
        public string DeleteSeller(Guid Id)
        {
            return Delete<EnterpriseSeller>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        #endregion

        #region 设备管理
        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteDevice(Guid Id)
        {
            return Delete<EnterpriseDevice>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 删除清洗记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteDeviceClean(Guid Id)
        {
            return Remove<EnterpriseDeviceClean>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 删除维护记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteDeviceFix(Guid Id)
        {
            return Remove<EnterpriseDeviceFix>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        #endregion

        #region 设施管理
        /// <summary>
        /// 删除设施
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteFac(Guid Id)
        {
            return Delete<EnterpriseFacilities>(t => t.Id == Id) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 删除设施附加
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteFacAttach(Guid Id)
        {
            return Remove<EnterpriseFacilitiesAttach>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        #endregion

        #region 关键点控制
        /// <summary>
        /// 删除指标
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteTarget(Guid Id)
        {
            return Delete<EnterpriseTarget>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        #endregion

        #region 物码管理
        /// <summary>
        /// 删除绑定信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteBindTagInfo(Guid Id)
        {
            EnterpriseTagAttach TagAttach = Kily.Set<EnterpriseTagAttach>().Where(t => t.Id == Id).AsNoTracking().FirstOrDefault();
            EnterpriseTag Tag = Kily.Set<EnterpriseTag>().Where(t => t.Id == TagAttach.TagId).AsNoTracking().FirstOrDefault();
            Tag.UseNum = 0;
            Tag.TotalNo += TagAttach.UseNum;
            IList<String> Fields = new List<String> { "UseNum", "TotalNo" };
            UpdateField(Tag, null, Fields);
            return Remove<EnterpriseTagAttach>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        #endregion

        #region 企业自查
        /// <summary>
        /// 删除企业自查记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteTemplate(Guid Id)
        {
            return Remove<GovtTemplateChild>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        #endregion
        #endregion

        #region 基础管理
        #region 企业资料
        /// <summary>
        /// 企业资料分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterprise> GetInfoPage(PageParamList<RequestEnterprise> pageParam)
        {
            var data = Kily.Set<EnterpriseInfo>().Where(t => t.Id == pageParam.QueryParam.Id).Select(t => new ResponseEnterprise()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                CompanyName = t.CompanyName,
                CompanyAccount = t.CompanyAccount,
                CommunityCode = t.CommunityCode,
                Certification = t.Certification,
                CompanyAddress = t.CompanyAddress,
                TypePath = t.TypePath,
                NatureAgent = t.NatureAgent,
                TagCodeNum = t.TagCodeNum,
                CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType)
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 获取企业资料
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseEnterprise GetEnterpriseInfo(Guid Id)
        {
            var query = Kily.Set<SystemStayContract>().Where(t => t.EnterpriseOrMerchant == 1).AsNoTracking();
            var data = Kily.Set<EnterpriseInfo>().Where(t => t.Id == Id)
                .GroupJoin(query, t => (t.CompanyId != null ? t.CompanyId : t.Id), x => x.CompanyId, (t, x) => new ResponseEnterprise()
                {
                    Id = t.Id,
                    CompanyAccount = t.CompanyAccount,
                    CommunityCode = t.CommunityCode,
                    CompanyAddress = t.CompanyAddress,
                    CompanyName = t.CompanyName,
                    CompanyPhone = t.CompanyPhone,
                    CompanyType = t.CompanyType,
                    CardExpiredDate = t.CardExpiredDate,
                    SafeOffer = t.SafeOffer,
                    OfferLv = t.OfferLv,
                    CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType),
                    AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(x.FirstOrDefault() != null ? x.FirstOrDefault().AuditType : 0),
                    VersionName = AttrExtension.GetSingleDescription<SystemVersionEnum, DescriptionAttribute>(t.Version),
                    Version = t.Version,
                    VideoAddress = t.VideoAddress,
                    LngAndLat = t.LngAndLat,
                    PassWord = t.PassWord,
                    TypePath = t.TypePath,
                    Certification = t.Certification,
                    Honor = t.HonorCertification,
                    CodeStar = t.CodeStar,
                    Discription = t.Discription,
                    NetAddress = t.NetAddress,
                    ProductionAddress = t.ProductionAddress,
                    SellerAddress = t.SellerAddress,
                    IdCard = t.IdCard,
                    SafeNo = t.SafeNo,
                    Scope = t.Scope,
                    NatureAgent = t.NatureAgent,
                    TagCodeNum = t.TagCodeNum,
                    SafeCompany = t.SafeCompany,
                    ComImage = t.ComImage,
                    MainPro = t.MainPro,
                    ComplainPhone = t.ComplainPhone,
                    MainProRemark = t.MainProRemark
                }).FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 获取子公司
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IDictionary<Guid, String> GetChildAccount(Guid Id)
        {
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false).AsNoTracking();
            var Maps = queryable.Where(t => t.CompanyId == Id).ToDictionary(t => t.Id, t => t.CompanyName);
            var Map = queryable.Where(t => t.Id == Id).ToDictionary(t => t.Id, t => t.CompanyName);
            Maps.Add(Map.Keys.FirstOrDefault(), Map.Values.FirstOrDefault());
            return Maps;
        }
        #endregion

        #region 保存合同
        /// <summary>
        /// 保存合同和缴费凭证
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public ResponseStayContract SaveContract(RequestStayContract Param)
        {
            Param.AuditType = AuditEnum.WaitAduit;
            RequestAliPayModel AliPayModel = new RequestAliPayModel
            {
                OrderTitle = CompanyInfo().CompanyName + "合同缴费"
            };
            SystemStayContract contract = Param.MapToEntity<SystemStayContract>();
            contract.EnterpriseOrMerchant = 1;
            EnterpriseInfo info = Kily.Set<EnterpriseInfo>().Where(t => t.Id == contract.CompanyId).FirstOrDefault();
            var Demo = Kily.Set<SystemStayContract>().Where(t => t.CompanyId == info.Id).Where(t => t.EnterpriseOrMerchant == 1).FirstOrDefault();
            if (Demo != null)
                return new ResponseStayContract()
                {
                    Id = contract.Id,
                    VersionType = Param.VersionType,
                    TagNum = info.TagCodeNum,
                    PayInfoMsg = "请勿重复提交合同"
                };
            if (Param.VersionType == SystemVersionEnum.Test)
            {
                info.TagCodeNum = ServiceMessage.TEST;
                if (info.CompanyType == CompanyEnum.Plant || info.CompanyType == CompanyEnum.Culture)
                    AliPayModel.Money = ConfigMoney.PlantAndCultureTest * Convert.ToInt32(Param.ContractYear);
                if (info.CompanyType == CompanyEnum.Production)
                    AliPayModel.Money = ConfigMoney.ProductionTest * Convert.ToInt32(Param.ContractYear);
                if (info.CompanyType == CompanyEnum.Circulation)
                    AliPayModel.Money = ConfigMoney.CirculationTest * Convert.ToInt32(Param.ContractYear);
            }
            if (Param.VersionType == SystemVersionEnum.Base)
            {
                info.TagCodeNum = ServiceMessage.BASE;
                if (info.CompanyType == CompanyEnum.Plant || info.CompanyType == CompanyEnum.Culture)
                    AliPayModel.Money = ConfigMoney.PlantAndCultureBase * Convert.ToInt32(Param.ContractYear);
                if (info.CompanyType == CompanyEnum.Production)
                    AliPayModel.Money = ConfigMoney.ProductionBase * Convert.ToInt32(Param.ContractYear);
                if (info.CompanyType == CompanyEnum.Circulation)
                    AliPayModel.Money = ConfigMoney.CirculationBase * Convert.ToInt32(Param.ContractYear);
            }
            if (Param.VersionType == SystemVersionEnum.Level)
            {
                info.TagCodeNum = ServiceMessage.LEVEL;
                if (info.CompanyType == CompanyEnum.Plant || info.CompanyType == CompanyEnum.Culture)
                    AliPayModel.Money = ConfigMoney.PlantAndCultureLv * Convert.ToInt32(Param.ContractYear);
                if (info.CompanyType == CompanyEnum.Production)
                    AliPayModel.Money = ConfigMoney.ProductionLv * Convert.ToInt32(Param.ContractYear);
                if (info.CompanyType == CompanyEnum.Circulation)
                    AliPayModel.Money = ConfigMoney.CirculationLv * Convert.ToInt32(Param.ContractYear);
            }
            if (Param.VersionType == SystemVersionEnum.Enterprise)
            {
                info.TagCodeNum = ServiceMessage.ENTERPRISE;
                if (info.CompanyType == CompanyEnum.Plant || info.CompanyType == CompanyEnum.Culture)
                    AliPayModel.Money = ConfigMoney.PlantAndCultureEnterprise * Convert.ToInt32(Param.ContractYear);
                if (info.CompanyType == CompanyEnum.Production)
                    AliPayModel.Money = ConfigMoney.ProductionEnterprise * Convert.ToInt32(Param.ContractYear);
                if (info.CompanyType == CompanyEnum.Circulation)
                    AliPayModel.Money = ConfigMoney.CirculationEnterprise * Convert.ToInt32(Param.ContractYear);
            }
            contract.Id = Guid.NewGuid();
            if (contract.ContractType == 1)
            {
                contract.IsPay = false;
                contract.TryOut = "/";
                contract.EndTime = DateTime.Now.AddYears(Convert.ToInt32(contract.ContractYear));
                //银联
                if (contract.PayType == PayEnum.Unionpay || contract.PayType == PayEnum.AgentPay)
                {
                    contract.TotalPrice = (decimal)AliPayModel.Money;
                    return new ResponseStayContract()
                    {
                        Id = contract.Id,
                        VersionType = Param.VersionType,
                        TagNum = info.TagCodeNum,
                        PayInfoMsg = Insert<SystemStayContract>(contract) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL
                    };
                }
                //支付宝支付
                else if (contract.PayType == PayEnum.Alipay)
                {
                    contract.TotalPrice = (decimal)AliPayModel.Money;
                    SystemStayContract CompanyContract = Kily.Set<SystemStayContract>().Where(t => t.CompanyId == contract.CompanyId)
                      .Where(t => t.PayType == PayEnum.Alipay)
                      .Where(t => t.EnterpriseOrMerchant == 1)
                      .AsNoTracking().FirstOrDefault();
                    if (CompanyContract == null)
                    {
                        Insert(contract, false);
                        Insert(new SystemPayInfo()
                        {
                            MerchantId = contract.CompanyId,
                            GoodsId = contract.Id,
                            PayType = contract.PayType,
                            TradeNo = AliPayCore.Instance.GetTradeNo(),
                            Version = Param.VersionType,
                            TagNum = info.TagCodeNum
                        });
                        return new ResponseStayContract()
                        {
                            Id = contract.Id,
                            VersionType = Param.VersionType,
                            TagNum = info.TagCodeNum,
                            PayInfoMsg = AliPayCore.Instance.WebPay(AliPayModel)
                        };
                    }
                    else
                    {
                        SystemPayInfo PayInfo = Kily.Set<SystemPayInfo>().Where(t => t.GoodsId == CompanyContract.Id)
                            .Where(t => t.PayType == PayEnum.Alipay)
                            .Where(t => t.MerchantId == CompanyContract.CompanyId).AsNoTracking().FirstOrDefault();
                        PayInfo.TradeNo = AliPayCore.Instance.GetTradeNo();
                        UpdateField(PayInfo, "TradeNo");
                        return new ResponseStayContract()
                        {
                            Id = contract.Id,
                            VersionType = Param.VersionType,
                            TagNum = info.TagCodeNum,
                            PayInfoMsg = AliPayCore.Instance.WebPay(AliPayModel)
                        };
                    }
                }
                //微信支付
                else
                {
                    RequestWxPayModel WxPayModel = AliPayModel.MapToEntity<RequestWxPayModel>();
                    contract.TotalPrice = WxPayModel.Money;
                    SystemStayContract CompanyContract = Kily.Set<SystemStayContract>().Where(t => t.CompanyId == contract.CompanyId)
                        .Where(t => t.PayType == PayEnum.WxPay)
                        .Where(t => t.EnterpriseOrMerchant == 1)
                        .AsNoTracking().FirstOrDefault();
                    if (CompanyContract == null)
                    {
                        Insert(contract, false);
                        Insert(new SystemPayInfo()
                        {
                            MerchantId = contract.CompanyId,
                            GoodsId = contract.Id,
                            PayType = contract.PayType,
                            TradeNo = WxPayCore.Instance.GetTradeNo(),

                        });
                        return new ResponseStayContract()
                        {
                            Id = contract.Id,
                            VersionType = Param.VersionType,
                            TagNum = info.TagCodeNum,
                            PayInfoMsg = WxPayCore.Instance.WebPay(WxPayModel)
                        };
                    }
                    else
                    {
                        SystemPayInfo PayInfo = Kily.Set<SystemPayInfo>().Where(t => t.GoodsId == CompanyContract.Id)
                            .Where(t => t.PayType == PayEnum.WxPay)
                            .Where(t => t.MerchantId == CompanyContract.CompanyId).AsNoTracking().FirstOrDefault();
                        PayInfo.TradeNo = WxPayCore.Instance.GetTradeNo();
                        UpdateField(PayInfo, "TradeNo");
                        return new ResponseStayContract()
                        {
                            Id = CompanyContract.Id,
                            VersionType = Param.VersionType,
                            TagNum = info.TagCodeNum,
                            PayInfoMsg = WxPayCore.Instance.WebPay(WxPayModel)
                        };
                    }
                }
            }
            else
            {
                contract.PayType = PayEnum.AgentPay;
                contract.IsPay = false;
                contract.TryOut = "30";
                contract.TryStarDate = DateTime.Now;
                contract.TryEndDate = contract.TryStarDate.Value.AddDays(30);
                contract.TotalPrice = (decimal)AliPayModel.Money;
                contract.EndTime = DateTime.Now.AddYears(Convert.ToInt32(contract.ContractYear));
                return new ResponseStayContract()
                {
                    Id = contract.Id,
                    VersionType = Param.VersionType,
                    TagNum = info.TagCodeNum,
                    PayInfoMsg = Insert<SystemStayContract>(contract) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL
                };
            }
        }
        #endregion

        #region 人员管理
        /// <summary>
        /// 获取人员管理分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseUser> GetUserPage(PageParamList<RequestEnterpriseUser> pageParam)
        {
            IQueryable<EnterpriseUser> queryable = Kily.Set<EnterpriseUser>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.TrueName))
                queryable = queryable.Where(t => t.TrueName.Contains(pageParam.QueryParam.TrueName));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseEnterpriseUser()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                TrueName = t.TrueName,
                Phone = t.Phone,
                Account = t.Account,
                IdCard = t.IdCard
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 人员列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseUser> GetUserList()
        {
            IQueryable<EnterpriseUser> queryable = Kily.Set<EnterpriseUser>().Where(t => t.IsDelete == false);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.Id == CompanyUser().CompanyId);
            var data = queryable.Select(t => new ResponseEnterpriseUser()
            {
                TrueName = t.TrueName,
            }).ToList();
            return data;
        }
        /// <summary>
        /// 获取子账户详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseEnterpriseUser GetUserDetail(Guid Id)
        {
            IQueryable<EnterpriseUser> queryable = Kily.Set<EnterpriseUser>().Where(t => t.Id == Id);
            var data = queryable.Select(t => new ResponseEnterpriseUser()
            {
                Id = t.Id,
                TrueName = t.TrueName,
                CompanyId = t.CompanyId,
                CompanyType = t.CompanyType,
                CompanyName = t.CompanyName,
                Version = t.Version,
                Account = t.Account,
                Phone = t.Phone,
                RoleAuthorType = t.RoleAuthorType,
                IdCard = t.IdCard,
                PassWord = t.PassWord
            }).AsNoTracking().FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 集团账户权限列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseRoleAuthorWeb> GetRoleAuthorList()
        {
            IQueryable<EnterpriseRoleAuthorWeb> queryable = Kily.Set<EnterpriseRoleAuthorWeb>().OrderByDescending(t => t.CreateTime);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CreateUser == CompanyInfo().Id.ToString() || GetChildIdList(CompanyInfo().Id).Contains(Guid.Parse(t.CreateUser)));
            else
                queryable = queryable.Where(t => t.CreateUser == CompanyUser().Id.ToString());
            var data = queryable.Select(t => new ResponseRoleAuthorWeb()
            {
                Id = t.Id,
                AuthorName = t.AuthorName
            }).ToList();
            return data;
        }
        #endregion

        #region 权限角色
        /// <summary>
        /// 账户分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRoleAuthorWeb> GetRoleAuthorPage(PageParamList<RequestRoleAuthorWeb> pageParam)
        {
            IQueryable<EnterpriseRoleAuthorWeb> queryable = Kily.Set<EnterpriseRoleAuthorWeb>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.AuthorName))
                queryable = queryable.Where(t => t.AuthorName.Contains(pageParam.QueryParam.AuthorName));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CreateUser == CompanyInfo().Id.ToString() || GetChildIdList(CompanyInfo().Id).Contains(Guid.Parse(t.CreateUser)));
            else
                queryable = queryable.Where(t => t.CreateUser == CompanyUser().Id.ToString());
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseRoleAuthorWeb()
            {
                Id = t.Id,
                AuthorName = t.AuthorName,
                AuthorMenuPath = t.AuthorMenuPath
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        #endregion

        #region 集团账户
        /// <summary>
        /// 集团账户列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterprise> GetChildInfo(PageParamList<RequestEnterprise> pageParam)
        {
            var data = Kily.Set<EnterpriseInfo>().Where(t => t.CompanyId == pageParam.QueryParam.Id)
                .OrderByDescending(t => t.CreateTime).Select(t => new ResponseEnterprise()
                {
                    Id = t.Id,
                    CompanyId = t.CompanyId,
                    CompanyName = t.CompanyName,
                    Certification = t.Certification,
                    CompanyAccount = t.CompanyAccount,
                    TagCodeNum = t.TagCodeNum,
                    CompanyAddress = t.CompanyAddress,
                }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        #endregion

        #region  企业认证
        /// <summary>
        /// 认证分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseIdent> GetIndentPage(PageParamList<RequestEnterpriseIdent> pageParam)
        {
            IQueryable<EnterpriseIdent> queryable = Kily.Set<EnterpriseIdent>().OrderByDescending(t => t.CreateTime);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseIdent()
            {
                IdentNo = t.IdentNo,
                IdentStarName = AttrExtension.GetSingleDescription<IdentEnum, DescriptionAttribute>(t.IdentStar),
                AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(t.AuditType),
                IdentYear = t.IdentYear,
                Representative = t.Representative,
                SendPerson = t.SendPerson
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 企业认证
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string SaveEnterpriseIdent(RequestEnterpriseIdent param)
        {
            param.Id = Guid.NewGuid();
            param.IdentId = param.Id;
            param.AuditType = AuditEnum.WaitAduit;
            param.IdentEndTime = param.IdentStartTime.AddYears(param.IdentYear);
            if (CompanyInfo() != null)
            {
                param.CompanyId = CompanyInfo().Id;
                param.CompanyName = CompanyInfo().CompanyName;
                param.CompanyType = CompanyInfo().CompanyType;
            }
            else
            {
                param.CompanyId = CompanyUser().Id;
                param.CompanyName = CompanyUser().CompanyName;
                param.CompanyType = CompanyUser().CompanyType;
            }
            EnterpriseIdent Ident = param.MapToEntity<EnterpriseIdent>();
            if (CompanyInfo().CompanyType == CompanyEnum.Plant)
            {
                EnterprisePlantIdentAttach Plant = param.MapToEntity<EnterprisePlantIdentAttach>();
                Insert<EnterprisePlantIdentAttach>(Plant);
            }
            if (CompanyInfo().CompanyType == CompanyEnum.Culture)
            {
                EnterpriseCultureIdentAttach Culture = param.MapToEntity<EnterpriseCultureIdentAttach>();
                Insert<EnterpriseCultureIdentAttach>(Culture);
            }
            if (CompanyInfo().CompanyType == CompanyEnum.Production)
            {
                EnterpriseProductionIdentAttach Production = param.MapToEntity<EnterpriseProductionIdentAttach>();
                Insert<EnterpriseProductionIdentAttach>(Production);
            }
            if (CompanyInfo().CompanyType == CompanyEnum.Circulation)
            {
                EnterpriseCirculationIdentAttach Circulation = param.MapToEntity<EnterpriseCirculationIdentAttach>();
                Insert<EnterpriseCirculationIdentAttach>(Circulation);
            }
            if (CompanyInfo().CompanyType == CompanyEnum.Other)
            {
                EnterpriseOtherIdentAttach Other = param.MapToEntity<EnterpriseOtherIdentAttach>();
                Insert<EnterpriseOtherIdentAttach>(Other);
            }
            if (Insert<EnterpriseIdent>(Ident, false))
                return ServiceMessage.INSERTSUCCESS;
            else
                return ServiceMessage.INSERTFAIL;
        }
        #endregion

        #region 企业字典
        /// <summary>
        /// 字典分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseDictionary> GetDicPage(PageParamList<RequestEnterpriseDictionary> pageParam)
        {
            IQueryable<EnterpriseDictionary> queryable = Kily.Set<EnterpriseDictionary>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.DicType))
                queryable = queryable.Where(t => t.DicType.Contains(pageParam.QueryParam.DicType));
            if (!string.IsNullOrEmpty(pageParam.QueryParam.DicName))
                queryable = queryable.Where(t => t.DicType.Contains(pageParam.QueryParam.DicName));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.AsNoTracking().Select(t => new ResponseEnterpriseDictionary()
            {
                Id = t.Id,
                DicType = t.DicType,
                DicName = t.DicName,
                DicValue = t.DicValue
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 字典详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseEnterpriseDictionary GetDicDetail(Guid Id)
        {
            var data = Kily.Set<EnterpriseDictionary>().Where(t => t.Id == Id).AsNoTracking().Select(t => new ResponseEnterpriseDictionary()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                DicType = t.DicType,
                DicName = t.DicName,
                DicValue = t.DicValue,
                Remark = t.Remark
            }).FirstOrDefault();
            return data;
        }
        #endregion

        #region 升级续费
        /// <summary>
        /// 查看版本信息
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseLevelUp> GetLvPage(PageParamList<RequestEnterpriseLevelUp> pageParam)
        {
            IQueryable<SystemStayContract> queryable = Kily.Set<SystemStayContract>().Where(t => t.CompanyId == pageParam.QueryParam.Id).Where(t => t.EnterpriseOrMerchant == 1).Where(t => t.IsDelete == false);
            var data = queryable.Select(t => new ResponseEnterpriseLevelUp()
            {
                Id = t.CompanyId,
                StarTime = t.CreateTime,
                EndTime = t.EndTime,
                Year = t.ContractYear,
                VersionName = AttrExtension.GetSingleDescription<SystemVersionEnum, DescriptionAttribute>(t.VersionType),
                VersionType = t.VersionType
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 续费记录
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseContinued> GetContinuedPage(PageParamList<RequestEnterpriseContinued> pageParam)
        {
            IQueryable<EnterpriseContinued> queryable = Kily.Set<EnterpriseContinued>().Where(t => t.CompanyId == pageParam.QueryParam.CompanyId);
            var data = queryable.Select(t => new ResponseEnterpriseContinued()
            {
                Id = t.Id,
                ContinuedYear = t.ContinuedYear,
                PayTypeName = AttrExtension.GetSingleDescription<PayEnum, DescriptionAttribute>(t.PayType),
                AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(t.AuditType),
                IsPay = t.IsPay,
                PayTicket = t.PayTicket
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 升级记录
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseUpLevel> GetUpLevelPage(PageParamList<RequestEnterpriseUpLevel> pageParam)
        {
            IQueryable<EnterpriseUpLevel> queryable = Kily.Set<EnterpriseUpLevel>().Where(t => t.CompanyId == pageParam.QueryParam.CompanyId);
            var data = queryable.Select(t => new ResponseEnterpriseUpLevel()
            {
                Id = t.Id,
                ContinuedYear = t.ContinuedYear,
                PayTypeName = AttrExtension.GetSingleDescription<PayEnum, DescriptionAttribute>(t.PayType),
                AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(t.AuditType),
                VersionTypeName = AttrExtension.GetSingleDescription<SystemVersionEnum, DescriptionAttribute>(t.VersionType),
                IsPay = t.IsPay,
                PayTicket = t.PayTicket
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 审核审计续费
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string AuditContinuedAndLevel(Guid Id, bool Param)
        {
            EnterpriseContinued continued = null;
            EnterpriseUpLevel level = null;
            if (Param)
                continued = Kily.Set<EnterpriseContinued>().Where(t => t.Id == Id).FirstOrDefault();
            else
                level = Kily.Set<EnterpriseUpLevel>().Where(t => t.Id == Id).FirstOrDefault();
            if (UserInfo().AccountType > AccountEnum.Country)
            {
                if (Param)
                {
                    continued.AuditType = AuditEnum.AuditSuccess;
                    return UpdateField(continued, "AuditType") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
                }
                else
                {
                    level.AuditType = AuditEnum.AuditSuccess;
                    return UpdateField(level, "AuditType") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
                }
            }
            else
            {
                if (Param)
                {
                    SystemStayContract contract = Kily.Set<SystemStayContract>().Where(t => t.EnterpriseOrMerchant == 1).Where(t => t.CompanyId == continued.CompanyId).FirstOrDefault();
                    contract.CreateTime = DateTime.Now;
                    contract.EndTime = DateTime.Now.AddYears(Convert.ToInt32(continued.ContinuedYear));
                    contract.ContractYear = continued.ContinuedYear;
                    contract.PayTicket = continued.PayTicket;
                    contract.PayType = continued.PayType;
                    IList<string> Fields = new List<string> { "CreateTime", "EndTime", "ContractYear", "PayTicket", "PayType" };
                    IList<string> Field = new List<string> { "IsPay", "AuditType" };
                    UpdateField(contract, null, Fields);
                    continued.AuditType = AuditEnum.FinanceSuccess;
                    continued.IsPay = true;
                    return UpdateField(continued, null, Field) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
                }
                else
                {
                    SystemStayContract contract = Kily.Set<SystemStayContract>().Where(t => t.EnterpriseOrMerchant == 1).Where(t => t.CompanyId == level.CompanyId).FirstOrDefault();
                    EnterpriseInfo info = Kily.Set<EnterpriseInfo>().Where(t => t.Id == level.CompanyId).FirstOrDefault();
                    contract.CreateTime = DateTime.Now;
                    contract.EndTime = DateTime.Now.AddYears(Convert.ToInt32(continued.ContinuedYear));
                    contract.ContractYear = level.ContinuedYear;
                    contract.PayTicket = level.PayTicket;
                    contract.PayType = level.PayType;
                    contract.VersionType = level.VersionType;
                    IList<string> Fields = new List<string> { "CreateTime", "EndTime", "ContractYear", "PayTicket", "PayType", "VersionType" };
                    IList<string> Field = new List<string> { "IsPay", "AuditType" };
                    UpdateField(contract, null, Fields);
                    if (level.VersionType == SystemVersionEnum.Test)
                        info.TagCodeNum += ServiceMessage.TEST;
                    if (level.VersionType == SystemVersionEnum.Base)
                        info.TagCodeNum += ServiceMessage.BASE;
                    if (level.VersionType == SystemVersionEnum.Level)
                        info.TagCodeNum += ServiceMessage.LEVEL;
                    if (level.VersionType == SystemVersionEnum.Enterprise)
                        info.TagCodeNum += ServiceMessage.ENTERPRISE;
                    UpdateField(info, "TagCodeNum");
                    level.AuditType = AuditEnum.FinanceSuccess;
                    level.IsPay = true;
                    return UpdateField(level, null, Field) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
                }
            }
        }
        #endregion

        #region 内部文件
        /// <summary>
        /// 文件分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseInsideFile> GetFilePage(PageParamList<RequestEnterpriseInsideFile> pageParam)
        {
            IQueryable<EnterpriseInsideFile> queryable = Kily.Set<EnterpriseInsideFile>().OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.FileTitle))
                queryable = queryable.Where(t => t.FileTitle.Contains(pageParam.QueryParam.FileTitle));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseInsideFile()
            {
                Id = t.Id,
                FileTitle = t.FileTitle
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 内部文件详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseEnterpriseInsideFile GetFileDetail(Guid Id)
        {
            var data = Kily.Set<EnterpriseInsideFile>().Where(t => t.Id == Id).Select(t => new ResponseEnterpriseInsideFile()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                FileTitle = t.FileTitle,
                FileContent = t.FileContent
            }).AsNoTracking().FirstOrDefault();
            return data;
        }
        #endregion

        #region 视频监控
        /// <summary>
        /// 视频分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseVedio> GetVedioPage(PageParamList<RequestEnterpriseVedio> pageParam)
        {
            IQueryable<EnterpriseVedio> queryable = Kily.Set<EnterpriseVedio>().OrderByDescending(t => t.CreateTime);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            if (pageParam.QueryParam.IsIndex.HasValue)
                queryable = queryable.Where(t => t.IsIndex == pageParam.QueryParam.IsIndex);
            var data = queryable.Select(t => new ResponseEnterpriseVedio()
            {
                Id = t.Id,
                VedioName = t.VedioName,
                VedioAddr = t.VedioAddr,
                VedioCover = t.VedioCover,
                IsIndex = t.IsIndex
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 编辑视频
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditVedio(RequestEnterpriseVedio Param)
        {
            EnterpriseVedio vedio = Param.MapToEntity<EnterpriseVedio>();
            return Insert(vedio) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 删除视频
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteVedio(Guid Id)
        {
            return Remove<EnterpriseVedio>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 显示视频
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string ShowVedio(Guid Id, bool flag)
        {
            var video = Kily.Set<EnterpriseVedio>().Where(t => t.Id == Id).AsNoTracking().FirstOrDefault();
            video.IsIndex = flag;
            return UpdateField(video, "IsIndex") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATESUCCESS;
        }
        #endregion

        #region 企业自查
        /// <summary>
        /// 获取企业检查分页
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtTemplateChild> GetTemplateChild(PageParamList<RequestGovtTemplateChild> pageParam)
        {
            IQueryable<GovtTemplateChild> queryable = Kily.Set<GovtTemplateChild>().OrderByDescending(t => t.CreateTime).AsNoTracking();
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.TypePath.Contains(CompanyInfo().TypePath));
            else
                queryable = queryable.Where(t => t.TypePath.Contains(CompanyUser().TypePath));
            if (!string.IsNullOrEmpty(pageParam.QueryParam.TemplateName))
                queryable = queryable.Where(t => t.CompanyName.Contains(pageParam.QueryParam.TemplateName));
            var data = queryable.Select(t => new ResponseGovtTemplateChild()
            {
                Id = t.Id,
                CompanyName = t.CompanyName,
                CompanyType = t.CompanyType,
                TemplateName = t.TemplateName,
                CheckUser = t.CheckUser
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 编辑企业自查信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditTemplateChild(RequestGovtTemplateChild Param)
        {
            GovtTemplateChild Data = Param.MapToEntity<GovtTemplateChild>();
            return Insert(Data) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        #endregion
        #endregion

        #region 成长档案
        #region 成长流程
        /// <summary>
        /// 成长流程分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseAgeUp> GetAgeUpPage(PageParamList<RequestEnterpriseAgeUp> pageParam)
        {
            IQueryable<EnterpriseAgeUp> queryable = Kily.Set<EnterpriseAgeUp>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.LvName))
                queryable = queryable.Where(t => t.LvName.Contains(pageParam.QueryParam.LvName));
            if (!string.IsNullOrEmpty(pageParam.QueryParam.BatchNo))
                queryable = queryable.Where(t => t.BatchNo.Contains(pageParam.QueryParam.BatchNo));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseEnterpriseAgeUp()
            {
                Id = t.Id,
                BatchNo = t.BatchNo,
                LvName = t.LvName,
                LvImg = t.LvImg
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 编辑成长流程
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditAgeUp(RequestEnterpriseAgeUp Param)
        {
            EnterpriseAgeUp AgeUp = Param.MapToEntity<EnterpriseAgeUp>();
            return Insert<EnterpriseAgeUp>(AgeUp) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 删除流程
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveAgeUp(Guid Id)
        {
            return Delete<EnterpriseAgeUp>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        #endregion

        #region 育苗信息
        /// <summary>
        /// 育苗分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseGrowInfo> GetGrowInfoPage(PageParamList<RequestEnterpriseGrowInfo> pageParam)
        {
            IQueryable<EnterpriseGrowInfo> queryable = Kily.Set<EnterpriseGrowInfo>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime).AsNoTracking();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.GrowName))
                queryable = queryable.Where(t => t.GrowName.Contains(pageParam.QueryParam.GrowName));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseGrowInfo()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                BatchNo = t.BatchNo,
                GrowName = t.GrowName,
                BuyNum = t.BuyNum,
                PlantTime = t.PlantTime,
                Unit = t.Unit
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 获取育苗详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseEnterpriseGrowInfo GetGrowDetail(Guid Id)
        {
            var data = Kily.Set<EnterpriseGrowInfo>().Where(t => t.Id == Id).Select(t => new ResponseEnterpriseGrowInfo()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                BatchNo = t.BatchNo,
                GrowName = t.GrowName,
                BuyNum = t.BuyNum,
                PlantTime = t.PlantTime,
                Unit = t.Unit,
                Remark = t.Remark,
                SupplierName = t.SupplierName
            }).AsNoTracking().FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 编辑育苗信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditGrow(RequestEnterpriseGrowInfo Param)
        {
            EnterpriseGrowInfo GrowInfo = Param.MapToEntity<EnterpriseGrowInfo>();
            if (Param.Id != Guid.Empty)
            {
                if (Update<EnterpriseGrowInfo, RequestEnterpriseGrowInfo>(GrowInfo, Param))
                    return ServiceMessage.UPDATESUCCESS;
                else
                    return ServiceMessage.UPDATEFAIL;
            }
            else
            {
                if (Insert<EnterpriseGrowInfo>(GrowInfo))
                    return ServiceMessage.INSERTSUCCESS;
                else
                    return ServiceMessage.INSERTFAIL;
            }
        }
        /// <summary>
        /// 删除育苗信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveGrow(Guid Id)
        {
            if (Delete<EnterpriseGrowInfo>(ExpressionExtension.GetExpression<EnterpriseGrowInfo>("Id", Id, ExpressionEnum.Equals)))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 获取批次列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseGrowInfo> GetGrowList()
        {
            IQueryable<EnterpriseGrowInfo> queryable = Kily.Set<EnterpriseGrowInfo>().Where(t => t.IsDelete == false).AsNoTracking();
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseGrowInfo()
            {
                BatchNo = t.BatchNo,
                GrowName = t.GrowName
            }).ToList();
            return data;
        }
        #endregion

        #region 施养管理
        /// <summary>
        /// 施养管理分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterprisePlanting> GetPlantingPage(PageParamList<RequestEnterprisePlanting> pageParam)
        {
            IQueryable<EnterprisePlanting> queryable = Kily.Set<EnterprisePlanting>()
                .Where(t => t.IsDelete == false)
                .OrderByDescending(t => t.CreateTime)
                .Where(t => t.IsType == pageParam.QueryParam.IsType);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.FeedName))
                queryable = queryable.Where(t => t.FeedName.Contains(pageParam.QueryParam.FeedName));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterprisePlanting()
            {
                Id = t.Id,
                BatchNo = t.BatchNo,
                CompanyId = t.CompanyId,
                FeedName = t.FeedName,
                Brand = t.Brand,
                CheckReport = t.CheckReport,
                Supplier = t.Supplier,
                PlantTime = t.PlantTime
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 新增施养记录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditPlanting(RequestEnterprisePlanting Param)
        {
            if (CompanyInfo() != null)
                Param.CompanyId = CompanyInfo().Id;
            else
                Param.CompanyId = CompanyUser().Id;
            EnterprisePlanting planting = Param.MapToEntity<EnterprisePlanting>();
            if (Insert<EnterprisePlanting>(planting))
                return ServiceMessage.INSERTSUCCESS;
            else
                return ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemovePlanting(Guid Id)
        {
            if (Delete<EnterprisePlanting>(t => t.Id == Id))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }
        #endregion

        #region 农药疫情
        /// <summary>
        /// 农药疫情分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseDrug> GetDrugPage(PageParamList<RequestEnterpriseDrug> pageParam)
        {
            IQueryable<EnterpriseDrug> queryable = Kily.Set<EnterpriseDrug>()
                .Where(t => t.IsDelete == false)
                .OrderByDescending(t => t.CreateTime)
                .Where(t => t.IsType == pageParam.QueryParam.IsType);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.DrugName))
                queryable = queryable.Where(t => t.DrugName.Contains(pageParam.QueryParam.DrugName));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseDrug()
            {
                Id = t.Id,
                BatchNo = t.BatchNo,
                CompanyId = t.CompanyId,
                DrugName = t.DrugName,
                Brand = t.Brand,
                CheckReport = t.CheckReport,
                Supplier = t.Supplier,
                PlantTime = t.PlantTime
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 新增农药疫情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditDrug(RequestEnterpriseDrug Param)
        {
            if (CompanyInfo() != null)
                Param.CompanyId = CompanyInfo().Id;
            else
                Param.CompanyId = CompanyUser().Id;
            EnterpriseDrug planting = Param.MapToEntity<EnterpriseDrug>();
            if (Insert<EnterpriseDrug>(planting))
                return ServiceMessage.INSERTSUCCESS;
            else
                return ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveDrug(Guid Id)
        {
            if (Delete<EnterpriseDrug>(t => t.Id == Id))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }
        #endregion

        #region 环境检测
        /// <summary>
        /// 环境检测分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseEnvironment> GetEnvPage(PageParamList<RequestEnterpriseEnvironment> pageParam)
        {
            IQueryable<EnterpriseEnvironment> queryable = Kily.Set<EnterpriseEnvironment>().Where(t => t.IsDelete == false);
            if (pageParam.QueryParam.RecordTime.HasValue)
                queryable = queryable.Where(t => t.RecordTime <= pageParam.QueryParam.RecordTime);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseEnterpriseEnvironment()
            {
                Id = t.Id,
                BatchNo = t.BatchNo,
                CompanyId = t.CompanyId,
                AirEnv = t.AirEnv,
                AirHdy = t.AirHdy,
                SoilEnv = t.SoilEnv,
                SoilHdy = t.SoilHdy,
                Light = t.Light,
                CO2 = t.CO2,
                RecordTime = t.RecordTime
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 新增环境检测
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditEnv(RequestEnterpriseEnvironment Param)
        {
            if (CompanyInfo() != null)
                Param.CompanyId = CompanyInfo().Id;
            else
                Param.CompanyId = CompanyUser().Id;
            EnterpriseEnvironment Env = Param.MapToEntity<EnterpriseEnvironment>();
            if (Insert<EnterpriseEnvironment>(Env))
                return ServiceMessage.INSERTSUCCESS;
            else
                return ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 删除环境检测
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveEnv(Guid Id)
        {
            if (Delete<EnterpriseEnvironment>(t => t.Id == Id))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 环境检测附加表分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseEnvironmentAttach> GetEnvAttachPage(PageParamList<RequestEnterpriseEnvironmentAttach> pageParam)
        {
            IQueryable<EnterpriseEnvironmentAttach> queryable = Kily.Set<EnterpriseEnvironmentAttach>().Where(t => t.IsDelete == false).AsNoTracking();
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CreateUser == CompanyInfo().Id.ToString() || GetChildIdList(CompanyInfo().Id).Contains(Guid.Parse(t.CreateUser)));
            else
                queryable = queryable.Where(t => t.CreateUser == CompanyUser().Id.ToString());
            var data = queryable.Select(t => new ResponseEnterpriseEnvironmentAttach()
            {
                Id = t.Id,
                BatchNo = t.BatchNo,
                AirReport = t.AirReport,
                MetalReport = t.MetalReport,
                RecordTime = t.RecordTime,
                SoilReport = t.SoilReport,
                WaterReport = t.WaterReport
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 新增环境附加信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditEnvAttach(RequestEnterpriseEnvironmentAttach Param)
        {
            EnterpriseEnvironmentAttach Attach = Param.MapToEntity<EnterpriseEnvironmentAttach>();
            if (Insert<EnterpriseEnvironmentAttach>(Attach))
                return ServiceMessage.INSERTSUCCESS;
            else
                return ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 删除环境检测
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveEnvAttach(Guid Id)
        {
            if (Delete<EnterpriseEnvironmentAttach>(t => t.Id == Id))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }
        #endregion

        #region 成长日记
        /// <summary>
        /// 日记分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseNote> GetNotePage(PageParamList<RequestEnterpriseNote> pageParam)
        {
            IQueryable<EnterpriseNote> queryable = Kily.Set<EnterpriseNote>().Where(t => t.IsDelete == false).AsNoTracking();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.NoteName))
                queryable = queryable.Where(t => t.NoteName.Contains(pageParam.QueryParam.NoteName));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseEnterpriseNote()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                BatchNo = t.BatchNo,
                NoteName = t.NoteName,
                ResultTime = t.ResultTime,
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 日记列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseNote> GetNoteList()
        {
            IQueryable<EnterpriseNote> queryable = Kily.Set<EnterpriseNote>().Where(t => t.IsDelete == false).AsNoTracking();
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseNote()
            {
                Id = t.Id,
                NoteName = t.NoteName
            }).ToList();
            return data;
        }
        /// <summary>
        /// 编辑日记
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditNote(RequestEnterpriseNote Param)
        {
            EnterpriseNote Note = Param.MapToEntity<EnterpriseNote>();
            if (Param.Id != Guid.Empty)
            {
                if (Update<EnterpriseNote, RequestEnterpriseNote>(Note, Param))
                    return ServiceMessage.UPDATESUCCESS;
                else
                    return ServiceMessage.UPDATEFAIL;
            }
            else
            {
                if (Insert<EnterpriseNote>(Note))
                    return ServiceMessage.INSERTSUCCESS;
                else
                    return ServiceMessage.INSERTFAIL;
            }
        }
        /// <summary>
        /// 删除日记
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveNote(Guid Id)
        {
            if (Delete<EnterpriseNote>(t => t.Id == Id))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 获取日记详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseEnterpriseNote GetNoteDetail(Guid Id)
        {
            var data = Kily.Set<EnterpriseNote>().Where(t => t.Id == Id).AsNoTracking().Select(t => new ResponseEnterpriseNote()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                BatchNo = t.BatchNo,
                NoteName = t.NoteName,
                ResultTime = t.ResultTime
            }).FirstOrDefault();
            return data;
        }
        #endregion
        #endregion

        #region 物码管理
        /// <summary>
        /// 二维码分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseTag> GetTagPage(PageParamList<RequestEnterpriseTag> pageParam)
        {
            IQueryable<EnterpriseTag> queryable = Kily.Set<EnterpriseTag>().Where(t => t.IsDelete == false).Where(t => t.TagType == pageParam.QueryParam.TagType).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.BatchNo))
                queryable = queryable.Where(t => t.BatchNo.Contains(pageParam.QueryParam.BatchNo));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.AsNoTracking().Select(t => new ResponseEnterpriseTag()
            {
                Id = t.Id,
                BatchNo = t.BatchNo,
                CompanyId = t.CompanyId,
                EndSerialNo = t.CodeDiscern + t.EndSerialNo,
                StarSerialNo = t.CodeDiscern + t.StarSerialNo,
                TotalNo = t.TotalNo,
                TagType = t.TagType,
                UseNum = t.UseNum,
                IsCreate = t.IsCreate,
                TagTypeName = AttrExtension.GetSingleDescription<TagEnum, DescriptionAttribute>(t.TagType)
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 首页号段查询接口
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ResponseEnterpriseTag GetTagDetailWeb(String key)
        {
            long LastStr = 0;
            if (key.Contains("P"))
                LastStr = Convert.ToInt64(key.Split("P")[1].Substring(0, 12));
            else if (key.Contains("W"))
                LastStr = Convert.ToInt64(key.Split("W")[1].Substring(0, 12));
            else
                return null;
            IQueryable<EnterpriseTagAttach> queryable = Kily.Set<EnterpriseTagAttach>().AsNoTracking()
                .Where(t => t.StarSerialNo <= LastStr && t.EndSerialNo >= LastStr);
            IQueryable<EnterpriseTag> queryables = Kily.Set<EnterpriseTag>().AsNoTracking();
            return queryables.Join(queryable, t => t.Id, x => x.TagId, (t, x) => new ResponseEnterpriseTag()
            {
                BatchNo = t.BatchNo,
                TagTypeName = AttrExtension.GetSingleDescription<TagEnum, DescriptionAttribute>(t.TagType),
                IsCreate = t.IsCreate,
                EndSerialNo = t.CodeDiscern + t.EndSerialNo,
                StarSerialNo = t.CodeDiscern + t.StarSerialNo,
            }).FirstOrDefault();
        }
        /// <summary>
        /// 创建空白标签
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string UpdateEmptyTag(Guid Id)
        {
            EnterpriseTag Tag = Kily.Set<EnterpriseTag>().Where(t => t.Id == Id).AsNoTracking().FirstOrDefault();
            Tag.IsCreate = true;
            return UpdateField(Tag, "IsCreate") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <returns></returns>
        public string CreateTag(RequestEnterpriseTag Param)
        {
            if ((int)Param.TagType == 0)
                return "请选择类型!";
            //取省份code
            IQueryable<SystemProvince> queryable = Kily.Set<SystemProvince>().AsNoTracking();
            //企业信息
            EnterpriseInfo info = Kily.Set<EnterpriseInfo>().Where(t => t.Id == Param.CompanyId).FirstOrDefault();
            if (info.TagCodeNum <= 0)
                return "二维码数量小于等于零，请购买二维码标签！";
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => CompanyInfo().TypePath.Contains(t.Id.ToString()));
            else
                queryable = queryable.Where(t => CompanyUser().TypePath.Contains(t.Id.ToString()));
            SystemProvince Province = queryable.FirstOrDefault();
            //取申请表
            EnterpriseTagApply Apply = Kily.Set<EnterpriseTagApply>().Where(t => t.AuditType == AuditEnum.FinanceSuccess)
                .Where(t => t.CompanyId == Param.CompanyId && t.IsDelete == false).FirstOrDefault();
            //懒加载主表信息
            IQueryable<EnterpriseTag> queryables = Kily.Set<EnterpriseTag>()
                .Where(t => t.CompanyId == Param.CompanyId).Where(t => t.TagType == Param.TagType).OrderByDescending(t => t.CreateTime);
            List<EnterpriseTag> TagList = queryables.ToList();
            if (TagList.Count == 0)
                Param.StarSerialNo = Convert.ToInt64(Province.Code + "1000000001");
            else
                Param.StarSerialNo = TagList.FirstOrDefault().EndSerialNo + 1;
            Param.EndSerialNo = Param.StarSerialNo + Param.TotalNo - 1;
            EnterpriseTag Tag = Param.MapToEntity<EnterpriseTag>();
            Tag.TotalNo = Tag.TotalNo;
            //生成企业码更新企业信息表的二维码数量
            if (Tag.TagType == TagEnum.OneEnterprise)
            {
                Tag.TotalNo = 1;
                Tag.CodeDiscern = Param.CodeStar + "C";
                Tag.EndSerialNo = Tag.StarSerialNo;
                var data = queryables.Where(t => t.TagType == TagEnum.OneEnterprise).ToList().Count >= 1 ?
                     "企业只能拥有一个企业二维码!" :
                     (Tag.TotalNo > 1 ? "一个企业只能创建一个企业二维码!" :
                     (Insert(Tag) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL));
                info.TagCodeNum -= 1;
                UpdateField<EnterpriseInfo>(info, "TagCodeNum");
                return data;
            }
            else
            {
                if (Tag.TagType == TagEnum.OneBrand)
                    Tag.CodeDiscern = Param.CodeStar + "P";
                else if (Tag.TagType == TagEnum.OneThing)
                    Tag.CodeDiscern = Param.CodeStar + "W";
                else
                    Tag.CodeDiscern = Param.CodeStar + "B";
                info.TagCodeNum -= Tag.TotalNo;
                if (info.TagCodeNum < 0)
                    return $"当前剩余标签数量:{info.TagCodeNum},请升级版本或申请购买数量!";
                else
                    UpdateField<EnterpriseInfo>(info, "TagCodeNum");
                return Insert<EnterpriseTag>(Tag) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
        }
        /// <summary>
        /// 删除二维码
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveTag(Guid Id)
        {
            if (Remove<EnterpriseTag>(t => t.Id == Id))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 申请标签分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseApply> GetTagApplyPage(PageParamList<RequestEnterpriseApply> pageParam)
        {
            IQueryable<EnterpriseTagApply> queryable = Kily.Set<EnterpriseTagApply>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.BatchNo))
                queryable = queryable.Where(t => t.BatchNo.Contains(pageParam.QueryParam.BatchNo));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.OrderByDescending(t => t.CreateTime).AsNoTracking().Select(t => new ResponseEnterpriseApply()
            {
                Id = t.Id,
                BatchNo = t.BatchNo,
                TagTypeName = AttrExtension.GetSingleDescription<TagEnum, DescriptionAttribute>(t.TagType),
                ApplyNum = t.ApplyNum,
                ApplyMoney = t.ApplyMoney,
                Payment = t.Payment,
                IsPay = t.IsPay,
                AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(t.AuditType)
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 删除申请标签
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveApplyTag(Guid Id)
        {
            if (Delete<EnterpriseTagApply>(t => t.Id == t.Id))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 获取缴费详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseEnterpriseApply GetPaymentDetail(Guid Id)
        {
            var data = Kily.Set<EnterpriseTagApply>().Where(t => t.Id == Id).Select(t => new ResponseEnterpriseApply()
            {
                Id = t.Id,
                BatchNo = t.BatchNo,
                ApplyMoney = t.ApplyMoney
            }).FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 申请标签
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string ApplyEdit(RequestEnterpriseApply Param)
        {
            EnterpriseTagApply TagApply = Param.MapToEntity<EnterpriseTagApply>();
            if (Param.Id == Guid.Empty)
            {
                TagApply.AuditType = AuditEnum.WaitAduit;
                if (TagApply.Payment == 1)
                {
                    if (string.IsNullOrEmpty(TagApply.PaytTicket))
                        return "请先付款";
                    else
                        return Insert<EnterpriseTagApply>(TagApply) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
                }
                else
                {
                    IList<EnterpriseTagApply> Apply = Kily.Set<EnterpriseTagApply>().Where(t => t.CompanyId == TagApply.CompanyId).ToList();
                    if (Apply.Count == 0)
                    {
                        return Convert.ToInt32(Param.ApplyNum) > 10000 ? "申请失败" : (Insert<EnterpriseTagApply>(TagApply) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL);
                    }
                    else
                    {
                        if (Apply.Where(t => t.IsPay == false).ToList().Count > 0)
                            return "请先完成以前款项";
                        else
                            return Convert.ToInt32(Param.ApplyNum) > 100000 ? "申请失败" : (Insert<EnterpriseTagApply>(TagApply) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL);
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Param.PaytTicket))
                    return "请上传缴费凭证";
                IList<String> Fieds = new List<String>
                {
                    "IsPay",
                    "PaytTicket"
                };
                return UpdateField<EnterpriseTagApply>(TagApply, null, Fieds) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            }
        }
        /// <summary>
        /// 纹理二维码分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseVeinTag> GetVeinTargetPage(PageParamList<RequestVeinTag> pageParam)
        {
            IQueryable<EnterpriseVeinTag> queryable = Kily.Set<EnterpriseVeinTag>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.BatchNo))
                queryable = queryable.Where(t => t.BatchNo.Contains(pageParam.QueryParam.BatchNo));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseVeinTag()
            {
                Id = t.Id,
                BatchNo = t.BatchNo,
                StarSerialNo = t.StarSerialNo,
                EndSerialNo = t.EndSerialNo,
                TotalNo = t.TotalNo,
                AcceptUserName = t.AcceptUserName,
                AllotType = t.AllotType,
                AllotNum = t.UseNum,
                IsAccept = t.IsAccept,
                IsAcceptName = t.IsAccept ? "已签收" : "未签收"
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 签收
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string AcceptVeinTarget(Guid Id)
        {
            EnterpriseVeinTag VeinTag = Kily.Set<EnterpriseVeinTag>().Where(t => t.Id == Id).FirstOrDefault();
            VeinTag.IsAccept = true;
            VeinTag.AcceptTime = DateTime.Now;
            if (UpdateField<EnterpriseVeinTag>(VeinTag, "IsAccept"))
                return ServiceMessage.UPDATESUCCESS;
            else
                return ServiceMessage.UPDATEFAIL;
        }
        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveVeinTarget(Guid Id)
        {
            return Delete<EnterpriseVeinTag>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 获取标签批次
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Object GetTagList(int type)
        {
            if (type == 1)
            {
                IQueryable<EnterpriseVeinTag> queryable = Kily.Set<EnterpriseVeinTag>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
                if (CompanyInfo() != null)
                    queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
                else
                    queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
                return queryable.Where(t => t.TotalNo != 0).Select(t => new ResponseVeinTag()
                {
                    Id = t.Id,
                    BatchNo = t.BatchNo,
                    TotalNo = t.TotalNo,
                }).AsNoTracking().ToList();
            }
            else
            {
                IQueryable<EnterpriseTag> queryable = Kily.Set<EnterpriseTag>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
                if (CompanyInfo() != null)
                    queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
                else
                    queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
                if (type == 2)
                    queryable = queryable.Where(t => t.TagType == TagEnum.OneThing);
                if (type == 3)
                    queryable = queryable.Where(t => t.TagType == TagEnum.OneBrand);
                return queryable.Where(t => t.TotalNo != 0).Select(t => new ResponseEnterpriseTag()
                {
                    Id = t.Id,
                    BatchNo = t.BatchNo,
                    TotalNo = t.TotalNo,
                }).AsNoTracking().ToList();
            }
        }
        /// <summary>
        /// 获取开始标签
        /// </summary>
        /// <param name="BatchNo"></param>
        /// <returns></returns>
        public Object GetCodeNo(int Type, string BatchNo)
        {
            IList<EnterpriseTagAttach> Tags = Kily.Set<EnterpriseTagAttach>().Where(t => t.IsDelete == false).Where(t => t.TagBatchNo == BatchNo).OrderByDescending(t => t.CreateTime).AsNoTracking().ToList();
            EnterpriseTag Tag = Kily.Set<EnterpriseTag>().Where(t => t.IsDelete == false).Where(t => t.BatchNo == BatchNo).FirstOrDefault();
            EnterpriseVeinTag VeinTag = Kily.Set<EnterpriseVeinTag>().Where(t => t.IsDelete == false).Where(t => t.BatchNo == BatchNo).FirstOrDefault();
            if (Tags.Count == 0)
            {
                if (Type != 1)
                    return Tag.StarSerialNo;
                else
                    return VeinTag.StarSerialNo;
            }
            else
            {
                return Tags.FirstOrDefault().EndSerialNo + 1;
            }
        }
        /// <summary>
        /// 扫码管理
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseScanCode> GetScanCodePage(PageParamList<RequestEnterpriseGoods> pageParam)
        {
            IQueryable<EnterpriseGoods> goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseGoodsStock> stocks = Kily.Set<EnterpriseGoodsStock>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseTagAttach> attaches = Kily.Set<EnterpriseTagAttach>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseTag> tags = Kily.Set<EnterpriseTag>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseGoodsStockAttach> stockAttaches = Kily.Set<EnterpriseGoodsStockAttach>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.ProductName))
                goods = goods.Where(t => t.ProductName.Contains(pageParam.QueryParam.ProductName));
            if (CompanyInfo() != null)
                goods = goods.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                goods = goods.Where(t => t.CompanyId == CompanyUser().Id);

            var data = goods.Join(stocks, t => t.Id, x => x.GoodsId, (t, x) => new { t, x })
                  .Join(attaches, y => y.x.GoodsBatchNo, z => z.StockNo, (y, z) => new { y, z })
                  .Join(stockAttaches, q => q.y.x.Id, p => p.StockId, (q, p) => new { q, p })
                  .Join(tags, n => n.q.z.TagId, m => m.Id, (n, m) => new { n, m.IsCreate })
                  .Where(t => t.n.q.y.x.GoodsId == t.n.q.z.GoodsId)
                  .Select(t => new ResponseEnterpriseScanCode()
                  {
                      Id = t.n.q.y.x.Id,
                      ProductName = t.n.q.y.t.ProductName,
                      ExpiredDate = t.n.q.y.t.ExpiredDate,
                      StarSerialNo = t.n.q.z.StarSerialNo,
                      EndSerialNo = t.n.q.z.EndSerialNo,
                      StarSerialNos = t.n.q.z.StarSerialNos,
                      EndSerialNos = t.n.q.z.EndSerialNos,
                      ProductType = t.n.q.y.t.ProductType,
                      BatchNo = t.n.p.GoodsBatchNo,
                      IsCreate = t.IsCreate
                  })
                  .ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 查看绑定信息
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseTagAttach> GetTagAttachPage(PageParamList<RequestEnterpriseTagAttach> pageParam)
        {
            var data = Kily.Set<EnterpriseTagAttach>().Where(t => t.TagId == pageParam.QueryParam.Id)
                .OrderByDescending(t => t.CreateTime)
                .Select(t => new ResponseEnterpriseTagAttach()
                {
                    Id = t.Id,
                    StarSerialNos = t.StarSerialNos,
                    StarSerialNo = t.StarSerialNo,
                    EndSerialNos = t.EndSerialNos,
                    EndSerialNo = t.EndSerialNo,
                    StockNo = t.StockNo,
                    UseNum = t.UseNum,
                    StockStutas = Kily.Set<EnterpriseGoodsStock>().Where(x => x.GoodsBatchNo == t.StockNo).AsNoTracking().FirstOrDefault()
                }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 箱码绑定情况
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseBoxing> GetBoxPage(PageParamList<RequestEnterpriseBoxing> pageParam)
        {
            IQueryable<EnterpriseBoxing> queryable = Kily.Set<EnterpriseBoxing>().Where(t => t.IsDelete == false);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            Int64 Star = Convert.ToInt64(pageParam.QueryParam.StarCode.Split("B")[1]);
            Int64 End = Convert.ToInt64(pageParam.QueryParam.EndCode.Split("B")[1]);
            var data = queryable.Where(t => t.BoxCodeSort >= Star && t.BoxCodeSort <= End).Select(t => new ResponseEnterpriseBoxing
            {
                Id = t.Id,
                BoxCode = t.BoxCode,
                StockBatchNo = t.StockBatchNo,
                BoxBatchNo = t.BoxBatchNo,
                GoodName = t.GoodName,
                BoxCount = t.BoxCount,
                ProductionBatchNo = t.ProductionBatchNo,
                BoxTime = t.BoxTime
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        #endregion

        #region 厂商管理
        /// <summary>
        /// 厂商分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseSeller> GetSellerPage(PageParamList<RequestEnterpriseSeller> pageParam)
        {
            IQueryable<EnterpriseSeller> queryable = Kily.Set<EnterpriseSeller>().Where(t => t.IsDelete == false).Where(t => t.SellerType == pageParam.QueryParam.SellerType);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.SupplierName))
                queryable = queryable.Where(t => t.SupplierName.Contains(pageParam.QueryParam.SupplierName));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.OrderByDescending(t => t.CreateTime).AsNoTracking().Select(t => new ResponseEnterpriseSeller()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                No = t.No,
                SupplierType = t.SupplierType,
                SupplierName = t.SupplierName,
                DutyMan = t.DutyMan,
                LinkPhone = t.LinkPhone,
                Address = t.Address,
                Code = t.Code,
                RunCard = t.RunCard,
                OkayCard = t.OkayCard,
                IdCard = t.IdCard,
                ProductCard = t.ProductCard,
                SellerType = t.SellerType,
                SellerTypeName = AttrExtension.GetSingleDescription<SellerEnum, DescriptionAttribute>(t.SellerType)
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 编辑厂商
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditSeller(RequestEnterpriseSeller Param)
        {
            EnterpriseSeller seller = Param.MapToEntity<EnterpriseSeller>();
            if (Param.Id != Guid.Empty)
            {
                return Update<EnterpriseSeller, RequestEnterpriseSeller>(seller, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            }
            else
            {
                return Insert<EnterpriseSeller>(seller) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
        }
        /// <summary>
        /// 厂商详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseEnterpriseSeller GetSellerDetail(Guid Id)
        {
            var data = Kily.Set<EnterpriseSeller>().Where(t => t.Id == Id).AsNoTracking().Select(t => new ResponseEnterpriseSeller()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                No = t.No,
                SupplierType = t.SupplierType,
                SupplierName = t.SupplierName,
                DutyMan = t.DutyMan,
                LinkPhone = t.LinkPhone,
                Address = t.Address,
                Code = t.Code,
                RunCard = t.RunCard,
                OkayCard = t.OkayCard,
                IdCard = t.IdCard,
                ProductCard = t.ProductCard,
                SellerType = t.SellerType
            }).FirstOrDefault();
            return data;
        }
        #endregion

        #region 原料管理
        #region 原料
        /// <summary>
        /// 原辅料分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseMaterial> GetMaterialPage(PageParamList<RequestEnterpriseMaterial> pageParam)
        {
            IQueryable<EnterpriseMaterial> queryable = Kily.Set<EnterpriseMaterial>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime).AsNoTracking();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.MaterName))
                queryable = queryable.Where(t => t.MaterName.Contains(pageParam.QueryParam.MaterName));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseMaterial()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                BatchNo = t.BatchNo,
                MaterName = t.MaterName,
                Spec = t.Spec,
                Standard = t.Standard,
                Supplier = t.Supplier,
                Unit = t.Unit,
                Address = t.Address,
                ExpiredDay = t.ExpiredDay,
                PackageType = t.PackageType
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 编辑原料
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditMaterial(RequestEnterpriseMaterial Param)
        {
            EnterpriseMaterial material = Param.MapToEntity<EnterpriseMaterial>();
            return Insert<EnterpriseMaterial>(material) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 删除原料
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveMaterial(Guid Id)
        {
            return Delete(ExpressionExtension.GetExpression<EnterpriseMaterial>("Id", Id, ExpressionEnum.Equals)) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 获取原料列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseMaterial> GetMaterialList()
        {
            IQueryable<EnterpriseMaterial> queryable = Kily.Set<EnterpriseMaterial>().Where(t => t.IsDelete == false).AsNoTracking().OrderByDescending(t => t.CreateTime);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseMaterial()
            {
                Id = t.Id,
                BatchNo = t.BatchNo,
                MaterName = t.MaterName
            }).ToList();
            return data;
        }
        #endregion
        #region 入库
        /// <summary>
        /// 获取入库分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseMaterialStock> GetStockPage(PageParamList<RequestEnterpriseMaterialStock> pageParam)
        {
            IQueryable<EnterpriseMaterialStock> queryable = Kily.Set<EnterpriseMaterialStock>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseMaterial> queryables = Kily.Set<EnterpriseMaterial>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.MaterName))
                queryables = queryables.Where(t => t.MaterName.Contains(pageParam.QueryParam.MaterName));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.OrderByDescending(t => t.CreateTime)
                .Join(queryables, t => t.BatchNo, x => x.BatchNo, (t, x) => new ResponseEnterpriseMaterialStock()
                {
                    Id = t.Id,
                    MaterName = x.MaterName,
                    CompanyId = t.CompanyId,
                    SerializNo = t.SerializNo,
                    BatchNo = t.BatchNo,
                    StockType = t.StockType,
                    CheckMaterialId = t.CheckMaterialId,
                    SetStockNum = t.SetStockNum,
                    SetStockTime = t.SetStockTime,
                    SetStockUser = t.SetStockUser
                }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 编辑入库
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditStock(RequestEnterpriseMaterialStock Param)
        {
            EnterpriseMaterialStock stock = Param.MapToEntity<EnterpriseMaterialStock>();
            return Insert<EnterpriseMaterialStock>(stock) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 删除入库
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveStock(Guid Id)
        {
            return Remove<EnterpriseMaterialStock>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        public string UpdateMaterStockCheck(RequestEnterpriseMaterialStock Param)
        {
            EnterpriseMaterialStock queryable = Kily.Set<EnterpriseMaterialStock>().Where(t => t.IsDelete == false).Where(t => t.Id == Param.Id).AsNoTracking().FirstOrDefault();
            queryable.CheckMaterialId = Param.CheckMaterialId;
            return UpdateField(queryable, "CheckMaterialId") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }
        #endregion
        #region 出库
        /// <summary>
        /// 出库分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseMaterialStockAttach> GetOutStockPage(PageParamList<RequestEnterpriseMaterialStockAttach> pageParam)
        {
            IQueryable<EnterpriseMaterialStockAttach> StockAttach = Kily.Set<EnterpriseMaterialStockAttach>().Where(t => t.IsDelete == false).AsNoTracking();
            IQueryable<EnterpriseMaterialStock> Stock = Kily.Set<EnterpriseMaterialStock>().Where(t => t.IsDelete == false).AsNoTracking();
            IQueryable<EnterpriseMaterial> Material = Kily.Set<EnterpriseMaterial>().Where(t => t.IsDelete == false).AsNoTracking();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.MaterName))
                Material = Material.Where(t => t.MaterName.Contains(pageParam.QueryParam.MaterName));
            if (CompanyInfo() != null)
                StockAttach = StockAttach.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                StockAttach = StockAttach.Where(t => t.CompanyId == CompanyUser().Id);
            var data = StockAttach.OrderByDescending(t => t.CreateTime)
                 .Join(Stock, t => t.MaterialStockId, x => x.Id, (t, x) => new { t, x })
                 .Join(Material, p => p.x.BatchNo, y => y.BatchNo, (p, y) => new ResponseEnterpriseMaterialStockAttach()
                 {
                     Id = p.t.Id,
                     MaterName = y.MaterName,
                     CompanyId = p.t.CompanyId,
                     SerializNo = p.t.SerializNo,
                     BatchNo = y.BatchNo,
                     StockType = p.t.StockType,
                     OutStockNum = p.t.OutStockNum,
                     OutStockTime = p.t.OutStockTime,
                     OutStockUser = p.t.OutStockUser,
                     StockEx = p.x.SetStockNum - p.t.OutStockNum,
                 }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 删除出库记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveStockAttach(Guid Id)
        {
            return Remove<EnterpriseMaterialStockAttach>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 编辑出库
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditOutStock(RequestEnterpriseMaterialStockAttach Param)
        {
            EnterpriseMaterialStock Stock = Kily.Set<EnterpriseMaterialStock>().Where(t => t.Id == Param.MaterialStockId).FirstOrDefault();
            EnterpriseMaterialStockAttach Attach = Param.MapToEntity<EnterpriseMaterialStockAttach>();
            int Count = Convert.ToInt32(Stock.SetStockNum) - Convert.ToInt32(Attach.OutStockNum);
            if (Count < 0)
                return $"当前库存剩余{Stock.SetStockNum},实际出库为{Attach.OutStockNum}";
            else
            {
                Stock.SetStockNum = Count;
                UpdateField<EnterpriseMaterialStock>(Stock, "SetStockNum");
                return Insert<EnterpriseMaterialStockAttach>(Attach) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
        }
        /// <summary>
        /// 获取出库原料列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseMaterial> GetOutStockMaterialList()
        {
            IQueryable<EnterpriseMaterialStockAttach> Attach = Kily.Set<EnterpriseMaterialStockAttach>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseMaterialStock> Stock = Kily.Set<EnterpriseMaterialStock>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseMaterial> queryable = Kily.Set<EnterpriseMaterial>().Where(t => t.IsDelete == false).AsNoTracking().OrderByDescending(t => t.CreateTime);
            if (CompanyInfo() != null)
                Attach = Attach.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                Attach = Attach.Where(t => t.CompanyId == CompanyUser().Id);
            var data = Attach.Join(Stock, t => t.MaterialStockId, y => y.Id, (t, y) => new { t, y }).Join(queryable, o => o.y.BatchNo, p => p.BatchNo, (o, p) => new ResponseEnterpriseMaterial()
            {
                Id = p.Id,
                BatchNo = o.t.SerializNo,
                MaterName = p.MaterName
            }).ToList();
            return data;
        }
        #endregion
        #endregion

        #region 生产管理
        #region 设备管理
        /// <summary>
        /// 设备分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseDevice> GetDevicePage(PageParamList<RequestEnterpriseDevice> pageParam)
        {
            IQueryable<EnterpriseDevice> queryable = Kily.Set<EnterpriseDevice>().Where(t => t.IsDelete == false).AsNoTracking();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.DeviceName))
                queryable = queryable.Where(t => t.DeviceName.Contains(pageParam.QueryParam.DeviceName));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseEnterpriseDevice
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                DeviceName = t.DeviceName,
                ProductTime = t.ProductTime,
                ModelName = t.ModelName,
                SupplierName = t.SupplierName,
                Manager = t.Manager,
                Life = t.Life
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 设备列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseDevice> GetDeviceList()
        {
            IQueryable<EnterpriseDevice> queryable = Kily.Set<EnterpriseDevice>().Where(t => t.IsDelete == false);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseDevice()
            {
                Id = t.Id,
                DeviceName = t.DeviceName
            }).AsNoTracking().ToList();
            return data;
        }
        /// <summary>
        /// 设备清洗分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseDeviceClean> GetDeviceCleanPage(PageParamList<RequestEnterpriseDeviceClean> pageParam)
        {
            IQueryable<EnterpriseDeviceClean> queryable = Kily.Set<EnterpriseDeviceClean>().Where(t => t.IsDelete == false).Where(t => t.DeviceId == pageParam.QueryParam.DeviceId);
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseEnterpriseDeviceClean()
            {
                Id = t.Id,
                CleanTime = t.CleanTime,
                Manager = t.Manager,
                Ways = t.Ways
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 设备维护分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseDeviceFix> GetDeviceFixPage(PageParamList<RequestEnterpriseDeviceFix> pageParam)
        {
            IQueryable<EnterpriseDeviceFix> queryable = Kily.Set<EnterpriseDeviceFix>().Where(t => t.IsDelete == false).Where(t => t.DeviceId == pageParam.QueryParam.DeviceId);
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseEnterpriseDeviceFix()
            {
                Id = t.Id,
                FixTime = t.FixTime,
                Manager = t.Manager,
                Reason = t.Reason
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }


        #endregion
        #region 产品系列
        /// <summary>
        /// 产品系列分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseProductSeries> GetSeriesPage(PageParamList<RequestEnterpriseProductSeries> pageParam)
        {
            IQueryable<EnterpriseProductSeries> queryable = Kily.Set<EnterpriseProductSeries>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.SeriesName))
                queryable = queryable.Where(t => t.SeriesName.Contains(pageParam.QueryParam.SeriesName));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseEnterpriseProductSeries()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                SeriesName = t.SeriesName,
                Standard = t.Standard,
                TargetName = string.Join(",", Kily.Set<EnterpriseTarget>().Where(x => t.TargetId.Contains(x.Id.ToString())).Select(x => x.TargetName).ToArray())
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 编辑系列
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditSeries(RequestEnterpriseProductSeries Param)
        {
            EnterpriseProductSeries series = Param.MapToEntity<EnterpriseProductSeries>();
            return Insert<EnterpriseProductSeries>(series) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 删除系列
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveSeries(Guid Id)
        {
            return Delete<EnterpriseProductSeries>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 系列列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseProductSeries> GetSeriesList()
        {
            IQueryable<EnterpriseProductSeries> queryable = Kily.Set<EnterpriseProductSeries>().Where(t => t.IsDelete == false);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseProductSeries()
            {
                Id = t.Id,
                SeriesName = t.SeriesName
            }).ToList();
            return data;
        }
        #endregion
        #region 指标把控
        /// <summary>
        /// 指标分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseTarget> GetTargetPage(PageParamList<RequestEnterpriseTarget> pageParam)
        {
            IQueryable<EnterpriseTarget> queryable = Kily.Set<EnterpriseTarget>().Where(t => t.IsDelete == false);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.TargetName))
                queryable = queryable.Where(t => t.TargetName.Contains(pageParam.QueryParam.TargetName));
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseEnterpriseTarget()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                TargetName = t.TargetName,
                TargetValue = t.TargetValue,
                Standard = t.Standard,
                TargetUnit = t.TargetUnit
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 获取指标列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseTarget> GetTargetList()
        {
            IQueryable<EnterpriseTarget> queryable = Kily.Set<EnterpriseTarget>().Where(t => t.IsDelete == false).AsNoTracking();
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseTarget()
            {
                Id = t.Id,
                TargetName = t.TargetName
            }).ToList();
            return data;
        }
        #endregion
        #region 生产批次
        /// <summary>
        /// 生产批次分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseProductionBatch> GetProBatchPage(PageParamList<RequestEnterpriseProductionBatch> pageParam)
        {
            IQueryable<EnterpriseProductionBatch> queryable = Kily.Set<EnterpriseProductionBatch>().Where(t => t.IsDelete == false).AsNoTracking();
            IQueryable<EnterpriseProductSeries> queryables = Kily.Set<EnterpriseProductSeries>().Where(t => t.IsDelete == false).AsNoTracking();
            var Material = Kily.Set<EnterpriseMaterialStockAttach>().Where(t => t.IsDelete == false)
               .Join(Kily.Set<EnterpriseMaterialStock>().Where(t => t.IsDelete == false), t => t.MaterialStockId, x => x.Id, (t, x) => new { t, x })
               .Join(Kily.Set<EnterpriseMaterial>().Where(t => t.IsDelete == false), p => p.x.BatchNo, y => y.BatchNo, (p, y) => new ResponseEnterpriseMaterial
               {
                   Id = y.Id,
                   MaterName = y.MaterName,
               }).AsNoTracking();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.SeriesName))
                queryables = queryables.Where(t => t.SeriesName.Contains(pageParam.QueryParam.SeriesName));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.OrderByDescending(t => t.CreateTime).Join(queryables, t => t.SeriesId, x => x.Id, (t, x) => new ResponseEnterpriseProductionBatch()
            {
                DeviceName = t.DeviceName,
                SeriesName = x.SeriesName,
                Id = t.Id,
                CompanyId = t.CompanyId,
                Manager = t.Manager,
                StartTime = t.StartTime,
                BatchNo = t.BatchNo,
                MaterialId = t.MaterialId,
                MaterialList = Material.ToList()
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 生产批次列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseProductionBatch> GetProBatchList()
        {
            IQueryable<EnterpriseProductionBatch> queryable = Kily.Set<EnterpriseProductionBatch>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseProductSeries> queryables = Kily.Set<EnterpriseProductSeries>().Where(t => t.IsDelete == false);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.GroupJoin(queryables, t => t.SeriesId, x => x.Id, (t, x) => new ResponseEnterpriseProductionBatch()
            {
                Id = t.Id,
                SeriesName = x.FirstOrDefault() != null ? x.FirstOrDefault().SeriesName : null,
                BatchNo = t.BatchNo
            }).AsNoTracking().ToList();
            return data;
        }
        /// <summary>
        /// 删除生产批次
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveProBatch(Guid Id)
        {
            return Delete<EnterpriseProductionBatch>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 编辑生产批次
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditProBatch(RequestEnterpriseProductionBatch Param)
        {
            EnterpriseProductionBatch ProductionBatch = Param.MapToEntity<EnterpriseProductionBatch>();
            return Insert(ProductionBatch) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 对比指标值
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IList<ResponseEnterpriseProductionBatchAttach> GetProBatchAttachList(Guid Id)
        {
            IQueryable<EnterpriseProductionBatchAttach> queryable = Kily.Set<EnterpriseProductionBatchAttach>().Where(t => t.IsDelete == false && t.ProBatchId == Id);
            var entity = queryable.FirstOrDefault();
            if (entity == null)
            {
                IQueryable<EnterpriseProductionBatch> queryables = Kily.Set<EnterpriseProductionBatch>().Where(t => t.IsDelete == false && t.Id == Id);
                IQueryable<EnterpriseProductSeries> Series = Kily.Set<EnterpriseProductSeries>().Where(t => t.IsDelete == false);
                IQueryable<EnterpriseTarget> Target = Kily.Set<EnterpriseTarget>().Where(t => t.IsDelete == false);
                string TargetId = queryables.Join(Series, t => t.SeriesId, y => y.Id, (t, y) => new { t, y }).Select(t => t.y.TargetId).FirstOrDefault();
                var data = Target.Where(t => TargetId.Contains(t.Id.ToString())).Select(t => new ResponseEnterpriseProductionBatchAttach()
                {
                    TargetName = t.TargetName,
                    TargetUnit = t.TargetUnit,
                    TargetValue = t.TargetValue
                }).ToList();
                return data;
            }
            else
            {
                var data = queryable.Select(t => new ResponseEnterpriseProductionBatchAttach()
                {
                    TargetName = t.TargetName,
                    TargetUnit = t.TargetUnit,
                    TargetValue = t.TargetValue,
                    Id = t.Id,
                    Result = t.Result,
                    ResultTime = t.ResultTime,
                    Manager = t.Manager
                }).ToList();
                return data;
            }
        }
        /// <summary>
        /// 编辑对比指标
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditProBatchAttach(RequestEnterpriseProductionBatchAttach Param)
        {
            EnterpriseProductionBatchAttach Attach = Param.MapToEntity<EnterpriseProductionBatchAttach>();
            return Insert(Attach) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        #endregion
        #region 设施管理
        /// <summary>
        /// 设施列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseFacilities> GetFacList()
        {
            IQueryable<EnterpriseFacilities> queryable = Kily.Set<EnterpriseFacilities>().Where(t => t.IsDelete == false);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseFacilities()
            {
                Id = t.Id,
                WorkShopName = t.WorkShopName
            }).ToList();
            return data;
        }
        /// <summary>
        /// 设施分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseFacilities> GetFacPage(PageParamList<RequestEnterpriseFacilities> pageParam)
        {
            IQueryable<EnterpriseFacilities> queryable = Kily.Set<EnterpriseFacilities>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.WorkShopName))
                queryable = queryable.Where(t => t.WorkShopName.Contains(pageParam.QueryParam.WorkShopName));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseFacilities()
            {
                Id = t.Id,
                GetWater = t.GetWater,
                Environment = t.Environment,
                Light = t.Light,
                Waste = t.Waste,
                WaterOut = t.WaterOut,
                Wind = t.Wind,
                WorkShopName = t.WorkShopName
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 设施附加分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseFacilitiesAttach> GetFacAttachPage(PageParamList<RequestEnterpriseFacilitiesAttach> pageParam)
        {
            IQueryable<EnterpriseFacilitiesAttach> queryable = Kily.Set<EnterpriseFacilitiesAttach>().Where(t => t.FacId == pageParam.QueryParam.FacId).OrderByDescending(t => t.CreateTime);
            var data = queryable.Select(t => new ResponseEnterpriseFacilitiesAttach()
            {
                Id = t.Id,
                DisinfectionName = t.DisinfectionName,
                CleanTime = t.CleanTime,
                HandlerUser = t.HandlerUser
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        #endregion
        #endregion

        #region 产品管理
        #region 产品列表
        /// <summary>
        /// 产品分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseGoods> GetGoodsPage(PageParamList<RequestEnterpriseGoods> pageParam)
        {
            IQueryable<EnterpriseGoods> queryable = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseProductSeries> queryables = Kily.Set<EnterpriseProductSeries>().Where(t => t.IsDelete == false);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.ProductName))
                queryable = queryable.Where(t => t.ProductName.Contains(pageParam.QueryParam.ProductName));
            var data = queryable.OrderByDescending(t => t.CreateTime).GroupJoin(queryables, t => t.ProductSeriesId, x => x.Id, (t, x) => new ResponseEnterpriseGoods()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                Spec = t.Spec,
                ProductSeriesName = x.FirstOrDefault().SeriesName,
                ExpiredDate = t.ExpiredDate,
                ProductName = t.ProductName,
                ProductType = t.ProductType,
                Unit = t.Unit
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveGoods(Guid Id)
        {
            return Delete<EnterpriseGoods>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 编辑产品
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditGoods(RequestEnterpriseGoods Param)
        {
            Param.AuditType = AuditEnum.WaitAduit;
            EnterpriseGoods Goods = Param.MapToEntity<EnterpriseGoods>();
            if (Param.Id != Guid.Empty)
                return Update<EnterpriseGoods, RequestEnterpriseGoods>(Goods, Param) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return Insert<EnterpriseGoods>(Goods) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 获取产品详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseEnterpriseGoods GetGoodsDetail(Guid Id)
        {
            IQueryable<EnterpriseGoods> queryable = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false).Where(t => t.Id == Id);
            IQueryable<EnterpriseProductSeries> queryables = Kily.Set<EnterpriseProductSeries>().Where(t => t.IsDelete == false);
            var data = queryable.OrderByDescending(t => t.CreateTime).Join(queryables, t => t.ProductSeriesId, x => x.Id, (t, x) => new ResponseEnterpriseGoods()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                ProductSeriesId = t.ProductSeriesId,
                Spec = t.Spec,
                ProductSeriesName = x.SeriesName,
                ExpiredDate = t.ExpiredDate,
                ProductName = t.ProductName,
                ProductType = t.ProductType,
                Unit = t.Unit
            }).AsNoTracking().FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 产品下拉
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseGoods> GetGoodsList()
        {
            IQueryable<EnterpriseGoods> queryable = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var Temp = queryable.Join(Kily.Set<EnterpriseProductSeries>(), x => x.ProductSeriesId, t => t.Id, (x, t) => new EnterpriseGoods
            {
                Id = x.Id,
                CompanyId = t.Id
            }).Join(Kily.Set<EnterpriseProductionBatch>(), y => y.CompanyId, p => p.SeriesId, (y, p) => new EnterpriseGoods
            {
                Id = y.Id,
                Spec = p.BatchNo
            }).AsNoTracking().ToList();
            var data = queryable.Select(t => new ResponseEnterpriseGoods()
            {
                Id = t.Id,
                ProductName = t.ProductName,
                Specs = t.Spec,
                Spec = Temp.Where(x => x.Id == t.Id).Select(x => x.Spec).FirstOrDefault()
            }).AsNoTracking().ToList();
            return data;
        }
        #endregion
        #region 产品仓库
        /// <summary>
        /// 编辑说明书
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditExplanation(RequestEnterpriseGoodsStock Param)
        {
            EnterpriseGoodsStock stock = Kily.Set<EnterpriseGoodsStock>().Where(t => t.Id == Param.Id).FirstOrDefault();
            stock.Explanation = Param.Explanation;
            return UpdateField(stock, "Explanation") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }
        /// <summary>
        ///提交产品审核
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string AuditGoods(RequestEnterpriseGoodsStock Param)
        {
            EnterpriseGoodsStock stock = Kily.Set<EnterpriseGoodsStock>().Where(t => t.Id == Param.Id).FirstOrDefault();
            EnterpriseGoods goods = Kily.Set<EnterpriseGoods>().Where(t => t.Id == stock.GoodsId).FirstOrDefault();
            goods.AuditType = AuditEnum.AuditLoading;
            stock.ImgUrl = Param.ImgUrl;
            stock.Remark = Param.Remark;
            IList<String> Fields = new List<String> { "ImgUrl", "Remark" };
            return (UpdateField(stock, null, Fields) && UpdateField(goods, "AuditType")) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }
        /// <summary>
        /// 产品入库分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseGoodsStock> GetGoodsStockPage(PageParamList<RequestEnterpriseGoodsStock> pageParam)
        {
            IQueryable<EnterpriseGoods> Goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseGoodsStock> Stock = Kily.Set<EnterpriseGoodsStock>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseProductionBatch> Batch = Kily.Set<EnterpriseProductionBatch>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseNote> Note = Kily.Set<EnterpriseNote>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseBuyer> Buyer = Kily.Set<EnterpriseBuyer>().Where(t => t.IsDelete == false);
            IQueryable<ResponseEnterpriseMaterial> Material = Kily.Set<EnterpriseMaterialStockAttach>().Where(t => t.IsDelete == false)
                .Join(Kily.Set<EnterpriseMaterialStock>().Where(t => t.IsDelete == false), t => t.MaterialStockId, x => x.Id, (t, x) => new { x.BatchNo })
                .Join(Kily.Set<EnterpriseMaterial>().Where(t => t.IsDelete == false), p => p.BatchNo, y => y.BatchNo, (p, y) => new ResponseEnterpriseMaterial
                {
                    Id = y.Id,
                    MaterName = y.MaterName,
                }).AsNoTracking();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.GoodsName))
                Goods = Goods.Where(t => t.ProductName.Contains(pageParam.QueryParam.GoodsName));
            if (CompanyInfo() != null)
            {
                Stock = Stock.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
                var Temp = Stock.OrderByDescending(t => t.CreateTime).Join(Goods, t => t.GoodsId, x => x.Id, (t, x) => new { t, x }).AsNoTracking();
                if (CompanyInfo().CompanyType == CompanyEnum.Plant || CompanyInfo().CompanyType == CompanyEnum.Culture)
                    return Temp.GroupJoin(Note, p => p.t.GrowNoteId, o => o.Id, (p, o) => new ResponseEnterpriseGoodsStock()
                    {
                        Id = p.t.Id,
                        CompanyId = p.t.CompanyId,
                        GoodsName = p.x.ProductName,
                        GoodsBatchNo = p.t.GoodsBatchNo,
                        StockType = p.t.StockType,
                        InStockNum = p.t.InStockNum,
                        ProBatch = o.FirstOrDefault().BatchNo,
                        GoodsId = p.x.Id,
                        ImgUrl = p.t.ImgUrl,
                        IsBindBoxCode = p.t.IsBindBoxCode,
                        Manager = p.t.Manager,
                        CheckGoodsId = p.t.CheckGoodsId,
                        AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(p.x.AuditType),
                        MaterialList = Material.ToList()
                    }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
                if (CompanyInfo().CompanyType == CompanyEnum.Production)
                    return Temp.GroupJoin(Batch, p => p.t.BatchId, o => o.Id, (p, o) => new ResponseEnterpriseGoodsStock()
                    {
                        Id = p.t.Id,
                        CompanyId = p.t.CompanyId,
                        GoodsName = p.x.ProductName,
                        GoodsBatchNo = p.t.GoodsBatchNo,
                        StockType = p.t.StockType,
                        IsBindBoxCode = p.t.IsBindBoxCode,
                        InStockNum = p.t.InStockNum,
                        ProBatch = o.FirstOrDefault().BatchNo,
                        GoodsId = p.x.Id,
                        ImgUrl = p.t.ImgUrl,
                        Manager = p.t.Manager,
                        CheckGoodsId = p.t.CheckGoodsId,
                        MaterialId = o.FirstOrDefault().MaterialId,
                        AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(p.x.AuditType),
                        MaterialList = Material.ToList()
                    }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
                if (CompanyInfo().CompanyType == CompanyEnum.Circulation)
                    return Temp.GroupJoin(Buyer, p => p.t.BuyId, o => o.Id, (p, o) => new ResponseEnterpriseGoodsStock()
                    {
                        Id = p.t.Id,
                        CompanyId = p.t.CompanyId,
                        GoodsName = p.x.ProductName,
                        GoodsBatchNo = p.t.GoodsBatchNo,
                        StockType = p.t.StockType,
                        InStockNum = p.t.InStockNum,
                        ProBatch = o.FirstOrDefault().BatchNo,
                        GoodsId = p.x.Id,
                        ImgUrl = p.t.ImgUrl,
                        IsBindBoxCode = p.t.IsBindBoxCode,
                        CheckGoodsId = p.t.CheckGoodsId,
                        Manager = p.t.Manager,
                        AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(p.x.AuditType),
                        MaterialList = Material.ToList()
                    }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            }
            else
            {
                Stock = Stock.Where(t => t.CompanyId == CompanyUser().Id);
                var Temp = Stock.OrderByDescending(t => t.CreateTime).Join(Goods, t => t.GoodsId, x => x.Id, (t, x) => new { t, x }).AsNoTracking();
                if (CompanyUser().CompanyType == CompanyEnum.Plant || CompanyUser().CompanyType == CompanyEnum.Culture)
                    return Temp.GroupJoin(Note, p => p.t.GrowNoteId, o => o.Id, (p, o) => new ResponseEnterpriseGoodsStock()
                    {
                        Id = p.t.Id,
                        CompanyId = p.t.CompanyId,
                        GoodsName = p.x.ProductName,
                        GoodsBatchNo = p.t.GoodsBatchNo,
                        StockType = p.t.StockType,
                        IsBindBoxCode = p.t.IsBindBoxCode,
                        InStockNum = p.t.InStockNum,
                        ProBatch = o.FirstOrDefault().BatchNo,
                        GoodsId = p.x.Id,
                        ImgUrl = p.t.ImgUrl,
                        CheckGoodsId = p.t.CheckGoodsId,
                        Manager = p.t.Manager,
                        AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(p.x.AuditType),
                        MaterialList = Material.ToList()
                    }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
                if (CompanyUser().CompanyType == CompanyEnum.Production)
                    return Temp.GroupJoin(Batch, p => p.t.BatchId, o => o.Id, (p, o) => new ResponseEnterpriseGoodsStock()
                    {
                        Id = p.t.Id,
                        CompanyId = p.t.CompanyId,
                        GoodsName = p.x.ProductName,
                        GoodsBatchNo = p.t.GoodsBatchNo,
                        StockType = p.t.StockType,
                        IsBindBoxCode = p.t.IsBindBoxCode,
                        InStockNum = p.t.InStockNum,
                        ImgUrl = p.t.ImgUrl,
                        ProBatch = o.FirstOrDefault().BatchNo,
                        GoodsId = p.x.Id,
                        MaterialId = o.FirstOrDefault().MaterialId,
                        Manager = p.t.Manager,
                        CheckGoodsId = p.t.CheckGoodsId,
                        AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(p.x.AuditType),
                        MaterialList = Material.ToList()
                    }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
                if (CompanyUser().CompanyType == CompanyEnum.Circulation)
                    return Temp.GroupJoin(Buyer, p => p.t.BuyId, o => o.Id, (p, o) => new ResponseEnterpriseGoodsStock()
                    {
                        Id = p.t.Id,
                        CompanyId = p.t.CompanyId,
                        GoodsName = p.x.ProductName,
                        GoodsBatchNo = p.t.GoodsBatchNo,
                        StockType = p.t.StockType,
                        ImgUrl = p.t.ImgUrl,
                        IsBindBoxCode = p.t.IsBindBoxCode,
                        InStockNum = p.t.InStockNum,
                        ProBatch = o.FirstOrDefault().BatchNo,
                        GoodsId = p.x.Id,
                        Manager = p.t.Manager,
                        CheckGoodsId = p.t.CheckGoodsId,
                        AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(p.x.AuditType),
                        MaterialList = Material.ToList()
                    }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            }
            return null;
        }
        /// <summary>
        /// 删除仓库中产品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveGoodsStock(Guid Id)
        {
            return Delete<EnterpriseGoodsStock>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 编辑产品仓库
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditGoodsStock(RequestEnterpriseGoodsStock Param)
        {
            EnterpriseGoodsStock Stock = Param.MapToEntity<EnterpriseGoodsStock>();
            return Insert<EnterpriseGoodsStock>(Stock) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 装箱管理
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditBoxing(RequestEnterpriseBoxing Param)
        {
            try
            {
                Param.ThingCode = Param.ThingCode.Replace("\r\n", ",");
                var Num = Param.ThingCode.Split(",").ToList();
                if (string.IsNullOrEmpty(Num[Num.Count - 1]))
                    Num.RemoveAt(Num.Count - 1);
                if (Num.Count > 100)
                    return "装箱数量最大支持每箱100个物件";
                Param.BoxCount = Num.Count.ToString();
                Param.BoxCodeSort = Convert.ToInt64(Param.BoxCode.Split("B")[1].Substring(0, 12));
                EnterpriseBoxing Box = Param.MapToEntity<EnterpriseBoxing>();
                EnterpriseGoodsStock Stock = Kily.Set<EnterpriseGoodsStock>().Where(t => t.GoodsBatchNo == Param.StockBatchNo).AsNoTracking().FirstOrDefault();
                EnterpriseGoodsStockAttach StockAttach = Kily.Set<EnterpriseGoodsStockAttach>().Where(t => t.GoodsBatchNo == Param.StockBatchNo).AsNoTracking().FirstOrDefault();
                var TotalSum = Kily.Set<EnterpriseBoxing>().Where(t => t.StockBatchNo == Param.StockBatchNo).Sum(t => Convert.ToInt32(t.BoxCount));
                //判断是否使用了箱码
                var TempBox = Kily.Set<EnterpriseBoxing>().Where(t => t.BoxCode.Contains(Box.BoxCodeSort.ToString())).FirstOrDefault();
                if (TempBox != null)
                    return "该箱码已经使用!";
                if (Stock != null)
                {
                    var Tag = Kily.Set<EnterpriseTagAttach>().Where(t => t.StockNo == Param.StockBatchNo).AsNoTracking().ToList();
                    if (Num.Any(t => t.Contains("W") || t.Contains("P")))
                        foreach (var item in Num)
                        {
                            long Temp = 0;
                            string Host = string.Empty;
                            if (item.Contains("W"))
                            {
                                Temp = Convert.ToInt64(item.Split("W")[1].Substring(0, 12));
                                Host = item.Split("W")[0] + "W";
                            }
                            else
                            {
                                Temp = Convert.ToInt64(item.Split("P")[1].Substring(0, 12));
                                Host = item.Split("P")[0] + "P";
                            }
                            var TempEntity = Tag.Where(t => t.StarSerialNo <= Temp || t.EndSerialNo >= Temp).FirstOrDefault();
                            if (TempEntity == null)
                                return $"{Host + Temp}溯源号段不在此批次中";
                        }
                    if (TotalSum + Num.Count > Stock.InStockNum)
                        return "超出库存!";
                    else if (TotalSum + Num.Count == Stock.InStockNum)
                    {
                        Stock.IsBindBoxCode = true;
                        UpdateField(Stock, "IsBindBoxCode");
                    }
                }
                else
                {
                    var InStockNo = Kily.Set<EnterpriseGoodsStock>().Where(t => t.Id == StockAttach.StockId).Select(t => t.GoodsBatchNo).AsNoTracking().FirstOrDefault();
                    var Tag = Kily.Set<EnterpriseTagAttach>().Where(t => t.StockNo == InStockNo).AsNoTracking().ToList();
                    if (Num.Any(t => t.Contains("W") || t.Contains("P")))
                        foreach (var item in Num)
                        {
                            long Temp = 0;
                            string Host = string.Empty;
                            if (item.Contains("W"))
                            {
                                Temp = Convert.ToInt64(item.Split("W")[1].Substring(0, 12));
                                Host = item.Split("W")[0] + "W";
                            }
                            else
                            {
                                Temp = Convert.ToInt64(item.Split("P")[1].Substring(0, 12));
                                Host = item.Split("P")[0] + "P";
                            }
                            var TempEntity = Tag.Where(t => t.StarSerialNo <= Temp || t.EndSerialNo >= Temp).FirstOrDefault();
                            if (TempEntity == null)
                                return $"{Host + Temp}溯源号段不在此批次中";
                        }
                    StockAttach.BoxCodeNo = Param.BoxCode;
                    StockAttach.BoxCount = Param.BoxCount;
                    List<String> Field = new List<String> { "BoxCodeNo", "BoxCount" };
                    UpdateField(StockAttach, null, Field);
                }
                return Insert(Box) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
            catch
            {
                return "请填入正确的二维码格式 xxW13000...01X";
            }
        }
        /// <summary>
        /// 绑定二维码
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string BindTarget(RequestEnterpriseTagAttach Param)
        {
            EnterpriseTagAttach TagAttach = Param.MapToEntity<EnterpriseTagAttach>();
            int Count = (int)(Param.EndSerialNo - Param.StarSerialNo) + 1;
            TagAttach.UseNum = Count;
            if (string.IsNullOrEmpty(Param.TagBatchNo))
                return "请选择正确批次";
            if (TagAttach.TagType != "1" && TagAttach.TagType != "2" && TagAttach.TagType != "3")
                return ServiceMessage.HANDLEFAIL;
            if (TagAttach.TagType.Equals("1"))
            {
                EnterpriseVeinTag data = Kily.Set<EnterpriseVeinTag>().Where(t => t.IsDelete == false).Where(t => t.BatchNo == Param.TagBatchNo).FirstOrDefault();
                int SumCount = Kily.Set<EnterpriseTagAttach>().Where(t => t.IsDelete == false).Where(t => t.TagBatchNo == Param.TagBatchNo).Select(t => t.UseNum).Sum();
                if (data.IsAccept)
                    return "请先签收";
                data.UseNum += Count;
                if (data.UseNum - data.TotalNo < 0)
                {
                    return "纹理二维码数量不足";
                }
                else if (Param.StarSerialNo - (data.StarSerialNo + SumCount) < 0)
                {
                    return $"号段不匹配！新的起始号段为{data.StarSerialNo + SumCount }";
                }
                else
                {
                    UpdateField<EnterpriseVeinTag>(data, "UseNum");
                    return Insert<EnterpriseTagAttach>(TagAttach) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
                }
            }
            else
            {
                IQueryable<EnterpriseTag> queryable = Kily.Set<EnterpriseTag>().Where(t => t.IsDelete == false).Where(t => t.BatchNo == Param.TagBatchNo);
                EnterpriseTag data = null;
                if (TagAttach.TagType.Equals("2"))
                    data = queryable.Where(t => t.TagType == TagEnum.OneThing).AsNoTracking().FirstOrDefault();
                if (TagAttach.TagType.Equals("3"))
                    data = queryable.Where(t => t.TagType == TagEnum.OneBrand).AsNoTracking().FirstOrDefault();
                int SumCount = Kily.Set<EnterpriseTagAttach>().Where(t => t.IsDelete == false).Where(t => t.TagBatchNo == Param.TagBatchNo).Select(t => t.UseNum).Sum();
                data.UseNum += Count;
                data.TotalNo = data.TotalNo - Count;
                if (data.TotalNo < 0)
                {
                    return "当前批次号段不足！";
                }
                else if (Param.StarSerialNo - (data.StarSerialNo + SumCount) < 0)
                {
                    return $"号段不匹配！新的起始号段为{data.StarSerialNo + SumCount }";
                }
                else
                {
                    IList<string> fields = new List<string>
                {
                    "UseNum",
                    "TotalNo"
                };
                    UpdateField<EnterpriseTag>(data, null, fields);
                    return Insert<EnterpriseTagAttach>(TagAttach) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
                }
            }
        }
        /// <summary>
        /// 产品出库分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseGoodsStockAttach> GetGoodsStockAttachPage(PageParamList<RequestEnterpriseGoodsStockAttach> pageParam)
        {
            IQueryable<EnterpriseGoodsStockAttach> queryable = Kily.Set<EnterpriseGoodsStockAttach>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<EnterpriseGoodsStock> Stock = Kily.Set<EnterpriseGoodsStock>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<EnterpriseGoods> Goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.GoodsName))
                Goods = Goods.Where(t => t.ProductName.Contains(pageParam.QueryParam.GoodsName));
            if (CompanyInfo() != null)
                Stock = Stock.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                Stock = Stock.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Join(Stock, t => t.StockId, x => x.Id, (t, x) => new { t, x })
                .Join(Goods, o => o.x.GoodsId, p => p.Id, (o, p) => new ResponseEnterpriseGoodsStockAttach()
                {
                    Id = o.t.Id,
                    GoodsBatchNo = o.t.GoodsBatchNo,
                    OutStockUser = o.t.OutStockUser,
                    OutStockType = o.t.OutStockType,
                    StockBatch = o.x.GoodsBatchNo,
                    BoxCount = o.t.BoxCount,
                    GoodsName = p.ProductName,
                    OutStockNum = o.t.OutStockNum,
                    StockEx = o.x.InStockNum
                }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 出库编辑
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditStockAttach(RequestEnterpriseGoodsStockAttach Param)
        {
            if (Param.CodeType == 1)
            {
                if (!string.IsNullOrEmpty(Param.BoxCodeNo))
                {
                    Param.BoxCodeNo = Param.BoxCodeNo.Replace("\r\n", ",");
                    var Num = Param.BoxCodeNo.Split(",").ToList();
                    if (string.IsNullOrEmpty(Num[Num.Count - 1]))
                        Num.RemoveAt(Num.Count - 1);
                    Param.BoxCount = Num.Count.ToString();
                    Param.OutStockNum = Kily.Set<EnterpriseBoxing>().Where(t => Num.Contains(t.BoxCode)).Select(t => t.ThingCode).ToList().SelectMany(t => t.Split(",")).Count();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Param.SourceCodeNo))
                {
                    Param.SourceCodeNo = Param.SourceCodeNo.Replace("\r\n", ",");
                    var Num = Param.SourceCodeNo.Split(",").ToList();
                    if (string.IsNullOrEmpty(Num[Num.Count - 1]))
                        Num.RemoveAt(Num.Count - 1);
                    Param.OutStockNum = Num.Count;
                }
            }
            if (Param.OutStockNum <= 0)
                return "出库数量必须大于0";
            EnterpriseGoodsStock stock = Kily.Set<EnterpriseGoodsStock>().Where(t => t.IsDelete == false)
                .Where(t => t.Id == Param.StockId).AsNoTracking().FirstOrDefault();
            if (stock.InStockNum < Param.OutStockNum)
                return "当前库存少于出库量";
            stock.InStockNum -= Param.OutStockNum;
            EnterpriseGoodsStockAttach Attach = Param.MapToEntity<EnterpriseGoodsStockAttach>();
            UpdateField(stock, "InStockNum");
            return Insert<EnterpriseGoodsStockAttach>(Attach) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 删除出库
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveGoodsStockAttach(Guid Id)
        {
            return Delete(ExpressionExtension.GetExpression<EnterpriseGoodsStockAttach>("Id", Id, ExpressionEnum.Equals)) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 获取出库批次编号
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseGoodsStockAttach> GetStockOutNoList()
        {
            IQueryable<EnterpriseGoodsStockAttach> queryable = Kily.Set<EnterpriseGoodsStockAttach>().Where(t => t.IsDelete == false).AsNoTracking();
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            return queryable.Select(t => new ResponseEnterpriseGoodsStockAttach()
            {
                GoodsBatchNo = t.GoodsBatchNo,
                GoodsName = Kily.Set<EnterpriseGoods>()
                .Where(o => o.Id == Kily.Set<EnterpriseGoodsStock>()
                .Where(x => x.Id == t.StockId).Select(x => x.GoodsId)
                .FirstOrDefault()).Select(o => o.ProductName).FirstOrDefault()
            }).ToList();
        }
        /// <summary>
        /// 添加产品检测
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string UpdateStockCheck(RequestEnterpriseGoodsStock Param)
        {
            EnterpriseGoodsStock Stock = Kily.Set<EnterpriseGoodsStock>().Where(t => t.Id == Param.Id).AsNoTracking().FirstOrDefault();
            Stock.CheckGoodsId = Param.CheckGoodsId;
            return UpdateField(Stock, "CheckGoodsId") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }
        #endregion
        #region 仓库类型
        /// <summary>
        /// 仓库类型分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseStockType> GetStockTypePage(PageParamList<RequestEnterpriseStockType> pageParam)
        {
            IQueryable<EnterpriseStockType> queryable = Kily.Set<EnterpriseStockType>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.StockName))
                queryable = queryable.Where(t => t.StockName.Contains(pageParam.QueryParam.StockName));
            var data = queryable.Select(t => new ResponseEnterpriseStockType()
            {
                Id = t.Id,
                StockName = t.StockName,
                SaveH2 = t.SaveH2,
                SaveTemp = t.SaveTemp,
                SaveType = t.SaveType,
                StockNo = t.StockNo,
                StockType = t.StockType,
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 仓库类型列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseStockType> GetStockTypeList(String Key)
        {
            IQueryable<EnterpriseStockType> queryable = Kily.Set<EnterpriseStockType>()
                .Where(t => t.IsDelete == false).Where(t => t.StockType.Equals(Key))
                .OrderByDescending(t => t.CreateTime);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseStockType()
            {
                Id = t.Id,
                StockName = t.StockName
            }).ToList();
            return data;
        }
        /// <summary>
        /// 删除仓库类型
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveStockType(Guid Id)
        {
            return Delete<EnterpriseStockType>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 编辑仓库类型
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditStockType(RequestEnterpriseStockType Param)
        {
            EnterpriseStockType stockType = Param.MapToEntity<EnterpriseStockType>();
            return Insert(stockType) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        #endregion
        #endregion

        #region 品质管理
        #region 原料产品质检
        /// <summary>
        /// 原料质检分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseCheckMaterial> GetCheckMaterialPage(PageParamList<RequestEnterpriseCheckMaterial> pageParam)
        {
            IQueryable<EnterpriseCheckMaterial> queryable = Kily.Set<EnterpriseCheckMaterial>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<EnterpriseMaterial> queryables = Kily.Set<EnterpriseMaterial>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CheckName))
                queryable = queryable.Where(t => t.CheckName.Contains(pageParam.QueryParam.CheckName));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Join(queryables, t => t.MaterId, x => x.Id, (t, x) => new ResponseEnterpriseCheckMaterial()
            {
                Id = t.Id,
                CheckName = t.CheckName,
                MaterName = x.MaterName,
                CheckResult = t.CheckResult,
                CheckUint = t.CheckUint,
                CheckUser = t.CheckUser,
                CheckReport = t.CheckReport
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 编辑原料质检
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditCheckMaterial(RequestEnterpriseCheckMaterial Param)
        {
            EnterpriseCheckMaterial material = Param.MapToEntity<EnterpriseCheckMaterial>();
            return Insert(material) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 删除原料质检
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveCheckMaterial(Guid Id)
        {
            return Delete<EnterpriseCheckMaterial>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 原料质检列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseCheckMaterial> GetCheckMaterial()
        {
            IQueryable<EnterpriseCheckMaterial> queryable = Kily.Set<EnterpriseCheckMaterial>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseCheckMaterial()
            {
                Id = t.Id,
                CheckName = t.CheckName,
            }).ToList();
            return data;
        }
        /// <summary>
        /// 产品质检分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseCheckGoods> GetCheckGoodsPage(PageParamList<RequestEnterpriseCheckGoods> pageParam)
        {
            IQueryable<EnterpriseCheckGoods> queryable = Kily.Set<EnterpriseCheckGoods>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<EnterpriseProductionBatch> queryables = Kily.Set<EnterpriseProductionBatch>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseBuyer> buyers = Kily.Set<EnterpriseBuyer>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CheckName))
                queryable = queryable.Where(t => t.CheckName.Contains(pageParam.QueryParam.CheckName));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            if (queryables.ToList().Count() != 0)
            {
                var data = queryable.Join(queryables, t => t.GoodsId, x => x.Id, (t, x) => new ResponseEnterpriseCheckGoods()
                {
                    Id = t.Id,
                    CheckName = t.CheckName,
                    GoodsName = Kily.Set<EnterpriseProductSeries>().Where(o => o.Id == x.SeriesId).Select(o => o.SeriesName).FirstOrDefault(),
                    CheckResult = t.CheckResult,
                    CheckUint = t.CheckUint,
                    CheckUser = t.CheckUser,
                    CheckReport = t.CheckReport
                }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
                return data;
            }
            return queryable.Join(buyers, t => t.BuyerId, x => x.Id, (t, x) => new ResponseEnterpriseCheckGoods()
            {
                Id = t.Id,
                CheckName = t.CheckName,
                GoodsName = x.GoodName,
                CheckResult = t.CheckResult,
                CheckUint = t.CheckUint,
                CheckUser = t.CheckUser,
                CheckReport = t.CheckReport
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
        }
        /// <summary>
        /// 编辑产品质检
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditCheckGoods(RequestEnterpriseCheckGoods Param)
        {
            EnterpriseCheckGoods material = Param.MapToEntity<EnterpriseCheckGoods>();
            return Insert(material) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 删除产品质检
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveCheckGoods(Guid Id)
        {
            return Delete<EnterpriseCheckGoods>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 产品质检列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseCheckGoods> GetCheckGoodsList()
        {
            IQueryable<EnterpriseCheckGoods> queryable = Kily.Set<EnterpriseCheckGoods>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseCheckGoods()
            {
                Id = t.Id,
                CheckName = t.CheckName,
            }).ToList();
            return data;
        }
        #endregion
        #region 过期不合格处理
        /// <summary>
        /// 过期数据
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public Object GetExpiredPage(PageParamList<Object> pageParam)
        {
            IQueryable<EnterpriseGoods> queryable = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<EnterpriseGoodsStock> queryables = Kily.Set<EnterpriseGoodsStock>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Join(queryables, t => t.Id, x => x.GoodsId, (t, x) => new
            {
                t.Id,
                t.ProductType,
                x.GoodsBatchNo,
                t.ProductName,
                t.ExpiredDate,
                x.ProductTime,
                ExpiredTime = x.ProductTime.AddDays(Convert.ToInt32(t.ExpiredDate))
            }).Where(t => t.ExpiredTime.Subtract(DateTime.Now).TotalDays <= 7)
            .ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseInferiorExprired> GetInferiorExpriredPage(PageParamList<RequestEnterpriseInferiorExprired> pageParam)
        {
            IQueryable<EnterpriseInferiorExprired> queryable = Kily.Set<EnterpriseInferiorExprired>().Where(t => t.IsDelete == false).Where(t => t.InferiorExprired == pageParam.QueryParam.InferiorExprired).OrderByDescending(t => t.CreateTime);
            var Temp = Kily.Set<EnterpriseGoods>().Join(Kily.Set<EnterpriseProductSeries>(), x => x.ProductSeriesId, t => t.Id, (x, t) => new EnterpriseGoods
            {
                Id = x.Id,
                CompanyId = t.Id
            }).Join(Kily.Set<EnterpriseProductionBatch>(), y => y.CompanyId, p => p.SeriesId, (y, p) => new EnterpriseGoods
            {
                Id = y.Id,
                Spec = p.BatchNo
            }).AsNoTracking().ToList();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CustomName))
                queryable = queryable.Where(t => t.CustomName.Contains(pageParam.QueryParam.CustomName));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseInferiorExprired()
            {
                Id = t.Id,
                CustomName = t.CustomName,
                InferName = t.InferName,
                InferType = t.InferType,
                HandleUser = t.HandleUser,
                InferNum = t.InferNum,
                HandleWays = t.HandleWays,
                HandleTime = t.HandleTime,
                HandleReason = t.HandleReason,
                BatchNo = pageParam.QueryParam.InferiorExprired != 1 ? null : Temp.Where(x => x.Id == t.InferId).Select(x => x.Spec).FirstOrDefault(),
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseEnterpriseInferiorExprired GetInferiorExpriredDetail(Guid Id)
        {
            IQueryable<EnterpriseInferiorExprired> queryable = Kily.Set<EnterpriseInferiorExprired>().Where(t => t.Id == Id).AsNoTracking();
            var data = queryable.Select(t => new ResponseEnterpriseInferiorExprired()
            {
                Id = t.Id,
                CustomName = t.CustomName,
                InferName = t.InferName,
                InferType = t.InferType,
                HandleUser = t.HandleUser,
                HandleWays = t.HandleWays,
                HandleTime = t.HandleTime,
                HandleReason = t.HandleReason,
                InferId = t.InferId
            }).FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 删除过期不合格处理
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveInferiorExprired(Guid Id)
        {
            return Delete<EnterpriseInferiorExprired>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 编辑过期不合格处理
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditInferiorExprired(RequestEnterpriseInferiorExprired Param)
        {
            EnterpriseInferiorExprired exprired = Param.MapToEntity<EnterpriseInferiorExprired>();
            if (Param.Id == Guid.Empty)
                return Insert(exprired) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return ServiceMessage.HANDLESUCCESS;
        }
        #endregion
        #region 召回处理
        /// <summary>
        /// 召回分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseRecover> GetRecoverPage(PageParamList<RequestEnterpriseRecover> pageParam)
        {
            IQueryable<EnterpriseRecover> queryable = Kily.Set<EnterpriseRecover>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.RecoverGoodsName))
                queryable = queryable.Where(t => t.RecoverGoodsName.Contains(pageParam.QueryParam.RecoverGoodsName));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseEnterpriseRecover()
            {
                Id = t.Id,
                RecoverStarTime = t.RecoverStarTime,
                RecoverEndTime = t.RecoverEndTime,
                RecoverGoodsName = t.RecoverGoodsName,
                RecoverReason = t.RecoverReason,
                States = t.States,
                RecoverNum = t.RecoverNum
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseEnterpriseRecover GetRecoverDetail(Guid Id)
        {
            IQueryable<EnterpriseRecover> queryable = Kily.Set<EnterpriseRecover>().Where(t => t.IsDelete == false).Where(t => t.Id == Id);
            var data = queryable.Select(t => new ResponseEnterpriseRecover()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                RecoverStarTime = t.RecoverStarTime,
                RecoverEndTime = t.RecoverEndTime,
                RecoverGoodsName = t.RecoverGoodsName,
                RecoverReason = t.RecoverReason,
                RecoverNum = t.RecoverNum,
                HandleTime = t.HandleTime,
                HandleUser = t.HandleUser,
                HandleWays = t.HandleWays
            }).FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 删除召回
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveRecover(Guid Id)
        {
            return Delete<EnterpriseRecover>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        #endregion
        #endregion

        #region 物流管理
        #region 装车管理
        /// <summary>
        /// 装车分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseGoodsPackage> GetPackagePage(PageParamList<RequestEnterpriseGoodsPackage> pageParam)
        {
            IQueryable<EnterpriseGoodsPackage> Package = Kily.Set<EnterpriseGoodsPackage>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<EnterpriseGoodsStockAttach> Attach = Kily.Set<EnterpriseGoodsStockAttach>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseGoodsStock> Stock = Kily.Set<EnterpriseGoodsStock>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseGoods> Goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.PackageNo))
                Package = Package.Where(t => t.PackageNo.Contains(pageParam.QueryParam.PackageNo));
            if (CompanyInfo() != null)
                Package = Package.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                Package = Package.Where(t => t.CompanyId == CompanyUser().Id);
            var data = Package.Select(t => new ResponseEnterpriseGoodsPackage()
            {
                Id = t.Id,
                PackageNo = t.PackageNo,
                ProductName = string.Join(",", Goods.Where(p =>
                 Stock.Where(o =>
                 Attach.Where(x =>
                 t.ProductOutStockNo.Contains(x.GoodsBatchNo))
                 .Select(x => x.StockId).Contains(o.Id))
                 .Select(o => o.GoodsId).Contains(p.Id))
                      .Select(p => p.ProductName).ToList()),
                ProductOutStockNo = t.ProductOutStockNo,
                PackageTime = t.PackageTime,
                PackageNum = t.PackageNum,
                Manager = t.Manager,
                IsSend = Kily.Set<EnterpriseLogistics>().Where(x => x.PackageNo == t.PackageNo).AsNoTracking().FirstOrDefault() != null ? true : false
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 编辑装车
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditGoodsPackage(RequestEnterpriseGoodsPackage Param)
        {
            Param.BoxCode = Param.BoxCode.Replace("\r\n", ",");
            var Num = Param.BoxCode.Split(",").ToList();
            if (string.IsNullOrEmpty(Num[Num.Count - 1]))
                Num.RemoveAt(Num.Count - 1);
            Param.PackageNum = Num.Count;
            var data = Kily.Set<EnterpriseGoodsStockAttach>().Where(t => Param.ProductOutStockNo.Contains(t.GoodsBatchNo)).Select(t => t.BoxCodeNo).ToList();
            foreach (var item in data)
            {
                if (!Param.BoxCode.Contains(item))
                    return "当前批次不包括装箱码：" + item;
            }
            EnterpriseGoodsPackage package = Param.MapToEntity<EnterpriseGoodsPackage>();
            if (Param.Id == Guid.Empty)
                return Insert(package) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return Update(package, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }
        /// <summary>
        /// 装车详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseEnterpriseGoodsPackage GetGoodsPackageDetail(Guid Id)
        {
            IQueryable<EnterpriseGoodsPackage> Package = Kily.Set<EnterpriseGoodsPackage>().Where(t => t.Id == Id).AsNoTracking();
            var data = Package.Select(t => new ResponseEnterpriseGoodsPackage()
            {
                Id = t.Id,
                PackageNo = t.PackageNo,
                ProductOutStockNo = t.ProductOutStockNo,
                PackageTime = t.PackageTime,
                PackageNum = t.PackageNum,
                BoxCode = t.BoxCode,
                Manager = t.Manager
            }).FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 删除装车
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveGoodsPackge(Guid Id)
        {
            return Delete<EnterpriseGoodsPackage>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 装车批次号
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseGoodsPackage> GetPackagesList()
        {
            IQueryable<EnterpriseGoodsPackage> queryable = Kily.Set<EnterpriseGoodsPackage>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<EnterpriseGoodsStockAttach> Attach = Kily.Set<EnterpriseGoodsStockAttach>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseGoodsStock> Stock = Kily.Set<EnterpriseGoodsStock>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseGoods> Goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseLogistics> Logistics = Kily.Set<EnterpriseLogistics>().Where(t => t.IsDelete == false);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseGoodsPackage()
            {
                PackageNo = t.PackageNo,
                ProductName = string.Join("|", Goods.Where(p =>
                      Stock.Where(o =>
                      Attach.Where(x =>
                      t.ProductOutStockNo.Contains(x.GoodsBatchNo))
                      .Select(x => x.StockId).Contains(o.Id))
                      .Select(o => o.GoodsId).Contains(p.Id))
                      .Select(p => p.ProductName).ToList())
            }).ToList();
            IList<ResponseEnterpriseGoodsPackage> Result = new List<ResponseEnterpriseGoodsPackage>();
            data.ForEach(t =>
            {
                var temp = Logistics.Where(p => p.PackageNo == t.PackageNo).AsNoTracking().Select(p => p.Id).Count();
                if (temp == 0)
                    Result.Add(t);
            });
            return Result;
        }
        #endregion
        #region 发货收货
        /// <summary>
        /// 发货收货分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseLogistics> GetLogisticsPage(PageParamList<RequestEnterpriseLogistics> pageParam)
        {
            IQueryable<EnterpriseLogistics> queryable = Kily.Set<EnterpriseLogistics>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.GoodsName))
                queryable = queryable.Where(t => t.GoodsName.Contains(pageParam.QueryParam.GoodsName));
            if (pageParam.QueryParam.GainId != Guid.Empty)
                queryable = queryable.Where(t => t.GainId == pageParam.QueryParam.GainId);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseLogistics()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                GainId = t.GainId,
                SendAddress = t.SendAddress,
                GoodsName = t.GoodsName,
                BatchNo = t.BatchNo,
                LinkPhone = t.LinkPhone,
                Address = t.Address,
                WayBill = t.WayBill,
                SendGoodsNum = t.SendGoodsNum,
                GainUser = t.GainUser,
                SendTime = t.SendTime,
                Flag = t.Flag,
                Traffic = t.Traffic,
                PackageNo = t.PackageNo,
                TransportWay = t.TransportWay,
                CorrectError = t.Error / ((t.Error + t.Correct) == 0 ? 1 : (t.Error + t.Correct))
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 串货详情
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseScanCodeInfo> GetLogisticsErrorPage(PageParamList<RequestEnterpriseScanCodeInfo> pageParam)
        {
            var data = Kily.Set<EnterpriseScanCodeInfo>()
                 .Where(t => t.ScanPackageNo.Contains(pageParam.QueryParam.ScanPackageNo))
                 .OrderByDescending(t => t.CreateTime).Select(t => new ResponseEnterpriseScanCodeInfo
                 {
                     ScanAddress = t.ScanAddress,
                     ScanCode = t.ScanCode,
                     ScanIP = t.ScanIP,
                     ScanNum = t.ScanNum,
                     ScanPackageNo = t.ScanPackageNo
                 }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 获取收获地址
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseLogistics> GetReceipt()
        {
            IQueryable<EnterpriseLogistics> queryable = Kily.Set<EnterpriseLogistics>().Where(t => t.IsDelete == false);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            return queryable.AsNoTracking().Select(t => new ResponseEnterpriseLogistics()
            {
                GainUser = t.GainUser,
                LinkPhone = t.LinkPhone,
                Address = t.Address
            }).ToList();
        }
        /// <summary>
        /// 编辑发货
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditLogistics(RequestEnterpriseLogistics Param)
        {
            IQueryable<EnterpriseGoodsPackage> queryable = Kily.Set<EnterpriseGoodsPackage>().Where(t => t.IsDelete == false);
            if (Param.PackageNo.Contains("_"))
            {
                int count = 0;
                if (!Param.PackageNo.Contains(","))
                {
                    Param.GoodsName = Param.PackageNo.Split("_")[1];
                    Param.PackageNo = Param.PackageNo.Split("_")[0];
                    Param.SendGoodsNum = queryable.Where(t => t.PackageNo == Param.PackageNo).Select(t => t.PackageNum).FirstOrDefault().ToString();
                }
                else
                {
                    Param.PackageNo.Split(",").ToList().ForEach(t =>
                    {
                        Param.GoodsName += t.Split("_")[1] + "|";
                        Param.PackageNo = t.Split("_")[0] + ",";
                        count += queryable.Where(x => x.PackageNo == Param.PackageNo).Select(x => x.PackageNum).FirstOrDefault();
                    });
                    Param.SendGoodsNum = count.ToString();
                }
            }
            EnterpriseLogistics logistics = Param.MapToEntity<EnterpriseLogistics>();
            return Insert<EnterpriseLogistics>(logistics) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 删除发货
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveLogistics(Guid Id)
        {
            return Delete<EnterpriseLogistics>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        #endregion
        #region 进货管理
        /// <summary>
        /// 进货分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseBuyer> GetBuyerPage(PageParamList<RequestEnterpriseBuyer> pageParam)
        {
            IQueryable<EnterpriseBuyer> queryable = Kily.Set<EnterpriseBuyer>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.GoodName))
                queryable = queryable.Where(t => t.GoodName.Contains(pageParam.QueryParam.GoodName));
            var data = queryable.Select(t => new ResponseEnterpriseBuyer()
            {
                Id = t.Id,
                GoodName = t.GoodName,
                Address = t.Address,
                BatchNo = t.BatchNo,
                Supplier = t.Supplier,
                ExpiredDate = t.ExpiredDate,
                GetGoodsTime = t.GetGoodsTime,
                ProTime = t.ProTime,
                Num = t.Num,
                Spec = t.Spec,
                CheckReport = t.CheckReport
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 编辑进货
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditBuyer(RequestEnterpriseBuyer Param)
        {
            EnterpriseBuyer buyer = Param.MapToEntity<EnterpriseBuyer>();
            return Insert<EnterpriseBuyer>(buyer) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 删除进货
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveBuyer(Guid Id)
        {
            return Delete<EnterpriseBuyer>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 进货列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseBuyer> GetBuyerList()
        {
            IQueryable<EnterpriseBuyer> queryable = Kily.Set<EnterpriseBuyer>().Where(t => t.IsDelete == false).AsNoTracking();
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseBuyer()
            {
                Id = t.Id,
                BatchNo = t.BatchNo,
                GoodName = t.GoodName
            }).ToList();
            return data;
        }
        #endregion
        #endregion

        #region 微信和支付宝调用
        /// <summary>
        /// 版本续费和升级使用支付宝支付
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public string AliPay(int Key, int? Value)
        {
            if (CompanyInfo() == null)
                return "请使用企业账户进行操作！";
            EnterpriseInfo info = Kily.Set<EnterpriseInfo>().Where(t => t.Id == CompanyInfo().Id).FirstOrDefault();
            RequestAliPayModel AliPayModel = new RequestAliPayModel();
            AliPayModel.OrderTitle = CompanyInfo().CompanyName + (Value == null ? "版本续费" : "版本升级");

            if ((Value == null ? info.Version : (SystemVersionEnum)(Value)) == SystemVersionEnum.Test)
            {
                info.TagCodeNum += ServiceMessage.TEST;
                if (info.CompanyType == CompanyEnum.Plant || info.CompanyType == CompanyEnum.Culture)
                    AliPayModel.Money = ConfigMoney.PlantAndCultureTest * Key;
                if (info.CompanyType == CompanyEnum.Production)
                    AliPayModel.Money = ConfigMoney.ProductionTest * Key;
                if (info.CompanyType == CompanyEnum.Circulation)
                    AliPayModel.Money = ConfigMoney.CirculationTest * Key;
            }
            if ((Value == null ? info.Version : (SystemVersionEnum)(Value)) == SystemVersionEnum.Base)
            {
                info.TagCodeNum += ServiceMessage.BASE;
                if (info.CompanyType == CompanyEnum.Plant || info.CompanyType == CompanyEnum.Culture)
                    AliPayModel.Money = ConfigMoney.PlantAndCultureBase * Key;
                if (info.CompanyType == CompanyEnum.Production)
                    AliPayModel.Money = ConfigMoney.ProductionBase * Key;
                if (info.CompanyType == CompanyEnum.Circulation)
                    AliPayModel.Money = ConfigMoney.CirculationBase * Key;
            }
            if ((Value == null ? info.Version : (SystemVersionEnum)(Value)) == SystemVersionEnum.Level)
            {
                info.TagCodeNum += ServiceMessage.LEVEL;
                if (info.CompanyType == CompanyEnum.Plant || info.CompanyType == CompanyEnum.Culture)
                    AliPayModel.Money = ConfigMoney.PlantAndCultureLv * Key;
                if (info.CompanyType == CompanyEnum.Production)
                    AliPayModel.Money = ConfigMoney.ProductionLv * Key;
                if (info.CompanyType == CompanyEnum.Circulation)
                    AliPayModel.Money = ConfigMoney.CirculationLv * Key;
            }
            if ((Value == null ? info.Version : (SystemVersionEnum)(Value)) == SystemVersionEnum.Enterprise)
            {
                info.TagCodeNum += ServiceMessage.ENTERPRISE;
                if (info.CompanyType == CompanyEnum.Plant || info.CompanyType == CompanyEnum.Culture)
                    AliPayModel.Money = ConfigMoney.PlantAndCultureEnterprise * Key;
                if (info.CompanyType == CompanyEnum.Production)
                    AliPayModel.Money = ConfigMoney.ProductionEnterprise * Key;
                if (info.CompanyType == CompanyEnum.Circulation)
                    AliPayModel.Money = ConfigMoney.CirculationEnterprise * Key;
            }
            return AliPayCore.Instance.WebPay(AliPayModel);
        }
        /// <summary>
        /// 版本续费和升级使用微信支付
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public string WxPay(int Key, int? Value)
        {
            if (CompanyInfo() == null)
                return "请使用企业账户进行操作！";
            EnterpriseInfo info = Kily.Set<EnterpriseInfo>().Where(t => t.Id == CompanyInfo().Id).FirstOrDefault();
            RequestWxPayModel WxPayModel = new RequestWxPayModel
            {
                OrderTitle = CompanyInfo().CompanyName + (Value == null ? "版本续费" : "版本升级")
            };
            if ((Value == null ? info.Version : (SystemVersionEnum)(Value)) == SystemVersionEnum.Test)
            {
                info.TagCodeNum += ServiceMessage.TEST;
                if (info.CompanyType == CompanyEnum.Plant || info.CompanyType == CompanyEnum.Culture)
                    WxPayModel.Money = ConfigMoney.PlantAndCultureTest * Key;
                if (info.CompanyType == CompanyEnum.Production)
                    WxPayModel.Money = ConfigMoney.ProductionTest * Key;
                if (info.CompanyType == CompanyEnum.Circulation)
                    WxPayModel.Money = ConfigMoney.CirculationTest * Key;
            }
            if ((Value == null ? info.Version : (SystemVersionEnum)(Value)) == SystemVersionEnum.Base)
            {
                info.TagCodeNum += ServiceMessage.BASE;
                if (info.CompanyType == CompanyEnum.Plant || info.CompanyType == CompanyEnum.Culture)
                    WxPayModel.Money = ConfigMoney.PlantAndCultureBase * Key;
                if (info.CompanyType == CompanyEnum.Production)
                    WxPayModel.Money = ConfigMoney.ProductionBase * Key;
                if (info.CompanyType == CompanyEnum.Circulation)
                    WxPayModel.Money = ConfigMoney.CirculationBase * Key;
            }
            if ((Value == null ? info.Version : (SystemVersionEnum)(Value)) == SystemVersionEnum.Level)
            {
                info.TagCodeNum += ServiceMessage.LEVEL;
                if (info.CompanyType == CompanyEnum.Plant || info.CompanyType == CompanyEnum.Culture)
                    WxPayModel.Money = ConfigMoney.PlantAndCultureLv * Key;
                if (info.CompanyType == CompanyEnum.Production)
                    WxPayModel.Money = ConfigMoney.ProductionLv * Key;
                if (info.CompanyType == CompanyEnum.Circulation)
                    WxPayModel.Money = ConfigMoney.CirculationLv * Key;
            }
            if ((Value == null ? info.Version : (SystemVersionEnum)(Value)) == SystemVersionEnum.Enterprise)
            {
                info.TagCodeNum += ServiceMessage.ENTERPRISE;
                if (info.CompanyType == CompanyEnum.Plant || info.CompanyType == CompanyEnum.Culture)
                    WxPayModel.Money = ConfigMoney.PlantAndCultureEnterprise * Key;
                if (info.CompanyType == CompanyEnum.Production)
                    WxPayModel.Money = ConfigMoney.ProductionEnterprise * Key;
                if (info.CompanyType == CompanyEnum.Circulation)
                    WxPayModel.Money = ConfigMoney.CirculationEnterprise * Key;
            }
            return WxPayCore.Instance.WebPay(WxPayModel);
        }
        /// <summary>
        /// 查询微信支付
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string WxQueryPay(RequestContractTemp Param)
        {
            SystemPayInfo PayInfo = Kily.Set<SystemPayInfo>().Where(t => t.GoodsId == Param.GoodsId)
                .Where(t => t.MerchantId == Param.MerchantId)
                .Where(t => t.PayType == PayEnum.WxPay)
                .AsNoTracking().FirstOrDefault();
            if (PayInfo != null)
            {
                String ResultCode = WxPayCore.Instance.QueryWxPay(PayInfo.TradeNo);
                if (string.IsNullOrEmpty(ResultCode))
                    return null;
                if (ResultCode.Equals("SUCCESS"))
                {
                    EnterpriseInfo Info = Kily.Set<EnterpriseInfo>().Where(t => t.Id == Param.MerchantId).FirstOrDefault();
                    PayInfo.PayDes = "SUCCESS";
                    IList<string> Fields = new List<string> { "Version", "TagCodeNum" };
                    Info.Version = Param.VersionType;
                    Info.TagCodeNum = Param.TagNum;
                    if (string.IsNullOrEmpty(PayInfo.PayDes))
                    {
                        UpdateField(PayInfo, "PayDes");
                        UpdateField(Info, null, Fields);
                    }
                    return "http://main.cfdacx.com/StaticHtml/WxNotify.html";
                }
                else
                    return null;
            }
            else
                return null;
        }
        #endregion

        #region 导出Excel
        /// <summary>
        /// 原料入库Excel导出
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public IList<Object> ExportMaterialInStockFile(String Param)
        {
            IQueryable<EnterpriseMaterialStock> queryable = Kily.Set<EnterpriseMaterialStock>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseMaterial> queryables = Kily.Set<EnterpriseMaterial>().Where(t => t.IsDelete == false);
            if (Param.Contains(","))
            {
                List<String> Ids = Param.Split(",").ToList();
                queryable = queryable.Where(t => Ids.Contains(t.Id.ToString()));
            }
            else
                queryable = queryable.Where(t => t.Id.ToString() == Param);
            var data = queryable.Join(queryables, t => t.BatchNo, x => x.BatchNo, (t, x) => new
            {
                编号 = "",
                原料名称 = x.MaterName,
                入库批次 = t.SerializNo,
                入库类型 = t.StockType,
                入库数量 = t.SetStockNum,
                入库时间 = t.SetStockTime,
                负责人 = t.SetStockUser,
                供应商 = x.Supplier
            }).ToList<Object>();
            return data;
        }
        /// <summary>
        /// 原料出库Excel导出
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public IList<Object> ExportMaterialOutStockFile(String Param)
        {
            IQueryable<EnterpriseMaterialStockAttach> queryable = Kily.Set<EnterpriseMaterialStockAttach>().Where(t => t.IsDelete == false);
            if (Param.Contains(","))
            {
                List<String> Ids = Param.Split(",").ToList();
                queryable = queryable.Where(t => Ids.Contains(t.Id.ToString()));
            }
            else
                queryable = queryable.Where(t => t.Id.ToString() == Param);
            var data = queryable.Select(t => new
            {
                编号 = "",
                出库时间 = t.OutStockTime,
                负责人 = t.OutStockUser,
                出库数量 = t.OutStockNum,
                出库批次 = t.SerializNo,
                出库类型 = t.StockType
            }).ToList<Object>();
            return data;
        }
        /// <summary>
        /// 产品入库Excel导出
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public IList<Object> ExportProInStockFile(String Param)
        {
            IQueryable<EnterpriseGoodsStock> queryable = Kily.Set<EnterpriseGoodsStock>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseGoods> queryables = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false);
            if (Param.Contains(","))
            {
                List<String> Ids = Param.Split(",").ToList();
                queryable = queryable.Where(t => Ids.Contains(t.Id.ToString()));
            }
            else
                queryable = queryable.Where(t => t.Id.ToString() == Param);
            var data = queryable.Join(queryables, t => t.GoodsId, x => x.Id, (t, x) => new
            {
                编号 = "",
                产品名称 = x.ProductName,
                入库批次 = t.GoodsBatchNo,
                入库类型 = t.StockType,
                入库数量 = t.InStockNum,
                入库时间 = t.ProductTime,
                负责人 = t.Manager
            }).ToList<Object>();
            return data;
        }
        /// <summary>
        /// 产品出库Excel导出
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public IList<Object> ExportProOutStockFile(String Param)
        {
            IQueryable<EnterpriseGoodsStockAttach> queryable = Kily.Set<EnterpriseGoodsStockAttach>().Where(t => t.IsDelete == false);
            if (Param.Contains(","))
            {
                List<String> Ids = Param.Split(",").ToList();
                queryable = queryable.Where(t => Ids.Contains(t.Id.ToString()));
            }
            else
                queryable = queryable.Where(t => t.Id.ToString() == Param);
            var data = queryable.Select(t => new
            {
                编号 = "",
                出库批次 = t.GoodsBatchNo,
                出库数量 = t.OutStockNum,
                出库时间 = t.OutStockTime,
                负责人 = t.OutStockUser,
                出库类型 = t.OutStockType
            }).ToList<Object>();
            return data;
        }
        #endregion

        #region 数据统计
        /// <summary>
        /// 数据统计
        /// </summary>
        /// <returns></returns>
        public Object GetDataCount(Guid? Id)
        {
            IQueryable<EnterpriseProductSeries> series = Kily.Set<EnterpriseProductSeries>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id);
            IQueryable<EnterpriseGoods> goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id);
            IQueryable<EnterpriseSeller> sellers = Kily.Set<EnterpriseSeller>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id);
            IQueryable<EnterpriseInferiorExprired> exprireds = Kily.Set<EnterpriseInferiorExprired>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id);
            IQueryable<EnterpriseRecover> recovers = Kily.Set<EnterpriseRecover>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id);
            IQueryable<EnterpriseTagAttach> tagAttaches = Kily.Set<EnterpriseTagAttach>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id);
            IQueryable<EnterpriseScanCodeInfo> infos = Kily.Set<EnterpriseScanCodeInfo>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id);
            IQueryable<SystemMessage> msg = Kily.Set<SystemMessage>().Where(t => t.IsDelete == false).Where(t => t.CompanyId == Id);
            int Series = series.Select(t => t.Id).Count();
            int Goods = goods.GroupBy(t => t.ProductSeriesId).AsNoTracking().Count();
            int Supplier = sellers.Where(t => t.SellerType == SellerEnum.Supplier).Select(t => t.Id).Count();
            int Sale = sellers.Where(t => t.SellerType == SellerEnum.Sale).Select(t => t.Id).Count();
            int Inferior = exprireds.Where(t => t.InferiorExprired == 1).Select(t => t.Id).Count();
            int Exprired = exprireds.Where(t => t.InferiorExprired == 2).Select(t => t.Id).Count();
            int Recover = recovers.Select(t => t.Id).Count();
            int Msg = msg.Select(t => t.Id).Count();
            int TagClass = tagAttaches.Where(t => t.TagType == "3").Sum(t => t.UseNum);
            int TagThing = tagAttaches.Where(t => t.TagType == "2").Sum(t => t.UseNum);
            int VeinTag = tagAttaches.Where(t => t.TagType == "1").Sum(t => t.UseNum);
            int Info = infos.Sum(t => t.ScanNum);
            Object obj = new { Series, Goods, Supplier, Sale, Inferior, Exprired, Recover, Msg, VeinTag, TagClass, TagThing, Info };
            return obj;
        }
        /// <summary>
        /// 产量统计
        /// </summary>
        /// <returns></returns>
        public ResponseDataCount GetPieCount(Guid? Id)
        {
            IQueryable<EnterpriseGoodsStock> queryable = Kily.Set<EnterpriseGoodsStock>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id);
            IQueryable<EnterpriseGoodsStockAttach> queryables = Kily.Set<EnterpriseGoodsStockAttach>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id);
            int WeekDataCount = queryable.Where(t => t.ProductTime > DateTime.Now.AddDays(-7))
                .Where(t => t.ProductTime < DateTime.Now)
                .GroupJoin(queryables, t => t.Id, x => x.StockId, (t, x) => new
                {
                    Total = t.InStockNum + x.Sum(o => o.OutStockNum)
                }).Sum(t => t.Total);
            int MonthDataCount = queryable.Where(t => t.ProductTime > DateTime.Now.AddMonths(-1))
              .Where(t => t.ProductTime < DateTime.Now)
              .GroupJoin(queryables, t => t.Id, x => x.StockId, (t, x) => new
              {
                  Total = t.InStockNum + x.Sum(o => o.OutStockNum)
              }).Sum(t => t.Total);
            int YearDataCount = queryable.Where(t => t.ProductTime > DateTime.Now.AddYears(-1))
                .Where(t => t.ProductTime < DateTime.Now)
                .GroupJoin(queryables, t => t.Id, x => x.StockId, (t, x) => new
                {
                    Total = t.InStockNum + x.Sum(o => o.OutStockNum)
                }).Sum(t => t.Total);
            IList<DataPie> OutSideData = new List<DataPie>
            {
                new DataPie { value = WeekDataCount, name = "周产量" },
                new DataPie { value = MonthDataCount, name = "月产量" },
                new DataPie { value = YearDataCount, name = "年产量" }
            };
            List<String> title = new List<String>() { "周产量", "月产量", "年产量" };
            ResponseDataCount dataCount = new ResponseDataCount()
            {
                Name = "数据统计",
                Type = true,
                DataTitle = title,
                InSideData = null,
                OutSideData = OutSideData
            };
            return dataCount;
        }
        /// <summary>
        /// 批次统计
        /// </summary>
        /// <returns></returns>
        public ResponseDataCount GetPieCountBatch(Guid? Id)
        {
            IQueryable<EnterpriseNote> notes = Kily.Set<EnterpriseNote>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id);
            IQueryable<EnterpriseProductionBatch> batches = Kily.Set<EnterpriseProductionBatch>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id);
            IQueryable<EnterpriseBuyer> buyers = Kily.Set<EnterpriseBuyer>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id);
            int Note = notes.GroupBy(t => t.BatchNo).Select(t => t.Key).Count();
            int Batch = batches.GroupBy(t => t.BatchNo).Select(t => t.Key).Count();
            int But = buyers.GroupBy(t => t.BatchNo).Select(t => t.Key).Count();
            IList<DataPie> OutSideData = new List<DataPie>();
            List<String> title = new List<String>();
            if (CompanyInfo() != null)
            {
                if (CompanyInfo().CompanyType == CompanyEnum.Plant || CompanyInfo().CompanyType == CompanyEnum.Culture)
                {
                    OutSideData.Add(new DataPie { value = Note, name = "成长日记" });
                    title.Add("成长日记");
                }
                if (CompanyInfo().CompanyType == CompanyEnum.Production)
                {
                    OutSideData.Add(new DataPie { value = Batch, name = "生产批次" });
                    title.Add("生产批次");
                }
                if (CompanyInfo().CompanyType == CompanyEnum.Circulation)
                {
                    OutSideData.Add(new DataPie { value = But, name = "进货批次" });
                    title.Add("进货批次");
                }
            }
            else
            {
                if (CompanyUser().CompanyType == CompanyEnum.Plant || CompanyUser().CompanyType == CompanyEnum.Culture)
                {
                    OutSideData.Add(new DataPie { value = Note, name = "成长日记" });
                    title.Add("成长日记");
                }
                if (CompanyUser().CompanyType == CompanyEnum.Production)
                {
                    OutSideData.Add(new DataPie { value = Batch, name = "生产批次" });
                    title.Add("生产批次");
                }
                if (CompanyUser().CompanyType == CompanyEnum.Circulation)
                {
                    OutSideData.Add(new DataPie { value = But, name = "进货批次" });
                    title.Add("进货批次");
                }
            }
            ResponseDataCount dataCount = new ResponseDataCount()
            {
                Name = "批次统计",
                Type = true,
                DataTitle = title,
                InSideData = null,
                OutSideData = OutSideData
            };
            return dataCount;
        }
        #endregion

        #region 手机扫描页面
        /// <summary>
        /// 一企一码
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseEnterprise GetScanCompanyFirst(Guid Id)
        {
            IQueryable<EnterpriseInfo> enterpriseInfos = Kily.Set<EnterpriseInfo>().Where(t => t.Id == Id).AsNoTracking();
            IQueryable<EnterpriseVedio> enterpriseVedios = Kily.Set<EnterpriseVedio>().OrderByDescending(t => t.CreateTime).Where(t => t.IsIndex == true).AsNoTracking();
            var data = enterpriseInfos.GroupJoin(enterpriseVedios, t => t.Id, x => x.CompanyId, (t, x) => new ResponseEnterprise
            {
                CompanyName = t.CompanyName,
                CompanyAddress = t.CompanyAddress,
                Scope = t.Scope,
                NetAddress = t.NetAddress,
                OfferLv = t.OfferLv,
                Discription = t.Discription,
                CompanyPhone = t.CompanyPhone,
                MainProRemark = t.MainProRemark,
                MainPro = t.MainPro,
                CompanySafeLv = t.CompanySafeLv,
                ComImage = t.ComImage,
                Certification = t.Certification,
                Honor = t.HonorCertification,
                TypePath = t.TypePath,
                CompanyType=t.CompanyType,
                CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType),
                VideoMap = x.ToDictionary(o => o.VedioName, o => o.VedioAddr)
            }).AsNoTracking().FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 手机端扫码查询
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Code"></param>
        public ResponseEnterpriseScanCodeContent GetScanCodeInfo(Guid? Id, String Code)
        {
            String SearchCode = String.Empty;
            String PreFix = String.Empty;
            int CodeType = 0;
            if (Code.Contains("W"))
            {
                SearchCode = Code.Split("W")[1].Substring(0, 12);
                PreFix = Code.Split("W")[0];
                CodeType = 2;
            }
            else if (Code.Contains("P"))
            {
                SearchCode = Code.Split("P")[1].Substring(0, 12);
                PreFix = Code.Split("P")[0];
                CodeType = 3;
            }
            else
            {
                SearchCode = Code.Substring(0, 11);
                CodeType = 1;
            }
            SqlParameter[] Param = {
             new SqlParameter("@Id", Id),
             new SqlParameter("@Code",SearchCode),
             new SqlParameter("@CodeType",CodeType),
             new SqlParameter("@PreFix",PreFix),
            };
            if (!Id.HasValue)
                Param[0].Value = DBNull.Value;
            if (String.IsNullOrEmpty(PreFix))
                Param[3].Value = DBNull.Value;
            var data = Kily.Execute("Sp_GetScanCodeInfo", Param).ToCollection<ResponseEnterpriseScanCodeContent>().FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 新增扫码记录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditScanInfo(RequestEnterpriseScanCodeInfo Param)
        {
            EnterpriseScanCodeInfo CodeInfo = Param.MapToEntity<EnterpriseScanCodeInfo>();
            EnterpriseScanCodeInfo Code = Kily.Set<EnterpriseScanCodeInfo>()
                .Where(t => t.ScanPackageNo.Equals(CodeInfo.ScanPackageNo))
                .Where(t => t.TakeCarId == Param.TakeCarId)
                .Where(t => t.ScanIP == Param.ScanIP)
                .AsNoTracking().FirstOrDefault();
            if (Code != null)
            {
                Code.ScanNum += 1;
                return UpdateField(Code, "ScanNum") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            }
            EnterpriseLogistics Log = Kily.Set<EnterpriseLogistics>().Where(t => t.PackageNo == CodeInfo.ScanPackageNo)
                .Where(t => t.IsDelete == false).AsNoTracking().FirstOrDefault();
            if (CodeInfo.ScanAddress.Contains(Log.Address))
            {
                Log.Correct += 1;
                UpdateField(Log, "Correct");
            }
            else
            {
                Log.Error += 1;
                UpdateField(Log, "Error");
            }
            CodeInfo.ScanNum += 1;
            return Insert(CodeInfo) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 手机端箱码
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public ResponseEnterpriseBoxing GetScanBoxInfo(Guid? Id, String Code)
        {
            IQueryable<EnterpriseBoxing> queryable = Kily.Set<EnterpriseBoxing>().Where(t => Code.Contains(Code)).Where(t => t.IsDelete == false);
            if (Id.HasValue)
                queryable = queryable.Where(t => t.Id == Id);
            var data = queryable.FirstOrDefault().MapToEntity<ResponseEnterpriseBoxing>();
            return data;
        }
        /// <summary>
        /// 发货信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public RequestEnterpriseLogistics GetScanSendInfo(String Id)
        {
            var data = Kily.Set<EnterpriseLogistics>().Where(t => t.GainUser.Equals(Id))
                 .AsNoTracking().FirstOrDefault().MapToEntity<RequestEnterpriseLogistics>();
            return data;
        }
        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string CheckLogistics(RequestEnterpriseLogistics Param)
        {
            var Temp = Kily.Set<EnterpriseSeller>().Where(t => t.IsDelete == false)
                .Where(t => t.SellerType == SellerEnum.Sale)
                .Where(t => t.LinkPhone.Equals(Param.LinkPhone)).AsNoTracking().FirstOrDefault();
            if (Temp == null)
                return "请勿串货";
            EnterpriseLogistics logistics = Kily.Set<EnterpriseLogistics>().Where(t => t.GainId == Temp.Id).FirstOrDefault();
            logistics.Flag = true;
            logistics.GetGoodTime = DateTime.Now;
            List<String> Fields = new List<String> { "Flag", "GetGoodTime" };
            return UpdateField(logistics, null, Fields) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }
        /// <summary>
        /// 装车清单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseEnterpriseGoodsPackage GetScanPackageInfo(Guid Id)
        {
            var data = Kily.Set<EnterpriseGoodsPackage>().Where(t => t.Id == Id)
                .AsNoTracking().FirstOrDefault().MapToEntity<ResponseEnterpriseGoodsPackage>();
            List<Guid> StockIds = Kily.Set<EnterpriseGoodsStockAttach>().Where(t => data.ProductOutStockNo.Contains(t.GoodsBatchNo)).Select(t => t.StockId).ToList();
            List<Guid> GoodsIds = Kily.Set<EnterpriseGoodsStock>().Where(t => StockIds.Contains(t.Id)).Select(t => t.GoodsId).ToList();
            data.ProductName = string.Join(",", Kily.Set<EnterpriseGoods>().Where(t => GoodsIds.Contains(t.Id)).Select(t => t.ProductName).ToArray());
            return data;

        }
        #endregion
    }
}