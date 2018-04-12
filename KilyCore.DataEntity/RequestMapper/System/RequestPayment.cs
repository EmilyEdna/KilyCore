using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.System
{
    public class RequestPayment
    {
        public Guid TableId { get; set; }
        public string TableName { get; set; }
        public string PayCertificate { get; set; }
        public string PaymentUser { get; set; }
        public string LinkPhone { get; set; }
        public DateTime PayTime { get; set; }
        public decimal Paymony { get; set; }
        public  string Remark { get; set; }
    }
}
