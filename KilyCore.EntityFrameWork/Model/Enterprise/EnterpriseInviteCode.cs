using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 邀请码表
    /// </summary>
    public class EnterpriseInviteCode : BaseEntity
    {
        /// <summary>
        /// 邀请码
        /// </summary>
        public virtual string InviteCode { get; set; }
        /// <summary>
        /// 邀请码开始时间
        /// </summary>
        public virtual DateTime? EffectiveSt { get; set; }
        /// <summary>
        /// 邀请码过期时间
        /// </summary>
        public virtual DateTime? EffectiveEt { get; set; }
        /// <summary>
        /// 使用时间
        /// </summary>
        public virtual DateTime? UseTime { get; set; }
        /// <summary>
        /// 使用公司
        /// </summary>
        public virtual string UseCompany { get; set; }
        /// <summary>
        /// 使用公司电话
        /// </summary>
        public virtual string UsePhone { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public virtual CompanyEnum UseCompanyType { get; set; }
        /// <summary>
        /// 公司所在区域
        /// </summary>
        public virtual string UseTypePath { get; set; }
    }
}
