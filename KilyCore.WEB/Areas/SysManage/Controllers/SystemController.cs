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
        public IActionResult SystemMenu()
        {
            return View();
        }
        public IActionResult SystemMenuEdit()
        {
            return View();
        }
        #endregion
        #region 角色
        public IActionResult SystemRole()
        {
            return View();
        }
        #endregion
        #region 用户
        public IActionResult SystemUser()
        {
            return View();
        }
        public IActionResult SystemUserEdit()
        {
            return View();
        }
        #endregion
        #region 任务
        public IActionResult SystemQuartz()
        {
            return View();
        }
        public IActionResult SystemQuartzEdit()
        {
            return View();
        }
        #endregion
        #region 人员
        public IActionResult SystemPreson()
        {
            return View();
        }
        public IActionResult SystemPresonEdit()
        {
            return View();
        }
        #endregion
        #region 合同
        public IActionResult SystemEnterpriseContract()
        {
            return View();
        }
        public IActionResult SystemAuditContract()
        {
            return View();
        }
        public IActionResult SystemAuditRecord()
        {
            return View();
        }
        public IActionResult SystemContinued()
        {
            return View();
        }
        public IActionResult SystemUpdate()
        {
            return View();
        }
        public IActionResult SystemRepastContract()
        {
            return View();
        }
        #endregion
        public IActionResult SystemPayment()
        {
            return View();
        }
    }
}