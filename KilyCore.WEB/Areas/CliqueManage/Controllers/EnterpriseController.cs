using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.CliqueManage.Controllers
{
    [Area("CliqueManage")]
    public class EnterpriseController : Controller
    {
        #region 菜单
        public IActionResult EnterpriseMenu()
        {
            return View();
        }
        public IActionResult EnterpriseMenuEdit()
        {
            return View();
        }
        #endregion
        public IActionResult EnterpriseRole()
        {
            return View();
        }
    }
}