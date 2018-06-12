using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebCompanyManage.Controllers
{
    /// <summary>
    /// 生产管理
    /// </summary>
    [Area("WebCompanyManage")]
    public class ProductController : Controller
    {
        #region 设备管理
        /// <summary>
        /// 设备管理
        /// </summary>
        /// <returns></returns>
        public IActionResult Device()
        {
            return View();
        }
        /// <summary>
        /// 编辑设备
        /// </summary>
        /// <returns></returns>
        public IActionResult EditDevice()
        {
            return View();
        }
        /// <summary>
        /// 维护
        /// </summary>
        /// <returns></returns>
        public IActionResult DeviceFix()
        {
            return View();
        }
        /// <summary>
        /// 清洗
        /// </summary>
        /// <returns></returns>
        public IActionResult DeviceClean()
        {
            return View();
        }
        #endregion
    }
}