using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
    public class ResponseEnterpriseApply
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string BacthNo { get; set; }
        public string ApplyNum { get; set; }
        public TagEnum TagType { get; set; }
        public string TagTypeName { get; set; }
        public decimal? ApplyMoney { get; set; }
        public int Payment { get; set; }
        public string PaytTicket { get; set; }
        public string PaymentType { get => Payment == 1 ? "年付费" : "按需付费"; }
        public bool? IsPay { get; set; }
        public string PayType { get => IsPay.HasValue ? ((bool)IsPay ? "已付款" : "未付款") : "未付款"; }
        public AuditEnum AuditType { get; set; }
        public string AuditTypeName { get; set; }
        public string TableName { get; set; }
    }
}
