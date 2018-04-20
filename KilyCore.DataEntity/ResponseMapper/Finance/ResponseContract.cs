using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.Finance
{
    public class ResponseContract
    {
        public string MerchantName { get; set; }
        public Guid Id { get; set; }
        public int PayType { get; set; }
        public string PayTypeName => PayType == 1 ? "月订单支付" : "年付费";
        public string Contract { get; set; }
    }

    public class ResponseStayContract
    {
        public Guid Id { get; set; }
        public Guid? StayCompanyId { get; set; }
        public string StayCompanyName { get; set; }
        public string StayCompanyContract { get; set; }
    }
}
