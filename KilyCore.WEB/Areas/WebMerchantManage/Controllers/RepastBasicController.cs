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
        /// <summary>
        /// 商家资料
        /// </summary>
        /// <returns></returns>
        public IActionResult MerchantInfo()
        {
            return View();
        }
    }
}