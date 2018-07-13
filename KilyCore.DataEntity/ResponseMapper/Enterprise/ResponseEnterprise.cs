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
namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterprise
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public CompanyEnum CompanyType { get; set; }
        public string CompanyAccount { get; set; }
        public SystemVersionEnum Version { get; set; }
        public string PassWord { get; set; }
        #region 辅助字段
        public string VersionName { get; set; }
        public string CompanyTypeName { get; set; }
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
                return !string.IsNullOrEmpty(TypePath) ? (TypePath.Split(',').Length >= 4 ? TypePath.Split(',')[3] : null) : null;
            }
        }
        public IList<string> HonorCertification
        {
            get
            {
                if (!string.IsNullOrEmpty(Honor))
                    return Honor.Split("|").ToList();
                else return null;
            }
        }
        public ResponseAudit AuditDetails { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        #endregion
        public string TypePath { get; set; }
        public string CommunityCode { get; set; }
        public string CompanyAddress { get; set; }
        public string SellerAddress { get; set; }
        public string ProductionAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string NetAddress { get; set; }
        public string Discription { get; set; }
        public string Certification { get; set; }
        /// <summary>
        /// 审核类型
        /// </summary>
        public string AuditTypeName { get; set; }
        public string Honor { get; set; }
        public AuditEnum AuditType { get; set; }
        public Guid? EnterpriseRoleId { get; set; }
        public string IdCard { get; set; }
        public int NatureAgent { get; set; }
        public string NatureName { get => NatureAgent == 1 ? "企业或合作社" : "个体商业户"; }
        public long? TagCodeNum { get; set; }
        public string SafeNo { get; set; }
        public string SafeCompany { get; set; }
    }
}
