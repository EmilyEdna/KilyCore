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
        /// 箱码管理
        /// </summary>
        /// <returns></returns>
        public IActionResult BoxCode()
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
        /// <summary>
        /// 添加说明
        /// </summary>
        /// <returns></returns>
        public IActionResult AddExplanation()
        {
            return View();
        }
        /// <summary>
        /// 二维码绑定情况
        /// </summary>
        /// <returns></returns>
        public IActionResult WatchCodeUse()
        {
            return View();
        }
        /// <summary>
        /// 箱码绑定情况
        /// </summary>
        /// <returns></returns>
        public IActionResult WatchBoxCodeBind()
        {
            return View();
        }
        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <returns></returns>
        public IActionResult ScanCodeAttach()
        {
            return View();
        }
        /// <summary>
        /// 企业二维码
        /// </summary>
        /// <returns></returns>
        public IActionResult CompanyCodeView()
        {
            return View();
        }
        public IActionResult PackCode()
        {
            return View();
        }
        public IActionResult BindEdit()
        {
            return View();
        }
    }
}