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
        public IActionResult DeviceEdit()
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
        #region 指标把控
        public IActionResult Control()
        {
            return View();
        }
        public IActionResult ControlEdit()
        {
            return View();
        }
        #endregion
        #region 产品系列
        public IActionResult Series()
        {
            return View();
        }
        public IActionResult SeriesEdit()
        {
            return View();
        }
        #endregion
        #region 产品批次
        public IActionResult GoodsBatch()
        {
            return View();
        }
        public IActionResult GoodsBatchEdit()
        {
            return View();
        }
        public IActionResult ShowTarget()
        {
            return View();
        }
        #endregion
        #region 设施管理
        public IActionResult Facility()
        {
            return View();
        }
        public IActionResult FacilityEdit()
        {
            return View();
        }
        public IActionResult FacilityAttach()
        {
            return View();
        }
        public IActionResult FacilityAttachEdit()
        {
            return View();
        }
        #endregion
    }
}