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
        /// 用户角色名称
        /// </summary>
        public virtual string UserRoleAuthorName{ get; set; }
        /// <summary>
        /// 选中的菜单
        /// </summary>
        public virtual string AuthorMenuPath { get; set; }
    }
}
