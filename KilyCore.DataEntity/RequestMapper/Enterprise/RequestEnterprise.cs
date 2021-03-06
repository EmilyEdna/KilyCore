﻿using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterprise
    {
        public Guid Id { get; set; }
        public Guid? CompanyId { get; set; }
        public string LngAndLat { get; set; }
        public string CompanyName { get; set; }
        public CompanyEnum CompanyType { get; set; }
        public string CompanyAccount { get; set; }
        public string PassWord { get; set; }
        public SystemVersionEnum Version { get; set; }
        #region 辅助字段
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Town { get; set; }
        public string AreaTree { get; set; }
        #endregion
        public string TypePath
        {
            get
            {
                if (!string.IsNullOrEmpty(Province) || !string.IsNullOrEmpty(City) || !string.IsNullOrEmpty(Area) || !string.IsNullOrEmpty(Town))
                    return Province + "," + City + "," + Area + "," + Town;
                else return null;
            }
        }
        public string CommunityCode { get; set; }
        public string CompanyAddress { get; set; }
        public string SellerAddress { get; set; }
        public string ProductionAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string NetAddress { get; set; }
        public string Discription { get; set; }
        public string VideoAddress { get; set; }
        public string Certification { get; set; }
        public string HonorCertification { get; set; }
        public AuditEnum? AuditType { get; set; }
        public Guid? EnterpriseRoleId { get; set; }
        public string IdCard { get; set; }
        public int NatureAgent { get; set; }
        public string SafeNo { get; set; }
        public string SafeCompany { get; set; }
        public string Scope { get; set; }
        public DateTime? CardExpiredDate { get; set; }
        public string SafeOffer { get; set; }
        public string OfferLv { get; set; }
        public string CodeStar { get; set; }
        public string Category { get; set; }
        /// <summary>
        /// 安全等级
        /// </summary>
        public string CompanySafeLv { get; set; }
        /// <summary>
        /// 主要产品
        /// </summary>
        public string MainPro { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string MainProRemark { get; set; }
        /// <summary>
        /// 企业形象
        /// </summary>
        public string ComImage { get; set; }
        /// <summary>
        /// 投诉电话
        /// </summary>
        public string ComplainPhone { get; set; }
        /// <summary>
        /// 邀请码
        /// </summary>
        public string InviteCode { get; set; }
    }
}
