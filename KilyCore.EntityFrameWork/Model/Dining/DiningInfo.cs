﻿using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Dining
{
    /// <summary>
    /// 餐饮用户表
    /// </summary>
    public class DiningInfo : BaseEntity
    {
        /// <summary>
        /// 账号
        /// </summary>
        public virtual string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public virtual string PassWord { get; set; }
        /// <summary>
        /// 社会统一信用代码
        /// </summary>
        public virtual string CommunityCode { get; set; }
        /// <summary>
        /// 商家名称
        /// </summary>
        public virtual string MerchantName { get; set; }
        /// <summary>
        /// 商家类型
        /// </summary>
        public virtual MerchantEnum DiningType { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 区域组织
        /// </summary>
        public virtual string TypePath { get; set; }
        /// <summary>
        /// 餐饮角色ID
        /// </summary>
        public virtual Guid? DingRoleId { get; set; }
        /// <summary>
        /// 审核类型
        /// </summary>
        public virtual AuditEnum AuditType { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public virtual string Email { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        public virtual string Certification { get; set; }
        /// <summary>
        /// 代表人
        /// </summary>
        public virtual string ImplUser { get; set; }
    }
}