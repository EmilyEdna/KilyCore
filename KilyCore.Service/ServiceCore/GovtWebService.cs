﻿using KilyCore.DataEntity.RequestMapper.Cook;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Govt;
using KilyCore.DataEntity.RequestMapper.Repast;
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
#endregion
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
        #endregion

        #region 获取全局集团菜单
        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <returns></returns>
        public IList<ResponseGovtMenu> GetGovtMenu()
        {
            IQueryable<GovtMenu> queryable = Kily.Set<GovtMenu>().Where(t => t.Level == MenuEnum.LevelOne).Where(t => t.IsDelete == false).AsNoTracking().AsQueryable().OrderBy(t => t.CreateTime);
            IQueryable<GovtRoleAuthor> queryables = Kily.Set<GovtRoleAuthor>().Where(t => t.IsDelete == false);
            if (GovtInfo().AccountType <= GovtAccountEnum.Area)
                queryables = queryables.Where(t => !t.AuthorName.Contains("乡镇"));
            else
                queryables = queryables.Where(t => t.AuthorName.Contains("乡镇"));
            GovtRoleAuthor Author = queryables.FirstOrDefault();
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
        #endregion

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
        #endregion

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
                     Phone = t.Phone,
                     AccountType = t.AccountType,
                     TrueName = t.TrueName,
                     TypePath = t.TypePath,
                     AccountTypeName = AttrExtension.GetSingleDescription<GovtAccountEnum, DescriptionAttribute>(t.AccountType),
                     DepartName = t.DepartName,
                     PassWord = t.PassWord,
                     DepartId = t.DepartId,
                     Email = t.Email,
                     Flag = t.UpdateUser
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
        #endregion

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
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CompanyName))
                queryable = queryable.Where(t => t.CompanyName.Contains(pageParam.QueryParam.CompanyName));
            var data = queryable.Select(t => new ResponseEnterprise()
            {
                Id = t.Id,
                CompanyName = t.CompanyName,
                CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType),
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
            if (!string.IsNullOrEmpty(pageParam.QueryParam.MerchantName))
                queryable = queryable.Where(t => t.MerchantName.Contains(pageParam.QueryParam.MerchantName));
            var data = queryable.Select(t => new ResponseMerchant()
            {
                Id = t.Id,
                MerchantName = t.MerchantName,
                DiningTypeName = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType),
                CommunityCode = t.CommunityCode,
                Phone = t.Phone,
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
                CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType),
                Scope = t.Scope,
                VideoAddress = t.VideoAddress,
                ProductionAddress = t.ProductionAddress,
                SellerAddress = t.SellerAddress,
                NatureAgent = t.NatureAgent,
                NetAddress = t.NetAddress,
                IdCard = t.IdCard,
                Honor = t.HonorCertification,
                Discription = t.Discription,
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
                Remark = t.Remark,
                Video = Kily.Set<RepastVideo>().Where(x => x.InfoId == Id && x.IsIndex == true)
                .OrderByDescending(x => x.CreateTime).Select(x => x.MonitorAddress).Take(4).ToList()
            }).AsNoTracking().FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 获取所有企业
        /// </summary>
        /// <param name="Area"></param>
        /// <param name="ComType"></param>
        /// <returns></returns>
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
        public object GetAllMer(string Area, int ComType)
        {
            IQueryable<RepastInfo> queryable = Kily.Set<RepastInfo>()
                 .Where(t => t.AuditType == AuditEnum.AuditSuccess)
                 .OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(Area))
                queryable = queryable.Where(t => t.TypePath.Contains(Area));
            if (ComType != 0)
            {
                if(ComType<=20)
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
        public object GetAllVideo(Guid Id,int Type)
        {
            if(Type==10)
            return Kily.Set<EnterpriseVedio>().Where(x => x.CompanyId == Id && x.IsIndex == true)
                 .OrderByDescending(x => x.CreateTime).Take(4).ToList();
            if(Type!=10)
                return Kily.Set<RepastVideo>().Where(x => x.InfoId == Id && x.IsIndex == true)
                     .OrderByDescending(x => x.CreateTime).Take(4).ToList();
            return null;
        }
        #endregion

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
            if (!string.IsNullOrEmpty(pageParam.QueryParam.DepartName))
                queryable = queryable.Where(t => t.DepartName.Contains(pageParam.QueryParam.DepartName));
            if (GovtInfo().AccountType == GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
            if (GovtInfo().AccountType == GovtAccountEnum.Area)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().Area));
            if (GovtInfo().AccountType == GovtAccountEnum.Town)
                queryable = queryable.Where(t => t.Id == GovtInfo().Id);
            var data = queryable.Select(t => new ResponseGovtInfo()
            {
                Id = t.Id,
                Account = t.Account,
                DepartName = t.DepartName,
                TrueName = t.TrueName,
                AccountType = t.AccountType,
                Phone = t.Phone,
                Email = t.Email
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
            Info.TypePath = GovtInfo().TypePath;
            if (NormalUtil.CheckStringChineseUn(Info.Account))
                return "账号不能包含中文和特殊字符";
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
        /// <summary>
        /// 获取所有政府用户
        /// </summary>
        /// <returns></returns>
        public List<ResponseGovtInfo> GetAllGovt()
        {
            return Kily.Set<GovtInfo>().Where(t => t.IsDelete == false).ToList().MapToList<GovtInfo, ResponseGovtInfo>();
        }
        #endregion

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
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>().Where(t => t.AuditType == AuditEnum.AuditSuccess);
            IQueryable<RepastInfo> queryables = Kily.Set<RepastInfo>().Where(t => t.AuditType == AuditEnum.AuditSuccess);
            if (GovtInfo().AccountType <= GovtAccountEnum.Area)
            {
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
                queryables = queryables.Where(t => t.TypePath.Contains(GovtInfo().City));
            }
            IList<string> Areas = GetDepartArea();
            if (Areas != null)
            {
                if (Areas.Count > 1)
                    foreach (var item in Areas)
                    {
                        queryable = queryable.Where(t => t.TypePath.Contains(item));
                        queryables = queryables.Where(t => t.TypePath.Contains(item));
                    }
                else
                {
                    queryable = queryable.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                    queryables = queryables.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                }
            }
            var data = queryable.Select(t => new ResponseGovtDistribut()
            {
                Name = t.CompanyName,
                LngAndLat = t.LngAndLat,
                Address = t.CompanyAddress,
                CompanyCode = t.CommunityCode,
                CompanyType = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType)
            }).ToList();
            var temp = queryables.Select(t => new ResponseGovtDistribut()
            {
                Name = t.MerchantName,
                LngAndLat = t.LngAndLat,
                Address = t.Address,
                CompanyCode = t.CommunityCode,
                CompanyType = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType)
            }).ToList();
            data.AddRange(temp);
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
                    foreach (var item in Areas)
                    {
                        queryable = queryable.Where(t => t.Id.ToString() == item);
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

        #endregion

        #region 产品监管
        /// <summary>
        /// 加工产品分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseGoods> GetWorkPage(PageParamList<RequestEnterpriseGoods> pageParam)
        {
            IQueryable<EnterpriseGoods> goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false);
            if(!string.IsNullOrEmpty(pageParam.QueryParam.ProductType))
                goods= goods.Where(t => pageParam.QueryParam.ProductType.Contains(t.ProductType));
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>().Where(t => t.AuditType == AuditEnum.AuditSuccess);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
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
            if (!string.IsNullOrEmpty(pageParam.QueryParam.ProductName))
                goods = goods.Where(t => t.ProductName.Contains(pageParam.QueryParam.ProductName));
            var data = goods.Join(queryable, t => t.CompanyId, x => x.Id, (t, x) => new ResponseEnterpriseGoods()
            {
                Id = t.Id,
                ProductName = t.ProductName,
                ProductType = t.ProductType,
                ExpiredDate = t.ExpiredDate,
                Spec = t.Spec,
                Unit = x.ProductionAddress,
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
                 .Join(ProductionBatch, c => c.b.BatchId, d => d.Id, (c, d) => new { c, d })
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
                t.k.i.g.e.d.DeviceName,
                OutNo = t.k.i.g.e.c.a.GoodsBatchNo,
                t.k.i.g.e.c.a.OutStockTime,
                t.k.i.g.e.c.a.OutStockUser,
                t.k.i.g.e.c.b.Manager,
                t.k.i.g.e.c.b.ProductTime,
                InNo = t.k.i.g.e.c.b.GoodsBatchNo,
                t.k.i.g.e.d.MaterialId,
                t.k.i.h.FirstOrDefault().CheckUint,
                t.k.i.h.FirstOrDefault().CheckUser,
                t.k.i.h.FirstOrDefault().CheckResult,
                t.k.i.h.FirstOrDefault().CheckReport,
                t.l.StockName,
                t.l.SaveType,
                t.l.SaveH2,
                t.l.SaveTemp
            }).FirstOrDefault();
            var MaterialData = MaterialsData.Where(t => GoodData.MaterialId.Contains(t.e.d.Id.ToString())).Select(t => new
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
            return new
            {
                GoodData.StockName,
                GoodData.SaveH2,
                GoodData.SaveType,
                GoodData.SaveTemp,
                GoodData.GoodsName,
                GoodData.ExpiredDay,
                GoodData.Spec,
                GoodData.ProductType,
                GoodData.Standard,
                GoodData.DeviceName,
                GoodData.OutNo,
                GoodData.OutStockTime,
                GoodData.OutStockUser,
                GoodData.Manager,
                GoodProductTime = GoodData.ProductTime,
                GoodData.InNo,
                GoodCheckUint = GoodData.CheckUint,
                GoodCheckUser = GoodData.CheckUser,
                GoodCheckResult = GoodData.CheckResult,
                GoodCheckReport = GoodData.CheckReport,
                MaterialCheckUint = MaterialData.CheckUint,
                MaterialCheckUser = MaterialData.CheckUser,
                MaterialCheckResult = MaterialData.CheckResult,
                MaterialCheckReport = MaterialData.CheckReport,
                MaterialAddress = MaterialData.Address,
                MaterialExpiredDay = MaterialData.ExpiredDay,
                MaterialData.MaterName,
                MaterialData.MaterType,
                MaterialData.Supplier,
                MaterialProductTime = MaterialData.ProductTime
            };
        }
        /// <summary>
        /// 食用品分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseGoods> GetEdiblePage(PageParamList<RequestEnterpriseGoods> pageParam)
        {
            IQueryable<EnterpriseGoods> goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false)
                .Where(t => pageParam.QueryParam.ProductType.Contains(t.ProductType));
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>().Where(t => t.AuditType == AuditEnum.AuditSuccess);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
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
            if (!string.IsNullOrEmpty(pageParam.QueryParam.ProductName))
                goods = goods.Where(t => t.ProductName.Contains(pageParam.QueryParam.ProductName));
            var data = goods.Join(queryable, t => t.CompanyId, x => x.Id, (t, x) => new ResponseEnterpriseGoods()
            {
                Id = t.Id,
                ProductName = t.ProductName,
                ProductType = t.ProductType,
                ExpiredDate = t.ExpiredDate,
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
            IQueryable<EnterpriseStockType> StockType = Kily.Set<EnterpriseStockType>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseCheckGoods> CheckGoods = Kily.Set<EnterpriseCheckGoods>().Where(t => t.IsDelete == false);
            //产品的查询
            var GoodsData = GoodsStockAttach.Join(GoodStock, a => a.StockId, b => b.Id, (a, b) => new { a, b })
                .Join(Goods, c => c.b.GoodsId, d => d.Id, (c, d) => new { c, d })
                .Join(StockType, e => e.c.b.StockTypeId, f => f.Id, (e, f) => new { e, f })
                .GroupJoin(CheckGoods, g => g.e.c.b.CheckGoodsId, h => h.Id, (g, h) => new { g, h })
                .Where(t => t.g.e.d.Id == Id).AsNoTracking();
            return GoodsData.Select(t => new
            {
                t.h.FirstOrDefault().CheckUint,
                t.h.FirstOrDefault().CheckUser,
                t.h.FirstOrDefault().CheckResult,
                t.h.FirstOrDefault().CheckReport,
                t.g.f.StockName,
                t.g.f.SaveType,
                t.g.f.SaveH2,
                t.g.f.SaveTemp,
                t.g.e.d.ExpiredDate,
                t.g.e.d.ProductName,
                t.g.e.d.ProductType,
                t.g.e.d.Spec,
                t.g.e.c.b.ProductTime,
                t.g.e.c.b.Manager,
                t.g.e.c.a.OutStockTime,
                t.g.e.c.a.OutStockUser
            }).FirstOrDefault();
        }
        #endregion

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
            if (!string.IsNullOrEmpty(pageParam.QueryParam.HoldType))
                queryable = queryable.Where(t => t.HoldType.Contains(pageParam.QueryParam.HoldType));
            var data = queryable.Select(t => new ResponseCookBanquet()
            {
                Id = t.Id,
                CookId = t.CookId,
                HoldName = t.HoldName,
                Phone = t.Phone,
                Address = t.Address,
                HoldDay = t.HoldDay,
                HoldTime = t.HoldTime,
                HoldType = t.HoldType,
                Stauts = t.Stauts,
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
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
        #endregion

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
                Remark=t.Remark,
                TypePath=t.TypePath
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
                TrageType = risk.TradeType
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
        /// 获取事件数
        /// </summary>
        /// <returns></returns>
        public int GetRiskCount()
        {
            IQueryable<GovtRisk> queryable = Kily.Set<GovtRisk>().Where(t => t.ReportPlay == true);
            if (GovtInfo() != null)
            {
                if (GovtInfo().AccountType <= GovtAccountEnum.City)
                    queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
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
            return queryable.Count();
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
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CompanyName))
            {
                queryable = queryable.Where(t => t.CompanyName.Contains(pageParam.QueryParam.CompanyName));
                queryables = queryables.Where(t => t.MerchantName.Contains(pageParam.QueryParam.CompanyName));
            }
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
            {
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
                queryables = queryables.Where(t => t.TypePath.Contains(GovtInfo().City));
            }
            IList<string> Areas = GetDepartArea();
            if (Areas != null)
            {
                if (Areas.Count > 1)
                    foreach (var item in Areas)
                    {
                        queryable = queryable.Where(t => t.TypePath.Contains(item));
                        queryables = queryables.Where(t => t.TypePath.Contains(item));
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
            var Enterprise = queryable.Select(t => new
            {
                t.Id,
                Name = t.CompanyName,
                CompanyType = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType),
                t.CardExpiredDate
            }).AsNoTracking().ToList();
            var Repast = queryables.Select(t => new
            {
                t.Id,
                Name = t.MerchantName,
                CompanyType = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType),
                t.CardExpiredDate
            }).AsNoTracking().ToList();
            Enterprise.AddRange(Repast);
            return Enterprise.ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
        }
        /// <summary>
        /// 证件到期提醒
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string ReportCardWaring(Guid Id, string Key)
        {
            SystemMessage message = new SystemMessage
            {
                CompanyId = Id,
                TypePath = GovtInfo().TypePath,
                ReleaseTime = DateTime.Now,
                TrageType = Key,
                MsgName = "证件到期提醒",
                MsgContent = "您的证件日期即将到期，请尽快续期",
            };
            return Insert(message) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        #endregion

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
            if (patrol == null)
            {
                govtNet.PotrolNum = 1;
                return Insert(govtNet) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
            else
            {
                patrol.PotrolNum += 1;
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
        /// 通报批评
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditNetPatrol(RequestGovtMsg Param)
        {
            GovtNetPatrol govtNet = Kily.Set<GovtNetPatrol>().Where(t => t.Id == Param.Id).AsNoTracking().FirstOrDefault();
            govtNet.BulletinNum += 1;
            govtNet.QualifiedNum = ((govtNet.PotrolNum * 1.0) / govtNet.BulletinNum).ToString();
            List<String> Fields = new List<String> { "BulletinNum", "QualifiedNum" };
            UpdateField(govtNet, null, Fields);
            SystemMessage message = new SystemMessage
            {
                CompanyId = govtNet.CompanyId,
                MsgContent = Param.Content,
                MsgName = Param.Title,
                ReleaseTime = DateTime.Now,
                TrageType = govtNet.TradeType,
                TypePath = govtNet.TypePath
            };
            return Insert(message) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        #endregion
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
            var data = queryable.Select(t => new ResponseGovtPatrolCategory()
            {
                Id = t.Id,
                CategoryName = t.CategoryName,
                CategoryType = t.CategoryType,
                Remark = t.Remark
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
        #endregion
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
                Sb.Append(@"<table cellpadding='2' width='98%' cellspacing='0' border='1' bordercolor='#000000'>");
                Sb.Append(@"<tr style='text-align:center;line-height:28px;font-size:15px;'><th>序号</th><th>检查项</th><th>结果</th><th>分值</th></tr>");
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
                    string Score = CategoryAttach.AnswerScore.Split("|").ToList().ElementAtOrDefault(index);
                    Sb.Append($@"<tr style='font-size:13px;'><td align='center'>{++EleIndex}</td><td>{CategoryAttach.QuestionTitle}</td><td align='center'>{AnswerList[1]}</td><td align='center'>{Score}</td></tr>");
                    TotalScore += Convert.ToDouble(Score);
                });
                Sb.Append($@"<tr><td align='center' style='font-size:13px;'>得分总和</td><td colspan='3' style='font-size:13px;'>{TotalScore}分</td></tr>");
                Sb.Append(@"<tr><td colspan='4' style='font-size:13px;'><center>现场照片</center>");
                Param.ImgList.Split(",").ToList().ForEach(t =>
                {
                    Sb.Append($@"<center><img src='http://www.cfdacx.com{t}' style='width:100%;max-width:456px;'></center>");
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
                    .Replace("{companyCode}", Param.CompanyPhone)
                    .Replace("{companySign}", Param.CompanySign)
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
                PatrolTable = t.PatrolTable
            }).FirstOrDefault();
        }
        #endregion
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
        #endregion
        #endregion

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
                Remark=t.Remark
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
        #endregion
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
                TypePath = GovtInfo().TypePath
            };
            return Insert(message) ? ServiceMessage.HANDLESUCCESS : ServiceMessage.HANDLEFAIL;
        }
        #endregion
        #endregion

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
            if (!string.IsNullOrEmpty(pageParam.QueryParam.ComplainContent))
                queryable = queryable.Where(t => t.ComplainContent.Contains(pageParam.QueryParam.ComplainContent));
            var data = queryable.Select(t => new ResponseGovtComplain
            {
                Id = t.Id,
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
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
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
            complain.ComplainTime = DateTime.Now;
            return Insert(complain) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
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
                TypePath = complain.TypePath
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
            GovtComplain complain = Kily.Set<GovtComplain>().Where(t => t.Id == Id).FirstOrDefault();
            complain.HandlerContent = Param;
            complain.Status = "已处理";
            List<String> Fields = new List<String> { "HandlerContent", "Status" };
            return UpdateField(complain, null, Fields) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }
        #endregion

        #region 数据统计
        /// <summary>
        /// 获取产品统计
        /// </summary>
        /// <returns></returns>
        public IList<DataPie> GetProductRank()
        {
            IQueryable<EnterpriseGoods> goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false).Where(t => t.AuditType == AuditEnum.AuditSuccess);
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
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
            return goods.Join(queryable, t => t.CompanyId, x => x.Id, (t, x) => new { t.ProductType }).GroupBy(t => t.ProductType).Select(t => new DataPie
            {
                value = t.Count(),
                name = t.Key
            }).ToList();
        }
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
            IList<string> Areas = GetDepartArea();
            if (Areas != null)
            {
                if (Areas.Count > 1)
                    foreach (var item in Areas)
                    {
                        //外环
                        Patrol = Patrol.Where(t => t.TypePath.Contains(item));
                    }
                else
                    Patrol = Patrol.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
            }
            else
                Patrol = Patrol.Where(t => t.TypePath.Contains(GovtInfo().Area));
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
            IList<string> Areas = GetDepartArea();
            if (Areas != null)
            {
                if (Areas.Count > 1)
                    foreach (var item in Areas)
                    {
                        com = com.Where(t => t.TypePath.Contains(item));
                        mer = mer.Where(t => t.TypePath.Contains(item));
                        risk = risk.Where(t => t.TypePath.Contains(item));
                        plain = plain.Where(t => t.TypePath.Contains(item));
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
        /// <summary>
        /// 获取入驻的企业地图
        /// </summary>
        /// <returns></returns>
        public ResponseGovtMap GetAllCityMerchantCount()
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
            data = data.OrderByDescending(t => t.TotalCount).ToList();
            return new ResponseGovtMap { CityName = City.CityName, City = City.CityId, DataList = data };
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
            //分组
            var ZZ_Com = queryable.Where(t => t.CompanyType.Equals("种植企业")).OrderByDescending(t => t.CreateTime).ToList();
            var YZ_Com = queryable.Where(t => t.CompanyType.Equals("养殖企业")).OrderByDescending(t => t.CreateTime).ToList();
            var SC_Com = queryable.Where(t => t.CompanyType.Equals("生产企业")).OrderByDescending(t => t.CreateTime).ToList();
            var LT_Com = queryable.Where(t => t.CompanyType.Equals("流通企业")).OrderByDescending(t => t.CreateTime).ToList();
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
            #endregion
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
            #endregion
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
            #endregion
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
            #endregion
            List<int> ZCom = new List<int> { Z3, Z7, Z15, Z30, Z180, Z365 };
            List<int> YCom = new List<int> { Y3, Y7, Y15, Y30, Y180, Y365 };
            List<int> SCom = new List<int> { S3, S7, S15, S30, S180, S365 };
            List<int> LCom = new List<int> { L3, L7, L15, L30, L180, L365 };
            return new { ZCom, YCom, SCom, LCom };
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
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
            {
                //外环
                Merchant = Merchant.Where(t => t.TypePath.Contains(GovtInfo().City));
                //内环
                Enterprise = Enterprise.Where(t => t.TypePath.Contains(GovtInfo().City));
                Banquet = Banquet.Where(t => t.TypePath.Contains(GovtInfo().City));
            }
            IList<string> Areas = GetDepartArea();
            if (Areas != null)
            {
                if (Areas.Count > 1)
                    foreach (var item in Areas)
                    {
                        //外环
                        Merchant = Merchant.Where(t => t.TypePath.Contains(item));
                        //内环
                        Enterprise = Enterprise.Where(t => t.TypePath.Contains(item));
                        Banquet = Banquet.Where(t => t.TypePath.Contains(item));
                    }
                else
                {
                    //外环
                    Merchant = Merchant.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                    //内环
                    Enterprise = Enterprise.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                    Banquet = Banquet.Where(t => t.TypePath.Contains(Areas.FirstOrDefault()));
                }
            }
            else
            {
                //外环
                Merchant = Merchant.Where(t => t.TypePath.Contains(GovtInfo().Area));
                //内环
                Enterprise = Enterprise.Where(t => t.TypePath.Contains(GovtInfo().Area));
                Banquet = Banquet.Where(t => t.TypePath.Contains(GovtInfo().Area));
            }
            //月入住
            var CompanyTotal = (Enterprise.Count() + Merchant.Count()) == 0 ? 1 : (Enterprise.Count() + Merchant.Count());
            int CMSum = Enterprise.Where(t => t.CreateTime >= DateTime.Now.AddMonths(-1)).Count() + Merchant.Where(t => t.CreateTime >= DateTime.Now.AddMonths(-1)).Count();
            decimal CMTotal = Math.Round((CMSum / CompanyTotal) * 100M, 2);
            //月产品总数
            IQueryable<EnterpriseGoods> goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false);
            int CGSum = goods.Join(Enterprise, t => t.CompanyId, x => x.Id, (t, x) => new { t.CreateTime }).Where(t => t.CreateTime >= DateTime.Now.AddMonths(-1)).Count();
            var TotalPro = goods.Join(Enterprise, t => t.CompanyId, x => x.Id, (t, x) => new { t.CreateTime }).Count();
            decimal CGTotal = Math.Round((CGSum / (TotalPro==0?1: TotalPro)) * 100M, 2);
            //月移动执法总数
            IQueryable<GovtMovePatrol> MovePatrol = Kily.Set<GovtMovePatrol>().Where(t => t.GovtId == GovtInfo().Id);
            int CPSum = MovePatrol.Where(t => t.CreateTime >= DateTime.Now.AddMonths(-1)).Count();
            decimal CPTotal = Math.Round((CPSum / (MovePatrol.Count() == 0 ? 1 : MovePatrol.Count())) * 100M, 2);
            //月群宴数
            int CBSum = Banquet.Where(t => t.CreateTime >= DateTime.Now.AddMonths(-1)).Count();
            decimal CBTotal = Math.Round((CPSum / (Banquet.Count() == 0 ? 1 : Banquet.Count())) * 100M, 2);
            return new { CMSum, CMTotal, CGSum, CGTotal, CPSum, CPTotal, CBSum, CBTotal };
        }
        /// <summary>
        /// 投诉占比
        /// </summary>
        /// <returns></returns>
        public Object GetComplainDataRatio()
        {
            IQueryable<GovtComplain> queryable = Kily.Set<GovtComplain>().AsNoTracking();
            if (GovtInfo().AccountType <= GovtAccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(GovtInfo().City));
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
            //计算周投诉
            int WG = queryable.Where(t => t.CreateTime >= DateTime.Now.AddDays(-7)).Count();
            decimal WGSum = Math.Round((WG / queryable.Count()) * 100M, 2);
            //计算月投诉
            int MG = queryable.Where(t => t.CreateTime >= DateTime.Now.AddMonths(-1)).Count();
            decimal MGSum = Math.Round((MG / queryable.Count()) * 100M, 2);
            //计算总数
            int Total = WG + MG;
            decimal Sum = WGSum + MGSum;
            return new { WG, WGSum, MG, MGSum, Total, Sum };
        }
        #endregion

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
        #endregion
    }
}