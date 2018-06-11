using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseMaterial
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/5 16:31:03
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 原辅料表
    /// </summary>
    public class EnterpriseMaterial : EnterpriseBase
    {
        /// <summary>
        /// 批次号
        /// </summary>
        public virtual string BatchNo { get; set; }
        /// <summary>
        /// 原料名称
        /// </summary>
        public virtual string MaterName { get; set; }
        /// <summary>
        /// 保质期
        /// </summary>
        public virtual string ExpiredDay { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public virtual string Spec { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public virtual string Unit { get; set; }
        /// <summary>
        /// 执行标准
        /// </summary>
        public virtual string Standard { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public virtual string Supplier { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 采购数量
        /// </summary>
        public virtual int MaterNum { get; set; }
        /// <summary>
        /// 包装类型
        /// </summary>
        public virtual string PackageType { get; set; }
    }
}
