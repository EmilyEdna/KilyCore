using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestGovtMenu
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/7 10:14:03
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Govt
{
    public class RequestGovtMenu
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
