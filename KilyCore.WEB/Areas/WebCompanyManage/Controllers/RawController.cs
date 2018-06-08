using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebCompanyManage.Controllers
{
    /// <summary>
    /// 作者：刘泽华
    /// 时间：2018年6月6日10点11分
    /// </summary>
    [Area("WebCompanyManage")]
    public class RawController : Controller
    {
        /// <summary>
        /// 原料清单
        /// </summary>
        /// <returns></returns>
        public IActionResult MaterialList()
        {
            return View();
        }
        /// <summary>
        /// 编辑原料
        /// </summary>
        /// <returns></returns>
        public IActionResult MaterialEdit()
        {
            return View();
        }
        /// <summary>
        /// 原料仓库
        /// </summary>
        /// <returns></returns>
        public IActionResult Stock()
        {
            return View();
        }
        /// <summary>
        /// 入库
        /// </summary>
        /// <returns></returns>
        public IActionResult StockIn() {
            return View();
        }
        /// <summary>
        /// 出库
        /// </summary>
        /// <returns></returns>
        public IActionResult StockOut() {
            return View();
        }
    }
}