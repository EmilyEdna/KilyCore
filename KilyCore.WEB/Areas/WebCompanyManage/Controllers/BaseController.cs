using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebCompanyManage.Controllers
{
    [Area("WebCompanyManage")]
    public class BaseController : Controller
    {
        public IActionResult CompanyData()
        {
            return View();
        }
    }
}