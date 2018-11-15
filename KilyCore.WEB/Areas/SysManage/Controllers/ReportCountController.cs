using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.SysManage.Controllers
{
    [Area("SysManage")]
    public class ReportCountController : Controller
    {
        public IActionResult CodeCenter()
        {
            return View();
        }
        public IActionResult ProductCenter()
        {
            return View();
        }
        public IActionResult CompanyCenter()
        {
            return View();
        }
    }
}