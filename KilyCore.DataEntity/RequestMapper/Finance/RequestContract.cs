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
    public class RequestStayContract
    {
        public Guid? StayCompanyId { get; set; }
        public string StayCompanyName { get; set; }
        public string StayCompanyContract { get; set; }
    }
}
