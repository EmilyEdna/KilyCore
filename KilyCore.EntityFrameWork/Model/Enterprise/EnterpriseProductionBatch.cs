using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseProductionBatch
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/11 11:28:55
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 生产批次表
    /// </summary>
    public class EnterpriseProductionBatch : EnterpriseBase
    {
        /// <summary>
        /// 批次号
        /// </summary>
        public virtual string BatchNo { get; set; }
        /// <summary>
        /// 产品系列Id
        /// </summary>
        public virtual Guid SeriesId { get; set; }
        /// <summary>
        /// 生产开始日期
        /// </summary>
        public virtual DateTime? StartTime { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public virtual string DeviceName { get; set; }
        /// <summary>
        /// 原辅料Id
        /// </summary>
        public virtual string MaterialId { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public virtual string Manager { get; set; }
        /// <summary>
        /// 车间表Id
        /// </summary>
        public virtual Guid? FacId { get; set; }  
    }
    /// <summary>
    /// 生产批次与指标对照表
    /// </summary>
    public class EnterpriseProductionBatchAttach : EnterpriseBase
    {
        
        /// <summary>
        /// 生产批次表Id
        /// </summary>
        public virtual Guid ProBatchId { get; set; }
        /// <summary>
        /// 指标名称
        /// </summary>
        public virtual string TargetName { get; set; }
        /// <summary>
        /// 指标限制值
        /// </summary>
        public virtual string TargetValue { get; set; }
        /// <summary>
        /// 关键点单位
        /// </summary>
        public virtual string TargetUnit { get; set; }
        /// <summary>
        /// 记录结果
        /// </summary>
        public virtual string Result { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public virtual string ResultTime { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public virtual string Manager { get; set; }
    }
}
