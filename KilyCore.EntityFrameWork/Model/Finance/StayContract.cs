using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Finance
{
    /// <summary>
    /// 入住合同
    /// </summary>
    public class StayContract : BaseEntity
    {
        /// <summary>
        /// 入住企业的Id
        /// </summary>
        public virtual Guid? StayCompanyId { get; set; }
        /// <summary>
        /// 入住企业名称
        /// </summary>
        public virtual string StayCompanyName { get; set; }
        /// <summary>
        /// 入住合同
        /// </summary>
        public virtual string StayCompanyContract { get; set; }

    }
}
