using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Controllers
{
    public class RepastController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Regist()
        {
            return View();
        }

        public IActionResult DataCounts()
        {
            return View();
        }
    }
}