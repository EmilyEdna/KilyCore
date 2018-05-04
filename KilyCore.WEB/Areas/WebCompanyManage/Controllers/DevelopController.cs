using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebCompanyManage.Controllers
{
    /// <summary>
    /// 成长档案
    /// </summary>
    [Area("WebCompanyManage")]
    public class DevelopController : Controller
    {
        /// <summary>
        /// 施养管理
        /// </summary>
        /// <returns></returns>
        public IActionResult Planting()
        {
            return View();
        }
    }
}