using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.FoodManage.Controllers
{
    [Area("RepastManage")]
    public class RepastController : Controller
    {
        #region 商家中心
        public IActionResult Merchant()
        {
            return View();
        }
        public IActionResult MerchantDetail()
        {
            return View();
        }
        #endregion
        #region 餐饮菜单
        public IActionResult FoodMenu()
        {
            return View();
        }
        public IActionResult FoodMenuEdit()
        {
            return View();
        }
        #endregion
        #region 餐饮角色
        public IActionResult FoodRole()
        {
            return View();
        }
        #endregion
        #region  审核商家
        public IActionResult AuditMerchant()
        {
            return View();
        }
        #endregion
        #region 认证审核
        public IActionResult AuditIdent()
        {
            return View();
        }
        public IActionResult AuditIdentInfo()
        {
            return View();
        }
        public IActionResult AuditIdentDetail()
        {
            return View();
        }
        public IActionResult Payment()
        {
            return View();
        }
        #endregion
    }
}