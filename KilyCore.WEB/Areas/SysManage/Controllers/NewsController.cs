using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.SysManage.Controllers
{
    [Area("SysManage")]
    public class NewsController : Controller
    {
        public IActionResult NewsCenter()
        {
            return View();
        }
        public IActionResult NewsCenterEdit()
        {
            return View();
        }
    }
}