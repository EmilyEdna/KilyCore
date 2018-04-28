﻿using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 集团用户表
    /// </summary>
   public class EnterpriseUser:BaseEntity
    {
        /// <summary>
        /// 企业Id
        /// </summary>
        public virtual Guid CompanyId { get; set; }
        /// <summary>
        /// 企业Id
        /// </summary>
        public virtual string CompanyName { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        public virtual CompanyEnum CompanyType { get; set; }
        /// <summary>
        /// 系统版本
        /// </summary>
        public virtual SystemVersionEnum Version { get; set; }
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
        /// 电话
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public virtual string IdCard { get; set; }
        /// <summary>
        /// 集团账户类型
        /// </summary>
        public virtual Guid? RoleAuthorType { get; set; }
    }
}