using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestAliPayModel
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.System
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/24 17:24:00
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.System
{
    public class RequestAliPayModel
    {
        public double Money { get; set; }
        public string OrderTitle { get; set; }
        public string OrderDescription { get; set; }
    }
}
