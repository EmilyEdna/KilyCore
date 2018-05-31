using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebCompanyManage.Controllers
{
    [Area("WebCompanyManage")]
    public class SellerController : Controller
    {
        public IActionResult Supplier()
        {
            return View();
        }
        public IActionResult Production()
        {
            return View();
        }
        public IActionResult ProductionEdit()
        {
            return View();
        }
    }
}