using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KilyCore.EntityFrameWork.ModelEnum
{
    /// <summary>
    /// 商家类型
    /// </summary>
    public enum MerchantEnum
    {
        /// <summary>
        /// 餐饮企业
        /// </summary>
        [Description("餐饮企业")]
        Normal=10,
        /// <summary>
        /// 乡村厨师
        /// </summary>
        [Description("乡村厨师")]
        VillageCook = 20,
        /// <summary>
        /// 单位食堂
        /// </summary>
        [Description("单位食堂")]
        UnitCanteen=30,
        /// <summary>
        /// 小作坊
        /// </summary>
        [Description("小作坊")]
        WorkShop=40,
        /// <summary>
        /// 小摊贩
        /// </summary>
        [Description("小摊贩")]
        Vendors=50,
        /// <summary>
        /// 小经营店
        /// </summary>
        [Description("小经营店")]
        SmallStore=60


    }
}
