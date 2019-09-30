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
        public IActionResult CategoryPatrolAttach()
        {
            return View();
        }
        public IActionResult NetPatrolEdit()
        {
            return View();
        }
        public IActionResult NetPatrolDetail()
        {
            return View();
        }
        public IActionResult CategoryPatrolEdit()
        {
            return View();
        }
        public IActionResult CategoryPatrolAttachEdit()
        {
            return View();
        }
        public IActionResult MovePatrolEdit()
        {
            return View();
        }
        public IActionResult CompanyPatrol()
        {
            return View();
        }
        public IActionResult CompanyPatrolEdit() {
            return View();
        }
        public IActionResult CompanyPatrolDetail()
        {
            return View();
        }
        public IActionResult CompanyPatrolAttachEdit()
        {
            return View();
        }
        public IActionResult CompanyPatrolAttach()
        {
            return View();
        }
        public IActionResult HandlerLog() {
            return View();
        }
    }
}