using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 二维码使用记录表
    /// </summary>
    public class EnterpriseTagUseRecord: EnterpriseBase
    {
        /// <summary>
        /// 使用者
        /// </summary>
        public virtual string TagUser { get; set; }
        /// <summary>
        /// 使用数量
        /// </summary>
        public virtual int TagCount { get; set; }
        /// <summary>
        /// 使用来源
        /// </summary>
        public virtual string Source { get; set; }
        /// <summary>
        /// 码
        /// </summary>
        public virtual string Code { get; set; }
    }
}
