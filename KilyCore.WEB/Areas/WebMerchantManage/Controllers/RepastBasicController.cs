using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebMerchantManage.Controllers
{
    [Area("WebMerchantManage")]
    public class RepastBasicController : Controller
    {
        #region 商家资料
        public IActionResult MerchantInfo()
        {
            return View();
        }
        public IActionResult MerchantInfoEdit()
        {
            return View();
        }
        public IActionResult Payment()
        {
            return View();
        }
        #endregion
        #region 商家认证
        public IActionResult MerchantIdent()
        {
            return View();
        }
        #endregion
        #region 权限角色
        public IActionResult MerchantRole()
        {
            return View();
        }
        #endregion
        #region 人员管理
        public IActionResult MerchantUser()
        {
            return View();
        }
        public IActionResult MerchantUserEdit()
        {
            return View();
        }
        #endregion
    }
}