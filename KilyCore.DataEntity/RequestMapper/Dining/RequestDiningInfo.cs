using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.DataEntity.RequestMapper.Dining
{
    public class RequestDiningInfo
    {
        public Guid Id { get; set; }
        public string MerchantName { get; set; }
        public Guid? DingRoleId { get; set; }
    }
}
