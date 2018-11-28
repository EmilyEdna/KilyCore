using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.WEB.Areas.WebCompanyManage.Controllers
{
    /// <summary>
    /// 成长档案
    /// </summary>
    [Area("WebCompanyManage")]
    public class DevelopController : Controller
    {
        /// <summary>
        /// 水肥管理
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
        /// 喂养管理
        /// </summary>
        /// <returns></returns>
        public IActionResult Culture()
        {
            return View();
        }
        /// <summary>
        /// 新增喂养记录
        /// </summary>
        /// <returns></returns>
        public IActionResult CultureEdit()
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
        /// 疫苗管理
        /// </summary>
        /// <returns></returns>
        public IActionResult Vaccine()
        {
            return View();
        }
        /// <summary>
        /// 新增疫苗信息
        /// </summary>
        /// <returns></returns>
        public IActionResult VaccineEdit()
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
        /// <summary>
        /// 成长日记
        /// </summary>
        /// <returns></returns>
        public IActionResult Diary()
        {
            return View();
        }
        /// <summary>
        /// 编辑日记
        /// </summary>
        /// <returns></returns>
        public IActionResult DiaryEdit()
        {
            return View();
        }
        /// <summary>
        /// 成长流程
        /// </summary>
        /// <returns></returns>
        public IActionResult AgeUp()
        {
            return View();
        }
        /// <summary>
        /// 编辑成长流程
        /// </summary>
        /// <returns></returns>
        public IActionResult AgeUpEdit()
        {
            return View();
        }
    }
}