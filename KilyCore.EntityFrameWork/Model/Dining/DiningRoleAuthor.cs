using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Dining
{
    /// <summary>
    /// 餐饮角色表
    /// </summary>
    public class DiningRoleAuthor:BaseEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public virtual string AuthorName { get; set; }
        /// <summary>
        /// 选中的菜单
        /// </summary>
        public virtual string AuthorMenuPath { get; set; }
    }
}
