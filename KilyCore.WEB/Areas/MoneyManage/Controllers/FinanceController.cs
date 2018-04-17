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
        #region 加盟缴费
        public IActionResult JoinPay()
        {
            return View();
        }
        public IActionResult JoinPayEdit()
        {
            return View();
        }
        public IActionResult JoinPayNotice()
        {
            return View();
        }
        #endregion
        #region 企业认证
        public IActionResult IdentEnterprisePay()
        {
            return View();
        }
        #endregion
        #region 餐饮认证
        public IActionResult IdentFoodPay()
        {
            return View();
        }
        #endregion
        #region 餐饮缴费
        public IActionResult FoodPay()
        {
            return View();
        }
        public IActionResult FoodPayUpdate()
        {
            return View();
        }
        #endregion
        #region 认证缴费
        public IActionResult PaymentNotice()
        {
            return View();
        }
        #endregion
        #region 餐饮合同
        public IActionResult FoodContract()
        {
            return View();
        }
        #endregion
    }
}