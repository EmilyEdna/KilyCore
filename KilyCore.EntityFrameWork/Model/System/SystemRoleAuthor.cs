using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 角色权限表
    /// </summary>
    public class SystemRoleAuthor:BaseEntity
    {
        /// <summary>
        /// 角色权限名称
        /// </summary>
        public virtual string AuthorName { get; set; }
        /// <summary>
        /// 权限等级
        /// </summary>
        public virtual Guid? AuthorRoleLvId { get; set; }
        /// <summary>
        /// 选中的菜单
        /// </summary>
        public virtual string AuthorMenuPath { get; set; }
        /// <summary>
        /// 所属管理区域
        /// </summary>
        public virtual string TypePath { get; set; }
    }
}
