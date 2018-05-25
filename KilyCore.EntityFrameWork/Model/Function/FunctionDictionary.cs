using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Function
{
    /// <summary>
    /// 系统码表
    /// </summary>
    public class FunctionDictionary : BaseEntity
    {
        /// <summary>
        /// 码表名称
        /// </summary>
        public virtual string DicName { get; set; }
        /// <summary>
        /// 码表值
        /// </summary>
        public virtual string DicValue { get; set; }
        /// <summary>
        /// 码表介绍
        /// </summary>
        public virtual string DicDescript { get; set; }
    }
}