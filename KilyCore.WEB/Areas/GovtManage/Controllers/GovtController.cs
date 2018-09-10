using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.GovtManage.Controllers
{
    [Area("GovtManage")]
    public class GovtController : Controller
    {
        #region 政府菜单
        public IActionResult GovtMenu()
        {
            return View();
        }
        public IActionResult GovtMenuEdit()
        {
            return View();
        }
        #endregion
        #region 政府角色
        public IActionResult GovtRoler()
        {
            return View();
        }
        #endregion
        #region 政府账号
        public IActionResult GovtAccount()
        {
            return View();
        }
        public IActionResult GovtAccountEdit()
        {
            return View();
        }
        #endregion
    }
}