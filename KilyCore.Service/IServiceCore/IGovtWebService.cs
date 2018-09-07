using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Govt;
using KilyCore.DataEntity.ResponseMapper.Govt;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：IGovtWebService
* 类 描 述 ：
* 命名空间 ：KilyCore.Service.IServiceCore
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/7 11:21:04
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 政府监管前台后台业务逻辑接口
    /// </summary>
    public interface IGovtWebService:IService
    {
        #region 获取全局集团菜单
        IList<ResponseGovtMenu> GetGovtMenu();
        #endregion

        #region 登录
        ResponseGovtInfo GovtLogin(RequestGovtInfo Param);
        #endregion
    }
}
