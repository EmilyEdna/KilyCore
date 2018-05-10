using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterprisePlanting
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string FeedName { get; set; }
        public string Brand { get; set; }
        public string Supplier { get; set; }
        public DateTime PlantTime { get; set; }
        public string CheckReport { get; set; }
        public string BacthNo { get; set; }
    }
}
