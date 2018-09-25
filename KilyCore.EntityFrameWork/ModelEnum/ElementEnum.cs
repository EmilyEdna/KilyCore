using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ElementEnum
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.ModelEnum
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/25 10:54:07
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.ModelEnum
{
    /// <summary>
    /// 节点类型
    /// </summary>
    public enum ElementEnum
    {
        /// <summary>
        /// 单行文本
        /// </summary>
        [Description("单行文本")]
        SingleText=10,
        /// <summary>
        /// 多行文本
        /// </summary>
        [Description("多行文本")]
        DoubleText =20,
        /// <summary>
        /// 下拉选择
        /// </summary>
        [Description("下拉选择")]
        Select =30,
        /// <summary>
        /// 单选
        /// </summary>
        [Description("单选")]
        Radio =40,
        /// <summary>
        /// 多选
        /// </summary>
        [Description("多选")]
        CheckBox =50,
        /// <summary>
        /// 单选+其他(文本框)
        /// </summary>
        [Description("单选+其他(文本框)")]
        RadioText =60,
        /// <summary>
        /// 多选+其他(文本框)
        /// </summary>
        [Description("多选+其他(文本框)")]
        CheckBoxText =70
    }
}
