﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebCompanyManage.Controllers
{
    /// <summary>
    /// 基础管理
    /// </summary>
    [Area("WebCompanyManage")]
    public class BaseController : Controller
    {
        /// <summary>
        /// 企业资料
        /// </summary>
        /// <returns></returns>
        public IActionResult CompanyData()
        {
            return View();
        }
        /// <summary>
        /// 企业资料
        /// </summary>
        /// <returns></returns>
        public IActionResult CompanyDataEdit()
        {
            return View();
        }
        /// <summary>
        /// 缴费
        /// </summary>
        /// <returns></returns>
        public IActionResult Payment()
        {
            return View();
        }
        /// <summary>
        /// 人员管理
        /// </summary>
        /// <returns></returns>
        public IActionResult UserManage()
        {
            return View();
        }
        /// <summary>
        /// 用户编辑
        /// </summary>
        /// <returns></returns>
        public IActionResult UserEdit()
        {
            return View();
        }
        /// <summary>
        /// 集团账户
        /// </summary>
        /// <returns></returns>
        public IActionResult GroupAccount()
        {
            return View();
        }
        /// <summary>
        /// 企业认证
        /// </summary>
        /// <returns></returns>
        public IActionResult CompanyIdent()
        {
            return View();
        }
        /// <summary>
        /// 企业字典
        /// </summary>
        /// <returns></returns>
        public IActionResult ComDictionary()
        {
            return View();
        }
        /// <summary>
        /// 编辑字典
        /// </summary>
        /// <returns></returns>
        public IActionResult DicEdit()
        {
            return View();
        }
        /// <summary>
        /// 升级续费
        /// </summary>
        /// <returns></returns>
        public IActionResult UpLevel()
        {
            return View();
        }
        /// <summary>
        /// 内部文件
        /// </summary>
        /// <returns></returns>
        public IActionResult InsideFile()
        {
            return View();
        }
        /// <summary>
        /// 编辑内部文件
        /// </summary>
        /// <returns></returns>
        public IActionResult InsideFileEdit()
        {
            return View();
        }
        /// <summary>
        /// 集团账户
        /// </summary>
        /// <returns></returns>
        public IActionResult ChildAccount()
        {
            return View();
        }
        /// <summary>
        /// 编辑子账户
        /// </summary>
        /// <returns></returns>
        public IActionResult ChildrenAccountEdit()
        {
            return View();
        }
        /// <summary>
        /// 编辑账号
        /// </summary>
        /// <returns></returns>
        public IActionResult CompanyEditAccount()
        {
            return View();
        }
        /// <summary>
        /// 编辑区域
        /// </summary>
        /// <returns></returns>
        public IActionResult CompanyEditArea()
        {
            return View();
        }
        /// <summary>
        /// 企业认证
        /// </summary>
        /// <returns></returns>
        public IActionResult CompanyIdentEdit()
        {
            return View();
        }
        /// <summary>
        /// 视频监控
        /// </summary>
        /// <returns></returns>
        public IActionResult Vedio()
        {
            return View();
        }
        /// <summary>
        /// 编辑视频
        /// </summary>
        /// <returns></returns>
        public IActionResult VedioEdit()
        {
            return View();
        }
        /// <summary>
        /// 企业自查
        /// </summary>
        /// <returns></returns>
        public IActionResult CompanyCheck()
        {
            return View();
        }
        /// <summary>
        /// 编辑自查
        /// </summary>
        /// <returns></returns>
        public IActionResult CompanyCheckEdit()
        {
            return View();
        }
    }
}