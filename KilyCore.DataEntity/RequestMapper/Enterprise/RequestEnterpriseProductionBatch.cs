using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestEnterpriseProductionBatch
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/11 15:02:28
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseProductionBatch
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid SeriesId { get; set; }
        public  string BatchNo { get; set; }
        public string SeriesName { get; set; }
        public DateTime? StartTime { get; set; }
        public string DeviceName { get; set; }
        public string MaterId { get; set; }
        public string Manager { get; set; }
    }
    public class RequestEnterpriseProductionBatchAttach
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string TargetName { get; set; }
        public string TargetValue { get; set; }
        public string TargetUnit { get; set; }
        public string Result { get; set; }
        public string ResultTime { get; set; }
        public string Manager { get; set; }
    }
}
