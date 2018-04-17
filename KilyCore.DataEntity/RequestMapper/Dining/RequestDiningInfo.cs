using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.Dining
{
    public class RequestDiningInfo
    {
        public Guid Id { get; set; }
        public string MerchantName { get; set; }
        public Guid? DingRoleId { get; set; }
    }
}
