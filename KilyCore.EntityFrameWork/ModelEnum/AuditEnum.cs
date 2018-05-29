using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.ModelEnum
{
    /// <summary>
    /// 审核枚举
    /// </summary>
    public enum AuditEnum
    {
        /// <summary>
        /// 审核不通过
        /// </summary>
        [Description("审核不通过")]
        AduitFail = 10,
        /// <summary>
        /// 等待审核
        /// </summary>
        [Description("等待审核")]
        WaitAduit = 20,
        /// <summary>
        /// 审核中
        /// </summary>
        [Description("审核中")]
        AuditLoading = 30,
        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("审核通过")]
        AuditSuccess=40,
        /// <summary>
        /// 财务审核不通过
        /// </summary>
        [Description("终审不通过")]
        FinanceFail=50,
        /// <summary>
        /// 财务审核通过
        /// </summary>
        [Description("终审通过")]
        FinanceSuccess=60
    }
}
