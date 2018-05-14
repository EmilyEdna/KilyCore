using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.System
{
    public class ResponseAudit
    {
        public Guid Id { get; set; }
        public string TabelName { get; set; }
        public Guid TableId { get; set; }
        public string AuditSuggestion { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        public string AuditName { get; set; }
        public string CreateUser { get; set; }
        public AuditEnum AuditType { get; set; }
        public string AuditTypeName { get; set; }
    }
}
