﻿using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.Model.Enterprise;
using KilyCore.EntityFrameWork.Model.System;
using KilyCore.EntityFrameWork.ModelEnum;
using KilyCore.Extension.AttributeExtension;
using KilyCore.Extension.AutoMapperExtension;
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
        /// 厂商列表
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public IList<ResponseEnterpriseSeller> GetSellerList(int Type)
        {
            IQueryable<EnterpriseSeller> queryable = Kily.Set<EnterpriseSeller>().Where(t => t.IsDelete == false).AsNoTracking();
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
        public IList<ResponseEnterpriseDictionary> GetDictionaryList()
        {
            var data = Kily.Set<EnterpriseDictionary>()
                .Where(t => t.IsDelete == false).GroupBy(t=>t.DicType)
                .Select(t => new ResponseEnterpriseDictionary()
                {
                    DicType=t.Key.ToString(),
                    DictionaryList = Kily.Set<EnterpriseDictionary>()
                   .Where(x => x.IsDelete == false)
                   .Where(x => x.DicType == t.Key.ToString()).Select(x => new ResponseEnterpriseDictionary()
                   {
                       Id = x.Id,
                       DicName = x.DicName,
                       DicValue = x.DicValue,
                       Remark = x.Remark
                   }).ToList()
                }).AsNoTracking().ToList();
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
                EnterpriseRoleAuthor Author = Kily.Set<EnterpriseRoleAuthor>().Where(t => t.IsDelete == false)
                    .Where(t => t.Id == CompanyInfo().EnterpriseRoleId).AsNoTracking().FirstOrDefault();
                queryable = queryable.Where(t => Author.AuthorMenuPath.Contains(t.Id.ToString())).AsNoTracking();
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
                    .Where(x => Author.AuthorMenuPath.Contains(x.Id.ToString()))
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
        public IList<ResponseParentTree> GetEnterpriseWebTree()
        {
            if (CompanyInfo() != null)
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
                         SelectedIcon = "fa fa-refresh fa-spin",
                         Nodes = Kily.Set<EnterpriseMenu>().Where(x => x.IsDelete == false)
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
                             SelectedIcon = "fa fa-refresh fa-spin",
                         }).AsQueryable()
                     }).AsQueryable();
                var data = queryable.ToList();
                return data;
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
                         BackClolor = "white",
                         SelectedIcon = "fa fa-refresh fa-spin",
                         Nodes = Kily.Set<EnterpriseMenu>().Where(x => x.IsDelete == false)
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
                             SelectedIcon = "fa fa-refresh fa-spin",
                         }).AsQueryable()
                     }).AsQueryable();
                var data = queryable.ToList();
                return data;

            }
        }
        #endregion

        #region 基础管理
        #region 企业资料
        /// <summary>
        /// 获取企业资料
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseEnterprise GetEnterpriseInfo(Guid Id)
        {
            var data = Kily.Set<EnterpriseInfo>().Where(t => t.Id == Id).Select(t => new ResponseEnterprise()
            {
                Id = t.Id,
                CompanyAccount = t.CompanyAccount,
                CommunityCode = t.CommunityCode,
                CompanyAddress = t.CompanyAddress,
                CompanyName = t.CompanyName,
                CompanyPhone = t.CompanyPhone,
                CompanyType = t.CompanyType,
                Version = t.Version,
                PassWord = t.PassWord,
                TypePath = t.TypePath,
                Certification = t.Certification,
                Honor = t.HonorCertification,
                Discription = t.Discription,
                NetAddress = t.NetAddress,
                ProductionAddress = t.ProductionAddress,
                SellerAddress = t.SellerAddress,
                VideoAddress = t.VideoAddress
            }).FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 更新企业资料
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditEnterprise(RequestEnterprise Param)
        {
            Param.AuditType = AuditEnum.WaitAduit;
            EnterpriseInfo data = Kily.Set<EnterpriseInfo>().Where(t => t.Id == Param.Id).FirstOrDefault();
            Param.EnterpriseRoleId = data.EnterpriseRoleId;

            EnterpriseInfo Info = Param.MapToEntity<EnterpriseInfo>();
            if (Update<EnterpriseInfo, RequestEnterprise>(Info, Param))
                return ServiceMessage.UPDATESUCCESS;
            else
                return ServiceMessage.UPDATEFAIL;
        }
        #endregion

        #region 保存合同和缴费凭证
        /// <summary>
        /// 保存合同和缴费凭证
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveContract(RequestStayContract Param)
        {
            Param.AuditType = AuditEnum.WaitAduit;
            SystemStayContract contract = Param.MapToEntity<SystemStayContract>();
            if (Insert<SystemStayContract>(contract))
                return ServiceMessage.INSERTSUCCESS;
            else
                return ServiceMessage.INSERTFAIL;
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
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id);
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
        /// 编辑子账户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditUser(RequestEnterpriseUser Param)
        {
            EnterpriseUser User = Param.MapToObj<RequestEnterpriseUser, EnterpriseUser>();
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
        /// 删除子账号
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveUser(Guid Id)
        {
            if (Delete(ExpressionExtension.GetExpression<EnterpriseUser>("Id", Id, ExpressionEnum.Equals)))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 集团账户权限列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseRoleAuthorWeb> GetRoleAuthorList()
        {
            IQueryable<EnterpriseRoleAuthorWeb> queryable = Kily.Set<EnterpriseRoleAuthorWeb>().OrderByDescending(t => t.CreateTime);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CreateUser == CompanyInfo().Id.ToString());
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

        #region 集团账户
        /// <summary>
        /// 新增账户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditRoleAuthor(RequestRoleAuthorWeb Param)
        {
            EnterpriseRoleAuthorWeb Author = Param.MapToEntity<EnterpriseRoleAuthorWeb>();
            if (Insert<EnterpriseRoleAuthorWeb>(Author))
                return ServiceMessage.INSERTSUCCESS;
            else
                return ServiceMessage.INSERTFAIL;
        }
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
                queryable = queryable.Where(t => t.CreateUser == CompanyInfo().Id.ToString());
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
        /// <summary>
        /// 删除账户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveRole(Guid Id)
        {
            if (Delete<EnterpriseRoleAuthorWeb>(t => t.Id == Id))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }
        #endregion

        #region  企业认证
        /// <summary>
        /// 企业认证
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string EditEnterpriseIdent(RequestEnterpriseIdent param)
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
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id);
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
        /// 删除码表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveDic(Guid Id)
        {
            return Delete<EnterpriseDictionary>(ExpressionExtension.GetExpression<EnterpriseDictionary>("Id", Id, ExpressionEnum.Equals)) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 字典详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseEnterpriseDictionary GetDicDetail(Guid Id)
        {
            var data = Kily.Set<EnterpriseDictionary>().AsNoTracking().Select(t => new ResponseEnterpriseDictionary()
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
        /// <summary>
        /// 编辑字典
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditDic(RequestEnterpriseDictionary Param)
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
            if (!string.IsNullOrEmpty(pageParam.QueryParam.BacthNo))
                queryable = queryable.Where(t => t.BacthNo.Contains(pageParam.QueryParam.BacthNo));
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseEnterpriseAgeUp()
            {
                Id = t.Id,
                BacthNo = t.BacthNo,
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
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id);
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseGrowInfo()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                BacthNo = t.BacthNo,
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
                BacthNo = t.BacthNo,
                GrowName = t.GrowName,
                BuyNum = t.BuyNum,
                PlantTime = t.PlantTime,
                Unit = t.Unit,
                Remark = t.Remark
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
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id);
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseGrowInfo()
            {
                BacthNo = t.BacthNo,
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
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id);
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterprisePlanting()
            {
                Id = t.Id,
                BacthNo = t.BacthNo,
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
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id);
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseDrug()
            {
                Id = t.Id,
                BacthNo = t.BacthNo,
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
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id);
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseEnterpriseEnvironment()
            {
                Id = t.Id,
                BacthNo = t.BacthNo,
                CompanyId = t.CompanyId,
                AirEnv = t.AirEnv,
                AirHdy = t.AirHdy,
                SoilEnv = t.SoilEnv,
                SoilHdy = t.SoilHdy,
                Light = t.Light,
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
            IQueryable<EnterpriseEnvironmentAttach> queryable = Kily.Set<EnterpriseEnvironmentAttach>().Where(t => t.IsDelete == false).Where(t => t.EnvId == pageParam.QueryParam.EnvId).AsNoTracking();
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CreateUser == CompanyInfo().Id.ToString());
            else
                queryable = queryable.Where(t => t.CreateUser == CompanyUser().Id.ToString());
            var data = queryable.Select(t => new ResponseEnterpriseEnvironmentAttach()
            {
                Id = t.Id,
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
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id);
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseEnterpriseNote()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                BacthNo = t.BacthNo,
                NoteName = t.NoteName,
                ResultTime = t.ResultTime,
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
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
                BacthNo = t.BacthNo,
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
            if (!string.IsNullOrEmpty(pageParam.QueryParam.BacthNo))
                queryable = queryable.Where(t => t.BacthNo.Contains(pageParam.QueryParam.BacthNo));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id);
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseTag()
            {
                Id = t.Id,
                BacthNo = t.BacthNo,
                CompanyId = t.CompanyId,
                EndSerialNo = t.EndSerialNo,
                StarSerialNo = t.StarSerialNo,
                TotalNo = t.TotalNo,
                TagType = t.TagType,
                TagTypeName = AttrExtension.GetSingleDescription<TagEnum, DescriptionAttribute>(t.TagType)
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
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
                .Where(t => t.CompanyId == Param.CompanyId).OrderByDescending(t => t.CreateTime);
            List<EnterpriseTag> TagList = queryables.ToList();
            if (TagList.Count == 0)
                Param.StarSerialNo = Convert.ToInt64(Province.Code + "100000000000");
            else
                Param.StarSerialNo = TagList.FirstOrDefault().EndSerialNo + 1;
            Param.EndSerialNo = Param.StarSerialNo + Param.TotalNo;
            EnterpriseTag Tag = Param.MapToEntity<EnterpriseTag>();
            if (Tag.TagType == TagEnum.OneEnterprise)
                return queryables.Where(t => t.TagType == TagEnum.OneEnterprise).ToList().Count >= 1 ?
                    "企业只能拥有一个企业二维码!" :
                    (Tag.TotalNo > 1 ? "一个企业只能创建一个企业二维码!" :
                    (Insert(Tag) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL));
            else
            {
                if (Apply == null)
                {
                    long Total = queryables.Where(t => t.IsApplay == false).Sum(t => t.TotalNo);
                    long Totals = (Total + Param.TotalNo);
                    Tag.IsApplay = false;
                    if ((CompanyInfo() != null ? CompanyInfo().Version : CompanyUser().Version) == SystemVersionEnum.Test)
                        return ServiceMessage.TEST - Totals >= 0 ? (Insert(Tag) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL) : $"当前剩余标签数量:{ServiceMessage.TEST - Total},请升级版本或申请购买数量!";
                    else if ((CompanyInfo() != null ? CompanyInfo().Version : CompanyUser().Version) == SystemVersionEnum.Base)
                        return ServiceMessage.BASE - Totals >= 0 ? (Insert(Tag) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL) : $"当前剩余标签数量:{ServiceMessage.BASE - Total},请升级版本或申请购买数量!";
                    else if ((CompanyInfo() != null ? CompanyInfo().Version : CompanyUser().Version) == SystemVersionEnum.Level)
                        return ServiceMessage.LEVEL - Totals >= 0 ? (Insert(Tag) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL) : $"当前剩余标签数量:{ServiceMessage.LEVEL - Total},请升级版本或申请购买数量!";
                    else
                        return ServiceMessage.ENTERPRISE - Totals >= 0 ? (Insert(Tag) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL) : $"当前剩产标签数量:{ServiceMessage.ENTERPRISE - Total},请升级版本或申请购买数量!";
                }
                else
                {
                    long Total = queryables.Where(t => t.IsApplay == true).Sum(t => t.TotalNo);
                    long Totals = (Total + Param.TotalNo);
                    Tag.IsApplay = true;
                    if (Apply.Payment == 2)
                    {
                        return Convert.ToInt64(Apply.ApplyNum) - Totals >= 0 ? (Insert(Tag) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL) : $"当前剩余标签数量:{Convert.ToInt64(Apply.ApplyNum) - Total},请升级版本或申请购买数量!";
                    }
                    else
                    {
                        return (bool)Apply.IsPay ? (Insert(Tag) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL) : "请先付款";
                    }
                }
            }
        }
        /// <summary>
        /// 删除二维码
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveTag(Guid Id)
        {
            if (Delete<EnterpriseTag>(t => t.Id == Id))
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
            if (!string.IsNullOrEmpty(pageParam.QueryParam.BacthNo))
                queryable = queryable.Where(t => t.BacthNo.Contains(pageParam.QueryParam.BacthNo));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id);
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.OrderByDescending(t => t.CreateTime).AsNoTracking().Select(t => new ResponseEnterpriseApply()
            {
                Id = t.Id,
                BacthNo = t.BacthNo,
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
                BacthNo = t.BacthNo,
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
                IList<String> Fieds = new List<String>();
                Fieds.Add("IsPay");
                Fieds.Add("PaytTicket");
                return UpdateField<EnterpriseTagApply>(TagApply, null, Fieds) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            }
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
                queryable = queryable.Where(t => t.CreateUser == CompanyInfo().Id.ToString());
            else
                queryable = queryable.Where(t => t.CreateUser == CompanyUser().Id.ToString());
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
        /// 删除厂商
        /// </summary>
        /// <param name="Id"></param>
        public string RemoveSeller(Guid Id)
        {
            return Delete<EnterpriseSeller>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
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
                queryable = queryable.Where(t => t.CreateUser == CompanyInfo().Id.ToString());
            else
                queryable = queryable.Where(t => t.CreateUser == CompanyUser().Id.ToString());
            var data = queryable.Select(t => new ResponseEnterpriseMaterial()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                BacthNo = t.BacthNo,
                MaterName = t.MaterName,
                Spec = t.Spec,
                Standard = t.Standard,
                Supplier = t.Supplier,
                Unit = t.Unit,
                Address = t.Address,
                ExpiredDay = t.ExpiredDay,
                PackageType=t.PackageType,
                MaterNum=t.MaterNum
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
        #endregion
    }
}