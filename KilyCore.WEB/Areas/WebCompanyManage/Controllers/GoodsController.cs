using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebCompanyManage.Controllers
{
    /// <summary>
    /// 产品管理
    /// </summary>
    [Area("WebCompanyManage")]
    public class GoodsController : Controller
    {
        #region 产品列表
        public IActionResult GoodsList()
        {
            return View();
        }
        public IActionResult GoodsEdit()
        {
            return View();
        }
        #endregion
        #region 产品仓库
        public IActionResult Stock()
        {
            return View();
        }
        public IActionResult StockOut()
        {
            return View();
        }
        public IActionResult StockIn()
        {
            return View();
        }
        public IActionResult BindTarget() {
            return View();
        }
        #endregion
    }
}