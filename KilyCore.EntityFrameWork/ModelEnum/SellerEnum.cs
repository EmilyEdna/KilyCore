using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

/// <summary>
/// 作者：刘泽华
/// 时间：2018/5/30 15:56:10
/// </summary>
namespace KilyCore.EntityFrameWork.ModelEnum
{
    /// <summary>
    /// 厂商类型
    /// </summary>
    public enum SellerEnum
    {
        /// <summary>
        /// 供应商
        /// </summary>
        [Description("供应商")]
        Supplier=10,
        /// <summary>
        /// 分销商
        /// </summary>
        [Description("分销商")]
        Sale =20,
        /// <summary>
        /// 生产商
        /// </summary>
        [Description("生产商")]
        Production =30
    }
}
