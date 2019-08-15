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
        public virtual string  OrderNo { get; set; }
        public virtual string  OrderAccepter { get; set; }
        public virtual string  ScoreCompany { get; set; }
        public virtual string  Score { get; set; }
        public virtual DateTime?  ScoreTime { get; set; }
        public virtual string  Remark { get; set; }
    }
}
