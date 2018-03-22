using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 城市表
    /// </summary>
    public class SystemCity : BaseEntity
    {
        /// <summary>
        /// 市名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 市代码
        /// </summary>
        public virtual int Code { get; set; }
        /// <summary>
        /// 省代码
        /// </summary>
        public virtual int ProvinceCode { get; set; }
    }
}
