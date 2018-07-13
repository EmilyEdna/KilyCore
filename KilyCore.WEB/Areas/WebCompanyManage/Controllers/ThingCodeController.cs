using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebCompanyManage.Controllers
{
    /// <summary>
    /// 物码管理
    /// </summary>
    [Area("WebCompanyManage")]
    public class ThingCodeController : Controller
    {
        /// <summary>
        /// 一物一码
        /// </summary>
        /// <returns></returns>
        public IActionResult OneCode()
        {
            return View();
        }
        /// <summary>
        /// 一品一码
        /// </summary>
        /// <returns></returns>
        public IActionResult ClassCode()
        {
            return View();
        }
        /// <summary>
        /// 一企一码
        /// </summary>
        /// <returns></returns>
        public IActionResult CompanyCode()
        {
            return View();
        }
        /// <summary>
        /// 编辑二维码
        /// </summary>
        /// <returns></returns>
        public IActionResult ThingCodeEdit()
        {
            return View();
        }
        /// <summary>
        /// 申请标签
        /// </summary>
        /// <returns></returns>
        public IActionResult ApplyCode()
        {
            return View();
        }
        /// <summary>
        /// 新增申请
        /// </summary>
        /// <returns></returns>
        public IActionResult ApplyEdit()
        {
            return View();
        }
        /// <summary>
        /// 支付
        /// </summary>
        /// <returns></returns>
        public IActionResult Payment()
        {
            return View();
        }
        /// <summary>
        /// 纹理标签
        /// </summary>
        /// <returns></returns>
        public IActionResult VeinTarget()
        {
            return View();
        }
        /// <summary>
        /// 扫码管理
        /// </summary>
        /// <returns></returns>
        public IActionResult ScanCode()
        {
            return View();
        }
        public IActionResult AddExplantion()
        {
            return View();
        }
    }
}