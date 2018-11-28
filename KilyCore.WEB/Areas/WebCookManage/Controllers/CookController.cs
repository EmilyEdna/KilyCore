using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebCookManage.Controllers
{
    [Area("WebCookManage")]
    public class CookController : Controller
    {
        public IActionResult CookAccount()
        {
            return View();
        }
        public IActionResult CookAccountEdit()
        {
            return View();
        }
        public IActionResult CookInfo()
        {
            return View();
        }
        public IActionResult Payment()
        {
            return View();
        }
    }
}