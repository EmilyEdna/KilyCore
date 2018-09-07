using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.GovtManage.Controllers
{
    [Area("GovtManage")]
    public class GovtController : Controller
    {
        public IActionResult GovtMenu()
        {
            return View();
        }
        public IActionResult GovtMenuEdit()
        {
            return View();
        }
        public IActionResult GovtRoler()
        {
            return View();
        }
    }
}