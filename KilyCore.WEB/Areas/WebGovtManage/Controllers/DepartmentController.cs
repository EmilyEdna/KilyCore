using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebGovtManage.Controllers
{
    [Area("WebGovtManage")]
    public class DepartmentController : Controller
    {
        public IActionResult InstitutionDepartment()
        {
            return View();
        }
        public IActionResult UserDepartment()
        {
            return View();
        }
    }
}