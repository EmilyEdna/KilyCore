using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.MoneyManage.Controllers
{
    [Area("MoneyManage")]
    public class FinanceController : Controller
    {
        #region 加盟管理
        public IActionResult JoinPay()
        {
            return View();
        }
        public IActionResult JoinPayEdit()
        {
            return View();
        }
        #endregion
    }
}