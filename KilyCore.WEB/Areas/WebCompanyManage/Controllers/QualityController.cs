﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebCompanyManage.Controllers
{
    [Area("WebCompanyManage")]
    public class QualityController : Controller
    {
        /// <summary>
        /// 原料质检
        /// </summary>
        /// <returns></returns>
        public IActionResult RawCheck()
        {
            return View();
        }
        /// <summary>
        /// 编辑原料质检
        /// </summary>
        /// <returns></returns>
        public IActionResult RawCheckEdit()
        {
            return View();
        }

        #region 产品召回
        public IActionResult Recover()
        {
            return View();
        }
        public IActionResult RecoverEdit()
        {
            return View();
        }
        #endregion
        #region 过期处理
        public IActionResult Expired()
        {
            return View();
        }
        public IActionResult ExpiredEdit()
        {
            return View();
        }
        #endregion
        #region 不合格处理
        public IActionResult Inferior()
        {
            return View();
        }
        public IActionResult InferiorEdit()
        {
            return View();
        }
        #endregion
    }
}