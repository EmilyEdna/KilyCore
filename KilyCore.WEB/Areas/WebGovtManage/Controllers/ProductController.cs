using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebGovtManage.Controllers
{
    [Area("WebGovtManage")]
    public class ProductController : Controller
    {
        public IActionResult PlantSupervise()
        {
            return View();
        }
        public IActionResult DuckSupervise()
        {
            return View();
        }
        public IActionResult GoodSupervise()
        {
            return View();
        }
        public IActionResult EquipSupervise()
        {
            return View();
        }
        public IActionResult EdibleSupervise()
        {
            return View();
        }
        public IActionResult WorkSupervise()
        {
            return View();
        }
        public IActionResult WorkSuperviseDetail()
        {
            return View();
        }
        public IActionResult EdibleSuperviseDetail()
        {
            return View();
        }
        public IActionResult OtherSupervise()
        {
            return View();
        }
        public IActionResult OtherSuperviseDetail()
        {
            return View();
        }
    }
}