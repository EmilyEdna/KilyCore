using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
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
    /// 餐饮企业表
    /// </summary>
    public class RepastInfo : BaseEntity
    {
        /// <summary>
        /// 母公司Id
        /// </summary>
        public virtual Guid? InfoId { get; set; }
        /// <summary>
        /// 营业范围
        /// </summary>
        public virtual string SaleScope { get; set; }
        /// <summary>
        /// 营业时间
        /// </summary>
        public virtual string SaleTime { get; set; }
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
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }
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
        /// <summary>
        /// 商家描述
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 所属单位
        /// </summary>
        public virtual string AllowUnit { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public virtual string IdCard { get; set; }
        /// <summary>
        /// 系统版本
        /// </summary>
        public virtual SystemVersionEnum VersionType { get; set; }
        /// <summary>
        /// 经纬度
        /// </summary>
        public virtual string LngAndLat { get; set; }
        /// <summary>
        /// 荣誉证书
        /// </summary>
        public virtual string HonorCertification { get; set; }
        /// <summary>
        /// 证件到期时间
        /// </summary>
        public virtual DateTime? CardExpiredDate { get; set; }
        /// <summary>
        /// 安全员
        /// </summary>
        public virtual string SafeOffer { get; set; }
        /// <summary>
        /// 安全员等级
        /// </summary>
        public virtual string OfferLv { get; set; }
        /// <summary>
        /// 合同状态
        /// </summary>
        public virtual bool IsPayContract { get; set; }
        /// <summary>
        /// 企业形象
        /// </summary>
        public virtual string MerchantImage { get; set; }
        /// <summary>
        /// 安全等级
        /// </summary>
        public virtual string MerchantSafeLv { get; set; }
        /// <summary>
        /// 投诉电话
        /// </summary>
        public virtual string ComplainPhone { get; set; }
        /// <summary>
        /// 邀请码
        /// </summary>
        public virtual string InviteCode { get; set; }
    }
}
