using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Repast
{
    /// <summary>
    /// 配餐制度表
    /// </summary>
    public class RepastUnitIns : RepastBase
    {
        /// <summary>
        /// 制度名称
        /// </summary>
        public virtual string InsName { get; set; }
        /// <summary>
        /// 制度内容
        /// </summary>
        public virtual string InsContent { get; set; }
        /// <summary>
        /// 制度时间
        /// </summary>
        public virtual DateTime? InsTime { get; set; }
    }
    /// <summary>
    /// 配餐记录表
    /// </summary>
    public class RepastUnitInsRecord : RepastBase
    {
        /// <summary>
        /// 配餐主题
        /// </summary>
        public virtual string InsTheme { get; set; }
        /// <summary>
        /// 配餐负责人
        /// </summary>
        public virtual string InsUser { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public virtual string InsContent { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public virtual DateTime? InsTime { get; set; }
    }
}
