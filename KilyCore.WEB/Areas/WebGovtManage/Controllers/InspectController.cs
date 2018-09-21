using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebGovtManage.Controllers
{
    [Area("WebGovtManage")]
    public class InspectController : Controller
    {
        public IActionResult NetPatrol()
        {
            return View();
        }
        public IActionResult MovePatrol()
        {
            return View();
        }
        public IActionResult CategoryPatrol()
        {
            return View();
        }
        public IActionResult NetPatrolEdit()
        {
            return View();
        }
    }
}