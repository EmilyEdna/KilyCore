using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebCompanyManage.Controllers
{
    /// <summary>
    /// 物流管理
    /// </summary>
    [Area("WebCompanyManage")]
    public class LogisticsController : Controller
    {
        #region 打包
        public IActionResult Package()
        {
            return View();
        }
        public IActionResult PackageEdit()
        {
            return View();
        }
        #endregion
        #region 发货
        public IActionResult SendGoods()
        {
            return View();
        }
        public IActionResult SendGoodsEdit()
        {
            return View();
        }
        public IActionResult GetGoods()
        {
            return View();
        }
        #endregion
    }
}