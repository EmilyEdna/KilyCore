using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.System
{
    public class RequestSystemOrderScore
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 接单人
        /// </summary>
        public string OrderAccepter { get; set; }
        /// <summary>
        /// 评分企业
        /// </summary>
        public string ScoreCompany { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public string Score { get; set; }
        /// <summary>
        /// 评分时间
        /// </summary>
        public DateTime? ScoreTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
