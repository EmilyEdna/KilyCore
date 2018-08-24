﻿using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseCookMenu
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Cook
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/24 14:59:02
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Cook
{
    public class ResponseCookMenu
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
        public IList<ResponseCookMenu> MenuChildren { get; set; }
    }
}
