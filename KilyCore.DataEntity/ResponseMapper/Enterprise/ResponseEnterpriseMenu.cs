﻿using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseMenu
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid? MenuId { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string MenuAddress { get; set; }
        /// <summary>
        /// 是否有子节点
        /// </summary>
        public bool HasChildrenNode { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        public string MenuIcon { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public IList<ResponseEnterpriseMenu> MenuChildren { get; set; }
    }
}
