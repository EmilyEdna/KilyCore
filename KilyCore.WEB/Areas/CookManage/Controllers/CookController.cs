using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.CookManage.Controllers
{
    [Area("CookManage")]
    public class CookController : Controller
    {
        public IActionResult CookInfo()
        {
            return View();
        }
        public IActionResult CookInfoDetail()
        {
            return View();
        }
        public IActionResult CookInfoAudit()
        {
            return View();
        }
        public IActionResult CookService()
        {
            return View();
        }
        /// <summary>
        /// 厨师角色
        /// </summary>
        /// <returns></returns>
        public IActionResult CookRole()
        {
            return View();
        }
        #region 厨师菜单
        public IActionResult CookMenu()
        {
            return View();
        }
        public IActionResult CookMenuEdit()
        {
            return View();
        }
        #endregion
    }
}