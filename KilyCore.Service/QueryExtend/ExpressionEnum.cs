using System.ComponentModel;

/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
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