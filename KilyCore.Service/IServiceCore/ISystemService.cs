using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 系统业务逻辑接口
    /// </summary>
    public interface ISystemService : IService
    {
        #region 用户登陆
        ResponseAdmin SystemLogin(RequestValidate LoginValidate);
        #endregion
        #region 系统菜单
        IList<ResponseMenu> GetSystemMenu();
        IList<ResponseMenu> GetParentMenu();
        PagedResult<ResponseMenu> GetMenuPage(PageParamList<RequestMenu> pageParam);
        ResponseMenu GetMenuDetail(Guid Id);
        String RemoveMenu(Guid Id);
        String EditMenu(RequestMenu Param);
        #endregion
        #region 角色权限
        IList<ResponseRoleLv> GetRoleLv();
        String EditRole(RequestAuthorRole param);
        PagedResult<ResponseAuthorRole> GetAuthorPage(PageParamList<RequestAuthorRole> pageParam);
        String RemoveAuthorRole(Guid Id);
        IList<ResponseAuthorRole> GetAuthorRole();
        #endregion
        #region 区域树
        IList<ResponseTree> GetSystemAreaTree();
        IList<ResponseTree> GetSystemAreaTrees();
        #endregion
        #region 权限菜单树
        IList<ResponseParentTree> GetSystemParentTree();
        #endregion
        #region 省市区
        IList<ResponseProvince> GetProvince();
        IList<ResponseCity> GetCity(int Pid);
        IList<ResponseArea> GetArea(int Cid);
        IList<ResponseTown> GetTown(int Aid);
        #endregion
        #region 用户管理
        String EditAdmin(RequestAdmin Param);
        PagedResult<ResponseAdmin> GetAdminPage(PageParamList<RequestAdmin> pageParam);
        String RemoveAdmin(Guid Id);
        ResponseAdmin GetAdminDetail(Guid Id);
        #endregion
    }
}
