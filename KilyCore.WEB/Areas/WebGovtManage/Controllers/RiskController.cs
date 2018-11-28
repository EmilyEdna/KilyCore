using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebGovtManage.Controllers
{
    [Area("WebGovtManage")]
    public class RiskController : Controller
    {
        public IActionResult EventRisk()
        {
            return View();
        }
        public IActionResult WaringRisk()
        {
            return View();
        }
        public IActionResult CertificateRisk()
        {
            return View();
        }
        public IActionResult WaringRiskEdit()
        {
            return View();
        }
    }
}