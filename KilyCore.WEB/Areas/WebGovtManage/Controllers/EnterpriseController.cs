using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebGovtManage.Controllers
{
    [Area("WebGovtManage")]
    public class EnterpriseController : Controller
    {
        public IActionResult PlantSupervise()
        {
            return View();
        }
        public IActionResult BreedSupervise()
        {
            return View();
        }
        public IActionResult CirculationSupervise()
        {
            return View();
        }
        public IActionResult ProductionSupervise()
        {
            return View();
        }
        public IActionResult OhterSupervise()
        {
            return View();
        }
        public IActionResult RepastSupervise() {
            return View();
        }
    }
}