using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebCompanyManage.Controllers
{
    public class LogisticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}