using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebMerchantManage.Controllers
{
    [Area("WebMerchantManage")]
    public class RepastBasicController : Controller
    {
        #region 商家资料
        public IActionResult MerchantInfo()
        {
            return View();
        }
        public IActionResult MerchantInfoEdit()
        {
            return View();
        }
        public IActionResult Payment()
        {
            return View();
        }
        #endregion
    }
}