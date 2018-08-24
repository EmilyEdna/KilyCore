using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Cook;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Cook;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ICookServiceWeb
* 类 描 述 ：
* 命名空间 ：KilyCore.Service.IServiceCore
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/24 15:07:29
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 厨师前台后台业务逻辑接口
    /// </summary>
    public interface ICookWebService:IService
    {
        #region 登录注册
        String RegistCookAccount(RequestCookInfo Param);
        ResponseCookInfo CookLogin(RequestValidate LoginValidate);
        #endregion

        #region 全局菜单
        IList<ResponseCookMenu> GetCookMenu();
        #endregion
    }
}
