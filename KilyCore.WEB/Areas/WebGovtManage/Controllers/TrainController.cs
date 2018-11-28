using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebGovtManage.Controllers
{
    [Area("WebGovtManage")]
    public class TrainController : Controller
    {
        public IActionResult NoticeTrain()
        {
            return View();
        }
        public IActionResult ReportTrain()
        {
            return View();
        }
        public IActionResult ReportTrainEdit()
        {
            return View();
        }
        public IActionResult NoticeTrainEdit()
        {
            return View();
        }
    }
}