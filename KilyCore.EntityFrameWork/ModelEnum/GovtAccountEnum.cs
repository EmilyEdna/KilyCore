using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：GovtAccountEnum
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.ModelEnum
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/6 15:13:01
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.ModelEnum
{
    /// <summary>
    /// 政府账号类型
    /// </summary>
    public enum GovtAccountEnum
    {
        /// <summary>
        /// 省级账号
        /// </summary>
        [Description("省级账号")]
        Province = 10,
        /// <summary>
        /// 市级账号
        /// </summary>
        [Description("市级账号")]
        City = 20,
        /// <summary>
        /// 区县账号
        /// </summary>
        [Description("区县账号")]
        Area = 30,
        /// <summary>
        /// 乡镇账号
        /// </summary>
        [Description("乡镇账号")]
        Town = 40
    }
}
