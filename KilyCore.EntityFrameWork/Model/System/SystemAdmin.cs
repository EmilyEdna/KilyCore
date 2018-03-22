﻿using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 系统管理员表
    /// </summary>
    public class SystemAdmin : BaseEntity
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string TrueName { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public virtual string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public virtual string PassWord { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public virtual string Email { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public virtual string IdCard { get; set; }
        /// <summary>
        /// 区域组织
        /// </summary>
        public virtual string TypePath { get; set; }
        /// <summary>
        /// 账号类型
        /// </summary>
        public virtual AccountEnum AccountType { get; set; }
        /// <summary>
        /// 角色权限类型
        /// </summary>
        public virtual Guid RoleAuthorType { get; set; }
    }
}
