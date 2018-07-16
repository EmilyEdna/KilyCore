using KilyCore.Configure;
using KilyCore.DataEntity.ResponseMapper.Repast;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：IRepastWebService
* 类 描 述 ：
* 命名空间 ：KilyCore.Service.IServiceCore
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/16 15:14:21
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 餐饮系统业务逻辑接口
    /// </summary>
    public interface IRepastWebService : IService
    {
        #region 获取全局集团菜单
        IList<ResponseRepastMenu> GetRepastMenu();
        #endregion
    }
}
