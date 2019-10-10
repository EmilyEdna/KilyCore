using KilyCore.Configure;
using System;

#region << 版 本 注 释 >>

/*----------------------------------------------------------------
* 类 名 称 ：IIocProviderService
* 类 描 述 ：
* 命名空间 ：KilyCore.Service.IServiceCore
* 机器名称 ：DESKTOP-QPIVQ28
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/21 14:50:14
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/

#endregion << 版 本 注 释 >>

namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 系统初始化使用
    /// </summary>
    public interface IIocProviderService : IService
    {
        String RestartQuartz();
    }
}