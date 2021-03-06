﻿using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.Model.Cook;
using KilyCore.EntityFrameWork.Model.Enterprise;
using KilyCore.EntityFrameWork.Model.Function;
using KilyCore.EntityFrameWork.Model.Govt;
using KilyCore.EntityFrameWork.Model.Repast;
using KilyCore.EntityFrameWork.Model.System;
using KilyCore.EntityFrameWork.ModelEnum;
using KilyCore.Extension.AttributeExtension;
using KilyCore.Extension.AutoMapperExtension;
using KilyCore.Extension.PayCore.AliPay;
using KilyCore.Extension.PayCore.WxPay;
using KilyCore.Extension.UtilExtension;
using KilyCore.Quartz;
using KilyCore.Repositories.BaseRepository;
using KilyCore.Service.ConstMessage;
using KilyCore.Service.IServiceCore;
using KilyCore.Service.QueryExtend;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
namespace KilyCore.Service.ServiceCore
{
    public class SystemServcie : Repository, ISystemService
    {
        #region 菜单管理

        #region 系统菜单查询

        /// <summary>
        /// 菜单分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseMenu> GetMenuPage(PageParamList<RequestMenu> pageParam)
        {
            var query = Kily.Set<SystemMenu>().Where(t => t.IsDelete == false).AsNoTracking().AsQueryable();
            if (!String.IsNullOrEmpty(pageParam.QueryParam.MenuName))
                query = query.Where(t => t.MenuName.Contains(pageParam.QueryParam.MenuName));
            var data = query.OrderByDescending(t => t.CreateTime).Select(t => new ResponseMenu()
            {
                Id = t.Id,
                MenuId = t.MenuId,
                ParentId = t.ParentId,
                MenuAddress = t.MenuAddress,
                MenuName = t.MenuName,
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 获取左侧菜单列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseMenu> GetSystemMenu()
        {
            IQueryable<SystemMenu> queryable = Kily.Set<SystemMenu>().Where(t => t.Level == MenuEnum.LevelOne)
                   .Where(t => t.IsDelete == false).AsNoTracking().AsQueryable().OrderBy(t => t.CreateTime);
            if (UserInfo().AccountType == AccountEnum.Admin)
            {
                var data = queryable.OrderBy(t => t.CreateTime).Select(t => new ResponseMenu()
                {
                    Id = t.Id,
                    MenuId = t.MenuId,
                    ParentId = t.ParentId,
                    MenuAddress = t.MenuAddress,
                    MenuName = t.MenuName,
                    HasChildrenNode = t.HasChildrenNode,
                    MenuIcon = t.MenuIcon,
                    MenuChildren = Kily.Set<SystemMenu>()
                       .Where(x => x.ParentId == t.MenuId)
                       .Where(x => x.Level != MenuEnum.LevelOne)
                       .Where(x => x.IsDelete == false)
                       .OrderBy(x => x.CreateTime)
                       .Select(x => new ResponseMenu()
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
                //取权限菜单
                SystemRoleAuthor Author = Kily.Set<SystemRoleAuthor>().Where(t => t.Id == UserInfo().RoleAuthorType).AsNoTracking().FirstOrDefault();
                queryable = queryable.Where(t => Author.AuthorMenuPath.Contains(t.Id.ToString())).AsNoTracking().AsQueryable();
                var data = queryable.OrderBy(t => t.CreateTime).Select(t => new ResponseMenu()
                {
                    Id = t.Id,
                    MenuId = t.MenuId,
                    ParentId = t.ParentId,
                    MenuAddress = t.MenuAddress,
                    MenuName = t.MenuName,
                    HasChildrenNode = t.HasChildrenNode,
                    MenuIcon = t.MenuIcon,
                    MenuChildren = Kily.Set<SystemMenu>()
                    .Where(x => x.ParentId == t.MenuId)
                    .Where(x => x.Level != MenuEnum.LevelOne)
                    .Where(x => x.IsDelete == false)
                    .Where(x => Author.AuthorMenuPath.Contains(x.Id.ToString()))
                    .OrderBy(x => x.CreateTime)
                    .Select(x => new ResponseMenu()
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
        /// 获取父级菜单
        /// </summary>
        /// <returns></returns>
        public IList<ResponseMenu> AddSystemParentMenu()
        {
            var query = Kily.Set<SystemMenu>().Where(t => t.Level == MenuEnum.LevelOne).Where(t => t.ParentId == null).AsNoTracking().AsQueryable();
            var data = query.Select(t => new ResponseMenu()
            {
                MenuId = t.MenuId,
                MenuName = t.MenuName
            }).ToList();
            return data;
        }

        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMenu GetMenuDetail(Guid Id)
        {
            var data = Kily.Set<SystemMenu>().Where(t => t.Id == Id).AsNoTracking().Select(t => new ResponseMenu()
            {
                Id = t.Id,
                ParentId = t.ParentId,
                MenuId = t.MenuId,
                MenuName = t.MenuName,
                MenuIcon = t.MenuIcon,
                MenuAddress = t.MenuAddress,
                HasChildrenNode = t.HasChildrenNode
            }).FirstOrDefault();
            return data;
        }

        #endregion 系统菜单查询

        #region 系统菜单增删改

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="Id"></param>
        public string RemoveMenu(Guid Id)
        {
            if (Delete<SystemMenu>(ExpressionExtension.GetExpression<SystemMenu>("Id", Id, ExpressionEnum.Equals)))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string EditMenu(RequestMenu Param)
        {
            SystemMenu tree = Param.MapToEntity<SystemMenu>();
            if (Param.Id != Guid.Empty)
            {
                //修改
                if (Update<SystemMenu, RequestMenu>(tree, Param))
                    return ServiceMessage.UPDATESUCCESS;
                else
                    return ServiceMessage.UPDATEFAIL;
            }
            else
            {
                //新增
                if (tree.HasChildrenNode) //true就是一级菜单
                {
                    tree.ParentId = null;
                    tree.Level = MenuEnum.LevelOne;
                    tree.MenuAddress = null;
                    tree.MenuId = Guid.NewGuid();
                }
                else
                {
                    tree.Level = MenuEnum.LevelTwo;
                    tree.MenuId = Guid.NewGuid();
                }
                if (Insert<SystemMenu>(tree))
                    return ServiceMessage.INSERTSUCCESS;
                else
                    return ServiceMessage.INSERTFAIL;
            }
        }

        #endregion 系统菜单增删改

        #endregion 菜单管理

        #region 用户管理

        /// <summary>
        /// 中间系统调用
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string InsertAdmin(RequestAdmin Param)
        {
            Param.RoleAuthorType = Guid.NewGuid();
            SystemAdmin Admin = Param.MapToEntity<SystemAdmin>();
            if (Insert<SystemAdmin>(Admin))
                return ServiceMessage.INSERTSUCCESS;
            else
                return ServiceMessage.INSERTFAIL;
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditAdmin(RequestAdmin Param)
        {
            //根据角色权限类型获取角色权限等级
            SystemRoleAuthor Author = Kily.Set<SystemRoleAuthor>().Where(t => t.Id == Param.RoleAuthorType).AsNoTracking().FirstOrDefault();
            if (Author == null)
                return "请选择账户权限";
            SystemRoleLevel Level = Kily.Set<SystemRoleLevel>().Where(t => t.Id == Author.AuthorRoleLvId).AsNoTracking().FirstOrDefault();
            switch (Level.RoleLv)
            {
                case RoleEnum.LV1:
                    Param.AccountType = AccountEnum.Admin;
                    break;

                case RoleEnum.LV2:
                    Param.AccountType = AccountEnum.Country;
                    break;

                case RoleEnum.LV3:
                    Param.AccountType = AccountEnum.Province;
                    break;

                case RoleEnum.LV4:
                    Param.AccountType = AccountEnum.City;
                    break;

                case RoleEnum.LV5:
                    Param.AccountType = AccountEnum.Area;
                    break;

                case RoleEnum.LV6:
                    Param.AccountType = AccountEnum.Village;
                    break;
            }
            SystemAdmin Admin = Param.MapToEntity<SystemAdmin>();
            var Adm = Kily.Set<SystemAdmin>().Where(t => t.Account.Equals(Param.Account)).AsNoTracking().FirstOrDefault();
            if (Param.Id != Guid.Empty)
            {
                if (!NormalUtil.CheckStringChineseUn(Admin.Account))
                {
                    if (Update<SystemAdmin, RequestAdmin>(Admin, Param))
                        return ServiceMessage.UPDATESUCCESS;
                    else
                        return ServiceMessage.UPDATEFAIL;
                }
                else
                {
                    return "账号不能包含中文和特殊字符";
                }
            }
            else
            {
                if (Adm != null) return "该账号已经存在!";
                if (Insert<SystemAdmin>(Admin))
                    return ServiceMessage.INSERTSUCCESS;
                else
                    return ServiceMessage.INSERTFAIL;
            }
        }

        /// <summary>
        /// 用户分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseAdmin> GetAdminPage(PageParamList<RequestAdmin> pageParam)
        {
            IQueryable<SystemAdmin> queryable = Kily.Set<SystemAdmin>().AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.TrueName))
                queryable = queryable.Where(t => t.TrueName.Contains(pageParam.QueryParam.TrueName));
            if (!string.IsNullOrEmpty(pageParam.QueryParam.AreaTree))
                queryable = queryable.Where(t => t.TypePath.Contains(pageParam.QueryParam.AreaTree));
            if (UserInfo().AccountType == AccountEnum.Country)
                queryable = queryable.Where(t => t.AccountType >= AccountEnum.Country);
            if (UserInfo().AccountType == AccountEnum.Province)
                queryable = queryable.Where(t => t.AccountType >= AccountEnum.Province).Where(t => t.TypePath.Contains(pageParam.QueryParam.Province));
            if (UserInfo().AccountType == AccountEnum.City)
                queryable = queryable.Where(t => t.AccountType >= AccountEnum.City).Where(t => t.TypePath.Contains(pageParam.QueryParam.City));
            if (UserInfo().AccountType == AccountEnum.Area)
                queryable = queryable.Where(t => t.AccountType >= AccountEnum.Area).Where(t => t.TypePath.Contains(pageParam.QueryParam.Area));
            if (UserInfo().AccountType == AccountEnum.Village)
                queryable = queryable.Where(t => t.AccountType >= AccountEnum.Village).Where(t => t.TypePath.Contains(pageParam.QueryParam.Town));
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseAdmin()
            {
                Id = t.Id,
                CompanyName = t.CompanyName,
                TrueName = t.TrueName,
                Account = t.Account,
                AccountTypeName = AttrExtension.GetSingleDescription<AccountEnum, DescriptionAttribute>(t.AccountType),
                Phone = t.Phone,
                OpenNet = t.OpenNet,
                CommunityCode = t.CommunityCode,
                AccountStatus = t.IsDelete.Value ? "已禁用" : "已启用",
                IsUse = t.IsDelete
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string RemoveAdmin(Guid Id)
        {
            if (Delete<SystemAdmin>(ExpressionExtension.GetExpression<SystemAdmin>("Id", Id, ExpressionEnum.Equals)))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 启用账户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string OpenAdmin(Guid Id)
        {
            SystemAdmin Admin = Kily.Set<SystemAdmin>().Where(t => t.Id == Id).AsNoTracking().FirstOrDefault();
            Admin.IsDelete = false;
            return UpdateField(Admin, "IsDelete") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseAdmin GetAdminDetail(Guid Id)
        {
            return Kily.Set<SystemAdmin>().Where(t => t.Id == Id).AsNoTracking()
                 .Select(t => new ResponseAdmin()
                 {
                     Id = t.Id,
                     Account = t.Account,
                     CompanyName = t.CompanyName,
                     TrueName = t.TrueName,
                     PassWord = t.PassWord,
                     Phone = t.Phone,
                     Email = t.Email,
                     BankCard = t.BankCard,
                     BankName = t.BankName,
                     IdCard = t.IdCard,
                     RoleAuthorType = t.RoleAuthorType,
                     TypePath = t.TypePath,
                     OpenNet = t.OpenNet,
                     Chapter = t.Chapter,
                     CommunityCode = t.CommunityCode,
                     Address = t.Address
                 }).FirstOrDefault();
        }

        /// <summary>
        /// 回收或开启网签
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string CG(Guid Id, bool Param)
        {
            SystemAdmin Admin = Kily.Set<SystemAdmin>().Where(t => t.Id == Id).FirstOrDefault();
            Admin.OpenNet = Param;
            if (UserInfo().AccountType > AccountEnum.Country)
                if (Param)
                    if (UserInfo().AccountType == Admin.AccountType)
                        return "只可为下级开启，不可为自己开启，如要开启请联系上级。";
            return UpdateField(Admin, "OpenNet") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        /// <summary>
        /// 获取可以签到合同的代理商
        /// </summary>
        /// <param name="mm"></param>
        /// <returns></returns>
        public IList<ResponseAdmin> GetAuthorAdmin(string TypePath)
        {
            List<ResponseAdmin> admin = Kily.Set<SystemAdmin>().Where(t => t.IsDelete == false)
                .Where(t => t.OpenNet == true).Where(t => t.AccountType == AccountEnum.Country).Select(t => new ResponseAdmin()
                {
                    Id = t.Id,
                    CompanyName = t.CompanyName,
                    CommunityCode = t.CommunityCode,
                    Chapter = t.Chapter,
                    Address = t.Address,
                    BankCard = t.BankCard,
                    BankName = t.BankName
                }).ToList();
            return admin;
        }

        /// <summary>
        /// 获取银行账户信息
        /// </summary>
        /// <returns></returns>
        public IList<ResponseAdmin> GetBankInfo()
        {
            IQueryable<SystemAdmin> queryable = Kily.Set<SystemAdmin>().Where(t => t.IsDelete == false)
                .Where(t => t.AccountType != AccountEnum.Admin);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.TypePath.Contains(CompanyInfo().Province)
                || t.TypePath.Contains(CompanyInfo().City)
                || t.TypePath.Contains(CompanyInfo().Area)
                || t.TypePath.Contains(CompanyInfo().Town)
                || t.AccountType == AccountEnum.Country).AsNoTracking();
            else if (CompanyUser() != null)
                queryable = queryable.Where(t => t.TypePath.Contains(CompanyUser().Province)
                || t.TypePath.Contains(CompanyUser().City)
                || t.TypePath.Contains(CompanyUser().Area)
                || t.TypePath.Contains(CompanyUser().Town)
                || t.AccountType == AccountEnum.Country).AsNoTracking();
            else if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.TypePath.Contains(MerchantInfo().Province)
                || t.TypePath.Contains(MerchantInfo().City)
                || t.TypePath.Contains(MerchantInfo().Area)
                || t.TypePath.Contains(MerchantInfo().Town)
                || t.AccountType == AccountEnum.Country).AsNoTracking();
            else
                queryable = queryable.Where(t => t.TypePath.Contains(MerchantUser().Province)
                || t.TypePath.Contains(MerchantUser().City)
                || t.TypePath.Contains(MerchantUser().Area)
                || t.TypePath.Contains(MerchantUser().Town)
                || t.AccountType == AccountEnum.Country).AsNoTracking();
            var data = queryable.Select(t => new ResponseAdmin()
            {
                CompanyName = t.CompanyName,
                BankCard = t.BankCard,
                BankName = t.BankName
            }).ToList();
            return data;
        }

        #endregion 用户管理

        #region 角色权限

        #region 角色等级查询

        /// <summary>
        /// 异步获取权限等级
        /// </summary>
        /// <returns></returns>
        public IList<ResponseRoleLv> GetRoleLv()
        {
            IQueryable<SystemRoleLevel> queryable = Kily.Set<SystemRoleLevel>().Where(t => t.IsDelete == false).AsNoTracking();
            if (UserInfo().AccountType == AccountEnum.Country)
                queryable = queryable.Where(t => t.RoleLv >= RoleEnum.LV2);
            if (UserInfo().AccountType == AccountEnum.Province)
                queryable = queryable.Where(t => t.RoleLv >= RoleEnum.LV3);
            if (UserInfo().AccountType == AccountEnum.City)
                queryable = queryable.Where(t => t.RoleLv >= RoleEnum.LV4);
            if (UserInfo().AccountType == AccountEnum.Area)
                queryable = queryable.Where(t => t.RoleLv >= RoleEnum.LV5);
            if (UserInfo().AccountType == AccountEnum.Village)
                queryable = queryable.Where(t => t.RoleLv == RoleEnum.LV6);
            var data = queryable.OrderBy(t => t.CreateTime).Select(t => new ResponseRoleLv()
            {
                Id = t.Id,
                LvName = t.LvName,
                RoleLv = t.RoleLv
            }).ToList();
            return data;
        }

        #endregion 角色等级查询

        #region 角色列表分页

        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public PagedResult<ResponseAuthorRole> GetAuthorPage(PageParamList<RequestAuthorRole> pageParam)
        {
            IQueryable<SystemRoleAuthor> queryable = Kily.Set<SystemRoleAuthor>().Where(t => t.IsDelete == false);
            IQueryable<SystemRoleLevel> query = Kily.Set<SystemRoleLevel>().Where(t => t.IsDelete == false).AsNoTracking();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.AuthorName))
                queryable = queryable.Where(t => t.AuthorName.Contains(pageParam.QueryParam.AuthorName));
            if (UserInfo().AccountType == AccountEnum.Country)
                query = query.Where(t => t.RoleLv >= RoleEnum.LV2);
            if (UserInfo().AccountType == AccountEnum.Province)
                query = query.Where(t => t.RoleLv >= RoleEnum.LV3);
            if (UserInfo().AccountType == AccountEnum.City)
                query = query.Where(t => t.RoleLv >= RoleEnum.LV4);
            if (UserInfo().AccountType == AccountEnum.Area)
                query = query.Where(t => t.RoleLv >= RoleEnum.LV5);
            if (UserInfo().AccountType == AccountEnum.Village)
                query = query.Where(t => t.RoleLv == RoleEnum.LV6);
            //得到权限等级的id集合
            List<Guid> RoleLvIds = query.Select(t => t.Id).ToList();
            queryable = queryable.Where(t => RoleLvIds.Contains((Guid)t.AuthorRoleLvId)).AsNoTracking();
            var data = queryable.Join(Kily.Set<SystemRoleLevel>(), t => t.AuthorRoleLvId, x => x.Id, (t, x) => new ResponseAuthorRole()
            {
                Id = t.Id,
                AuthorName = t.AuthorName,
                AuthorMenuPath = t.AuthorMenuPath,
                AuthorRoleLv = x.LvName,
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        #endregion 角色列表分页

        #region 角色添加删除

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string EditRole(RequestAuthorRole Param)
        {
            SystemRoleAuthor Author = Param.MapToEntity<SystemRoleAuthor>();
            if (Param.AuthorRoleLvId == null)
                return "请选择权限等级!";
            if (Param.Id == Guid.Empty)
            {
                if (Kily.Set<SystemRoleAuthor>().Where(t => t.IsDelete == false).Where(t => t.AuthorName.Equals(Author.AuthorName)).AsNoTracking().FirstOrDefault() != null)
                    return "角色名称重复请重新添加!";
                return Insert<SystemRoleAuthor>(Author) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
            else
                return Update<SystemRoleAuthor, RequestAuthorRole>(Author, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveAuthorRole(Guid Id)
        {
            return Remove<SystemRoleAuthor>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        #endregion 角色添加删除

        #region 获取权限列表

        /// <summary>
        /// 获取权限下拉列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseAuthorRole> GetAuthorRole()
        {
            IQueryable<SystemRoleAuthor> queryable = Kily.Set<SystemRoleAuthor>().Where(t => t.IsDelete == false).AsNoTracking();
            IQueryable<SystemRoleLevel> queryables = Kily.Set<SystemRoleLevel>().Where(t => t.IsDelete == false).AsNoTracking();
            if (UserInfo().AccountType <= AccountEnum.Country)
            {
                var data = queryable.Join(queryables.Where(t => t.RoleLv <= RoleEnum.LV3), t => t.AuthorRoleLvId, x => x.Id, (t, x) => new ResponseAuthorRole()
                {
                    AuthorName = t.AuthorName,
                    Id = t.Id
                }).ToList();
                return data;
            }
            else
            {
                var data = queryable.Join(queryables.Where(t => t.RoleLv > RoleEnum.LV3), t => t.AuthorRoleLvId, x => x.Id, (t, x) => new ResponseAuthorRole()
                {
                    AuthorName = t.AuthorName,
                    Id = t.Id
                }).ToList();
                return data;
            }
        }

        #endregion 获取权限列表

        #endregion 角色权限

        #region 用户登陆

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="LoginValidate"></param>
        /// <returns></returns>
        public ResponseAdmin SystemLogin(RequestValidate LoginValidate)
        {
            var Admin = Kily.Set<SystemAdmin>()
           .Where(t => t.Account.Equals(LoginValidate.Account) && t.PassWord.Equals(LoginValidate.PassWord))
           .Where(t => t.IsDelete == false).Select(t => new ResponseAdmin()
           {
               Id = t.Id,
               TrueName = t.TrueName,
               Account = t.Account,
               TypePath = t.TypePath,
               IdCard = t.IdCard,
               Phone = t.Phone,
               Email = t.Email,
               CompanyName = t.CompanyName,
               AccountType = t.AccountType,
               RoleAuthorType = t.RoleAuthorType,
               Address = t.Address,
               CommunityCode = t.CommunityCode,
               TableName = typeof(ResponseAdmin).Name
           }).FirstOrDefault();
            return Admin ?? null;
        }

        #endregion 用户登陆

        #region 区域树

        /// <summary>
        /// 获取完整省市区县区域树速度慢
        /// </summary>
        /// <returns></returns>
        public IList<ResponseTree> GetSystemAreaTrees()
        {
            IQueryable<ResponseTree> queryable = Kily.Set<SystemProvince>()
                 .Where(t => t.IsDelete == false)
                 .AsNoTracking().OrderBy(t => t.Code).Select(t => new ResponseTree()
                 {
                     Id = t.Id,
                     Text = t.Name,
                     Color = "black",
                     BackClolor = "white",
                     SelectedIcon = "fa fa-refresh fa-spin",
                     State = new States(),
                     Nodes = Kily.Set<SystemCity>().Where(x => x.IsDelete == false)
                   .Where(x => x.ProvinceCode == t.Code).OrderBy(x => x.Code).AsNoTracking()
                   .Select(x => new ResponseCityTree()
                   {
                       Id = x.Id,
                       Text = x.Name,
                       Color = "black",
                       BackClolor = "white",
                       SelectedIcon = "fa fa-refresh fa-spin",
                       State = new States(),
                       Nodes = Kily.Set<SystemArea>().Where(y => y.IsDelete == false)
                       .Where(y => y.CityCode == x.Code).OrderBy(y => y.Code).AsNoTracking()
                       .Select(y => new ResponseAreaTree()
                       {
                           Id = y.Id,
                           Text = y.Name,
                           Color = "black",
                           BackClolor = "white",
                           SelectedIcon = "fa fa-refresh fa-spin",
                           State = new States(),
                           Nodes = Kily.Set<SystemTown>().Where(o => o.IsDelete == false)
                          .Where(o => o.AreaCode == y.Code).OrderBy(o => o.Code).AsNoTracking()
                          .Select(o => new ResponseTownTree()
                          {
                              Id = o.Id,
                              Text = o.Name,
                              Color = "black",
                              BackClolor = "white",
                              SelectedIcon = "fa fa-refresh fa-spin",
                              State = new States(),
                          }).AsEnumerable()
                       }).AsEnumerable()
                   }).AsEnumerable()
                 });
            var data = queryable.ToList();
            return data;
        }

        /// <summary>
        /// 获取完整省市区区域树速度快
        /// </summary>
        /// <returns></returns>
        public IList<ResponseTree> GetSystemAreaTree()
        {
            IQueryable<ResponseTree> queryable = Kily.Set<SystemProvince>()
                 .Where(t => t.IsDelete == false)
                 .AsNoTracking().OrderBy(t => t.Code).Select(t => new ResponseTree()
                 {
                     Id = t.Id,
                     Text = t.Name,
                     Color = "black",
                     BackClolor = "white",
                     SelectedIcon = "fa fa-refresh fa-spin",
                     State = new States(),
                     Nodes = Kily.Set<SystemCity>().Where(x => x.IsDelete == false)
                   .Where(x => x.ProvinceCode == t.Code).OrderBy(x => x.Code).AsNoTracking()
                   .Select(x => new ResponseCityTree()
                   {
                       Id = x.Id,
                       Text = x.Name,
                       Color = "black",
                       BackClolor = "white",
                       SelectedIcon = "fa fa-refresh fa-spin",
                       State = new States(),
                       Nodes = Kily.Set<SystemArea>().Where(y => y.IsDelete == false)
                       .Where(y => y.CityCode == x.Code).OrderBy(y => y.Code).AsNoTracking()
                       .Select(y => new ResponseAreaTree()
                       {
                           Id = y.Id,
                           Text = y.Name,
                           Color = "black",
                           BackClolor = "white",
                           SelectedIcon = "fa fa-refresh fa-spin",
                           State = new States()
                       }).AsEnumerable()
                   }).AsEnumerable()
                 });
            var data = queryable.ToList();
            return data;
        }

        #endregion 区域树

        #region 权限菜单树

        /// <summary>
        /// 获取权限菜单树
        /// </summary>
        /// <returns></returns>
        public IList<ResponseParentTree> GetSystemAdminTree(String Key)
        {
            IQueryable<SystemMenu> queryables = Kily.Set<SystemMenu>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(Key))
                queryables = queryables.Where(t => Key.Contains(t.Id.ToString()));
            if (UserInfo().AccountType == AccountEnum.Admin)
            {
                IQueryable<ResponseParentTree> queryable = Kily.Set<SystemMenu>().Where(t => t.IsDelete == false)
                    .Where(t => t.Level == MenuEnum.LevelOne)
                    .AsNoTracking().Select(t => new ResponseParentTree()
                    {
                        Id = t.Id,
                        Text = t.MenuName,
                        Color = "black",
                        BackClolor = "white",
                        SelectedIcon = "fa fa-refresh fa-spin",
                        State = string.IsNullOrEmpty(Key) ? null : (queryables.Where(x => x.Id == t.Id).AsNoTracking().FirstOrDefault() != null ? new States { Checked = true } : null),
                        Nodes = Kily.Set<SystemMenu>().Where(x => x.IsDelete == false)
                        .Where(x => x.Level != MenuEnum.LevelOne).Where(x => x.ParentId == t.MenuId).AsNoTracking()
                         .Select(x => new ResponseChildTree()
                         {
                             Id = x.Id,
                             Text = x.MenuName,
                             Color = "black",
                             State = string.IsNullOrEmpty(Key) ? null : (queryables.Where(p => p.Id == x.Id).AsNoTracking().FirstOrDefault() != null ? new States { Checked = true } : null),
                             BackClolor = "white",
                             SelectedIcon = "fa fa-refresh fa-spin",
                         }).AsQueryable()
                    }).AsQueryable();
                var data = queryable.ToList();
                return data;
            }
            else
            {
                //取权限菜单
                SystemRoleAuthor Author = Kily.Set<SystemRoleAuthor>().Where(t => t.Id == UserInfo().RoleAuthorType).AsNoTracking().FirstOrDefault();
                IQueryable<ResponseParentTree> queryable = Kily.Set<SystemMenu>().Where(t => t.IsDelete == false)
                    .Where(t => t.Level == MenuEnum.LevelOne)
                    .Where(t => Author.AuthorMenuPath.Contains(t.Id.ToString())).AsQueryable().AsNoTracking()
                     .Select(t => new ResponseParentTree()
                     {
                         Id = t.Id,
                         Text = t.MenuName,
                         Color = "black",
                         State = string.IsNullOrEmpty(Key) ? null : (queryables.Where(x => x.Id == t.Id).AsNoTracking().FirstOrDefault() != null ? new States { Checked = true } : null),
                         BackClolor = "white",
                         SelectedIcon = "fa fa-refresh fa-spin",
                         Nodes = Kily.Set<SystemMenu>().Where(x => x.IsDelete == false)
                         .Where(x => x.Level != MenuEnum.LevelOne).Where(x => x.ParentId == t.MenuId).AsNoTracking()
                         .Where(x => Author.AuthorMenuPath.Contains(x.Id.ToString()))
                         .Select(x => new ResponseChildTree()
                         {
                             Id = x.Id,
                             Text = x.MenuName,
                             Color = "black",
                             BackClolor = "white",
                             State = string.IsNullOrEmpty(Key) ? null : (queryables.Where(p => p.Id == x.Id).AsNoTracking().FirstOrDefault() != null ? new States { Checked = true } : null),
                             SelectedIcon = "fa fa-refresh fa-spin",
                         }).AsQueryable()
                     }).AsQueryable();
                var data = queryable.ToList();
                return data;
            }
        }

        #endregion 权限菜单树

        #region 省市区乡

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <returns></returns>
        public IList<ResponseProvince> GetProvince()
        {
            var data = Kily.Set<SystemProvince>().Where(t => t.IsDelete == false).AsNoTracking()
                .Select(t => new ResponseProvince()
                {
                    Id = t.Id,
                    ProvinceId = t.Code,
                    ProvinceName = t.Name
                }).OrderBy(t => t.ProvinceId).ToList();
            return data;
        }

        /// <summary>
        /// 获取省份下的城市
        /// </summary>
        /// <param name="Pid"></param>
        /// <returns></returns>
        public IList<ResponseCity> GetCity(int Pid)
        {
            var data = Kily.Set<SystemCity>().Where(t => t.IsDelete == false).Where(t => t.ProvinceCode == Pid).AsNoTracking()
                .Select(t => new ResponseCity()
                {
                    Id = t.Id,
                    CityId = t.Code,
                    CityName = t.Name
                }).OrderBy(t => t.CityId).ToList();
            return data;
        }

        /// <summary>
        /// 获取城市下的区县
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public IList<ResponseArea> GetArea(int Cid)
        {
            var data = Kily.Set<SystemArea>().Where(t => t.IsDelete == false).Where(t => t.CityCode == Cid).AsNoTracking()
                .Select(t => new ResponseArea()
                {
                    Id = t.Id,
                    AreaId = t.Code,
                    AreaName = t.Name
                }).OrderBy(t => t.AreaId).ToList();
            return data;
        }

        /// <summary>
        /// 获取区域下的乡镇
        /// </summary>
        /// <param name="Aid"></param>
        /// <returns></returns>
        public IList<ResponseTown> GetTown(int Aid)
        {
            var data = Kily.Set<SystemTown>().Where(t => t.IsDelete == false).Where(t => t.AreaCode == Aid).AsNoTracking()
              .Select(t => new ResponseTown()
              {
                  Id = t.Id,
                  TownId = t.Code,
                  TownName = t.Name
              }).OrderBy(t => t.TownId).ToList();
            return data;
        }

        /// <summary>
        /// 获取中文区域
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string GetAreaWithChinese(string Param)
        {
            var Path = Param.Split(',').ToList();
            Path.RemoveAll(t => string.IsNullOrEmpty(t));
            var ProvinceName = Kily.Set<SystemProvince>().Where(t => t.Id.ToString() == Path.FirstOrDefault()).FirstOrDefault().Name;
            var CityName = Kily.Set<SystemCity>().Where(t => t.Id.ToString() == Path[1]).FirstOrDefault().Name;
            var AreaName = Kily.Set<SystemArea>().Where(t => t.Id.ToString() == Path[2]).FirstOrDefault().Name;
            string TownName = string.Empty;
            if (Path.Count() > 3)
            {
                TownName = Kily.Set<SystemTown>().Where(t => t.Id.ToString() == Path[3]).FirstOrDefault().Name;
            }
            return $"{ProvinceName},{CityName},{AreaName},{TownName}";
        }

        #endregion 省市区乡

        #region 任务调度

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string AddJob(RequestQuartz Param)
        {
            SystemQuartz quartz = Param.MapToEntity<SystemQuartz>();
            if (Insert(quartz))
                return ServiceMessage.INSERTSUCCESS;
            else
                return ServiceMessage.INSERTFAIL;
        }

        /// <summary>
        /// 任务调度分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseQuartz> GetJobPage(PageParamList<RequestQuartz> pageParam)
        {
            IQueryable<SystemQuartz> queryable = Kily.Set<SystemQuartz>();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.JobName))
                queryable = queryable.Where(t => t.JobName.Contains(pageParam.QueryParam.JobName));
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseQuartz()
            {
                Id = t.Id,
                JobGroup = t.JobGroup,
                JobName = t.JobName,
                StartTime = t.StartTime,
                EndTime = t.EndTime,
                IntervalSecond = t.IntervalSecond,
                RunTimes = t.RunTimes > 0 ? t.RunTimes.ToString() : "无限执行",
                Cron = t.Cron,
                JobDetail = t.JobDetail,
                JobState = AttrExtension.GetSingleDescription<JobEnum, DescriptionAttribute>(t.JobType)
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string ExcuteJob(RequestQuartz Param)
        {
            try
            {
                QuartzMap quartz = Param.MapToEntity<QuartzMap>();
                SystemQuartz quartzs = Param.MapToEntity<SystemQuartz>();
                UpdateField<SystemQuartz>(quartzs, "JobType");
                return QuartzCoreFactory.QuartzCore().AddJob(quartz).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        public string StopJob()
        {
            try
            {
                QuartzCoreFactory.QuartzCore().StopJob();
                return "停止成功!";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 恢复暂停任务
        /// </summary>
        /// <param name="Param"></param>
        public string RecoverPauseJob(RequestQuartz Param)
        {
            try
            {
                QuartzMap quartz = Param.MapToEntity<QuartzMap>();
                SystemQuartz quartzs = Param.MapToEntity<SystemQuartz>();
                if (UpdateField<SystemQuartz>(quartzs, "JobType"))
                {
                    QuartzCoreFactory.QuartzCore().ResumeJob(quartz);
                    return ServiceMessage.UPDATESUCCESS;
                }
                else return ServiceMessage.UPDATEFAIL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 暂停指定任务
        /// </summary>
        /// <param name="Param"></param>
        public string PauseAppointJob(RequestQuartz Param)
        {
            try
            {
                QuartzMap quartz = Param.MapToEntity<QuartzMap>();
                SystemQuartz quartzs = Param.MapToEntity<SystemQuartz>();
                if (UpdateField<SystemQuartz>(quartzs, "JobType"))
                {
                    QuartzCoreFactory.QuartzCore().StopResumeJob(quartz);
                    return ServiceMessage.UPDATESUCCESS;
                }
                else return ServiceMessage.UPDATEFAIL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string RemoveJob(RequestQuartz Param)
        {
            try
            {
                QuartzMap quartz = Param.MapToEntity<QuartzMap>();
                SystemQuartz quartzs = Param.MapToEntity<SystemQuartz>();
                QuartzCoreFactory.QuartzCore().StopResumeJob(quartz);
                if (Remove<SystemQuartz>(ExpressionExtension.GetExpression<SystemQuartz>("Id", quartzs.Id, ExpressionEnum.Equals)))
                    return ServiceMessage.REMOVESUCCESS;
                else
                    return ServiceMessage.REMOVEFAIL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion 任务调度

        #region 人员归档

        /// <summary>
        /// 人员分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponsePreson> GetPresonPage(PageParamList<RequestPreson> pageParam)
        {
            IQueryable<SystemPreson> queryable = Kily.Set<SystemPreson>().OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.WorkNum))
                queryable = queryable.Where(t => t.WorkNum.Contains(pageParam.QueryParam.WorkNum));
            if (UserInfo().AccountType == AccountEnum.Province)
                queryable = queryable.Where(t => t.WorkNum.Contains("省"));
            if (UserInfo().AccountType == AccountEnum.City)
                queryable = queryable.Where(t => t.WorkNum.Contains("市"));
            if (UserInfo().AccountType == AccountEnum.Area)
                queryable = queryable.Where(t => t.WorkNum.Contains("区"));
            if (UserInfo().AccountType == AccountEnum.Village)
                queryable = queryable.Where(t => t.WorkNum.Contains("镇"));
            var data = queryable.Select(t => new ResponsePreson()
            {
                Id = t.Id,
                TrueName = t.TrueName,
                Address = t.Address,
                IdCard = t.IdCard,
                LinkPhone = t.LinkPhone,
                WorkNum = t.WorkNum,
                Status = t.IsDelete.Value ? "禁用" : "启用"
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 获取人员详情
        /// </summary>
        public ResponsePreson GetPresonDetail(Guid Id)
        {
            return Kily.Set<SystemPreson>().Where(t => t.Id == Id).FirstOrDefault().MapToEntity<ResponsePreson>();
        }

        /// <summary>
        /// 首页人员查询
        /// </summary>
        public PagedResult<ResponsePreson> GetPresonDetailWeb(String key, int pageIndex, int pageSize)
        {
            var data = Kily.Set<SystemPreson>().Where(t => t.WorkNum.Contains(key) || t.TrueName.Contains(key) || t.ServciePath.Contains(key))
                .Select(t => new ResponsePreson
                {
                    Id = t.Id,
                    TrueName = t.TrueName,
                    WorkNum = t.WorkNum,
                    Type = t.Type,
                    LinkPhone = t.LinkPhone,
                    HeadImage = t.HeadImage,
                    Address = t.Address,
                    IdCard = t.IdCard,
                    ServiceYear = t.ServiceYear,
                    STime = t.STime,
                    ETime = t.ETime,
                    ServciePath = t.ServciePath,
                }).ToPagedResult(pageIndex, pageSize);
            return data;
        }

        /// <summary>
        /// 编辑人员
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string PresonEdit(RequestPreson Param)
        {
            SystemPreson Preson = Param.MapToEntity<SystemPreson>();
            var User = UserInfo();
            SystemCity City = Kily.Set<SystemCity>().Where(t => t.Id.ToString() == User.City).AsNoTracking().FirstOrDefault();
            SystemAreaCar AreaCar = Kily.Set<SystemAreaCar>().Where(t => t.CityCode == City.Code.ToString()).AsNoTracking().FirstOrDefault();
            if (Preson.Id != Guid.Empty)
            {
                if (Update<SystemPreson, RequestPreson>(Preson, Param))
                    return ServiceMessage.UPDATESUCCESS;
                else
                    return ServiceMessage.UPDATEFAIL;
            }
            else
            {
                if (UserInfo().AccountType == AccountEnum.Admin)
                {
                    int Num = Kily.Set<SystemPreson>().Where(t => t.Type.Contains("全国运营")).Count() + 1;
                    if (Num > 100)
                        Preson.WorkNum = "全国运营0" + Num;
                    if (Num > 10)
                        Preson.WorkNum = "全国运营00" + Num;
                    if (Num < 10)
                        Preson.WorkNum = "全国运营000" + Num;
                    Preson.Type = "全国运营";
                }
                if (UserInfo().AccountType == AccountEnum.Country)
                {
                    int Num = Kily.Set<SystemPreson>().Where(t => t.Type.Contains("全国运营")).Count() + 1;
                    if (Num > 100)
                        Preson.WorkNum = "全国运营0" + Num;
                    if (Num > 10)
                        Preson.WorkNum = "全国运营00" + Num;
                    if (Num < 10)
                        Preson.WorkNum = "全国运营000" + Num;
                    Preson.Type = "全国运营";
                }
                if (UserInfo().AccountType == AccountEnum.Province)
                {
                    int Num = Kily.Set<SystemPreson>().Where(t => t.Type.Contains("省级运营")).Count() + 1;

                    if (Num > 100)
                        Preson.WorkNum = AreaCar + "0" + Num;
                    if (Num > 10)
                        Preson.WorkNum = AreaCar + "00" + Num;
                    if (Num < 10)
                        Preson.WorkNum = AreaCar + "000" + Num;
                    Preson.Type = "省级运营";
                }
                if (UserInfo().AccountType == AccountEnum.City)
                {
                    int Num = Kily.Set<SystemPreson>().Where(t => t.Type.Contains("市级运营")).Count() + 1;
                    if (Num > 100)
                        Preson.WorkNum = AreaCar + "0" + Num;
                    if (Num > 10)
                        Preson.WorkNum = AreaCar + "00" + Num;
                    if (Num < 10)
                        Preson.WorkNum = AreaCar + "000" + Num;
                    Preson.Type = "市级运营";
                }
                if (UserInfo().AccountType == AccountEnum.Area)
                {
                    int Num = Kily.Set<SystemPreson>().Where(t => t.Type.Contains("区域运营")).Count() + 1;
                    if (Num > 100)
                        Preson.WorkNum = AreaCar + "0" + Num;
                    if (Num > 10)
                        Preson.WorkNum = AreaCar + "00" + Num;
                    if (Num < 10)
                        Preson.WorkNum = AreaCar + "000" + Num;
                    Preson.Type = "区域运营";
                }
                if (UserInfo().AccountType == AccountEnum.Village)
                {
                    int Num = Kily.Set<SystemPreson>().Where(t => t.Type.Contains("乡镇运营")).Count() + 1;
                    if (Num > 100)
                        Preson.WorkNum = "乡镇运营0" + Num;
                    if (Num > 10)
                        Preson.WorkNum = "乡镇运营00" + Num;
                    if (Num < 10)
                        Preson.WorkNum = "乡镇运营000" + Num;
                    Preson.Type = "乡镇运营";
                }
                if (Insert<SystemPreson>(Preson))
                    return ServiceMessage.INSERTSUCCESS;
                else
                    return ServiceMessage.INSERTFAIL;
            }
        }

        /// <summary>
        /// 删除人员
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemovePreson(Guid Id, bool key)
        {
            SystemPreson preson = Kily.Set<SystemPreson>().Where(t => t.Id == Id).FirstOrDefault();
            if (key == true)
                Delete<SystemPreson>(t => t.Id == Id);
            else
            {
                preson.IsDelete = false;
                UpdateField(preson, "IsDelete");
            }
            return ServiceMessage.HANDLESUCCESS;
        }

        #endregion 人员归档

        #region 服务网点
        public PagedResult<ResponseSystemNetService> GetNetServicePage(PageParamList<RequestSystemNetService> pageParam)
        {
            IQueryable<SystemNetService> queryable = Kily.Set<SystemNetService>().OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CompanyName))
                queryable = queryable.Where(t => t.CompanyName.Contains(pageParam.QueryParam.CompanyName));
            var data = queryable.Select(t => new ResponseSystemNetService
            {
                Id = t.Id,
                Status = t.IsDelete.Value ? "禁用" : "启用",
                ServiceNetName = t.ServiceNetName,
                CompanyName = t.CompanyName,
                Address = t.Address,
                Code = t.Code,
                Off = t.Off,
                LinkPhone = t.LinkPhone
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        public ResponseSystemNetService GetNetServiceDetail(Guid Id)
        {
            return Kily.Set<SystemNetService>().Where(t => t.Id == Id).FirstOrDefault().MapToEntity<ResponseSystemNetService>();
        }
        public string IsOpen(Guid Id, bool key)
        {
            SystemNetService preson = Kily.Set<SystemNetService>().Where(t => t.Id == Id).FirstOrDefault();
            if (key == true)
                Delete<SystemNetService>(t => t.Id == Id);
            else
            {
                preson.IsDelete = false;
                UpdateField(preson, "IsDelete");
            }
            return ServiceMessage.HANDLESUCCESS;
        }
        public string EditNetService(RequestSystemNetService param)
        {
            SystemNetService service = param.MapToEntity<SystemNetService>();
            if (param.Id == Guid.Empty)
                return Insert(service) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return Update(service, param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }
        public PagedResult<ResponseSystemNetService> GetNetServiceWeb(String key, int pageIndex, int pageSize)
        {
            var data = Kily.Set<SystemNetService>().OrderByDescending(t => t.CreateTime).Where(t => t.CompanyName.Contains(key) || t.ServciePath.Contains(key)).Select(t => new ResponseSystemNetService
            {
                Id = t.Id,
                ServiceNetName = t.ServiceNetName,
                CompanyName = t.CompanyName,
                Code = t.Code,
                LinkPhone = t.LinkPhone,
                Off = t.Off,
                Address = t.Address,
                IdImage = t.IdImage,
                ServiceYear = t.ServiceYear,
                STime = t.STime,
                ETime = t.ETime,
                ServciePath = t.ServciePath,
            }).ToPagedResult(pageIndex, pageSize);
            return data;
        }
        #endregion

        #region 入住合同

        /// <summary>
        /// 入住合同
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseStayContract> GetStayContractPage(PageParamList<RequestStayContract> pageParam)
        {
            IQueryable<SystemStayContract> queryable = Kily.Set<SystemStayContract>().Where(t => t.EnterpriseOrMerchant == pageParam.QueryParam.EnterpriseOrMerchant).Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CompanyName))
                queryable = queryable.Where(t => t.CompanyName.Contains(pageParam.QueryParam.CompanyName));
            if (pageParam.QueryParam.AuditType.HasValue)
                queryable = queryable.Where(t => t.AuditType == pageParam.QueryParam.AuditType);
            if (UserInfo().AccountType == AccountEnum.Province)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().Province));
            if (UserInfo().AccountType == AccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().City));
            if (UserInfo().AccountType == AccountEnum.Area)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().Area));
            IQueryable<EnterpriseUpLevel> elvs = Kily.Set<EnterpriseUpLevel>().Where(t => t.IsDelete == false);
            IQueryable<RepastUpLevel> rlvs = Kily.Set<RepastUpLevel>().Where(t => t.IsDelete == false);
            //所属区域下的合同
            var data = queryable.OrderByDescending(t => t.CreateTime).AsNoTracking().Select(t => new ResponseStayContract()
            {
                Id = t.Id,
                Record = (pageParam.QueryParam.EnterpriseOrMerchant == 1 ? (elvs.Where(x => x.CompanyId == t.CompanyId).AsNoTracking().FirstOrDefault() as Object) : (rlvs.Where(x => x.InfoId == t.CompanyId).AsNoTracking().FirstOrDefault() as Object)),
                TotalPrice = t.TotalPrice,
                CompanyName = t.CompanyName,
                PayTicket = t.PayTicket,
                StayTime = t.CreateTime,
                EndTime = t.EndTime,
                AuditType = t.AuditType,
                CompanyId = t.CompanyId,
                AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(t.AuditType),
                TableName = t.GetType().Name,
                ContractType = t.ContractType,
                IsPay = t.IsPay,
                TryOut = t.TryOut,
                ActualPrice = t.ActualPrice,
                VersionTypeName = AttrExtension.GetSingleDescription<SystemVersionEnum, DescriptionAttribute>(t.VersionType),
                PayTypeName = AttrExtension.GetSingleDescription<PayEnum, DescriptionAttribute>(t.PayType)
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 确认缴费
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string EditContract(Guid Id, decimal Money)
        {
            SystemStayContract Contract = Kily.Set<SystemStayContract>().Where(t => t.IsDelete == false).Where(t => t.Id == Id).FirstOrDefault();
            Contract.IsPay = true;
            Contract.TryOut = null;
            Contract.ActualPrice = Money;
            List<String> Fieds = new List<string> { "IsPay", "TryOut", "ActualPrice" };
            return UpdateField<SystemStayContract>(Contract, null, Fieds) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        /// <summary>
        /// 审核合同
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string AuditContract(RequestAudit Param)
        {
            Param.AuditName = UserInfo().TrueName;
            SystemAudit Audit = Param.MapToEntity<SystemAudit>();
            if (Insert<SystemAudit>(Audit))
            {
                SystemStayContract Contract = Kily.Set<SystemStayContract>().Where(t => t.IsDelete == false).Where(t => t.Id == Param.TableId).FirstOrDefault();
                Contract.AuditType = Param.AuditType;
                if (UpdateField<SystemStayContract>(Contract, "AuditType"))
                    return ServiceMessage.HANDLESUCCESS;
                else
                    return ServiceMessage.HANDLEFAIL;
            }
            else
                return ServiceMessage.INSERTFAIL;
        }

        /// <summary>
        /// 获取审核记录
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseAudit> GetContractRecord(PageParamList<RequestAudit> pageParam)
        {
            var data = Kily.Set<SystemAudit>().Where(t => t.IsDelete == false)
                 .Where(t => t.TableId == pageParam.QueryParam.TableId && t.TableName.Contains(pageParam.QueryParam.TableName))
                 .Select(t => new ResponseAudit()
                 {
                     Id = t.Id,
                     AuditName = t.AuditName,
                     AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(t.AuditType),
                     AuditSuggestion = t.AuditSuggestion,
                     CreateTime = t.CreateTime
                 }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveRecord(Guid Id)
        {
            if (Delete<SystemAudit>(t => t.Id == Id))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }

        #endregion 入住合同

        #region 支付宝微信银行支付

        /// <summary>
        /// 支付宝
        /// </summary>
        /// <param name="Money"></param>
        /// <returns></returns>
        public string AliPay(int Money)
        {
            RequestAliPayModel AliPayModel = new RequestAliPayModel
            {
                OrderTitle = "营运中心缴费",
                Money = Money
            };
            return AliPayCore.Instance.WebPay(AliPayModel);
        }

        /// <summary>
        /// 微信
        /// </summary>
        /// <param name="Money"></param>
        /// <returns></returns>
        public string WxPay(int Money)
        {
            RequestWxPayModel WxPayModel = new RequestWxPayModel
            {
                OrderTitle = "营运中心缴费",
                Money = Money
            };
            return WxPayCore.Instance.WebPay(WxPayModel);
        }

        /// <summary>
        /// 查询支付宝支付
        /// </summary>
        /// <param name="TradeNo"></param>
        /// <returns></returns>
        public string AliQueryPay(String TradeNo)
        {
            SystemPayInfo PayInfo = Kily.Set<SystemPayInfo>().Where(t => t.PayType == PayEnum.Alipay)
                .Where(t => t.TradeNo.Equals(TradeNo)).AsNoTracking().FirstOrDefault();
            if (PayInfo != null)
            {
                String ResultCode = AliPayCore.Instance.QueryAliPay(TradeNo);
                if (string.IsNullOrEmpty(ResultCode))
                    return null;
                if (ResultCode.Equals("TRADE_SUCCESS"))
                {
                    EnterpriseInfo EInfo = Kily.Set<EnterpriseInfo>().Where(t => t.Id == PayInfo.MerchantId).AsNoTracking().FirstOrDefault();
                    RepastInfo RInfo = Kily.Set<RepastInfo>().Where(t => t.Id == PayInfo.MerchantId).AsNoTracking().FirstOrDefault();
                    CookVip CInfo = Kily.Set<CookVip>().Where(t => t.Id == PayInfo.MerchantId).AsNoTracking().FirstOrDefault();
                    PayInfo.PayDes = "TRADE_SUCCESS";
                    IList<string> Fields = new List<string>();
                    if (EInfo != null)
                    {
                        EInfo.Version = (SystemVersionEnum)PayInfo.Version;
                        EInfo.TagCodeNum = PayInfo.TagNum;
                        Fields.Add("Version");
                        Fields.Add("TagCodeNum");
                        if (string.IsNullOrEmpty(PayInfo.PayDes))
                        {
                            UpdateField(PayInfo, "PayDes");
                            UpdateField(EInfo, null, Fields);
                        }
                    }
                    if (RInfo != null)
                    {
                        RInfo.VersionType = (SystemVersionEnum)PayInfo.Version;
                        if (string.IsNullOrEmpty(PayInfo.PayDes))
                        {
                            UpdateField(PayInfo, "PayDes");
                            UpdateField(RInfo, "VersionType");
                        }
                    }
                    if (CInfo != null)
                    {
                        CInfo.IsVip = true;
                        CInfo.StartTime = DateTime.Now;
                        CInfo.EndTime = DateTime.Now.AddYears(Convert.ToInt32(PayInfo.TagNum));
                        Fields.Add("IsVip");
                        Fields.Add("StartTime");
                        Fields.Add("EndTime");
                        if (string.IsNullOrEmpty(PayInfo.PayDes))
                        {
                            UpdateField(PayInfo, "PayDes");
                            UpdateField(CInfo, null, Fields);
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 更新支付
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditPay(RequestStayContract Param)
        {
            SystemStayContract contract = Kily.Set<SystemStayContract>().Where(t => t.Id == Param.Id).FirstOrDefault();
            contract.PayTicket = Param.PayTicket;
            contract.PayType = Param.PayType;
            List<String> Fields = new List<String>() { "PayTicket", "PayType" };
            return UpdateField(contract, null, Fields) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        #endregion 支付宝微信银行支付

        #region 消息盒子

        /// <summary>
        /// 消息盒子
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseSystemMessage> GetMsgPage(PageParamList<Object> pageParam)
        {
            IQueryable<SystemMessage> queryable = Kily.Set<SystemMessage>().OrderByDescending(t => t.CreateTime);
            IQueryable<GovtComplain> queryables = Kily.Set<GovtComplain>().OrderByDescending(t => t.CreateTime);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || t.TypePath.Contains(CompanyInfo().Area));
            else if (CompanyUser() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id || t.TypePath.Contains(CompanyUser().Area));
            else if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == MerchantInfo().Id || t.TypePath.Contains(MerchantInfo().Area));
            else if (MerchantUser() != null)
                queryable = queryable.Where(t => t.CompanyId == MerchantUser().Id || t.TypePath.Contains(MerchantUser().Area));
            var data = queryable.GroupJoin(queryables, t => t.ComplainId, x => x.Id, (t, x) => new ResponseSystemMessage()
            {
                MsgName = t.MsgName,
                MsgContent = t.MsgContent,
                ReleaseTime = t.ReleaseTime,
                Category = t.Category,
                ComplainId = (t.ComplainId == Guid.Empty ? t.Id : t.ComplainId),
                Status = string.IsNullOrEmpty(x.FirstOrDefault().Status) ? t.Status : x.FirstOrDefault().Status
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 消息详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Object GetMsgDetail(Guid? Id)
        {
            IQueryable<SystemMessage> queryable = Kily.Set<SystemMessage>().OrderByDescending(t => t.CreateTime);
            IQueryable<GovtComplain> queryables = Kily.Set<GovtComplain>().OrderByDescending(t => t.CreateTime);
            SystemMessage message = queryable.Where(t => t.Id == Id).AsNoTracking().FirstOrDefault();
            GovtComplain complain = queryables.Where(t => t.Id == Id).AsNoTracking().FirstOrDefault();
            var obj = new
            {
                Title = message != null ? message.MsgName : complain.ProductName,
                Content = message != null ? message.MsgContent : complain.ComplainContent,
                Replay = message != null ? message.HandleContent : complain.HandlerContent,
                Status = message != null ? message.Status : complain.Status,
                Category = message != null ? message.Category : complain.Category
            };
            return obj;
        }
        #endregion 消息盒子

        #region 新闻资讯

        /// <summary>
        /// 新闻分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseSystemNews> GetNewsPage(PageParamList<RequestSystemNews> pageParam)
        {
            IQueryable<SystemNews> queryable = Kily.Set<SystemNews>().OrderByDescending(t => t.CreateTime);
            if ((int)pageParam.QueryParam.NewsType >= 10 && (int)pageParam.QueryParam.NewsType <= 50)
                queryable = queryable.Where(t => t.NewsType == pageParam.QueryParam.NewsType);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.Title))
                queryable = queryable.Where(t => t.Title == pageParam.QueryParam.Title);
            var data = queryable.Select(t => new ResponseSystemNews()
            {
                Id = t.Id,
                Title = t.Title,
                SubTitle = t.SubTitle,
                NewsTypeName = AttrExtension.GetSingleDescription<NewsEnum, DescriptionAttribute>(t.NewsType),
                ReleaseDate = t.ReleaseDate,
                NewsType = t.NewsType,
                NewsContent = t.NewsContent,
                Writer = t.Writer
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 编辑新闻
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditNews(RequestSystemNews Param)
        {
            SystemNews news = Param.MapToEntity<SystemNews>();
            if (Param.Id == Guid.Empty)
                return Insert(news) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return Update(news, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        /// <summary>
        /// 新闻详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseSystemNews GetNewsDetail(Guid Id)
        {
            var data = Kily.Set<SystemNews>().Where(t => t.Id == Id).Select(t => new ResponseSystemNews()
            {
                Id = t.Id,
                Title = t.Title,
                SubTitle = t.SubTitle,
                ReleaseDate = t.ReleaseDate,
                NewsType = t.NewsType,
                NewsContent = t.NewsContent,
                Writer = t.Writer
            }).FirstOrDefault();
            return data;
        }

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveNews(Guid Id)
        {
            return Remove<SystemNews>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        #endregion 新闻资讯

        #region 数据报表

        /// <summary>
        /// 二维码统计
        /// </summary>
        /// <returns></returns>
        public IList<ResponseSystemCodeCount> GetCodeCountCenter()
        {
            List<EnterpriseTagApply> queryable = Kily.Set<EnterpriseTagApply>().Where(t => t.IsDelete == false).Where(t => t.IsPay != null && t.IsPay == true).ToList();
            List<FunctionVeinTag> queryables = Kily.Set<FunctionVeinTag>().Where(t => t.IsDelete == false).ToList();
            List<EnterpriseInfo> InfoTemp = Kily.Set<EnterpriseInfo>().Where(t => !string.IsNullOrEmpty(t.TypePath)).Where(t => t.IsDelete == false).ToList();
            List<SystemAdmin> AdminTemp = Kily.Set<SystemAdmin>().Where(t => t.IsDelete == false).ToList();
            var ApplyTag = queryable.GroupJoin(InfoTemp, t => t.CompanyId, x => x.Id, (t, x) => new { t, x });
            var ComVein = queryables.Where(t => t.AllotType == 1).Join(InfoTemp, t => t.AcceptUser, x => x.Id.ToString(), (t, x) => new { t, x });
            var AdmVein = queryables.Where(t => t.AllotType == 2).Join(AdminTemp, t => t.AcceptUser, x => x.Id.ToString(), (t, x) => new { t, x });
            IList<ResponseSystemCodeCount> CodeCountList = new List<ResponseSystemCodeCount>();
            List<TmepArea> areas = null;
            if (UserInfo().AccountType <= AccountEnum.Country)
                areas = Kily.Set<SystemProvince>().Select(t => new TmepArea() { Id = t.Id.ToString(), Name = t.Name }).ToList();
            else if (UserInfo().AccountType == AccountEnum.Province)
            {
                var code = Kily.Set<SystemProvince>().Where(t => t.Id.ToString() == UserInfo().Province).Select(t => t.Code).FirstOrDefault();
                areas = Kily.Set<SystemCity>().Where(t => t.ProvinceCode == code).Select(t => new TmepArea() { Id = t.Id.ToString(), Name = t.Name }).ToList();
            }
            else if (UserInfo().AccountType == AccountEnum.City)
            {
                var code = Kily.Set<SystemCity>().Where(t => t.Id.ToString() == UserInfo().City).Select(t => t.Code).FirstOrDefault();
                areas = Kily.Set<SystemArea>().Where(t => t.CityCode == code).Select(t => new TmepArea() { Id = t.Id.ToString(), Name = t.Name }).ToList();
            }
            else if (UserInfo().AccountType == AccountEnum.Area)
            {
                var code = Kily.Set<SystemArea>().Where(t => t.Id.ToString() == UserInfo().Area).Select(t => t.Code).FirstOrDefault();
                areas = Kily.Set<SystemTown>().Where(t => t.AreaCode == code).Select(t => new TmepArea() { Id = t.Id.ToString(), Name = t.Name }).ToList();
            }
            areas.ForEach(o =>
            {
                var Temp = ApplyTag.Where(t => t.x.FirstOrDefault().TypePath.Contains(o.Id));
                var VempCom = ComVein.Where(t => t.x.TypePath.Contains(o.Id));
                var VempAdm = AdmVein.Where(t => t.x.TypePath.Contains(o.Id));
                //历史累计
                int HistoryThing = Temp.Where(t => t.t.CreateTime < DateTime.Now.AddDays(-DateTime.Now.Day)).Where(t => t.t.TagType == TagEnum.OneThing).Sum(t => Convert.ToInt32(t.t.ApplyNum));
                int HistoryBrand = Temp.Where(t => t.t.CreateTime < DateTime.Now.AddDays(-DateTime.Now.Day)).Where(t => t.t.TagType == TagEnum.OneBrand).Sum(t => Convert.ToInt32(t.t.ApplyNum));
                int HistoryCompany = Temp.Where(t => t.t.CreateTime < DateTime.Now.AddDays(-DateTime.Now.Day)).Where(t => t.t.TagType == TagEnum.OneEnterprise).Sum(t => Convert.ToInt32(t.t.ApplyNum));
                int HistoryVein = VempCom.Where(t => t.t.CreateTime < DateTime.Now.AddDays(-DateTime.Now.Day)).Sum(t => t.t.AllotNum) + VempAdm.Where(t => t.t.CreateTime < DateTime.Now.AddDays(-DateTime.Now.Day)).Sum(t => t.t.AllotNum);
                //本月新增
                int NowThing = Temp.Where(t => t.t.CreateTime >= DateTime.Now.AddDays(-DateTime.Now.Day + 1)).Where(t => t.t.TagType == TagEnum.OneThing).Sum(t => Convert.ToInt32(t.t.ApplyNum));
                int NowBrand = Temp.Where(t => t.t.CreateTime >= DateTime.Now.AddDays(-DateTime.Now.Day + 1)).Where(t => t.t.TagType == TagEnum.OneBrand).Sum(t => Convert.ToInt32(t.t.ApplyNum));
                int NowCompany = Temp.Where(t => t.t.CreateTime >= DateTime.Now.AddDays(-DateTime.Now.Day + 1)).Where(t => t.t.TagType == TagEnum.OneEnterprise).Sum(t => Convert.ToInt32(t.t.ApplyNum));
                int NowVein = VempCom.Where(t => t.t.CreateTime >= DateTime.Now.AddDays(-DateTime.Now.Day + 1)).Sum(t => t.t.AllotNum) + VempAdm.Where(t => t.t.CreateTime >= DateTime.Now.AddDays(-DateTime.Now.Day + 1)).Sum(t => t.t.AllotNum);
                ResponseSystemCodeCount CodeCount = new ResponseSystemCodeCount
                {
                    AreaName = o.Name,
                    HistoryVeinCount = HistoryVein,
                    HistoryThingCount = HistoryThing,
                    HistoryClassCount = HistoryBrand,
                    HistoryCompanyCount = HistoryCompany,
                    NowVeinCount = NowVein,
                    NowThingCount = NowThing,
                    NowClassCount = NowBrand,
                    NowCompanyCount = NowCompany
                };
                CodeCountList.Add(CodeCount);
            });
            return CodeCountList;
        }

        /// <summary>
        /// 入住企业统计
        /// </summary>
        /// <returns></returns>
        public IList<ResponseSystemCompanyCount> GetCompanyCountCenter()
        {
            List<EnterpriseInfo> Enterprise = Kily.Set<EnterpriseInfo>().Where(t => !string.IsNullOrEmpty(t.TypePath)).Where(t => t.IsDelete == false).ToList();
            List<RepastInfo> Merchant = Kily.Set<RepastInfo>().Where(t => !string.IsNullOrEmpty(t.TypePath)).Where(t => t.IsDelete == false).ToList();
            List<CookInfo> Cook = Kily.Set<CookInfo>().Where(t => !string.IsNullOrEmpty(t.TypePath)).Where(t => t.IsDelete == false).ToList();
            IList<ResponseSystemCompanyCount> CompanyCountList = new List<ResponseSystemCompanyCount>();
            List<TmepArea> areas = null;
            if (UserInfo().AccountType <= AccountEnum.Country)
                areas = Kily.Set<SystemProvince>().Where(t => t.IsDelete == false).AsNoTracking()
                    .Select(t => new TmepArea() { Id = t.Id.ToString(), Name = t.Name }).ToList();
            else if (UserInfo().AccountType == AccountEnum.Province)
            {
                var code = Kily.Set<SystemProvince>().Where(t => t.Id.ToString() == UserInfo().Province).Select(t => t.Code).FirstOrDefault();
                areas = Kily.Set<SystemCity>().Where(t => t.ProvinceCode == code).Select(t => new TmepArea() { Id = t.Id.ToString(), Name = t.Name }).ToList();
            }
            else if (UserInfo().AccountType == AccountEnum.City)
            {
                var code = Kily.Set<SystemCity>().Where(t => t.Id.ToString() == UserInfo().City).Select(t => t.Code).FirstOrDefault();
                areas = Kily.Set<SystemArea>().Where(t => t.CityCode == code).Select(t => new TmepArea() { Id = t.Id.ToString(), Name = t.Name }).ToList();
            }
            else if (UserInfo().AccountType == AccountEnum.Area)
            {
                var code = Kily.Set<SystemArea>().Where(t => t.Id.ToString() == UserInfo().Area).Select(t => t.Code).FirstOrDefault();
                areas = Kily.Set<SystemTown>().Where(t => t.AreaCode == code).Select(t => new TmepArea() { Id = t.Id.ToString(), Name = t.Name }).ToList();
            }
            areas.ForEach(o =>
            {
                var ComTemp = Enterprise.Where(t => t.TypePath.Contains(o.Id));
                var MerTemp = Merchant.Where(t => t.TypePath.Contains(o.Id));
                var CookTemp = Cook.Where(t => t.TypePath.Contains(o.Id));
                //历史累计
                var HistoryPlant = ComTemp.Where(t => t.CreateTime < DateTime.Now.AddDays(-DateTime.Now.Day)).Where(t => t.CompanyType == CompanyEnum.Plant).Count();
                var HistoryCulture = ComTemp.Where(t => t.CreateTime < DateTime.Now.AddDays(-DateTime.Now.Day)).Where(t => t.CompanyType == CompanyEnum.Culture).Count();
                var HistoryProduction = ComTemp.Where(t => t.CreateTime < DateTime.Now.AddDays(-DateTime.Now.Day)).Where(t => t.CompanyType == CompanyEnum.Production).Count();
                var HistoryCirculation = ComTemp.Where(t => t.CreateTime < DateTime.Now.AddDays(-DateTime.Now.Day)).Where(t => t.CompanyType == CompanyEnum.Circulation).Count();
                var HistoryOther = ComTemp.Where(t => t.CreateTime < DateTime.Now.AddDays(-DateTime.Now.Day)).Where(t => t.CompanyType == CompanyEnum.Other).Count();
                var HistoryNormal = MerTemp.Where(t => t.CreateTime < DateTime.Now.AddDays(-DateTime.Now.Day)).Where(t => t.DiningType == MerchantEnum.Normal).Count();
                var HistoryUnitCanteen = MerTemp.Where(t => t.CreateTime < DateTime.Now.AddDays(-DateTime.Now.Day)).Where(t => t.DiningType == MerchantEnum.UnitCanteen).Count();
                var HistorySmall = MerTemp.Where(t => t.CreateTime < DateTime.Now.AddDays(-DateTime.Now.Day)).Where(t => t.DiningType > MerchantEnum.UnitCanteen).Count();
                var HistoryCook = CookTemp.Where(t => t.CreateTime < DateTime.Now.AddDays(-DateTime.Now.Day)).Count();
                //本月新增
                var NowPlant = ComTemp.Where(t => t.CreateTime >= DateTime.Now.AddDays(-DateTime.Now.Day + 1)).Where(t => t.CompanyType == CompanyEnum.Plant).Count();
                var NowCulture = ComTemp.Where(t => t.CreateTime >= DateTime.Now.AddDays(-DateTime.Now.Day + 1)).Where(t => t.CompanyType == CompanyEnum.Culture).Count();
                var NowProduction = ComTemp.Where(t => t.CreateTime >= DateTime.Now.AddDays(-DateTime.Now.Day + 1)).Where(t => t.CompanyType == CompanyEnum.Production).Count();
                var NowCirculation = ComTemp.Where(t => t.CreateTime >= DateTime.Now.AddDays(-DateTime.Now.Day + 1)).Where(t => t.CompanyType == CompanyEnum.Circulation).Count();
                var NowOther = ComTemp.Where(t => t.CreateTime >= DateTime.Now.AddDays(-DateTime.Now.Day + 1)).Where(t => t.CompanyType == CompanyEnum.Other).Count();
                var NowNormal = MerTemp.Where(t => t.CreateTime >= DateTime.Now.AddDays(-DateTime.Now.Day + 1)).Where(t => t.DiningType == MerchantEnum.Normal).Count();
                var NowUnitCanteen = MerTemp.Where(t => t.CreateTime >= DateTime.Now.AddDays(-DateTime.Now.Day + 1)).Where(t => t.DiningType == MerchantEnum.UnitCanteen).Count();
                var NowSmall = MerTemp.Where(t => t.CreateTime >= DateTime.Now.AddDays(-DateTime.Now.Day + 1)).Where(t => t.DiningType > MerchantEnum.UnitCanteen).Count();
                var NowCook = CookTemp.Where(t => t.CreateTime >= DateTime.Now.AddDays(-DateTime.Now.Day + 1)).Count();
                ResponseSystemCompanyCount CompanyCount = new ResponseSystemCompanyCount
                {
                    AreaName = o.Name,
                    HistoryPlant = HistoryPlant,
                    HistoryCulture = HistoryCulture,
                    HistoryProduction = HistoryProduction,
                    HistoryCirculation = HistoryCirculation,
                    HistoryOther = HistoryOther,
                    HistoryNormal = HistoryNormal,
                    HistoryUnitCanteen = HistoryUnitCanteen,
                    HistorySmall = HistorySmall,
                    HistoryCook = HistoryCook,
                    NowPlant = NowPlant,
                    NowCulture = NowCulture,
                    NowProduction = NowProduction,
                    NowCirculation = NowCirculation,
                    NowOther = NowOther,
                    NowNormal = NowNormal,
                    NowUnitCanteen = NowUnitCanteen,
                    NowSmall = NowSmall,
                    NowCook = NowCook
                };
                CompanyCountList.Add(CompanyCount);
            });
            return CompanyCountList;
        }

        /// <summary>
        /// 产品统计
        /// </summary>
        /// <returns></returns>
        public IList<ResponseSystemProductCount> GetProductCountCenter()
        {
            List<EnterpriseGoods> queryable = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false).Where(t => t.AuditType == AuditEnum.AuditSuccess).ToList();
            List<EnterpriseInfo> Enterprise = Kily.Set<EnterpriseInfo>().Where(t => !string.IsNullOrEmpty(t.TypePath)).Where(t => t.IsDelete == false).ToList();
            IList<ResponseSystemProductCount> ProductCountList = new List<ResponseSystemProductCount>();
            List<TmepArea> areas = null;
            if (UserInfo().AccountType <= AccountEnum.Country)
                areas = Kily.Set<SystemProvince>().Where(t => t.IsDelete == false).AsNoTracking()
                    .Select(t => new TmepArea() { Id = t.Id.ToString(), Name = t.Name }).ToList();
            else if (UserInfo().AccountType == AccountEnum.Province)
            {
                var code = Kily.Set<SystemProvince>().Where(t => t.Id.ToString() == UserInfo().Province).Select(t => t.Code).FirstOrDefault();
                areas = Kily.Set<SystemCity>().Where(t => t.ProvinceCode == code).Select(t => new TmepArea() { Id = t.Id.ToString(), Name = t.Name }).ToList();
            }
            else if (UserInfo().AccountType == AccountEnum.City)
            {
                var code = Kily.Set<SystemCity>().Where(t => t.Id.ToString() == UserInfo().City).Select(t => t.Code).FirstOrDefault();
                areas = Kily.Set<SystemArea>().Where(t => t.CityCode == code).Select(t => new TmepArea() { Id = t.Id.ToString(), Name = t.Name }).ToList();
            }
            else if (UserInfo().AccountType == AccountEnum.Area)
            {
                var code = Kily.Set<SystemArea>().Where(t => t.Id.ToString() == UserInfo().Area).Select(t => t.Code).FirstOrDefault();
                areas = Kily.Set<SystemTown>().Where(t => t.AreaCode == code).Select(t => new TmepArea() { Id = t.Id.ToString(), Name = t.Name }).ToList();
            }
            areas.ForEach(o =>
            {
                var Temp = queryable.Join(Enterprise, t => t.CompanyId, x => x.Id, (t, x) => new { t, x }).Where(t => t.x.TypePath.Contains(o.Id));
                //历史累计
                int HistoryFarmer = Temp.Where(t => t.t.CreateTime < DateTime.Now.AddDays(-DateTime.Now.Day)).Where(t => t.t.ProductType.Equals("农产品")).Count();
                int HistoryFood = Temp.Where(t => t.t.CreateTime < DateTime.Now.AddDays(-DateTime.Now.Day)).Where(t => t.t.ProductType.Equals("食品")).Count();
                int HistoryDrug = Temp.Where(t => t.t.CreateTime < DateTime.Now.AddDays(-DateTime.Now.Day)).Where(t => t.t.ProductType.Equals("药品")).Count();
                int HistoryCosplay = Temp.Where(t => t.t.CreateTime < DateTime.Now.AddDays(-DateTime.Now.Day)).Where(t => t.t.ProductType.Equals("化妆品")).Count();
                int HistoryMachine = Temp.Where(t => t.t.CreateTime < DateTime.Now.AddDays(-DateTime.Now.Day)).Where(t => t.t.ProductType.Equals("医疗器械")).Count();
                int HistoryOhter = Temp.Where(t => t.t.CreateTime < DateTime.Now.AddDays(-DateTime.Now.Day)).Where(t => t.t.ProductType.Equals("其他")).Count();
                //本月新增
                int NowFarmer = Temp.Where(t => t.t.CreateTime >= DateTime.Now.AddDays(-DateTime.Now.Day + 1)).Where(t => t.t.ProductType.Equals("农产品")).Count();
                int NowFood = Temp.Where(t => t.t.CreateTime >= DateTime.Now.AddDays(-DateTime.Now.Day + 1)).Where(t => t.t.ProductType.Equals("食品")).Count();
                int NowDrug = Temp.Where(t => t.t.CreateTime >= DateTime.Now.AddDays(-DateTime.Now.Day + 1)).Where(t => t.t.ProductType.Equals("药品")).Count();
                int NowCosplay = Temp.Where(t => t.t.CreateTime >= DateTime.Now.AddDays(-DateTime.Now.Day + 1)).Where(t => t.t.ProductType.Equals("化妆品")).Count();
                int NowMachine = Temp.Where(t => t.t.CreateTime >= DateTime.Now.AddDays(-DateTime.Now.Day + 1)).Where(t => t.t.ProductType.Equals("医疗器械")).Count();
                int NowOhter = Temp.Where(t => t.t.CreateTime >= DateTime.Now.AddDays(-DateTime.Now.Day + 1)).Where(t => t.t.ProductType.Equals("其他")).Count();
                ResponseSystemProductCount ProductCount = new ResponseSystemProductCount
                {
                    AreaName = o.Name,
                    HistoryFarmer = HistoryFarmer,
                    HistoryFood = HistoryFood,
                    HistoryDrug = HistoryDrug,
                    HistoryCosplay = HistoryCosplay,
                    HistoryMachine = HistoryMachine,
                    HistoryOhter = HistoryOhter,
                    NowFarmer = NowFarmer,
                    NowFood = NowFood,
                    NowDrug = NowDrug,
                    NowCosplay = NowCosplay,
                    NowMachine = NowMachine,
                    NowOhter = NowOhter
                };
                ProductCountList.Add(ProductCount);
            });
            return ProductCountList;
        }

        /// <summary>
        /// 合同统计
        /// </summary>
        /// <param name="RangTime"></param>
        /// <returns></returns>
        public ResponseSystemContractTotalCount GetContractCountCenter(RequestRangeDate Range)
        {
            IQueryable<SystemStayContract> queryable = Kily.Set<SystemStayContract>().Where(t => t.AuditType == AuditEnum.AuditSuccess);
            if (Range.STime.HasValue && Range.ETime.HasValue)
                queryable = queryable.Where(t => t.CreateTime >= Range.STime && t.CreateTime <= Range.ETime);
            List<SystemStayContract> Contracts = queryable.ToList();
            List<EnterpriseInfo> Enterprises = Kily.Set<EnterpriseInfo>().Where(t => !string.IsNullOrEmpty(t.TypePath)).Where(t => t.AuditType == AuditEnum.AuditSuccess).Where(t => t.IsDelete == false).ToList();
            List<RepastInfo> Repasts = Kily.Set<RepastInfo>().Where(t => !string.IsNullOrEmpty(t.TypePath)).Where(t => t.AuditType == AuditEnum.AuditSuccess).Where(t => t.IsDelete == false).ToList();
            List<TmepArea> areas = null;
            if (UserInfo().AccountType <= AccountEnum.Country)
                areas = Kily.Set<SystemProvince>().Where(t => t.IsDelete == false).AsNoTracking()
                    .Select(t => new TmepArea() { Id = t.Id.ToString(), Name = t.Name }).ToList();
            else if (UserInfo().AccountType == AccountEnum.Province)
            {
                var code = Kily.Set<SystemProvince>().Where(t => t.Id.ToString() == UserInfo().Province).Select(t => t.Code).FirstOrDefault();
                areas = Kily.Set<SystemCity>().Where(t => t.ProvinceCode == code).Select(t => new TmepArea() { Id = t.Id.ToString(), Name = t.Name }).ToList();
            }
            else if (UserInfo().AccountType == AccountEnum.City)
            {
                var code = Kily.Set<SystemCity>().Where(t => t.Id.ToString() == UserInfo().City).Select(t => t.Code).FirstOrDefault();
                areas = Kily.Set<SystemArea>().Where(t => t.CityCode == code).Select(t => new TmepArea() { Id = t.Id.ToString(), Name = t.Name }).ToList();
            }
            else if (UserInfo().AccountType == AccountEnum.Area)
            {
                var code = Kily.Set<SystemArea>().Where(t => t.Id.ToString() == UserInfo().Area).Select(t => t.Code).FirstOrDefault();
                areas = Kily.Set<SystemTown>().Where(t => t.AreaCode == code).Select(t => new TmepArea() { Id = t.Id.ToString(), Name = t.Name }).ToList();
            }
            List<ResponseSystemContractCount> TotalContract = new List<ResponseSystemContractCount>();
            areas.ForEach(item =>
            {
                var data = Contracts.Where(t => t.TypePath.Contains(item.Id.ToLower())).Select(t => new ResponseSystemContractCount
                {
                    CompanyName = t.CompanyName,
                    AreaName = item.Name,
                    Plant = Enterprises.Where(x => x.Id == t.CompanyId).Where(x => x.CompanyType == CompanyEnum.Plant).Count(),
                    Feed = Enterprises.Where(x => x.Id == t.CompanyId).Where(x => x.CompanyType == CompanyEnum.Culture).Count(),
                    Production = Enterprises.Where(x => x.Id == t.CompanyId).Where(x => x.CompanyType == CompanyEnum.Production).Count(),
                    Roop = Enterprises.Where(x => x.Id == t.CompanyId).Where(x => x.CompanyType == CompanyEnum.Circulation).Count(),
                    Mer = Repasts.Where(x => x.Id == t.CompanyId).Where(x => x.DiningType == MerchantEnum.Normal).Count(),
                    Unit = Repasts.Where(x => x.Id == t.CompanyId).Where(x => x.DiningType == MerchantEnum.UnitCanteen).Count(),
                    Alipay = Contracts.Where(x => x.Id == t.Id).Where(x => x.PayType == PayEnum.Alipay).Count(),
                    WxPay = Contracts.Where(x => x.Id == t.Id).Where(x => x.PayType == PayEnum.WxPay).Count(),
                    UnionPay = Contracts.Where(x => x.Id == t.Id).Where(x => x.PayType == PayEnum.Unionpay).Count(),
                    AgentPay = Contracts.Where(x => x.Id == t.Id).Where(x => x.PayType == PayEnum.AgentPay).Count(),
                    Test = Contracts.Where(x => x.Id == t.Id).Where(x => x.VersionType == SystemVersionEnum.Test).Count(),
                    Base = Contracts.Where(x => x.Id == t.Id).Where(x => x.VersionType == SystemVersionEnum.Base).Count(),
                    Lv = Contracts.Where(x => x.Id == t.Id).Where(x => x.VersionType == SystemVersionEnum.Level).Count(),
                    LastVer = Contracts.Where(x => x.Id == t.Id).Where(x => x.VersionType == SystemVersionEnum.Enterprise).Count(),
                    ContractMoneny = Convert.ToInt64(t.TotalPrice),
                    WayTags = (t.VersionType == SystemVersionEnum.Test ? ServiceMessage.TEST : (t.VersionType == SystemVersionEnum.Base ? ServiceMessage.BASE : (t.VersionType == SystemVersionEnum.Level ? ServiceMessage.LEVEL : ServiceMessage.ENTERPRISE))),
                    TargetMoneny = Convert.ToInt64(t.ActualPrice),
                    LostMoneny = Convert.ToInt64(t.ActualPrice),
                    CountYear = Convert.ToInt32(t.ContractYear)
                }).ToList();
                TotalContract.AddRange(data);
            });
            if (!string.IsNullOrEmpty(Range.Area))
                TotalContract = TotalContract.Where(t => t.AreaName.Equals(Range.Area)).ToList();
            ResponseSystemContractTotalCount Temp = new ResponseSystemContractTotalCount
            {
                ContractCounts = TotalContract,
                Psum = TotalContract.Sum(t => t.Plant),
                Fsum = TotalContract.Sum(t => t.Feed),
                Prsum = TotalContract.Sum(t => t.Production),
                Rsum = TotalContract.Sum(t => t.Roop),
                Msum = TotalContract.Sum(t => t.Mer),
                Usum = TotalContract.Sum(t => t.Unit),
                AlipaySum = TotalContract.Sum(t => t.Alipay),
                WxPaySum = TotalContract.Sum(t => t.WxPay),
                UnionPaySum = TotalContract.Sum(t => t.UnionPay),
                AgentPaySum = TotalContract.Sum(t => t.AgentPay),
                TestSum = TotalContract.Sum(t => t.Test),
                BaseSum = TotalContract.Sum(t => t.Base),
                LvSum = TotalContract.Sum(t => t.Lv),
                LastVerSum = TotalContract.Sum(t => t.LastVer),
                ContractMonenySum = TotalContract.Sum(t => t.ContractMoneny),
                WayTagsSum = TotalContract.Sum(t => t.WayTags),
                TargetMonenySum = TotalContract.Sum(t => t.TargetMoneny),
                LostMonenySum = TotalContract.Sum(t => t.LostMoneny),
                CountYearSum = TotalContract.Sum(t => t.CountYear)
            };
            return Temp;
        }

        #endregion 数据报表

        #region 订单管理

        #region 订单中心

        /// <summary>
        /// 订单分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseSystemOrder> GetOrderPage(PageParamList<RequestSystemOrder> pageParam)
        {
            IQueryable<SystemOrder> queryable = Kily.Set<SystemOrder>().OrderByDescending(t => t.SubmitTime);
            if (pageParam.QueryParam.OrderStatus.HasValue)
                queryable = queryable.Where(t => t.OrderStatus == pageParam.QueryParam.OrderStatus);
            if (pageParam.QueryParam.IsExpire.HasValue)
                queryable = queryable.Where(t => t.IsExpire == pageParam.QueryParam.IsExpire);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.OrderNo))
                queryable = queryable.Where(t => t.OrderNo == pageParam.QueryParam.OrderNo);
            if (pageParam.QueryParam.IsApp)
                queryable = queryable.Where(t => t.OrderStatus == OrderEnum.Dispatch && !t.IsExpire.Value);
            if (pageParam.QueryParam.CompanyId.HasValue)
                queryable = queryable.Where(t => t.CompanyId == pageParam.QueryParam.CompanyId);
            if (pageParam.QueryParam.GovtId.HasValue)
                queryable = queryable.Where(t => t.GovtId == pageParam.QueryParam.GovtId);
            var data = queryable.Select(t => new ResponseSystemOrder
            {
                Id = t.Id,
                CompanyName = t.CompanyName,
                OrderType = t.OrderType,
                GovtName = t.GovtName,
                IsExpire = t.IsExpire,
                SubmitTime = t.SubmitTime,
                OrderStatusTxt = (t.GovtId.HasValue && t.OrderStatus == OrderEnum.WaitAudit) ? "等待处理" : AttrExtension.GetSingleDescription<OrderEnum, DescriptionAttribute>(t.OrderStatus),
                OrderAccepter = t.OrderAccepter,
                OrderNo = t.OrderNo,
                OrderAccepterTime = t.OrderAccepterTime,
                OrderStatus = t.OrderStatus,
                ServicePrice = t.ServicePrice,
                ServiceVersion = t.ServiceVersion
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 下单
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string OrderEdit(RequestSystemOrder Param)
        {
            SystemOrder Order = Param.MapToEntity<SystemOrder>();
            Order.OrderStatus = OrderEnum.WaitAudit;
            if (Order.Id == Guid.Empty)
                return Insert(Order) ? "下单成功" : "下单失败";
            else
                return Update(Order, Param, true) ? "改单成功" : "改单失败";
        }

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseSystemOrder GetOrderDetail(Guid Id)
        {
            return Kily.Set<SystemOrder>().Where(t => t.Id == Id).FirstOrDefault().MapToEntity<ResponseSystemOrder>();
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public string OrderCheck(RequestSystemOrder Param)
        {
            SystemOrder Order = Kily.Set<SystemOrder>().Where(t => t.Id == Param.Id).FirstOrDefault();
            RequestSystemOrderLog Log = new RequestSystemOrderLog
            {
                OrderId = Param.Id,
                HandlerTime = DateTime.Now,
                HandlerUser = UserInfo().TrueName,
                OrderStatus = Param.OrderStatus,
                OrderRemark = Param.Remark
            };
            //接单
            if (!string.IsNullOrEmpty(Param.OrderAccepter))
            {
                Log.HandlerUser = Param.OrderAccepter;
                Update(Order, Param);
            }
            switch (Param.OrderStatus)
            {
                case OrderEnum.NoAudit:
                    Order.OrderStatus = OrderEnum.NoAudit;
                    Log.LogType = "审核";
                    break;

                case OrderEnum.AuditSuccess:
                    Order.OrderStatus = OrderEnum.AuditSuccess;
                    Log.LogType = "审核";
                    break;

                case OrderEnum.Dispatch:
                    Order.OrderStatus = OrderEnum.Dispatch;
                    Log.LogType = "派单";
                    break;

                case OrderEnum.AcceptOrder:
                    Order.OrderStatus = OrderEnum.AcceptOrder;
                    Log.LogType = "接单";
                    break;

                case OrderEnum.Complete:
                    Order.OrderStatus = OrderEnum.Complete;
                    Log.LogType = "完成";
                    break;

                case OrderEnum.End:
                    Order.OrderStatus = OrderEnum.End;
                    Log.LogType = "归档";
                    break;

                default:
                    Order.OrderStatus = Order.OrderStatus;
                    break;
            }
            EditOrderLog(Log);
            return UpdateField(Order, "OrderStatus") ? ServiceMessage.HANDLESUCCESS : ServiceMessage.HANDLEFAIL;
        }

        #endregion 订单中心

        #region 订单日志

        /// <summary>
        /// 日志分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseSystemOrderLog> GetOrderLogPage(PageParamList<RequestSystemOrderLog> pageParam)
        {
            IQueryable<SystemOrderLog> queryable = Kily.Set<SystemOrderLog>().OrderByDescending(t => t.HandlerTime).Where(t => t.IsDelete == false);
            if (pageParam.QueryParam.HandlerTime.HasValue)
                queryable = queryable.Where(t => t.HandlerTime <= pageParam.QueryParam.HandlerTime);
            if (pageParam.QueryParam.OrderStatus.HasValue)
                queryable = queryable.Where(t => t.OrderStatus == pageParam.QueryParam.OrderStatus);
            var data = queryable.Select(t => new ResponseSystemOrderLog
            {
                HandlerUser = t.HandlerUser,
                HandlerTime = t.HandlerTime,
                LogType = t.LogType,
                Id = t.Id,
                OrderId = t.OrderId,
                OrderStatus = t.OrderStatus,
                OrderRemark = t.OrderRemark,
                OrderStatusTxt = AttrExtension.GetSingleDescription<OrderEnum, DescriptionAttribute>(t.OrderStatus)
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditOrderLog(RequestSystemOrderLog Param)
        {
            SystemOrderLog Log = Param.MapToEntity<SystemOrderLog>();
            return Insert(Log) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveLog(Guid Id)
        {
            return Remove<SystemOrderLog>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 日志详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseSystemOrderLog GetOrderLogDetail(Guid Id)
        {
            return Kily.Set<SystemOrderLog>().Where(t => t.Id == Id).FirstOrDefault().MapToEntity<ResponseSystemOrderLog>();
        }

        #endregion 订单日志

        #region 评分记录

        /// <summary>
        /// 评分列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseSystemOrderScore> GetOrderScorePage(PageParamList<RequestSystemOrderScore> pageParam)
        {
            IQueryable<SystemOrderScore> queryable = Kily.Set<SystemOrderScore>().OrderByDescending(t => t.ScoreTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.ScoreCompany))
                queryable = queryable.Where(t => t.ScoreCompany.Contains(pageParam.QueryParam.ScoreCompany));
            if (pageParam.QueryParam.ScoreTime.HasValue)
                queryable = queryable.Where(t => t.ScoreTime <= pageParam.QueryParam.ScoreTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.ScoreCompany))
                queryable = queryable.Where(t => t.ScoreCompany.Equals(pageParam.QueryParam.ScoreCompany));
            var data = queryable.Join(Kily.Set<SystemOrder>(), t => t.OrderNo, x => x.OrderNo, (t, x) => new { t, x.Id }).Select(t => new ResponseSystemOrderScore
            {
                Id = t.t.Id,
                OrderId = t.Id,
                OrderNo = t.t.OrderNo,
                Score = t.t.Score,
                ScoreTime = t.t.ScoreTime,
                ScoreCompany = t.t.ScoreCompany,
                OrderAccepter = t.t.OrderAccepter
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 新增评分记录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditOrderScore(RequestSystemOrderScore Param)
        {
            SystemOrderScore Score = Param.MapToEntity<SystemOrderScore>();
            SystemOrder Order = Kily.Set<SystemOrder>().Where(t => t.OrderNo == Score.OrderNo).FirstOrDefault();
            Order.OrderStatus = OrderEnum.Evaluative;
            EditOrderLog(new RequestSystemOrderLog
            {
                OrderId = Order.Id,
                HandlerTime = DateTime.Now,
                HandlerUser = Param.ScoreCompany,
                OrderStatus = OrderEnum.Evaluative,
                OrderRemark = Param.Remark,
                LogType = "评分"
            });
            UpdateField(Order, "OrderStatus");
            return Insert(Score) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        /// <summary>
        /// 评分详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseSystemOrderScore GetOrderScoreDetail(Guid Id)
        {
            return Kily.Set<SystemOrderScore>().Where(t => t.Id == Id).FirstOrDefault().MapToEntity<ResponseSystemOrderScore>();
        }

        #endregion 评分记录

        #region 线下人员

        /// <summary>
        /// 线下人员分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseSystemPersonOff> GetPersonOffPage(PageParamList<RequestSystemPersonOff> pageParam)
        {
            IQueryable<SystemPersonOff> queryable = Kily.Set<SystemPersonOff>().OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.UserNo))
                queryable = queryable.Where(t => t.UserNo == pageParam.QueryParam.UserNo);
            var data = queryable.Select(t => new ResponseSystemPersonOff
            {
                Id = t.Id,
                TrueName = t.TrueName,
                UserNo = t.UserNo,
                Gender = t.Gender,
                Phone = t.Phone,
                Status = t.Status,
                PayInfo = t.PayInfo
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 人员详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseSystemPersonOff GetOffDetail(Guid Id)
        {
            return Kily.Set<SystemPersonOff>().Where(t => t.Id == Id).FirstOrDefault().MapToEntity<ResponseSystemPersonOff>();
        }

        #endregion 线下人员

        #endregion 订单管理
    }
}