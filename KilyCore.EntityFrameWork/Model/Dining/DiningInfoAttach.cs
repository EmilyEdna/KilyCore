using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Dining
{
    /// <summary>
    /// 餐饮用户信息附加表
    /// </summary>
    public class DiningInfoAttach:BaseEntity
    {
        /// <summary>
        /// 主表Id
        /// </summary>
        public virtual Guid InfoId { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        public virtual string Certification { get; set; }
        /// <summary>
        /// 代表人
        /// </summary>
        public virtual string ImplUser { get; set; }
    }
}
