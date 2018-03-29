using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 集团菜单表
    /// </summary>
    public class EnterpriseMenu: BaseEntity
    {
        /// <summary>
        /// 树Id
        /// </summary>
        public virtual Guid? MenuId { get; set; }
        /// <summary>
        /// 树父Id
        /// </summary>
        public virtual Guid? ParentId { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public virtual string MenuName { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public virtual MenuEnum Level { get; set; }
        /// <summary>
        /// 菜单链接地址
        /// </summary>
        public virtual string MenuAddress { get; set; }
        /// <summary>
        /// 是否拥有孩子节点
        /// </summary>
        public virtual bool HasChildrenNode { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        public virtual string MenuIcon { get; set; }
    }
}
