﻿using KilyCore.DataEntity.RequestMapper.Cook;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Govt;
using KilyCore.DataEntity.RequestMapper.Repast;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Cook;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.Function;
using KilyCore.DataEntity.ResponseMapper.Govt;
using KilyCore.DataEntity.ResponseMapper.Repast;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.Model.Cook;
using KilyCore.EntityFrameWork.Model.Enterprise;
using KilyCore.EntityFrameWork.Model.Govt;
using KilyCore.EntityFrameWork.Model.Repast;
using KilyCore.EntityFrameWork.Model.System;
using KilyCore.EntityFrameWork.ModelEnum;
using KilyCore.Extension.AttributeExtension;
using KilyCore.Extension.AutoMapperExtension;
using KilyCore.Extension.UtilExtension;
using KilyCore.Repositories.BaseRepository;
using KilyCore.Service.ConstMessage;
using KilyCore.Service.IServiceCore;
using KilyCore.Service.QueryExtend;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

#region << 版 本 注 释 >>

/*----------------------------------------------------------------
* 类 名 称 ：GovtWebService
* 类 描 述 ：
* 命名空间 ：KilyCore.Service.ServiceCore
* 机器名称 ：EMILY
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/7 11:20:01
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/

#endregion << 版 本 注 释 >>

namespace KilyCore.Service.ServiceCore
{
    public class GovtWebService : Repository, IGovtWebService
    {
        #region 获取机构管理区域

        /// <summary>
        /// 获取机构管理区域
        /// </summary>
        /// <returns></returns>
        public IList<string> GetDepartArea()
        {
            IQueryable<GovtInstitution> queryable = Kily.Set<GovtInstitution>().Where(t => t.IsDelete == false).Where(t => t.Id == GovtInfo().DepartId);
            String Area = queryable.Select(t => t.ManageArea).FirstOrDefault();
            if (!string.IsNullOrEmpty(Area))
            {
                if (Area.Contains(","))
                    return Area.Split(",").ToList();
                else
                    return Area.Split("").ToList();
            }
            else
                return null;
        }

        #endregion 获取机构管理区域

        #region 获取全局集团菜单

        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <returns></returns>
        public IList<ResponseGovtMenu> GetGovtMenu()
        {
            IQueryable<GovtMenu> queryable = Kily.Set<GovtMenu>().Where(t => t.Level == MenuEnum.LevelOne).Where(t => t.IsDelete == false).AsNoTracking().AsQueryable().OrderBy(t => t.CreateTime);
            IQueryable<GovtRoleAuthor> queryables = Kily.Set<GovtRoleAuthor>().Where(t => t.IsDelete == false);
            GovtRoleAuthor Author = null;
            if (!GovtInfo().IsEdu.Value)
            {
                if (GovtInfo().AccountType <= GovtAccountEnum.Area)
                {
                    queryables = queryables.Where(t => t.AuthorName.Contains("区县"));
                    queryable = queryable.Where(t => t.MenuName != "学校监管");
                }
                else
                {
                    queryables = queryables.Where(t => t.AuthorName.Contains("乡镇"));
                    queryable = queryable.Where(t => t.MenuName != "学校监管");
                }
                Author = queryables.FirstOrDefault();
            }
            else
            {
                queryables = queryables.Where(t => t.AuthorName.Contains("教育局"));
                Author = queryables.FirstOrDefault();
                queryable = queryable.Where(t => Author.AuthorMenuPath.Contains(t.Id.ToString()));
            }
            var data = queryable.OrderBy(t => t.CreateTime).Select(t => new ResponseGovtMenu()
            {
                Id = t.Id,
                MenuId = t.MenuId,
                ParentId = t.ParentId,
                MenuAddress = t.MenuAddress,
                MenuName = t.MenuName,
                HasChildrenNode = t.HasChildrenNode,
                MenuIcon = t.MenuIcon,
                MenuChildren = Kily.Set<GovtMenu>()
             .Where(x => x.ParentId == t.MenuId)
             .Where(x => x.Level != MenuEnum.LevelOne)
             .Where(x => x.IsDelete == false)
             .Where(x => Author.AuthorMenuPath.Contains(x.Id.ToString()))
             .OrderBy(x => x.CreateTime).Select(x => new ResponseGovtMenu()
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

        #endregion 获取全局集团菜单

        #region 获取所有商家和企业

        /// <summary>
        /// 获取所有商家和企业
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public Object GetAllMerchant(String Key)
        {
            var Data = Kily.Set<EnterpriseInfo>().Where(t => t.CompanyName.Contains(Key)).AsNoTracking().Select(t => new
            {
                t.Id,
                Name = t.CompanyName
            }).ToList();
            var Datas = Kily.Set<RepastInfo>().Where(t => t.MerchantName.Contains(Key)).AsNoTracking().Select(t => new
            {
                t.Id,
                Name = t.MerchantName
            }).ToList();
            Data.AddRange(Datas);
            return Data;
        }

        #endregion 获取所有商家和企业

        #region 登录

        /// <summary>
        /// 监管登录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public ResponseGovtInfo GovtLogin(RequestGovtInfo Param)
        {
            var data = Kily.Set<GovtInfo>().Where(t => t.Account.Equals(Param.Account) || t.Phone.Equals(Param.Account))
                 .Where(t => t.PassWord.Equals(Param.PassWord)).Select(t => new ResponseGovtInfo()
                 {
                     Id = t.Id,
                     GovtId = t.GovtId,
                     TableName = typeof(ResponseGovtInfo).Name,
                     Account = t.Account,
                     IsEdu = t.IsEdu,
                     Phone = t.Phone,
                     AccountType = t.AccountType,
                     TrueName = t.TrueName,
                     TypePath = t.TypePath,
                     AccountTypeName = AttrExtension.GetSingleDescription<GovtAccountEnum, DescriptionAttribute>(t.AccountType),
                     DepartName = t.DepartName,
                     PassWord = t.PassWord,
                     DepartId = t.DepartId,
                     Email = t.Email,
                     Flag = t.UpdateUser,
                     IsActiveUser = t.IsActiveUser,
                     WorkNum = t.WorkNum,
                     ActiveImg = t.ActiveImg
                 }).AsNoTracking().FirstOrDefault();
            return data;
        }

        /// <summary>
        /// 第一次登录更新密码
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditPwd(RequestGovtInfo Param)
        {
            GovtInfo info = Kily.Set<GovtInfo>().Where(t => t.Id == Param.Id).FirstOrDefault();
            info.PassWord = Param.PassWord;
            return UpdateField(info, "PassWord") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        #endregion 登录

        #region 企业监管

        /// <summary>
        /// 监管企业分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterprise> GetCompanyPage(PageParamList<RequestEnterprise> pageParam)
        {
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>()
                 .Where(t => t.CompanyType == pageParam.QueryParam.CompanyType)
                 .Where(t => t.AuditType == AuditEnum.AuditSuccess)
                 .OrderByDescending(t => t.CreateTime);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<EnterpriseInfo, bool>> exp_1 = null;

                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                            }
                        }
                        queryable = queryable.Where(exp_1);
                    }
                    else
                    {
                        queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                    }
                }
                else
                    queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
            }
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CompanyName))
                queryable = queryable.Where(t => t.CompanyName.Contains(pageParam.QueryParam.CompanyName));
            var data = queryable.Select(t => new ResponseEnterprise()
            {
                Id = t.Id,
                CompanyName = t.CompanyName,
                CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType),
                Category = t.Category,
                CommunityCode = t.CommunityCode,
                CompanyPhone = t.CompanyPhone,
                CompanyAddress = t.CompanyAddress
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 监管餐饮分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseMerchant> GetMerchantPage(PageParamList<RequestMerchant> pageParam)
        {
            IQueryable<RepastInfo> queryable = Kily.Set<RepastInfo>()
                .Where(t => t.AuditType == AuditEnum.AuditSuccess)
                .OrderByDescending(t => t.CreateTime);
            if (pageParam.QueryParam.DiningType <= MerchantEnum.UnitCanteen)
                queryable = queryable.Where(t => t.DiningType == pageParam.QueryParam.DiningType);
            else
                queryable = queryable.Where(t => t.DiningType > MerchantEnum.UnitCanteen);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<RepastInfo, bool>> exp_1 = null;

                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                            }
                        }
                        queryable = queryable.Where(exp_1);
                    }
                    else
                        queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                }
                else
                    queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
            }
            if (!string.IsNullOrEmpty(pageParam.QueryParam.MerchantName))
                queryable = queryable.Where(t => t.MerchantName.Contains(pageParam.QueryParam.MerchantName));
            if (!string.IsNullOrEmpty(pageParam.QueryParam.AllowUnit))
                queryable = queryable.Where(t => t.AllowUnit.Contains(pageParam.QueryParam.AllowUnit));
            PagedResult<ResponseMerchant> data = new PagedResult<ResponseMerchant>();
            if (!GovtInfo().IsEdu.Value)
                data = queryable.Select(t => new ResponseMerchant()
                {
                    Id = t.Id,
                    MerchantName = t.MerchantName,
                    DiningTypeName = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType),
                    CommunityCode = t.CommunityCode,
                    Phone = t.Phone,
                    CardExpiredDate = t.CardExpiredDate,
                    MerchantSafeLv = t.MerchantSafeLv,
                    Certification = t.Certification,
                    AllowUnit = t.AllowUnit,
                    Address = t.Address,
                    ImplUser = t.ImplUser
                }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            else
                data = queryable.Select(t => new ResponseMerchant()
                {
                    Id = t.Id,
                    MerchantName = t.MerchantName,
                    DiningTypeName = t.AllowUnit,
                    CommunityCode = t.CommunityCode,
                    Phone = t.Phone,
                    CardExpiredDate = t.CardExpiredDate,
                    MerchantSafeLv = t.MerchantSafeLv,
                    Certification = t.Certification,
                    AllowUnit = t.AllowUnit,
                    Address = t.Address,
                    ImplUser = t.ImplUser
                }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 企业详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseEnterprise GetCompanyDetail(Guid Id)
        {
            var data = Kily.Set<EnterpriseInfo>().Where(t => t.Id == Id).Select(t => new ResponseEnterprise()
            {
                Id = t.Id,
                CompanyName = t.CompanyName,
                CompanyAddress = t.CompanyAddress,
                CommunityCode = t.CommunityCode,
                CompanyPhone = t.CompanyPhone,
                Certification = t.Certification,
                CompanyType = t.CompanyType,
                CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType),
                Scope = t.Scope,
                VideoAddress = t.VideoAddress,
                CardExpiredDate = t.CardExpiredDate,
                ProductionAddress = t.ProductionAddress,
                OfferLv = t.OfferLv,
                SafeOffer = t.SafeOffer,
                SellerAddress = t.SellerAddress,
                NatureAgent = t.NatureAgent,
                NetAddress = t.NetAddress,
                MainPro = t.MainPro,
                IdCard = t.IdCard,
                Honor = t.HonorCertification,
                Discription = t.Discription,
                Category = t.Category,
                Video = Kily.Set<EnterpriseVedio>().Where(x => x.CompanyId == Id && x.IsIndex == true)
                .OrderByDescending(x => x.CreateTime).Select(x => x.VedioAddr).Take(4).ToList()
            }).AsNoTracking().FirstOrDefault();
            return data;
        }

        /// <summary>
        /// 商家详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseMerchant GetRepastDetail(Guid Id)
        {
            var data = Kily.Set<RepastInfo>().Where(t => t.Id == Id).Select(t => new ResponseMerchant()
            {
                Id = t.Id,
                MerchantName = t.MerchantName,
                CommunityCode = t.CommunityCode,
                Certification = t.Certification,
                IdCard = t.IdCard,
                Address = t.Address,
                DiningTypeName = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType),
                Email = t.Email,
                ImplUser = t.ImplUser,
                Phone = t.Phone,
                AllowUnit = t.AllowUnit,
                Honor = t.HonorCertification,
                Remark = t.Remark.Replace("/Upload/", "http://system.cfda.vip/Upload/"),
                Video = Kily.Set<RepastVideo>().Where(x => x.InfoId == Id && x.IsIndex == true)
                .OrderByDescending(x => x.CreateTime).Select(m => new ResponseRepastVideo
                {
                    MonitorAddress = m.MonitorAddress,
                    VideoAddress = m.VideoAddress,
                    CoverPhoto = m.CoverPhoto
                }).Take(4).ToList()
            }).AsNoTracking().FirstOrDefault();
            return data;
        }

        #region 过时的接口

        /// <summary>
        /// 查询企业
        /// </summary>
        /// <param name="KeyWord"></param>
        /// <returns></returns>
        [Obsolete]
        public object GetAllComWithKeyWord(string KeyWord)
        {
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>()
              .Where(t => t.AuditType == AuditEnum.AuditSuccess)
              .OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(KeyWord))
                queryable = queryable.Where(t => t.CompanyName.Contains(KeyWord));
            return queryable.ToList();
        }

        /// <summary>
        /// 查询餐饮
        /// </summary>
        /// <param name="KeyWord"></param>
        /// <returns></returns>
        [Obsolete]
        public object GetAllMerWithKeyWord(string KeyWord)
        {
            IQueryable<RepastInfo> queryable = Kily.Set<RepastInfo>()
              .Where(t => t.AuditType == AuditEnum.AuditSuccess)
              .OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(KeyWord))
                queryable = queryable.Where(t => t.MerchantName.Contains(KeyWord));
            return queryable.ToList();
        }

        /// <summary>
        /// 获取所有企业
        /// </summary>
        /// <param name="Area"></param>
        /// <param name="ComType"></param>
        /// <returns></returns>
        [Obsolete]
        public object GetAllCom(string Area, int ComType)
        {
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>()
                 .Where(t => t.AuditType == AuditEnum.AuditSuccess)
                 .OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(Area))
                queryable = queryable.Where(t => t.TypePath.Contains(Area));
            if (ComType != 0)
                queryable = queryable.Where(t => t.CompanyType == (CompanyEnum)ComType);
            return queryable.ToList();
        }

        /// <summary>
        /// 获取所有商家
        /// </summary>
        /// <param name="Area"></param>
        /// <param name="ComType"></param>
        /// <returns></returns>
        [Obsolete]
        public object GetAllMer(string Area, int ComType)
        {
            IQueryable<RepastInfo> queryable = Kily.Set<RepastInfo>()
                 .Where(t => t.AuditType == AuditEnum.AuditSuccess)
                 .OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(Area))
                queryable = queryable.Where(t => t.TypePath.Contains(Area));
            if (ComType != 0)
            {
                if (ComType <= 20)
                    queryable = queryable.Where(t => t.DiningType == (MerchantEnum)ComType);
                else
                    queryable = queryable.Where(t => t.DiningType >= (MerchantEnum)ComType);
            }
            return queryable.ToList();
        }

        /// <summary>
        /// 所有视频
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Obsolete]
        public object GetAllVideo(Guid Id, int Type)
        {
            if (Type == 10)
                return Kily.Set<EnterpriseVedio>().Where(x => x.CompanyId == Id && x.IsIndex == true)
                     .OrderByDescending(x => x.CreateTime).Take(4).ToList();
            if (Type != 10)
                return Kily.Set<RepastVideo>().Where(x => x.InfoId == Id && x.IsIndex == true)
                     .OrderByDescending(x => x.CreateTime).Take(4).ToList();
            return null;
        }

        #endregion 过时的接口

        #endregion 企业监管

        #region 部门信息

        /// <summary>
        /// 机构分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtInstitution> GetInsPage(PageParamList<RequestGovtInstitution> pageParam)
        {
            IQueryable<GovtInstitution> queryable = Kily.Set<GovtInstitution>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.InstitutionName))
                queryable = queryable.Where(t => t.InstitutionName.Contains(pageParam.QueryParam.InstitutionName));
            if (GovtInfo().AccountType == GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
            if (GovtInfo().AccountType == GovtAccountEnum.Area)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
            var data = queryable.Select(t => new ResponseGovtInstitution()
            {
                Id = t.Id,
                GovtId = t.GovtId,
                InstitutionName = t.InstitutionName,
                ChargeUser = t.ChargeUser,
                ManageAreaName = t.ManageAreaName
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 机构列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseGovtInstitution> GetInsList()
        {
            IQueryable<GovtInstitution> queryable = Kily.Set<GovtInstitution>().AsNoTracking();
            if (GovtInfo().AccountType == GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
            if (GovtInfo().AccountType == GovtAccountEnum.Area)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
            var data = queryable.Select(t => new ResponseGovtInstitution()
            {
                Id = t.Id,
                InstitutionName = t.InstitutionName
            }).AsNoTracking().ToList();
            return data;
        }

        /// <summary>
        /// 获取机构详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseGovtInstitution GetInsDetail(Guid Id)
        {
            var data = Kily.Set<GovtInstitution>().Where(t => t.Id == Id).Select(t => new ResponseGovtInstitution()
            {
                Id = t.Id,
                InstitutionName = t.InstitutionName,
                ChargeUser = t.ChargeUser,
                GovtId = t.GovtId,
                ManageArea = t.ManageArea,
                Remark = t.Remark,
                TypePath = t.TypePath,
                ManageAreaName = t.ManageAreaName
            }).FirstOrDefault();
            return data;
        }

        /// <summary>
        /// 删除机构
        /// </summary>
        /// <returns></returns>
        public string RemoveIns(Guid Id)
        {
            return Remove<GovtInstitution>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 编辑机构
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditIns(RequestGovtInstitution Param)
        {
            GovtInstitution govt = Param.MapToEntity<GovtInstitution>();
            govt.TypePath = GovtInfo().TypePath;
            if (Param.Id == Guid.Empty)
                return Insert(govt) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return Update(govt, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        /// <summary>
        /// 政府用户分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtInfo> GetGovtInfoPage(PageParamList<RequestGovtInfo> pageParam)
        {
            IQueryable<GovtInfo> queryable = Kily.Set<GovtInfo>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            var LoginInfo = GovtInfo();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.DepartName))
                queryable = queryable.Where(t => t.DepartName.Contains(pageParam.QueryParam.DepartName));
            if (LoginInfo.AccountType == GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(LoginInfo.City));
            if (LoginInfo.AccountType == GovtAccountEnum.Area)
                queryable = queryable.Where(t => t.TypePath.Contains(LoginInfo.Area));
            if (LoginInfo.AccountType == GovtAccountEnum.Town)
                queryable = queryable.Where(t => t.Id == LoginInfo.Id);
            var data = queryable.Select(t => new ResponseGovtInfo()
            {
                Id = t.Id,
                Account = t.Account,
                DepartName = t.DepartName,
                TrueName = t.TrueName,
                AccountType = t.AccountType,
                Phone = t.Phone,
                Email = t.Email,
                WorkNum = t.WorkNum
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 删除政府用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveGovtInfo(Guid Id)
        {
            return Remove<GovtInfo>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 政府用户详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseGovtInfo GetGovtInfoDetail(Guid Id)
        {
            var data = Kily.Set<GovtInfo>().Where(t => t.Id == Id).Select(t => new ResponseGovtInfo()
            {
                Id = t.Id,
                IsEdu = t.IsEdu,
                Account = t.Account,
                AccountType = t.AccountType,
                DepartId = t.DepartId,
                DepartName = t.DepartName,
                Email = t.Email,
                GovtId = t.GovtId,
                PassWord = t.PassWord,
                Phone = t.Phone,
                TrueName = t.TrueName,
                TypePath = t.TypePath
            }).FirstOrDefault();
            return data;
        }

        /// <summary>
        /// 编辑政府用户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditUser(RequestGovtInfo Param)
        {
            GovtInfo Info = Param.MapToEntity<GovtInfo>();
            if (NormalUtil.CheckStringChineseUn(Info.Account))
                return "账号不能包含中文和特殊字符";
            if (GovtInfo() != null)
            {
                Info.TypePath = GovtInfo().TypePath;
                if (Param.Id != Guid.Empty)
                    return Update(Info, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
                else
                {
                    var Infos = Kily.Set<GovtInfo>().Where(t => t.Account.Equals(Param.Account)).AsNoTracking().FirstOrDefault();
                    if (Infos != null) return "该账号已经存在!";
                    if (GovtInfo().AccountType == GovtAccountEnum.City)
                        Info.AccountType = GovtAccountEnum.Area;
                    else if (GovtInfo().AccountType == GovtAccountEnum.Area)
                        Info.AccountType = GovtAccountEnum.Town;
                    return Insert(Info) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
                }
            }
            else
            {
                if (Param.Id != Guid.Empty)
                    return Update(Info, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
                else
                {
                    var Infos = Kily.Set<GovtInfo>().Where(t => t.Account.Equals(Param.Account)).AsNoTracking().FirstOrDefault();
                    if (Infos != null) return "该账号已经存在!";
                    return Insert(Info) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
                }
            }
        }

        /// <summary>
        /// 获取所有政府用户
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public List<ResponseGovtInfo> GetAllGovt()
        {
            return Kily.Set<GovtInfo>().Where(t => t.IsDelete == false).ToList().MapToList<GovtInfo, ResponseGovtInfo>();
        }

        #endregion 部门信息

        #region 管辖区域

        /// <summary>
        /// 获取所属区域的市名称
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public String GetCityName(Guid Id)
        {
            return Kily.Set<SystemCity>().Where(t => t.Id == Id).Select(t => t.Name).FirstOrDefault();
        }

        /// <summary>
        /// 获取分配的区域
        /// </summary>
        /// <returns></returns>
        public IList<ResponseGovtDistribut> GetDistributArea()
        {
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>().Where(t => t.AuditType == AuditEnum.AuditSuccess && t.IsDelete == false);
            IQueryable<RepastInfo> queryables = Kily.Set<RepastInfo>().Where(t => t.AuditType == AuditEnum.AuditSuccess && t.IsDelete == false);
            if (GovtInfo().AccountType <= GovtAccountEnum.Area)
            {
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
                queryables = queryables.Where(t => t.TypePath.Contains(GovtInfo().City));
            }
            IList<string> Areas = GetDepartArea();
            if (Areas != null)
            {
                if (Areas.Count > 1)
                {
                    Expression<Func<EnterpriseInfo, bool>> exp_1 = null;
                    Expression<Func<RepastInfo, bool>> exp_2 = null;
                    for (int i = 0; i < Areas.Count; i++)
                    {
                        if (i == 0)
                        {
                            exp_1 = ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                            exp_2 = ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                        }
                        else
                        {
                            exp_1 = exp_1.Or(ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                            exp_2 = exp_2.Or(ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                        }
                    }
                    queryable = queryable.Where(exp_1);
                    queryables = queryables.Where(exp_2);
                }
                else
                {
                    queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                    queryables = queryables.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                }
            }
            else
            {
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
                queryables = queryables.Where(t => t.TypePath.Contains(GovtInfo().Area));
            }
            List<ResponseGovtDistribut> data = new List<ResponseGovtDistribut>();
            if (!GovtInfo().IsEdu.Value)
            {
                data = queryable.Select(t => new ResponseGovtDistribut()
                {
                    Id = t.Id.ToString(),
                    Name = t.CompanyName,
                    LngAndLat = t.LngAndLat,
                    Address = t.CompanyAddress,
                    CompanyImg = t.ComImage,
                    CompanyCode = t.CommunityCode,
                    CompanyType = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType)
                }).ToList();
                var temp = queryables.Select(t => new ResponseGovtDistribut()
                {
                    Id = t.Id.ToString(),
                    Name = t.MerchantName,
                    LngAndLat = t.LngAndLat,
                    Address = t.Address,
                    CompanyImg = t.MerchantImage,
                    CompanyCode = t.CommunityCode,
                    CompanyType = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType)
                }).ToList();
                data.AddRange(temp);
            }
            else
            {
                data = queryables.Where(t => t.DiningType == MerchantEnum.UnitCanteen).Select(t => new ResponseGovtDistribut()
                {
                    Id = t.Id.ToString(),
                    Name = t.MerchantName,
                    LngAndLat = t.LngAndLat,
                    Address = t.Address,
                    CompanyCode = t.CommunityCode,
                    CompanyImg = t.MerchantImage,
                    CompanyType = t.AllowUnit
                }).ToList();
            }
            return data;
        }

        /// <summary>
        /// 当前登录账号是市级账号则查询该市下所有区县
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IList<ResponseArea> GetArea(Guid Id)
        {
            int CityCode = Kily.Set<SystemCity>().Where(t => t.Id == Id).Select(t => t.Code).FirstOrDefault();
            var data = Kily.Set<SystemArea>().Where(t => t.CityCode == CityCode).Select(t => new ResponseArea()
            {
                Id = t.Id,
                AreaName = t.Name
            }).AsNoTracking().ToList();
            return data;
        }

        /// <summary>
        /// 当前登录账号是区县级账号则查询该市下所有乡镇
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IList<ResponseTown> GetTown(Guid Id)
        {
            IQueryable<SystemArea> queryable = Kily.Set<SystemArea>();
            IQueryable<SystemTown> queryables = Kily.Set<SystemTown>();
            IList<string> Areas = GetDepartArea();
            //当录入账号是市级账号时先查询机构表中是否纯在该账号已经分配的区域，然后在查询该区域下所有的乡镇
            if (Areas != null)
            {
                if (Areas.Count > 1)
                {
                    foreach (var item in Areas)
                    {
                        queryable = queryable.Where(t => t.Id.ToString() == item);
                    }
                    Expression<Func<SystemArea, bool>> exp_1 = null;
                    for (int i = 0; i < Areas.Count; i++)
                    {
                        if (i == 0)
                        {
                            exp_1 = ExpressionExtension.GetExpression<SystemArea>("Id", Areas[i], ExpressionEnum.Equals);
                        }
                        else
                        {
                            exp_1 = exp_1.Or(ExpressionExtension.GetExpression<SystemArea>("Id", Areas[i], ExpressionEnum.Equals));
                        }
                    }
                    queryable = queryable.Where(exp_1);
                }
                else
                    queryable = queryable.Where(t => t.Id.ToString() == Areas.FirstOrDefault());

                IList<int> AreaCodes = queryable.Select(t => t.Code).ToList();
                var data = queryables.Where(t => AreaCodes.Contains(t.AreaCode)).Select(t => new ResponseTown()
                {
                    Id = t.Id,
                    TownName = t.Name
                }).AsNoTracking().ToList();
                return data;
            }
            //当录入账号是区县账号时直接查询自生账号下所有的乡镇
            else
            {
                int AreaCode = queryable.Where(t => t.Id == Id).Select(t => t.Code).FirstOrDefault();
                var data = queryables.Where(t => t.AreaCode == AreaCode).Select(t => new ResponseTown()
                {
                    Id = t.Id,
                    TownName = t.Name
                }).AsNoTracking().ToList();
                return data;
            }
        }

        #endregion 管辖区域

        #region 产品监管

        /// <summary>
        /// 加工产品分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseGoods> GetWorkPage(PageParamList<RequestEnterpriseGoods> pageParam)
        {
            IQueryable<EnterpriseGoods> goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false).Where(t => t.AuditType == AuditEnum.AuditSuccess);
            IQueryable<EnterpriseProductSeries> queryables = Kily.Set<EnterpriseProductSeries>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.ProductType))
                goods = goods.Where(t => pageParam.QueryParam.ProductType.Contains(t.ProductType));
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>().Where(t => t.AuditType == AuditEnum.AuditSuccess);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<EnterpriseInfo, bool>> exp_1 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                            }
                        }
                        queryable = queryable.Where(exp_1);
                    }
                    else
                        queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                }
                else
                    queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
            }
            if (!string.IsNullOrEmpty(pageParam.QueryParam.ProductName))
                goods = goods.Where(t => t.ProductName.Contains(pageParam.QueryParam.ProductName));
            var data = goods.Join(queryable, t => t.CompanyId, x => x.Id, (t, x) => new ResponseEnterpriseGoods()
            {
                Id = t.Id,
                ProductName = t.ProductName,
                CompanyId = t.CompanyId,
                CompanyName = x.CompanyName,
                ProductType = t.ProductType,
                ExpiredDate = t.ExpiredDate + "天",
                Image = t.Image,
                Price = t.Price,
                SellWebNet = t.SellWebNet,
                LineCode = t.LineCode,
                Remark = t.Remark,
                Spec = t.Spec,
                Unit = x.ProductionAddress,
                ProductSeriesName = "-",
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 加工产品详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Object GetWorkDetail(Guid Id)
        {
            IQueryable<EnterpriseGoodsStockAttach> GoodsStockAttach = Kily.Set<EnterpriseGoodsStockAttach>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<EnterpriseGoodsStock> GoodStock = Kily.Set<EnterpriseGoodsStock>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<EnterpriseGoods> Goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<EnterpriseProductionBatch> ProductionBatch = Kily.Set<EnterpriseProductionBatch>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseStockType> StockType = Kily.Set<EnterpriseStockType>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseMaterialStockAttach> MaterialStockAttach = Kily.Set<EnterpriseMaterialStockAttach>().Where(t => t.IsDelete == false).AsNoTracking();
            IQueryable<EnterpriseMaterialStock> MaterialStock = Kily.Set<EnterpriseMaterialStock>().Where(t => t.IsDelete == false).AsNoTracking();
            IQueryable<EnterpriseMaterial> Material = Kily.Set<EnterpriseMaterial>().Where(t => t.IsDelete == false).AsNoTracking();
            IQueryable<EnterpriseProductSeries> ProductSeries = Kily.Set<EnterpriseProductSeries>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseCheckMaterial> CheckMaterial = Kily.Set<EnterpriseCheckMaterial>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseCheckGoods> CheckGoods = Kily.Set<EnterpriseCheckGoods>().Where(t => t.IsDelete == false);
            //产品的查询
            var GoodsData = GoodsStockAttach.Join(GoodStock, a => a.StockId, b => b.Id, (a, b) => new { a, b })
                 .GroupJoin(ProductionBatch, c => c.b.BatchId, d => d.Id, (c, d) => new { c, d })
                 .Join(Goods, e => e.c.b.GoodsId, f => f.Id, (e, f) => new { e, f })
                 .GroupJoin(CheckGoods, g => g.e.c.b.CheckGoodsId, h => h.Id, (g, h) => new { g, h })
                 .Join(ProductSeries, i => i.g.f.ProductSeriesId, j => j.Id, (i, j) => new { i, j })
                 .Join(StockType, k => k.i.g.e.c.b.StockTypeId, l => l.Id, (k, l) => new { k, l }).Where(t => t.k.i.g.f.Id == Id)
                 .AsNoTracking();
            //原材料的查询
            var MaterialsData = MaterialStockAttach.Join(MaterialStock, a => a.MaterialStockId, b => b.Id, (a, b) => new { a, b })
                 .Join(Material, c => c.b.BatchNo, d => d.BatchNo, (c, d) => new { c, d })
                 .GroupJoin(CheckMaterial, e => e.c.b.CheckMaterialId, f => f.Id, (e, f) => new { e, f }).AsNoTracking();
            var GoodData = GoodsData.Select(t => new
            {
                GoodsName = t.k.i.g.f.ProductName,
                ExpiredDay = t.k.i.g.f.ExpiredDate,
                t.k.i.g.f.Spec,
                t.k.i.g.f.ProductType,
                t.k.j.Standard,
                DeviceName = t.k.i.g.e.d.FirstOrDefault() == null ? "" : t.k.i.g.e.d.FirstOrDefault().DeviceName,
                OutNo = t.k.i.g.e.c.a.GoodsBatchNo,
                t.k.i.g.e.c.a.OutStockTime,
                t.k.i.g.e.c.a.OutStockUser,
                t.k.i.g.e.c.b.Manager,
                t.k.i.g.e.c.b.ProductTime,
                InNo = t.k.i.g.e.c.b.GoodsBatchNo,
                MaterialId = t.k.i.g.e.d.FirstOrDefault() == null ? "" : t.k.i.g.e.d.FirstOrDefault().MaterialId,
                t.k.i.h.FirstOrDefault().CheckUint,
                t.k.i.h.FirstOrDefault().CheckUser,
                t.k.i.h.FirstOrDefault().CheckResult,
                t.k.i.h.FirstOrDefault().CheckReport,
                t.l.StockName,
                t.l.SaveType,
                t.l.SaveH2,
                t.l.SaveTemp
            }).FirstOrDefault();
            dynamic MaterialData = null;
            if (GoodData != null)
            {
                MaterialData = MaterialsData.Where(t => GoodData.MaterialId.Contains(t.e.d.Id.ToString())).Select(t => new
                {
                    t.f.FirstOrDefault().CheckUint,
                    t.f.FirstOrDefault().CheckUser,
                    t.f.FirstOrDefault().CheckResult,
                    t.f.FirstOrDefault().CheckReport,
                    t.e.d.Address,
                    t.e.d.ExpiredDay,
                    t.e.d.MaterName,
                    t.e.d.MaterType,
                    t.e.d.Supplier,
                    t.e.c.b.ProductTime
                }).FirstOrDefault();
            }

            return new
            {
                StockName = GoodData?.StockName,
                SaveH2 = GoodData?.SaveH2,
                SaveType = GoodData?.SaveType,
                SaveTemp = GoodData?.SaveTemp,
                GoodsName = GoodData?.GoodsName,
                ExpiredDay = GoodData?.ExpiredDay,
                Spec = GoodData?.Spec,
                ProductType = GoodData?.ProductType,
                Standard = GoodData?.Standard,
                DeviceName = GoodData?.DeviceName,
                OutNo = GoodData?.OutNo,
                OutStockTime = GoodData?.OutStockTime,
                OutStockUser = GoodData?.OutStockUser,
                Manager = GoodData?.Manager,
                GoodProductTime = GoodData?.ProductTime,
                InNo = GoodData?.InNo,
                GoodCheckUint = GoodData?.CheckUint,
                GoodCheckUser = GoodData?.CheckUser,
                GoodCheckResult = GoodData?.CheckResult,
                GoodCheckReport = GoodData?.CheckReport,
                MaterialCheckUint = MaterialData?.CheckUint,
                MaterialCheckUser = MaterialData?.CheckUser,
                MaterialCheckResult = MaterialData?.CheckResult,
                MaterialCheckReport = MaterialData?.CheckReport,
                MaterialAddress = MaterialData?.Address,
                MaterialExpiredDay = MaterialData?.ExpiredDay,
                MaterName = MaterialData?.MaterName,
                MaterType = MaterialData?.MaterType,
                Supplier = MaterialData?.Supplier,
                MaterialProductTime = MaterialData?.ProductTime
            };
        }

        /// <summary>
        /// 食用品分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseGoods> GetEdiblePage(PageParamList<RequestEnterpriseGoods> pageParam)
        {
            IQueryable<EnterpriseGoods> goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false).Where(t => t.AuditType == AuditEnum.AuditSuccess)
                .Where(t => pageParam.QueryParam.ProductType.Contains(t.ProductType));
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>().Where(t => t.AuditType == AuditEnum.AuditSuccess);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<EnterpriseInfo, bool>> exp_1 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                            }
                        }
                        queryable = queryable.Where(exp_1);
                    }
                    else
                        queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                }
                else
                    queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
            }
            if (!string.IsNullOrEmpty(pageParam.QueryParam.ProductName))
                goods = goods.Where(t => t.ProductName.Contains(pageParam.QueryParam.ProductName));
            var data = goods.Join(queryable, t => t.CompanyId, x => x.Id, (t, x) => new ResponseEnterpriseGoods()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                ProductName = t.ProductName,
                CompanyName = x.CompanyName,
                ProductType = t.ProductType,
                ExpiredDate = t.ExpiredDate,
                ProductImg = t.Image,
                Spec = t.Spec,
                Unit = x.ProductionAddress,
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 食用品详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Object GetEdibleDetail(Guid Id)
        {
            IQueryable<EnterpriseGoodsStockAttach> GoodsStockAttach = Kily.Set<EnterpriseGoodsStockAttach>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<EnterpriseGoodsStock> GoodStock = Kily.Set<EnterpriseGoodsStock>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<EnterpriseGoods> Goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<EnterpriseInfo> Companys = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<EnterpriseStockType> StockType = Kily.Set<EnterpriseStockType>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseCheckGoods> CheckGoods = Kily.Set<EnterpriseCheckGoods>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseNote> Note = Kily.Set<EnterpriseNote>().Where(t => t.IsDelete == false);
            //产品的查询
            var GoodsData = GoodsStockAttach.Join(GoodStock, a => a.StockId, b => b.Id, (a, b) => new { a, b })
                .Join(Goods, c => c.b.GoodsId, d => d.Id, (c, d) => new { c, d })
                .Join(StockType, e => e.c.b.StockTypeId, f => f.Id, (e, f) => new { e, f })
                .GroupJoin(CheckGoods, g => g.e.c.b.CheckGoodsId, h => h.Id, (g, h) => new { g, h })
                .Where(t => t.g.e.d.Id == Id).AsNoTracking();
            //库存列表
            var StockGoods = GoodsData.Where(t => t.g.e.c.b.GoodsId == Id && t.g.e.c.b.ProductTime >= DateTime.Now.AddDays(-60)).Select(t => new
            {
                t.g.e.c.b.GoodsBatchNo,
                t.g.e.d.ProductName,
                t.g.e.d.Spec,
                StockInTime = t.g.e.c.b.ProductTime,
                t.g.e.d.ExpiredDate,
                t.g.e.c.b.InStockNum,
                TotalCount = GoodsStockAttach.Where(i => i.StockId == t.g.e.c.b.Id).Select(j => j.OutStockNum).Sum() + t.g.e.c.b.InStockNum,
                t.g.e.c.b.Manager
            }).ToList();
            return GoodsData.Select(t => new
            {
                t.h.FirstOrDefault().CheckUint,
                t.h.FirstOrDefault().CheckUser,
                t.h.FirstOrDefault().CheckResult,
                t.h.FirstOrDefault().CheckReport,
                t.g.e.c.b.Explanation,
                t.g.e.c.b.Remark,
                t.g.f.StockName,
                t.g.f.SaveType,
                t.g.f.SaveH2,
                t.g.f.SaveTemp,
                t.g.e.d.ExpiredDate,
                t.g.e.d.Image,
                Content = t.g.e.d.Remark,
                t.g.e.d.ProductName,
                t.g.e.d.ProductType,
                t.g.e.d.Spec,
                t.g.e.c.b.ProductTime,
                t.g.e.c.b.Manager,
                t.g.e.c.a.OutStockTime,
                t.g.e.c.a.OutStockUser,
                Note = Note.Where(m => m.Id == t.g.e.c.b.GrowNoteId),
                StockGoods = StockGoods
            }).FirstOrDefault();
        }

        /// <summary>
        /// 企业产品列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public object GetGoodsPage(Guid CompanyId)
        {
            IQueryable<EnterpriseGoods> queryable = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false).Where(t => t.AuditType == AuditEnum.AuditSuccess);
            IQueryable<EnterpriseProductSeries> queryables = Kily.Set<EnterpriseProductSeries>().Where(t => t.IsDelete == false);
            queryable = queryable.Where(t => t.CompanyId == CompanyId);
            var data = queryable.OrderByDescending(t => t.CreateTime).GroupJoin(queryables, t => t.ProductSeriesId, x => x.Id, (t, x) => new ResponseEnterpriseGoods()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                Spec = t.Spec,
                ProductSeriesName = x.FirstOrDefault().SeriesName + "-" + x.FirstOrDefault().Standard,
                ExpiredDate = t.ExpiredDate,
                ProductName = t.ProductName,
                ProductType = t.ProductType,
                Unit = t.Unit,
                Image = t.Image,
                Remark = t.Remark
            }).AsNoTracking().ToList();
            return data;
        }

        #endregion 产品监管

        #region 餐饮监管

        /// <summary>
        /// 群宴报备分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseCookBanquet> GetBanquetPage(PageParamList<RequestCookBanquet> pageParam)
        {
            IQueryable<CookBanquet> queryable = Kily.Set<CookBanquet>().OrderByDescending(t => t.CreateTime);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<CookBanquet, bool>> exp_1 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<CookBanquet>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<CookBanquet>("TypePath", Areas[i], ExpressionEnum.Like));
                            }
                        }
                        queryable = queryable.Where(exp_1);
                    }
                    else
                        queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                }
                else
                    queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
            }
            if (!string.IsNullOrEmpty(pageParam.QueryParam.HoldType))
                queryable = queryable.Where(t => t.HoldType.Contains(pageParam.QueryParam.HoldType));
            var data = queryable.Select(t => new ResponseCookBanquet()
            {
                Id = t.Id,
                CookId = t.CookId,
                HoldName = t.HoldName,
                Phone = t.Phone,
                HoldTheme = t.HoldTheme,
                HoldFoo = t.HoldFoo,
                HoldTotal = t.HoldTotal,
                DeskNum = t.DeskNum,
                Address = t.Address,
                HoldDay = t.HoldDay,
                HoldTime = t.HoldTime,
                HoldType = t.HoldType,
                Stauts = t.Stauts,
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 现场检测
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SiteImg(Guid Id, string Param)
        {
            CookBanquet cook = Kily.Set<CookBanquet>().Where(t => t.Id == Id).FirstOrDefault();
            cook.ResultImg = Param;
            return UpdateField(cook, "ResultImg") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        /// <summary>
        /// 群宴报备详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseCookBanquet GetBanquetDetail(Guid Id)
        {
            IQueryable<CookBanquet> queryable = Kily.Set<CookBanquet>().Where(t => t.Id == Id);
            var data = queryable.Select(t => new ResponseCookBanquet()
            {
                HoldName = t.HoldName,
                Phone = t.Phone,
                Address = t.Address,
                Facility = t.Facility,
                Poisonous = t.Poisonous,
                Processing = t.Processing,
                Ingredients = t.Ingredients,
                ResultImg = t.ResultImg,
                HoldFoo = t.HoldFoo,
                HoldTheme = t.HoldTheme,
                HoldTotal = t.HoldTotal,
                DeskNum = t.DeskNum,
                HoldTime = t.HoldTime,
                Helpers = Kily.Set<CookHelper>().Where(x => x.CookId == t.CookId && t.Helper.Contains(x.HelperName))
                 .Select(x => new ResponseCookHelper()
                 {
                     HelperName = x.HelperName,
                     Phone = x.Phone,
                     ExpiredDate = x.ExpiredDate,
                     HealthCard = x.HealthCard
                 }).AsNoTracking().ToList(),
                Foods = Kily.Set<CookFood>().Where(x => t.Ingredients.Contains(x.Id.ToString()))
                .Select(x => new ResponseCookFood()
                {
                    Supplier = x.Supplier,
                    Phone = x.Phone,
                    BuyTime = x.BuyTime,
                    BuyUser = x.BuyUser
                }).AsNoTracking().ToList()
            }).AsNoTracking().FirstOrDefault();
            return data;
        }

        /// <summary>
        /// 审核群宴
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string EditCookBanquet(Guid Id, string Param)
        {
            CookBanquet cook = Kily.Set<CookBanquet>().Where(t => t.Id == Id).FirstOrDefault();
            cook.Stauts = Param;
            return UpdateField(cook, "Stauts") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        #endregion 餐饮监管

        #region 风险预警

        /// <summary>
        /// 预警信息分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtRisk> GetWaringRiskPage(PageParamList<RequestGovtRisk> pageParam)
        {
            IQueryable<GovtRisk> queryable = Kily.Set<GovtRisk>().OrderByDescending(t => t.CreateTime);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<GovtRisk, bool>> exp_1 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<GovtRisk>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<GovtRisk>("TypePath", Areas[i], ExpressionEnum.Like));
                            }
                        }
                        queryable = queryable.Where(exp_1);
                    }
                    else
                        queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                }
                else
                    queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
            }
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

        /// <summary>
        /// 编辑预警信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditWaringRisk(RequestGovtRisk Param)
        {
            GovtRisk risk = Param.MapToEntity<GovtRisk>();
            return Insert(risk) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        /// <summary>
        /// 发布广播
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string ReportWaringRisk(Guid Id)
        {
            GovtRisk risk = Kily.Set<GovtRisk>().Where(t => t.Id == Id).FirstOrDefault();
            risk.ReportPlay = true;
            SystemMessage message = new SystemMessage
            {
                TypePath = GovtInfo().TypePath,
                ReleaseTime = DateTime.Now,
                MsgName = risk.EventName,
                MsgContent = risk.Remark,
                TrageType = risk.TradeType,
                Category = "预警"
            };
            Insert(message);
            return UpdateField(risk, "ReportPlay") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveWaringRisk(Guid Id)
        {
            return Remove<GovtRisk>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 预警详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseGovtRisk GetWaringDetail(Guid Id)
        {
            var data = Kily.Set<GovtRisk>().Where(t => t.Id == Id).Select(t => new ResponseGovtRisk()
            {
                Id = t.Id,
                GovtId = t.GovtId,
                WaringLv = t.WaringLv,
                TypePath = t.TypePath,
                EventName = t.EventName,
                TradeType = t.TradeType,
                Remark = t.Remark,
                ReleaseTime = t.ReleaseTime
            }).FirstOrDefault();
            return data;
        }

        /// <summary>
        /// 获取事件数
        /// </summary>
        /// <returns></returns>
        public List<int> GetRiskCount()
        {
            IQueryable<GovtRisk> queryable = Kily.Set<GovtRisk>().Where(t => t.ReportPlay == true);
            IQueryable<GovtComplain> complains = Kily.Set<GovtComplain>().Where(t => t.Status == "待处理");
            if (GovtInfo() != null)
            {
                if (GovtInfo().AccountType <= GovtAccountEnum.City)
                {
                    queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
                    complains = complains.Where(t => t.TypePath.Contains(GovtInfo().City));
                }
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<GovtRisk, bool>> exp_1 = null;
                        Expression<Func<GovtComplain, bool>> exp_2 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<GovtRisk>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_2 = ExpressionExtension.GetExpression<GovtComplain>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<GovtRisk>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_2 = exp_2.Or(ExpressionExtension.GetExpression<GovtComplain>("TypePath", Areas[i], ExpressionEnum.Like));
                            }
                        }
                        queryable = queryable.Where(exp_1);
                        complains = complains.Where(exp_2);
                    }
                    else
                    {
                        queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        complains = complains.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                    }
                }
                else
                {
                    queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    complains = complains.Where(t => t.TypePath.Contains(GovtInfo().Area));
                }
            }
            return new List<int> { queryable.Count(), complains.Count() }; ;
        }

        /// <summary>
        /// 获取市名称
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GetCity(Guid Id)
        {
            return Kily.Set<SystemCity>().Where(t => t.Id == Id).Select(t => t.Name).FirstOrDefault();
        }

        /// <summary>
        /// 企业证件到期分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public Object GetCardPage(PageParamList<RequestGovtRiskCompany> pageParam)
        {
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<RepastInfo> queryables = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<RepastInfoUser> users = Kily.Set<RepastInfoUser>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<GovtComplain> complain = Kily.Set<GovtComplain>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CompanyName))
            {
                queryable = queryable.Where(t => t.CompanyName.Contains(pageParam.QueryParam.CompanyName));
                queryables = queryables.Where(t => t.MerchantName.Contains(pageParam.QueryParam.CompanyName));
                users = users.Where(t => t.MerchantName.Contains(pageParam.QueryParam.CompanyName));
            }
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
            {
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
                queryables = queryables.Where(t => t.TypePath.Contains(GovtInfo().City));
                users = users.Where(t => t.TypePath.Contains(GovtInfo().City));
                complain = complain.Where(t => t.TypePath.Contains(GovtInfo().City));
            }
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<EnterpriseInfo, bool>> exp_1 = null;
                        Expression<Func<RepastInfo, bool>> exp_2 = null;
                        Expression<Func<RepastInfoUser, bool>> exp_3 = null;
                        Expression<Func<GovtComplain, bool>> exp_4 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_2 = ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_3 = ExpressionExtension.GetExpression<RepastInfoUser>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_4 = ExpressionExtension.GetExpression<GovtComplain>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_2 = exp_2.Or(ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_3 = exp_3.Or(ExpressionExtension.GetExpression<RepastInfoUser>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_4 = exp_4.Or(ExpressionExtension.GetExpression<GovtComplain>("TypePath", Areas[i], ExpressionEnum.Like));
                            }
                        }
                        queryable = queryable.Where(exp_1);
                        queryables = queryables.Where(exp_2);
                        users = users.Where(exp_3);
                        complain = complain.Where(exp_4);
                    }
                    else
                    {
                        queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        queryables = queryables.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        users = users.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        complain = complain.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                    }
                }
                else
                {
                    queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    queryables = queryables.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    users = users.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    complain = complain.Where(t => t.TypePath.Contains(GovtInfo().Area));
                }
            }
            if (!GovtInfo().IsEdu.Value)
            {
                var Enterprise = queryable.Select(t => new
                {
                    t.Id,
                    Name = t.CompanyName,
                    CardType = "营业执照",
                    PersonName = "",
                    CompanyType = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType),
                    CardImg = t.Certification,
                    Remark = t.CompanyName + "营业执照于" + (t.CardExpiredDate.HasValue ? t.CardExpiredDate.Value.ToString("yyyy年MM月dd日") : "-") + "到期.",
                    t.CardExpiredDate
                }).ToList();
                var Repast = queryables.Select(t => new
                {
                    t.Id,
                    Name = t.MerchantName,
                    CardType = "营业执照",
                    PersonName = "",
                    CompanyType = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType),
                    CardImg = t.Certification,
                    Remark = t.MerchantName + "营业执照于" + (t.CardExpiredDate.HasValue ? t.CardExpiredDate.Value.ToString("yyyy年MM月dd日") : "-") + "到期.",
                    t.CardExpiredDate
                }).ToList();
                var MerUser = users.Select(t => new
                {
                    t.Id,
                    Name = t.MerchantName,
                    CardType = "健康证",
                    PersonName = t.TrueName,
                    CompanyType = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType),
                    CardImg = t.HealthCard,
                    Remark = t.MerchantName + "人员：" + t.TrueName + "健康证于" + t.ExpiredTime.Value.ToString("yyyy年MM月dd日") + "到期.",
                    CardExpiredDate = t.ExpiredTime
                }).ToList();
                var complains = complain.Where(t => t.Status != "已处理").Select(t => new
                {
                    t.Id,
                    Name = t.CompanyName,
                    CardType = "投诉",
                    PersonName = t.ComplainUser,
                    CompanyType = t.CompanyType,
                    CardImg = t.ComplainContent,
                    Remark = t.ComplainContent,
                    CardExpiredDate = t.ComplainTime
                }).ToList();
                Enterprise.AddRange(Repast);
                Enterprise.AddRange(MerUser);
                Enterprise.AddRange(complains);
                Enterprise.RemoveAll(t => !t.CardExpiredDate.HasValue);
                return Enterprise.Where(t => t.CardExpiredDate.Value <= DateTime.Now.AddDays(20)).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            }
            else
            {
                var Repast = queryables.Select(t => new
                {
                    t.Id,
                    Name = t.MerchantName,
                    CardType = "营业执照",
                    PersonName = "",
                    CompanyType = t.AllowUnit,
                    CardImg = t.Certification,
                    t.CardExpiredDate,
                    Remark = t.MerchantName + "营业执照于" + (t.CardExpiredDate.HasValue ? t.CardExpiredDate.Value.ToString("yyyy年MM月dd日") : "-") + "到期."
                }).ToList();
                var MerUser = users.Where(t => t.DiningType == MerchantEnum.UnitCanteen).Select(t => new
                {
                    t.Id,
                    Name = t.MerchantName,
                    CardType = "健康证",
                    PersonName = t.TrueName,
                    CompanyType = Repast.Where(x => x.Id == t.InfoId).Select(x => x.CompanyType).FirstOrDefault(),
                    CardImg = t.HealthCard,
                    CardExpiredDate = t.ExpiredTime,
                    Remark = t.MerchantName + "人员：" + t.TrueName + "健康证于" + t.ExpiredTime.Value.ToString("yyyy年MM月dd日") + "到期."
                }).ToList();
                var complains = complain.Where(t => t.CompanyType == "单位食堂" && t.Status != "已处理").Select(t => new
                {
                    t.Id,
                    Name = t.CompanyName,
                    CardType = "投诉",
                    PersonName = t.ComplainUser,
                    CompanyType = t.CompanyType,
                    CardImg = t.ComplainContent,
                    CardExpiredDate = t.ComplainTime,
                    Remark = t.ComplainContent
                }).ToList();
                Repast.AddRange(MerUser);
                Repast.AddRange(complains);
                return Repast.Where(t => t.CardExpiredDate.Value <= DateTime.Now.AddDays(20)).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            }
        }

        /// <summary>
        /// 证件到期提醒
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string ReportCardWaring(Guid Id, string Key)
        {
            SystemMessage message = new SystemMessage();
            if (Key != "投诉")
            {
                message = new SystemMessage
                {
                    CompanyId = Id,
                    TypePath = GovtInfo().TypePath,
                    ReleaseTime = DateTime.Now,
                    TrageType = Key,
                    MsgName = "证件到期提醒",
                    MsgContent = "您的证件日期即将到期，请尽快续期",
                    Category = "常规"
                };
            }
            else
            {
                message = new SystemMessage
                {
                    CompanyId = Id,
                    TypePath = GovtInfo().TypePath,
                    ReleaseTime = DateTime.Now,
                    TrageType = Key,
                    MsgName = "投诉建议",
                    MsgContent = "您有新的投诉信息请尽快处理！",
                    Category = "常规"
                };
            }
            return Insert(message) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 预警提示
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public Object GetWarnList(PageParamList<RequestGovtRiskCompany> pageParam)
        {
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<RepastInfo> queryables = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<RepastInfoUser> users = Kily.Set<RepastInfoUser>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<GovtComplain> complain = Kily.Set<GovtComplain>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CompanyName))
            {
                queryable = queryable.Where(t => t.CompanyName.Contains(pageParam.QueryParam.CompanyName));
                queryables = queryables.Where(t => t.MerchantName.Contains(pageParam.QueryParam.CompanyName));
                users = users.Where(t => t.MerchantName.Contains(pageParam.QueryParam.CompanyName));
            }
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
            {
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
                queryables = queryables.Where(t => t.TypePath.Contains(GovtInfo().City));
                users = users.Where(t => t.TypePath.Contains(GovtInfo().City));
                complain = complain.Where(t => t.TypePath.Contains(GovtInfo().City));
            }
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<EnterpriseInfo, bool>> exp_1 = null;
                        Expression<Func<RepastInfo, bool>> exp_2 = null;
                        Expression<Func<RepastInfoUser, bool>> exp_3 = null;
                        Expression<Func<GovtComplain, bool>> exp_4 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_2 = ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_3 = ExpressionExtension.GetExpression<RepastInfoUser>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_4 = ExpressionExtension.GetExpression<GovtComplain>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_2 = exp_2.Or(ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_3 = exp_3.Or(ExpressionExtension.GetExpression<RepastInfoUser>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_4 = exp_4.Or(ExpressionExtension.GetExpression<GovtComplain>("TypePath", Areas[i], ExpressionEnum.Like));
                            }
                        }
                        queryable = queryable.Where(exp_1);
                        queryables = queryables.Where(exp_2);
                        users = users.Where(exp_3);
                        complain = complain.Where(exp_4);
                    }
                    else
                    {
                        queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        queryables = queryables.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        users = users.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        complain = complain.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                    }
                }
                else
                {
                    queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    queryables = queryables.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    users = users.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    complain = complain.Where(t => t.TypePath.Contains(GovtInfo().Area));
                }
            }
            if (!GovtInfo().IsEdu.Value)
            {
                var Enterprise = queryable.Select(t => new
                {
                    t.Id,
                    Name = t.CompanyName,
                    CardType = "营业执照",
                    PersonName = "",
                    CompanyType = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType),
                    CardImg = t.Certification,
                    Title = t.CompanyName + "营业执照到期",
                    Times = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                    Remark = t.CompanyName + "营业执照于" + (t.CardExpiredDate.HasValue ? t.CardExpiredDate.Value.ToString("yyyy年MM月dd日") : "-") + "到期.",
                    t.CardExpiredDate
                }).ToList();
                var Repast = queryables.Select(t => new
                {
                    t.Id,
                    Name = t.MerchantName,
                    CardType = "营业执照",
                    PersonName = "",
                    CompanyType = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType),
                    CardImg = t.Certification,
                    Title = t.MerchantName + "营业执照到期",
                    Times = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                    Remark = t.MerchantName + "营业执照于" + (t.CardExpiredDate.HasValue ? t.CardExpiredDate.Value.ToString("yyyy年MM月dd日") : "-") + "到期.",
                    t.CardExpiredDate
                }).ToList();
                var MerUser = users.Select(t => new
                {
                    t.Id,
                    Name = t.MerchantName,
                    CardType = "健康证",
                    PersonName = t.TrueName,
                    CompanyType = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType),
                    CardImg = t.HealthCard,
                    Title = t.MerchantName + "人员：" + t.TrueName + "健康证到期",
                    Times = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                    Remark = t.MerchantName + "人员：" + t.TrueName + "健康证于" + t.ExpiredTime.Value.ToString("yyyy年MM月dd日") + "到期.",
                    CardExpiredDate = t.ExpiredTime
                }).ToList();
                var complains = complain.Where(t => t.Status != "已处理").Select(t => new
                {
                    t.Id,
                    Name = t.CompanyName,
                    CardType = "投诉",
                    PersonName = t.ComplainUser,
                    CompanyType = t.CompanyType,
                    CardImg = "",
                    Title = t.ComplainUser + "投诉[" + t.CompanyName + "]",
                    Times = t.ComplainTime.Value.ToString("yyyy-MM-dd HH:mm"),
                    Remark = t.ComplainUser + "投诉[" + t.CompanyName + "]:" + t.ComplainContent,
                    CardExpiredDate = t.ComplainTime,
                }).ToList();
                Enterprise.AddRange(Repast);
                Enterprise.AddRange(MerUser);
                Enterprise.AddRange(complains);
                Enterprise.RemoveAll(t => !t.CardExpiredDate.HasValue);
                return Enterprise.Where(t => t.CardExpiredDate.Value <= DateTime.Now.AddDays(20)).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            }
            else
            {
                var Repast = queryables.Select(t => new
                {
                    t.Id,
                    Name = t.MerchantName,
                    CardType = "营业执照",
                    PersonName = "",
                    CompanyType = t.AllowUnit,
                    CardImg = t.Certification,
                    Title = t.MerchantName + "营业执照到期",
                    Times = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                    Remark = t.MerchantName + "营业执照于" + (t.CardExpiredDate.HasValue ? t.CardExpiredDate.Value.ToString("yyyy年MM月dd日") : "-") + "到期.",
                    t.CardExpiredDate,
                }).ToList();
                var MerUser = users.Where(t => t.DiningType == MerchantEnum.UnitCanteen).Select(t => new
                {
                    t.Id,
                    Name = t.MerchantName,
                    CardType = "健康证",
                    PersonName = t.TrueName,
                    CompanyType = Repast.Where(x => x.Id == t.InfoId).Select(x => x.CompanyType).FirstOrDefault(),
                    CardImg = t.HealthCard,
                    Title = t.MerchantName + "人员：" + t.TrueName + "健康证到期",
                    Times = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                    Remark = t.MerchantName + "人员：" + t.TrueName + "健康证于" + t.ExpiredTime.Value.ToString("yyyy年MM月dd日") + "到期.",
                    CardExpiredDate = t.ExpiredTime
                }).ToList();
                var complains = complain.Where(t => t.CompanyType == "单位食堂" && t.Status != "已处理").Select(t => new
                {
                    t.Id,
                    Name = t.CompanyName,
                    CardType = "投诉",
                    PersonName = t.ComplainUser,
                    CompanyType = t.CompanyType,
                    CardImg = "",
                    Title = t.ComplainUser + "投诉[" + t.CompanyName + "]",
                    Times = t.ComplainTime.Value.ToString("yyyy-MM-dd HH:mm"),
                    Remark = t.ComplainUser + "投诉[" + t.CompanyName + "]:" + t.ComplainContent,
                    CardExpiredDate = t.ComplainTime,
                }).ToList();
                Repast.AddRange(MerUser);
                Repast.AddRange(complains);
                return Repast.Where(t => t.CardExpiredDate.Value <= DateTime.Now.AddDays(20)).OrderByDescending(o => o.CardExpiredDate).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            }
        }
        #endregion 风险预警

        #region 执法检查

        #region 网上执法

        /// <summary>
        /// 网上执法分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtNetPatrol> GetNetPatrolPage(PageParamList<RequestGovtNetPatrol> pageParam)
        {
            IQueryable<GovtNetPatrol> queryable = Kily.Set<GovtNetPatrol>().OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CompanyName))
                queryable = queryable.Where(t => t.CompanyName.Contains(pageParam.QueryParam.CompanyName));
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<GovtNetPatrol, bool>> exp_1 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                                exp_1 = ExpressionExtension.GetExpression<GovtNetPatrol>("TypePath", Areas[i], ExpressionEnum.Like);
                            else
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<GovtNetPatrol>("TypePath", Areas[i], ExpressionEnum.Like));
                        }
                        queryable = queryable.Where(exp_1);
                    }
                    else
                        queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                }
                else
                    queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
            }
            if (GovtInfo().IsEdu.Value)
                queryable = queryable.Where(t => !t.TradeType.Contains("企业") && !t.TradeType.Contains("小"));
            var data = queryable.Select(t => new ResponseGovtNetPatrol()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                CompanyName = t.CompanyName,
                BulletinNum = t.BulletinNum,
                PotrolNum = t.PotrolNum,
                TradeType = t.TradeType,
                QualifiedNum = t.QualifiedNum
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 添加网上执法
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditPatrol(RequestGovtNetPatrol Param)
        {
            GovtNetPatrol patrol = Kily.Set<GovtNetPatrol>().Where(t => t.CompanyId == Param.CompanyId)
                 .Where(t => t.TradeType.Equals(Param.TradeType))
                 .Where(t => t.CompanyName.Equals(Param.CompanyName))
                 .AsNoTracking().FirstOrDefault();
            GovtNetPatrol govtNet = Param.MapToEntity<GovtNetPatrol>();
            var Info = GovtInfo();
            GovtNetPatrolLog Log = new GovtNetPatrolLog
            {
                CompanyId = Param.CompanyId,
                CompanyName = Param.CompanyName,
                RecordTime = DateTime.Now,
                GovtId = Info.Id,
                RocordUser = $"{Info.TrueName}({Info.DepartName})"
            };
            if (patrol == null)
            {
                govtNet.PotrolNum = 1;
                Insert(Log);
                return Insert(govtNet) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
            else
            {
                patrol.PotrolNum += 1;
                Insert(Log);
                return UpdateField(patrol, "PotrolNum") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            }
        }

        /// <summary>
        /// 删除网上执法
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemovePatrol(Guid Id)
        {
            return Remove<GovtNetPatrol>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 获取网上巡查 2019-08-22
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseGovtNetPatrol GetNetPatrolDetail(Guid Id)
        {
            var data = Kily.Set<GovtNetPatrol>().Where(t => t.Id == Id).Select(t => new ResponseGovtNetPatrol()
            {
                Id = t.Id,
                GovtId = t.GovtId,
                CompanyId = t.CompanyId,
                CompanyName = t.CompanyName,
                BulletinNum = t.BulletinNum,
                PotrolNum = t.PotrolNum,
                QualifiedNum = t.QualifiedNum,
                TradeType = t.TradeType,
                CheckTime = t.CreateTime.Value.ToString("yyyy年MM月dd日 HH:mm"),
            }).FirstOrDefault();
            //通报记录
            data.MsgList = Kily.Set<SystemMessage>().Where(t => t.CompanyId == data.CompanyId && t.Category == "通报").Select(t => new ResponseSystemMessage()
            {
                MsgContent = t.MsgContent,
                MsgName = t.MsgName,
                HandleContent = t.HandleContent,
                HandleTime = t.HandleTime,
                ReleaseTime = t.ReleaseTime,
                Status = t.Status ?? "待处理",
            }).OrderByDescending(o => o.ReleaseTime).ToList();
            return data;
        }

        /// <summary>
        /// 通报批评
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditNetPatrol(RequestGovtMsg Param)
        {
            GovtNetPatrol govtNet = Kily.Set<GovtNetPatrol>().Where(t => t.Id == Param.Id).AsNoTracking().FirstOrDefault();
            if (govtNet == null)//详情通报
            {
                govtNet = Kily.Set<GovtNetPatrol>().Where(t => t.CompanyId == Param.Id).AsNoTracking().FirstOrDefault();
            }
            govtNet.BulletinNum += 1;
            govtNet.QualifiedNum = (((govtNet.PotrolNum - govtNet.BulletinNum) * 100) / govtNet.PotrolNum).ToString() + "%";
            List<String> Fields = new List<String> { "BulletinNum", "QualifiedNum" };
            UpdateField(govtNet, null, Fields);
            SystemMessage message = new SystemMessage
            {
                CompanyId = govtNet.CompanyId,
                MsgContent = Param.Content,
                MsgName = Param.Title,
                ReleaseTime = DateTime.Now,
                TrageType = govtNet.TradeType,
                TypePath = govtNet.TypePath,
                Category = "通报",
            };
            return Insert(message) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        /// <summary>
        /// 获取巡查处理内容
        /// </summary>
        /// <returns></returns>
        public List<ResponseSystemMessage> GetMsgList()
        {
            IQueryable<SystemMessage> queryable = Kily.Set<SystemMessage>();//.Where(t => t.Status == "已处理")
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<SystemMessage, bool>> exp_1 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                                exp_1 = ExpressionExtension.GetExpression<SystemMessage>("TypePath", Areas[i], ExpressionEnum.Like);
                            else
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<SystemMessage>("TypePath", Areas[i], ExpressionEnum.Like));
                        }
                        queryable = queryable.Where(exp_1);
                    }
                    else
                        queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                }
                else
                    queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
            }
            return queryable.Where(t => t.Category == "通报").Select(t => new ResponseSystemMessage
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                HandleTime = t.HandleTime,
                HandleContent = t.HandleContent
            }).ToList();
        }

        /// <summary>
        /// 巡查记录日志
        /// </summary>
        /// <returns></returns>
        public List<ResponseGovtNetPatrolLog> GetNetPatrolLogs(Guid Id)
        {
            return Kily.Set<GovtNetPatrolLog>().Where(t => t.GovtId == GovtInfo().Id && t.CompanyId == Id).OrderByDescending(t => t.CreateTime).AsNoTracking().ToList().MapToList<GovtNetPatrolLog, ResponseGovtNetPatrolLog>();
        }

        #endregion 网上执法

        #region 执法类目

        /// <summary>
        /// 执法类目分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtPatrolCategory> GetCategoryPage(PageParamList<RequestGovtPatrolCategory> pageParam)
        {
            IQueryable<GovtPatrolCategory> queryable = Kily.Set<GovtPatrolCategory>().OrderByDescending(t => t.CreateTime).Where(t => t.TypePath.Contains(GovtInfo().City));
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CategoryName))
                queryable = queryable.Where(t => t.CategoryName.Contains(pageParam.QueryParam.CategoryName));
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CategoryType))
                queryable = queryable.Where(t => t.CategoryType.Contains(pageParam.QueryParam.CategoryType));
            var data = queryable.Select(t => new ResponseGovtPatrolCategory()
            {
                Id = t.Id,
                CategoryName = t.CategoryName,
                CategoryType = t.CategoryType,
                CompanyType = t.CompanyType
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 编辑类目
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditCategory(RequestGovtPatrolCategory Param)
        {
            GovtPatrolCategory category = Param.MapToEntity<GovtPatrolCategory>();
            if (Param.Id == Guid.Empty)
                return Insert(category) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return Update(category, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        /// <summary>
        /// 类目详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseGovtPatrolCategory GetCategoryDetail(Guid Id)
        {
            var data = Kily.Set<GovtPatrolCategory>().Where(t => t.Id == Id).Select(t => new ResponseGovtPatrolCategory()
            {
                Id = t.Id,
                GovtId = t.GovtId,
                CategoryName = t.CategoryName,
                Template = t.Template,
                CategoryType = t.CategoryType,
                Grade = t.Grade,
                Remark = t.Remark,
                TypePath = t.TypePath
            }).AsNoTracking().FirstOrDefault();
            return data;
        }

        /// <summary>
        /// 删除类目
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveCategory(Guid Id)
        {
            return Remove<GovtPatrolCategory>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 题库分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtPatrolCategoryAttach> GetCategoryAttachPage(PageParamList<RequestGovtPatrolCategoryAttach> pageParam)
        {
            IQueryable<GovtPatrolCategoryAttach> queryable = Kily.Set<GovtPatrolCategoryAttach>()
                .Where(t => t.PatralCategoryId == pageParam.QueryParam.PatralCategoryId)
                .OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.QuestionTitle))
                queryable = queryable.Where(t => t.QuestionTitle.Contains(pageParam.QueryParam.QuestionTitle));
            var data = queryable.Select(t => new ResponseGovtPatrolCategoryAttach()
            {
                Id = t.Id,
                QuestionTitle = t.QuestionTitle,
                SelectTypeName = AttrExtension.GetSingleDescription<ElementEnum, DescriptionAttribute>(t.SelectType),
                Answer = t.Answer,
                AnswerScore = t.AnswerScore,
                Score = t.Score,
                Type = t.Type
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 编辑题库
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditCategoryAttach(RequestGovtPatrolCategoryAttach Param)
        {
            GovtPatrolCategoryAttach category = Param.MapToEntity<GovtPatrolCategoryAttach>();
            if (Param.Id == Guid.Empty)
                return Insert(category) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return Update(category, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        /// <summary>
        /// 题库详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseGovtPatrolCategoryAttach GetCategoryAttachDetail(Guid Id)
        {
            var data = Kily.Set<GovtPatrolCategoryAttach>().Where(t => t.Id == Id).Select(t => new ResponseGovtPatrolCategoryAttach()
            {
                Id = t.Id,
                GovtId = t.GovtId,
                QuestionTitle = t.QuestionTitle,
                SelectType = t.SelectType,
                Type = t.Type,
                PatralCategoryId = t.PatralCategoryId,
                Answer = t.Answer,
                AnswerScore = t.AnswerScore,
                Score = t.Score
            }).AsNoTracking().FirstOrDefault();
            return data;
        }

        /// <summary>
        /// 删除题库
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveCategoryAttach(Guid Id)
        {
            return Remove<GovtPatrolCategoryAttach>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        #endregion 执法类目

        #region 移动执法

        /// <summary>
        /// 移动执法分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtMovePatrol> GetMovePatralPage(PageParamList<RequestGovtMovePatrol> pageParam)
        {
            IQueryable<GovtMovePatrol> queryable = Kily.Set<GovtMovePatrol>().Where(t => t.GovtId == GovtInfo().Id).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CompanyName))
                queryable = queryable.Where(t => t.CompanyName.Contains(pageParam.QueryParam.CompanyName));
            var data = queryable.Select(t => new ResponseGovtMovePatrol()
            {
                Id = t.Id,
                GovtId = t.GovtId,
                CompanyName = t.CompanyName,
                CategoryName = t.CategoryName,
                PatrolUser = t.PatrolUser,
                PatrolTime = t.PatrolTime
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 删除移动执法记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveMovePatral(Guid Id)
        {
            return Remove<GovtMovePatrol>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 编辑移动执法
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditMovePatrol(RequestGovtMovePatrol Param)
        {
            GovtMovePatrol GovtMove = Param.MapToEntity<GovtMovePatrol>();
            if (Param.Id == Guid.Empty)
            {
                GovtPatrolCategory category = Kily.Set<GovtPatrolCategory>().Where(t => t.Id == Param.CategoryId).FirstOrDefault();
                StringBuilder Sb = new StringBuilder();
                Sb.Append($@"<center>{category.Grade}</center>");
                Sb.Append(@"<table cellpadding='4' width='98%' cellspacing='0' border='1' bordercolor='#000000'>");
                Sb.Append(@"<tr style='text-align:center;line-height:28px;font-size:15px;'><th align='center'>序号</th><th align='center'>检查项</th><th align='center'>结果</th><th align='center'>分值</th></tr>");
                double TotalScore = 0;
                int EleIndex = 0;
                Param.AnswerList.Split(",").ToList().ForEach(t =>
                {
                    var AnswerList = t.Split("|").ToList();
                    Guid CategoryAttachId = Guid.Parse(AnswerList[0]);
                    ResponseGovtPatrolCategoryAttach CategoryAttach = Kily.Set<GovtPatrolCategoryAttach>()
                    .Where(x => x.Id == CategoryAttachId).Select(x => new ResponseGovtPatrolCategoryAttach()
                    {
                        QuestionTitle = x.QuestionTitle,
                        Answer = x.Answer,
                        AnswerScore = x.AnswerScore
                    }).FirstOrDefault();
                    int index = CategoryAttach.Answer.Split("|").ToList().FindIndex(p => p.Equals(AnswerList[1]));
                    string Score = CategoryAttach.AnswerScore != "" ? CategoryAttach.AnswerScore.Split("|").ToList().ElementAtOrDefault(index) : "";
                    Sb.Append($@"<tr style='font-size:13px;'><td align='center'>{++EleIndex}</td><td>{CategoryAttach.QuestionTitle}</td><td align='center'>{AnswerList[1]}</td><td align='center'>{Score}</td></tr>");
                    if (!string.IsNullOrEmpty(Score))
                        TotalScore += Convert.ToDouble(Score);
                });
                Sb.Append($@"<tr><td align='center' style='font-size:13px;'>得分总和</td><td colspan='3' style='font-size:13px;'>{TotalScore}分</td></tr>");
                Sb.Append(@"<tr><td colspan='4' style='font-size:13px;'><center>现场照片</center>");
                Param.ImgList.Split(",").ToList().ForEach(t =>
                {
                    Sb.Append($@"<center><img src='http://system.cfda.vip{t}' style='width:100%;max-width:456px;'></center>");
                });
                if (!string.IsNullOrEmpty(Param.Sound))
                {
                    Sb.Append($@"<tr><td colspan='4' style='font-size:13px;'><iframe style='height: 80px; width:800px;overflow:hidden' src='' frameborder='0' scrolling='no'></iframe></td></tr>");
                }
                Sb.Append($@"<tr><td colspan='4' style='font-size:13px;'>检查备注：{Param.CheckRemark}</td></tr>");
                Sb.Append($@"<tr><td colspan='4' style='font-size:13px;'>备注：{category.Remark}</td></tr>");
                Sb.Append(@"</table>");
                GovtMove.PatrolTable = category.Template.Replace("{companyName}", Param.CompanyName)
                    .Replace("{companyAddress}", Param.CompanyAddress)
                    .Replace("{personName}", Param.PersonName)
                    .Replace("{companyPhone}", Param.CompanyPhone)
                    .Replace("{companyCode}", Param.CompanyCode)
                    .Replace("{companySign}", $"<img src='http://system.cfda.vip{Param.CompanySign}' style='height:36px;'/>")
                    .Replace("{CheckTime}", Param.PatrolTime.ToString())
                    .Replace("{CheckPerson}", Param.PatrolUser)
                    .Replace("{CheckItemList}", Sb.ToString());
                return Insert(GovtMove) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
            else
            {
                GovtMovePatrol patrol = Kily.Set<GovtMovePatrol>().Where(t => t.Id == Param.Id).FirstOrDefault();
                patrol.PatrolTable = Param.PatrolTable;
                return UpdateField(patrol, "PatrolTable") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            }
        }

        /// <summary>
        /// 获取移动执法表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseGovtMovePatrol GetGovtMovePatrolDetail(Guid Id)
        {
            return Kily.Set<GovtMovePatrol>().Where(t => t.Id == Id).Select(t => new ResponseGovtMovePatrol()
            {
                Id = t.Id,
                Sound = t.Sound,
                PatrolTable = t.PatrolTable
            }).FirstOrDefault();
        }

        #endregion 移动执法

        #region 企业自查模板

        /// <summary>
        /// 获取企业检查分页
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtTemplateChild> GetTemplateChild(PageParamList<RequestGovtTemplateChild> pageParam)
        {
            IQueryable<GovtTemplateChild> queryable = Kily.Set<GovtTemplateChild>().Where(t => t.TypePath.Contains(GovtInfo().TypePath)).OrderByDescending(t => t.CreateTime).AsNoTracking();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CompanyName))
                queryable = queryable.Where(t => t.CompanyName.Contains(pageParam.QueryParam.CompanyName));
            var data = queryable.Select(t => new ResponseGovtTemplateChild()
            {
                Id = t.Id,
                CompanyName = t.CompanyName,
                CompanyType = t.CompanyType,
                TemplateName = t.TemplateName,
                CreateTime = t.CreateTime.Value.ToString("yyyy年MM月dd日"),
                CheckUser = t.CheckUser
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 获取企业检查详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseGovtTemplateChild GetTemplateChildDetail(Guid Id)
        {
            return Kily.Set<GovtTemplateChild>().Where(t => t.Id == Id).AsNoTracking().FirstOrDefault().MapToEntity<ResponseGovtTemplateChild>();
        }

        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="CompanyType"></param>
        /// <param name="TypePath"></param>
        /// <returns></returns>
        public IList<ResponseGovtTemplate> GetTemplateContentList(String CompanyType, String TypePath)
        {
            if (CompanyType.Contains("小经营店") || CompanyType.Contains("小作坊") || CompanyType.Contains("小摊贩"))
                CompanyType = "三小商家";
            var data = Kily.Set<GovtTemplate>().Where(t => t.CompanyType.Equals(CompanyType)).Where(t => TypePath.Contains(t.TypePath))
                 .Select(t => new ResponseGovtTemplate()
                 {
                     TemplateName = t.TemplateName,
                     TemplateContent = t.TemplateContent
                 }).ToList();
            return data;
        }

        /// <summary>
        /// 自查模板分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtTemplate> GetTemplatePage(PageParamList<RequestGovtTemplate> pageParam)
        {
            IQueryable<GovtTemplate> queryable = Kily.Set<GovtTemplate>().Where(t => t.GovtId == GovtInfo().Id).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.TemplateName))
                queryable = queryable.Where(t => t.TemplateName.Contains(pageParam.QueryParam.TemplateName));
            var data = queryable.AsNoTracking().Select(t => new ResponseGovtTemplate()
            {
                Id = t.Id,
                GovtId = t.GovtId,
                TemplateName = t.TemplateName,
                CompanyType = t.CompanyType
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 编辑模板
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditGovtTemplate(RequestGovtTemplate Param)
        {
            GovtTemplate Govt = Param.MapToEntity<GovtTemplate>();
            if (Param.Id != Guid.Empty)
                return Update(Govt, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            else
                return Insert(Govt) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveTemplate(Guid Id)
        {
            return Remove<GovtTemplate>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseGovtTemplate GetTemplateDetail(Guid Id)
        {
            return Kily.Set<GovtTemplate>().Where(t => t.Id == Id).AsNoTracking().FirstOrDefault().MapToEntity<ResponseGovtTemplate>();
        }

        #endregion 企业自查模板

        #endregion 执法检查

        #region 应急培训

        #region 培训通知

        /// <summary>
        /// 通知分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtTrainNotice> GetTrainNoticePage(PageParamList<RequestGovtTrainNotice> pageParam)
        {
            IQueryable<GovtTrainNotice> queryable = Kily.Set<GovtTrainNotice>().Where(t => t.GovtId == GovtInfo().Id).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.TrainTitle))
                queryable = queryable.Where(t => t.TrainTitle.Contains(pageParam.QueryParam.TrainTitle));
            var data = queryable.Select(t => new ResponseGovtTrainNotice()
            {
                Id = t.Id,
                TrainTitle = t.TrainTitle,
                TrainPlace = t.TrainPlace,
                TrainTime = t.TrainTime,
                CompanyType = t.CompanyType,
                Remark = t.Remark,
                Desc = t.Remark.NoHtml()
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 编辑通知
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditNotice(RequestGovtTrainNotice Param)
        {
            GovtTrainNotice notice = Param.MapToEntity<GovtTrainNotice>();
            if (Param.Id == Guid.Empty)
                return Insert(notice) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return Update(notice, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveNotice(Guid Id)
        {
            return Remove<GovtTrainNotice>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 通知详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseGovtTrainNotice GetTrainNoticeDetail(Guid Id)
        {
            var data = Kily.Set<GovtTrainNotice>().Where(t => t.Id == Id).Select(t => new ResponseGovtTrainNotice()
            {
                Id = t.Id,
                GovtId = t.GovtId,
                CompanyType = t.CompanyType,
                TrainTime = t.TrainTime,
                TrainPlace = t.TrainPlace,
                Remark = t.Remark,
                TrainTitle = t.TrainTitle
            }).FirstOrDefault();
            return data;
        }

        /// <summary>
        /// 推送通知
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string ReportTrainNotice(Guid Id)
        {
            GovtTrainNotice notice = Kily.Set<GovtTrainNotice>().Where(t => t.Id == Id).FirstOrDefault();
            SystemMessage message = new SystemMessage();
            message.MsgName = notice.TrainTitle;
            message.MsgContent = notice.Remark;
            message.ReleaseTime = DateTime.Now;
            message.TypePath = GovtInfo().TypePath;
            message.Category = "常规";
            if (!notice.CompanyType.Contains(","))
            {
                message.TrageType = notice.CompanyType;
                return Insert(message) ? ServiceMessage.HANDLESUCCESS : ServiceMessage.HANDLEFAIL;
            }
            else
            {
                notice.CompanyType.Split(",").ToList().ForEach(t =>
                {
                    message.TrageType = t;
                    Insert(message);
                });
                return ServiceMessage.HANDLESUCCESS;
            }
        }

        #endregion 培训通知

        #region 培训报道

        /// <summary>
        /// 报道分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtTrainReport> GetTrainReportPage(PageParamList<RequestGovtTrainReport> pageParam)
        {
            IQueryable<GovtTrainReport> queryable = Kily.Set<GovtTrainReport>().Where(t => t.GovtId == GovtInfo().Id).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.InfoTitle))
                queryable = queryable.Where(t => t.InfoTitle.Contains(pageParam.QueryParam.InfoTitle));
            var data = queryable.Select(t => new ResponseGovtTrainReport()
            {
                Id = t.Id,
                InfoTitle = t.InfoTitle,
                CompanyType = t.CompanyType,
                CreateTime = t.CreateTime.Value.ToString("yyyy年MM月dd日 HH:mm"),
                InfoContent = t.InfoContent
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 编辑报道
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditTrainReport(RequestGovtTrainReport Param)
        {
            GovtTrainReport report = Param.MapToEntity<GovtTrainReport>();
            if (Param.Id == Guid.Empty)
                return Insert(report) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return Update(report, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        /// <summary>
        /// 删除报道
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveReport(Guid Id)
        {
            return Remove<GovtTrainReport>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 报道详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseGovtTrainReport GetTrainReportDetail(Guid Id)
        {
            var data = Kily.Set<GovtTrainReport>().Where(t => t.Id == Id).Select(t => new ResponseGovtTrainReport()
            {
                Id = t.Id,
                GovtId = t.GovtId,
                CompanyType = t.CompanyType,
                InfoContent = t.InfoContent,
                CreateTime = t.CreateTime.Value.ToString("yyyy年MM月dd日"),
                InfoTitle = t.InfoTitle
            }).FirstOrDefault();
            return data;
        }

        /// <summary>
        /// 推送报道
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string ReportTrainReport(Guid Id)
        {
            GovtTrainReport report = Kily.Set<GovtTrainReport>().Where(t => t.Id == Id).FirstOrDefault();
            SystemMessage message = new SystemMessage
            {
                MsgName = report.InfoTitle,
                MsgContent = report.InfoContent,
                ReleaseTime = DateTime.Now,
                TrageType = report.CompanyType,
                Category = "常规",
                TypePath = GovtInfo().TypePath
            };
            return Insert(message) ? ServiceMessage.HANDLESUCCESS : ServiceMessage.HANDLEFAIL;
        }

        #endregion 培训报道

        #endregion 应急培训

        #region 投诉信息

        /// <summary>
        /// 投诉信息分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtComplain> GetComplainPage(PageParamList<RequestGovtComplain> pageParam)
        {
            IQueryable<GovtComplain> queryable = Kily.Set<GovtComplain>().OrderByDescending(t => t.CreateTime);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<GovtComplain, bool>> exp_1 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                                exp_1 = ExpressionExtension.GetExpression<GovtComplain>("TypePath", Areas[i], ExpressionEnum.Like);
                            else
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<GovtComplain>("TypePath", Areas[i], ExpressionEnum.Like));
                        }
                        queryable = queryable.Where(exp_1);
                    }
                    else
                        queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                }
                else
                    queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
            }
            if (!string.IsNullOrEmpty(pageParam.QueryParam.ComplainContent))
                queryable = queryable.Where(t => t.ComplainContent.Contains(pageParam.QueryParam.ComplainContent));
            if (GovtInfo().IsEdu.Value)
                queryable = queryable.Where(t => t.CompanyType.Contains("单位"));
            var data = queryable.Select(t => new ResponseGovtComplain
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                CompanyName = t.CompanyName,
                Status = t.Status,
                CompanyType = t.CompanyType,
                ComplainContent = t.ComplainContent,
                ComplainTime = t.ComplainTime,
                ComplainUserPhone = t.ComplainUserPhone,
                ProductName = t.ProductName,
                ComplainUser = t.ComplainUser,
                HandlerContent = t.HandlerContent,
                SendStatus = t.IsDelete == true ? "已推送" : "待推送"
            }).ToList();
            if (GovtInfo().IsEdu.Value)
                data = data.Select(t => new ResponseGovtComplain
                {
                    Id = t.Id,
                    CompanyId = t.CompanyId,
                    CompanyName = t.CompanyName,
                    Status = t.Status,
                    CompanyType = Kily.Set<RepastInfo>().Where(x => x.Id == t.CompanyId).Where(x => x.DiningType == MerchantEnum.UnitCanteen).Select(x => x.AllowUnit).FirstOrDefault(),
                    ComplainContent = t.ComplainContent,
                    ComplainTime = t.ComplainTime,
                    ComplainUserPhone = t.ComplainUserPhone,
                    ProductName = t.ProductName,
                    ComplainUser = t.ComplainUser,
                    HandlerContent = t.HandlerContent,
                    SendStatus = t.SendStatus
                }).ToList();
            return data.ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
        }

        /// <summary>
        /// 删除投诉
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveComplain(Guid Id)
        {
            return Remove<GovtComplain>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 编辑投诉
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditComplain(RequestGovtComplain Param)
        {
            GovtComplain complain = Param.MapToEntity<GovtComplain>();
            complain.Status = "待处理";
            complain.Category = "投诉";
            complain.ComplainTime = DateTime.Now;
            return Insert(complain) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        /// <summary>
        /// 投诉详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseGovtComplain GetComplainDetail(Guid Id)
        {
            var queryable = Kily.Set<GovtComplain>().Where(t => t.Id == Id);
            var data = queryable.Select(t => new ResponseGovtComplain
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                CompanyName = t.CompanyName,
                Status = t.Status,
                CompanyType = t.CompanyType,
                ComplainContent = t.ComplainContent,
                ComplainTime = t.ComplainTime,
                ComplainUserPhone = t.ComplainUserPhone,
                HandlerTime = t.UpdateTime.HasValue ? t.UpdateTime.Value.ToString("yyyy年MM月dd日 HH:mm") : "-",
                ProductName = t.ProductName,
                ComplainUser = t.ComplainUser,
                HandlerContent = t.HandlerContent,
                SendStatus = t.IsDelete == true ? "已推送" : "待推送"
            }).FirstOrDefault();
            return data;
        }

        /// <summary>
        /// 推送投诉
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string ReportComplain(Guid Id)
        {
            GovtComplain complain = Kily.Set<GovtComplain>().Where(t => t.Id == Id).FirstOrDefault();
            Delete<GovtComplain>(t => t.Id == Id);
            SystemMessage message = new SystemMessage
            {
                ComplainId = complain.Id,
                CompanyId = complain.CompanyId,
                MsgName = complain.ProductName,
                MsgContent = complain.ComplainContent,
                ReleaseTime = DateTime.Now,
                TrageType = complain.CompanyType,
                TypePath = complain.TypePath,
                Category = "投诉",
            };
            return Insert(message) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        /// <summary>
        /// 处理投诉
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string ReportComplainInfo(Guid Id, string Param)
        {
            GovtComplain complain = Kily.Set<GovtComplain>().Where(t => t.Id == Id).AsNoTracking().FirstOrDefault();
            SystemMessage message = Kily.Set<SystemMessage>().Where(t => t.Id == Id).AsNoTracking().FirstOrDefault();
            SystemLogInfo Log = new SystemLogInfo();
            if (complain != null)
            {
                //日志记录
                Log.HandlerUser = complain.CompanyName;
                Log.TypePath = complain.TypePath;
                Log.HandlerType = "投诉处理";
                Log.HandlerTime = DateTime.Now;
                Log.Id = Guid.NewGuid();
                Log.HandlerContent = $"[{Log.HandlerUser}]对投诉内容：{complain.ComplainContent}进行了处理，处理时间[{Log.HandlerTime.Value.ToShortDateString()}]";
                Kily.Add(Log);
                Kily.SaveChanges();
                //投诉状态
                complain.HandlerContent = Param;
                complain.Status = "已处理";
                List<String> Fields = new List<String> { "HandlerContent", "Status" };
                return UpdateField(complain, null, Fields) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            }
            else
            {
                if (message.TrageType.Contains("企业") && message.TrageType != "餐饮企业")
                {
                    Log.HandlerUser = Kily.Set<EnterpriseInfo>().Where(o => o.Id == message.CompanyId).FirstOrDefault().CompanyName;
                }
                else
                {
                    Log.HandlerUser = Kily.Set<RepastInfo>().Where(o => o.Id == message.CompanyId).FirstOrDefault().MerchantName;
                }
                //日志记录

                Log.TypePath = message.TypePath;
                Log.HandlerType = "通报处理";
                Log.HandlerTime = DateTime.Now;
                Log.Id = Guid.NewGuid();
                Log.HandlerContent = $"[{ Log.HandlerUser}]对通报内容：{message.MsgContent}进行了处理，处理时间[{Log.HandlerTime.Value.ToShortDateString()}]";
                Kily.Add(Log);
                Kily.SaveChanges();

                message.HandleTime = DateTime.Now;
                message.HandleContent = Param;
                message.Status = "已处理";
                List<String> Fields = new List<String> { "HandleContent", "Status", "HandleTime" };
                return UpdateField(message, null, Fields) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            }
        }

        #endregion 投诉信息

        #region 数据统计

        #region 新旧大屏

        /// <summary>
        /// 获取产品统计
        /// </summary>
        /// <returns></returns>
        public IList<DataPie> GetProductRank()
        {
            IQueryable<EnterpriseGoods> goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false && t.AuditType == AuditEnum.AuditSuccess);
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false).Where(t => t.AuditType == AuditEnum.AuditSuccess);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
            IList<string> Areas = GetDepartArea();
            if (Areas != null)
            {
                if (Areas.Count > 1)
                {
                    Expression<Func<EnterpriseInfo, bool>> exp_1 = null;
                    for (int i = 0; i < Areas.Count; i++)
                    {
                        if (i == 0)
                            exp_1 = ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                        else
                            exp_1 = exp_1.Or(ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                    }
                    queryable = queryable.Where(exp_1);
                }
                else
                    queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
            }
            else
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
            return goods.Join(queryable, t => t.CompanyId, x => x.Id, (t, x) => new { t.ProductType }).GroupBy(t => t.ProductType).Select(t => new DataPie
            {
                value = t.Count(),
                name = t.Key,
                url = ""
            }).ToList();
        }

        /// <summary>
        /// 获取人员统计
        /// </summary>
        /// <returns></returns>
        public IList<DataPie> GetPersonBank()
        {
            IQueryable<RepastInfoUser> users = Kily.Set<RepastInfoUser>().Where(t => t.IsDelete == false);
            IQueryable<RepastInfo> queryable = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false).Where(t => t.AuditType == AuditEnum.AuditSuccess && t.DiningType == MerchantEnum.UnitCanteen);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
            IList<string> Areas = GetDepartArea();
            if (Areas != null)
            {
                if (Areas.Count > 1)
                {
                    Expression<Func<RepastInfo, bool>> exp_1 = null;
                    for (int i = 0; i < Areas.Count; i++)
                    {
                        if (i == 0)
                            exp_1 = ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                        else
                            exp_1 = exp_1.Or(ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                    }
                    queryable = queryable.Where(exp_1);
                }
                else
                    queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
            }
            else
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
            var companyList = queryable.ToList().Select(o => o.Id.ToString()).ToList<string>();
            companyList.Add("");
            var companyIds = string.Join(",", companyList);
            var UserList = users.Where(o => companyList.Contains(o.InfoId.ToString())).ToList();
            var OutPie = new DataPie
            {
                value = UserList.Where(o => o.ExpiredTime.Value <= DateTime.Now).ToList().Count,
                name = "已到期",
                url = ""
            };
            var OKPie = new DataPie
            {
                value = UserList.Where(o => o.ExpiredTime.Value > DateTime.Now).ToList().Count,
                name = "正常",
                url = ""
            };
            var list = new List<DataPie>();
            list.Add(OutPie);
            list.Add(OKPie);
            return list;
        }

        /// <summary>
        /// 获取入驻的企业地图
        /// </summary>
        /// <returns></returns>
        public ResponseGovtMap GetAllCityMerchantCount()
        {
            var Temp = GovtInfo();
            ResponseCity City = Kily.Set<SystemCity>().Where(t => t.Id.ToString() == GovtInfo().City).Select(t => new ResponseCity
            {
                CityId = t.Code,
                CityName = t.Name
            }).FirstOrDefault();
            List<ResponseArea> Area = Kily.Set<SystemArea>().Where(t => t.CityCode == City.CityId).AsNoTracking().Select(t => new ResponseArea
            {
                Id = t.Id,
                AreaName = t.Name
            }).ToList();
            List<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false).Where(t => !string.IsNullOrEmpty(t.TypePath)).Where(o => o.AuditType == AuditEnum.AuditSuccess).ToList();
            List<RepastInfo> queryables = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false).Where(t => !string.IsNullOrEmpty(t.TypePath)).ToList();
            List<ResponseGovtRanking> data = new List<ResponseGovtRanking>();
            Area.ForEach(t =>
            {
                int TotalCompany = queryable.Where(x => x.TypePath.Contains(t.Id.ToString())).Select(x => x.Id).Count();
                int TotalMerchant = queryables.Where(x => x.TypePath.Contains(t.Id.ToString())).Select(x => x.Id).Count();
                if (!Temp.IsEdu.Value)
                    data.Add(new ResponseGovtRanking { AreaName = t.AreaName, TotalCount = TotalCompany + TotalMerchant });
                else
                {
                    var total = queryables.Where(x => x.TypePath.Contains(t.Id.ToString())).Where(x => x.DiningType == MerchantEnum.UnitCanteen).Count();
                    data.Add(new ResponseGovtRanking { AreaName = t.AreaName, TotalCount = total });
                }
            });
            data = data.OrderByDescending(t => t.TotalCount).ToList();
            int Total = data.Sum(t => t.TotalCount);
            return new ResponseGovtMap { CityName = City.CityName, City = City.CityId, DataList = data, All = Total };
        }

        #endregion 新旧大屏

        #region 新大屏

        /// <summary>
        /// 今日数据
        /// </summary>
        /// <returns></returns>
        public Object GetNewStayInTodayCount()
        {
            IQueryable<EnterpriseInfo> coms = Kily.Set<EnterpriseInfo>().Where(t => t.AuditType == AuditEnum.AuditSuccess);
            IQueryable<RepastInfo> mers = Kily.Set<RepastInfo>().Where(t => t.AuditType == AuditEnum.AuditSuccess);
            IQueryable<EnterpriseGoods> goods = Kily.Set<EnterpriseGoods>().Where(t => 1 == 1).Where(t => t.AuditType == AuditEnum.AuditSuccess);
            IQueryable<EnterpriseScanCodeInfo> infos = Kily.Set<EnterpriseScanCodeInfo>().Where(t => 1 == 1);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
            {
                coms = coms.Where(t => t.TypePath.Contains(GovtInfo().City));
                mers = mers.Where(t => t.TypePath.Contains(GovtInfo().City));
            }
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<EnterpriseInfo, bool>> exp_1 = null;
                        Expression<Func<RepastInfo, bool>> exp_2 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_2 = ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_2 = exp_2.Or(ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                            }
                        }
                        coms = coms.Where(exp_1);
                        mers = mers.Where(exp_2);
                    }
                    else
                    {
                        coms = coms.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        mers = mers.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                    }
                }
                else
                {
                    coms = coms.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    mers = mers.Where(t => t.TypePath.Contains(GovtInfo().Area));
                }
            }
            int ProductSum_Today = coms.Join(goods, t => t.Id, x => x.CompanyId, (t, x) => new { x.Id }).Count();
            int ScanCodeSum_Today = coms.Join(infos, t => t.Id, x => x.CompanyId, (t, x) => new { x.ScanNum }).Sum(t => t.ScanNum);
            int Company = coms.Count() + mers.Count();
            return new { Company, ProductSum_Today, ScanCodeSum_Today };
        }

        /// <summary>
        /// 历史所有企业数据统计
        /// </summary>
        /// <returns></returns>
        public IList<DataPie> GetNewStayInAllCompanyCount()
        {
            var Temp = GovtInfo();
            IQueryable<EnterpriseInfo> coms = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false && t.AuditType == AuditEnum.AuditSuccess);
            IQueryable<RepastInfo> mers = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false && t.AuditType == AuditEnum.AuditSuccess);
            IQueryable<CookInfo> cooks = Kily.Set<CookInfo>().Where(t => t.IsDelete == false && t.AuditType == AuditEnum.AuditSuccess);
            if (Temp.AccountType <= GovtAccountEnum.City)
            {
                coms = coms.Where(t => t.TypePath.Contains(Temp.City));
                mers = mers.Where(t => t.TypePath.Contains(Temp.City));
                cooks = cooks.Where(t => t.TypePath.Contains(Temp.City));
            }
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<EnterpriseInfo, bool>> exp_1 = null;
                        Expression<Func<RepastInfo, bool>> exp_2 = null;
                        Expression<Func<CookInfo, bool>> exp_3 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_2 = ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_3 = ExpressionExtension.GetExpression<CookInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_2 = exp_2.Or(ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_3 = exp_3.Or(ExpressionExtension.GetExpression<CookInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                            }
                        }
                        coms = coms.Where(exp_1);
                        mers = mers.Where(exp_2);
                        cooks = cooks.Where(exp_3);
                    }
                    else
                    {
                        coms = coms.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        mers = mers.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        cooks = cooks.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                    }
                }
                else
                {
                    coms = coms.Where(t => t.TypePath.Contains(Temp.Area));
                    mers = mers.Where(t => t.TypePath.Contains(Temp.Area));
                    cooks = cooks.Where(t => t.TypePath.Contains(Temp.Area));
                }
            }
            List<DataPie> Pie = coms.GroupBy(t => t.CompanyType).Select(t => new DataPie { name = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.Key), value = t.Count() }).ToList();
            Pie.AddRange(mers.GroupBy(t => t.DiningType).Select(t => new DataPie { name = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.Key), value = t.Count() }).ToList());
            var total = Pie.Where(t => t.name == "小经营店" || t.name == "小作坊" || t.name == "小摊贩").Sum(t => t.value);
            Pie.RemoveAll(t => t.name == "小经营店" || t.name == "小作坊" || t.name == "小摊贩");
            Pie.Add(new DataPie { name = "三小企业", value = total });
            if (!Temp.IsEdu.Value)
                return Pie;
            else
            {
                Pie = new List<DataPie>();
                Pie = mers.Where(t => t.DiningType == MerchantEnum.UnitCanteen).GroupBy(t => t.AllowUnit).Select(t => new DataPie { name = t.Key, value = t.Count() }).ToList();
                return Pie;
            }
        }

        /// <summary>
        /// 今日入住
        /// </summary>
        /// <returns></returns>
        public String GetTodayNow()
        {
            IQueryable<EnterpriseInfo> coms = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime).Where(t => t.CreateTime.Value.ToShortDateString() == DateTime.Now.ToShortDateString());
            IQueryable<RepastInfo> mers = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime).Where(t => t.CreateTime.Value.ToShortDateString() == DateTime.Now.ToShortDateString());
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
            {
                coms = coms.Where(t => t.TypePath.Contains(GovtInfo().City));
                mers = mers.Where(t => t.TypePath.Contains(GovtInfo().City));
            }
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<EnterpriseInfo, bool>> exp_1 = null;
                        Expression<Func<RepastInfo, bool>> exp_2 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_2 = ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_2 = exp_2.Or(ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                            }
                        }
                        coms = coms.Where(exp_1);
                        mers = mers.Where(exp_2);
                    }
                    else
                    {
                        coms = coms.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        mers = mers.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                    }
                }
                else
                {
                    coms = coms.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    mers = mers.Where(t => t.TypePath.Contains(GovtInfo().Area));
                }
            }
            List<string> data = new List<string>();
            if (!GovtInfo().IsEdu.Value)
            {
                data.AddRange(coms.Select(t => t.CompanyName).ToList());
                data.AddRange(mers.Select(t => t.MerchantName).ToList());
            }
            else
            {
                data.AddRange(mers.Select(t => t.MerchantName).ToList());
            }
            return string.Join(",", data);
        }

        /// <summary>
        /// 投诉和风险
        /// </summary>
        /// <returns></returns>
        public IList<DataBar> GetNewWeekRiskAndComplainCount()
        {
            IQueryable<GovtRisk> risks = Kily.Set<GovtRisk>().Where(t => t.IsDelete == false);
            IQueryable<GovtComplain> complains = Kily.Set<GovtComplain>();
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<RepastInfo> queryables = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<RepastInfoUser> users = Kily.Set<RepastInfoUser>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
            {
                risks = risks.Where(t => t.TypePath.Contains(GovtInfo().City));
                queryables = queryables.Where(t => t.TypePath.Contains(GovtInfo().City));
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
                users = users.Where(t => t.TypePath.Contains(GovtInfo().City));
                complains = complains.Where(t => t.TypePath.Contains(GovtInfo().City));
            }
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<GovtRisk, bool>> exp_1 = null;
                        Expression<Func<GovtComplain, bool>> exp_2 = null;
                        Expression<Func<EnterpriseInfo, bool>> exp_3 = null;
                        Expression<Func<RepastInfo, bool>> exp_4 = null;
                        Expression<Func<RepastInfoUser, bool>> exp_5 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<GovtRisk>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_2 = ExpressionExtension.GetExpression<GovtComplain>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_3 = ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_4 = ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_5 = ExpressionExtension.GetExpression<RepastInfoUser>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<GovtRisk>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_2 = exp_2.Or(ExpressionExtension.GetExpression<GovtComplain>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_3 = exp_3.Or(ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_4 = exp_4.Or(ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_5 = exp_5.Or(ExpressionExtension.GetExpression<RepastInfoUser>("TypePath", Areas[i], ExpressionEnum.Like));
                            }
                        }
                        risks = risks.Where(exp_1);
                        complains = complains.Where(exp_2);
                        queryables = queryables.Where(exp_4);
                        queryable = queryable.Where(exp_3);
                        users = users.Where(exp_5);
                    }
                    else
                    {
                        risks = risks.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        complains = complains.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        queryables = queryables.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        users = users.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                    }
                }
                else
                {
                    risks = risks.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    complains = complains.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    queryables = queryables.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                    queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                    users = users.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                }
            }
            List<DataBar> bars = new List<DataBar>();
            //风险
            bars.Add(new DataBar
            {
                name = "风险信息",
                data = new List<int> {
                    risks.Where(t => t.ReleaseTime.Value.Day-DateTime.Now.Day==-6).Count(),
                    risks.Where(t => t.ReleaseTime.Value.Day-DateTime.Now.Day==-5).Count(),
                    risks.Where(t => t.ReleaseTime.Value.Day-DateTime.Now.Day==-4).Count(),
                    risks.Where(t => t.ReleaseTime.Value.Day-DateTime.Now.Day==-3).Count(),
                    risks.Where(t => t.ReleaseTime.Value.Day-DateTime.Now.Day==-2).Count(),
                    risks.Where(t => t.ReleaseTime.Value.Day-DateTime.Now.Day==-1).Count(),
                    risks.Where(t => t.ReleaseTime.Value.Day-DateTime.Now.Day==0).Count(),
                }
            });
            //投诉
            bars.Add(new DataBar
            {
                name = "投诉信息",
                data = new List<int> {
                    complains.Where(t => t.ComplainTime.Value.Day-DateTime.Now.Day==-6).Count(),
                    complains.Where(t => t.ComplainTime.Value.Day-DateTime.Now.Day==-5).Count(),
                    complains.Where(t => t.ComplainTime.Value.Day-DateTime.Now.Day==-4).Count(),
                    complains.Where(t => t.ComplainTime.Value.Day-DateTime.Now.Day==-3).Count(),
                    complains.Where(t => t.ComplainTime.Value.Day-DateTime.Now.Day==-1).Count(),
                    complains.Where(t => t.ComplainTime.Value.Day-DateTime.Now.Day==-1).Count(),
                    complains.Where(t => t.ComplainTime.Value.Day-DateTime.Now.Day==0).Count(),
                }
            });
            try
            {
                //预警
                bars.Add(new DataBar
                {
                    name = "证件到期",
                    data = new List<int> {
                    queryables.Where(t => t.CardExpiredDate.Value.Day-DateTime.Now.Day==-6).Count()+queryable.Where(t => t.CardExpiredDate.Value.Day-DateTime.Now.Day==-6).Count()+users.Where(t => t.ExpiredTime.Value.Day-DateTime.Now.Day==-6).Count(),
                    queryables.Where(t => t.CardExpiredDate.Value.Day-DateTime.Now.Day==-5).Count()+queryable.Where(t => t.CardExpiredDate.Value.Day-DateTime.Now.Day==-5).Count()+users.Where(t => t.ExpiredTime.Value.Day-DateTime.Now.Day==-5).Count(),
                    queryables.Where(t => t.CardExpiredDate.Value.Day-DateTime.Now.Day==-4).Count()+queryable.Where(t => t.CardExpiredDate.Value.Day-DateTime.Now.Day==-4).Count()+users.Where(t => t.ExpiredTime.Value.Day-DateTime.Now.Day==-4).Count(),
                    queryables.Where(t => t.CardExpiredDate.Value.Day-DateTime.Now.Day==-3).Count()+queryable.Where(t => t.CardExpiredDate.Value.Day-DateTime.Now.Day==-3).Count()+users.Where(t => t.ExpiredTime.Value.Day-DateTime.Now.Day==-3).Count(),
                    queryables.Where(t => t.CardExpiredDate.Value.Day-DateTime.Now.Day==-1).Count()+queryable.Where(t => t.CardExpiredDate.Value.Day-DateTime.Now.Day==-1).Count()+users.Where(t => t.ExpiredTime.Value.Day-DateTime.Now.Day==-1).Count(),
                    queryables.Where(t => t.CardExpiredDate.Value.Day-DateTime.Now.Day==-1).Count()+queryable.Where(t => t.CardExpiredDate.Value.Day-DateTime.Now.Day==-1).Count()+users.Where(t => t.ExpiredTime.Value.Day-DateTime.Now.Day==-1).Count(),
                    queryables.Where(t => t.CardExpiredDate.Value.Day-DateTime.Now.Day==0).Count()+queryable.Where(t => t.CardExpiredDate.Value.Day-DateTime.Now.Day==0).Count()+users.Where(t => t.ExpiredTime.Value.Day-DateTime.Now.Day==0).Count(),
                }
                });
            }
            catch
            {

            }

            //告警
            bars.Add(new DataBar
            {
                name = "视频告警",
                data = new List<int> {
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0
                }
            });
            return bars;
        }

        /// <summary>
        /// 网上巡查统计
        /// </summary>
        /// <returns></returns>
        public IList<DataLine> GetNewNetCheckCount()
        {
            IQueryable<GovtTemplateChild> children = Kily.Set<GovtTemplateChild>().Where(t => t.IsDelete == false);
            IQueryable<GovtMovePatrol> moves = Kily.Set<GovtMovePatrol>().Where(t => t.IsDelete == false);
            IQueryable<GovtNetPatrol> patrols = Kily.Set<GovtNetPatrol>().Where(t => t.IsDelete == false);
            IQueryable<SystemMessage> msg = Kily.Set<SystemMessage>();
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
            {
                children = children.Where(t => t.TypePath.Contains(GovtInfo().City));
                patrols = patrols.Where(t => t.TypePath.Contains(GovtInfo().City));
            }
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<GovtTemplateChild, bool>> exp_1 = null;
                        Expression<Func<GovtNetPatrol, bool>> exp_2 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<GovtTemplateChild>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_2 = ExpressionExtension.GetExpression<GovtNetPatrol>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<GovtTemplateChild>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_2 = exp_2.Or(ExpressionExtension.GetExpression<GovtNetPatrol>("TypePath", Areas[i], ExpressionEnum.Like));
                            }
                        }
                        children = children.Where(exp_1);
                        patrols = patrols.Where(exp_2);
                    }
                    else
                    {
                        children = children.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        patrols = patrols.Where(t => t.TypePath.Contains(GovtInfo().City));
                    }
                }
                else
                {
                    children = children.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    patrols = patrols.Where(t => t.TypePath.Contains(GovtInfo().City));
                }
            }
            //是否教育局
            if (GovtInfo().IsEdu.HasValue)
            {
                //patrols = patrols.Where(o => o.GovtId == GovtInfo().Id);
            }
            else
            {

            }
            List<DataLine> lines = new List<DataLine>();
            var datas = patrols.Join(msg, t => t.CompanyId, x => x.CompanyId, (t, x) => new { x.ReleaseTime, x.Category });
            moves = moves.Where(t => t.GovtId == GovtInfo().Id);
            //自查
            lines.Add(new DataLine
            {
                name = "自查",
                data = new List<int> {
                    children.Where(t => t.CreateTime.Value.Day-DateTime.Now.Day==-6&& t.CreateTime.Value.Month==DateTime.Now.Month&&t.CreateTime.Value.Year==DateTime.Now.Year).Count(),
                    children.Where(t => t.CreateTime.Value.Day-DateTime.Now.Day==-5&& t.CreateTime.Value.Month==DateTime.Now.Month&&t.CreateTime.Value.Year==DateTime.Now.Year).Count(),
                    children.Where(t => t.CreateTime.Value.Day-DateTime.Now.Day==-4&& t.CreateTime.Value.Month==DateTime.Now.Month&&t.CreateTime.Value.Year==DateTime.Now.Year).Count(),
                    children.Where(t => t.CreateTime.Value.Day-DateTime.Now.Day==-3&& t.CreateTime.Value.Month==DateTime.Now.Month&&t.CreateTime.Value.Year==DateTime.Now.Year).Count(),
                    children.Where(t => t.CreateTime.Value.Day-DateTime.Now.Day==-2&& t.CreateTime.Value.Month==DateTime.Now.Month&&t.CreateTime.Value.Year==DateTime.Now.Year).Count(),
                    children.Where(t => t.CreateTime.Value.Day-DateTime.Now.Day==-1&& t.CreateTime.Value.Month==DateTime.Now.Month&&t.CreateTime.Value.Year==DateTime.Now.Year).Count(),
                    children.Where(t => t.CreateTime.Value.Day-DateTime.Now.Day==0&& t.CreateTime.Value.Month==DateTime.Now.Month&&t.CreateTime.Value.Year==DateTime.Now.Year).Count()
                }
            });
            //抽查
            lines.Add(new DataLine
            {
                name = "抽查",
                data = new List<int> {
                    patrols.Where(t => t.CreateTime.Value.Day-DateTime.Now.Day==-6&& t.CreateTime.Value.Month==DateTime.Now.Month&&t.CreateTime.Value.Year==DateTime.Now.Year).Sum(t=>t.PotrolNum),
                    patrols.Where(t => t.CreateTime.Value.Day-DateTime.Now.Day==-5&& t.CreateTime.Value.Month==DateTime.Now.Month&&t.CreateTime.Value.Year==DateTime.Now.Year).Sum(t=>t.PotrolNum),
                    patrols.Where(t => t.CreateTime.Value.Day-DateTime.Now.Day==-4&& t.CreateTime.Value.Month==DateTime.Now.Month&&t.CreateTime.Value.Year==DateTime.Now.Year).Sum(t=>t.PotrolNum),
                    patrols.Where(t => t.CreateTime.Value.Day-DateTime.Now.Day==-3&& t.CreateTime.Value.Month==DateTime.Now.Month&&t.CreateTime.Value.Year==DateTime.Now.Year).Sum(t=>t.PotrolNum),
                    patrols.Where(t => t.CreateTime.Value.Day-DateTime.Now.Day==-2&& t.CreateTime.Value.Month==DateTime.Now.Month&&t.CreateTime.Value.Year==DateTime.Now.Year).Sum(t=>t.PotrolNum),
                    patrols.Where(t => t.CreateTime.Value.Day-DateTime.Now.Day==-1&& t.CreateTime.Value.Month==DateTime.Now.Month&&t.CreateTime.Value.Year==DateTime.Now.Year).Sum(t=>t.PotrolNum),
                    patrols.Where(t => t.CreateTime.Value.Day-DateTime.Now.Day==0&& t.CreateTime.Value.Month==DateTime.Now.Month&&t.CreateTime.Value.Year==DateTime.Now.Year).Sum(t=>t.PotrolNum)
                }
            });
            //通报
            lines.Add(new DataLine
            {
                name = "通报",
                data = new List<int> {
                    datas.Where(t=>t.Category.Equals("通报")).Where(t => t.ReleaseTime.Value.Day-DateTime.Now.Day==-6&& t.ReleaseTime.Value.Month==DateTime.Now.Month&&t.ReleaseTime.Value.Year==DateTime.Now.Year).Count(),
                    datas.Where(t=>t.Category.Equals("通报")).Where(t => t.ReleaseTime.Value.Day-DateTime.Now.Day==-5&& t.ReleaseTime.Value.Month==DateTime.Now.Month&&t.ReleaseTime.Value.Year==DateTime.Now.Year).Count(),
                    datas.Where(t=>t.Category.Equals("通报")).Where(t => t.ReleaseTime.Value.Day-DateTime.Now.Day==-4&& t.ReleaseTime.Value.Month==DateTime.Now.Month&&t.ReleaseTime.Value.Year==DateTime.Now.Year).Count(),
                    datas.Where(t=>t.Category.Equals("通报")).Where(t => t.ReleaseTime.Value.Day-DateTime.Now.Day==-3&& t.ReleaseTime.Value.Month==DateTime.Now.Month&&t.ReleaseTime.Value.Year==DateTime.Now.Year).Count(),
                    datas.Where(t=>t.Category.Equals("通报")).Where(t => t.ReleaseTime.Value.Day-DateTime.Now.Day==-2&& t.ReleaseTime.Value.Month==DateTime.Now.Month&&t.ReleaseTime.Value.Year==DateTime.Now.Year).Count(),
                    datas.Where(t=>t.Category.Equals("通报")).Where(t => t.ReleaseTime.Value.Day-DateTime.Now.Day==-1&& t.ReleaseTime.Value.Month==DateTime.Now.Month&&t.ReleaseTime.Value.Year==DateTime.Now.Year).Count(),
                    datas.Where(t=>t.Category.Equals("通报")).Where(t => t.ReleaseTime.Value.Day-DateTime.Now.Day==-0&& t.ReleaseTime.Value.Month==DateTime.Now.Month&&t.ReleaseTime.Value.Year==DateTime.Now.Year).Count()
                }
            });
            //执法
            lines.Add(new DataLine
            {
                name = "执法",
                data = new List<int> {
                    moves.Where(t => t.PatrolTime.Value.Day-DateTime.Now.Day==-6&& t.PatrolTime.Value.Month==DateTime.Now.Month&&t.PatrolTime.Value.Year==DateTime.Now.Year).Count(),
                    moves.Where(t => t.PatrolTime.Value.Day-DateTime.Now.Day==-5&& t.PatrolTime.Value.Month==DateTime.Now.Month&&t.PatrolTime.Value.Year==DateTime.Now.Year).Count(),
                    moves.Where(t => t.PatrolTime.Value.Day-DateTime.Now.Day==-4&& t.PatrolTime.Value.Month==DateTime.Now.Month&&t.PatrolTime.Value.Year==DateTime.Now.Year).Count(),
                    moves.Where(t => t.PatrolTime.Value.Day-DateTime.Now.Day==-3&& t.PatrolTime.Value.Month==DateTime.Now.Month&&t.PatrolTime.Value.Year==DateTime.Now.Year).Count(),
                    moves.Where(t => t.PatrolTime.Value.Day-DateTime.Now.Day==-2&& t.PatrolTime.Value.Month==DateTime.Now.Month&&t.PatrolTime.Value.Year==DateTime.Now.Year).Count(),
                    moves.Where(t => t.PatrolTime.Value.Day-DateTime.Now.Day==-1&& t.PatrolTime.Value.Month==DateTime.Now.Month&&t.PatrolTime.Value.Year==DateTime.Now.Year).Count(),
                    moves.Where(t => t.PatrolTime.Value.Day-DateTime.Now.Day==-0&& t.PatrolTime.Value.Month==DateTime.Now.Month&&t.PatrolTime.Value.Year==DateTime.Now.Year).Count()
                }
            });
            return lines;
        }

        /// <summary>
        /// 获取最新视频
        /// </summary>
        /// <returns></returns>
        public Object GetNewVedioToday(Guid? Id)
        {
            IQueryable<EnterpriseVedio> vedios = Kily.Set<EnterpriseVedio>().Where(t => t.IsIndex).OrderByDescending(t => t.CreateTime);
            IQueryable<RepastVideo> videos = Kily.Set<RepastVideo>().Where(t => t.IsIndex).OrderByDescending(t => t.CreateTime);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
            {
                vedios = vedios.Where(t => t.TypePath.Contains(GovtInfo().City));
                videos = videos.Where(t => t.TypePath.Contains(GovtInfo().City));
            }
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<EnterpriseVedio, bool>> exp_1 = null;
                        Expression<Func<RepastVideo, bool>> exp_2 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<EnterpriseVedio>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_2 = ExpressionExtension.GetExpression<RepastVideo>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<EnterpriseVedio>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_2 = exp_2.Or(ExpressionExtension.GetExpression<RepastVideo>("TypePath", Areas[i], ExpressionEnum.Like));
                            }
                        }
                        vedios = vedios.Where(exp_1);
                        videos = videos.Where(exp_2);
                    }
                    else
                    {
                        vedios = vedios.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        videos = videos.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                    }
                }
                else
                {
                    vedios = vedios.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    videos = videos.Where(t => t.TypePath.Contains(GovtInfo().Area));
                }
            }
            int CompanyVedio = 0;
            int MerchantVedio = 0;
            if (Id.HasValue)
            {
                vedios = vedios.Where(t => t.CompanyId == Id.Value);
                videos = videos.Where(t => t.InfoId == Id.Value);
                CompanyVedio = vedios.Count();
                MerchantVedio = videos.Count();
            }
            else
            {
                CompanyVedio = vedios.Count();
                MerchantVedio = videos.Count();
            }
            if (!GovtInfo().IsEdu.Value)
            {
                var data = vedios.Select(t => new
                {
                    t.VedioAddr,
                    t.VedioName,
                    t.VedioCover
                }).Take(4).ToList();
                var datas = videos.Select(t => new
                {
                    VedioAddr = t.VideoAddress,
                    VedioName = t.MonitorAddress,
                    VedioCover = t.CoverPhoto
                }).Take(4).ToList();
                data.AddRange(datas);
                return new { Vedio = data, CompanyVedio, MerchantVedio };
            }
            else
            {
                var Ids = Kily.Set<RepastInfo>().Where(t => t.DiningType == MerchantEnum.UnitCanteen).Select(t => t.Id).ToList();
                var datas = videos.Where(t => Ids.Contains(t.InfoId)).Select(t => new
                {
                    VedioAddr = t.VideoAddress,
                    VedioName = t.MonitorAddress,
                    VedioCover = t.CoverPhoto
                }).Take(4).ToList();
                MerchantVedio = videos.Where(o => Ids.Contains(o.InfoId)).ToList().Count;
                return new { Vedio = datas, CompanyVedio, MerchantVedio };
            }
        }

        #endregion 新大屏

        #region 旧大屏

        /// <summary>
        /// 获取执行检查统计
        /// </summary>
        /// <returns></returns>
        public Object GetLawRank()
        {
            IQueryable<GovtNetPatrol> Patrol = Kily.Set<GovtNetPatrol>()
                .Where(t => t.CreateTime.Value.Year == DateTime.Now.Year);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                Patrol = Patrol.Where(t => t.TypePath.Contains(GovtInfo().City));
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<GovtNetPatrol, bool>> exp_1 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<GovtNetPatrol>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<GovtNetPatrol>("TypePath", Areas[i], ExpressionEnum.Like));
                            }
                        }
                        Patrol = Patrol.Where(exp_1);
                    }
                    else
                        Patrol = Patrol.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                }
                else
                    Patrol = Patrol.Where(t => t.TypePath.Contains(GovtInfo().Area));
            }
            return Patrol.GroupBy(t => t.CreateTime.Value.Month).Select(t => new
            {
                Month = t.Key,
                MonthCount = t.Count(),
                MonthAvg = t.Select(m => Convert.ToDouble(m.QualifiedNum)).Sum() / (t.Count() == 0 ? 1 : t.Count()),
                MonthPlain = t.Select(m => m.BulletinNum).Sum() / (t.Count() == 0 ? 1 : t.Count())
            }).ToList();
        }

        /// <summary>
        /// 统计数据
        /// </summary>
        /// <returns></returns>
        public Object GetCountNum()
        {
            var today = DateTime.Parse(DateTime.Now.ToShortDateString());
            var tomorrow = today.AddDays(1);
            IQueryable<EnterpriseInfo> com = Kily.Set<EnterpriseInfo>().Where(t => t.CreateTime >= today && t.CreateTime < tomorrow).AsNoTracking();
            IQueryable<RepastInfo> mer = Kily.Set<RepastInfo>().Where(t => t.CreateTime >= today && t.CreateTime < tomorrow).AsNoTracking();
            IQueryable<GovtRisk> risk = Kily.Set<GovtRisk>().Where(t => t.ReleaseTime >= today && t.ReleaseTime < tomorrow).AsNoTracking();
            IQueryable<GovtComplain> plain = Kily.Set<GovtComplain>().Where(t => t.ComplainTime >= today && t.ComplainTime < tomorrow).AsNoTracking();
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
            {
                com = com.Where(t => t.TypePath.Contains(GovtInfo().City));
                mer = mer.Where(t => t.TypePath.Contains(GovtInfo().City));
                risk = risk.Where(t => t.TypePath.Contains(GovtInfo().City));
                plain = plain.Where(t => t.TypePath.Contains(GovtInfo().City));
            }
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<EnterpriseInfo, bool>> exp_1 = null;
                        Expression<Func<RepastInfo, bool>> exp_2 = null;
                        Expression<Func<GovtRisk, bool>> exp_3 = null;
                        Expression<Func<GovtComplain, bool>> exp_4 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_2 = ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_3 = ExpressionExtension.GetExpression<GovtRisk>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_4 = ExpressionExtension.GetExpression<GovtComplain>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_2 = exp_2.Or(ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_3 = exp_3.Or(ExpressionExtension.GetExpression<GovtRisk>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_4 = exp_4.Or(ExpressionExtension.GetExpression<GovtComplain>("TypePath", Areas[i], ExpressionEnum.Like));
                            }
                        }
                        com = com.Where(exp_1);
                        mer = mer.Where(exp_2);
                        risk = risk.Where(exp_3);
                        plain = plain.Where(exp_4);
                    }
                    else
                    {
                        com = com.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        mer = mer.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        risk = risk.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        plain = plain.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                    }
                }
                else
                {
                    com = com.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    mer = mer.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    risk = risk.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    plain = plain.Where(t => t.TypePath.Contains(GovtInfo().Area));
                }
            }
            var week_risk = Kily.Set<GovtRisk>().Where(t => t.ReleaseTime >= today.AddDays(-7));
            var week_plain = Kily.Set<GovtComplain>().Where(t => t.ComplainTime >= today.AddDays(-7));
            double Rw = (risk.Count() * 1.0) / (week_risk.Count() == 0 ? 1 : week_risk.Count());
            double Pw = (plain.Count() * 1.0) / (week_plain.Count() == 0 ? 1 : week_plain.Count());
            var data = new { Com = com.Count(), Mer = mer.Count(), Risk = risk.Count(), Plain = plain.Count(), Rw, Pw };
            return data;
        }

        /// <summary>
        /// 区域入住分布排行
        /// </summary>
        /// <returns></returns>
        public IList<ResponseGovtRanking> GetAreaRank()
        {
            ResponseCity City = Kily.Set<SystemCity>().Where(t => t.Id.ToString() == GovtInfo().City).Select(t => new ResponseCity
            {
                CityId = t.Code,
                CityName = t.Name
            }).FirstOrDefault();
            List<ResponseArea> Area = Kily.Set<SystemArea>().Where(t => t.CityCode == City.CityId).AsNoTracking().Select(t => new ResponseArea
            {
                Id = t.Id,
                AreaName = t.Name
            }).ToList();
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false);
            IQueryable<RepastInfo> queryables = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false);
            List<ResponseGovtRanking> data = new List<ResponseGovtRanking>();
            Area.ForEach(t =>
            {
                int TotalCompany = queryable.Where(x => x.TypePath.Contains(t.Id.ToString())).Select(x => x.Id).Count();
                int TotalMerchant = queryables.Where(x => x.TypePath.Contains(t.Id.ToString())).Select(x => x.Id).Count();
                data.Add(new ResponseGovtRanking { AreaName = t.AreaName, TotalCount = TotalCompany + TotalMerchant });
            });
            return data.OrderByDescending(t => t.TotalCount).Take(10).ToList();
        }

        #endregion 旧大屏

        #region 首页统计

        /// <summary>
        /// 投诉占比
        /// </summary>
        /// <returns></returns>
        public Object GetComplainDataRatio()
        {
            IQueryable<GovtComplain> queryable = Kily.Set<GovtComplain>().AsNoTracking();
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<GovtComplain, bool>> exp = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                                exp = ExpressionExtension.GetExpression<GovtComplain>("TypePath", Areas[i], ExpressionEnum.Like);
                            else
                                exp = exp.Or(ExpressionExtension.GetExpression<GovtComplain>("TypePath", Areas[i], ExpressionEnum.Like));
                        }
                        queryable = queryable.Where(exp);
                    }
                    else
                        queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                }
                else
                    queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
            }
            //计算周投诉
            int WG = queryable.Where(t => t.CreateTime >= DateTime.Now.AddDays(-7)).Count();
            decimal WGSum = Math.Round((WG / (queryable.Count() == 0 ? 1 : queryable.Count())) * 100M, 2);
            //计算月投诉
            int MG = queryable.Where(t => t.CreateTime >= DateTime.Now.AddMonths(-1)).Count();
            decimal MGSum = Math.Round((MG / (queryable.Count() == 0 ? 1 : queryable.Count())) * 100M, 2);
            //计算总数
            int Total = WG + MG;
            decimal Sum = WGSum + MGSum;
            return new { WG, WGSum, MG, MGSum, Total, Sum };
        }

        /// <summary>
        /// 板块占比
        /// </summary>
        /// <returns></returns>
        public Object GetComDataRatio()
        {
            IQueryable<EnterpriseInfo> Enterprise = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false).Where(t => t.AuditType == AuditEnum.AuditSuccess);
            IQueryable<RepastInfo> Merchant = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false).Where(t => t.AuditType == AuditEnum.AuditSuccess);
            IQueryable<CookBanquet> Banquet = Kily.Set<CookBanquet>();
            IQueryable<GovtNetPatrol> Patrol = Kily.Set<GovtNetPatrol>();
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
            {
                //外环
                Merchant = Merchant.Where(t => t.TypePath.Contains(GovtInfo().City));
                //内环
                Enterprise = Enterprise.Where(t => t.TypePath.Contains(GovtInfo().City));
                Banquet = Banquet.Where(t => t.TypePath.Contains(GovtInfo().City));
                Patrol = Patrol.Where(t => t.TypePath.Contains(GovtInfo().City));
            }
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<EnterpriseInfo, bool>> exp_1 = null;
                        Expression<Func<RepastInfo, bool>> exp_2 = null;
                        Expression<Func<CookBanquet, bool>> exp_3 = null;
                        Expression<Func<GovtNetPatrol, bool>> exp_4 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_2 = ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_3 = ExpressionExtension.GetExpression<CookBanquet>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_4 = ExpressionExtension.GetExpression<GovtNetPatrol>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_2 = exp_2.Or(ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_3 = exp_3.Or(ExpressionExtension.GetExpression<CookBanquet>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_4 = exp_4.Or(ExpressionExtension.GetExpression<GovtNetPatrol>("TypePath", Areas[i], ExpressionEnum.Like));
                            }
                        }
                        //外环
                        Merchant = Merchant.Where(exp_2);
                        //内环
                        Enterprise = Enterprise.Where(exp_1);
                        Banquet = Banquet.Where(exp_3);
                        Patrol = Patrol.Where(exp_4);
                    }
                    else
                    {
                        //外环
                        Merchant = Merchant.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        //内环
                        Enterprise = Enterprise.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        Banquet = Banquet.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        Patrol = Patrol.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                    }
                }
                else
                {
                    //外环
                    Merchant = Merchant.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    //内环
                    Enterprise = Enterprise.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    Banquet = Banquet.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    Patrol = Patrol.Where(t => t.TypePath.Contains(GovtInfo().Area));
                }
            }
            //月入住
            var CompanyTotal = (Enterprise.Count() + Merchant.Count()) == 0 ? 1 : (Enterprise.Count() + Merchant.Count());
            int CMSum = Enterprise.Where(t => t.CreateTime >= DateTime.Now.AddMonths(-1)).Count() + Merchant.Where(t => t.CreateTime >= DateTime.Now.AddMonths(-1)).Count();
            decimal CMTotal = Math.Round((CMSum / CompanyTotal) * 100M, 2);
            //月产品总数
            IQueryable<EnterpriseGoods> goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false);
            int CGSum = goods.Join(Enterprise, t => t.CompanyId, x => x.Id, (t, x) => new { t.CreateTime }).Where(t => t.CreateTime >= DateTime.Now.AddMonths(-1)).Count();
            var TotalPro = goods.Join(Enterprise, t => t.CompanyId, x => x.Id, (t, x) => new { t.CreateTime }).Count();
            decimal CGTotal = Math.Round((CGSum / (TotalPro == 0 ? 1 : TotalPro)) * 100M, 2);
            //月移动执法总数
            //IQueryable<GovtMovePatrol> MovePatrol = Kily.Set<GovtMovePatrol>().Where(t => t.GovtId == GovtInfo().Id);
            //int CPSum = MovePatrol.Where(t => t.CreateTime >= DateTime.Now.AddMonths(-1)).Count();
            //decimal CPTotal = Math.Round((CPSum / (MovePatrol.Count() == 0 ? 1 : MovePatrol.Count())) * 100M, 2);
            int CPSum = Patrol.Where(t => t.CreateTime >= DateTime.Now.AddMonths(-1)).Count();
            decimal CPTotal = Math.Round((CPSum / (Patrol.Count() == 0 ? 1 : Patrol.Count())) * 100M, 2);
            //月群宴数
            int CBSum = Banquet.Where(t => t.CreateTime >= DateTime.Now.AddMonths(-1)).Count();
            decimal CBTotal = Math.Round((CBSum / (Banquet.Count() == 0 ? 1 : Banquet.Count())) * 100M, 2);
            return new { CMSum, CMTotal, CGSum, CGTotal, CPSum, CPTotal, CBSum, CBTotal, CompanyTotal, TotalPro, PtTotal = Patrol.Count(), BqTotal = Banquet.Count() };
        }

        /// <summary>
        /// 投诉折线图
        /// </summary>
        /// <returns></returns>
        public Object GetComplainLine()
        {
            IQueryable<GovtComplain> queryable = Kily.Set<GovtComplain>().AsNoTracking();
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<GovtComplain, bool>> exp = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                                exp = ExpressionExtension.GetExpression<GovtComplain>("TypePath", Areas[i], ExpressionEnum.Like);
                            else
                                exp = exp.Or(ExpressionExtension.GetExpression<GovtComplain>("TypePath", Areas[i], ExpressionEnum.Like));
                        }
                        queryable = queryable.Where(exp);
                    }
                    else
                        queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                }
                else
                    queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
            }
            List<GovtComplain> ZZ_Com = null;
            List<GovtComplain> YZ_Com = null;
            List<GovtComplain> SC_Com = null;
            List<GovtComplain> LT_Com = null;
            if (!GovtInfo().IsEdu.Value)
            {
                //分组
                ZZ_Com = queryable.Where(t => t.CompanyType.Equals("种植企业")).OrderByDescending(t => t.CreateTime).ToList();
                YZ_Com = queryable.Where(t => t.CompanyType.Equals("养殖企业")).OrderByDescending(t => t.CreateTime).ToList();
                SC_Com = queryable.Where(t => t.CompanyType.Equals("生产企业")).OrderByDescending(t => t.CreateTime).ToList();
                LT_Com = queryable.Where(t => t.CompanyType.Equals("流通企业")).OrderByDescending(t => t.CreateTime).ToList();
            }
            else
            {
                //分组
                ZZ_Com = queryable.Where(t => t.CompanyType.Equals("学前教育")).OrderByDescending(t => t.CreateTime).ToList();
                YZ_Com = queryable.Where(t => t.CompanyType.Equals("义务教育")).OrderByDescending(t => t.CreateTime).ToList();
                SC_Com = queryable.Where(t => t.CompanyType.Equals("高中教育")).OrderByDescending(t => t.CreateTime).ToList();
                LT_Com = queryable.Where(t => t.CompanyType.Equals("高等教育")).OrderByDescending(t => t.CreateTime).ToList();
            }
            var ZYJY = queryable.Where(t => t.CompanyType.Equals("职业教育")).OrderByDescending(t => t.CreateTime).ToList();
            //时间分组

            #region 种植

            //近3天投诉
            var Z3 = ZZ_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                 .Where(t => DateTime.Now.Day - t.CreateTime.Value.Day <= 3).Count();
            //近7天投诉
            var Z7 = ZZ_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Day - t.CreateTime.Value.Day <= 7).Count();
            //近15天投诉
            var Z15 = ZZ_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Day - t.CreateTime.Value.Day <= 15).Count();
            //近30天投诉
            var Z30 = ZZ_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Month - t.CreateTime.Value.Month <= 1).Count();
            //近180天投诉
            var Z180 = ZZ_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Month - t.CreateTime.Value.Month <= 6).Count();
            //近365天投诉
            var Z365 = ZZ_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Year - t.CreateTime.Value.Year <= 1).Count();

            #endregion 种植

            #region 养殖

            //近3天投诉
            var Y3 = YZ_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Day - t.CreateTime.Value.Day <= 3).Count();
            //近7天投诉
            var Y7 = YZ_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Day - t.CreateTime.Value.Day <= 7).Count();
            //近15天投诉
            var Y15 = YZ_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Day - t.CreateTime.Value.Day <= 15).Count();
            //近30天投诉
            var Y30 = YZ_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Month - t.CreateTime.Value.Month <= 1).Count();
            //近180天投诉
            var Y180 = YZ_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Month - t.CreateTime.Value.Month <= 6).Count();
            //近365天投诉
            var Y365 = YZ_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Year - t.CreateTime.Value.Year <= 1).Count();

            #endregion 养殖

            #region 生产

            //近3天投诉
            var S3 = SC_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Day - t.CreateTime.Value.Day <= 3).Count();
            //近7天投诉
            var S7 = SC_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Day - t.CreateTime.Value.Day <= 7).Count();
            //近15天投诉
            var S15 = SC_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Day - t.CreateTime.Value.Day <= 15).Count();
            //近30天投诉
            var S30 = SC_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Month - t.CreateTime.Value.Month <= 1).Count();
            //近180天投诉
            var S180 = SC_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Month - t.CreateTime.Value.Month <= 6).Count();
            //近365天投诉
            var S365 = SC_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Year - t.CreateTime.Value.Year <= 1).Count();

            #endregion 生产

            #region 流通

            //近3天投诉
            var L3 = LT_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Day - t.CreateTime.Value.Day <= 3).Count();
            //近7天投诉
            var L7 = LT_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Day - t.CreateTime.Value.Day <= 7).Count();
            //近15天投诉
            var L15 = LT_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Day - t.CreateTime.Value.Day <= 15).Count();
            //近30天投诉
            var L30 = LT_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Month - t.CreateTime.Value.Month <= 1).Count();
            //近180天投诉
            var L180 = LT_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Month - t.CreateTime.Value.Month <= 6).Count();
            //近365天投诉
            var L365 = LT_Com.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Year - t.CreateTime.Value.Year <= 1).Count();

            #endregion 流通

            #region 流通

            //近3天投诉
            var ZY3 = ZYJY.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Day - t.CreateTime.Value.Day <= 3).Count();
            //近7天投诉
            var ZY7 = ZYJY.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Day - t.CreateTime.Value.Day <= 7).Count();
            //近15天投诉
            var ZY15 = ZYJY.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Day - t.CreateTime.Value.Day <= 15).Count();
            //近30天投诉
            var ZY30 = ZYJY.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Month - t.CreateTime.Value.Month <= 1).Count();
            //近180天投诉
            var ZY180 = ZYJY.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Month - t.CreateTime.Value.Month <= 6).Count();
            //近365天投诉
            var ZY365 = ZYJY.Where(t => DateTime.Now.Day - t.CreateTime.Value.Day >= 1)
                .Where(t => DateTime.Now.Year - t.CreateTime.Value.Year <= 1).Count();

            #endregion 流通

            List<int> ZCom = new List<int> { Z3, Z7, Z15, Z30, Z180, Z365 };
            List<int> YCom = new List<int> { Y3, Y7, Y15, Y30, Y180, Y365 };
            List<int> SCom = new List<int> { S3, S7, S15, S30, S180, S365 };
            List<int> LCom = new List<int> { L3, L7, L15, L30, L180, L365 };
            List<int> ZYJYS = new List<int> { ZY3, ZY7, ZY15, ZY30, ZY180, ZY365 };
            if (!GovtInfo().IsEdu.Value)
                return new { ZCom, YCom, SCom, LCom };
            else
                return new { ZCom, YCom, SCom, LCom, ZYJYS };
        }

        #endregion 首页统计

        #endregion 数据统计

        #region 责任协议

        /// <summary>
        /// 协议分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtAgree> GetAgreePage(PageParamList<RequestGovtAgree> pageParam)
        {
            IQueryable<GovtAgree> queryable = Kily.Set<GovtAgree>().Where(t => t.GovtId == GovtInfo().Id).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.Title))
                queryable = queryable.Where(t => t.Title.Contains(pageParam.QueryParam.Title));
            var data = queryable.Select(t => new ResponseGovtAgree()
            {
                Id = t.Id,
                Title = t.Title
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 编辑协议
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditAgree(RequestGovtAgree Param)
        {
            GovtAgree agree = Param.MapToEntity<GovtAgree>();
            return Insert(agree) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        /// <summary>
        /// 协议详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseGovtAgree GetAgreeDetail(Guid Id)
        {
            return Kily.Set<GovtAgree>().Where(t => t.Id == Id).Select(t => new ResponseGovtAgree()
            {
                Title = t.Title,
                AgreeConent = t.AgreeConent,
            }).AsNoTracking().FirstOrDefault();
        }

        /// <summary>
        /// 删除协议
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveAgree(Guid Id)
        {
            return Remove<GovtAgree>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        #endregion 责任协议

        #region App统计

        public Object GetAppTodayCount()
        {
            IQueryable<GovtRisk> risks = Kily.Set<GovtRisk>().Where(t => t.IsDelete == false);
            IQueryable<GovtComplain> complains = Kily.Set<GovtComplain>();
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
            {
                risks = risks.Where(t => t.TypePath.Contains(GovtInfo().City));
                complains = complains.Where(t => t.TypePath.Contains(GovtInfo().City));
            }
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                        foreach (var item in Areas)
                        {
                            risks = risks.Where(t => t.TypePath.Contains(item));
                            complains = complains.Where(t => t.TypePath.Contains(item));
                        }
                    else
                    {
                        risks = risks.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        complains = complains.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                    }
                }
                else
                {
                    risks = risks.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    complains = complains.Where(t => t.TypePath.Contains(GovtInfo().Area));
                }
            }
            List<DataBar> bars = new List<DataBar>();
            //风险
            var RiskCount = risks.Where(t => t.ReleaseTime.Value.Day - DateTime.Now.Day == 0).Count();
            //投诉
            var ComplainCount = complains.Where(t => t.ComplainTime.Value.Day - DateTime.Now.Day == 0).Count();
            IQueryable<GovtTemplateChild> children = Kily.Set<GovtTemplateChild>().Where(t => t.IsDelete == false);
            IQueryable<GovtNetPatrol> patrols = Kily.Set<GovtNetPatrol>().Where(t => t.IsDelete == false);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
            {
                children = children.Where(t => t.TypePath.Contains(GovtInfo().City));
                patrols = patrols.Where(t => t.TypePath.Contains(GovtInfo().City));
            }
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                        foreach (var item in Areas)
                        {
                            children = children.Where(t => t.TypePath.Contains(item));
                            patrols = patrols.Where(t => t.TypePath.Contains(GovtInfo().City));
                        }
                    else
                    {
                        children = children.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        patrols = patrols.Where(t => t.TypePath.Contains(GovtInfo().City));
                    }
                }
                else
                {
                    children = children.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    patrols = patrols.Where(t => t.TypePath.Contains(GovtInfo().City));
                }
            }
            List<DataLine> lines = new List<DataLine>();
            //自查
            var SelfCount = children.Where(t => t.CreateTime.Value.Day - DateTime.Now.Day == 0).Count();
            //抽查
            var PatrolsCount = patrols.Where(t => t.CreateTime.Value.Day - DateTime.Now.Day == 0).Sum(t => t.PotrolNum);
            //通报
            var BadCount = patrols.Where(t => t.CreateTime.Value.Day - DateTime.Now.Day == 0).Sum(t => t.BulletinNum);

            IQueryable<CookBanquet> queryable = Kily.Set<CookBanquet>().OrderByDescending(t => t.CreateTime);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                        foreach (var item in Areas)
                        {
                            queryable = queryable.Where(t => t.TypePath.Contains(item));
                        }
                    else
                        queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                }
                else
                    queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
            }
            //群宴
            var PartyCounts = queryable.Where(t => t.CreateTime.Value.Day - DateTime.Now.Day == 0).Count();
            return new { RiskCount = RiskCount, ComplainCount = ComplainCount, SelfCount = SelfCount, PatrolsCount = PatrolsCount, BadCount = BadCount, PartyCounts = PartyCounts, Percents = (BadCount * 100 / (PatrolsCount == 0 ? 1 : PatrolsCount)) + "%" };
        }

        #endregion App统计

        #region 操作日志

        /// <summary>
        /// 获取操作日志
        /// </summary>
        /// <returns></returns>
        public List<ResponseSystemLogInfo> GetLogInfos()
        {
            IQueryable<SystemLogInfo> queryable = Kily.Set<SystemLogInfo>().Where(o => o.Status != "已读");
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<SystemLogInfo, bool>> exp_1 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                                exp_1 = ExpressionExtension.GetExpression<SystemLogInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                            else
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<SystemLogInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                        }
                        queryable = queryable.Where(exp_1);
                    }
                    else
                        queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                }
                else
                    queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
            }
            return queryable.ToList().MapToList<SystemLogInfo, ResponseSystemLogInfo>();
        }
        /// <summary>
        /// 日志列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseSystemLogInfo> GetHandlerLogPage(PageParamList<RequestSystemLogInfo> pageParam)
        {
            IQueryable<SystemLogInfo> queryable = Kily.Set<SystemLogInfo>();
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<SystemLogInfo, bool>> exp_1 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                                exp_1 = ExpressionExtension.GetExpression<SystemLogInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                            else
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<SystemLogInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                        }
                        queryable = queryable.Where(exp_1);
                    }
                    else
                        queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                }
                else
                    queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
            }
            if (!string.IsNullOrEmpty(pageParam.QueryParam.HandlerType))
                queryable = queryable.Where(t => t.HandlerType.Contains(pageParam.QueryParam.HandlerType));
            var data = queryable.OrderByDescending(x => x.HandlerTime).ToList().MapToList<SystemLogInfo, ResponseSystemLogInfo>().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 批量阅读
        /// </summary>
        /// <param name="Keys"></param>
        public string EditHandlerLog(List<Guid> Keys, bool flag)
        {
            var Info = Kily.Set<SystemLogInfo>().Where(t => Keys.Contains(t.Id)).ToList();
            foreach (var item in Info)
            {
                if (flag)
                {
                    item.Status = "已读";
                    UpdateField(item, "Status");
                }
                else
                {
                    var CompanyType = Kily.Set<EnterpriseInfo>().Where(t => t.CompanyName.Contains(item.HandlerUser)).Select(t => AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType)).FirstOrDefault();
                    var MerchantType = Kily.Set<RepastInfo>().Where(t => t.MerchantName.Contains(item.HandlerUser)).Select(t => AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType)).FirstOrDefault();
                    SystemMessage message = new SystemMessage
                    {
                        TypePath = item.TypePath,
                        ReleaseTime = DateTime.Now,
                        MsgName = "操作日志",
                        MsgContent = item.HandlerContent,
                        TrageType = string.IsNullOrEmpty(CompanyType)? MerchantType: CompanyType,
                        Category = "常规"
                    };
                    Insert(message);
                    item.Open = "已广播";
                    UpdateField(item, "Open");
                }
            }
            return ServiceMessage.UPDATESUCCESS;
        }

        #endregion 操作日志

        #region  预警提示

        #endregion

        #region 综合统计
        /// <summary>
        /// 区域综合
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object GetAreaShow(string name, string type)
        {
            IQueryable<EnterpriseInfo> Info = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false);
            IQueryable<RepastInfo> Infos = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
            {
                Info = Info.Where(t => t.TypePath.Contains(GovtInfo().City));
                Infos = Infos.Where(t => t.TypePath.Contains(GovtInfo().City));
            }
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<EnterpriseInfo, bool>> exp_1 = null;
                        Expression<Func<RepastInfo, bool>> exp_2 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_2 = ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_2 = ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                        }
                        Info = Info.Where(exp_1);
                        Infos = Infos.Where(exp_2);
                    }
                    else
                    {
                        Info = Info.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        Infos = Infos.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                    }
                }
                else
                {
                    Info = Info.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    Infos = Infos.Where(t => t.TypePath.Contains(GovtInfo().Area));
                }
            }
            if (!GovtInfo().IsEdu.Value)
            {
                var Einfo = Info.Select(t => new ResponseEnterprise()
                {
                    CompanyId = t.Id,
                    CompanyName = t.CompanyName,
                    TypePath = Kily.Set<SystemArea>().Where(x => x.Id.ToString() == GovtInfo().Area).FirstOrDefault().Name,
                    SafeOffer = t.SafeOffer,
                    CommunityCode = t.CommunityCode,
                    CompanyAddress = t.CompanyAddress,
                    CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType),
                    CompanyPhone = t.CompanyPhone
                }).ToList();
                var Rinfo = Infos.Select(t => new ResponseEnterprise()
                {
                    CompanyId = t.Id,
                    CompanyName = t.MerchantName,
                    TypePath = Kily.Set<SystemArea>().Where(x => x.Id.ToString() == GovtInfo().Area).FirstOrDefault().Name,
                    SafeOffer = t.SafeOffer,
                    CommunityCode = t.CommunityCode,
                    CompanyAddress = t.Address,
                    CompanyTypeName = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType),
                    CompanyPhone = t.Phone
                }).ToList();
                Einfo.AddRange(Rinfo);
                if (!string.IsNullOrEmpty(name))
                    return Einfo.Where(t => t.CompanyName.Contains(name)).ToList();
                if (!string.IsNullOrEmpty(type))
                    return Einfo.Where(t => t.CompanyTypeName.Contains(type)).ToList();
                return Einfo;
            }
            else
            {

                var Einfo = Infos.Where(o => o.DiningType == MerchantEnum.UnitCanteen).Select(t => new ResponseEnterprise()
                {
                    CompanyId = t.Id,
                    CompanyName = t.MerchantName,
                    TypePath = Kily.Set<SystemArea>().Where(x => x.Id.ToString() == GovtInfo().Area).FirstOrDefault().Name,
                    SafeOffer = t.SafeOffer,
                    CommunityCode = t.CommunityCode,
                    CompanyAddress = t.Address,
                    CompanyTypeName = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType),
                    CompanyPhone = t.Phone
                }).ToList();
                if (!string.IsNullOrEmpty(name))
                    return Einfo.Where(t => t.CompanyName.Contains(name)).ToList();
                if (!string.IsNullOrEmpty(type))
                    return Einfo.Where(t => t.CompanyTypeName.Contains(type)).ToList();
                return Einfo;
            }

        }
        /// <summary>
        /// 企业巡查
        /// </summary>
        /// <param name="type"></param>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <returns></returns>
        public object GetAreaBill(string type, string name)
        {
            var einfo = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false);
            var rinfo = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false);
            var temp = Kily.Set<GovtTemplateChild>().Where(t => t.IsDelete == false);
            var web = Kily.Set<GovtNetPatrol>().Where(t => t.IsDelete == false);
            var app = Kily.Set<GovtMovePatrol>().Where(t => t.IsDelete == false);
            var plain = Kily.Set<GovtComplain>().Where(t => t.IsDelete == false);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
            {
                einfo = einfo.Where(t => t.TypePath.Contains(GovtInfo().City));
                rinfo = rinfo.Where(t => t.TypePath.Contains(GovtInfo().City));
            }
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<EnterpriseInfo, bool>> exp_1 = null;
                        Expression<Func<RepastInfo, bool>> exp_2 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                                exp_2 = ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                                exp_2 = ExpressionExtension.GetExpression<RepastInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                        }
                        einfo = einfo.Where(exp_1);
                        rinfo = rinfo.Where(exp_2);
                    }
                    else
                    {
                        einfo = einfo.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                        rinfo = rinfo.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                    }
                }
                else
                {
                    einfo = einfo.Where(t => t.TypePath.Contains(GovtInfo().Area));
                    rinfo = rinfo.Where(t => t.TypePath.Contains(GovtInfo().Area));
                }
            }
            if (!GovtInfo().IsEdu.Value)
            {
                var data1 = einfo.Select(t => new
                {
                    CompanyName = t.CompanyName,
                    CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType),
                    Temp = temp.Where(x => x.CompanyName == t.CompanyName).Count(),
                    Web = web.Where(x => x.CompanyId == t.Id).Select(x => x.PotrolNum).Sum(),
                    App = app.Where(x => x.CompanyId == t.Id).Count(),
                    Risk = Kily.Set<SystemMessage>().Where(x => x.CompanyId == t.Id).Where(x => x.MsgName == "证件到期提醒").Count(),
                    Plain = plain.Where(x => x.CompanyId == t.Id).Count(),
                    Back = web.Where(x => x.CompanyId == t.Id).Select(x => x.BulletinNum).Sum(),
                }).ToList();
                var data2 = rinfo.Select(t => new
                {
                    CompanyName = t.MerchantName,
                    CompanyTypeName = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType),
                    Temp = temp.Where(x => x.CompanyName == t.MerchantName).Count(),
                    Web = web.Where(x => x.CompanyId == t.Id).Select(x => x.PotrolNum).Sum(),
                    App = app.Where(x => x.CompanyId == t.Id).Count(),
                    Risk = Kily.Set<SystemMessage>().Where(x => x.CompanyId == t.Id).Where(x => x.MsgName == "证件到期提醒").Count(),
                    Plain = plain.Where(x => x.CompanyId == t.Id).Count(),
                    Back = web.Where(x => x.CompanyId == t.Id).Select(x => x.BulletinNum).Sum(),
                }).ToList();
                data1.AddRange(data2);
                if (!string.IsNullOrEmpty(name))
                    return data1.Where(t => t.CompanyName.Contains(name)).ToList();
                if (!string.IsNullOrEmpty(type))
                    return data1.Where(t => t.CompanyName.Contains(type)).ToList();
                return data1;
            }
            else
            {

                var data1 = rinfo.Where(o => o.DiningType == MerchantEnum.UnitCanteen).Select(t => new
                {
                    CompanyName = t.MerchantName,
                    CompanyTypeName = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType),
                    Temp = temp.Where(x => x.CompanyName == t.MerchantName).Count(),
                    Web = web.Where(x => x.CompanyId == t.Id).Select(x => x.PotrolNum).Sum(),
                    App = app.Where(x => x.CompanyId == t.Id).Count(),
                    Risk = Kily.Set<SystemMessage>().Where(x => x.CompanyId == t.Id).Where(x => x.MsgName == "证件到期提醒").Count(),
                    Plain = plain.Where(x => x.CompanyId == t.Id).Count(),
                    Back = web.Where(x => x.CompanyId == t.Id).Select(x => x.BulletinNum).Sum(),
                }).ToList();
                if (!string.IsNullOrEmpty(name))
                    return data1.Where(t => t.CompanyName.Contains(name)).ToList();
                if (!string.IsNullOrEmpty(type))
                    return data1.Where(t => t.CompanyName.Contains(type)).ToList();
                return data1;
            }
        }
        /// <summary>
        /// 所有产品
        /// </summary>
        /// <param name="ProName"></param>
        /// <returns></returns>
        public object GetAllPro(string ProName)
        {
            var info = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false);
            var good = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false);
            var stock = Kily.Set<EnterpriseGoodsStock>().Where(t => t.IsDelete == false);
            var stocks = Kily.Set<EnterpriseGoodsStockAttach>().Where(t => t.IsDelete == false);
            var plain = Kily.Set<GovtComplain>().Where(t => t.IsDelete == false);
            var code = Kily.Set<EnterpriseScanCodeInfo>().Where(t => t.IsDelete == false);
            var Logs = Kily.Set<EnterpriseLogistics>().Where(t => t.IsDelete == false);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                info = info.Where(t => t.TypePath.Contains(GovtInfo().City));
            else
            {
                IList<string> Areas = GetDepartArea();
                if (Areas != null)
                {
                    if (Areas.Count > 1)
                    {
                        Expression<Func<EnterpriseInfo, bool>> exp_1 = null;
                        for (int i = 0; i < Areas.Count; i++)
                        {
                            if (i == 0)
                            {
                                exp_1 = ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like);
                            }
                            else
                            {
                                exp_1 = exp_1.Or(ExpressionExtension.GetExpression<EnterpriseInfo>("TypePath", Areas[i], ExpressionEnum.Like));
                            }
                        }
                        info = info.Where(exp_1);
                    }
                    else
                        info = info.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                }
                else
                    info = info.Where(t => t.TypePath.Contains(GovtInfo().Area));
            }
            var data = info.Join(good, t => t.Id, x => x.CompanyId, (t, x) => new { t, x }).Select(x => new
            {
                Id = x.x.Id,
                CompanyId = x.x.CompanyId,
                x.t.CompanyName,
                x.x.ProductName,
                x.x.Spec,
                x.x.ProductType,
                Plian = plain.Where(t => t.CompanyId == x.t.Id).Where(t => t.ProductName == x.x.ProductName).Count(),
                Lost = stock.Join(stocks, m => m.Id, n => n.StockId, (m, n) => new { m.GoodsId, m.InStockNum }).Where(t => t.GoodsId == x.x.Id).Select(t => t.InStockNum).Sum(),
                Sell = stock.Join(stocks, m => m.Id, n => n.StockId, (m, n) => new { n.OutStockNum, m.GoodsId }).Where(t => t.GoodsId == x.x.Id).Select(t => t.OutStockNum).Sum(),
                Scan = code.Join(Logs, a => a.TakeCarId, b => b.Id, (a, b) => new { b.GoodsName, a.ScanNum }).Join(good, c => c.GoodsName, d => d.ProductName, (c, d) => new { c.ScanNum, d.CompanyId }).Where(t => t.CompanyId == x.t.Id).Select(t => t.ScanNum).Sum()
            }).ToList();
            if (!string.IsNullOrEmpty(ProName))
                return data.Where(t => t.ProductName.Contains(ProName)).ToList();
            return data;
        }
        #endregion

        #region 台账管理

        /// <summary>
        /// 进销台账(日期范围内)
        /// </summary>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public Object GetTickPrint(Dictionary<String, String> pairs)
        {
            var Id = Guid.Parse(pairs["Id"]);
            var CompanyType = (CompanyEnum)Convert.ToInt32(pairs["CompanyType"]);
            DateTime? SearchTime = null;//结束日期
            DateTime? StartTime = null;//开始日期
            if (pairs.ContainsKey("Date"))
                SearchTime = DateTime.Parse(pairs["Date"]);
            else
                SearchTime = DateTime.Parse(DateTime.Now.ToShortDateString());
            if (pairs.ContainsKey("SDate"))
                StartTime = DateTime.Parse(pairs["SDate"]);
            else
                StartTime = DateTime.Parse(DateTime.Now.ToShortDateString());
            List<EnterpriseGoods> Goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false).Where(t => t.CompanyId == Id).ToList();
            List<EnterpriseGoodsStock> Stocks = Kily.Set<EnterpriseGoodsStock>().Where(t => t.IsDelete == false).Where(t => t.CompanyId == Id).ToList();
            List<EnterpriseBuyer> Buyers = Kily.Set<EnterpriseBuyer>().Where(t => t.IsDelete == false).Where(t => t.CompanyId == Id).ToList();
            List<EnterpriseGoodsStockAttach> StockAttach = Kily.Set<EnterpriseGoodsStockAttach>().Where(t => t.IsDelete == false).Where(t => t.CompanyId == Id).ToList();
            List<EnterpriseCheckGoods> Checks = Kily.Set<EnterpriseCheckGoods>().Where(t => t.IsDelete == false).Where(t => t.CompanyId == Id).ToList();
            List<EnterpriseLogistics> Logistics = Kily.Set<EnterpriseLogistics>().Where(t => t.IsDelete == false).Where(t => t.CompanyId == Id).ToList();
            List<EnterpriseGoodsPackage> Packs = Kily.Set<EnterpriseGoodsPackage>().Where(t => t.IsDelete == false).Where(t => t.CompanyId == Id).ToList();
            var Temp = Goods.Join(Stocks, t => t.Id, x => x.GoodsId, (t, x) => new { t.ProductName, t.Spec, t.Unit, x.InStockNum, x.Id, x.CheckGoodsId, x.BuyId })
                 .Join(StockAttach, x => x.Id, t => t.StockId, (x, t) => new { x, t.OutStockNum, t.GoodsBatchNo, t.OutStockTime, t.OutStockUser })
                 .Join(Checks, t => t.x.CheckGoodsId, x => x.Id, (t, x) => new { t, x.CheckResult }).ToList();
            if (Packs.Count == 0)
            {
                var Temps = Temp.GroupJoin(Logistics, n => n.t.x.ProductName, m => m.GoodsName, (n, m) => new { n, GainUser = (m.FirstOrDefault() == null ? "" : m.FirstOrDefault().GainUser) }).ToList();
                if (CompanyEnum.Circulation == CompanyType)
                {
                    var sell = Temps.Join(Buyers, o => o.n.t.x.BuyId, y => y.Id, (o, y) => new { o.n.t.x.ProductName, o.n.t.x.Spec, o.n.t.x.Unit, y.Num, BatchNo = o.n.t.GoodsBatchNo, Supplier = "", Time = o.n.t.OutStockTime, o.n.t.OutStockUser, o.n.CheckResult, Seller = o.GainUser })
                        .Where(t => t.Time <= SearchTime && t.Time >= StartTime).ToList();
                    var buy = Temps.Join(Buyers, o => o.n.t.x.BuyId, y => y.Id, (o, y) => new { o.n.t.x.ProductName, o.n.t.x.Spec, o.n.t.x.Unit, y.Num, y.BatchNo, y.Supplier, Time = y.GetGoodsTime.Value, o.n.t.OutStockUser, o.n.CheckResult, Seller = "" })
                        .Where(t => t.Time <= SearchTime && t.Time >= StartTime).ToList();
                    sell.AddRange(buy);
                    return sell;
                }
                else
                    return Temps.Select(o => new { o.n.t.x.ProductName, o.n.t.x.Spec, o.n.t.x.Unit, Num = o.n.t.OutStockNum, BatchNo = o.n.t.GoodsBatchNo, Supplier = "", Time = o.n.t.OutStockTime, o.n.t.OutStockUser, o.n.CheckResult, Seller = o.GainUser })
                        .Where(t => t.Time <= SearchTime && t.Time >= StartTime).ToList();
            }
            else
            {
                var Temps = Temp.GroupJoin(Packs, a => a.t.GoodsBatchNo, b => b.ProductOutStockNo, (a, b) => new { a, b.FirstOrDefault()?.PackageNo })
                    .GroupJoin(Logistics, n => n.a.t.x.ProductName, m => m.GoodsName, (n, m) => new { n, GainUser = (m.FirstOrDefault() == null ? "" : m.FirstOrDefault().GainUser) }).ToList();
                if (CompanyEnum.Circulation == CompanyType)
                    return Temps.Join(Buyers, o => o.n.a.t.x.BuyId, y => y.Id, (o, y) => new { o.n.a.t.x.ProductName, o.n.a.t.x.Spec, o.n.a.t.x.Unit, y.Num, y.BatchNo, y.Supplier, Time = y.GetGoodsTime, o.n.a.t.OutStockUser, o.n.a.CheckResult, Seller = o.GainUser })
                        .Where(t => t.Time <= SearchTime && t.Time >= StartTime).ToList();
                else
                    return Temps.Select(o => new { o.n.a.t.x.ProductName, o.n.a.t.x.Spec, o.n.a.t.x.Unit, Num = o.n.a.t.OutStockNum, BatchNo = o.n.a.t.GoodsBatchNo, Supplier = "", Time = o.n.a.t.OutStockTime, o.n.a.t.OutStockUser, o.n.a.CheckResult, Seller = o.GainUser })
                         .Where(t => t.Time <= SearchTime && t.Time >= StartTime).ToList();
            }
        }

        #endregion 台账管理
    }
}