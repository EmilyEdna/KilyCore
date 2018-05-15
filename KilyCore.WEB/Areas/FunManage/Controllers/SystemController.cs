using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.WEB.Areas.FunManage.Controllers
{
    [Area("FunManage")]
    public class SystemController : Controller
    {
        #region 数据统计
        public IActionResult DataSum()
        {
            return View();
        }
        #endregion
        #region 消息中心
        public IActionResult InfoCenter()
        {
            return View();
        }
        #endregion
        #region  区域价目
        public IActionResult AreaPrice()
        {
            return View();
        }
        public IActionResult AreaPriceEdit()
        {
            return View();
        }
        #endregion
        #region 纹理二维码
        public IActionResult VeinTag()
        {
            return View();
        }
        public IActionResult RecordTag() {
            return View();
        }
        #endregion
    }
}