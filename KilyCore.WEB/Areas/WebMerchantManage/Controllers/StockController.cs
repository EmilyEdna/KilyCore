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

        #region 物品仓库
        public IActionResult MerchantGoodsStock()
        {
            return View();
        }
        public IActionResult MerchantInStock()
        {
            return View();
        }
        public IActionResult MerchantOutStock()
        {
            return View();
        }

        #endregion

        #region 报表
        public IActionResult MerchantReport()
        {
            return View();
        }
        #endregion

        #region 名称类型
        public IActionResult MerchantNameType()
        {
            return View();
        }
        public IActionResult MerchantNameTypeEdit()
        {
            return View();
        }
        #endregion
    }
}