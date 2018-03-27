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
        public IActionResult IdentAudit()
        {
            return View();
        }
        public IActionResult IdentAuditDetail()
        {
            return View();
        }
        public IActionResult IdentAuditInfo()
        {
            return View();
        }
        #endregion
    }
}