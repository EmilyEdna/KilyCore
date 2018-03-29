using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 集团用户组表
    /// </summary>
    public class EnterpriseGroupUser:BaseEntity
    {
        /// <summary>
        /// 企业Id
        /// </summary>
        public virtual Guid CompanyId { get; set; }
        /// <summary>
        /// 集团用户账号
        /// </summary>
        public virtual string EnterpriseUserAccount { get; set; }
        /// <summary>
        /// 集团用户姓名
        /// </summary>
        public virtual string EnterpriseUserName { get; set; }
        /// <summary>
        /// 集团用户密码
        /// </summary>
        public virtual string EnterpriseUserPassWord { get; set; }
        /// <summary>
        /// 集团用户电话
        /// </summary>
        public virtual string EnterpriseUserPhone { get; set; }
        /// <summary>
        /// 集团用户身份证
        /// </summary>
        public virtual string EnterpriseUserIdCard { get; set; }
        /// <summary>
        /// 集团用户邮箱
        /// </summary>
        public virtual string EnterpriseUserEmail { get; set; }
    }
}
