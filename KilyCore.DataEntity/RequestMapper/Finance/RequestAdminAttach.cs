using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.Finance
{
    public class RequestAdminAttach
    {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public  Guid AdminId { get; set; }
        public  DateTime? StartTime { get; set; }
        public  DateTime? EndTime { get; set; }
        public  bool? IsPay { get; set; }
        public  decimal Money { get; set; }
        public  string PayUser { get; set; }
    }
}
