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
        #region 留样
        public IActionResult MerchantSample()
        {
            return View();
        }
        public IActionResult MerchantSampleEdit()
        {
            return View();
        }
        #endregion
        #region 抽查
        public IActionResult MerchantDraw()
        {
            return View();
        }
        public IActionResult MerchantDrawEdit()
        {
            return View();
        }
        #endregion
        #region 废物
        public IActionResult MerchantDuck()
        {
            return View();
        }
        public IActionResult MerchantDuckEdit()
        {
            return View();
        }
        #endregion
        #region 消毒
        public IActionResult MerchantDisinfect()
        {
            return View();
        }
        public IActionResult MerchantDisinfectEdit()
        {
            return View();
        }
        #endregion
        #region 添加剂
        public IActionResult MerchantAdditive()
        {
            return View();
        }
        public IActionResult MerchantAdditiveEdit()
        {
            return View();
        }
        #endregion
    }
}