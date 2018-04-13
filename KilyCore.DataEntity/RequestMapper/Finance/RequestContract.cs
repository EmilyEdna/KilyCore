using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.Finance
{
    public class RequestContract
    {
        public Guid MerchantId { get; set; }
        public string MerchantName { get; set; }
        public string Contract { get; set; }
        public string PayType { get; set; }
    }
}
