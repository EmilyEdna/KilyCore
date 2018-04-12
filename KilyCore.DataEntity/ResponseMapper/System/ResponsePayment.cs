using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.System
{
    public class ResponsePayment
    {
        public Guid Id { get; set; }
        public Guid TableId { get; set; }
        public string TableName { get; set; }
        public string PayCertificate { get; set; }
        public string PaymentUser { get; set; }
        public string LinkPhone { get; set; }
        public DateTime PayTime { get; set; }
        public decimal Paymoney { get; set; }
        public string Remark { get; set; }
    }
}
