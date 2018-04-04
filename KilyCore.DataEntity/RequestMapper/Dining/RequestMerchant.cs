using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.Dining
{
    public class RequestMerchant
    {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public string PassWord { get; set; }
        public string CommunityCode { get; set; }
        public string MerchantName { get; set; }
        public MerchantEnum DiningType { get; set; }
        public string Phone { get; set; }
        public Guid? DingRoleId { get; set; }
        public AuditEnum AuditType { get; set; }
        public string AreaTree { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Town { get; set; }
        public string TypePath
        {
            get
            {
                if (!string.IsNullOrEmpty(Province) || !string.IsNullOrEmpty(City) || !string.IsNullOrEmpty(Area) || !string.IsNullOrEmpty(Town))
                    return Province + "," + City + "," + Area + "," + Town;
                else return null;
            }
        }
    }
    public class RequestMerchantAttach
    {
       public Guid InfoId { get; set; }
        public string Certification { get; set; }
        public string ImplUser { get; set; }
       
    }
}
