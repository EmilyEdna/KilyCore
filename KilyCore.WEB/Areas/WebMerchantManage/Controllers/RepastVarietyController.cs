using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebMerchantManage.Controllers
{
    [Area("WebMerchantManage")]
    public class RepastVarietyController : Controller
    {
        #region 菜品管理
        public IActionResult MerchantDish()
        {
            return View();
        }
        public IActionResult MerchantDishEdit()
        {
            return View();
        }
        #endregion
        #region 菜品管理
        public IActionResult MerchantCookbook()
        {
            return View();
        }
        public IActionResult MerchantCookbookEdit()
        {
            return View();
        }
        #endregion
    }
}