using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseTarget
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/8 16:53:13
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 指标把控表
    /// </summary>
    public class EnterpriseTarget:EnterpriseBase
    {
        /// <summary>
        /// 指标名称
        /// </summary>
        public virtual string TargetName { get; set; }
        /// <summary>
        /// 指标限制值
        /// </summary>
        public virtual string TargetValue { get; set; }
        /// <summary>
        /// 指标单位
        /// </summary>
        public virtual string TargetUnit { get; set; }
        /// <summary>
        /// 执行标准
        /// </summary>
        public virtual string Standard { get; set; }
    }
}
