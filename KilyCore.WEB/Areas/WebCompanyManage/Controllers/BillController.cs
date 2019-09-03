using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebCompanyManage.Controllers
{
    /// <summary>
    /// 台账管理
    /// </summary>
    [Area("WebCompanyManage")]
    public class BillController : Controller
    {
        public IActionResult TickPrint()
        {
            return View();
        }
    }
}