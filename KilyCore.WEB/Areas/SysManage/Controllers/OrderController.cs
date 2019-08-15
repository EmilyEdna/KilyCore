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
    }
}