using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace KilyCore.EntityFrameWork.ModelEnum
{
    public enum OrderEnum
    {
        /// <summary>
        /// 等待审核
        /// </summary>
        [Description("等待审核")]
        WaitAudit=10,
        /// <summary>
        /// 派单中
        /// </summary>
        [Description("派单中")]
        Dispatch=20,
        /// <summary>
        /// 已接单
        /// </summary>
        [Description("已接单")]
        TakingOrder=30,
        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Complete=40,
        /// <summary>
        /// 已评价
        /// </summary>
        [Description("已评价")]
        Evaluative=50
    }
}
