using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Base
{
    /// <summary>
    /// 企业公共表字段
    /// </summary>
    public class EnterpriseBase :BaseEntity
    {
        /// <summary>
        /// 企业Id
        /// </summary>
        public virtual Guid CompanyId { get; set; }
    }
}
