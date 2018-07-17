using KilyCore.DataEntity.RequestMapper.Repast;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Repast;
using KilyCore.DataEntity.ResponseMapper.System;
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
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
namespace KilyCore.Service.ServiceCore
{
    public class RepastService : Repository, IRepastService
    {
        #region 商家资料
        /// <summary>
        /// 商家分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseMerchant> GetMerchantPage(PageParamList<RequestMerchant> pageParam)
        {
            IQueryable<RepastInfo> queryable = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.MerchantName))
                queryable = queryable.Where(t => t.MerchantName.Contains(pageParam.QueryParam.MerchantName));
            if (!string.IsNullOrEmpty(pageParam.QueryParam.AreaTree))
                queryable = queryable.Where(t => t.TypePath.Contains(pageParam.QueryParam.AreaTree));
            if (UserInfo().AccountType == AccountEnum.Province)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().Province));
            if (UserInfo().AccountType == AccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().City));
            if (UserInfo().AccountType == AccountEnum.Area)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().Area));
            if (UserInfo().AccountType == AccountEnum.Village)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().Town));
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseMerchant()
            {
                Id = t.Id,
                CommunityCode = t.CommunityCode,
                Account = t.Account,
                PassWord = t.PassWord,
                MerchantName = t.MerchantName,
                DiningTypeName = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType),
                DingRoleId = t.DingRoleId,
                Phone = t.Phone,
                Email = t.Email,
                TypePath = t.TypePath,
                TableName = t.GetType().Name
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 获取商家详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseMerchant GetMerchantDetail(Guid Id)
        {
            IQueryable<RepastInfo> queryable = Kily.Set<RepastInfo>().Where(t => t.Id == Id);
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseMerchant()
            {
                Id = t.Id,
                CommunityCode = t.CommunityCode,
                Account = t.Account,
                PassWord = t.PassWord,
                MerchantName = t.MerchantName,
                DiningTypeName = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType),
                DingRoleId = t.DingRoleId,
                Phone = t.Phone,
                Email = t.Email,
                TypePath = t.TypePath,
                Certification = t.Certification,
                ImplUser = t.ImplUser,
                AllowUnit = t.AllowUnit,
                VersionTypeName = AttrExtension.GetSingleDescription<SystemVersionEnum, DescriptionAttribute>(t.VersionType),
                AuditInfo = Kily.Set<SystemAudit>()
                    .Where(x => x.IsDelete == false)
                    .Where(x => x.TableId == t.Id).ToList().MapToList<SystemAudit, ResponseAudit>()
            }).FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 审核商家资料
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string AuditMerchant(RequestAudit Param)
        {
            Param.AuditName = UserInfo().TrueName;
            SystemAudit Audit = Param.MapToEntity<SystemAudit>();
            if (Insert<SystemAudit>(Audit))
            {
                RepastInfo Info = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false).Where(t => t.Id == Param.TableId).FirstOrDefault();
                Info.AuditType = Param.AuditType;
                if (UpdateField<RepastInfo>(Info, "AuditType"))
                    return ServiceMessage.HANDLESUCCESS;
                else
                    return ServiceMessage.HANDLEFAIL;
            }
            else
                return ServiceMessage.INSERTFAIL;
        }
        #endregion

        #region 餐饮菜单
        /// <summary>
        /// 菜单分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastMenu> GetDiningMenuPage(PageParamList<RequestRepastMenu> pageParam)
        {
            IQueryable<RepastMenu> queryable = Kily.Set<RepastMenu>().Where(t => t.IsDelete == false).AsQueryable();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.MenuName))
                queryable = queryable.Where(t => t.MenuName.Contains(pageParam.QueryParam.MenuName));
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseRepastMenu()
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
        /// 编辑菜单
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditDiningMenu(RequestRepastMenu Param)
        {
            RepastMenu tree = Param.MapToEntity<RepastMenu>();
            if (Param.Id != Guid.Empty)
            {
                //修改
                if (Update<RepastMenu, RequestRepastMenu>(tree, Param))
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
                if (Insert<RepastMenu>(tree))
                    return ServiceMessage.INSERTSUCCESS;
                else
                    return ServiceMessage.INSERTFAIL;
            }
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveMenu(Guid Id)
        {
            if (Delete<RepastMenu>(ExpressionExtension.GetExpression<RepastMenu>("Id", Id, ExpressionEnum.Equals)))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 菜单详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseRepastMenu GetDiningMenuDetail(Guid Id)
        {
            var data = Kily.Set<RepastMenu>().Where(t => t.Id == Id).AsNoTracking().Select(t => new ResponseRepastMenu()
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
        /// <summary>
        /// 父级菜单
        /// </summary>
        /// <returns></returns>
        public IList<ResponseRepastMenu> AddDiningParentMenu()
        {
            var query = Kily.Set<RepastMenu>().Where(t => t.Level == MenuEnum.LevelOne).Where(t => t.ParentId == null).AsNoTracking().AsQueryable();
            var data = query.Select(t => new ResponseRepastMenu()
            {
                MenuId = t.MenuId,
                MenuName = t.MenuName
            }).ToList();
            return data;
        }
        #endregion

        #region 餐饮权限菜单树
        /// <summary>
        /// 餐饮权限菜单树
        /// </summary>
        /// <returns></returns>
        public IList<ResponseParentTree> GetDiningTree()
        {
            IQueryable<ResponseParentTree> queryable = Kily.Set<RepastMenu>().Where(t => t.IsDelete == false)
              .Where(t => t.Level == MenuEnum.LevelOne)
              .AsNoTracking().Select(t => new ResponseParentTree()
              {
                  Id = t.Id,
                  Text = t.MenuName,
                  Color = "black",
                  BackClolor = "white",
                  SelectedIcon = "fa fa-refresh fa-spin",
                  Nodes = Kily.Set<RepastMenu>().Where(x => x.IsDelete == false)
                  .Where(x => x.Level != MenuEnum.LevelOne)
                  .Where(x => x.ParentId == t.MenuId).AsNoTracking()
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
        #endregion

        #region 餐饮权限
        /// <summary>
        ///编辑角色
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditRole(RequestRepastRoleAuthor Param)
        {
            RepastRoleAuthor Author = Param.MapToEntity<RepastRoleAuthor>();
            if (Kily.Set<RepastRoleAuthor>().Where(t => t.IsDelete == false).Where(t => t.AuthorName.Equals(Author.AuthorName)).AsNoTracking().FirstOrDefault() != null)
            {
                return "角色名称重复请重新添加!";
            }
            else
            {
                if (Insert<RepastRoleAuthor>(Author))
                    return ServiceMessage.INSERTSUCCESS;
                else
                    return ServiceMessage.INSERTFAIL;
            }
        }
        /// <summary>
        /// 角色权限列表分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastRoleAuthor> GetMerchantAuthorPage(PageParamList<RequestRepastRoleAuthor> pageParam)
        {
            IQueryable<RepastRoleAuthor> queryable = Kily.Set<RepastRoleAuthor>();
            IQueryable<RepastInfo> queryables = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.MerchantName))
                queryables = queryables.Where(t => t.MerchantName.Contains(pageParam.QueryParam.MerchantName));
            var data = queryables.OrderBy(t => t.CreateTime).GroupJoin(queryable, t => t.DingRoleId, x => x.Id, (t, x) => new ResponseRepastRoleAuthor()
            {
                Id = t.Id,
                RepastRoleId = x.FirstOrDefault().Id,
                MerchantName = t.MerchantName,
                MerchantRoleName = x.FirstOrDefault().AuthorName,
                MerchantTypeName = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType),
                AuthorMenuPath = x.FirstOrDefault().AuthorMenuPath
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveAuthorRole(Guid Id)
        {
            if (Remove<RepastRoleAuthor>(t => t.Id == Id))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 角色分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastRoleAuthor> GetRoleAuthorPage(PageParamList<RequestRepastRoleAuthor> pageParam)
        {
            IQueryable<RepastRoleAuthor> queryable = Kily.Set<RepastRoleAuthor>();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.AuthorName))
                queryable = queryable.Where(t => t.AuthorName.Contains(pageParam.QueryParam.AuthorName));
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseRepastRoleAuthor()
            {
                Id = t.Id,
                MerchantRoleName = t.AuthorName,
                AuthorMenuPath = t.AuthorMenuPath
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IList<ResponseRepastRoleAuthor> GetRoleAuthorList()
        {
            return Kily.Set<RepastRoleAuthor>().Select(t => new ResponseRepastRoleAuthor()
            {
                Id = t.Id,
                MerchantRoleName = t.AuthorName
            }).ToList();
        }
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string DistributionRole(RequestRepastRoleAuthor Param)
        {
            RepastInfo info = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false).Where(t => t.Id == Param.Id).FirstOrDefault();
            if (Param.AuthorName.Contains("基本"))
                info.VersionType = SystemVersionEnum.Normal;
            if (Param.AuthorName.Contains("体验"))
                info.VersionType = SystemVersionEnum.Test;
            if (Param.AuthorName.Contains("基础"))
                info.VersionType = SystemVersionEnum.Base;
            if (Param.AuthorName.Contains("升级"))
                info.VersionType = SystemVersionEnum.Level;
            if (Param.AuthorName.Contains("旗舰"))
                info.VersionType = SystemVersionEnum.Enterprise;
            info.DingRoleId = Param.RepastRoleId;
            if (UpdateField<RepastInfo>(info, "DingRoleId"))
                return ServiceMessage.HANDLESUCCESS;
            else
                return ServiceMessage.HANDLEFAIL;
        }
        #endregion

        #region 认证审核
        /// <summary>
        /// 商家认证分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastIdent> GetDiningIdentPage(PageParamList<RequestRepastIdent> pageParam)
        {
            IQueryable<RepastIdent> queryable = Kily.Set<RepastIdent>().Where(t => t.IsDelete == false);
            IQueryable<RepastInfo> queryables = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false);
            if (pageParam.QueryParam.DiningType.HasValue)
                queryable = queryable.Where(t => t.DiningType == pageParam.QueryParam.DiningType);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.MerchantName))
                queryable = queryable.Where(t => t.MerchantName.Contains(pageParam.QueryParam.MerchantName));
            if (!string.IsNullOrEmpty(pageParam.QueryParam.AreaTree))
                queryables = queryables.Where(t => t.TypePath.Contains(pageParam.QueryParam.AreaTree));
            if (UserInfo().AccountType == AccountEnum.Province)
                queryables = queryables.Where(t => t.TypePath.Contains(UserInfo().Province));
            if (UserInfo().AccountType == AccountEnum.City)
                queryables = queryables.Where(t => t.TypePath.Contains(UserInfo().City));
            if (UserInfo().AccountType == AccountEnum.Area)
                queryables = queryables.Where(t => t.TypePath.Contains(UserInfo().Area));
            if (UserInfo().AccountType == AccountEnum.Village)
                queryables = queryables.Where(t => t.TypePath.Contains(UserInfo().Town));
            var data = queryables.Join(queryable, a => a.Id, t => t.InfoId, (a, t) => new ResponseRepastIdent()
            {
                Id = t.Id,
                MerchantName = t.MerchantName,
                IdentStarName = AttrExtension.GetSingleDescription<IdentEnum, DescriptionAttribute>(t.IdentStar),
                AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(t.AuditType),
                DiningTypeName = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType),
                IdentYear = t.IdentYear,
                TableName = t.GetType().Name,
                AuditType = t.AuditType,
                AuditInfo = Kily.Set<SystemAudit>()
                    .Where(x => x.IsDelete == false)
                    .Where(x => x.TableId == t.Id).ToList().MapToList<SystemAudit, ResponseAudit>()
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 获取认证详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseRepastIdent GetDiningIdentDetail(Guid Id)
        {
            IQueryable<RepastIdent> queryable = Kily.Set<RepastIdent>().Where(t => t.IsDelete == false);
            IQueryable<RepastIdentAttach> queryables = Kily.Set<RepastIdentAttach>().Where(t => t.IsDelete == false);
            var data = queryable.GroupJoin(queryables, x => x.Id, p => p.IdentId, (x, t) => new ResponseRepastIdent()
            {
                IdentNo = x.IdentNo,
                MerchantName = x.MerchantName,
                IdentStarName = AttrExtension.GetSingleDescription<IdentEnum, DescriptionAttribute>(x.IdentStar),
                AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(x.AuditType),
                DiningTypeName = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(x.DiningType),
                IdentYear = x.IdentYear,
                CommunityCode = x.CommunityCode,
                Representative = x.Representative,
                RepresentativeCard = x.RepresentativeCard,
                SendPerson = x.SendPerson,
                SendCard = x.SendCard,
                LinkPhone = x.LinkPhone,
                Remark = x.Remark,
                IdentStartTime = x.IdentStartTime,
                IdentEndTime = x.IdentEndTime,
                ImgCard = t.FirstOrDefault().ImgCard,
                ImgApply = t.FirstOrDefault().ImgApply,
                ImgResearch = t.FirstOrDefault().ImgResearch,
                ImgAgreement = t.FirstOrDefault().ImgAgreement,
                ImgMaterialOrder = t.FirstOrDefault().ImgMaterialOrder,
                ImgDisinfection = t.FirstOrDefault().ImgDisinfection,
                ImgMaterialSave = t.FirstOrDefault().ImgMaterialSave,
                ImgAbandoned = t.FirstOrDefault().ImgAbandoned,
                ImgSample = t.FirstOrDefault().ImgSample,
                ImgWorkingPerson = t.FirstOrDefault().ImgWorkingPerson,
                ImgOther = t.FirstOrDefault().ImgOther,
                AuditInfo = Kily.Set<SystemAudit>()
                    .Where(o => o.IsDelete == false)
                    .Where(o => o.TableId == x.Id).ToList().MapToList<SystemAudit, ResponseAudit>()
            }).FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 审核认证
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string AuditIdent(RequestAudit Param)
        {
            Param.AuditName = UserInfo().TrueName;
            SystemAudit Audit = Param.MapToEntity<SystemAudit>();
            if (Insert<SystemAudit>(Audit))
            {
                RepastIdent Ident = Kily.Set<RepastIdent>().Where(t => t.IsDelete == false).Where(t => t.Id == Param.TableId).FirstOrDefault();
                Ident.AuditType = Param.AuditType;
                if (UpdateField<RepastIdent>(Ident, "AuditType"))
                    return ServiceMessage.HANDLESUCCESS;
                else
                    return ServiceMessage.HANDLEFAIL;
            }
            else
                return ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 认证缴费
        /// </summary>
        /// <returns></returns>
        public string AuditPayment(RequestPayment Param)
        {
            SystemPayment payment = Param.MapToEntity<SystemPayment>();
            if (Insert<SystemPayment>(payment))
                return ServiceMessage.INSERTSUCCESS;
            else
                return ServiceMessage.INSERTFAIL;
        }
        #endregion

        #region 登录注册
        /// <summary>
        /// 餐饮系统注册
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string RegistRepastAccount(RequestMerchant Param)
        {
            Param.AuditType = AuditEnum.WaitAduit;
            RepastRoleAuthor Author = Kily.Set<RepastRoleAuthor>().Where(t => t.IsDelete == false).Where(t => t.AuthorName.Contains("基本")).OrderBy(t => t.CreateTime).FirstOrDefault();
            Param.DingRoleId = Author.Id;
            RepastInfo Info = Param.MapToEntity<RepastInfo>();
            if (Insert<RepastInfo>(Info))
                return ServiceMessage.INSERTSUCCESS;
            else
                return ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 餐饮商家登录
        /// </summary>
        /// <param name="LoginValidate"></param>
        /// <returns></returns>
        public Object MerchantLogin(RequestValidate LoginValidate)
        {
            #region 餐饮企业登录
            IQueryable<RepastInfo> queryable = Kily.Set<RepastInfo>()
               .Where(t => t.Account.Equals(LoginValidate.Account))
               .Where(t => t.PassWord.Equals(LoginValidate.PassWord))
               .Where(t => t.IsDelete == false);
            ResponseMerchant Info = queryable.Select(t => new ResponseMerchant()
            {
                Id = t.Id,
                Account = t.Account,
                CommunityCode = t.CommunityCode,
                MerchantName = t.MerchantName,
                Phone = t.Phone,
                DiningType = t.DiningType,
                VersionType = t.VersionType,
                VersionTypeName = AttrExtension.GetSingleDescription<SystemVersionEnum, DescriptionAttribute>(t.VersionType),
                DiningTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.DiningType),
                AuditType = t.AuditType,
                AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(t.AuditType),
                DingRoleId = t.DingRoleId,
                TypePath = t.TypePath,
                Certification = t.Certification,
                Email=t.Email,
                ImplUser=t.ImplUser,
                TableName = typeof(ResponseMerchant).Name
            }).FirstOrDefault();
            #endregion
            #region 非企业登录
            IQueryable<RepastInfoUser> queryables = Kily.Set<RepastInfoUser>()
              .Where(t => t.Account.Equals(LoginValidate.Account))
              .Where(t => t.PassWord.Equals(LoginValidate.PassWord))
              .Where(t => t.IsDelete == false);
            ResponseMerchantUser User = queryables.Select(t => new ResponseMerchantUser()
            {
                Id = t.InfoId,
                InfoId = t.Id,
                Account = t.Account,
                TrueName = t.TrueName,
                DiningType = t.DiningType,
                VersionType = t.VersionType,
                DingRoleId = t.DingRoleId,
                TypePath = t.TypePath,
                MerchantName=t.MerchantName,
                Phone=t.Phone,
                IdCard=t.Phone,
                VersionTypeName = AttrExtension.GetSingleDescription<SystemVersionEnum, DescriptionAttribute>(t.VersionType),
                DiningTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.DiningType),
                TableName = typeof(ResponseMerchantUser).Name
            }).FirstOrDefault();
            #endregion
            if (Info != null)
                return Info;
            else if (User != null)
                return User;
            else return null;
        }
        #endregion
    }
}
