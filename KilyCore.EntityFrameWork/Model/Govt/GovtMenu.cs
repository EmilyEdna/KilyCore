using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：GovtMenu
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/7 10:05:40
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Govt
{
    /// <summary>
    /// 政府监管菜单
    /// </summary>
    public class GovtMenu: BaseEntity
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
