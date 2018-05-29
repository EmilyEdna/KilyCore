using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.DataEntity.ResponseMapper.Finance
{
    public class ResponseDiningPay
    {
        public Guid Id { get; set; }
        public string MerchantName { get; set; }
        public Guid MerchantId { get; set; }
        public int PayType { get; set; }
        public string PayTpyeName => PayType == 1 ? "月付费" : "年付费";
        public DateTime PayTime { get; set; }
        public string EnableYear { get; set; }
        public DateTime? EnableYearEndTime { get; set; }
        public decimal? Paymoney { get; set; }
        public decimal? OrderMoneySum { get; set; }
        public string PayUser { get; set; }
        public string LinkPhone { get; set; }
        public string PayCertificate { get; set; }
    }
}
