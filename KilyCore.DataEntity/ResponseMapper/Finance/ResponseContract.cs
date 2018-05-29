using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
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
}
