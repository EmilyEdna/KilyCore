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
    /// 星级枚举
    /// </summary>
    public enum IdentEnum
    {
        /// <summary>
        /// 一星认证
        /// </summary>
        [Description("一星认证")]
        StarLv1 = 10,
        /// <summary>
        /// 二星认证
        /// </summary>
        [Description("二星认证")]
        StarLv2 = 20,
        /// <summary>
        /// 三星认证
        /// </summary>
        [Description("三星认证")]
        StarLv3 = 30,
        /// <summary>
        /// 四星认证
        /// </summary>
        [Description("四星认证")]
        StarLv4 = 40,
        /// <summary>
        /// 五星认证
        /// </summary>
        [Description("五星认证")]
        StarLv5 = 50
    }
}
