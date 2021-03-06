﻿using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Function;
using KilyCore.DataEntity.RequestMapper.Govt;
using KilyCore.DataEntity.RequestMapper.Repast;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Govt;
using KilyCore.DataEntity.ResponseMapper.Repast;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.Model.Enterprise;
using KilyCore.EntityFrameWork.Model.Govt;
using KilyCore.EntityFrameWork.Model.Repast;
using KilyCore.EntityFrameWork.Model.System;
using KilyCore.EntityFrameWork.ModelEnum;
using KilyCore.Extension.AttributeExtension;
using KilyCore.Extension.AutoMapperExtension;
using KilyCore.Extension.OutSideService;
using KilyCore.Extension.PayCore.AliPay;
using KilyCore.Extension.PayCore.WxPay;
using KilyCore.Extension.UtilExtension;
using KilyCore.Repositories.BaseRepository;
using KilyCore.Service.ConstMessage;
using KilyCore.Service.IServiceCore;
using KilyCore.Service.QueryExtend;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

#region << 版 本 注 释 >>

/*----------------------------------------------------------------
* 类 名 称 ：RepastWebService
* 类 描 述 ：
* 命名空间 ：KilyCore.Service.ServiceCore
* 机器名称 ：DESKTOP-QPIVQ28
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/16 15:13:14
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/

#endregion << 版 本 注 释 >>

namespace KilyCore.Service.ServiceCore
{
    public class RepastWebService : Repository, IRepastWebService
    {
        #region 获取全局集团菜单

        /// <summary>
        /// 返回子公司Id列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IQueryable<Guid> GetChildIdList(Guid Id)
        {
            return Kily.Set<RepastInfo>().Where(t => t.InfoId == Id).Select(t => t.Id).AsQueryable();
        }

        /// <summary>
        /// 下拉字典类型
        /// </summary>
        /// <returns></returns>
        public IList<ResponseRepastDictionary> GetDictionaryList(string Param)
        {
            IQueryable<RepastDictionary> queryable = Kily.Set<RepastDictionary>().Where(t => t.IsDelete == false);
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Where(t => t.DicType == Param).GroupBy(t => t.DicType)
                .Select(t => new ResponseRepastDictionary()
                {
                    DicType = t.Key.ToString(),
                }).AsNoTracking().ToList();
            data.ForEach(t =>
            {
                t.DictionaryList = queryable
                .Where(x => x.DicType == t.DicType)
                .Select(x => new ResponseRepastDictionary()
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
        /// 获取导航菜单
        /// </summary>
        /// <returns></returns>
        public IList<ResponseRepastMenu> GetRepastMenu()
        {
            IQueryable<RepastMenu> queryable = Kily.Set<RepastMenu>().Where(t => t.Level == MenuEnum.LevelOne).Where(t => t.IsDelete == false).AsNoTracking().AsQueryable().OrderBy(t => t.CreateTime);
            if (MerchantInfo() != null)
            {
                RepastRoleAuthor Author = null;
                RepastRoleAuthorWeb AuthorWeb = null;
                String RolePath = String.Empty;
                if (MerchantInfo().InfoId == null)
                {
                    Author = Kily.Set<RepastRoleAuthor>().Where(t => t.IsDelete == false).Where(t => t.Id == MerchantInfo().DingRoleId).AsNoTracking().FirstOrDefault();
                    RolePath = Author.AuthorMenuPath;
                }
                else
                {
                    AuthorWeb = Kily.Set<RepastRoleAuthorWeb>().Where(t => t.IsDelete == false).Where(t => t.Id == MerchantInfo().DingRoleId).AsNoTracking().FirstOrDefault();
                    RolePath = AuthorWeb.AuthorMenuPath;
                }
                queryable = queryable.Where(t => RolePath.Contains(t.Id.ToString())).AsNoTracking();
                var data = queryable.OrderBy(t => t.CreateTime).Select(t => new ResponseRepastMenu()
                {
                    Id = t.Id,
                    MenuId = t.MenuId,
                    ParentId = t.ParentId,
                    MenuAddress = t.MenuAddress,
                    MenuName = t.MenuName,
                    HasChildrenNode = t.HasChildrenNode,
                    MenuIcon = t.MenuIcon,
                    MenuChildren = Kily.Set<RepastMenu>()
                    .Where(x => x.ParentId == t.MenuId)
                    .Where(x => x.Level != MenuEnum.LevelOne)
                    .Where(x => x.IsDelete == false)
                    .Where(x => RolePath.Contains(x.Id.ToString()))
                    .OrderBy(x => x.CreateTime).Select(x => new ResponseRepastMenu()
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
                RepastRoleAuthorWeb AuthorWeb = Kily.Set<RepastRoleAuthorWeb>().Where(t => t.IsDelete == false).
                    Where(t => t.Id == MerchantUser().DingRoleId).AsNoTracking().FirstOrDefault();
                queryable = queryable.Where(t => AuthorWeb.AuthorMenuPath.Contains(t.Id.ToString())).AsNoTracking();
                var data = queryable.OrderBy(t => t.CreateTime).Select(t => new ResponseRepastMenu()
                {
                    Id = t.Id,
                    MenuId = t.MenuId,
                    ParentId = t.ParentId,
                    MenuAddress = t.MenuAddress,
                    MenuName = t.MenuName,
                    HasChildrenNode = t.HasChildrenNode,
                    MenuIcon = t.MenuIcon,
                    MenuChildren = Kily.Set<RepastMenu>()
                  .Where(x => x.ParentId == t.MenuId)
                  .Where(x => x.Level != MenuEnum.LevelOne)
                  .Where(x => x.IsDelete == false)
                  .Where(x => AuthorWeb.AuthorMenuPath.Contains(x.Id.ToString()))
                  .OrderBy(x => x.CreateTime).Select(x => new ResponseRepastMenu()
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
        public IList<ResponseParentTree> GetRepastWebTree(String Key)
        {
            IQueryable<RepastMenu> query = Kily.Set<RepastMenu>().Where(t => t.IsDelete == false).AsNoTracking();
            if (!string.IsNullOrEmpty(Key))
                query = query.Where(t => Key.Contains(t.Id.ToString()));
            if (MerchantInfo() != null)
            {
                if (MerchantInfo().InfoId == null)
                {
                    IQueryable<RepastRoleAuthor> queryables = Kily.Set<RepastRoleAuthor>().Where(t => t.IsDelete == false);
                    queryables = queryables.Where(t => t.Id == MerchantInfo().DingRoleId).AsNoTracking();
                    RepastRoleAuthor Author = queryables.FirstOrDefault();
                    IQueryable<ResponseParentTree> queryable = Kily.Set<RepastMenu>().Where(t => t.IsDelete == false)
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
                             Nodes = Kily.Set<RepastMenu>().Where(x => x.IsDelete == false)
                             .Where(x => x.Level != MenuEnum.LevelOne)
                             .Where(x => x.ParentId == t.MenuId)
                             .Where(x => Author.AuthorMenuPath.Contains(x.Id.ToString()))
                             .AsNoTracking()
                             .Select(x => new ResponseChildTree()
                             {
                                 Id = x.Id,
                                 Text = x.MenuName,
                                 Color = "black",
                                 BackClolor = "white",
                                 State = string.IsNullOrEmpty(Key) ? null : (query.Where(p => p.Id == x.Id).AsNoTracking().FirstOrDefault() != null ? new States { Checked = true } : null),
                                 SelectedIcon = "fa fa-refresh fa-spin",
                             }).AsQueryable()
                         }).AsQueryable();
                    var data = queryable.ToList();
                    return data;
                }
                else
                {
                    IQueryable<RepastRoleAuthorWeb> queryables = Kily.Set<RepastRoleAuthorWeb>().Where(t => t.IsDelete == false);
                    queryables = queryables.Where(t => t.Id == MerchantInfo().DingRoleId).AsNoTracking();
                    RepastRoleAuthorWeb Author = queryables.FirstOrDefault();
                    IQueryable<ResponseParentTree> queryable = Kily.Set<RepastMenu>().Where(t => t.IsDelete == false)
                         .Where(t => t.Level == MenuEnum.LevelOne)
                         .Where(t => Author.AuthorMenuPath.Contains(t.Id.ToString()))
                         .AsNoTracking().Select(t => new ResponseParentTree()
                         {
                             Id = t.Id,
                             Text = t.MenuName,
                             Color = "black",
                             BackClolor = "white",
                             SelectedIcon = "fa fa-refresh fa-spin",
                             State = string.IsNullOrEmpty(Key) ? null : (query.Where(x => x.Id == t.Id).AsNoTracking().FirstOrDefault() != null ? new States { Checked = true } : null),
                             Nodes = Kily.Set<RepastMenu>().Where(x => x.IsDelete == false)
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
                IQueryable<RepastRoleAuthorWeb> queryables = Kily.Set<RepastRoleAuthorWeb>().Where(t => t.IsDelete == false);
                queryables = queryables.Where(t => t.Id == MerchantUser().DingRoleId).AsNoTracking();
                RepastRoleAuthorWeb Author = queryables.FirstOrDefault();
                IQueryable<ResponseParentTree> queryable = Kily.Set<RepastMenu>().Where(t => t.IsDelete == false)
                     .Where(t => t.Level == MenuEnum.LevelOne)
                     .Where(t => Author.AuthorMenuPath.Contains(t.Id.ToString()))
                     .AsNoTracking().Select(t => new ResponseParentTree()
                     {
                         Id = t.Id,
                         Text = t.MenuName,
                         Color = "black",
                         BackClolor = "white",
                         State = string.IsNullOrEmpty(Key) ? null : (query.Where(p => p.Id == t.Id).AsNoTracking().FirstOrDefault() != null ? new States { Checked = true } : null),
                         SelectedIcon = "fa fa-refresh fa-spin",
                         Nodes = Kily.Set<RepastMenu>().Where(x => x.IsDelete == false)
                         .Where(x => x.Level != MenuEnum.LevelOne)
                         .Where(x => x.ParentId == t.MenuId)
                         .Where(x => Author.AuthorMenuPath.Contains(x.Id.ToString()))
                         .AsNoTracking()
                         .Select(x => new ResponseChildTree()
                         {
                             Id = x.Id,
                             Text = x.MenuName,
                             Color = "black",
                             BackClolor = "white",
                             State = string.IsNullOrEmpty(Key) ? null : (query.Where(p => p.Id == x.Id).AsNoTracking().FirstOrDefault() != null ? new States { Checked = true } : null),
                             SelectedIcon = "fa fa-refresh fa-spin",
                         }).AsQueryable()
                     }).AsQueryable();
                var data = queryable.ToList();
                return data;
            }
        }

        #endregion 获取全局集团菜单

        #region 编辑不需要权限的

        #region 商家资料

        /// <summary>
        /// 编辑商家资料
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveMerchant(RequestMerchant Param)
        {
            Param.AuditType = AuditEnum.WaitAduit;
            RepastInfo data = Kily.Set<RepastInfo>().Where(t => t.Id == Param.Id).AsNoTracking().FirstOrDefault();
            Param.DingRoleId = data.DingRoleId;
            Param.InfoId = data.InfoId;
            RepastInfo Info = Param.MapToEntity<RepastInfo>();
            //调用远程接口
            if (!string.IsNullOrEmpty(data.InviteCode))
            {
                if (data.AuditType != AuditEnum.AuditSuccess)
                {
                    var InviteCode = Encoding.Default.GetString(Convert.FromBase64String(data.InviteCode));
                    string Area = Kily.Set<EnterpriseInviteCode>().Where(t => t.InviteCode == InviteCode).Select(t => t.UseTypePath).FirstOrDefault() ?? "|";
                    if (!data.TypePath.Contains(Area))
                        return "请在邀请码选中区域使用!";
                    //验证信息是否正确
                    if (!CompanySearchApi.GetOutSideApiSearchApi.CheckCompany(Param.MerchantName, Param.CommunityCode))
                        return "对不起，您录入的企业名称和社会统一代码不一致！";
                    //正确才审核和提交合同
                    Info.AuditType = AuditEnum.AuditSuccess;
                    if (Kily.Set<SystemStayContract>().Where(t => t.CompanyId == Info.Id).FirstOrDefault() == null)
                    {
                        RequestStayContract contract = new RequestStayContract()
                        {
                            CompanyId = Info.Id,
                            TypePath = Info.TypePath,
                            CompanyName = Info.MerchantName,
                            VersionType = SystemVersionEnum.Base,
                            ContractYear = "1",
                            ContractType = 2,
                            IsFormInviteCode = true
                        };
                        SaveContract(contract);
                    }
                    var CompanyType = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(Info.DiningType);
                    EnterpriseRoleAuthor Role = Kily.Set<EnterpriseRoleAuthor>().Where(t => t.EnterpriseRoleName.Contains(CompanyType + "基础")).FirstOrDefault();
                    Info.DingRoleId = Role.Id;
                }
            }
            else
            {
                Info.AuditType = AuditEnum.WaitAduit;
            }
            return Update<RepastInfo, RequestMerchant>(Info, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        /// <summary>
        /// 修改账号密码
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveMerchantAccount(RequestMerchant Param)
        {
            RepastInfo info = Kily.Set<RepastInfo>().Where(t => t.Id == Param.Id).FirstOrDefault();
            info.Account = Param.Account;
            info.PassWord = Param.PassWord;
            List<String> Fields = new List<String>() { "Account", "PassWord" };
            return NormalUtil.CheckStringChineseUn(info.Account) ?
                "账号不能包含中文和特殊字符" :
                (UpdateField(info, null, Fields) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL);
        }

        /// <summary>
        /// 修改所属区域
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveMerchantArea(RequestMerchant Param)
        {
            RepastInfo info = Kily.Set<RepastInfo>().Where(t => t.Id == Param.Id).FirstOrDefault();
            info.TypePath = Param.TypePath;
            return UpdateField(info, "TypePath") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        #endregion 商家资料

        #region 商家认证

        /// <summary>
        /// 商家认证
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveMerchantIdent(RequestRepastIdent Param)
        {
            Param.Id = Guid.NewGuid();
            Param.IdentId = Param.Id;
            Param.AuditType = AuditEnum.WaitAduit;
            Param.IdentEndTime = Param.IdentStartTime.AddYears(Param.IdentYear);
            RepastIdent ident = Param.MapToEntity<RepastIdent>();
            RepastIdentAttach attach = Param.MapToEntity<RepastIdentAttach>();
            return Insert<RepastIdent>(ident, false) && Insert<RepastIdentAttach>(attach) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        #endregion 商家认证

        #region 权限角色

        /// <summary>
        /// 新增账户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveRoleAuthor(RequestRoleAuthorWeb Param)
        {
            RepastRoleAuthorWeb Author = Param.MapToEntity<RepastRoleAuthorWeb>();
            if (Param.Id == Guid.Empty)
                return Insert<RepastRoleAuthorWeb>(Author) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return Update<RepastRoleAuthorWeb, RequestRoleAuthorWeb>(Author, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        #endregion 权限角色

        #region 人员管理

        /// <summary>
        /// 编辑子账户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveUser(RequestMerchantUser Param)
        {
            RepastInfoUser User = Param.MapToObj<RequestMerchantUser, RepastInfoUser>();
            if (Param.Id == Guid.Empty)
            {
                var Users = Kily.Set<RepastInfoUser>().Where(t => t.Account.Equals(Param.Account)).AsNoTracking().FirstOrDefault();
                if (Users != null) return "该账号已经存在!";
            }
            if (MerchantInfo() != null)
                User.TypePath = MerchantInfo().TypePath;
            else
                User.TypePath = MerchantUser().TypePath;
            if (NormalUtil.CheckStringChineseUn(User.Account))
                return "账号不能包含中文和特殊字符";
            if (Param.Id != Guid.Empty)
            {
                if (Update<RepastInfoUser, RequestMerchantUser>(User, Param))
                    return ServiceMessage.UPDATESUCCESS;
                else
                    return ServiceMessage.UPDATEFAIL;
            }
            else
            {
                if (Insert<RepastInfoUser>(User))
                    return ServiceMessage.INSERTSUCCESS;
                else
                    return ServiceMessage.INSERTFAIL;
            }
        }

        #endregion 人员管理

        #region 集团账户

        /// <summary>
        /// 编辑子账户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveChildInfo(RequestMerchant Param)
        {
            if (MerchantInfo() != null)
            {
                if (MerchantInfo().InfoId != null)
                    return "无权限创建，仅限总公司使用!";
                RepastInfo info = Param.MapToEntity<RepastInfo>();
                var Infos = Kily.Set<RepastInfo>().Where(t => t.Account.Equals(Param.Account)).AsNoTracking().FirstOrDefault();
                if (Infos != null) return "该账号已经存在!";
                info.AuditType = AuditEnum.WaitAduit;
                info.DiningType = MerchantEnum.Normal;
                return Insert(info) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
            return "无权限创建!";
        }

        #endregion 集团账户

        #region 餐饮字典

        /// <summary>
        /// 编辑字典
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveDic(RequestRepastDictionary Param)
        {
            RepastDictionary dictionary = Param.MapToEntity<RepastDictionary>();
            if (Param.Id != Guid.Empty)
            {
                return Update<RepastDictionary, RequestRepastDictionary>(dictionary, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            }
            else
            {
                return Insert<RepastDictionary>(dictionary) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            }
        }

        #endregion 餐饮字典

        #region 升级续费

        /// <summary>
        /// 编辑续费
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveContinued(RequestRepastContinued Param)
        {
            RepastContinued continued = Param.MapToEntity<RepastContinued>();
            continued.AuditType = AuditEnum.WaitAduit;
            return Insert<RepastContinued>(continued) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        /// <summary>
        /// 编辑升级
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveUpLevel(RequestRepastUpLevel Param)
        {
            RepastUpLevel level = Param.MapToEntity<RepastUpLevel>();
            level.AuditType = AuditEnum.WaitAduit;
            return Insert<RepastUpLevel>(level) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        #endregion 升级续费

        #region 供应商

        /// <summary>
        /// 编辑供应商
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveSupplier(RequestRepastSupplier Param)
        {
            RepastSupplier supplier = Param.MapToEntity<RepastSupplier>();
            return Insert(supplier) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        #endregion 供应商

        #region 实时监控

        /// <summary>
        /// 编辑视频
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveVideo(RequestRepastVideo Param)
        {
            RepastVideo video = Param.MapToEntity<RepastVideo>();
            if (MerchantInfo() != null)
                video.TypePath = MerchantInfo().TypePath;
            else
                video.TypePath = MerchantUser().TypePath;
            return Insert(video) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        #endregion 实时监控

        #region 菜品管理

        /// <summary>
        /// 编辑菜品
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveDish(RequestRepastDish Param)
        {
            RepastDish dish = Param.MapToEntity<RepastDish>();
            if (Param.Id == Guid.Empty)
                return Insert<RepastDish>(dish) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return Update<RepastDish, RequestRepastDish>(dish, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        #endregion 菜品管理

        #endregion 编辑不需要权限的

        #region 删除不需要权限的

        #region 权限角色

        /// <summary>
        /// 删除账户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteRole(Guid Id)
        {
            if (Remove<RepastRoleAuthorWeb>(t => t.Id == Id))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }

        #endregion 权限角色

        #region 人员管理

        /// <summary>
        /// 删除子账号
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteUser(Guid Id)
        {
            if (Delete(ExpressionExtension.GetExpression<RepastInfoUser>("Id", Id, ExpressionEnum.Equals)))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }

        #endregion 人员管理

        #region 餐饮字典

        /// <summary>
        /// 删除码表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteDic(Guid Id)
        {
            return Remove<RepastDictionary>(ExpressionExtension.GetExpression<RepastDictionary>("Id", Id, ExpressionEnum.Equals)) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        #endregion 餐饮字典

        #region 供应商

        /// <summary>
        /// 删除供应商
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteSupplier(Guid Id)
        {
            return Delete(ExpressionExtension.GetExpression<RepastSupplier>("Id", Id, ExpressionEnum.Equals)) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        #endregion 供应商

        #region 台账凭证

        /// <summary>
        /// 删除台账凭证
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteTicket(Guid Id)
        {
            return Remove<RepastBillTicket>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        #endregion 台账凭证

        #region 周菜谱

        /// <summary>
        /// 删除周菜谱
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteWeekMenu(Guid Id)
        {
            return Delete<RepastFoodMenu>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        #endregion 周菜谱

        #region 实时监控

        /// <summary>
        /// 删除视频
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteVideo(Guid Id)
        {
            return Remove<RepastVideo>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        #endregion 实时监控

        #region 菜品管理

        /// <summary>
        /// 删除菜品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteDish(Guid Id)
        {
            return Delete(ExpressionExtension.GetExpression<RepastDish>("Id", Id, ExpressionEnum.Equals)) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        #endregion 菜品管理

        #endregion 删除不需要权限的

        #region 基础管理

        #region 商家资料

        /// <summary>
        /// 商家资料分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseMerchant> GetMerchantInfoPage(PageParamList<RequestMerchant> pageParam)
        {
            var data = Kily.Set<RepastInfo>().Where(t => t.Id == pageParam.QueryParam.Id).Select(t => new ResponseMerchant()
            {
                Id = t.Id,
                MerchantName = t.MerchantName,
                Account = t.Account,
                Address = t.Address,
                CommunityCode = t.CommunityCode,
                Certification = t.Certification,
                TypePath = t.TypePath,
                IsPayContract = t.IsPayContract,
                DiningTypeName = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType)
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseMerchant GetMerchantDetail(Guid Id)
        {
            var query = Kily.Set<SystemStayContract>().Where(t => t.EnterpriseOrMerchant == 2).AsNoTracking();
            var data = Kily.Set<RepastInfo>().Where(t => t.Id == Id)
                .GroupJoin(query, t => (t.InfoId != null ? t.InfoId : t.Id), x => x.CompanyId, (t, x) => new ResponseMerchant
                {
                    Id = t.Id,
                    Account = t.Account,
                    Address = t.Address,
                    PassWord = t.PassWord,
                    CommunityCode = t.CommunityCode,
                    MerchantName = t.MerchantName,
                    MerchantImage = t.MerchantImage,
                    LngAndLat = t.LngAndLat,
                    DiningType = t.DiningType,
                    CardExpiredDate = t.CardExpiredDate,
                    SafeOffer = t.SafeOffer,
                    OfferLv = t.OfferLv,
                    SaleScope = t.SaleScope,
                    SaleTime = t.SaleTime,
                    DiningTypeName = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType),
                    Phone = t.Phone,
                    VersionType = t.VersionType,
                    VersionTypeName = AttrExtension.GetSingleDescription<SystemVersionEnum, DescriptionAttribute>(t.VersionType),
                    AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(x.FirstOrDefault() != null ? x.FirstOrDefault().AuditType : 0),
                    TypePath = t.TypePath,
                    Certification = t.Certification,
                    Email = t.Email,
                    ImplUser = t.ImplUser,
                    AllowUnit = t.AllowUnit,
                    IdCard = t.IdCard,
                    Remark = t.Remark,
                    ComplainPhone = t.ComplainPhone
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
            IQueryable<RepastInfo> queryable = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false).AsNoTracking();
            var Maps = queryable.Where(t => t.InfoId == Id).ToDictionary(t => t.Id, t => t.MerchantName);
            var Map = queryable.Where(t => t.Id == Id).ToDictionary(t => t.Id, t => t.MerchantName);
            Maps.Add(Map.Keys.FirstOrDefault(), Map.Values.FirstOrDefault());
            return Maps;
        }

        /// <summary>
        /// 保存合同
        /// </summary>
        /// <returns></returns>
        public ResponseStayContract SaveContract(RequestStayContract Param)
        {
            Param.AuditType = AuditEnum.WaitAduit;
            RequestAliPayModel AliPayModel = new RequestAliPayModel();
            AliPayModel.OrderTitle = MerchantInfo().MerchantName + "合同缴费";
            SystemStayContract contract = Param.MapToEntity<SystemStayContract>();
            contract.EnterpriseOrMerchant = 2;
            RepastInfo info = Kily.Set<RepastInfo>().Where(t => t.Id == contract.CompanyId).FirstOrDefault();
            var Demo = Kily.Set<SystemStayContract>().Where(t => t.CompanyId == info.Id).Where(t => t.EnterpriseOrMerchant == 2).FirstOrDefault();
            if (Demo != null)
            {
                return new ResponseStayContract()
                {
                    Id = contract.Id,
                    VersionType = Param.VersionType,
                    PayInfoMsg = Update(Demo, Param) ? "提交成功" : "提交失败"
                };
            }
            info.IsPayContract = true;
            if (Param.VersionType == SystemVersionEnum.Test)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    AliPayModel.Money = ConfigMoney.RepastTest * Convert.ToInt32(Param.ContractYear);
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    AliPayModel.Money = ConfigMoney.UnitCanteenTest * Convert.ToInt32(Param.ContractYear);
            }
            if (Param.VersionType == SystemVersionEnum.Base)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    AliPayModel.Money = ConfigMoney.RepastBase * Convert.ToInt32(Param.ContractYear);
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    AliPayModel.Money = ConfigMoney.UnitCanteenBase * Convert.ToInt32(Param.ContractYear);
            }
            if (Param.VersionType == SystemVersionEnum.Level)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    AliPayModel.Money = ConfigMoney.RepastLv * Convert.ToInt32(Param.ContractYear);
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    AliPayModel.Money = ConfigMoney.UnitCanteenLv * Convert.ToInt32(Param.ContractYear);
            }
            if (Param.VersionType == SystemVersionEnum.Enterprise)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    AliPayModel.Money = ConfigMoney.RepastEnterprise * Convert.ToInt32(Param.ContractYear);
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    AliPayModel.Money = ConfigMoney.UnitCanteenEnterprise * Convert.ToInt32(Param.ContractYear);
            }
            if (Param.VersionType == SystemVersionEnum.Common)
                AliPayModel.Money = ConfigMoney.Common * Convert.ToInt32(Param.ContractYear);
            contract.Id = Guid.NewGuid();
            UpdateField(info, "IsPayContract");
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
                        PayInfoMsg = Insert<SystemStayContract>(contract) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL
                    };
                }
                //支付宝支付
                else if (contract.PayType == PayEnum.Alipay)
                {
                    contract.TotalPrice = (decimal)AliPayModel.Money;
                    SystemStayContract CompanyContract = Kily.Set<SystemStayContract>().Where(t => t.CompanyId == contract.CompanyId)
                      .Where(t => t.PayType == PayEnum.WxPay)
                      .Where(t => t.EnterpriseOrMerchant == 2)
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
                        });
                        return new ResponseStayContract()
                        {
                            Id = contract.Id,
                            VersionType = Param.VersionType,
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
                      .Where(t => t.EnterpriseOrMerchant == 2)
                      .AsNoTracking().FirstOrDefault();
                    if (CompanyContract == null)
                    {
                        Insert(contract, false);
                        Insert(new SystemPayInfo()
                        {
                            MerchantId = contract.CompanyId,
                            GoodsId = contract.Id,
                            PayType = contract.PayType,
                            TradeNo = WxPayCore.Instance.GetTradeNo()
                        });
                        return new ResponseStayContract()
                        {
                            Id = contract.Id,
                            VersionType = Param.VersionType,
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
                    PayInfoMsg = Insert<SystemStayContract>(contract) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL
                };
            }
        }

        /// <summary>
        /// 获取合同状态
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseStayContract> GetContractAudit(PageParamList<RequestStayContract> pageParam)
        {
            var data = Kily.Set<SystemStayContract>().Where(t => t.CompanyId == MerchantInfo().Id)
                .Where(t => t.CompanyName.Equals(MerchantInfo().MerchantName))
                .Select(t => new ResponseStayContract()
                {
                    Id = t.Id,
                    ContractYear = t.ContractYear,
                    ContractType = t.ContractType,
                    StayTime = t.CreateTime,
                    EndTime = t.EndTime,
                    VersionType = t.VersionType,
                    VersionTypeName = AttrExtension.GetSingleDescription<SystemVersionEnum, DescriptionAttribute>(t.VersionType),
                    AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(t.AuditType),
                }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        #endregion 商家资料

        #region 商家认证

        /// <summary>
        /// 商家认证分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastIdent> GetIndentPage(PageParamList<RequestRepastIdent> pageParam)
        {
            IQueryable<RepastIdent> queryable = Kily.Set<RepastIdent>().OrderByDescending(t => t.CreateTime);
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new ResponseRepastIdent()
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

        #endregion 商家认证

        #region 权限角色

        /// <summary>
        /// 账户分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRoleAuthorWeb> GetRoleAuthorPage(PageParamList<RequestRoleAuthorWeb> pageParam)
        {
            IQueryable<RepastRoleAuthorWeb> queryable = Kily.Set<RepastRoleAuthorWeb>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.AuthorName))
                queryable = queryable.Where(t => t.AuthorName.Contains(pageParam.QueryParam.AuthorName));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.CreateUser == MerchantInfo().Id.ToString() || GetChildIdList(MerchantInfo().Id).Contains(Guid.Parse(t.CreateUser)));
            else
                queryable = queryable.Where(t => t.CreateUser == MerchantUser().Id.ToString());
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseRoleAuthorWeb()
            {
                Id = t.Id,
                AuthorName = t.AuthorName,
                AuthorMenuPath = t.AuthorMenuPath
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 集团账户权限列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseRoleAuthorWeb> GetRoleAuthorList()
        {
            IQueryable<RepastRoleAuthorWeb> queryable = Kily.Set<RepastRoleAuthorWeb>().OrderByDescending(t => t.CreateTime);
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.CreateUser == MerchantInfo().Id.ToString() || GetChildIdList(MerchantInfo().Id).Contains(Guid.Parse(t.CreateUser)));
            else
                queryable = queryable.Where(t => t.CreateUser == MerchantUser().Id.ToString());
            var data = queryable.Select(t => new ResponseRoleAuthorWeb()
            {
                Id = t.Id,
                AuthorName = t.AuthorName
            }).ToList();
            return data;
        }

        #endregion 权限角色

        #region 人员管理

        /// <summary>
        /// 人员分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseMerchantUser> GetMerchantUserPage(PageParamList<RequestMerchantUser> pageParam)
        {
            IQueryable<RepastInfoUser> queryable = Kily.Set<RepastInfoUser>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.TrueName))
                queryable = queryable.Where(t => t.TrueName.Contains(pageParam.QueryParam.TrueName));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new ResponseMerchantUser()
            {
                Id = t.Id,
                InfoId = t.InfoId,
                TrueName = t.TrueName,
                HealthCard = t.HealthCard,
                ExpiredTime = t.ExpiredTime,
                Phone = t.Phone,
                Account = t.Account,
                IdCard = t.IdCard
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 获取子账户详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseMerchantUser GetUserDetail(Guid Id)
        {
            IQueryable<RepastInfoUser> queryable = Kily.Set<RepastInfoUser>().Where(t => t.Id == Id);
            var data = queryable.Select(t => new ResponseMerchantUser()
            {
                Id = t.Id,
                TrueName = t.TrueName,
                InfoId = t.InfoId,
                DiningType = t.DiningType,
                MerchantName = t.MerchantName,
                VersionType = t.VersionType,
                Account = t.Account,
                Phone = t.Phone,
                DingRoleId = t.DingRoleId,
                IdCard = t.IdCard,
                ExpiredTime = t.ExpiredTime,
                PassWord = t.PassWord,
                HealthCard = t.HealthCard
            }).AsNoTracking().FirstOrDefault();
            return data;
        }

        /// <summary>
        /// 人员列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseMerchantUser> GetMerchantList()
        {
            IQueryable<RepastInfoUser> queryable = Kily.Set<RepastInfoUser>().Where(t => t.IsDelete == false).AsNoTracking();
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.Id == MerchantUser().InfoId);
            var data = queryable.Select(t => new ResponseMerchantUser()
            {
                Id = t.Id,
                TrueName = t.TrueName
            }).ToList();
            return data;
        }

        #endregion 人员管理

        #region 集团账户

        /// <summary>
        /// 集团账户列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseMerchant> GetChildInfoPage(PageParamList<RequestMerchant> pageParam)
        {
            var data = Kily.Set<RepastInfo>().Where(t => t.InfoId == pageParam.QueryParam.Id)
               .OrderByDescending(t => t.CreateTime).Select(t => new ResponseMerchant()
               {
                   Id = t.Id,
                   InfoId = t.InfoId,
                   MerchantName = t.MerchantName,
                   Account = t.Account,
                   Address = t.Address,
                   Certification = t.Certification
               }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 集团账号详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseMerchant GetChildInfo(Guid Id)
        {
            return Kily.Set<RepastInfo>().Where(t => t.Id == Id).Select(t => new ResponseMerchant()
            {
                Id = t.Id,
                InfoId = t.InfoId,
                MerchantName = t.MerchantName,
                Account = t.Account,
                Address = t.Address,
                Phone = t.Phone,
                PassWord = t.PassWord,
                DingRoleId = t.DingRoleId
            }).FirstOrDefault();
        }

        #endregion 集团账户

        #region 餐饮字典

        /// <summary>
        /// 字典分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastDictionary> GetDicPage(PageParamList<RequestRepastDictionary> pageParam)
        {
            IQueryable<RepastDictionary> queryable = Kily.Set<RepastDictionary>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.DicType))
                queryable = queryable.Where(t => t.DicType.Contains(pageParam.QueryParam.DicType));
            if (!string.IsNullOrEmpty(pageParam.QueryParam.DicName))
                queryable = queryable.Where(t => t.DicName.Contains(pageParam.QueryParam.DicName));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.AsNoTracking().Select(t => new ResponseRepastDictionary()
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
        public ResponseRepastDictionary GetDicDetail(Guid Id)
        {
            var data = Kily.Set<RepastDictionary>().Where(t => t.Id == Id).AsNoTracking().Select(t => new ResponseRepastDictionary()
            {
                Id = t.Id,
                InfoId = t.InfoId,
                DicType = t.DicType,
                DicName = t.DicName,
                DicValue = t.DicValue,
                Remark = t.Remark
            }).FirstOrDefault();
            return data;
        }

        #endregion 餐饮字典

        #region 升级续费

        /// <summary>
        /// 查看版本信息
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastLevelUp> GetLvPage(PageParamList<RequestRepastLevelUp> pageParam)
        {
            IQueryable<SystemStayContract> queryable = Kily.Set<SystemStayContract>().Where(t => t.CompanyId == pageParam.QueryParam.Id).Where(t => t.EnterpriseOrMerchant == 2).Where(t => t.IsDelete == false);
            var data = queryable.Select(t => new ResponseRepastLevelUp()
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
        public PagedResult<ResponseRepastContinued> GetContinuedPage(PageParamList<RequestRepastContinued> pageParam)
        {
            IQueryable<RepastContinued> queryable = Kily.Set<RepastContinued>().Where(t => t.InfoId == pageParam.QueryParam.InfoId);
            var data = queryable.Select(t => new ResponseRepastContinued()
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
        public PagedResult<ResponseRepastUpLevel> GetUpLevelPage(PageParamList<RequestRepastUpLevel> pageParam)
        {
            IQueryable<RepastUpLevel> queryable = Kily.Set<RepastUpLevel>().Where(t => t.InfoId == pageParam.QueryParam.InfoId);
            var data = queryable.Select(t => new ResponseRepastUpLevel()
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
            RepastContinued continued = null;
            RepastUpLevel level = null;
            if (Param)
                continued = Kily.Set<RepastContinued>().Where(t => t.Id == Id).FirstOrDefault();
            else
                level = Kily.Set<RepastUpLevel>().Where(t => t.Id == Id).FirstOrDefault();
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
                    SystemStayContract contract = Kily.Set<SystemStayContract>().Where(t => t.EnterpriseOrMerchant == 2).Where(t => t.CompanyId == continued.InfoId).FirstOrDefault();
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
                    SystemStayContract contract = Kily.Set<SystemStayContract>().Where(t => t.EnterpriseOrMerchant == 2).Where(t => t.CompanyId == level.InfoId).FirstOrDefault();
                    contract.CreateTime = DateTime.Now;
                    contract.EndTime = DateTime.Now.AddYears(Convert.ToInt32(continued.ContinuedYear));
                    contract.ContractYear = level.ContinuedYear;
                    contract.PayTicket = level.PayTicket;
                    contract.PayType = level.PayType;
                    contract.VersionType = level.VersionType;
                    IList<string> Fields = new List<string> { "CreateTime", "EndTime", "ContractYear", "PayTicket", "PayType", "VersionType" };
                    IList<string> Field = new List<string> { "IsPay", "AuditType" };
                    UpdateField(contract, null, Fields);
                    level.AuditType = AuditEnum.FinanceSuccess;
                    level.IsPay = true;
                    return UpdateField(level, null, Field) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
                }
            }
        }

        #endregion 升级续费

        #region 商家自查

        /// <summary>
        /// 获取企业检查分页
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtTemplateChild> GetTemplateChild(PageParamList<RequestGovtTemplateChild> pageParam)
        {
            IQueryable<GovtTemplateChild> queryable = Kily.Set<GovtTemplateChild>().OrderByDescending(t => t.CreateTime).AsNoTracking();
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.TypePath.Contains(MerchantInfo().TypePath));
            else
                queryable = queryable.Where(t => t.TypePath.Contains(MerchantUser().TypePath));
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
        /// 自查详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseGovtTemplateChild GetTemplateChildDetail(Guid Id)
        {
            return Kily.Set<GovtTemplateChild>().Where(t => t.Id == Id).FirstOrDefault().MapToEntity<ResponseGovtTemplateChild>();
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

        /// <summary>
        /// 删除企业自查记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteTemplate(Guid Id)
        {
            return Remove<GovtTemplateChild>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        #endregion 商家自查

        #region 委员

        /// <summary>
        /// 委员分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastOrg> GetOrgPage(PageParamList<RequestRepastOrg> pageParam)
        {
            IQueryable<RepastOrg> queryable = Kily.Set<RepastOrg>().OrderByDescending(t => t.CreateTime).AsNoTracking();
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.TrueName))
                queryable = queryable.Where(t => t.TrueName.Contains(pageParam.QueryParam.TrueName));
            var data = queryable.Select(t => new ResponseRepastOrg()
            {
                Id = t.Id,
                TrueName = t.TrueName,
                LinkPhone = t.LinkPhone,
                IdCardNo = t.IdCardNo,
                Worker = t.Worker,
                IsWork = t.IsWork,
                Address = t.Address
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 委员详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseRepastOrg GetOrgDetail(Guid Id)
        {
            return Kily.Set<RepastOrg>().Where(t => t.Id == Id).FirstOrDefault().MapToEntity<ResponseRepastOrg>();
        }

        /// <summary>
        /// 编辑委员
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditOrg(RequestRepastOrg Param)
        {
            RepastOrg Org = Param.MapToEntity<RepastOrg>();
            if (Org.Id == Guid.Empty)
                return Insert(Org) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return Update(Org, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        /// <summary>
        /// 删除委员
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveOrg(Guid Id)
        {
            return Remove<RepastOrg>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 判断是否存在家委会
        /// </summary>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public string GetOrgInfo(String Phone)
        {
            RepastOrg repast = Kily.Set<RepastOrg>().Where(t => t.LinkPhone.Equals(Phone)).AsNoTracking().FirstOrDefault();
            if (repast == null)
                return "不存在";
            else
                return repast.InfoId.ToString();
        }

        #endregion 委员

        #endregion 基础管理

        #region 功能管理

        #region 供应商

        /// <summary>
        /// 供应商列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastSupplier> GetSupplierPage(PageParamList<RequestRepastSupplier> pageParam)
        {
            IQueryable<RepastSupplier> queryable = Kily.Set<RepastSupplier>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.SupplierName))
                queryable = queryable.Where(t => t.SupplierName.Contains(pageParam.QueryParam.SupplierName));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new ResponseRepastSupplier()
            {
                Id = t.Id,
                SupplierName = t.SupplierName,
                SupplierNo = t.SupplierNo,
                Address = t.Address,
                SupplierUser = t.SupplierUser,
                LinkPhone = t.LinkPhone,
                HealthCard = t.HealthCard,
                RunCard = t.RunCard
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 供应商列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseRepastSupplier> GetSupplierList()
        {
            IQueryable<RepastSupplier> queryable = Kily.Set<RepastSupplier>().Where(t => t.IsDelete == false);
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new ResponseRepastSupplier()
            {
                Id = t.Id,
                SupplierUser = t.SupplierUser,
                LinkPhone = t.LinkPhone,
                SupplierName = t.SupplierName,
                RunCard = t.RunCard,
                Address = t.Address,
            }).ToList();
            return data;
        }

        #endregion 供应商

        #region 进货台账

        /// <summary>
        /// 进货台账分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastBuybill> GetBuybillPage(PageParamList<RequestRepastBuybill> pageParam)
        {
            IQueryable<RepastBuybill> queryable = Kily.Set<RepastBuybill>().Where(t => t.IsDelete == false).AsNoTracking();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.GoodsName))
                queryable = queryable.Where(t => t.GoodsName.Contains(pageParam.QueryParam.GoodsName));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseRepastBuybill()
            {
                Id = t.Id,
                GoodsName = t.GoodsName,
                GoodsNum = t.GoodsNum,
                LinkPhone = t.LinkPhone,
                Purchase = t.Purchase,
                Unit = t.Unit,
                NoExp = t.NoExp,
                ProTime = t.ProTime,
                ToPay = t.ToPay,
                Supplier = t.Supplier,
                OrderTime = t.OrderTime,
                UnPay = t.UnPay
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 删除进货台账
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveBuybill(Guid Id)
        {
            return Delete<RepastBuybill>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 进货台账详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseRepastBuybill GetBuybillDetail(Guid Id)
        {
            var data = Kily.Set<RepastBuybill>().Where(t => t.Id == Id).Select(t => new ResponseRepastBuybill()
            {
                Id = t.Id,
                NoExp = t.NoExp,
                ProTime = t.ProTime,
                GoodsName = t.GoodsName,
                GoodsNum = t.GoodsNum,
                LinkPhone = t.LinkPhone,
                Purchase = t.Purchase,
                Unit = t.Unit,
                ToPay = t.ToPay,
                Supplier = t.Supplier,
                OrderTime = t.OrderTime,
                UnPay = t.UnPay
            }).FirstOrDefault();
            return data;
        }

        /// <summary>
        /// 编辑进货台账
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditBuybill(RequestRepastBuybill Param)
        {
            RepastBuybill buybill = Param.MapToEntity<RepastBuybill>();
            if (Param.Id == Guid.Empty)
            {
                buybill.InfoId = (MerchantInfo() == null ? MerchantUser().Id : MerchantInfo().Id);
                return Insert(buybill) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
            else
                return Update<RepastBuybill, RequestRepastBuybill>(buybill, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        #endregion 进货台账

        #region 台账凭证

        /// <summary>
        ///  凭证分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseBillTicket> GetMerchantTicketPage(PageParamList<RequestBillTicket> pageParam)
        {
            IQueryable<RepastBillTicket> queryable = Kily.Set<RepastBillTicket>().Where(t => t.IsDelete == false).AsNoTracking();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.Theme))
                queryable = queryable.Where(t => t.Theme.Contains(pageParam.QueryParam.Theme));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseBillTicket()
            {
                Id = t.Id,
                Theme = t.Theme,
                Content = t.Content.Replace("/Upload/", "http://system.cfda.vip/Upload/").Replace("<img", "<img style='max-width:100%;' "),
                UpTime = t.UpTime
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 台账详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseBillTicket GetMerchantTicketDetail(Guid Id)
        {
            return Kily.Set<RepastBillTicket>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.Id == Id).FirstOrDefault().MapToEntity<ResponseBillTicket>();
        }

        /// <summary>
        /// 添加凭证
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditTheme(RequestBillTicket Param)
        {
            RepastBillTicket ticket = Param.MapToEntity<RepastBillTicket>();
            return Insert(ticket) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        #endregion 台账凭证

        #region 销售台账

        /// <summary>
        /// 销售台账分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastSellbill> GetSellbillPage(PageParamList<RequestRepastSellbill> pageParam)
        {
            IQueryable<RepastSellbill> queryable = Kily.Set<RepastSellbill>().Where(t => t.IsDelete == false).AsNoTracking();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.GoodsName))
                queryable = queryable.Where(t => t.GoodsName.Contains(pageParam.QueryParam.GoodsName));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseRepastSellbill()
            {
                Id = t.Id,
                GoodsName = t.GoodsName,
                GoodsNum = t.GoodsNum,
                ToPay = t.ToPay,
                SellTime = t.SellTime,
                NoExp = t.NoExp,
                ProTime = t.ProTime,
                UnPay = t.UnPay,
                Manager = t.Manager
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 删除销售台账
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveSellbill(Guid Id)
        {
            return Delete<RepastSellbill>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 销售台账详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseRepastSellbill GetSellbillDetail(Guid Id)
        {
            var data = Kily.Set<RepastSellbill>().Where(t => t.Id == Id).Select(t => new ResponseRepastSellbill()
            {
                Id = t.Id,
                GoodsName = t.GoodsName,
                GoodsNum = t.GoodsNum,
                ToPay = t.ToPay,
                NoExp = t.NoExp,
                ProTime = t.ProTime,
                SellTime = t.SellTime,
                UnPay = t.UnPay,
                Manager = t.Manager,
            }).FirstOrDefault();
            return data;
        }

        /// <summary>
        /// 编辑销售台账
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditSellbill(RequestRepastSellbill Param)
        {
            RepastSellbill sellbill = Param.MapToEntity<RepastSellbill>();
            if (Param.Id == Guid.Empty)
            {
                sellbill.InfoId = (MerchantInfo() == null ? MerchantUser().Id : MerchantInfo().Id);
                return Insert(sellbill) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
            else
                return Update<RepastSellbill, RequestRepastSellbill>(sellbill, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        #endregion 销售台账

        #region 进销台账

        /// <summary>
        /// 进销台账分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<Object> GetSellBuyPage(PageParamList<RequestRepastSellbill> pageParam)
        {
            IQueryable<RepastSellbill> queryable = Kily.Set<RepastSellbill>().Where(t => t.IsDelete == false && t.CreateTime > DateTime.Now.AddDays(-3)).AsNoTracking();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.GoodsName))
                queryable = queryable.Where(t => t.GoodsName.Contains(pageParam.QueryParam.GoodsName));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseRepastSellbill()
            {
                Id = t.Id,
                GoodsName = t.GoodsName,
                GoodsNum = t.GoodsNum,
                ToPay = t.ToPay,
                SellTime = t.SellTime,
                UnPay = t.UnPay,
                Manager = t.Manager
            }).ToList();

            IQueryable<RepastBuybill> Buyqueryable = Kily.Set<RepastBuybill>().Where(t => t.IsDelete == false && t.CreateTime > DateTime.Now.AddDays(-3)).AsNoTracking();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.GoodsName))
                Buyqueryable = Buyqueryable.Where(t => t.GoodsName.Contains(pageParam.QueryParam.GoodsName));
            if (MerchantInfo() != null)
                Buyqueryable = Buyqueryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                Buyqueryable = Buyqueryable.Where(t => t.InfoId == MerchantUser().Id);
            var Buydata = Buyqueryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseRepastBuybill()
            {
                Id = t.Id,
                GoodsName = t.GoodsName,
                GoodsNum = t.GoodsNum,
                LinkPhone = t.LinkPhone,
                Purchase = t.Purchase,
                Unit = t.Unit,
                ToPay = t.ToPay,
                Supplier = t.Supplier,
                OrderTime = t.OrderTime,
                UnPay = t.UnPay
            }).ToList();

            var List = new List<dynamic>();
            foreach (var item in data)
            {
                List.Add(new
                {
                    Id = item.Id,
                    GoodsName = item.GoodsName,
                    GoodsNum = item.GoodsNum,
                    LinkPhone = "",
                    Type = "销售",
                    Purchase = "",
                    Unit = "",
                    ToPay = item.ToPay,
                    Supplier = "",
                    OrderTime = item.SellTime,
                    UnPay = item.UnPay
                });
            }
            foreach (var item in Buydata)
            {
                List.Add(new
                {
                    Id = item.Id,
                    GoodsName = item.GoodsName,
                    GoodsNum = item.GoodsNum,
                    LinkPhone = item.LinkPhone,
                    Purchase = item.Purchase,
                    Type = "采购",
                    Unit = item.Unit,
                    ToPay = item.ToPay,
                    Supplier = item.Supplier,
                    OrderTime = item.OrderTime,
                    UnPay = item.UnPay
                });
            }
            return List.ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
        }

        #endregion 进销台账

        #region 实时监控

        /// <summary>
        /// 视频分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastVideo> GetVideoPage(PageParamList<RequestRepastVideo> pageParam)
        {
            IQueryable<RepastVideo> queryable = Kily.Set<RepastVideo>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            if (pageParam.QueryParam.IsIndex.HasValue)
                queryable = queryable.Where(t => t.IsIndex == pageParam.QueryParam.IsIndex);
            var data = queryable.Select(t => new ResponseRepastVideo()
            {
                Id = t.Id,
                MonitorAddress = t.MonitorAddress,
                CoverPhoto = t.CoverPhoto,
                VideoAddress = t.VideoAddress,
                IsIndex = t.IsIndex
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 首页显示
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string ShowVideo(Guid Id, bool flag)
        {
            var Video = Kily.Set<RepastVideo>().Where(t => t.Id == Id).AsNoTracking().FirstOrDefault();
            Video.IsIndex = flag;
            return UpdateField(Video, "IsIndex") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        #endregion 实时监控

        #endregion 功能管理

        #region 菜品管理

        /// <summary>
        /// 菜品分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastDish> GetDishPage(PageParamList<RequestRepastDish> pageParam)
        {
            IQueryable<RepastDish> queryable = Kily.Set<RepastDish>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.DishName))
                queryable = queryable.Where(t => t.DishName.Contains(pageParam.QueryParam.DishName));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new ResponseRepastDish()
            {
                Id = t.Id,
                DishName = t.DishName,
                DishType = t.DishType,
                CookingTime = t.CookingTime,
                CookingType = t.CookingType
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 菜品详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseRepastDish GetDishDetail(Guid Id)
        {
            var data = Kily.Set<RepastDish>().Where(t => t.Id == Id).Select(t => new ResponseRepastDish()
            {
                Id = t.Id,
                InfoId = t.InfoId,
                DishName = t.DishName,
                Batching = t.Batching,
                Seasoning = t.Seasoning,
                CookingTime = t.CookingTime,
                CookingType = t.CookingType,
                DishType = t.DishType,
                MainBatch = t.MainBatch,
                Remark = t.Remark,
                Taste = t.Taste
            }).FirstOrDefault();
            return data;
        }

        /// <summary>
        /// 周菜谱
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseFoodMenu> GetMerchantWeekPage(PageParamList<RequestFoodMenu> pageParam)
        {
            IQueryable<RepastFoodMenu> queryable = Kily.Set<RepastFoodMenu>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.FoodMenuName))
                queryable = queryable.Where(t => t.FoodMenuName.Contains(pageParam.QueryParam.FoodMenuName));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new ResponseFoodMenu()
            {
                Id = t.Id,
                FoodMenuName = t.FoodMenuName,
                Content = t.Content.Replace("<img", "<img style='max-width:100%' ").Replace("/Upload", "http://system.cfda.vip/Upload"),
                UpTime = t.UpTime,
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 编辑周菜谱
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public String EditFoodMenu(RequestFoodMenu Param)
        {
            RepastFoodMenu Menu = Param.MapToEntity<RepastFoodMenu>();
            return Insert(Menu) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        #endregion 菜品管理

        #region 溯源追踪

        #region 原料溯源

        /// <summary>
        /// 原料溯源分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastStuff> GetStuffPage(PageParamList<RequestRepastStuff> pageParam)
        {
            IQueryable<RepastStuff> queryable = Kily.Set<RepastStuff>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.MaterialName))
                queryable = queryable.Where(t => t.MaterialName.Contains(pageParam.QueryParam.MaterialName));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new ResponseRepastStuff()
            {
                Id = t.Id,
                MaterialName = t.MaterialName,
                MaterialType = t.MaterialType,
                Supplier = t.Supplier,
                ExpiredDay = t.ExpiredDay,
                Phone = t.Phone
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 删除溯源信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveStuff(Guid Id)
        {
            return Delete<RepastStuff>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 编辑溯源信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditStuff(RequestRepastStuff Param)
        {
            RepastStuff stuff = Param.MapToEntity<RepastStuff>();
            if (Param.Id == Guid.Empty)
                return Insert(stuff) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return Update(stuff, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        /// <summary>
        /// 获取溯源详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseRepastStuff GetStuffDetail(Guid Id)
        {
            var data = Kily.Set<RepastStuff>().Where(t => t.Id == Id).Select(t => new ResponseRepastStuff()
            {
                Id = t.Id,
                InfoId = t.InfoId,
                MaterialName = t.MaterialName,
                MaterialType = t.MaterialType,
                Supplier = t.Supplier,
                ExpiredDay = t.ExpiredDay,
                Phone = t.Phone,
                Address = t.Address,
                SourceLink = t.SourceLink,
                Aptitude = t.Aptitude,
                Standard = t.Standard,
                SuppTime = t.SuppTime,
                QualityReport = t.QualityReport,
                Remark = t.Remark
            }).FirstOrDefault();
            return data;
        }

        #endregion 原料溯源

        #region 留样管理

        /// <summary>
        /// 留样分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastSample> GetSamplePage(PageParamList<RequestRepastSample> pageParam)
        {
            IQueryable<RepastSample> queryable = Kily.Set<RepastSample>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.DishName))
                queryable = queryable.Where(t => t.DishName.Contains(pageParam.QueryParam.DishName));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new ResponseRepastSample()
            {
                Id = t.Id,
                DishName = t.DishName,
                Phone = t.Phone,
                SampleImg = t.SampleImg,
                SampleTime = t.SampleTime,
                OperatUser = t.OperatUser
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 编辑留样
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditSample(RequestRepastSample Param)
        {
            RepastSample sample = Param.MapToEntity<RepastSample>();
            return Insert(sample) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        /// <summary>
        /// 删除留样
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveSample(Guid Id)
        {
            return Delete<RepastSample>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        #endregion 留样管理

        #region 抽样管理

        /// <summary>
        /// 抽样分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastDraw> GetDrawPage(PageParamList<RequestRepastDraw> pageParam)
        {
            IQueryable<RepastDraw> queryable = Kily.Set<RepastDraw>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.DrawUnit))
                queryable = queryable.Where(t => t.DrawUnit.Contains(pageParam.QueryParam.DrawUnit));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new ResponseRepastDraw()
            {
                Id = t.Id,
                DrawUnit = t.DrawUnit,
                Phone = t.Phone,
                DrawTime = t.DrawTime,
                Remark = t.Remark,
                DrawUser = t.DrawUser
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 编辑抽样
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditDraw(RequestRepastDraw Param)
        {
            RepastDraw draw = Param.MapToEntity<RepastDraw>();
            return Insert(draw) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        /// <summary>
        /// 删除抽样
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveDraw(Guid Id)
        {
            return Delete<RepastDraw>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        #endregion 抽样管理

        #region 废物处理

        /// <summary>
        /// 废物分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastDuck> GetDuckPage(PageParamList<RequestRepastDuck> pageParam)
        {
            IQueryable<RepastDuck> queryable = Kily.Set<RepastDuck>().Where(t => t.IsDelete == false).OrderByDescending(t => t.HandleTime);
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new ResponseRepastDuck()
            {
                Id = t.Id,
                HandleWays = t.HandleWays,
                Phone = t.Phone,
                HandleImg = t.HandleImg,
                HandleTime = t.HandleTime,
                HandleUser = t.HandleUser
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 编辑废物
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditDuck(RequestRepastDuck Param)
        {
            RepastDuck duck = Param.MapToEntity<RepastDuck>();
            return Insert(duck) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        /// <summary>
        /// 删除废物
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveDuck(Guid Id)
        {
            return Remove<RepastDuck>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        #endregion 废物处理

        #region 消毒管理

        /// <summary>
        /// 消毒分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastDisinfect> GetDisinfectPage(PageParamList<RequestRepastDisinfect> pageParam)
        {
            IQueryable<RepastDisinfect> queryable = Kily.Set<RepastDisinfect>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.DisinfectName))
                queryable = queryable.Where(t => t.DisinfectName.Contains(pageParam.QueryParam.DisinfectName));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new ResponseRepastDisinfect()
            {
                Id = t.Id,
                DisinfectName = t.DisinfectName,
                SupplierName = t.SupplierName,
                UsePerson = t.UsePerson,
                SupplierTime = t.SupplierTime
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 编辑消毒
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditDisinfect(RequestRepastDisinfect Param)
        {
            RepastDisinfect disinfect = Param.MapToEntity<RepastDisinfect>();
            if (Param.Id == Guid.Empty)
                return Insert(disinfect) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return Update(disinfect, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        /// <summary>
        /// 删除消毒
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveDisinfect(Guid Id)
        {
            return Delete<RepastDisinfect>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 获取消毒详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseRepastDisinfect GetDisinfectDetail(Guid Id)
        {
            var data = Kily.Set<RepastDisinfect>().Where(t => t.Id == Id).Select(t => new ResponseRepastDisinfect()
            {
                Id = t.Id,
                InfoId = t.InfoId,
                DisinfectName = t.DisinfectName,
                DisinfectTime = t.DisinfectTime,
                SupplierTime = t.SupplierTime,
                Metering = t.Metering,
                SupplierName = t.SupplierName,
                UsePerson = t.UsePerson
            }).FirstOrDefault();
            return data;
        }

        #endregion 消毒管理

        #region 添加剂管理

        /// <summary>
        /// 添加剂分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastAdditive> GetAdditivePage(PageParamList<RequestRepastAdditive> pageParam)
        {
            IQueryable<RepastAdditive> queryable = Kily.Set<RepastAdditive>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.AdditiveName))
                queryable = queryable.Where(t => t.AdditiveName.Contains(pageParam.QueryParam.AdditiveName));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new ResponseRepastAdditive()
            {
                Id = t.Id,
                AdditiveName = t.AdditiveName,
                SupplierName = t.SupplierName,
                UsePerson = t.UsePerson,
                SupplierTime = t.SupplierTime
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 编辑添加剂
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveAdditive(RequestRepastAdditive Param)
        {
            RepastAdditive additive = Param.MapToEntity<RepastAdditive>();
            if (Param.Id == Guid.Empty)
                return Insert(additive) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return Update(additive, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        /// <summary>
        /// 删除添加剂
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveAdditive(Guid Id)
        {
            return Delete<RepastAdditive>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 获取添加剂详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseRepastAdditive GetAdditiveDetail(Guid Id)
        {
            var data = Kily.Set<RepastAdditive>().Where(t => t.Id == Id).Select(t => new ResponseRepastAdditive()
            {
                Id = t.Id,
                InfoId = t.InfoId,
                AdditiveName = t.AdditiveName,
                Function = t.Function,
                ProFood = t.ProFood,
                UseTime = t.UseTime,
                SupplierTime = t.SupplierTime,
                Metering = t.Metering,
                SupplierName = t.SupplierName,
                UsePerson = t.UsePerson
            }).FirstOrDefault();
            return data;
        }

        #endregion 添加剂管理

        #endregion 溯源追踪

        #region 仓库管理

        #region 原料仓库-入库

        /// <summary>
        /// 原料入库分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastInStorage> GetInStoragePage(PageParamList<RequestRepastInStorage> pageParam)
        {
            IQueryable<RepastInStorage> queryable = Kily.Set<RepastInStorage>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.IngredientName))
                queryable = queryable.Where(t => t.IngredientName.Contains(pageParam.QueryParam.IngredientName));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new ResponseRepastInStorage()
            {
                Id = t.Id,
                InfoId = t.InfoId,
                BatchNo = t.BatchNo,
                IngredientName = t.IngredientName,
                InStorageNum = t.InStorageNum,
                Supplier = t.Supplier,
                BuyUser = t.BuyUser
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 仓库原料列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseRepastInStorage> GetInStorageList(string Param)
        {
            var queryable = Kily.Set<RepastInStorage>().Where(t => t.IsDelete == false);
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Where(t => t.MaterType.Equals(Param))
                .Select(t => new ResponseRepastInStorage()
                {
                    Id = t.Id,
                    IngredientName = "(" + t.BatchNo + ")" + t.IngredientName,
                }).AsNoTracking().ToList();
            return data;
        }

        /// <summary>
        /// 编辑原料入库
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditInStorage(RequestRepastInStorage Param)
        {
            RepastInStorage storage = Param.MapToEntity<RepastInStorage>();
            if (Param.Id == Guid.Empty)
                return Insert<RepastInStorage>(storage) ? ServiceMessage.SAVENOTUPDATESUCCESS : ServiceMessage.INSERTFAIL;
            else
                return null;
        }

        /// <summary>
        /// 删除原料入库
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveInStorage(Guid Id)
        {
            return Delete(ExpressionExtension.GetExpression<RepastInStorage>("Id", Id, ExpressionEnum.Equals)) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 原料入库详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseRepastInStorage GetInStorageDetail(Guid Id)
        {
            var data = Kily.Set<RepastInStorage>().Where(t => t.Id == Id).Select(t => new ResponseRepastInStorage()
            {
                Id = t.Id,
                InfoId = t.InfoId,
                BatchNo = t.BatchNo,
                IngredientName = t.IngredientName,
                Address = t.Address,
                ToPrice = t.ToPrice,
                ExpiredDay = t.ExpiredDay,
                SuppTime = t.SuppTime,
                BuyUser = t.BuyUser,
                InStorageNum = t.InStorageNum,
                Phone = t.Phone,
                MaterType = t.MaterType,
                PrePrice = t.PrePrice,
                QualityReport = t.QualityReport,
                Remark = t.Remark,
                SourceLink = t.SourceLink,
                Supplier = t.Supplier
            }).AsNoTracking().FirstOrDefault();
            return data;
        }

        #endregion 原料仓库-入库

        #region 原料仓库-出库

        /// <summary>
        /// 出库分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastOutStorage> GetOutStoragePage(PageParamList<RequestRepastOutStorage> pageParam)
        {
            IQueryable<RepastOutStorage> queryable = Kily.Set<RepastOutStorage>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<RepastInStorage> queryables = Kily.Set<RepastInStorage>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.IngredientName))
                queryable = queryable.Where(t => t.IngredientName.Contains(pageParam.QueryParam.IngredientName));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Join(queryables, t => t.InStorageId, x => x.Id, (t, x) => new ResponseRepastOutStorage()
            {
                Id = t.Id,
                BatchNo = t.BatchNo,
                InBatchNo = x.BatchNo,
                IngredientName = t.IngredientName,
                InStorageEx = x.InStorageNum,
                OutStorageNum = t.OutStorageNum,
                OutStorageTime = t.OutStorageTime,
                OutUser = t.OutUser
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 编辑出库
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditOutStorage(RequestRepastOutStorage Param)
        {
            if (Param.OutStorageNum < 0)
                return "出库数量必须大于0";
            RepastInStorage inStorage = Kily.Set<RepastInStorage>().Where(t => t.IsDelete == false).Where(t => t.Id == Param.InStorageId).FirstOrDefault();
            if (inStorage.InStorageNum - Param.OutStorageNum < 0)
                return "当前库存少于出库量";
            inStorage.InStorageNum -= Param.OutStorageNum;
            RepastOutStorage outStorage = Param.MapToEntity<RepastOutStorage>();
            return Insert(outStorage) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        /// <summary>
        /// 删除出库
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveOutStorage(Guid Id)
        {
            return Delete<RepastOutStorage>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        #endregion 原料仓库-出库

        #region 物品仓库-入库

        /// <summary>
        /// 物品入库分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastArticleInStock> GetInStockPage(PageParamList<RequestRepastArticleInStock> pageParam)
        {
            IQueryable<RepastArticleInStock> queryable = Kily.Set<RepastArticleInStock>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.ArticleName))
                queryable = queryable.Where(t => t.ArticleName.Contains(pageParam.QueryParam.ArticleName));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new ResponseRepastArticleInStock()
            {
                Id = t.Id,
                InfoId = t.InfoId,
                ArticleName = t.ArticleName,
                BatchNo = t.BatchNo,
                Supplier = t.Supplier,
                Phone = t.Phone,
                InStockNum = t.InStockNum
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 删除物品入库
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveInStock(Guid Id)
        {
            return Delete<RepastArticleInStock>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 编辑物品入库
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditInStock(RequestRepastArticleInStock Param)
        {
            RepastArticleInStock stock = Param.MapToEntity<RepastArticleInStock>();
            if (Param.Id == Guid.Empty)
                return Insert(stock) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return null;
        }

        /// <summary>
        /// 获取物品入库详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseRepastArticleInStock GetInStockDetail(Guid Id)
        {
            var data = Kily.Set<RepastArticleInStock>().Where(t => t.Id == Id).Select(t => new ResponseRepastArticleInStock()
            {
                Id = t.Id,
                InfoId = t.InfoId,
                ArticleName = t.ArticleName,
                BatchNo = t.BatchNo,
                Supplier = t.Supplier,
                Phone = t.Phone,
                InStockNum = t.InStockNum,
                Address = t.Address,
                BuyUser = t.BuyUser,
                ToPrice = t.ToPrice,
                PrePrice = t.PrePrice,
                Remark = t.Remark
            }).AsNoTracking().FirstOrDefault();
            return data;
        }

        #endregion 物品仓库-入库

        #region 物品仓库-出库

        /// <summary>
        /// 物品出库分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastArticleOutStock> GetOutStockPage(PageParamList<RequestRepastArticleOutStock> pageParam)
        {
            IQueryable<RepastArticleOutStock> queryable = Kily.Set<RepastArticleOutStock>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<RepastArticleInStock> queryables = Kily.Set<RepastArticleInStock>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.ArticleName))
                queryable = queryable.Where(t => t.ArticleName.Contains(pageParam.QueryParam.ArticleName));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Join(queryables, t => t.InStockId, x => x.Id, (t, x) => new ResponseRepastArticleOutStock()
            {
                Id = t.Id,
                ArticleName = t.ArticleName,
                BatchNo = t.BatchNo,
                InBatchNo = x.BatchNo,
                OutStockNum = t.OutStockNum,
                OutStockTime = t.OutStockTime,
                OutUser = t.OutUser,
                StockEx = x.InStockNum
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 删除物品出库
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveOutStock(Guid Id)
        {
            return Delete<RepastArticleOutStock>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 编辑物品出库
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditOutStock(RequestRepastArticleOutStock Param)
        {
            if (Param.OutStockNum < 0)
                return "出库数量必须大于0";
            RepastArticleInStock inStock = Kily.Set<RepastArticleInStock>().Where(t => t.Id == Param.InStockId).FirstOrDefault();
            if (inStock.InStockNum - Param.OutStockNum < 0)
                return "当前库存少于出库量";
            inStock.InStockNum -= Param.OutStockNum;
            RepastArticleOutStock outStock = Param.MapToEntity<RepastArticleOutStock>();
            return Insert(outStock) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        #endregion 物品仓库-出库

        #region 库存报表

        /// <summary>
        /// 原料报表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastStockReport> GetStorageReportPage(PageParamList<RequestRepastStockReport> pageParam)
        {
            IQueryable<RepastInStorage> queryable = Kily.Set<RepastInStorage>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<RepastOutStorage> queryables = Kily.Set<RepastOutStorage>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.Name))
                queryable = queryable.Where(t => t.IngredientName.Contains(pageParam.QueryParam.Name));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Join(queryables, t => t.Id, x => x.InStorageId, (t, x) => new
            {
                t.IngredientName,
                t.InStorageNum,
                x.OutStorageNum
            }).GroupBy(t => t.IngredientName).Select(t => new ResponseRepastStockReport()
            {
                Name = t.FirstOrDefault().IngredientName,
                InStock = t.Sum(x => x.InStorageNum),
                OutStock = t.Sum(x => x.OutStorageNum)
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 物品报表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastStockReport> GetStockReportPage(PageParamList<RequestRepastStockReport> pageParam)
        {
            IQueryable<RepastArticleInStock> queryable = Kily.Set<RepastArticleInStock>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<RepastArticleOutStock> queryables = Kily.Set<RepastArticleOutStock>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.Name))
                queryable = queryable.Where(t => t.ArticleName.Contains(pageParam.QueryParam.Name));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Join(queryables, t => t.Id, x => x.InStockId, (t, x) => new
            {
                t.ArticleName,
                t.InStockNum,
                x.OutStockNum
            }).GroupBy(t => t.ArticleName).Select(t => new ResponseRepastStockReport()
            {
                Name = t.FirstOrDefault().ArticleName,
                InStock = t.Sum(x => x.InStockNum),
                OutStock = t.Sum(x => x.OutStockNum)
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        #endregion 库存报表

        #region 名称管理

        /// <summary>
        /// 名称分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastTypeName> GetNamesPage(PageParamList<RequestRepastTypeName> pageParam)
        {
            IQueryable<RepastTypeName> queryable = Kily.Set<RepastTypeName>().OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.TypeNames))
                queryable = queryable.Where(t => t.TypeNames.Contains(pageParam.QueryParam.TypeNames));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new ResponseRepastTypeName()
            {
                Id = t.Id,
                TypeNames = t.TypeNames,
                Types = t.Types,
                Spec = t.Spec
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize); ;
            return data;
        }

        /// <summary>
        /// 编辑名称
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditNames(RequestRepastTypeName Param)
        {
            RepastTypeName typeName = Param.MapToEntity<RepastTypeName>();
            IQueryable<RepastTypeName> queryable = Kily.Set<RepastTypeName>();
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id).Where(t => t.TypeNames.Equals(typeName.TypeNames) && t.Types == 1);
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().InfoId).Where(t => t.TypeNames.Equals(typeName.TypeNames) && t.Types == 1);
            if (queryable.AsNoTracking().FirstOrDefault() != null)
                return "原料名称已经存在请勿重复添加";
            return Insert(typeName) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        /// <summary>
        /// 删除名称
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveNames(Guid Id)
        {
            return Remove<RepastTypeName>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 名称列表
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public IList<ResponseRepastTypeName> GetNamesList(int Key)
        {
            IQueryable<RepastTypeName> queryable = Kily.Set<RepastTypeName>().OrderByDescending(t => t.CreateTime);
            if (Key == 0)
                queryable = queryable.Where(t => t.Types == 2 || t.Types == 3);
            else
                queryable = queryable.Where(t => t.Types == Key);
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            return queryable.Select(t => new ResponseRepastTypeName()
            {
                Id = t.Id,
                TypeNames = $"{t.TypeNames}({t.Spec})"
            }).ToList();
        }

        #endregion 名称管理

        #endregion 仓库管理

        #region 陪餐管理

        /// <summary>
        /// 陪餐列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ReponseRepastUnitIns> GetUnitInsPage(PageParamList<RequestRepastUnitIns> pageParam)
        {
            IQueryable<RepastUnitIns> queryable = Kily.Set<RepastUnitIns>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.InsName))
                queryable = queryable.Where(t => t.InsName == pageParam.QueryParam.InsName);
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new ReponseRepastUnitIns()
            {
                Id = t.Id,
                InfoId = t.InfoId,
                InsName = t.InsName,
                InsTime = t.InsTime
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 删除陪餐
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteUnitIns(Guid Id)
        {
            return Remove<RepastUnitIns>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 编辑陪餐
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveUnitIns(RequestRepastUnitIns Param)
        {
            RepastUnitIns Ins = Param.MapToEntity<RepastUnitIns>();
            if (Ins.Id == Guid.Empty)
                return Insert(Ins) ? ServiceMessage.HANDLESUCCESS : ServiceMessage.HANDLEFAIL;
            else
                return Update(Param, Ins) ? ServiceMessage.HANDLESUCCESS : ServiceMessage.HANDLEFAIL;
        }

        /// <summary>
        /// 陪餐详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ReponseRepastUnitIns GetUnitInsDetail(Guid Id)
        {
            return Kily.Set<RepastUnitIns>().Where(t => t.Id == Id).FirstOrDefault().MapToEntity<ReponseRepastUnitIns>();
        }

        /// <summary>
        /// 陪餐记录
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ReponseRepastUnitInsRecord> GetUnitInsRecordPage(PageParamList<RequestRepastUnitInsRecord> pageParam)
        {
            IQueryable<RepastUnitInsRecord> queryable = Kily.Set<RepastUnitInsRecord>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.InsTheme))
                queryable = queryable.Where(t => t.InsTheme == pageParam.QueryParam.InsTheme);
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new ReponseRepastUnitInsRecord()
            {
                Id = t.Id,
                InfoId = t.InfoId,
                InsTheme = t.InsTheme,
                InsUser = t.InsUser,
                InsContent = t.InsContent.Replace("/Upload/", "http://system.cfda.vip/Upload/"),
                InsTime = t.InsTime
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 删除陪餐记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteUnitInsRecord(Guid Id)
        {
            return Remove<RepastUnitInsRecord>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 编辑陪餐记录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveUnitInsRecord(RequestRepastUnitInsRecord Param)
        {
            RepastUnitInsRecord Ins = Param.MapToEntity<RepastUnitInsRecord>();
            if (Ins.Id == Guid.Empty)
                return Insert(Ins) ? ServiceMessage.HANDLESUCCESS : ServiceMessage.HANDLEFAIL;
            else
                return Update(Ins, Param) ? ServiceMessage.HANDLESUCCESS : ServiceMessage.HANDLEFAIL;
        }

        /// <summary>
        /// 陪餐记录详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ReponseRepastUnitInsRecord GetUnitInsRecordDetail(Guid Id)
        {
            return Kily.Set<RepastUnitInsRecord>().Where(t => t.Id == Id).FirstOrDefault().MapToEntity<ReponseRepastUnitInsRecord>();
        }

        #endregion 陪餐管理

        #region 微信和支付宝调用

        /// <summary>
        /// 版本续费和升级使用支付宝支付
        /// </summary>
        /// <param name="Key">年限</param>
        /// <param name="Value">版本</param>
        /// <returns></returns>
        public string AliPay(int Key, int? Value)
        {
            if (MerchantInfo() == null)
                return "请使用企业账户进行操作！";
            RepastInfo info = Kily.Set<RepastInfo>().Where(t => t.Id == MerchantInfo().Id).FirstOrDefault();
            RequestAliPayModel AliPayModel = new RequestAliPayModel();
            AliPayModel.OrderTitle = MerchantInfo().MerchantName + (Value == null ? "版本续费" : "版本升级");
            if ((Value == null ? info.VersionType : (SystemVersionEnum)(Value)) == SystemVersionEnum.Test)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    AliPayModel.Money = ConfigMoney.RepastTest * Key;
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    AliPayModel.Money = ConfigMoney.UnitCanteenTest * Key;
            }
            if ((Value == null ? info.VersionType : (SystemVersionEnum)(Value)) == SystemVersionEnum.Base)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    AliPayModel.Money = ConfigMoney.RepastBase * Key;
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    AliPayModel.Money = ConfigMoney.UnitCanteenBase * Key;
            }
            if ((Value == null ? info.VersionType : (SystemVersionEnum)(Value)) == SystemVersionEnum.Level)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    AliPayModel.Money = ConfigMoney.RepastLv * Key;
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    AliPayModel.Money = ConfigMoney.UnitCanteenLv * Key;
            }
            if ((Value == null ? info.VersionType : (SystemVersionEnum)(Value)) == SystemVersionEnum.Enterprise)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    AliPayModel.Money = ConfigMoney.RepastEnterprise * Key;
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    AliPayModel.Money = ConfigMoney.UnitCanteenEnterprise * Key;
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
            if (MerchantInfo() == null)
                return "请使用企业账户进行操作！";
            RepastInfo info = Kily.Set<RepastInfo>().Where(t => t.Id == MerchantInfo().Id).FirstOrDefault();
            RequestWxPayModel WxPayModel = new RequestWxPayModel();
            WxPayModel.OrderTitle = MerchantInfo().MerchantName + (Value == null ? "版本续费" : "版本升级");
            if ((Value == null ? info.VersionType : (SystemVersionEnum)(Value)) == SystemVersionEnum.Test)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    WxPayModel.Money = ConfigMoney.RepastTest * Key;
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    WxPayModel.Money = ConfigMoney.UnitCanteenTest * Key;
            }
            if ((Value == null ? info.VersionType : (SystemVersionEnum)(Value)) == SystemVersionEnum.Base)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    WxPayModel.Money = ConfigMoney.RepastBase * Key;
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    WxPayModel.Money = ConfigMoney.UnitCanteenBase * Key;
            }
            if ((Value == null ? info.VersionType : (SystemVersionEnum)(Value)) == SystemVersionEnum.Level)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    WxPayModel.Money = ConfigMoney.RepastLv * Key;
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    WxPayModel.Money = ConfigMoney.UnitCanteenLv * Key;
            }
            if ((Value == null ? info.VersionType : (SystemVersionEnum)(Value)) == SystemVersionEnum.Enterprise)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    WxPayModel.Money = ConfigMoney.RepastEnterprise * Key;
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    WxPayModel.Money = ConfigMoney.UnitCanteenEnterprise * Key;
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
                    RepastInfo Info = Kily.Set<RepastInfo>().Where(t => t.Id == Param.MerchantId).FirstOrDefault();
                    PayInfo.PayDes = "SUCCESS";
                    Info.VersionType = Param.VersionType;
                    if (string.IsNullOrEmpty(PayInfo.PayDes))
                    {
                        UpdateField(PayInfo, "PayDes");
                        UpdateField(Info, "VersionType");
                    }
                    return "http://system.cfda.vip/StaticHtml/WxNotify.html";
                }
                else
                    return null;
            }
            else
                return null;
        }

        #endregion 微信和支付宝调用

        #region 导出Excel

        /// <summary>
        /// 食材入库Excel导出
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public IList<Object> ExportStuffInStockFile(String Param)
        {
            IQueryable<RepastInStorage> queryable = Kily.Set<RepastInStorage>().Where(t => t.IsDelete == false);
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
                食材名称 = t.IngredientName,
                入库批次 = t.BatchNo,
                入库数量 = t.InStorageNum,
                供应时间 = t.SuppTime,
                负责人 = t.BuyUser,
                供应商 = t.Supplier
            }).ToList<Object>();
            return data;
        }

        /// <summary>
        /// 食材出库Excel导出
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public IList<Object> ExportStuffOutStockFile(String Param)
        {
            IQueryable<RepastOutStorage> queryable = Kily.Set<RepastOutStorage>().Where(t => t.IsDelete == false);
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
                出库批次 = t.BatchNo,
                食材名称 = t.IngredientName,
                出库数量 = t.OutStorageNum,
                出库时间 = t.OutStorageTime,
                负责人 = t.OutUser
            }).ToList<Object>();
            return data;
        }

        /// <summary>
        /// 物品入库Excel导出
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public IList<Object> ExportGoodsInStockFile(String Param)
        {
            IQueryable<RepastArticleInStock> queryable = Kily.Set<RepastArticleInStock>().Where(t => t.IsDelete == false);
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
                产品名称 = t.ArticleName,
                入库批次 = t.BatchNo,
                供应商 = t.Supplier,
                入库数量 = t.InStockNum,
                负责人 = t.BuyUser
            }).ToList<Object>();
            return data;
        }

        /// <summary>
        /// 物品出库Excel导出
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public IList<Object> ExportGoodsOutStockFile(String Param)
        {
            IQueryable<RepastArticleOutStock> queryable = Kily.Set<RepastArticleOutStock>().Where(t => t.IsDelete == false);
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
                出库批次 = t.BatchNo,
                物品名称 = t.ArticleName,
                出库数量 = t.OutStockNum,
                出库时间 = t.OutStockTime,
                负责人 = t.OutUser,
            }).ToList<Object>();
            return data;
        }

        #endregion 导出Excel

        #region 数据统计

        /// <summary>
        /// 数据统计
        /// </summary>
        /// <returns></returns>
        public Object GetDataCount(Guid? Id)
        {
            var TypePath = Kily.Set<RepastInfo>().Where(t => t.Id == Id).Select(t => t.TypePath).FirstOrDefault();
            var bill = Kily.Set<RepastBillTicket>().Where(t => t.IsDelete == false).Where(t => t.InfoId == Id).Count();
            var supplier = Kily.Set<RepastSupplier>().Where(t => t.IsDelete == false).Where(t => t.InfoId == Id).Count();
            var record = Kily.Set<GovtTemplateChild>().Where(t => t.IsDelete == false).Where(t => t.TypePath == TypePath).Count();
            var touser = Kily.Set<RepastInfoUser>().Where(t => t.IsDelete == false).Where(t => t.InfoId == Id).Count();
            var exp = Kily.Set<RepastInfoUser>().Where(t => t.IsDelete == false).Where(t => t.InfoId == Id).Where(t => t.ExpiredTime <= DateTime.Now).Count();
            Object data = new { bill, supplier, record, touser, exp };
            return data;
        }

        /// <summary>
        /// 数据统计
        /// </summary>
        /// <returns></returns>
        public Object GetLineData()
        {
            IQueryable<GovtTemplateChild> GovtTemplateChilds = Kily.Set<GovtTemplateChild>().OrderByDescending(t => t.CreateTime);
            IQueryable<RepastUnitIns> RepastUnitInss = Kily.Set<RepastUnitIns>().OrderByDescending(t => t.CreateTime);
            IQueryable<RepastSample> RepastSamples = Kily.Set<RepastSample>().OrderByDescending(t => t.CreateTime);
            IQueryable<RepastDuck> RepastDucks = Kily.Set<RepastDuck>().OrderByDescending(t => t.HandleTime);
            if (MerchantInfo() != null)
            {
                GovtTemplateChilds = GovtTemplateChilds.Where(t => t.TypePath.Contains(MerchantInfo().TypePath));
                RepastUnitInss = RepastUnitInss.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
                RepastSamples = RepastSamples.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
                RepastDucks = RepastDucks.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            }
            else
            {
                RepastUnitInss = RepastUnitInss.Where(t => t.InfoId == MerchantUser().Id);
                RepastSamples = RepastSamples.Where(t => t.InfoId == MerchantUser().Id);
                RepastDucks = RepastDucks.Where(t => t.InfoId == MerchantUser().Id);
                GovtTemplateChilds = GovtTemplateChilds.Where(t => t.TypePath.Contains(MerchantUser().TypePath));
            }
            List<int> 自查 = new List<int> {
            GovtTemplateChilds.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==6).Count(),
            GovtTemplateChilds.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==5).Count(),
            GovtTemplateChilds.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==4).Count(),
            GovtTemplateChilds.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==3).Count(),
            GovtTemplateChilds.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==2).Count(),
            GovtTemplateChilds.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==1).Count(),
            GovtTemplateChilds.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==0).Count(),
            };
            List<int> 配餐 = new List<int> {
            RepastUnitInss.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==6).Count(),
            RepastUnitInss.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==5).Count(),
            RepastUnitInss.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==4).Count(),
            RepastUnitInss.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==3).Count(),
            RepastUnitInss.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==2).Count(),
            RepastUnitInss.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==1).Count(),
            RepastUnitInss.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==0).Count(),
            };
            List<int> 留样 = new List<int> {
            RepastSamples.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==6).Count(),
            RepastSamples.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==5).Count(),
            RepastSamples.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==4).Count(),
            RepastSamples.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==3).Count(),
            RepastSamples.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==2).Count(),
            RepastSamples.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==1).Count(),
            RepastSamples.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==0).Count(),
            };
            List<int> 废物 = new List<int> {
            RepastDucks.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==6).Count(),
            RepastDucks.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==5).Count(),
            RepastDucks.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==4).Count(),
            RepastDucks.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==3).Count(),
            RepastDucks.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==2).Count(),
            RepastDucks.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==1).Count(),
            RepastDucks.Where(t=>t.CreateTime.Value.Day-DateTime.Now.Day==0).Count(),
            };
            return new { 自查, 配餐, 留样, 废物 };
        }

        #endregion 数据统计

        #region 扫码信息

        /// <summary>
        /// 信息分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastScanInfo> GetScanInfoPage(PageParamList<RequestRepastScanInfo> pageParam)
        {
            IQueryable<RepastScanInfo> queryable = Kily.Set<RepastScanInfo>().OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.RecordName))
                queryable = queryable.Where(t => t.RecordName.Contains(pageParam.QueryParam.RecordName));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new ResponseRepastScanInfo()
            {
                Id = t.Id,
                InfoId = t.InfoId,
                RecordName = t.RecordName,
                ShowTime = t.ShowTime,
                IsDelete = t.IsDelete
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 编辑信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveScanInfo(RequestRepastScanInfo Param)
        {
            RepastScanInfo Scan = Param.MapToEntity<RepastScanInfo>();
            if (Param.Id == Guid.Empty)
            {
                RepastScanInfo info = Kily.Set<RepastScanInfo>().Where(t => t.InfoId == Param.InfoId).AsNoTracking().FirstOrDefault();
                if (info != null)
                    return "每个商家只能生成一个二维码";
                return Insert(Scan) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
            else
                return Update(Scan, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string RemoveScan(Guid Id, bool? Param)
        {
            if (Param.HasValue)
            {
                var Temp = Kily.Set<RepastScanInfo>().Where(t => t.Id == Id).FirstOrDefault();
                Temp.IsDelete = Param;
                return UpdateField<RepastScanInfo>(Temp, "IsDelete") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            }
            else
            {
                return Remove<RepastScanInfo>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
            }
        }

        /// <summary>
        /// 信息详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseRepastScanInfo GetScanInfoDetail(Guid Id)
        {
            var data = Kily.Set<RepastScanInfo>().Where(t => t.Id == Id).AsNoTracking().FirstOrDefault().MapToEntity<ResponseRepastScanInfo>();
            return data;
        }

        #endregion 扫码信息

        #region 列表集合

        /// <summary>
        /// 菜品列表
        /// </summary>
        /// <returns></returns>
        public Object GetDishList()
        {
            IQueryable<RepastDish> queryable = Kily.Set<RepastDish>().Where(t => t.IsDelete == false);
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new
            {
                t.Id,
                Name = t.DishName
            }).ToList();
            return data;
        }

        /// <summary>
        /// 原料列表
        /// </summary>
        /// <returns></returns>
        public Object GetStuffList()
        {
            IQueryable<RepastStuff> queryable = Kily.Set<RepastStuff>().Where(t => t.IsDelete == false);
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new
            {
                t.Id,
                Name = t.MaterialName
            }).ToList();
            return data;
        }

        /// <summary>
        /// 视频列表
        /// </summary>
        /// <returns></returns>
        public Object GetVideoList()
        {
            IQueryable<RepastVideo> queryable = Kily.Set<RepastVideo>().Where(t => t.IsDelete == false);
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new
            {
                t.Id,
                Name = t.MonitorAddress
            }).ToList();
            return data;
        }

        /// <summary>
        /// 人员列表
        /// </summary>
        /// <returns></returns>
        public Object GetUserList()
        {
            IQueryable<RepastInfoUser> queryable = Kily.Set<RepastInfoUser>().Where(t => t.IsDelete == false).AsNoTracking();
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new
            {
                t.Id,
                Name = t.TrueName
            }).ToList();
            return data;
        }

        /// <summary>
        /// 废物列表
        /// </summary>
        /// <returns></returns>
        public Object GetDuckList()
        {
            IQueryable<RepastDuck> queryable = Kily.Set<RepastDuck>().Where(t => t.IsDelete == false).AsNoTracking();
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new
            {
                t.Id,
                Name = t.HandleWays + "-" + t.HandleTime.Value.ToString("yyyy-MM-dd")
            }).ToList();
            return data;
        }

        /// <summary>
        /// 抽样列表
        /// </summary>
        /// <returns></returns>
        public Object GetDrawList()
        {
            IQueryable<RepastDraw> queryable = Kily.Set<RepastDraw>().Where(t => t.IsDelete == false).AsNoTracking();
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new
            {
                t.Id,
                Name = t.DrawUnit + "-" + t.DrawTime.Value.ToString("yyyy-MM-dd")
            }).ToList();
            return data;
        }

        /// <summary>
        /// 留样列表
        /// </summary>
        /// <returns></returns>
        public Object GetSampleList()
        {
            IQueryable<RepastSample> queryable = Kily.Set<RepastSample>().Where(t => t.IsDelete == false).AsNoTracking();
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new
            {
                t.Id,
                Name = t.DishName + "-" + t.SampleTime.Value.ToString("yyyy-MM-dd")
            }).ToList();
            return data;
        }

        /// <summary>
        /// 消毒列表
        /// </summary>
        /// <returns></returns>
        public Object GetDisinfectList()
        {
            IQueryable<RepastDisinfect> queryable = Kily.Set<RepastDisinfect>().Where(t => t.IsDelete == false).AsNoTracking();
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new
            {
                t.Id,
                Name = t.DisinfectName + "-" + t.DisinfectTime.Value.ToString("yyyy-MM-dd")
            }).ToList();
            return data;
        }

        /// <summary>
        /// 添加剂列表
        /// </summary>
        /// <returns></returns>
        public Object GetAdditiveList()
        {
            IQueryable<RepastAdditive> queryable = Kily.Set<RepastAdditive>().Where(t => t.IsDelete == false).AsNoTracking();
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new
            {
                t.Id,
                Name = t.AdditiveName + "-" + t.UseTime.Value.ToString("yyyy-MM-dd")
            }).ToList();
            return data;
        }

        /// <summary>
        /// 台账列表
        /// </summary>
        /// <returns></returns>
        public Object GetTicketList()
        {
            IQueryable<RepastBillTicket> queryable = Kily.Set<RepastBillTicket>().Where(t => t.IsDelete == false).AsNoTracking();
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new
            {
                t.Id,
                Name = t.Theme + "-" + t.UpTime.Value.ToString("yyyy-MM-dd")
            }).ToList();
            return data;
        }

        /// <summary>
        /// 周菜谱列表
        /// </summary>
        /// <returns></returns>
        public Object GetWeekMenuList()
        {
            IQueryable<RepastFoodMenu> queryable = Kily.Set<RepastFoodMenu>().Where(t => t.IsDelete == false).AsNoTracking();
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new
            {
                t.Id,
                Name = t.FoodMenuName + "-" + t.UpTime.Value.ToString("yyyy-MM-dd")
            }).ToList();
            return data;
        }

        #endregion 列表集合

        #region 手机端扫码信息

        /// <summary>
        /// 商家的二维码
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseRepastScanInfos GetMobileScanInfo(Guid Id)
        {
            SqlParameter[] Param = {
             new SqlParameter("@Id", Id)
            };
            var data = Kily.Execute("Sp_GetScanMerchantInfo", Param).ToCollection<ResponseRepastScanInfos>().FirstOrDefault();
            return data;
        }

        #endregion 手机端扫码信息

        #region APP接口

        /// <summary>
        /// 督查信息
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseSystemMessage> GetNetPatrolPage(PageParamList<Object> pageParam)
        {
            IQueryable<SystemMessage> queryable = Kily.Set<SystemMessage>().OrderByDescending(t => t.ReleaseTime);
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == MerchantInfo().Id);
            else
                queryable = queryable.Where(t => t.CompanyId == MerchantUser().Id);
            var data = queryable.Select(t => new ResponseSystemMessage()
            {
                Id = t.Id,
                MsgName = t.MsgName,
                MsgContent = t.MsgContent,
                Status = t.Status,
                ReleaseTime = t.ReleaseTime
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 投诉建议
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtComplain> GetComplainPage(PageParamList<RequestGovtComplain> pageParam)
        {
            IQueryable<GovtComplain> queryable = Kily.Set<GovtComplain>().OrderByDescending(t => t.ComplainTime);
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == MerchantInfo().Id);
            else
                queryable = queryable.Where(t => t.CompanyId == MerchantUser().Id);
            var data = queryable.Select(t => new ResponseGovtComplain()
            {
                Id = t.Id,
                ComplainUser = t.ComplainUser,
                ComplainContent = t.ComplainContent,
                ComplainUserPhone = t.ComplainUserPhone,
                HandlerContent = t.HandlerContent,
                Status = t.Status,
                ComplainTime = t.ComplainTime
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 风险预警
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtRisk> GetWaringRiskPage(PageParamList<RequestGovtRisk> pageParam)
        {
            IQueryable<GovtRisk> queryable = Kily.Set<GovtRisk>().OrderByDescending(t => t.CreateTime);
            queryable = queryable.Where(t => t.TypePath.Contains(MerchantInfo().City));
            if (!string.IsNullOrEmpty(pageParam.QueryParam.EventName))
                queryable = queryable.Where(t => t.EventName.Contains(pageParam.QueryParam.EventName));
            var data = queryable.Select(t => new ResponseGovtRisk()
            {
                Id = t.Id,
                EventName = t.EventName,
                TradeType = t.TradeType,
                WaringLv = t.WaringLv,
                ReleaseTime = t.ReleaseTime,
                ReportPlay = t.ReportPlay,
                Remark = t.Remark,
                Desc = t.Remark.NoHtml(),
                TypePath = t.TypePath
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        #endregion APP接口
    }
}