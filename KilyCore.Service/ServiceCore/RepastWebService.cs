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
#endregion
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
        public IList<ResponseParentTree> GetRepastWebTree()
        {
            if (MerchantInfo() != null)
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
                             SelectedIcon = "fa fa-refresh fa-spin",
                         }).AsQueryable()
                     }).AsQueryable();
                var data = queryable.ToList();
                return data;
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
                             SelectedIcon = "fa fa-refresh fa-spin",
                         }).AsQueryable()
                     }).AsQueryable();
                var data = queryable.ToList();
                return data;

            }
        }
        #endregion
        #region 基础管理
        #region 商家资料
        /// <summary>
        /// 商家资料分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseMerchant> GetMerchantInfo(PageParamList<RequestMerchant> pageParam)
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
            var data = Kily.Set<RepastInfo>().Where(t => t.Id == Id).Select(t => new ResponseMerchant()
            {
                Id = t.Id,
                Account = t.Account,
                Address = t.Address,
                PassWord = t.PassWord,
                CommunityCode = t.CommunityCode,
                MerchantName = t.MerchantName,
                DiningType = t.DiningType,
                Phone = t.Phone,
                VersionType = t.VersionType,
                TypePath = t.TypePath,
                Certification = t.Certification,
                Email = t.Email,
                ImplUser = t.ImplUser,
                AllowUnit = t.AllowUnit,
                IdCard = t.IdCard,
                Remark = t.Remark
            }).FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 编辑商家资料
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditMerchant(RequestMerchant Param)
        {
            Param.AuditType = AuditEnum.WaitAduit;
            RepastInfo data = Kily.Set<RepastInfo>().Where(t => t.Id == Param.Id).FirstOrDefault();
            Param.DingRoleId = data.DingRoleId;
            Param.InfoId = data.InfoId;
            RepastInfo info = Param.MapToEntity<RepastInfo>();
            return Update<RepastInfo, RequestMerchant>(info, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }
        /// <summary>
        /// 保存合同
        /// </summary>
        /// <returns></returns>
        public string SaveContract(RequestStayContract Param)
        {
            Param.AuditType = AuditEnum.WaitAduit;
            SystemStayContract contract = Param.MapToEntity<SystemStayContract>();
            contract.EnterpriseOrMerchant = 2;
            if (contract.ContractType == 1)
            {
                contract.AdminId = null;
                //调用支付
                if (contract.PayType == PayEnum.Unionpay)
                    contract.IsPay = false;
                else
                {
                    //支付宝和微信支付
                }
            }
            else
            {
                contract.PayType = PayEnum.AgentPay;
                contract.TryOut = "30";
                contract.EndTime = DateTime.Now.AddYears(Convert.ToInt32(contract.ContractYear));
            }
            if (Insert<SystemStayContract>(contract))
                return ServiceMessage.INSERTSUCCESS;
            else
                return ServiceMessage.INSERTFAIL;
        }
        #endregion
        #region 商家认证
        /// <summary>
        /// 商家认证
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditMerchantIdent(RequestRepastIdent Param)
        {
            Param.Id = Guid.NewGuid();
            Param.IdentId = Param.Id;
            Param.AuditType = AuditEnum.WaitAduit;
            Param.IdentEndTime = Param.IdentStartTime.AddYears(Param.IdentYear);
            RepastIdent ident = Param.MapToEntity<RepastIdent>();
            RepastIdentAttach attach = Param.MapToEntity<RepastIdentAttach>();
            return Insert<RepastIdent>(ident, false) && Insert<RepastIdentAttach>(attach) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        #endregion
        #region 权限角色
        /// <summary>
        /// 新增账户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditRoleAuthor(RequestRoleAuthorWeb Param)
        {
            RepastRoleAuthorWeb Author = Param.MapToEntity<RepastRoleAuthorWeb>();
            if (Insert<RepastRoleAuthorWeb>(Author))
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
        /// 删除账户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveRole(Guid Id)
        {
            if (Remove<RepastRoleAuthorWeb>(t => t.Id == Id))
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
        #endregion
        #region 人员管理
        /// <summary>
        /// 人员分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseMerchantUser> GetMerchantUserPage(PageParamList<RequestMerchantUser> pageParam)
        {
            IQueryable<RepastInfoUser> queryable = Kily.Set<RepastInfoUser>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.TrueName))
                queryable = queryable.Where(t => t.TrueName.Contains(pageParam.QueryParam.TrueName));
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id);
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new ResponseMerchantUser()
            {
                Id = t.Id,
                InfoId = t.InfoId,
                TrueName = t.TrueName,
                Phone = t.Phone,
                Account = t.Account,
                IdCard = t.IdCard
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 编辑子账户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditUser(RequestMerchantUser Param)
        {
            RepastInfoUser User = Param.MapToObj<RequestMerchantUser, RepastInfoUser>();
            if (MerchantInfo() != null)
                User.TypePath = MerchantInfo().TypePath;
            else
                User.TypePath = MerchantUser().TypePath;
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
            if (Delete(ExpressionExtension.GetExpression<RepastInfoUser>("Id", Id, ExpressionEnum.Equals)))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }
        #endregion
        #region 集团账户
        /// <summary>
        /// 集团账户列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseMerchant> GetChildInfo(PageParamList<RequestMerchant> pageParam)
        {
            var data = Kily.Set<RepastInfo>().Where(t => t.InfoId == pageParam.QueryParam.Id)
               .OrderByDescending(t => t.CreateTime).Select(t => new ResponseMerchant()
               {
                   Id = t.Id,
                   InfoId = t.InfoId,
                   MerchantName = t.MerchantName,
                   Account = t.Account,
                   Address = t.Address,
                   Certification=t.Certification
               }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 编辑子账户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditChildInfo(RequestMerchant Param)
        {
            if (MerchantInfo() != null)
            {
                if (MerchantInfo().InfoId != null)
                    return "无权限创建，仅限总公司使用!";
                RepastInfo info = Param.MapToEntity<RepastInfo>();
                info.AuditType = AuditEnum.WaitAduit;
                info.DiningType = MerchantEnum.Normal;
                return Insert(info) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
            return "无权限创建!";
        }
        #endregion
        #endregion
    }
}
