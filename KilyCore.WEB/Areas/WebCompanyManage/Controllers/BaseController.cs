using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebCompanyManage.Controllers
{
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
        public IActionResult UploadFile()
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
    }
}