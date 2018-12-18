using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 集团用户表
    /// </summary>
   public class EnterpriseUser: EnterpriseBase
    {
        /// <summary>
        /// 企业名称
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
        /// <summary>
        /// 所属区域
        /// </summary>
        public virtual string TypePath { get; set; }
        /// <summary>
        /// 码段前缀
        /// </summary>
        public virtual string CodeStar { get; set; }
    }
}
