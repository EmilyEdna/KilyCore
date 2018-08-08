using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebMerchantManage.Controllers
{
    [Area("WebMerchantManage")]
    public class SourceController : Controller
    {
        #region 原料溯源
        public IActionResult MerchantStuff()
        {
            return View();
        }
        public IActionResult MerchantStuffEdit()
        {
            return View();
        }
        #endregion
    }
}