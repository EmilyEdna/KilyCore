using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseEnterpriseTarget
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/8 17:05:20
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseTarget
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string TargetName { get; set; }
        public string TargetValue { get; set; }
        public string TargetUnit { get; set; }
        public string Standard { get; set; }
    }
    public class ResponseEnterpriseProductSeries
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string SeriesName { get; set; }
        public string TargetName { get; set; }
        public string Standard { get; set; }
    }
}
