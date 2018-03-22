using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KilyCore.EntityFrameWork.ModelEnum
{
    /// <summary>
    /// 企业类型
    /// </summary>
    public enum CompanyEnum
    {
        /// <summary>
        /// 种养(值/殖)企业
        /// </summary>
        [Description("种养(值/殖)企业")]
        Plant = 10,
        /// <summary>
        /// 生产企业
        /// </summary>
        [Description("生产企业")]
        Production = 20,
        /// <summary>
        /// 餐饮企业
        /// </summary>
        [Description("餐饮企业")]
        Dining = 30,
        /// <summary>
        /// 流通企业
        /// </summary>
        [Description("流通企业")]
        Circulation = 40,
        /// <summary>
        /// 其他企业
        /// </summary>
        [Description("其他企业")]
        Other=50
    }
}
