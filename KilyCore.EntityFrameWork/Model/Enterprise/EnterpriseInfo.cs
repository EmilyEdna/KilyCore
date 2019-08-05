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
    /// 企业信息表
    /// </summary>
    public class EnterpriseInfo : BaseEntity
    {
        /// <summary>
        /// 子公司所属集团Id
        /// </summary>
        public virtual Guid? CompanyId { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        public virtual string CompanyName { get; set; }
        /// <summary>
        /// 所属行业
        /// </summary>
        public virtual CompanyEnum CompanyType { get; set; }
        /// <summary>
        /// 公司登录的账号,对应商城登录账号
        /// </summary>
        public virtual string CompanyAccount { get; set; }
        /// <summary>
        /// 视频地址
        /// </summary>
        public virtual string VideoAddress { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public virtual string TypePath { get; set; }
        /// <summary>
        /// 企业密,对应商城密码MD5加密
        /// </summary>
        public virtual string PassWord { get; set; }
        /// <summary>
        /// 企业地址
        /// </summary>
        public virtual string CompanyAddress { get; set; }
        /// <summary>
        /// 社会统一信用代码
        /// </summary>
        public virtual string CommunityCode { get; set; }
        /// <summary>
        /// 销售地址
        /// </summary>
        public virtual string SellerAddress { get; set; }
        /// <summary>
        /// 生产地址
        /// </summary>
        public virtual string ProductionAddress { get; set; }
        /// <summary>
        /// 企业电话
        /// </summary>
        public virtual string CompanyPhone { get; set; }
        /// <summary>
        /// 企业网站地址
        /// </summary>
        public virtual string NetAddress { get; set; }
        /// <summary>
        /// 企业介绍
        /// </summary>
        public virtual string Discription { get; set; }
        /// <summary>
        /// 企业证书
        /// </summary>
        public virtual string Certification { get; set; }
        /// <summary>
        /// 审核类型
        /// </summary>
        public virtual AuditEnum AuditType { get; set; }
        /// <summary>
        /// 荣誉证书
        /// </summary>
        public virtual string HonorCertification { get; set; }
        /// <summary>
        /// 集团角色Id
        /// </summary>
        public virtual Guid? EnterpriseRoleId { get; set; }
        /// <summary>
        /// 系统版本
        /// </summary>
        public virtual SystemVersionEnum Version { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public virtual string IdCard { get; set; }
        /// <summary>
        /// 企业性质
        /// </summary>
        public virtual int NatureAgent { get; set; }
        /// <summary>
        /// 当前版本的二维码数量
        /// </summary>
        public virtual long? TagCodeNum { get; set; }
        /// <summary>
        /// 保单号
        /// </summary>
        public virtual string SafeNo { get; set; }
        /// <summary>
        /// 保险公司
        /// </summary>
        public virtual string SafeCompany { get; set; }
        /// <summary>
        /// 经营范围
        /// </summary>
        public virtual string Scope { get; set; }
        /// <summary>
        /// 经纬度
        /// </summary>
        public virtual string LngAndLat { get; set; }
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
        /// 码段前缀
        /// </summary>
        public virtual string CodeStar { get; set; }
        /// <summary>
        /// 安全等级
        /// </summary>
        public virtual string CompanySafeLv { get; set; }
        /// <summary>
        /// 主要产品
        /// </summary>
        public virtual string MainPro { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string MainProRemark { get; set; }
        /// <summary>
        /// 企业形象
        /// </summary>
        public virtual string ComImage { get; set; }
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
