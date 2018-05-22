using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseApply
    {
        public Guid CompanyId { get; set; }
        public string BacthNo { get; set; }
        public string ApplyNum { get; set; }
        public TagEnum TagType { get; set; }
        public decimal? ApplyMoney { get; set; }
        public int Payment { get; set; }
        public bool? IsPay { get; set; }
        public string PaytTicket { get; set; }
    }
}
