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
        public IList<String> Video { get; set; }
        public IDictionary<String, String> VideoMap { get; set; }
        public Guid Id { get; set; }
        public Guid? CompanyId { get; set; }
        public DateTime? CardExpiredDate { get; set; }
        public string SafeOffer { get; set; }
        public string OfferLv { get; set; }
        public string LngAndLat { get; set; }
        public string CompanyName { get; set; }
        public CompanyEnum CompanyType { get; set; }
        public string CompanyAccount { get; set; }
        public SystemVersionEnum Version { get; set; }
        public string Category { get; set; }
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
                    return Honor.Split(",").ToList();
                else return null;
            }
        }
        public List<ResponseAudit> AuditDetails { get; set; }
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
        public string Scope { get; set; }
        public string VideoAddress { get; set; }
        public string CodeStar { get; set; }
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
        /// 注册时间
        /// </summary>
        public DateTime CreateTime { set; get; }
        /// <summary>
        /// 邀请码
        /// </summary>
        public string InviteCode { set; get; }
        public string ComplainPhone { get; set; }
        public IDictionary<String, String> MainProduncts
        {
            get
            {
                Dictionary<String, String> Map = new Dictionary<String, String>();
                if (!string.IsNullOrEmpty(MainProRemark))
                {
                    if (MainProRemark.Contains(","))
                    {
                        var Remark = MainProRemark.Split(",");
                        var Pro = MainPro.Split(",");
                        var ProL = MainProRemark.Split(",").Length;
                        for (int i = 0; i < ProL; i++)
                        {
                            try
                            {
                                if(!Map.ContainsKey(Remark[i]))
                                    Map.Add(Remark[i], Pro[i] ?? "");
                            }
                            catch (Exception)
                            {
                                if (!Map.ContainsKey(Remark[i]))
                                    Map.Add(Remark[i], "");
                            }
                        }
                    }
                    return Map;
                }
                return null;
            }
        }
    }
}
