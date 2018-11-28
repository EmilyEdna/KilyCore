using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastContinued
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Repast
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/8/1 10:30:05
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Repast
{
    /// <summary>
    /// 续费记录表
    /// </summary>
    public class RepastContinued : RepastBase
    {
        /// <summary>
        /// 续费年限
        /// </summary>
        public virtual string ContinuedYear { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public virtual PayEnum PayType { get; set; }
        /// <summary>
        /// 是否付款
        /// </summary>
        public virtual bool? IsPay { get; set; }
        /// <summary>
        /// 票据
        /// </summary>
        public virtual string PayTicket { get; set; }
        /// <summary>
        /// 审核类型
        /// </summary>
        public virtual AuditEnum AuditType { get; set; }
    }
    /// <summary>
    /// 升降级记录表
    /// </summary>
    public class RepastUpLevel: RepastBase
    {
        /// <summary>
        /// 审核类型
        /// </summary>
        public virtual AuditEnum AuditType { get; set; }
        /// <summary>
        /// 版本类型
        /// </summary>
        public virtual SystemVersionEnum VersionType { get; set; }
        /// <summary>
        /// 续费年限
        /// </summary>
        public virtual string ContinuedYear { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public virtual PayEnum PayType { get; set; }
        /// <summary>
        /// 票据
        /// </summary>
        public virtual string PayTicket { get; set; }
        /// <summary>
        /// 是否付款
        /// </summary>
        public virtual bool? IsPay { get; set; }
    }
}
