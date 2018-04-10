﻿using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterprise
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public string CompanyAccount { get; set; }
        public string PassWord { get; set; }
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
        public bool IsEnable { get; set; }
        public AuditEnum AuditType { get; set; }
    }
}