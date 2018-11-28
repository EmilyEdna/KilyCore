using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequsetnterpriseDevice
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/8 14:33:21
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseDevice
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string DeviceName { get; set; }
        public string ModelName { get; set; }
        public string SupplierName { get; set; }
        public DateTime? ProductTime { get; set; }
        public string Life { get; set; }
        public string Manager { get; set; }
    }
    public class RequestEnterpriseDeviceClean
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid DeviceId { get; set; }
        public string Manager { get; set; }
        public DateTime? CleanTime { get; set; }
        public string Ways { get; set; }
    }
    public class RequestEnterpriseDeviceFix
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid DeviceId { get; set; }
        public DateTime? FixTime { get; set; }
        public string Reason { get; set; }
        public string Manager { get; set; }
    }
}
