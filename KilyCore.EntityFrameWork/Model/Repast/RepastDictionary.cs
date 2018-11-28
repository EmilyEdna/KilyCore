using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastDictionary
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Repast
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/8/1 10:19:48
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Repast
{
    /// <summary>
    /// 餐饮字典
    /// </summary>
    public class RepastDictionary: RepastBase
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        public virtual string DicType { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        public virtual string DicName { get; set; }
        /// <summary>
        /// 字典值
        /// </summary>
        public virtual string DicValue { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
