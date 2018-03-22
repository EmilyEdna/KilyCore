using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.ComManage.Controllers
{
    [Area("ComManage")]
    public class CompanyController : Controller
    {
        #region 资料审核
        public IActionResult AuditInfo()
        {
            return View();
        }
        public IActionResult CompanyDetail()
        {
            return View();
        }
        public IActionResult AuditCompany()
        {
            return View();
        }
        #endregion
        #region 企业认证
        public IActionResult CompanyIdent()
        {
            return View();
        }
        public IActionResult AuditIdent()
        {
            return View();
        }
        public IActionResult CompanyIdentDetail()
        {
            return View();
        }
        #endregion
    }
}