using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Function;
using KilyCore.DataEntity.RequestMapper.Govt;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.Function;
using KilyCore.DataEntity.ResponseMapper.Govt;
using KilyCore.DataEntity.ResponseMapper.Phone;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.Model.Enterprise;
using KilyCore.EntityFrameWork.Model.Govt;
using KilyCore.EntityFrameWork.Model.System;
using KilyCore.EntityFrameWork.ModelEnum;
using KilyCore.Extension.AttributeExtension;
using KilyCore.Extension.AutoMapperExtension;
using KilyCore.Extension.OutSideService;
using KilyCore.Extension.PayCore.AliPay;
using KilyCore.Extension.PayCore.WxPay;
using KilyCore.Extension.UtilExtension;
using KilyCore.Nethereums;
using KilyCore.Repositories.BaseRepository;
using KilyCore.Service.ConstMessage;
using KilyCore.Service.IServiceCore;
using KilyCore.Service.QueryExtend;
using Microsoft.EntityFrameworkCore;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            if (CompanyInfo() != null)
                Seller = Seller.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                Seller = Seller.Where(t => t.CompanyId == CompanyUser().Id);
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

        #endregion 下拉关联列表

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
        /// +
        ///
        ///
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

        #endregion 获取全局集团菜单

        #region 编辑不需要权限的

        #region 企业资料

        /// <summary>
        /// 更新企业资料
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string SaveEnterprise(RequestEnterprise Param)
        {
            EnterpriseInfo data = Kily.Set<EnterpriseInfo>().Where(t => t.Id == Param.Id).AsNoTracking().FirstOrDefault();
            Param.EnterpriseRoleId = data.EnterpriseRoleId;
            Param.CompanyId = data.CompanyId;
            Param.InviteCode = data.InviteCode;
            EnterpriseInfo Info = Param.MapToEntity<EnterpriseInfo>();
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
                    if (!CompanySearchApi.GetOutSideApiSearchApi.CheckCompany(Param.CompanyName, Param.CommunityCode))
                        return "对不起，您录入的企业名称和社会统一代码不一致！";
                    //正确才审核和提交合同
                    Info.AuditType = AuditEnum.AuditSuccess;
                    if (Kily.Set<SystemStayContract>().Where(t => t.CompanyId == Info.Id).FirstOrDefault() == null)
                    {
                        RequestStayContract contract = new RequestStayContract()
                        {
                            CompanyId = Info.Id,
                            TypePath = Info.TypePath,
                            CompanyName = Info.CompanyName,
                            VersionType = SystemVersionEnum.Base,
                            ContractYear = "1",
                            ContractType = 2,
                            IsFormInviteCode = true
                        };
                        SaveContract(contract);
                    }
                    var CompanyType = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(Info.CompanyType);
                    EnterpriseRoleAuthor Role = Kily.Set<EnterpriseRoleAuthor>().Where(t => t.EnterpriseRoleName.Contains(CompanyType + "基础")).FirstOrDefault();
                    Info.EnterpriseRoleId = Role.Id;
                }
            }
            else
            {
                Info.AuditType = AuditEnum.WaitAduit;
            }
            if (Update<EnterpriseInfo, RequestEnterprise>(Info, Param))
                return $"{ServiceMessage.UPDATESUCCESS}，请重新登录系统(重要)！";
            else
                return $"{ServiceMessage.UPDATEFAIL}，请重新登录系统(重要)！";
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

        #endregion 企业资料

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
            if (Param.Id == Guid.Empty)
            {
                var Users = Kily.Set<EnterpriseUser>().Where(t => t.Account.Equals(Param.Account)).AsNoTracking().FirstOrDefault();
                if (Users != null) return "该账号已经存在!";
            }
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

        #endregion 人员管理

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

        #endregion 权限角色

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

        #endregion 集团账户

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

        #endregion 企业字典

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

        #endregion 升级续费

        #region 内部文件

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

        #endregion 内部文件

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

        #endregion 设备管理

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

        #endregion 设施管理

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

        #endregion 关键点控制

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

        #endregion 产品召回

        #endregion 编辑不需要权限的

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

        #endregion 人员管理

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

        #endregion 权限角色

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

        #endregion 企业字典

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

        #endregion 内部文件

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

        #endregion 设备管理

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

        #endregion 设施管理

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

        #endregion 关键点控制

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

        #endregion 物码管理

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

        #endregion 企业自查

        #endregion 删除不需要权限的

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
                Category=t.Category,
                CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType)
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            //写入区块链
            //Task.Run(async () => {
            //    Web3 web = new Web3("http://localhost:8101");
            //    NethereumUtil.SendEtherBlock(web, "0x9B65ec00758DF15d7De0C7E70aFA6Ee0cD1859C3", "123456", "0x0FCB94b4B1f6763008aC6bE0de68B50B8f777a10", 2135566);
            //});
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
                    Category = t.Category,
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

        #endregion 企业资料

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
            {
                return new ResponseStayContract()
                {
                    Id = contract.Id,
                    VersionType = Param.VersionType,
                    TagNum = info.TagCodeNum,
                    PayInfoMsg = Update(Demo, Param) ? "提交成功" : "提交失败"
                };
            }
            if (Param.VersionType == SystemVersionEnum.Test)
            {
                info.TagCodeNum = ServiceMessage.TEST;
                if (info.CompanyType == CompanyEnum.Plant || info.CompanyType == CompanyEnum.Culture)
                    AliPayModel.Money = ConfigMoney.PlantAndCultureTest * Convert.ToInt32(Param.ContractYear);
                if (info.CompanyType == CompanyEnum.Production || info.CompanyType == CompanyEnum.Other)
                    AliPayModel.Money = ConfigMoney.ProductionTest * Convert.ToInt32(Param.ContractYear);
                if (info.CompanyType == CompanyEnum.Circulation)
                    AliPayModel.Money = ConfigMoney.CirculationTest * Convert.ToInt32(Param.ContractYear);
            }
            if (Param.VersionType == SystemVersionEnum.Base)
            {
                info.TagCodeNum = ServiceMessage.BASE;
                if (info.CompanyType == CompanyEnum.Plant || info.CompanyType == CompanyEnum.Culture)
                    AliPayModel.Money = ConfigMoney.PlantAndCultureBase * Convert.ToInt32(Param.ContractYear);
                if (info.CompanyType == CompanyEnum.Production || info.CompanyType == CompanyEnum.Other)
                    AliPayModel.Money = ConfigMoney.ProductionBase * Convert.ToInt32(Param.ContractYear);
                if (info.CompanyType == CompanyEnum.Circulation)
                    AliPayModel.Money = ConfigMoney.CirculationBase * Convert.ToInt32(Param.ContractYear);
            }
            if (Param.VersionType == SystemVersionEnum.Level)
            {
                info.TagCodeNum = ServiceMessage.LEVEL;
                if (info.CompanyType == CompanyEnum.Plant || info.CompanyType == CompanyEnum.Culture)
                    AliPayModel.Money = ConfigMoney.PlantAndCultureLv * Convert.ToInt32(Param.ContractYear);
                if (info.CompanyType == CompanyEnum.Production || info.CompanyType == CompanyEnum.Other)
                    AliPayModel.Money = ConfigMoney.ProductionLv * Convert.ToInt32(Param.ContractYear);
                if (info.CompanyType == CompanyEnum.Circulation)
                    AliPayModel.Money = ConfigMoney.CirculationLv * Convert.ToInt32(Param.ContractYear);
            }
            if (Param.VersionType == SystemVersionEnum.Enterprise)
            {
                info.TagCodeNum = ServiceMessage.ENTERPRISE;
                if (info.CompanyType == CompanyEnum.Plant || info.CompanyType == CompanyEnum.Culture)
                    AliPayModel.Money = ConfigMoney.PlantAndCultureEnterprise * Convert.ToInt32(Param.ContractYear);
                if (info.CompanyType == CompanyEnum.Production || info.CompanyType == CompanyEnum.Other)
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
                if (Param.IsFormInviteCode.Value == true)
                    contract.AuditType = AuditEnum.AuditSuccess;
                contract.PayType = PayEnum.AgentPay;
                contract.IsPay = true;
                contract.TryOut = "365";
                contract.TryStarDate = DateTime.Now;
                contract.TryEndDate = contract.TryStarDate.Value.AddDays(365);
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

        #endregion 保存合同

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

        #endregion 人员管理

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

        #endregion 权限角色

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

        #endregion 集团账户

        #region 企业认证

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

        #endregion 企业认证

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
                queryable = queryable.Where(t => t.DicName.Contains(pageParam.QueryParam.DicName));
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

        #endregion 企业字典

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

        #endregion 升级续费

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

        #endregion 内部文件

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
            if (CompanyInfo() != null)
                vedio.TypePath = CompanyInfo().TypePath;
            else
                vedio.TypePath = CompanyUser().TypePath;
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

        #endregion 视频监控

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

        #endregion 企业自查

        #endregion 基础管理

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

        #endregion 成长流程

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

        #endregion 育苗信息

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

        #endregion 施养管理

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

        #endregion 农药疫情

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

        #endregion 环境检测

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
                BatchNo = t.BatchNo,
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

        #endregion 成长日记

        #endregion 成长档案

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
                else if (Tag.TagType == TagEnum.OneBox)
                    Tag.CodeDiscern = Param.CodeStar + "B";
                else
                    Tag.CodeDiscern = Param.CodeStar + "Q";
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
                    BatchNo = t.AcceptNo,
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
            EnterpriseVeinTag VeinTag = Kily.Set<EnterpriseVeinTag>().Where(t => t.IsDelete == false).Where(t => t.AcceptNo == BatchNo).FirstOrDefault();
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
            IQueryable<EnterpriseTagAttach> data = Kily.Set<EnterpriseTagAttach>().OrderByDescending(t => t.CreateTime);
            if (pageParam.QueryParam.Id != Guid.Empty)
                data = data.Where(t => t.TagId == pageParam.QueryParam.Id);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.StockNo))
                data = data.Where(t => t.StockNo == pageParam.QueryParam.StockNo);
            var res = data.Select(t => new ResponseEnterpriseTagAttach()
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
            return res;
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

        /// <summary>
        /// 编辑包码
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditPackCode(RequestEnterprisePackCodeBind Param)
        {
            var Id = Regex.Match(Param.PackCode, "[a-fA-F0-9]{8}(-[a-fA-F0-9]{4}){3}-[a-fA-F0-9]{12}").Value;
            Param.TagId = Guid.Parse(Id);
            var entity = Param.MapToEntity<EnterprisePackCodeBind>();
            return Insert(entity) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        #endregion 物码管理

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

        /// <summary>
        /// 删除厂商
        /// </summary>
        /// <param name="Id"></param>
        public string RemoveSeller(Guid Id)
        {
            return Delete<EnterpriseSeller>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        #endregion 厂商管理

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

        #endregion 原料

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

        #endregion 入库

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

        #endregion 出库

        #endregion 原料管理

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

        #endregion 设备管理

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
                SeriesName = t.SeriesName,
                Standard = t.Standard,
                TargetName = string.Join(",", Kily.Set<EnterpriseTarget>().Where(x => t.TargetId.Contains(x.Id.ToString())).Select(x => x.TargetName).ToArray())
            }).ToList();
            return data;
        }

        #endregion 产品系列

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

        #endregion 指标把控

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
                }).OrderBy(o => o.ResultTime).ToList();
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
                    Img = t.Img,
                    ResultTime = t.ResultTime,
                    Manager = t.Manager
                }).OrderBy(o => o.ResultTime).ToList();
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
            if (Param.Id == Guid.Empty)
                return Insert(Attach) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
            {
                var data = Kily.Set<EnterpriseProductionBatchAttach>().Where(t => t.Id == Param.Id).AsNoTracking().FirstOrDefault();
                data.Img = Attach.Img;
                return UpdateField(data, "Img") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            }
        }

        #endregion 生产批次

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

        #endregion 设施管理

        #endregion 生产管理

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
                ProductSeriesName = x.FirstOrDefault().SeriesName + "-" + x.FirstOrDefault().Standard,
                ExpiredDate = t.ExpiredDate,
                ProductName = t.ProductName,
                ProductType = t.ProductType,
                Unit = t.Unit,
                Image = t.Image,
                Remark = t.Remark,
                BatchPrice = t.BatchPrice,
                Price = t.Price
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
                Unit = t.Unit,
                Image = t.Image,
                Remark = t.Remark,
                LineCode = t.LineCode,
                BatchPrice = t.BatchPrice,
                Price = t.Price,
                SellWebNet = t.SellWebNet
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

        #endregion 产品列表

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
            goods.AuditType = AuditEnum.AuditSuccess;
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
            IQueryable<EnterpriseGoodsStockAttach> attaches = Kily.Set<EnterpriseGoodsStockAttach>().Where(t => t.IsDelete == false);
            List<ResponseEnterpriseMaterial> Material = Kily.Set<EnterpriseMaterialStockAttach>().Where(t => t.IsDelete == false)
                .Join(Kily.Set<EnterpriseMaterialStock>().Where(t => t.IsDelete == false), t => t.MaterialStockId, x => x.Id, (t, x) => new { x.BatchNo })
                .Join(Kily.Set<EnterpriseMaterial>().Where(t => t.IsDelete == false), p => p.BatchNo, y => y.BatchNo, (p, y) => new ResponseEnterpriseMaterial
                {
                    Id = y.Id,
                    MaterName = y.MaterName,
                }).ToList();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.GoodsName))
                Goods = Goods.Where(t => t.ProductName.Contains(pageParam.QueryParam.GoodsName));
            if (CompanyInfo() != null)
            {
                var StockList = Stock.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
                var Temp = StockList.OrderByDescending(t => t.CreateTime).Join(Goods, t => t.GoodsId, x => x.Id, (t, x) => new { t, x });
                if (CompanyInfo().CompanyType == CompanyEnum.Plant || CompanyInfo().CompanyType == CompanyEnum.Culture)
                    return Temp.GroupJoin(Note, p => p.t.GrowNoteId, o => o.Id, (p, o) => new ResponseEnterpriseGoodsStock()
                    {
                        Id = p.t.Id,
                        CompanyId = p.t.CompanyId,
                        GoodsName = p.x.ProductName,
                        GoodsBatchNo = p.t.GoodsBatchNo,
                        StockType = p.t.StockType,
                        Spec = p.x.Spec,
                        InStockNum = p.t.InStockNum,
                        ProBatch = o.FirstOrDefault().BatchNo,
                        GoodsId = p.x.Id,
                        ImgUrl = p.t.ImgUrl,
                        IsBindBoxCode = p.t.IsBindBoxCode,
                        Manager = p.t.Manager,
                        CheckGoodsId = p.t.CheckGoodsId,
                        TotalCount = attaches.Where(t => t.StockId == p.t.Id).Select(t => t.OutStockNum).Sum() + p.t.InStockNum,
                        AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(p.x.AuditType),
                        MaterialList = Material
                    }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
                else if (CompanyInfo().CompanyType == CompanyEnum.Production)
                    return Temp.GroupJoin(Batch, p => p.t.BatchId, o => o.Id, (p, o) => new ResponseEnterpriseGoodsStock()
                    {
                        Id = p.t.Id,
                        CompanyId = p.t.CompanyId,
                        GoodsName = p.x.ProductName,
                        GoodsBatchNo = p.t.GoodsBatchNo,
                        StockType = p.t.StockType,
                        Spec = p.x.Spec,
                        IsBindBoxCode = p.t.IsBindBoxCode,
                        InStockNum = p.t.InStockNum,
                        ProBatch = o.FirstOrDefault().BatchNo,
                        GoodsId = p.x.Id,
                        ImgUrl = p.t.ImgUrl,
                        Manager = p.t.Manager,
                        CheckGoodsId = p.t.CheckGoodsId,
                        MaterialId = o.FirstOrDefault().MaterialId,
                        TotalCount = attaches.Where(t => t.StockId == p.t.Id).Select(t => t.OutStockNum).Sum() + p.t.InStockNum,
                        AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(p.x.AuditType),
                        MaterialList = Material.ToList()
                    }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
                else if (CompanyInfo().CompanyType == CompanyEnum.Circulation)
                    return Temp.GroupJoin(Buyer, p => p.t.BuyId, o => o.Id, (p, o) => new ResponseEnterpriseGoodsStock()
                    {
                        Id = p.t.Id,
                        CompanyId = p.t.CompanyId,
                        GoodsName = p.x.ProductName,
                        GoodsBatchNo = p.t.GoodsBatchNo,
                        StockType = p.t.StockType,
                        Spec = p.x.Spec,
                        InStockNum = p.t.InStockNum,
                        ProBatch = o.FirstOrDefault().BatchNo,
                        GoodsId = p.x.Id,
                        ImgUrl = p.t.ImgUrl,
                        IsBindBoxCode = p.t.IsBindBoxCode,
                        CheckGoodsId = p.t.CheckGoodsId,
                        Manager = p.t.Manager,
                        TotalCount = attaches.Where(t => t.StockId == p.t.Id).Select(t => t.OutStockNum).Sum() + p.t.InStockNum,
                        AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(p.x.AuditType),
                        MaterialList = Material.ToList()
                    }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
                else
                    return Temp.GroupJoin(Buyer, p => p.t.BuyId, o => o.Id, (p, o) => new ResponseEnterpriseGoodsStock()
                    {
                        Id = p.t.Id,
                        CompanyId = p.t.CompanyId,
                        GoodsName = p.x.ProductName,
                        GoodsBatchNo = p.t.GoodsBatchNo,
                        StockType = p.t.StockType,
                        Spec = p.x.Spec,
                        InStockNum = p.t.InStockNum,
                        ProBatch = o.FirstOrDefault().BatchNo,
                        GoodsId = p.x.Id,
                        ImgUrl = p.t.ImgUrl,
                        IsBindBoxCode = p.t.IsBindBoxCode,
                        CheckGoodsId = p.t.CheckGoodsId,
                        Manager = p.t.Manager,
                        TotalCount = attaches.Where(t => t.StockId == p.t.Id).Select(t => t.OutStockNum).Sum() + p.t.InStockNum,
                        AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(p.x.AuditType),
                        MaterialList = Material.ToList()
                    }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            }
            else
            {
                var StockList = Stock.Where(t => t.CompanyId == CompanyUser().Id).ToList();
                var Temp = StockList.OrderByDescending(t => t.CreateTime).Join(Goods, t => t.GoodsId, x => x.Id, (t, x) => new { t, x }).ToList();
                if (CompanyUser().CompanyType == CompanyEnum.Plant || CompanyUser().CompanyType == CompanyEnum.Culture)
                    return Temp.GroupJoin(Note, p => p.t.GrowNoteId, o => o.Id, (p, o) => new ResponseEnterpriseGoodsStock()
                    {
                        Id = p.t.Id,
                        CompanyId = p.t.CompanyId,
                        GoodsName = p.x.ProductName,
                        GoodsBatchNo = p.t.GoodsBatchNo,
                        Spec = p.x.Spec,
                        StockType = p.t.StockType,
                        IsBindBoxCode = p.t.IsBindBoxCode,
                        InStockNum = p.t.InStockNum,
                        ProBatch = o.FirstOrDefault().BatchNo,
                        GoodsId = p.x.Id,
                        ImgUrl = p.t.ImgUrl,
                        CheckGoodsId = p.t.CheckGoodsId,
                        Manager = p.t.Manager,
                        TotalCount = attaches.Where(t => t.StockId == p.t.Id).Select(t => t.OutStockNum).Sum() + p.t.InStockNum,
                        AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(p.x.AuditType),
                        MaterialList = Material
                    }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
                else if (CompanyUser().CompanyType == CompanyEnum.Production)
                    return Temp.GroupJoin(Batch, p => p.t.BatchId, o => o.Id, (p, o) => new ResponseEnterpriseGoodsStock()
                    {
                        Id = p.t.Id,
                        CompanyId = p.t.CompanyId,
                        GoodsName = p.x.ProductName,
                        GoodsBatchNo = p.t.GoodsBatchNo,
                        StockType = p.t.StockType,
                        IsBindBoxCode = p.t.IsBindBoxCode,
                        Spec = p.x.Spec,
                        InStockNum = p.t.InStockNum,
                        ImgUrl = p.t.ImgUrl,
                        ProBatch = o.FirstOrDefault().BatchNo,
                        GoodsId = p.x.Id,
                        MaterialId = o.FirstOrDefault().MaterialId,
                        Manager = p.t.Manager,
                        CheckGoodsId = p.t.CheckGoodsId,
                        TotalCount = attaches.Where(t => t.StockId == p.t.Id).Select(t => t.OutStockNum).Sum() + p.t.InStockNum,
                        AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(p.x.AuditType),
                        MaterialList = Material.ToList()
                    }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
                else if (CompanyUser().CompanyType == CompanyEnum.Circulation)
                    return Temp.GroupJoin(Buyer, p => p.t.BuyId, o => o.Id, (p, o) => new ResponseEnterpriseGoodsStock()
                    {
                        Id = p.t.Id,
                        CompanyId = p.t.CompanyId,
                        GoodsName = p.x.ProductName,
                        GoodsBatchNo = p.t.GoodsBatchNo,
                        StockType = p.t.StockType,
                        ImgUrl = p.t.ImgUrl,
                        Spec = p.x.Spec,
                        IsBindBoxCode = p.t.IsBindBoxCode,
                        InStockNum = p.t.InStockNum,
                        ProBatch = o.FirstOrDefault().BatchNo,
                        GoodsId = p.x.Id,
                        Manager = p.t.Manager,
                        CheckGoodsId = p.t.CheckGoodsId,
                        TotalCount = attaches.Where(t => t.StockId == p.t.Id).Select(t => t.OutStockNum).Sum() + p.t.InStockNum,
                        AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(p.x.AuditType),
                        MaterialList = Material.ToList()
                    }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
                else
                    return Temp.GroupJoin(Buyer, p => p.t.BuyId, o => o.Id, (p, o) => new ResponseEnterpriseGoodsStock()
                    {
                        Id = p.t.Id,
                        CompanyId = p.t.CompanyId,
                        GoodsName = p.x.ProductName,
                        GoodsBatchNo = p.t.GoodsBatchNo,
                        StockType = p.t.StockType,
                        ImgUrl = p.t.ImgUrl,
                        Spec = p.x.Spec,
                        IsBindBoxCode = p.t.IsBindBoxCode,
                        InStockNum = p.t.InStockNum,
                        ProBatch = o.FirstOrDefault().BatchNo,
                        GoodsId = p.x.Id,
                        Manager = p.t.Manager,
                        CheckGoodsId = p.t.CheckGoodsId,
                        TotalCount = attaches.Where(t => t.StockId == p.t.Id).Select(t => t.OutStockNum).Sum() + p.t.InStockNum,
                        AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(p.x.AuditType),
                        MaterialList = Material.ToList()
                    }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            }
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
            if (Param.BuyId.HasValue)
            {
                int buytotal = Convert.ToInt32(Kily.Set<EnterpriseBuyer>().Where(t => t.Id == Param.BuyId).Select(t => t.Num).FirstOrDefault());
                int total = Kily.Set<EnterpriseGoodsStock>().Where(t => t.BuyId == Param.BuyId).Select(t => t.InStockNum).Sum();
                int lost = buytotal - total;
                if (Param.InStockNum > lost)
                    return "入库数量超出了总进货数量";
            }
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
                Param.BoxCode = Regex.Match(Param.BoxCode, "e=(.*)?").Groups[1].Value;
                var HostStar = Param.BoxCode.Split("B")[0];
                var OncTag = Regex.Matches(Param.ThingCode, $"{HostStar}[W|P]\\d{{13}}").ToList();
                var VenTag = Regex.Matches(Param.ThingCode, "e=\\d{12}").Select(t => Regex.Split(t.Groups[0].Value, "=")[1]).ToList();
                if (VenTag.Count != 0 && OncTag.Count() != 0)
                    return "请勿混用纹理二维码和追溯码";
                if (VenTag.Count == 0 && OncTag.Count() == 0)
                    return "请勿扫入纹理二维码或者追溯码";
                if (OncTag.Count() != 0)
                {
                    if (OncTag.Count > 1000)
                        return "装箱数量最大支持每箱1000个物件";
                    Param.BoxCount = OncTag.Count.ToString();
                }
                else
                {
                    if (VenTag.Count > 1000)
                        return "装箱数量最大支持每箱1000个物件";
                    Param.BoxCount = VenTag.Count.ToString();
                }
                Param.BoxCodeSort = Convert.ToInt64(Param.BoxCode.Split("B")[1].Substring(0, 12));
                EnterpriseBoxing Box = Param.MapToEntity<EnterpriseBoxing>();
                EnterpriseGoodsStock Stock = Kily.Set<EnterpriseGoodsStock>().Where(t => t.GoodsBatchNo == Param.StockBatchNo).AsNoTracking().FirstOrDefault();
                EnterpriseGoodsStockAttach StockAttach = Kily.Set<EnterpriseGoodsStockAttach>().Where(t => t.GoodsBatchNo == Param.StockBatchNo).AsNoTracking().FirstOrDefault();
                //判断是否使用了箱码
                var TempBox = Kily.Set<EnterpriseBoxing>().Where(t => t.BoxCode.Contains(Param.BoxCode)).FirstOrDefault();
                if (TempBox != null)
                    return "该箱码已经使用!";
                if (Stock != null)
                {
                    var Tag = Kily.Set<EnterpriseTagAttach>().Where(t => t.StockNo == Param.StockBatchNo).AsNoTracking().ToList();
                    if (OncTag.Count != 0)
                    {
                        if (OncTag.Any(t => t.Contains("W") || t.Contains("P")))
                            foreach (var item in OncTag)
                            {
                                long Temp = 0;
                                string Host = string.Empty;
                                if (item.Contains("W"))
                                {
                                    Temp = Convert.ToInt64(item.Split("W")[1].Substring(0, 12));
                                    Host = HostStar + "W";
                                }
                                else
                                {
                                    Temp = Convert.ToInt64(item.Split("P")[1].Substring(0, 12));
                                    Host = HostStar + "P";
                                }
                                var TempEntity = Tag.Where(t => t.StarSerialNo <= Temp && t.EndSerialNo >= Temp).FirstOrDefault();
                                if (TempEntity == null)
                                    return $"{Host + Temp}溯源号段不在此批次中";
                            }
                        if (OncTag.Count > Stock.InStockNum)
                            return "超出库存!";
                        else if (OncTag.Count ==Stock.InStockNum)
                        {
                            Stock.IsBindBoxCode = true;
                            UpdateField(Stock, "IsBindBoxCode");
                        }
                    }
                    else
                    {
                        foreach (var item in VenTag)
                        {
                            long Temp = Convert.ToInt64(item.Substring(0, 11));
                            var TempEntity = Tag.Where(t => t.StarSerialNo <= Temp && t.EndSerialNo >= Temp).FirstOrDefault();
                            if (TempEntity == null)
                                return $"{Temp}纹理码号段不在此批次中";
                        }
                        if (VenTag.Count > Stock.InStockNum)
                            return "超出库存!";
                        else if (VenTag.Count == Stock.InStockNum)
                        {
                            Stock.IsBindBoxCode = true;
                            UpdateField(Stock, "IsBindBoxCode");
                        }
                    }
                }
                else
                {
                    var InStockNo = Kily.Set<EnterpriseGoodsStock>().Where(t => t.Id == StockAttach.StockId).Select(t => t.GoodsBatchNo).AsNoTracking().FirstOrDefault();
                    var Tag = Kily.Set<EnterpriseTagAttach>().Where(t => t.StockNo == InStockNo).AsNoTracking().ToList();
                    if (OncTag.Count() != 0)
                    {
                        if (OncTag.Any(t => t.Contains("W") || t.Contains("P")))
                            foreach (var item in OncTag)
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
                    }
                    else
                    {
                        foreach (var item in VenTag)
                        {
                            long Temp = Convert.ToInt64(item.Substring(0, 11));
                            var TempEntity = Tag.Where(t => t.StarSerialNo <= Temp && t.EndSerialNo >= Temp).FirstOrDefault();
                            if (TempEntity == null)
                                return $"{Temp}纹理码号段不在此批次中";
                        }
                    }
                    StockAttach.BoxCodeNo = Param.BoxCode;
                    StockAttach.BoxCount = Param.BoxCount;
                    List<String> Field = new List<String> { "BoxCodeNo", "BoxCount" };
                    UpdateField(StockAttach, null, Field);
                }
                return Insert(Box) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
            catch (Exception ex)
            {
                return "二维码格式不正确";
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
                EnterpriseVeinTag data = Kily.Set<EnterpriseVeinTag>().Where(t => t.IsDelete == false).Where(t => t.AcceptNo == Param.TagBatchNo).FirstOrDefault();
                int SumCount = Kily.Set<EnterpriseTagAttach>().Where(t => t.IsDelete == false).Where(t => t.TagBatchNo == Param.TagBatchNo).Select(t => t.UseNum).Sum();
                if (!data.IsAccept)
                    return "请先签收";
                data.UseNum += Count;
                if (data.TotalNo - data.UseNum < 0)
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
                    OutStockTime = o.t.OutStockTime,
                    StockEx = o.x.InStockNum,
                    BoxCodeNo = o.t.BoxCodeNo,
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
                    var Num = Param.BoxCodeNo.Split(",").ToList();
                    var HostStar = Regex.Match(Num.FirstOrDefault(), "e=(.*)?").Groups[1].Value.Split("B")[0];
                    var Nums = Regex.Matches(string.Join(",", Num), $"{HostStar}B\\d{{13}}").ToList();
                    Param.BoxCount = Nums.Count.ToString();
                    Param.OutStockNum = Kily.Set<EnterpriseBoxing>().Where(t => Nums.Contains(t.BoxCode)).Select(t => t.ThingCode).ToList().SelectMany(t => t.Split(",")).Where(t => !string.IsNullOrEmpty(t)).Count();
                    foreach (var item in Nums)
                    {
                        var IsUse = Kily.Set<EnterpriseGoodsStockAttach>().Where(t => t.BoxCodeNo.Contains(item)).FirstOrDefault();
                        if (IsUse != null)
                            return $"请勿重复使用尾号为{item}的箱码";
                    }
                }
            }
            else if (Param.CodeType == 2)
            {
                if (!string.IsNullOrEmpty(Param.SourceCodeNo))
                {
                    var stockInNo = Kily.Set<EnterpriseGoodsStock>().Where(t => t.Id == Param.StockId).Select(t => t.GoodsBatchNo).FirstOrDefault();
                    //拆箱发货
                    EnterpriseBoxing Box = Kily.Set<EnterpriseBoxing>().Where(t => t.StockBatchNo == stockInNo).FirstOrDefault();
                    if (Box != null)
                    {
                        foreach (var item in Param.SourceCodeNo.Split(',').ToList())
                        {
                            Box.ThingCode = Box.ThingCode.Replace(item, "");
                            var temp = Box.ThingCode.Split(",").ToList();
                            var res = temp.Where(t => !string.IsNullOrEmpty(t)).ToList();
                            Box.BoxCount = res.Count.ToString();
                            Box.ThingCode = string.Join(',', res);
                            List<string> Fields = new List<string>
                            {
                                "BoxCount","ThingCode"
                            };
                            UpdateField(Box, null, Fields);
                        }
                    }
                    //************************************************//
                    var VenTag = Regex.Matches(Param.SourceCodeNo, "e=\\d{12}").Select(t => Regex.Split(t.Groups[0].Value, "=")[1]).ToList();
                    var HostStar = Regex.Split(Regex.Match(Param.SourceCodeNo.Split(",")[0], "e=(.*)?").Groups[1].Value, "[W|P]")[0];
                    var OneTag = Regex.Matches(Param.SourceCodeNo, $"{HostStar}[W|P]\\d{{13}}").ToList();
                    if (OneTag.Count() == 0 && VenTag.Count() == 0)
                        return "请扫入溯源码或者纹理码";
                    if (OneTag.Count() != 0 && VenTag.Count() != 0)
                        return "请勿混用纹理二维码和追溯码";
                    var TagAttachs = Kily.Set<EnterpriseTagAttach>().Where(t => t.StockNo == stockInNo).AsNoTracking().ToList();
                    EnterpriseTagAttach TagAttach = null;
                    string UseTag = string.Empty;
                    if (OneTag.Count() != 0)
                    {
                        Param.OutStockNum = OneTag.Count;
                        foreach (var item in OneTag)
                        {
                            long No = item.Contains("W") ? Convert.ToInt64(item.Split("W")[1].Substring(0, 12)) : Convert.ToInt64(item.Split("P")[1].Substring(0, 12));
                            TagAttach = TagAttachs.Where(t => t.StarSerialNo <= No && t.EndSerialNo >= No).AsQueryable().AsNoTracking().FirstOrDefault();
                            //判断是否重复使用
                            if (!string.IsNullOrEmpty(TagAttach.UseTag))
                            {
                                if (TagAttach.UseTag.Split(',').Where(t => !string.IsNullOrEmpty(t)).ToList().Contains(item))
                                    UseTag += item + "|";
                                else
                                    TagAttach.UseTag += item + ",";
                            }
                            else
                            {
                                TagAttach.UseTag += item + ",";
                            }
                        }
                    }
                    else
                    {
                        Param.OutStockNum = VenTag.Count;
                        foreach (var item in VenTag)
                        {
                            long No = Convert.ToInt64(item.Substring(0, 11));
                            TagAttach = TagAttachs.Where(t => t.StarSerialNo <= No && t.EndSerialNo >= No).AsQueryable().AsNoTracking().FirstOrDefault();
                            //判断是否重复使用
                            if (!string.IsNullOrEmpty(TagAttach.UseTag))
                            {
                                if (TagAttach.UseTag.Split(',').Where(t => !string.IsNullOrEmpty(t)).ToList().Contains(item))
                                    UseTag += item + "|";
                                else
                                    TagAttach.UseTag += item + ",";
                            }
                            else
                            {
                                TagAttach.UseTag += item + ",";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(UseTag))
                    {
                        EnterpriseTagUseRecord record = new EnterpriseTagUseRecord
                        {
                            CompanyId = Param.CompanyId,
                            TagUser = CompanyInfo() == null ? CompanyUser().TrueName : CompanyInfo().CompanyName,
                            TagCount = UseTag.Split('|').Count() - 1,
                            Source = "重复使用出库异常",
                            Code = UseTag
                        };
                        Insert<EnterpriseTagUseRecord>(record);
                        return $"号段{UseTag}已经出库，请勿重复出库!";
                    }
                    else
                    {
                        EnterpriseTagUseRecord record = new EnterpriseTagUseRecord
                        {
                            CompanyId = Param.CompanyId,
                            TagUser = CompanyInfo() == null ? CompanyUser().TrueName : CompanyInfo().CompanyName,
                            TagCount = Param.OutStockNum,
                            Source = "正常出库",
                            Code = OneTag.Count() != 0 ? string.Join(",", OneTag) : string.Join(",", VenTag)
                        };
                        Insert<EnterpriseTagUseRecord>(record);
                        UpdateField(TagAttach, "UseTag");
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Param.Star) && !string.IsNullOrEmpty(Param.End))
                {
                    var stockInNo = Kily.Set<EnterpriseGoodsStock>().Where(t => t.Id == Param.StockId).Select(t => t.GoodsBatchNo).FirstOrDefault();
                    var TagAttachs = Kily.Set<EnterpriseTagAttach>().Where(t => t.StockNo == stockInNo).AsNoTracking().ToList();
                    EnterpriseTagAttach TagAttach = null;
                    string UseTag = string.Empty;
                    string TempTag = string.Empty;
                    if (Param.Star.Contains("W") || Param.Star.Contains("P"))
                    {
                        long SNo = Convert.ToInt64(Regex.Split(Param.Star, "[W|P]")[1]);
                        long ENo = Convert.ToInt64(Regex.Split(Param.End, "[W|P]")[1]);
                        String Host = Regex.Split(Param.End, "[W|P]")[0];
                        TagAttach = TagAttachs.Where(t => t.StarSerialNo <= SNo && t.EndSerialNo >= ENo).AsQueryable().AsNoTracking().FirstOrDefault();
                        string CodeType = TagAttach.StarSerialNos.Contains("W") ? "W" : "P";
                        for (long i = SNo; i <= ENo; i++)
                        {
                            string item = Host + CodeType + i;
                            TempTag += item + ",";
                            //判断是否重复使用
                            if (!string.IsNullOrEmpty(TagAttach.UseTag))
                            {
                                if (TagAttach.UseTag.Split(',').Where(t => !string.IsNullOrEmpty(t)).ToList().Contains(item))
                                    UseTag += item + "|";
                                else
                                    TagAttach.UseTag += item + ",";
                            }
                            else
                            {
                                TagAttach.UseTag += item + ",";
                            }
                        }
                        Param.OutStockNum = (int)(ENo - SNo) + 1;
                    }
                    else
                    {
                        long SNo = Convert.ToInt64(Param.Star);
                        long ENo = Convert.ToInt64(Param.End);
                        Param.OutStockNum = (int)(ENo - SNo) + 1;
                        TagAttach = TagAttachs.Where(t => t.StarSerialNo <= SNo && t.EndSerialNo >= ENo).AsQueryable().AsNoTracking().FirstOrDefault();
                        for (long i = SNo; i <= ENo; i++)
                        {
                            TempTag += i + ",";
                            //判断是否重复使用
                            if (!string.IsNullOrEmpty(TagAttach.UseTag))
                            {
                                if (TagAttach.UseTag.Split(',').Where(t => !string.IsNullOrEmpty(t)).ToList().Contains(i.ToString()))
                                    UseTag += i + "|";
                                else
                                    TagAttach.UseTag += i + ",";
                            }
                            else
                            {
                                TagAttach.UseTag += i + ",";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(UseTag))
                    {
                        EnterpriseTagUseRecord record = new EnterpriseTagUseRecord
                        {
                            CompanyId = Param.CompanyId,
                            TagUser = CompanyInfo() == null ? CompanyUser().TrueName : CompanyInfo().CompanyName,
                            TagCount = UseTag.Split('|').Count() - 1,
                            Source = "重复使用出库异常",
                            Code = UseTag
                        };
                        Insert<EnterpriseTagUseRecord>(record);
                        return $"号段{UseTag}已经出库，请勿重复出库!";
                    }
                    else
                    {
                        EnterpriseTagUseRecord record = new EnterpriseTagUseRecord
                        {
                            CompanyId = Param.CompanyId,
                            TagUser = CompanyInfo() == null ? CompanyUser().TrueName : CompanyInfo().CompanyName,
                            TagCount = Param.OutStockNum,
                            Source = "正常出库",
                            Code = TempTag
                        };
                        Insert<EnterpriseTagUseRecord>(record);
                        UpdateField(TagAttach, "UseTag");
                    }
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
            var data = Kily.Set<EnterpriseGoodsPackage>().Where(t => t.IsDelete == false).Select(t => t.ProductOutStockNo).ToList();
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            return queryable.Where(t => !string.IsNullOrEmpty(t.BoxCodeNo))
                .Where(t => !data.Contains(t.GoodsBatchNo))
                .Select(t => new ResponseEnterpriseGoodsStockAttach()
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

        /// <summary>
        /// 获取绑定出库的装箱码
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public List<string> GetBoxCodeNo(List<Guid> keys)
        {
            List<string> Codes = new List<string>();
            Kily.Set<EnterpriseGoodsStockAttach>().Where(t => keys.Contains(t.Id)).Select(t => new { t.BoxCodeNo }).Where(t => !string.IsNullOrEmpty(t.BoxCodeNo)).ToList().ForEach(item =>
              {
                  var Code = item.BoxCodeNo.Split("B")[1];
                  Codes.Add(Code);
              });
            return Codes.OrderBy(t => t).ToList();
        }

        #endregion 产品仓库

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

        #endregion 仓库类型

        #endregion 产品管理

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
            IQueryable<EnterpriseNote> note = Kily.Set<EnterpriseNote>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CheckName))
                queryable = queryable.Where(t => t.CheckName.Contains(pageParam.QueryParam.CheckName));
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            if (pageParam.QueryParam.Type == 10 || pageParam.QueryParam.Type == 20)
                return queryable.Join(note, t => t.NoteId, x => x.Id, (t, x) => new ResponseEnterpriseCheckGoods()
                {
                    Id = t.Id,
                    CheckName = t.CheckName,
                    GoodsName = x.NoteName,
                    CheckResult = t.CheckResult,
                    CheckUint = t.CheckUint,
                    CheckUser = t.CheckUser,
                    CheckReport = t.CheckReport
                }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            if (pageParam.QueryParam.Type == 30)
                return queryable.Join(queryables, t => t.GoodsId, x => x.Id, (t, x) => new ResponseEnterpriseCheckGoods()
                {
                    Id = t.Id,
                    CheckName = t.CheckName,
                    GoodsName = Kily.Set<EnterpriseProductSeries>().Where(o => o.Id == x.SeriesId).Select(o => o.SeriesName).FirstOrDefault(),
                    CheckResult = t.CheckResult,
                    CheckUint = t.CheckUint,
                    CheckUser = t.CheckUser,
                    CheckReport = t.CheckReport
                }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            else
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

        #endregion 原料产品质检

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
                InferNum = t.InferNum,
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

        #endregion 过期不合格处理

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

        #endregion 召回处理

        #endregion 品质管理

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
            try
            {
                var Num = Param.BoxCode.Split(",").ToList();
                Param.PackageNum = Num.Count;
                var data = Kily.Set<EnterpriseGoodsStockAttach>().Where(t => Param.ProductOutStockNo.Contains(t.GoodsBatchNo)).Select(t => t.BoxCodeNo).ToList();
                IQueryable<EnterpriseGoodsPackage> Package = Kily.Set<EnterpriseGoodsPackage>();
                if (CompanyInfo() != null)
                    Package = Package.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
                else
                    Package = Package.Where(t => t.CompanyId == CompanyUser().Id);
                var res = string.Join(",", Package.Select(t => t.BoxCode).ToList());
                foreach (var item in Num)
                {
                    if (res.Contains(item))
                        return $"当前装箱码：{item}已经被装车使用过！";
                }
                foreach (var item in data)
                {
                    if (!Param.BoxCode.Contains(item.Split(",")[0]))
                        return "当前批次不包括装箱码：" + item;
                }
                EnterpriseGoodsPackage package = Param.MapToEntity<EnterpriseGoodsPackage>();
                if (Param.Id == Guid.Empty)
                    return Insert(package) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
                else
                    return Update(package, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            }
            catch
            {
                return "请扫入装箱码";
            }
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

        #endregion 装车管理

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
                CorrectError = (t.Error / ((t.Error + t.Correct) == 0 ? 1 : (t.Error + t.Correct))) * 100
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
                 .Where(t => t.TakeCarId == pageParam.QueryParam.TakeCarId)
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
            if (Param.SendType == 1)
            {
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
            }
            else if (Param.SendType == 2)
            {
                Param.BoxCode = Param.BoxCode.Replace("\r\n", ",");
                var Num = Param.BoxCode.Split(",").ToList();
                if (string.IsNullOrEmpty(Num[Num.Count - 1]))
                    Num.RemoveAt(Num.Count - 1);
                var HostStar = Regex.Match(Num.FirstOrDefault(), "e=(.*)?").Groups[1].Value.Split("B")[0];
                var Nums = Regex.Matches(string.Join(",", Num), HostStar + "B(.*?)\\d{13}").ToList();
                IQueryable<EnterpriseBoxing> boxings = Kily.Set<EnterpriseBoxing>().Where(t => t.IsDelete == false);
                if (CompanyInfo() != null)
                    boxings = boxings.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
                else
                    boxings = boxings.Where(t => t.CompanyId == CompanyUser().Id);
                var Box = boxings.ToList();
                EnterpriseBoxing box = null;
                string UseTag = string.Empty;
                int num = 0;
                foreach (var item in Nums)
                {
                    box = Box.Where(t => t.BoxCode.Contains(item)).FirstOrDefault();
                    if (box == null)
                        return $"当前号段：{item}，未绑定！";
                    else
                    {
                        if (!string.IsNullOrEmpty(box.SendTag))
                        {
                            if (box.SendTag.Contains(item))
                                UseTag += item + "|";
                            else
                            {
                                box.SendTag += item + ",";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(UseTag))
                        return $"当前号段：{UseTag}，已经被发货使用过，请勿重复使用！";
                    UpdateField(box, "SendTag");
                    Param.GoodsName = Param.GoodsName.Split("_")[0];
                    num += box.ThingCode.Split(",").Count();
                }
                Param.SendGoodsNum = num.ToString();
            }
            else if (Param.SendType == 3)
            {
                var VenTag = Regex.Matches(Param.OneCode, "e=\\d{12}").Select(t => Regex.Split(t.Groups[0].Value, "=")[1]).ToList();
                var HostStar = Regex.Split(Regex.Match(Param.OneCode.Split(",")[0], "e=(.*)?").Groups[1].Value, "[W|P]")[0];
                var OneTag = Regex.Matches(Param.OneCode, $"{HostStar}[W|P]\\d{{13}}").ToList();
                if (OneTag.Count() == 0 && VenTag.Count() == 0)
                    return "请扫入溯源码或者纹理码";
                if (OneTag.Count() != 0 && VenTag.Count() != 0)
                    return "请勿混用纹理二维码和追溯码";
                var temp = Param.GoodsName;
                Param.GoodsName = temp.Split("_")[0];
                var GoodId = Guid.Parse(temp.Split("_")[1]);
                var TagAttachs = Kily.Set<EnterpriseTagAttach>().Where(t => t.GoodsId == GoodId).ToList();
                EnterpriseTagAttach TagAttach = null;
                string UseTag = string.Empty;
                string NoBing = string.Empty;
                //追溯码
                if (OneTag.Count() != 0)
                {
                    Param.SendGoodsNum = OneTag.Count().ToString();
                    foreach (var item in OneTag)
                    {
                        var Codes = Convert.ToInt64(Regex.Split(item, "[W|P]")[1].Substring(0, 12));
                        TagAttach = TagAttachs.Where(t => t.StarSerialNo <= Codes && t.EndSerialNo >= Codes).FirstOrDefault();
                        if (TagAttach == null)
                            NoBing += item + "|";
                        else
                        {
                            if (!string.IsNullOrEmpty(TagAttach.SendTag))
                            {
                                if (TagAttach.SendTag.Contains(item))
                                    UseTag += item + "|";
                                else
                                    TagAttach.SendTag += item + ",";
                            }
                            else
                                TagAttach.SendTag += item + ",";
                        }
                    }
                }
                if (VenTag.Count() != 0)
                {
                    Param.SendGoodsNum = VenTag.Count().ToString();
                    foreach (var item in VenTag)
                    {
                        var Codes = Convert.ToInt64(item.Substring(0, 11));
                        TagAttach = TagAttachs.Where(t => t.StarSerialNo <= Codes && t.EndSerialNo >= Codes).FirstOrDefault();
                        if (TagAttach == null)
                            NoBing += item + "|";
                        else
                        {
                            if (!string.IsNullOrEmpty(TagAttach.SendTag))
                            {
                                if (TagAttach.SendTag.Contains(item))
                                    UseTag += item + "|";
                                else
                                    TagAttach.SendTag += item + ",";
                            }
                            else
                                TagAttach.SendTag += item + ",";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(NoBing))
                    return $"当前选中的产品未绑定当前号段：{NoBing}，未绑定，请更换产品";
                if (!string.IsNullOrEmpty(UseTag))
                    return $"当前号段：{UseTag}，已经被发货使用过，请勿重复使用！";
                UpdateField(TagAttach, "SendTag");
            }
            else
            {
                var temp = Param.GoodsName;
                Param.GoodsName = temp.Split("_")[0];
                var GoodId = Guid.Parse(temp.Split("_")[1]);
                var TagAttachs = Kily.Set<EnterpriseTagAttach>().Where(t => t.GoodsId == GoodId).ToList();
                EnterpriseTagAttach TagAttach = null;
                string UseTag = string.Empty;
                string NoBing = string.Empty;
                String Temp = "http://phone.cfda.vip/NewPhone/CodeIndex.html?Id=&Code=";
                if (Param.Star.Contains("W") || Param.Star.Contains("P"))
                {
                    long SNo = Convert.ToInt64(Regex.Split(Param.Star, "[W|P]")[1]);
                    long ENo = Convert.ToInt64(Regex.Split(Param.End, "[W|P]")[1]);
                    String Host = Regex.Split(Param.End, "[W|P]")[0] + (Param.Star.Contains("W") ? "W" : "P");
                    Param.SendGoodsNum = (ENo - SNo + 1).ToString();
                    for (long i = SNo; i <= ENo; i++)
                    {
                        Param.OneCode += (Temp + Host + i + NormalUtil.CreateRandomNum() + ",");
                        TagAttach = TagAttachs.Where(t => t.StarSerialNo <= SNo && t.EndSerialNo >= i).AsQueryable().AsNoTracking().FirstOrDefault();
                        if (TagAttach == null)
                            NoBing += i + "|";
                        else
                        {
                            if (!string.IsNullOrEmpty(TagAttach.SendTag))
                            {
                                if (TagAttach.SendTag.Contains(Host + i.ToString()))
                                    UseTag += (Host + i + "|");
                                else
                                    TagAttach.SendTag += (Host + i + ",");
                            }
                            else
                                TagAttach.SendTag += (Host + i + ",");
                        }
                    }
                }
                else
                {
                    long SNo = Convert.ToInt64(Param.Star);
                    long ENo = Convert.ToInt64(Param.End);
                    Param.SendGoodsNum = (ENo - SNo + 1).ToString();
                    for (long i = SNo; i <= ENo; i++)
                    {
                        Param.OneCode += (Temp + i + NormalUtil.CreateRandomNum() + ",");
                        TagAttach = TagAttachs.Where(t => t.StarSerialNo <= SNo && t.EndSerialNo >= i).AsQueryable().AsNoTracking().FirstOrDefault();
                        if (TagAttach == null)
                            NoBing += i + "|";
                        else
                        {
                            if (!string.IsNullOrEmpty(TagAttach.SendTag))
                            {
                                if (TagAttach.SendTag.Contains(i.ToString()))
                                    UseTag += (i + "|");
                                else
                                    TagAttach.SendTag += (i + ",");
                            }
                            else
                                TagAttach.SendTag += (i + ",");
                        }
                    }
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

        #endregion 发货收货

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
                ProMerchant = t.ProMerchant,
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
            IQueryable<EnterpriseBuyer> queryable = Kily.Set<EnterpriseBuyer>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime).AsNoTracking();
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id || GetChildIdList(CompanyInfo().Id).Contains(t.CompanyId));
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyUser().Id);
            var data = queryable.Select(t => new ResponseEnterpriseBuyer()
            {
                Id = t.Id,
                BatchNo = t.BatchNo,
                Num = t.Num,
                GoodName = t.GoodName
            }).ToList();
            return data;
        }

        #endregion 进货管理

        #endregion 物流管理

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

        #endregion 导出Excel

        #region 数据统计

        /// <summary>
        /// 数据统计
        /// </summary>
        /// <returns></returns>
        public Object GetDataCount(Guid? Id)
        {
            List<EnterpriseProductSeries> series = Kily.Set<EnterpriseProductSeries>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id).ToList();
            List<EnterpriseGoods> goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id).ToList();
            List<EnterpriseSeller> sellers = Kily.Set<EnterpriseSeller>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id).ToList();
            List<EnterpriseInferiorExprired> exprireds = Kily.Set<EnterpriseInferiorExprired>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id).ToList();
            List<EnterpriseRecover> recovers = Kily.Set<EnterpriseRecover>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id).ToList();
            List<EnterpriseTagAttach> tagAttaches = Kily.Set<EnterpriseTagAttach>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id).ToList();
            List<EnterpriseScanCodeInfo> infos = Kily.Set<EnterpriseScanCodeInfo>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id).ToList();
            List<SystemMessage> msg = Kily.Set<SystemMessage>().Where(t => t.IsDelete == false).Where(t => t.CompanyId == Id).ToList();
            IList<DataPie> OutSideData = new List<DataPie>();
            IList<DataPie> OutSideDatas = new List<DataPie>();
            int Series = series.Select(t => t.Id).Count();
            int Goods = goods.GroupBy(t => t.ProductSeriesId).Count();
            int Supplier = sellers.Where(t => t.SellerType == SellerEnum.Supplier).Select(t => t.Id).Count();
            int Sale = sellers.Where(t => t.SellerType == SellerEnum.Sale).Select(t => t.Id).Count();
            OutSideData.Add(new DataPie { value = exprireds.Where(t => t.InferiorExprired == 1).Select(t => Convert.ToInt32(t.InferNum)).Sum(), name = "不合格数量" });
            OutSideData.Add(new DataPie { value = exprireds.Where(t => t.InferiorExprired == 2).Select(t => Convert.ToInt32(t.InferNum)).Sum(), name = "过期数量" });
            OutSideData.Add(new DataPie { value = recovers.Select(t => Convert.ToInt32(t.RecoverNum)).Sum(), name = "召回数量" });
            OutSideData.Add(new DataPie { value = msg.Select(t => t.Id).Count(), name = "投诉次数" });
            OutSideDatas.Add(new DataPie { value = tagAttaches.Where(t => t.TagType == "3").Sum(t => t.UseNum), name = "一品一码" });
            OutSideDatas.Add(new DataPie { value = tagAttaches.Where(t => t.TagType == "2").Sum(t => t.UseNum), name = "一物一码" });
            OutSideDatas.Add(new DataPie { value = tagAttaches.Where(t => t.TagType == "1").Sum(t => t.UseNum), name = "纹理二维码" });
            OutSideDatas.Add(new DataPie { value = infos.Sum(t => t.ScanNum), name = "扫码次数" });
            ResponseDataCount dataCount = new ResponseDataCount()
            {
                Name = "质量统计",
                Type = true,
                DataTitle = new List<string> { "不合格数量", "过期数量", "召回数量", "投诉次数" },
                InSideData = null,
                OutSideData = OutSideData
            };
            ResponseDataCount dataCounts = new ResponseDataCount()
            {
                Name = "溯源统计",
                Type = true,
                DataTitle = new List<string> { "一品一码", "一物一码", "纹理二维码", "扫码次数" },
                InSideData = null,
                OutSideData = OutSideDatas
            };
            Object obj = new { Series, Goods, Supplier, Sale, dataCount, dataCounts };
            return obj;
        }

        /// <summary>
        /// 产量统计
        /// </summary>
        /// <returns></returns>
        public Object GetPieCount(Guid? Id)
        {
            List<EnterpriseGoodsStock> queryable = Kily.Set<EnterpriseGoodsStock>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id).ToList();
            List<EnterpriseGoodsStockAttach> queryables = Kily.Set<EnterpriseGoodsStockAttach>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id).ToList();
            //上一周周产
            int WeekDataCount = queryable.Where(t => t.ProductTime > DateTime.Now.AddDays(-7)).Where(t => t.ProductTime < DateTime.Now).GroupJoin(queryables, t => t.Id, x => x.StockId, (t, x) => new
            {
                Total = t.InStockNum + x.Sum(o => o.OutStockNum)
            }).Sum(t => t.Total);
            //上两周周产
            int WeekDataCount_2 = queryable.Where(t => t.ProductTime > DateTime.Now.AddDays(-14)).Where(t => t.ProductTime < DateTime.Now).GroupJoin(queryables, t => t.Id, x => x.StockId, (t, x) => new
            {
                Total = t.InStockNum + x.Sum(o => o.OutStockNum)
            }).Sum(t => t.Total);
            //上一月
            int MonthDataCount = queryable.Where(t => t.ProductTime > DateTime.Now.AddMonths(-1))
                .Where(t => t.ProductTime < DateTime.Now)
                .GroupJoin(queryables, t => t.Id, x => x.StockId, (t, x) => new
                {
                    Total = t.InStockNum + x.Sum(o => o.OutStockNum)
                }).Sum(t => t.Total);
            //上两月
            int MonthDataCount_2 = queryable.Where(t => t.ProductTime > DateTime.Now.AddMonths(-1))
                .Where(t => t.ProductTime < DateTime.Now)
                .GroupJoin(queryables, t => t.Id, x => x.StockId, (t, x) => new
                {
                    Total = t.InStockNum + x.Sum(o => o.OutStockNum)
                }).Sum(t => t.Total);
            //上一年
            int YearDataCount = queryable.Where(t => t.ProductTime > DateTime.Now.AddYears(-1))
                .Where(t => t.ProductTime < DateTime.Now)
                .GroupJoin(queryables, t => t.Id, x => x.StockId, (t, x) => new
                {
                    Total = t.InStockNum + x.Sum(o => o.OutStockNum)
                }).Sum(t => t.Total);
            //前两年
            int YearDataCount_2 = queryable.Where(t => t.ProductTime > DateTime.Now.AddYears(-1))
                .Where(t => t.ProductTime < DateTime.Now).GroupJoin(queryables, t => t.Id, x => x.StockId, (t, x) => new
                {
                    Total = t.InStockNum + x.Sum(o => o.OutStockNum)
                }).Sum(t => t.Total);
            int Total = queryable.GroupJoin(queryables, t => t.Id, x => x.StockId, (t, x) => new
            {
                Total = t.InStockNum + x.Sum(o => o.OutStockNum)
            }).Sum(t => t.Total);
            int WeekSync = queryable.Where(t => t.ProductTime > DateTime.Now.AddDays(-7)).GroupJoin(queryables, t => t.Id, x => x.StockId, (t, x) => new
            {
                Total = t.InStockNum + x.Sum(o => o.OutStockNum)
            }).Sum(t => t.Total);
            return new { WeekDataCount, TwoWeek = WeekDataCount - WeekDataCount_2, MonthDataCount, TwoMonth = MonthDataCount - MonthDataCount_2, YearDataCount, TwoYear = YearDataCount - YearDataCount_2, Total, Totals = Total - WeekSync };
        }

        /// <summary>
        /// 批次统计
        /// </summary>
        /// <returns></returns>
        public ResponseDataCount GetPieCountBatch(Guid? Id)
        {
            List<EnterpriseNote> notes = Kily.Set<EnterpriseNote>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id).ToList();
            List<EnterpriseProductionBatch> batches = Kily.Set<EnterpriseProductionBatch>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id).ToList();
            List<EnterpriseBuyer> buyers = Kily.Set<EnterpriseBuyer>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id).ToList();
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

        #endregion 数据统计

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
                CommunityCode = t.CommunityCode,
                SafeOffer = t.SafeOffer,
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
                CompanyType = t.CompanyType,
                VideoAddress = t.VideoAddress,
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
        public BaseInfo GetScanCodeInfo(Guid? Id, String Code)
        {
            List<SqlParameter> Param = new List<SqlParameter>();
            String SearchCode = String.Empty;
            String PreFix = String.Empty;
            String SQL = String.Empty;
            int CodeType = 0;
            if (Code.Contains("W"))
            {
                SearchCode = Code.Split("W")[1].Substring(0, 12);
                PreFix = Code.Substring(0, 2);
                CodeType = 2;
                Param.Add(new SqlParameter("@Code", SearchCode));
                Param.Add(new SqlParameter("@CodeType", CodeType));
                Param.Add(new SqlParameter("@Fix", PreFix));
                SQL = SQLHelper.SQLBase + SQLHelper.CodeStar;
            }
            else if (Code.Contains("P"))
            {
                SearchCode = Code.Split("P")[1].Substring(0, 12);
                PreFix = Code.Substring(0, 2);
                CodeType = 3;
                Param.Add(new SqlParameter("@Code", SearchCode));
                Param.Add(new SqlParameter("@CodeType", CodeType));
                Param.Add(new SqlParameter("@Fix", PreFix));
                SQL = SQLHelper.SQLBase + SQLHelper.CodeStar;
            }
            else
            {
                SearchCode = Code.Substring(0, 11);
                CodeType = 1;
                Param.Add(new SqlParameter("@Code", SearchCode));
                Param.Add(new SqlParameter("@CodeType", CodeType));
                SQL = SQLHelper.SQLBase;
            }
            BaseInfo Base = Kily.ExecuteTable(SQL, Param.ToArray()).ToList<BaseInfo>().FirstOrDefault();
            EnterpriseGoodsPackage 装车 = Kily.Set<EnterpriseGoodsPackage>().Where(t => t.IsDelete == false).Where(t => t.ProductOutStockNo == Base.出库批次).FirstOrDefault();
            Base.装车编号 = 装车?.PackageNo;
            IQueryable<EnterpriseLogistics> 预发货 = Kily.Set<EnterpriseLogistics>().Where(t => t.IsDelete == false);
            var 产品系列 = Kily.Set<EnterpriseProductSeries>().Where(t => t.Id.ToString() == Base.产品系列).FirstOrDefault()??new EnterpriseProductSeries();
            Base.执行标准 = 产品系列.Standard;
            if (!string.IsNullOrEmpty(Base.装车编号))
            {
                var 预发货实体 = 预发货.Where(t => t.PackageNo.Equals(Base.装车编号)).Select(t => new BaseInfo
                {
                    装车标识 = t.Id.ToString(),
                    发货批次 = t.BatchNo,
                    运单号 = t.WayBill,
                    发货时间 = t.SendTime,
                    收货人 = t.GainUser,
                    收货地址 = t.Address,
                    发货地址 = t.SendAddress,
                    交通工具 = t.Traffic,
                    运输方式 = t.TransportWay,
                    收货标志 = t.Flag
                }).FirstOrDefault();
                Base.装车标识 = 预发货实体.装车标识;
                Base.发货批次 = 预发货实体.发货批次;
                Base.运单号 = 预发货实体.运单号;
                Base.发货时间 = 预发货实体.发货时间;
                Base.收货人 = 预发货实体.收货人;
                Base.收货地址 = 预发货实体.收货地址;
                Base.发货地址 = 预发货实体.发货地址;
                Base.交通工具 = 预发货实体.交通工具;
                Base.运输方式 = 预发货实体.运输方式;
                Base.收货标志 = 预发货实体.收货标志;
            }
            else
            {
                var 预发货列表 = 预发货.Select(t => new BaseInfo
                {
                    装车标识 = t.Id.ToString(),
                    发货绑定码 = (t.OneCode.ToUpper().Replace("http://phone.cfda.vip/newphone/codeindex.html?id=&Code=".ToUpper(), "")),
                    发货装箱码 = (t.BoxCode.ToUpper().Replace("http://phone.cfda.vip/newphone/codeindex.html?id=&Code=".ToUpper(), "")),
                    发货批次 = t.BatchNo,
                    运单号 = t.WayBill,
                    发货时间 = t.SendTime,
                    收货人 = t.GainUser,
                    收货人电话 = t.LinkPhone,
                    收货地址 = t.Address,
                    发货地址 = t.SendAddress,
                    交通工具 = t.Traffic,
                    运输方式 = t.TransportWay,
                    收货标志 = t.Flag
                }).ToList();
                var 预发货列表1 = 预发货列表.Where(t => !string.IsNullOrEmpty(t.发货绑定码)).ToList();

                var 预发货实体 = 预发货列表1.Where(t => t.发货绑定码.Contains(Code.ToUpper())).FirstOrDefault();
                if (预发货实体 == null)//箱码
                {
                    var BoxCode = Kily.Set<EnterpriseBoxing>().Where(t => t.ThingCode.Contains(Code)).FirstOrDefault().BoxCode;
                    var List = 预发货列表.Where(t => t.发货装箱码 != null).ToList();
                    预发货实体 = List.Where(t => t.发货装箱码.Contains(BoxCode.ToUpper())).FirstOrDefault();
                }
                Base.装车标识 = 预发货实体?.装车标识;
                Base.发货批次 = 预发货实体?.发货批次;
                Base.运单号 = 预发货实体?.运单号;
                Base.发货时间 = 预发货实体?.发货时间;
                Base.收货人 = 预发货实体?.收货人;
                Base.收货人电话 = 预发货实体?.收货人电话;
                Base.收货地址 = 预发货实体?.收货地址;
                Base.发货地址 = 预发货实体?.发货地址;
                Base.交通工具 = 预发货实体?.交通工具;
                Base.运输方式 = 预发货实体?.运输方式;
                Base.收货标志 = 预发货实体 == null ? false : 预发货实体.收货标志;
            }
            //生产企业
            if (Base.企业类型 == "30")
            {
                var 生产批次 = Kily.Set<EnterpriseProductionBatch>().Where(t => t.SeriesId.ToString() == Base.产品系列).FirstOrDefault();
                产品系列 = Kily.Set<EnterpriseProductSeries>().Where(t => t.Id.ToString() == Base.产品系列).FirstOrDefault();
                var 设备 = Kily.Set<EnterpriseDevice>().Where(t => t.DeviceName.Equals(生产批次.DeviceName)).FirstOrDefault();
                var 设施 = Kily.Set<EnterpriseFacilities>().Where(t => t.Id == 生产批次.FacId).FirstOrDefault();
                var 关键点 = Kily.Set<EnterpriseProductionBatchAttach>().Where(t => t.ProBatchId == 生产批次.Id).Select(t => new Target
                {
                    关键点名称 = t.TargetName,
                    关键点结果 = t.Result,
                    关键点限值 = t.TargetValue
                }).ToList();
                Base.生产批次号 = 生产批次.BatchNo;
                Base.生产时间 = 生产批次.StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                Base.设备名称 = 设备.DeviceName;
                Base.执行标准 = 产品系列.Standard;
                Base.产品系列 = 产品系列.SeriesName;
                Base.设备供应商 = 设备.SupplierName;
                Base.车间名称 = 设施.WorkShopName;
                Base.环境信息 = 设施.Environment;
                Base.Targets = 关键点;
                SqlParameter[] Mater = {
                    new SqlParameter("@Ids",生产批次.MaterialId)
                };
                var DataTable = Kily.ExecuteTable(SQLHelper.SQLMaterial, Mater);
                Base.Materials = DataTable != null ? DataTable.ToList<Material>() : null;
            }
            //种养殖企业
            if (Base.企业类型 == "10" || Base.企业类型 == "20")
            {
                var No = Kily.Set<EnterpriseNote>().Where(t => t.Id.ToString() == Base.成长档案).Select(t => t.BatchNo).FirstOrDefault();
                var Plants = Kily.Set<EnterprisePlanting>().AsQueryable();
                var Drugs = Kily.Set<EnterpriseDrug>().AsQueryable();
                var AgeUp = Kily.Set<EnterpriseAgeUp>().AsQueryable();
                if (Base.企业类型 == "10")
                {
                    Base.Plants = Plants.Where(t => t.IsType == 1).Select(t => new Plant
                    {
                        PlantTime = t.PlantTime,
                        FeedName = t.FeedName,
                        Producter = t.Producter
                    }).ToList();
                    Base.Drugs = Drugs.Where(t => t.IsType == 1).Select(t => new Drug
                    {
                        DrugName = t.DrugName,
                        PlantTime = t.PlantTime,
                        Producter = t.Producter
                    }).ToList();
                    Base.AgeUps = AgeUp.Where(t => t.BatchNo == No).Select(t => new AgeUp
                    {
                        LvName = t.LvName,
                        LvImg = t.LvImg
                    }).ToList();
                }
                else
                {
                    Base.Plants = Plants.Where(t => t.IsType == 2).Select(t => new Plant
                    {
                        PlantTime = t.PlantTime,
                        FeedName = t.FeedName,
                        Producter = t.Producter
                    }).ToList();
                    Base.Drugs = Drugs.Where(t => t.IsType == 2).Select(t => new Drug
                    {
                        DrugName = t.DrugName,
                        PlantTime = t.PlantTime,
                        Producter = t.Producter
                    }).ToList();
                    Base.AgeUps = AgeUp.Where(t => t.BatchNo == No).Select(t => new AgeUp
                    {
                        LvName = t.LvName,
                        LvImg = t.LvImg
                    }).ToList();
                }
                Base.Environs = Kily.Set<EnterpriseEnvironment>().Where(t => t.BatchNo == No).Select(t => new Environ
                {
                    AirEnv = t.AirEnv,
                    AirHdy = t.AirHdy,
                    SoilEnv = t.SoilEnv,
                    SoilHdy = t.SoilHdy
                }).ToList();
            }
            //流通企业
            if (Base.企业类型 == "40")
            {
                var 进货信息 = Kily.Set<EnterpriseBuyer>().Where(t => t.Id.ToString() == Base.进货信息).Select(t => new BaseInfo
                {
                    进货批次 = t.BatchNo,
                    进货产品 = t.GoodName,
                    产品产地 = t.Address,
                    进货产品供应商 = t.Supplier,
                    进货时间 = t.GetGoodsTime,
                    进货产品规格 = t.Spec,
                    生产时间 = t.ProTime.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    进货产品质检 = t.CheckReport,
                    进货生产商 = t.ProMerchant
                }).FirstOrDefault();
                Base.进货生产商地址 = Kily.Set<EnterpriseSeller>().Where(t => t.SellerType == SellerEnum.Production).Where(t => t.SupplierName.Equals(进货信息.进货生产商)).FirstOrDefault()?.Address;
                Base.进货批次 = 进货信息.进货批次;
                Base.进货产品 = 进货信息.进货产品;
                Base.产品产地 = 进货信息.产品产地;
                Base.生产时间 = 进货信息.生产时间;
                Base.进货产品供应商 = 进货信息.进货产品供应商;
                Base.进货时间 = 进货信息.进货时间;
                Base.进货生产商 = 进货信息.进货生产商;
                Base.进货产品规格 = 进货信息.进货产品规格;
                Base.进货产品质检 = 进货信息.进货产品质检;
            }
            BaseInfo Patrol = Kily.Set<GovtNetPatrol>().Where(t => t.CompanyId.ToString() == Base.企业).Select(t => new BaseInfo
            {
                抽查次数 = t.PotrolNum,
                通报次数 = t.BulletinNum,
                合格率 = t.QualifiedNum
            }).FirstOrDefault();
            Base.抽查次数 = Patrol != null ? Patrol.抽查次数 : 0;
            Base.通报次数 = Patrol != null ? Patrol.通报次数 : 0;
            Base.合格率 = Patrol?.合格率;
            Base.投诉次数 = Kily.Set<GovtComplain>().Where(t => t.CompanyId.ToString() == Base.企业).Count();
            EnterpriseRecover Recover = Kily.Set<EnterpriseRecover>().Where(t => t.CompanyId.ToString() == Base.企业).Where(t => t.RecoverGoodsName == Base.产品名称).Select(t => new EnterpriseRecover
            {
                RecoverStarTime = t.RecoverStarTime,
                RecoverEndTime = t.RecoverEndTime
            }).FirstOrDefault();
            Base.召回开始时间 = Recover?.RecoverStarTime;
            Base.召回截至时间 = Recover?.RecoverEndTime;
            Base.扫码次数 = Kily.Set<EnterpriseScanCodeInfo>().Where(t => t.ScanCode.Equals(Code)).Select(t => t.ScanNum).Sum();
            Base.Vedios = Kily.Set<EnterpriseVedio>().Where(t => t.CompanyId.ToString() == Base.企业).Where(t => t.IsIndex == true).Take(4).Select(t => new Vedio
            {
                视频地址 = t.VedioAddr,
                视频封面 = t.VedioCover,
                显示区域 = t.VedioName
            }).ToList();
            return Base;
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
                //.Where(t => t.ScanPackageNo.Equals(CodeInfo.ScanPackageNo))
                .Where(t => t.TakeCarId == Param.TakeCarId)
                .Where(t => t.ScanIP == Param.ScanIP)
                .AsNoTracking().FirstOrDefault();
            if (Code != null)
            {
                Code.ScanNum += 1;
                return UpdateField(Code, "ScanNum") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            }
            IQueryable<EnterpriseLogistics> queryable = Kily.Set<EnterpriseLogistics>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(CodeInfo.ScanPackageNo))
                queryable = queryable.Where(t => t.PackageNo == CodeInfo.ScanPackageNo);
            else
                queryable = queryable.Where(t => t.OneCode.Contains(CodeInfo.ScanCode));
            EnterpriseLogistics Log = queryable.FirstOrDefault()??new EnterpriseLogistics();
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
            IQueryable<EnterpriseBoxing> queryable = Kily.Set<EnterpriseBoxing>().Where(t => t.BoxCode.Contains(Code)).Where(t => t.IsDelete == false);
            var data = queryable.FirstOrDefault().MapToEntity<ResponseEnterpriseBoxing>();
            return data;
        }

        /// <summary>
        /// 发货信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public RequestEnterpriseLogistics GetScanSendInfo(String Id, String Phone, String Num)
        {
            var data = Kily.Set<EnterpriseLogistics>().Where(t => t.GainUser == Id && t.LinkPhone == Phone && t.SendGoodsNum == Num).FirstOrDefault().MapToEntity<RequestEnterpriseLogistics>();
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
                return "请勿窜货";
            EnterpriseLogistics logistics = Kily.Set<EnterpriseLogistics>().Where(t => t.GainId == Temp.Id).FirstOrDefault();
            logistics.Flag = false;
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

        /// <summary>
        ///  获取包详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Object GetPackCodeInfo(Guid Id)
        {
            EnterpriseTag Tag = Kily.Set<EnterpriseTag>().Where(t => t.Id == Id).FirstOrDefault();
            return Kily.Set<EnterprisePackCodeBind>().Where(t => t.TagId == Tag.Id).FirstOrDefault();
        }
        #endregion 手机扫描页面

        #region APP 接口

        /// <summary>
        /// 数据统计
        /// </summary>
        /// <returns></returns>
        public Object GetAppDataCount(Guid? Id)
        {
            List<EnterpriseGoods> goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id).ToList();
            List<EnterpriseTagAttach> tagAttaches = Kily.Set<EnterpriseTagAttach>().Where(t => t.IsDelete == false).AsNoTracking().Where(t => t.CompanyId == Id).ToList();
            List<SystemMessage> msg = Kily.Set<SystemMessage>().Where(t => t.IsDelete == false).Where(t => t.CompanyId == Id).ToList();

            Object obj = new { GoodCount = goods.Count, TagCount = tagAttaches.Count, MsgCount = msg.Count };
            return obj;
        }

        /// <summary>
        /// 风险预警
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtRisk> GetWaringRiskPage(PageParamList<RequestGovtRisk> pageParam)
        {
            IQueryable<GovtRisk> queryable = Kily.Set<GovtRisk>().OrderByDescending(t => t.CreateTime);
            queryable = queryable.Where(t => t.TypePath.Contains(CompanyInfo().City));
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
        /// 整改意见
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseSystemMessage> GetNetPatrolPage(PageParamList<Object> pageParam)
        {
            IQueryable<SystemMessage> queryable = Kily.Set<SystemMessage>().OrderByDescending(t => t.ReleaseTime);
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id);
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id);
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
            if (CompanyInfo() != null)
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id);
            else
                queryable = queryable.Where(t => t.CompanyId == CompanyInfo().Id);
            var data = queryable.Select(t => new ResponseGovtComplain()
            {
                Id = t.Id,
                ComplainUser = t.ComplainUser,
                ComplainContent = t.ComplainContent,
                ComplainUserPhone = t.ComplainUserPhone,
                HandlerContent = t.HandlerContent,
                ProductName = t.ProductName,
                CompanyName = t.CompanyName,
                Status = t.Status,
                ComplainTime = t.ComplainTime
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        #endregion APP 接口
    }
}