using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.SysManage.Controllers
{
    [Area("SysManage")]
    public class OrderController : Controller
    {
        public IActionResult OrderList()
        {
            return View();
        }
        public IActionResult OrderListDetail()
        {
            return View();
        }
        public IActionResult OrderLog()
        {
            return View();
        }
        public IActionResult OrderLogDetail()
        {
            return View();
        }
        public IActionResult OrderScore()
        {
            return View();
        }
        public IActionResult OrderScoreDetail()
        {
            return View();
        }
        public IActionResult OrderOff()
        {
            return View();
        }
        public IActionResult OrderOffDetail()
        {
            return View();
        }
    }
}