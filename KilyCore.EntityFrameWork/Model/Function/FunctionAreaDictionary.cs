using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Function
{
    /// <summary>
    /// 区域字典表
    /// </summary>
    public class FunctionAreaDictionary:BaseEntity
    {
        /// <summary>
        /// 省份Id
        /// </summary>
        public virtual Guid ProvinceId { get; set; }
        /// <summary>
        /// 系统字典Id
        /// </summary>
        public virtual Guid DictionaryId { get; set; }
    }
}
