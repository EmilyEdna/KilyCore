using KilyCore.EntityFrameWork.ModelEnum;
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
                if (!string.IsNullOrEmpty(Province) || !string.IsNullOrEmpty(City) || !string.IsNullOrEmpty(Area)|| !string.IsNullOrEmpty(Town))
                    return Province + "," + City + "," + Area+","+Town;
                else return null;
            }
        }
        public string CommunityCode { get; set; }
        public string CompanyAddress { get; set; }
        public string SellerAddress { get; set; }
        public string ProductionAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string NetAddress { get; set; }
        public string VideoAddress { get; set; }
        public string Discription { get; set; }
        public string Certification { get; set; }
        public string HonorCertification { get; set; }
        public AuditEnum AuditType { get; set; }
        public Guid? EnterpriseRoleId { get; set; }
    }
}
