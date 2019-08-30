using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebMerchantManage.Controllers
{
    [Area("WebMerchantManage")]
    public class UnitInsController : Controller
    {
        public IActionResult InsList()
        {
            return View();
        }
        public IActionResult InsEdit()
        {
            return View();
        }
        public IActionResult InsRecordList()
        {
            return View();
        }
        public IActionResult InsRecordEdit()
        {
            return View();
        }
    }
}