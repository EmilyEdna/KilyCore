using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebCompanyManage.Controllers
{
    /// <summary>
    /// 成长档案
    /// </summary>
    [Area("WebCompanyManage")]
    public class DevelopController : Controller
    {
        /// <summary>
        /// 施养管理
        /// </summary>
        /// <returns></returns>
        public IActionResult Planting()
        {
            return View();
        }
        /// <summary>
        /// 新增施养记录
        /// </summary>
        /// <returns></returns>
        public IActionResult PlantingEdit()
        {
            return View();
        }
        /// <summary>
        /// 农药疫情
        /// </summary>
        /// <returns></returns>
        public IActionResult Drug()
        {
            return View();
        }
        /// <summary>
        /// 新增农药疫情记录
        /// </summary>
        /// <returns></returns>
        public IActionResult DrugEdit()
        {
            return View();
        }
        /// <summary>
        /// 环境检测
        /// </summary>
        /// <returns></returns>
        public IActionResult Ambient()
        {
            return View();
        }
        /// <summary>
        /// 新增环境检测
        /// </summary>
        /// <returns></returns>
        public IActionResult AmbientEdit()
        {
            return View();
        }
        /// <summary>
        /// 环境报告
        /// </summary>
        /// <returns></returns>
        public IActionResult AmbientAttach()
        {
            return View();
        }
        /// <summary>
        /// 新增环境报告
        /// </summary>
        /// <returns></returns>
        public IActionResult AmbientAttachEdit()
        {
            return View();
        }
        /// <summary>
        /// 育苗信息
        /// </summary>
        /// <returns></returns>
        public IActionResult Grow()
        {
            return View();
        }
        /// <summary>
        /// 编辑育苗
        /// </summary>
        /// <returns></returns>
        public IActionResult GrowEdit()
        {
            return View();
        }
    }
}