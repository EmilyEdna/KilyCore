﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.WebGovtManage.Controllers
{
    [Area("WebGovtManage")]
    public class ProductController : Controller
    {
        public IActionResult EdibleSupervise()
        {
            return View();
        }
        public IActionResult WorkSupervise()
        {
            return View();
        }
    }
}