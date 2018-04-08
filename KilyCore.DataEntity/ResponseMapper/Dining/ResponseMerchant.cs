using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.Dining
{
    public class ResponseMerchant
    {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public string PassWord { get; set; }
        public string CommunityCode { get; set; }
        public string MerchantName { get; set; }
        public string Email { get; set; }
        public MerchantEnum DiningType { get; set; }
        public string DiningTypeName { get; set; }
        public string Phone { get; set; }
        public Guid? DingRoleId { get; set; }
        public string TypePath { get; set; }
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
        public string Certification { get; set; }
        public string ImplUser { get; set; }
    }
}
