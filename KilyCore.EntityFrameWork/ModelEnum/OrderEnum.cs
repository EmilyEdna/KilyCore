using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace KilyCore.EntityFrameWork.ModelEnum
{
    public enum OrderEnum
    {
        /// <summary>
        /// 审核不通过
        /// </summary>
        [Description("审核不通过")]
        NoAudit =10,
        /// <summary>
        /// 等待审核
        /// </summary>
        [Description("等待审核")]
        WaitAudit=20,
        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("审核通过")]
        AuditSuccess = 30,
        /// <summary>
        /// 派单中
        /// </summary>
        [Description("派单中")]
        Dispatch=40,
        /// <summary>
        /// 已接单
        /// </summary>
        [Description("已接单")]
        AcceptOrder = 50,
        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Complete=60,
        /// <summary>
        /// 已评价
        /// </summary>
        [Description("已评价")]
        Evaluative=70,
        /// <summary>
        /// 核实归档
        /// </summary>
        [Description("归档")]
        End = 80
    }
}
