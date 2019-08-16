using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebMerchantManage.Controllers
{
    [Area("WebMerchantManage")]
    public class OrderController: Controller
    {
        public IActionResult OrderPush()
        {
            return View();
        }
        public IActionResult OrderPushEidt()
        {
            return View();
        }
        public IActionResult OrderService()
        {
            return View();
        }
        public IActionResult OrderServiceEidt()
        {
            return View();
        }
    }
}
