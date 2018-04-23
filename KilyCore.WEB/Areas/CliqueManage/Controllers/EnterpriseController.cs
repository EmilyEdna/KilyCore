using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.CliqueManage.Controllers
{
    [Area("CliqueManage")]
    public class EnterpriseController : Controller
    {
        #region 集团菜单
        public IActionResult EnterpriseMenu()
        {
            return View();
        }
        public IActionResult EnterpriseMenuEdit()
        {
            return View();
        }
        #endregion

        #region 集团角色
        public IActionResult EnterpriseRole()
        {
            return View();
        }
        public IActionResult EnterpriseRoleEdit()
        {
            return View();
        }
        public IActionResult WatchRole() {
            return View();
        }
        #endregion

        #region 资料审核
        public IActionResult AuditData()
        {
            return View();
        }
        public IActionResult AuditDataDetail()
        {
            return View();
        }
        public IActionResult AuditDataInfo()
        {
            return View();
        }
        #endregion

        #region 企业认证
        public IActionResult AuditIdent()
        {
            return View();
        }
        public IActionResult AuditIdentDetail()
        {
            return View();
        }
        public IActionResult AuditIdentInfo()
        {
            return View();
        }
        public IActionResult Payment()
        {
            return View();
        }
        #endregion
    }
}