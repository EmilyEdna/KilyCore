using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebGovtManage.Controllers
{
    [Area("WebGovtManage")]
    public class AppController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DataTotal()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult BookOut()
        {
            return View();
        }
        public IActionResult Partys()
        {
            return View();
        }
        public IActionResult Products()
        {
            return View();
        }
        public IActionResult Trains()
        {
            return View();
        }
        public IActionResult Warns()
        {
            return View();
        }
        public IActionResult CompanyCount()
        {
            return View();
        }
        public IActionResult ProductCount()
        {
            return View();
        }
        public IActionResult WarnsCount()
        {
            return View();
        }
        public IActionResult CompanyWarn()
        {
            return View();
        }
    }
}