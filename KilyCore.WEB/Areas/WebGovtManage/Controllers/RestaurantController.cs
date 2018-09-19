using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebGovtManage.Controllers
{
    [Area("WebGovtManage")]
    public class RestaurantController : Controller
    {
        public IActionResult SamllSupervise()
        {
            return View();
        }
        public IActionResult BanquetSupervise()
        {
            return View();
        }
        public IActionResult UnitSupervise()
        {
            return View();
        }
        public IActionResult SamllSuperviseDetail()
        {
            return View();
        }
        public IActionResult BanquetSuperviseDetail()
        {
            return View();
        }
        public IActionResult UnitSuperviseDetail()
        {
            return View();
        }
    }
}