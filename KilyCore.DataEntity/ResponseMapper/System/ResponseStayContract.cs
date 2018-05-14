using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.System
{
    public class ResponseStayContract
    {
        public Guid Id { get; set; }
        public Guid ProvinceId { get; set; }
        public Guid? StayCompanyId { get; set; }
        public string StayCompanyName { get; set; }
        public List<string> Contract
        {
            get
            {
                return StayCompanyContract.Split(",").ToList();
            }
        }
        public string StayCompanyContract { get; set; }
        public string PayContract { get; set; }
        public AuditEnum AuditType { get; set; }
        public string AuditTypeName { get; set; }
        public string TableName { get; set; }
    }
}
