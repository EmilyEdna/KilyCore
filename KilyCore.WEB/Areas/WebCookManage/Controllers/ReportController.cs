using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebCookManage.Controllers
{
    [Area("WebCookManage")]
    public class ReportController : Controller
    {
        public IActionResult CookFeast()
        {
            return View();
        }
        public IActionResult CookFeastEdit()
        {
            return View();
        }
        public IActionResult CookHelp()
        {
            return View();
        }
        public IActionResult CookHelpEdit()
        {
            return View();
        }
        public IActionResult CookAgree()
        {
            return View();
        }
        public IActionResult CookAgreeEdit()
        {
            return View();
        }
        public IActionResult CookFood()
        {
            return View();
        }
        public IActionResult CookFoodEdit()
        {
            return View();
        }
    }
}