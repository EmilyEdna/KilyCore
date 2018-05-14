using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 入住合同
    /// </summary>
    public class SystemStayContract : BaseEntity
    {
        /// <summary>
        /// 入住企业的Id
        /// </summary>
        public virtual Guid? StayCompanyId { get; set; }
        /// <summary>
        /// 省份Id
        /// </summary>
        public virtual Guid ProvinceId { get; set; }
        /// <summary>
        /// 入住企业名称
        /// </summary>
        public virtual string StayCompanyName { get; set; }
        /// <summary>
        /// 入住合同
        /// </summary>
        public virtual string StayCompanyContract { get; set; }
        /// <summary>
        /// 入住缴费合同
        /// </summary>
        public virtual string PayContract { get; set; }
        /// <summary>
        /// 审核类型
        /// </summary>
        public virtual AuditEnum AuditType { get; set; }

    }
}
