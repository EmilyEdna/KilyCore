﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebGovtManage.Controllers
{
    [Area("WebGovtManage")]
    public class OrderController : Controller
    {
        public IActionResult WorkOrder()
        {
            return View();
        }
        public IActionResult WorkOrderEdit()
        {
            return View();
        }
    }
}