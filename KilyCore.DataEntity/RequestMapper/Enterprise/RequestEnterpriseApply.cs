﻿using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseApply
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string BatchNo { get; set; }
        public string ApplyNum { get; set; }
        public TagEnum TagType { get; set; }
        public decimal? ApplyMoney { get; set; }
        public int Payment { get; set; }
        public bool? IsPay { get; set; }
        public string PaytTicket { get; set; }
        public AuditEnum AuditType { get; set; }
        public string AreaTree { get; set; }
    }
}
