using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestWxPayModel
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.System
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/26 14:40:34
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.System
{
    public class RequestWxPayModel
    {
        public string OrderTitle { get; set; }
        /// <summary>
        /// 单位：分
        /// </summary>
        public int Money { get; set; }
    }
}
