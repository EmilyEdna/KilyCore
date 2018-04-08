using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.Model.System;
using KilyCore.EntityFrameWork.ModelEnum;
using KilyCore.Extension.AttributeExtension;
using KilyCore.Extension.AutoMapperExtension;
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
using System.Threading.Tasks;

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
                   .Where(t => t.IsDelete == false).AsNoTracking().AsQueryable().OrderBy(t=>t.CreateTime);
            if (UserInfo().AccountType == AccountEnum.Admin)
            {
                var data = queryable.Select(t => new ResponseMenu()
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
                var data = queryable.Select(t => new ResponseMenu()
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
        #endregion

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
        #endregion
        #endregion

        #region 用户管理
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditAdmin(RequestAdmin Param)
        {
            //根据角色权限类型获取角色权限等级
            SystemRoleAuthor Author = Kily.Set<SystemRoleAuthor>().Where(t => t.Id == Param.RoleAuthorType).AsNoTracking().FirstOrDefault();
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
            if (Param.Id != Guid.Empty)
            {
                if (Update<SystemAdmin, RequestAdmin>(Admin, Param))
                    return ServiceMessage.UPDATESUCCESS;
                else
                    return ServiceMessage.UPDATEFAIL;
            }
            else
            {
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
            IQueryable<SystemAdmin> queryable = Kily.Set<SystemAdmin>().Where(t => t.IsDelete == false).AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.TrueName))
                queryable = queryable.Where(t => t.TrueName.Contains(pageParam.QueryParam.TrueName));
            if (!string.IsNullOrEmpty(pageParam.QueryParam.AreaTree))
                queryable = queryable.Where(t => t.TypePath.Contains(pageParam.QueryParam.AreaTree));
            if (UserInfo().AccountType == AccountEnum.Country)
                queryable = queryable.Where(t => t.AccountType >= AccountEnum.Country);
            if (UserInfo().AccountType == AccountEnum.Province)
                queryable = queryable.Where(t => t.AccountType >= AccountEnum.Province);
            if (UserInfo().AccountType == AccountEnum.City)
                queryable = queryable.Where(t => t.AccountType >= AccountEnum.City);
            if (UserInfo().AccountType == AccountEnum.Area)
                queryable = queryable.Where(t => t.AccountType >= AccountEnum.Area);
            if (UserInfo().AccountType == AccountEnum.Village)
                queryable = queryable.Where(t => t.AccountType >= AccountEnum.Village);
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseAdmin()
            {
                Id = t.Id,
                TrueName = t.TrueName,
                Account = t.Account,
                AccountTypeName = AttrExtension.GetSingleDescription<AccountEnum, DescriptionAttribute>(t.AccountType),
                Phone = t.Phone
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
                     TrueName = t.TrueName,
                     PassWord = t.PassWord,
                     Phone = t.Phone,
                     Email = t.Email,
                     IdCard = t.IdCard,
                     RoleAuthorType = t.RoleAuthorType,
                     TypePath = t.TypePath
                 }).FirstOrDefault();
        }
        #endregion

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
                queryable = queryable.Where(t => t.RoleLv >= RoleEnum.LV2).Where(t => t.RoleLv <= RoleEnum.LV3);
            if (UserInfo().AccountType == AccountEnum.Province)
                queryable = queryable.Where(t => t.RoleLv >= RoleEnum.LV3).Where(t => t.RoleLv <= RoleEnum.LV4);
            if (UserInfo().AccountType == AccountEnum.City)
                queryable = queryable.Where(t => t.RoleLv >= RoleEnum.LV4).Where(t => t.RoleLv <= RoleEnum.LV5);
            if (UserInfo().AccountType == AccountEnum.Area)
                queryable = queryable.Where(t => t.RoleLv >= RoleEnum.LV5).Where(t => t.RoleLv <= RoleEnum.LV6);
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
        #endregion

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
                query = query.Where(t => t.RoleLv >= RoleEnum.LV2).Where(t => t.RoleLv <= RoleEnum.LV3);
            if (UserInfo().AccountType == AccountEnum.Province)
                query = query.Where(t => t.RoleLv >= RoleEnum.LV3).Where(t => t.RoleLv <= RoleEnum.LV4);
            if (UserInfo().AccountType == AccountEnum.City)
                query = query.Where(t => t.RoleLv >= RoleEnum.LV4).Where(t => t.RoleLv <= RoleEnum.LV5);
            if (UserInfo().AccountType == AccountEnum.Area)
                query = query.Where(t => t.RoleLv >= RoleEnum.LV5).Where(t => t.RoleLv <= RoleEnum.LV6);
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
                AuthorRoleLv = x.LvName
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        #endregion

        #region 角色添加删除
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string EditRole(RequestAuthorRole param)
        {
            SystemRoleAuthor Author = param.MapToEntity<SystemRoleAuthor>();
            if (param.AuthorRoleLvId == null)
                return "请选择权限等级!";
            if (string.IsNullOrEmpty(param.TypePath))
                return "请选中所属区域!";
            if (Kily.Set<SystemRoleAuthor>().Where(t => t.IsDelete == false).Where(t => t.AuthorName.Equals(Author.AuthorName)).AsNoTracking().FirstOrDefault() != null)
            {
                return "角色名称重复请重新添加!";
            }
            else
            {
                if (Insert<SystemRoleAuthor>(Author))
                    return ServiceMessage.INSERTSUCCESS;
                else
                    return ServiceMessage.INSERTFAIL;
            }
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveAuthorRole(Guid Id)
        {
            if (Delete<SystemRoleAuthor>(ExpressionExtension.GetExpression<SystemRoleAuthor>("Id", Id, ExpressionEnum.Equals)))
            {
                if (Delete<SystemAdmin>(ExpressionExtension.GetExpression<SystemAdmin>("RoleAuthorType", Id, ExpressionEnum.Equals)))
                    return ServiceMessage.REMOVESUCCESS;
                else
                    return ServiceMessage.REMOVEFAIL;
            }
            else
                return ServiceMessage.REMOVEFAIL;
        }
        #endregion

        #region 获取权限列表
        /// <summary>
        /// 获取权限下拉列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseAuthorRole> GetAuthorRole()
        {
            return Kily.Set<SystemRoleAuthor>().Where(t => t.IsDelete == false)
                .Where(t => t.CreateUser == UserInfo().Id.ToString()).AsNoTracking()
                 .Select(t => new ResponseAuthorRole()
                 {
                     AuthorName = t.AuthorName,
                     Id = t.Id
                 }).ToList();
        }
        #endregion
        #endregion

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
           .Where(t => t.IsDelete == false)
           .AsNoTracking().AsQueryable().Select(t => new ResponseAdmin()
           {
               Id = t.Id,
               TrueName = t.TrueName,
               Account = t.Account,
               TypePath = t.TypePath,
               IdCard = t.IdCard,
               Phone = t.Phone,
               Email = t.Email,
               AccountType = t.AccountType,
               RoleAuthorType = t.RoleAuthorType
           }).FirstOrDefault();
            if (null != Admin)
            {
                Cache.WriteCache<ResponseAdmin>(Admin, Configer.ClientIP, 2);
                return Admin;
            }
            else
                return null;
        }
        #endregion

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
        #endregion

        #region 权限菜单树
        /// <summary>
        /// 获取权限菜单树
        /// </summary>
        /// <returns></returns>
        public IList<ResponseParentTree> GetSystemAdminTree()
        {
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
                         Nodes = Kily.Set<SystemMenu>().Where(x => x.IsDelete == false)
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
                         BackClolor = "white",
                         SelectedIcon = "fa fa-refresh fa-spin",
                         Nodes = Kily.Set<SystemMenu>().Where(x => x.IsDelete == false)
                         .Where(x => x.Level != MenuEnum.LevelOne)
                         .Where(x => x.ParentId == t.MenuId).AsNoTracking()
                         .Where(x => Author.AuthorMenuPath.Contains(x.Id.ToString()))
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
        #endregion

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
        #endregion
    }
}
