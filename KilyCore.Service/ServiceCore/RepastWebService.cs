using KilyCore.DataEntity.RequestMapper.Repast;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Repast;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.Model.Repast;
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
        /// 下拉字典类型
        /// </summary>
        /// <returns></returns>
        public IList<ResponseRepastDictionary> GetDictionaryList()
        {
            IQueryable<RepastDictionary> queryable = Kily.Set<RepastDictionary>().Where(t => t.IsDelete == false);
            if (MerchantInfo() != null)
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
            else
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.GroupBy(t => t.DicType)
                .Select(t => new ResponseRepastDictionary()
                {
                    DicType = t.Key.ToString(),
                }).AsNoTracking().ToList();
            data.ForEach(t =>
            {
                t.DictionaryList = Kily.Set<RepastDictionary>()
                .Where(x => x.IsDelete == false)
                .Where(x => x.DicType == t.DicType).Select(x => new ResponseRepastDictionary()
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
            RequestAliPayModel AliPayModel = new RequestAliPayModel();
            AliPayModel.OrderTitle = MerchantInfo().MerchantName + "合同缴费";
            SystemStayContract contract = Param.MapToEntity<SystemStayContract>();
            contract.EnterpriseOrMerchant = 2;
            RepastInfo info = Kily.Set<RepastInfo>().Where(t => t.Id == contract.CompanyId).FirstOrDefault();
            info.VersionType = Param.VersionType;
            if (Param.VersionType == SystemVersionEnum.Test)
            {

                if (info.DiningType == MerchantEnum.Normal)
                    AliPayModel.Money = 360 * Convert.ToInt32(Param.ContractYear);
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    AliPayModel.Money = 240 * Convert.ToInt32(Param.ContractYear);
            }
            if (Param.VersionType == SystemVersionEnum.Base)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    AliPayModel.Money = 1500 * Convert.ToInt32(Param.ContractYear);
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    AliPayModel.Money = 1000 * Convert.ToInt32(Param.ContractYear);
            }
            if (Param.VersionType == SystemVersionEnum.Level)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    AliPayModel.Money = 5000 * Convert.ToInt32(Param.ContractYear);
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    AliPayModel.Money = 3000 * Convert.ToInt32(Param.ContractYear);
            }
            if (Param.VersionType == SystemVersionEnum.Enterprise)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    AliPayModel.Money = 10000 * Convert.ToInt32(Param.ContractYear);
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    AliPayModel.Money = 5000 * Convert.ToInt32(Param.ContractYear);
            }
            if (Param.VersionType == SystemVersionEnum.Common)
                AliPayModel.Money = 60 * Convert.ToInt32(Param.ContractYear);
            UpdateField(info, "VersionType");
            if (contract.ContractType == 1)
            {
                contract.AdminId = null;
                contract.IsPay = false;
                contract.TryOut = "/";
                contract.EndTime = DateTime.Now.AddYears(Convert.ToInt32(contract.ContractYear));
                //银联
                if (contract.PayType == PayEnum.Unionpay)
                {
                    return Insert<SystemStayContract>(contract) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
                }
                //支付宝支付
                else if (contract.PayType == PayEnum.Alipay)
                {
                    Insert<SystemStayContract>(contract);
                    return AliPayCore.Instance.WebPay(AliPayModel);
                }
                //微信支付
                else
                {
                    RequestWxPayModel WxPayModel = AliPayModel.MapToEntity<RequestWxPayModel>();
                    WxPayModel.Money = WxPayModel.Money * 100;
                    Insert<SystemStayContract>(contract);
                    return WxPayCore.Instance.WebPay(WxPayModel);

                }
            }
            else
            {
                contract.PayType = PayEnum.AgentPay;
                contract.TryOut = "30";
                contract.EndTime = DateTime.Now.AddYears(Convert.ToInt32(contract.ContractYear));
                return Insert<SystemStayContract>(contract) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
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
                queryable = queryable.Where(t => t.InfoId == MerchantInfo().Id || GetChildIdList(MerchantInfo().Id).Contains(t.InfoId));
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
                queryable = queryable.Where(t => t.InfoId == MerchantUser().Id);
            var data = queryable.Select(t => new ResponseMerchantUser()
            {
                Id = t.Id,
                TrueName = t.TrueName
            }).ToList();
            return data;
        }
        #endregion
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
                queryable = queryable.Where(t => t.DicType.Contains(pageParam.QueryParam.DicName));
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
        /// 删除码表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveDic(Guid Id)
        {
            return Remove<RepastDictionary>(ExpressionExtension.GetExpression<RepastDictionary>("Id", Id, ExpressionEnum.Equals)) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 字典详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseRepastDictionary GetDicDetail(Guid Id)
        {
            var data = Kily.Set<RepastDictionary>().AsNoTracking().Select(t => new ResponseRepastDictionary()
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
        /// <summary>
        /// 编辑字典
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditDic(RequestRepastDictionary Param)
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
        #endregion
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
        /// 编辑续费
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditContinued(RequestRepastContinued Param)
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
        public string EditUpLevel(RequestRepastUpLevel Param)
        {
            RepastUpLevel level = Param.MapToEntity<RepastUpLevel>();
            level.AuditType = AuditEnum.WaitAduit;
            return Insert<RepastUpLevel>(level) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
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
        #endregion
        #endregion

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
        /// 删除供应商
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveSupplier(Guid Id)
        {
            return Delete(ExpressionExtension.GetExpression<RepastSupplier>("Id", Id, ExpressionEnum.Equals)) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 编辑供应商
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditSupplier(RequestRepastSupplier Param)
        {
            RepastSupplier supplier = Param.MapToEntity<RepastSupplier>();
            return Insert(supplier) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
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
                LinkPhone = t.LinkPhone,
                SupplierName = t.SupplierName,
                Address=t.Address,
            }).ToList();
            return data;
        }
        #endregion
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
                return Insert(buybill) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return Update<RepastBuybill, RequestRepastBuybill>(buybill, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }
        #endregion
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
                UnPay = t.UnPay
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
                SellTime = t.SellTime,
                UnPay = t.UnPay
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
                return Insert(sellbill) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return Update<RepastSellbill, RequestRepastSellbill>(sellbill, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }
        #endregion
        #endregion

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
        /// 删除菜品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveDish(Guid Id)
        {
            return Delete(ExpressionExtension.GetExpression<RepastDish>("Id", Id, ExpressionEnum.Equals)) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 编辑菜品
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditDish(RequestRepastDish Param)
        {
            RepastDish dish = Param.MapToEntity<RepastDish>();
            if (Param.Id == Guid.Empty)
                return Insert<RepastDish>(dish) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return Update<RepastDish, RequestRepastDish>(dish, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }
        #endregion

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
                PrePrice = t.PrePrice,
                QualityReport = t.QualityReport,
                Remark = t.Remark,
                SourceLink = t.SourceLink,
                Supplier = t.Supplier
            }).AsNoTracking().FirstOrDefault();
            return data;
        }
        #endregion

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
        #endregion

        #endregion

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
                    AliPayModel.Money = 360 * Key;
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    AliPayModel.Money = 240 * Key;
            }
            if ((Value == null ? info.VersionType : (SystemVersionEnum)(Value)) == SystemVersionEnum.Base)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    AliPayModel.Money = 1500 * Key;
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    AliPayModel.Money = 1000 * Key;
            }
            if ((Value == null ? info.VersionType : (SystemVersionEnum)(Value)) == SystemVersionEnum.Level)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    AliPayModel.Money = 5000 * Key;
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    AliPayModel.Money = 3000 * Key;
            }
            if ((Value == null ? info.VersionType : (SystemVersionEnum)(Value)) == SystemVersionEnum.Enterprise)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    AliPayModel.Money = 10000 * Key;
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    AliPayModel.Money = 5000 * Key;
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
                    WxPayModel.Money = 100 * 360 * Key;
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    WxPayModel.Money = 100 * 240 * Key;
            }
            if ((Value == null ? info.VersionType : (SystemVersionEnum)(Value)) == SystemVersionEnum.Base)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    WxPayModel.Money = 100 * 1500 * Key;
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    WxPayModel.Money = 100 * 1000 * Key;
            }
            if ((Value == null ? info.VersionType : (SystemVersionEnum)(Value)) == SystemVersionEnum.Level)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    WxPayModel.Money = 100 * 5000 * Key;
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    WxPayModel.Money = 100 * 3000 * Key;
            }
            if ((Value == null ? info.VersionType : (SystemVersionEnum)(Value)) == SystemVersionEnum.Enterprise)
            {
                if (info.DiningType == MerchantEnum.Normal)
                    WxPayModel.Money = 100 * 10000 * Key;
                if (info.DiningType == MerchantEnum.UnitCanteen)
                    WxPayModel.Money = 100 * 5000 * Key;
            }
            return WxPayCore.Instance.WebPay(WxPayModel);
        }
        #endregion
    }
}
