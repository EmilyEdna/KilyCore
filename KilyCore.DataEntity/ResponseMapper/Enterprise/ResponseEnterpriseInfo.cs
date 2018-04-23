using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseInfo
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public CompanyEnum CompanyType { get; set; }
        public string CompanyAccount { get; set; }
        public string CompanyAddress { get; set; }
        public string CommunityCode { get; set; }
        public string SellerAddress { get; set; }
        public string ProductionAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string NetAddress { get; set; }
        public string VideoAddress { get; set; }
        public string Discription { get; set; }
        public string Certification { get; set; }
        public AuditEnum AuditType { get; set; }
        public string HonorCertification { get; set; }
        public Guid? EnterpriseRoleId { get; set; }
        public string TypePath { get; set; }
        #region 辅助字段
        public string CompanyTypeName { get; set; }
        public string AuditTypeName { get; set; }
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
        #endregion
    }
}
