using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.System
{
    public class RequestMenu
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        public string MenuIcon { get; set; }
        /// <summary>
        /// 菜单地址
        /// </summary>
        public string MenuAddress { get; set; }
        /// <summary>
        /// 是否子菜单
        /// </summary>
        public bool HasChildrenNode { get; set; }
        /// <summary>
        /// 父菜单ID
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 菜单Id
        /// </summary>
        public Guid? MenuId { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }
    }
}
