using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.System
{
    public class RequestAudit
    {
        public Guid Id { get; set; }
        public Guid TableId { get; set; }
         public string TableName { get; set; }
        public string AuditSuggestion { get; set; }
        public string AuditName { get; set; }
        public AuditEnum AuditType { get; set; }
    }
}
