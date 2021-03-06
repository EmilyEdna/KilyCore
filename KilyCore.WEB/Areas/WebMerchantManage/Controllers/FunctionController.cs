﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebMerchantManage.Controllers
{
    [Area("WebMerchantManage")]
    public class FunctionController : Controller
    {
        #region 供应商
        public IActionResult MerchantSupplier()
        {
            return View();
        }

        public IActionResult MerchantSupplierEdit()
        {
            return View();
        }
        #endregion

        #region 进货台账
        public IActionResult MerchantBuybill()
        {
            return View();
        }
        public IActionResult MerchantBuybillEdit()
        {
            return View();
        }
        #endregion

        #region 销售台账
        public IActionResult MerchantSellbill()
        {
            return View();
        }
        public IActionResult MerchantSellbillEdit()
        {
            return View();
        }
        #endregion

        #region 实时监控
        public IActionResult MerchantMonitor()
        {
            return View();
        }
        public IActionResult MerchantMonitorEdit()
        {
            return View();
        }
        #endregion

        #region 台账凭证
        public IActionResult MerchantTicket()
        {
            return View();
        }
        public IActionResult MerchantTicketEdit()
        {
            return View();
        }
        #endregion

        #region 二维码管理
        public IActionResult MerchantScan()
        {
            return View();
        }
        public IActionResult MerchantScanEdit()
        {
            return View();
        }
        #endregion
    }
}