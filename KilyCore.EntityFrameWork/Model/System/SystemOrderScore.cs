using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 订单评分表
    /// </summary>
    public class SystemOrderScore:BaseEntity
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public virtual string  OrderNo { get; set; }
        /// <summary>
        /// 接单人
        /// </summary>
        public virtual string  OrderAccepter { get; set; }
        /// <summary>
        /// 评分企业
        /// </summary>
        public virtual string  ScoreCompany { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public virtual string  Score { get; set; }
        /// <summary>
        /// 评分时间
        /// </summary>
        public virtual DateTime?  ScoreTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string  Remark { get; set; }
    }
}
