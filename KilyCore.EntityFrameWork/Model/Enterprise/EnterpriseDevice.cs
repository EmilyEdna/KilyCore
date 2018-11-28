using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseDevice
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/8 11:32:23
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 企业设备表
    /// </summary>
    public class EnterpriseDevice:EnterpriseBase
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public virtual string DeviceName { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public virtual string ModelName { get; set; }
        /// <summary>
        /// 生产商
        /// </summary>
        public virtual string SupplierName { get; set; }
        /// <summary>
        /// 生产时间
        /// </summary>
        public virtual DateTime? ProductTime { get; set; }
        /// <summary>
        /// 寿命
        /// </summary>
        public virtual string Life { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public virtual string Manager { get; set; }
    }
    /// <summary>
    /// 设备清洁表
    /// </summary>
    public class EnterpriseDeviceClean : EnterpriseBase
    {
        /// <summary>
        /// 主表Id
        /// </summary>
        public virtual Guid DeviceId { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public virtual string Manager { get; set; }
        /// <summary>
        /// 清洗时间
        /// </summary>
        public virtual DateTime? CleanTime { get; set; }
        /// <summary>
        /// 方式
        /// </summary>
        public virtual string Ways { get; set; }
    }
    /// <summary>
    /// 设备维护表
    /// </summary>
    public class EnterpriseDeviceFix : EnterpriseBase
    {
        /// <summary>
        /// 主表Id
        /// </summary>
        public virtual Guid DeviceId { get; set; }
        /// <summary>
        /// 维护时间
        /// </summary>
        public virtual DateTime? FixTime { get; set; }
        /// <summary>
        /// 原因
        /// </summary>
        public virtual string Reason { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public virtual string Manager { get; set; }
    }
}
