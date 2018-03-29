using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 集团用户角色表
    /// </summary>
    public class EnterpriseUserRoleAuthor:BaseEntity
    {
        /// <summary>
        /// 企业ID
        /// </summary>
        public virtual Guid GroupUserId { get; set; }
        /// <summary>
        /// 选中的菜单
        /// </summary>
        public virtual string AuthorMenuPath { get; set; }
    }
}
