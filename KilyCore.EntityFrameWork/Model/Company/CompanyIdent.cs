using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Company
{
    /// <summary>
    /// 企业认证表
    /// </summary>
    public class CompanyIdent:BaseEntity
    {
        /// <summary>
        /// 公司信息表主键
        /// </summary>
        public virtual Guid CompanyId { get; set; }
        /// <summary>
        /// 认证编号
        /// </summary>
        public virtual string IdentNo { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        public virtual string CompanyName { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        public virtual CompanyEnum CompanyType { get; set; }
        /// <summary>
        /// 认证星级
        /// </summary>
        public virtual IdentEnum IdentStar { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public virtual AuditEnum AuditType { get; set; }
        /// <summary>
        /// 认证年限
        /// </summary>
        public virtual int IdentYear { get; set; }
        /// <summary>
        /// 信用代码
        /// </summary>
        public virtual string CommunityCode { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        public virtual string Representative { get; set; }
        /// <summary>
        /// 法人身份证
        /// </summary>
        public virtual string RepresentativeCard { get; set; }
        /// <summary>
        /// 报送人
        /// </summary>
        public virtual string SendPerson { get; set; }
        /// <summary>
        /// 报送人身份证
        /// </summary>
        public virtual string SendCard { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public virtual string LinkPhone { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
