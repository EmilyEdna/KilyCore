using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.Govt
{
    public class ResponseGovtNetPatrolLog
    {
        /// <summary>
        /// 企业Id
        /// </summary>
        public Guid? CompanyId { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 巡查时间
        /// </summary>
        public DateTime? RecordTime { get; set; }
        /// <summary>
        /// 巡查人
        /// </summary>
        public string RocordUser { get; set; }
        public Guid Id { get; set; }
        public Guid? GovtId { get; set; }
    }
}
