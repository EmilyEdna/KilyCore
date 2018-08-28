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
        #region 厨师信息
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
        #endregion
        #region 厨师服务
        public IActionResult CookService()
        {
            return View();
        }
        #endregion
        #region 厨师角色
        public IActionResult CookRole()
        {
            return View();
        }
        #endregion
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