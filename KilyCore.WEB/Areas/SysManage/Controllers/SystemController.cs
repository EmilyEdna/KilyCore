using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.SysManage.Controllers
{
    [Area("SysManage")]
    public class SystemController : Controller
    {
        #region 菜单
        public IActionResult SysMenu()
        {
            return View();
        }
        public IActionResult SysMenuEdit()
        {
            return View();
        }
        public IActionResult SysCompanyMenu()
        {
            return View();
        }
        public IActionResult SysCompanyMenuEdit()
        {
            return View();
        }
        #endregion
        #region 角色
        public IActionResult SysRole()
        {
            return View();
        }
        #endregion
        #region 用户
        public IActionResult SysUser()
        {
            return View();
        }
        public IActionResult SysUserEdit()
        {
            return View();
        }
        #endregion
        #region 任务
        public IActionResult SysQuartz()
        {
            return View();
        }
        public IActionResult SysQuartzEdit()
        {
            return View();
        }
        #endregion
    }
}