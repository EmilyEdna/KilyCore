using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KilyCore.Service.QueryExtend
{
    public enum ExpressionEnum
    {
        /// <summary>
        /// 包含
        /// </summary>
        [Description("包含")]
        Like = 1,
        /// <summary>
        /// 不包含
        /// </summary>
        [Description("不包含")]
        NotLike = 2,
        /// <summary>
        /// 等于
        /// </summary>
        [Description("等于")]
        Equals = 3,
        /// <summary>
        /// 不等于
        /// </summary>
        [Description("不等于")]
        NotEquals = 4,
        /// <summary>
        /// 大于
        /// </summary>
        [Description("大于")]
        GreaterThan = 5,
        /// <summary>
        /// 大于等于
        /// </summary>
        [Description("大于等于")]
        GreaterThanOrEqual = 6,
        /// <summary>
        /// 小于
        /// </summary>
        [Description("小于")]
        LessThan = 7,
        /// <summary>
        /// 小于等于
        /// </summary>
        [Description("小于等于")]
        LessThanOrEqual = 8

    }
}
