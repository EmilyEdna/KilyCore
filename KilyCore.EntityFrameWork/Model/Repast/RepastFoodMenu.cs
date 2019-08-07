using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Repast
{
    /// <summary>
    /// 周菜谱
    /// </summary>
    public class RepastFoodMenu: RepastBase
    {
        /// <summary>
        /// 菜谱名称
        /// </summary>
        public virtual string FoodMenuName { get; set; }
        /// <summary>
        /// 上报时间
        /// </summary>
        public virtual DateTime? UpTime { get; set; }
        public virtual string Content { get; set; }
    }
}
