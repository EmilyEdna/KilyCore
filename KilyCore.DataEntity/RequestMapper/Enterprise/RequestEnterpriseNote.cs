using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseNote
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string NoteName { get; set; }
        public string BacthNo { get; set; }
        public DateTime? ResultTime { get; set; }
    }
}
