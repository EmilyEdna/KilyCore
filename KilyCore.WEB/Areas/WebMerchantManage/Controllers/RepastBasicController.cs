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
        public IActionResult MerchantAudit()
        {
            return View();
        }
        #endregion
        #region 商家认证
        public IActionResult MerchantIdent()
        {
            return View();
        }
        public IActionResult MerchantIdentEdit()
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
        #region 集团账户
        public IActionResult MerchantGroup()
        {
            return View();
        }
        public IActionResult MerchantGroupEdit()
        {
            return View();
        }
        #endregion
        #region 餐饮字典
        public IActionResult MerchantDic()
        {
            return View();
        }
        public IActionResult MerchantDicEdit()
        {
            return View();
        }
        #endregion
        #region 升级续费
        public IActionResult MerchantLv()
        {
            return View();
        }
        public IActionResult MerchantContinued()
        {
            return View();
        }
        public IActionResult MerchantUpdate()
        {
            return View();
        }
        #endregion
        #region 修改密码和区域
        public IActionResult MerchantInfoEditAccount()
        {
            return View();
        }
        public IActionResult MerchantInfoEditArea()
        {
            return View();
        }
        #endregion
        #region 自查
        public IActionResult MerchantCheck()
        {
            return View();
        }
        public IActionResult MerchantCheckEdit()
        {
            return View();
        }
        #endregion
        #region 委员会
        public IActionResult MerchantOrg()
        {
            return View();
        }
        public IActionResult MerchantOrgEdit()
        {
            return View();
        }
        #endregion
    }
}