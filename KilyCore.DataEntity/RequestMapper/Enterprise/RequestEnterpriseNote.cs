using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseNote
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string NoteName { get; set; }
        public string BacthNo { get; set; }
        public  DateTime? SowingTime { get; set; }
        public DateTime? ResultTime { get; set; }
    }
}
