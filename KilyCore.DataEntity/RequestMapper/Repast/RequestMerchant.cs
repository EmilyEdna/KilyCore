﻿using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.DataEntity.RequestMapper.Repast
{
    public class RequestMerchant
    {
        public Guid Id { get; set; }
        public Guid? InfoId { get; set; }
        public string Remark { get; set; }
        public string AllowUnit { get; set; }
        public string Account { get; set; }
        public string PassWord { get; set; }
        public string CommunityCode { get; set; }
        public string MerchantName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string IdCard { get; set; }
        public MerchantEnum DiningType { get; set; }
        public string Phone { get; set; }
        public string HonorCertification { get; set; }
        public Guid? DingRoleId { get; set; }
        public AuditEnum? AuditType { get; set; }
        public SystemVersionEnum VersionType { get; set; }
        public string AreaTree { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Town { get; set; }
        public string LngAndLat { get; set; }
        public string SaleScope { get; set; }
        public string SaleTime { get; set; }
        public string TypePath
        {
            get
            {
                if (!string.IsNullOrEmpty(Province) || !string.IsNullOrEmpty(City) || !string.IsNullOrEmpty(Area) || !string.IsNullOrEmpty(Town))
                    return Province + "," + City + "," + Area + "," + Town;
                else return null;
            }
        }
        public string Certification { get; set; }
        public string ImplUser { get; set; }
        public DateTime? CardExpiredDate { get; set; }
        public string SafeOffer { get; set; }
        public string OfferLv { get; set; }
        /// <summary>
        /// 企业形象
        /// </summary>
        public string MerchantImage { get; set; }
        /// <summary>
        /// 安全等级
        /// </summary>
        public string MerchantSafeLv { get; set; }
        public string ComplainPhone { get; set; }
        /// <summary>
        /// 邀请码
        /// </summary>
        public string InviteCode { get; set; }
    }
}
