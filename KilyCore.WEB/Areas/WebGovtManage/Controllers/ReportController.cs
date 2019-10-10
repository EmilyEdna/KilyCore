using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebGovtManage.Controllers
{
    [Area("WebGovtManage")]
    public class ReportController : Controller
    {
        public IActionResult ProductBill()
        {
            return View();
        }
        public IActionResult CompanyBill()
        {
            return View();
        }
        public IActionResult AreaBill()
        {
            return View();
        }
    }
}