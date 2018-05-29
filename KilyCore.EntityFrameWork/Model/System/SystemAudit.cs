using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 审核记录表
    /// </summary>
    public class SystemAudit :BaseEntity
    {
        /// <summary>
        /// 审核的表名
        /// </summary>
        public virtual string TableName { get; set; }
        /// <summary>
        /// 审核的表ID也就是主键
        /// </summary>
        public virtual Guid TableId { get; set; }
        /// <summary>
        /// 审核意见
        /// </summary>
        public virtual string AuditSuggestion { get; set; }
        /// <summary>
        /// 审核人
        /// </summary> 
        public virtual string AuditName { get; set; }
        /// <summary>
        /// 审核状态 
        /// </summary>
        public virtual AuditEnum AuditType { get; set; }
    }
}
