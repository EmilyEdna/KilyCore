using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.Model.Repast
{
    /// <summary>
    /// 餐饮角色表
    /// </summary>
    public class RepastRoleAuthor:BaseEntity
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
    /// <summary>
    /// 集团账户表
    /// </summary>
    public class RepastRoleAuthorWeb : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string AuthorName { get; set; }
        /// <summary>
        /// 选中的菜单
        /// </summary>
        public virtual string AuthorMenuPath { get; set; }
    }
}
