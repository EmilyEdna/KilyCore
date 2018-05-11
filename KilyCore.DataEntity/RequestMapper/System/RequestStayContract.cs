using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.System
{
    public class RequestStayContract
    {
        public Guid? StayCompanyId { get; set; }
        public string StayCompanyName { get; set; }
        public string StayCompanyContract { get; set; }
        public string PayContract { get; set; }
        public AuditEnum AuditType { get; set; }
    }
}
