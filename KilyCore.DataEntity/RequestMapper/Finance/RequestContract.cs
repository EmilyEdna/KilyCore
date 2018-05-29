using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
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
