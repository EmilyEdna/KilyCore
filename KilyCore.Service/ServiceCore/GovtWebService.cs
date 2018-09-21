using KilyCore.DataEntity.RequestMapper.Cook;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Govt;
using KilyCore.DataEntity.RequestMapper.Repast;
using KilyCore.DataEntity.ResponseMapper.Cook;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
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
                Discription = t.Discription
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
            var data = Kily.Set<RepastInfo>().Where(t => t.Id == Id).GroupJoin(Kily.Set<RepastVideo>(), t => t.Id, x => x.InfoId, (t, x) => new ResponseMerchant()
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
                Account = x.FirstOrDefault().VideoAddress
            }).AsNoTracking().FirstOrDefault();
            return data;
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
            if (Param.Id != Guid.Empty)
                return Update(Info, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            else
            {
                if (GovtInfo().AccountType == GovtAccountEnum.City)
                    Info.AccountType = GovtAccountEnum.Area;
                else if (GovtInfo().AccountType == GovtAccountEnum.Area)
                    Info.AccountType = GovtAccountEnum.Town;
                return Insert(Info) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
        }
        #endregion

        #region 管辖区域
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
            }).ToList();
            var temp = queryables.Select(t => new ResponseGovtDistribut()
            {
                Name = t.MerchantName,
                LngAndLat = t.LngAndLat,
                Address = t.Address
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
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>().Where(t => t.CompanyType == CompanyEnum.Production).Where(t => t.AuditType == AuditEnum.AuditSuccess);
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
                t.k.i.g.e.c.a.Seller,
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
                GoodData.Seller,
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
            IQueryable<EnterpriseGoods> goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>().Where(t => t.CompanyType <= CompanyEnum.Culture).Where(t => t.AuditType == AuditEnum.AuditSuccess);
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
                 }).AsNoTracking().ToList()
            }).AsNoTracking().FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 审核群宴
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string EditCookBanquet(Guid Id)
        {
            CookBanquet cook = Kily.Set<CookBanquet>().Where(t => t.Id == Id).FirstOrDefault();
            cook.Stauts = "完成";
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
                TrageType=risk.TradeType
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
        public string ReportCardWaring(Guid Id,string Key)
        {
            SystemMessage message = new SystemMessage
            {
                CompanyId=Id,
                TypePath = GovtInfo().TypePath,
                ReleaseTime = DateTime.Now,
                TrageType= Key,
                MsgName = "证件到期提醒",
                MsgContent = "您的证件日期即将到期，请尽快续期",
            };
            return Insert(message) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        #endregion
    }
}
