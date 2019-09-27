using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebGovtManage.Controllers
{
    [Area("WebGovtManage")]
    public class ComplainController : Controller
    {
        public IActionResult InfoComplain()
        {
            return View();
        }
        public IActionResult InfoComplainDetail()
        {
            return View();
        }
    }
}