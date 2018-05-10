using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseGrowInfo
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string BacthNo { get; set; }
        public string GrowName { get; set; }
        public DateTime BuyTime { get; set; }
        public string BuyNum { get; set; }
        public string Unit { get; set; }
        public string Remark { get; set; }
    }
}
