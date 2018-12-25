using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.DataEntity.ResponseMapper.Repast
{
    public class ResponseMerchant
    {
        public IList<String> Video { get; set; }
        public Guid Id { get; set; }
        public Guid? InfoId { get; set; }
        public string Account { get; set; }
        public string PassWord { get; set; }
        public string LngAndLat { get; set; }
        public string CommunityCode { get; set; }
        public string MerchantName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
        public MerchantEnum DiningType { get; set; }
        public SystemVersionEnum VersionType { get; set; }
        public string VersionTypeName { get; set; }
        public string DiningTypeName { get; set; }
        public string Phone { get; set; }
        public Guid? DingRoleId { get; set; }
        public string TypePath { get; set; }
        public string IdCard { get; set; }
        public string Honor { get; set; }
        public bool IsPayContract { get; set; }
        public string Province
        {
            get
            {
                return !string.IsNullOrEmpty(TypePath) ? (TypePath.Split(',').Length >= 1 ? TypePath.Split(',')[0] : null) : null;
            }
        }
        public string City
        {
            get
            {
                return !string.IsNullOrEmpty(TypePath) ? (TypePath.Split(',').Length >= 2 ? TypePath.Split(',')[1] : null) : null;
            }
        }
        public string Area
        {
            get
            {
                return !string.IsNullOrEmpty(TypePath) ? (TypePath.Split(',').Length >= 3 ? TypePath.Split(',')[2] : null) : null;
            }
        }
        public string Town
        {
            get
            {
                return !string.IsNullOrEmpty(TypePath) ? (TypePath.Split(',').Length >= 4 ? (TypePath.Split(',')[3]) : null) : null;
            }
        }
        public IList<string> HonorCertification
        {
            get
            {
                if (!string.IsNullOrEmpty(Honor))
                    return Honor.Split(",").ToList();
                else return null;
            }
        }
        public string Certification { get; set; }
        public string ImplUser { get; set; }
        public string TableName { get; set; }
        public IList<ResponseAudit> AuditInfo { get; set; }
        public string AllowUnit { get; set; }
        public AuditEnum AuditType { get; set; }
        public string AuditTypeName { get; set; }
        public DateTime? CardExpiredDate { get; set; }
        public string SafeOffer { get; set; }
        public string OfferLv { get; set; }
    }
}
