using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.FoodManage.Controllers
{
    [Area("FoodManage")]
    public class DiningController : Controller
    {
        #region 商家中心
        public IActionResult Merchant()
        {
            return View();
        }
        public IActionResult MerchantDetail()
        {
            return View();
        }
        #endregion
    }
}