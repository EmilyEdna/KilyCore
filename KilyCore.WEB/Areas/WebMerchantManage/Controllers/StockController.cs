using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebMerchantManage.Controllers
{
    [Area("WebMerchantManage")]
    public class StockController : Controller
    {
        #region 原料仓库
        public IActionResult MerchantStuffStock()
        {
            return View();
        }
        public IActionResult MerchantInStorage()
        {
            return View();
        }
        public IActionResult MerchantOutStorage()
        {
            return View();
        }
        #endregion
        public IActionResult MerchantReport()
        {
            return View();
        }
    }
}