using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Controllers
{
    public class CommonController : Controller
    {
        public IActionResult Msg()
        {
            return View();
        }
    }
}