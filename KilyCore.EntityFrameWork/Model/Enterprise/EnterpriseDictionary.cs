using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseDictionary
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/4 16:41:01
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 企业字典表
    /// </summary>
    public class EnterpriseDictionary:BaseEntity
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
