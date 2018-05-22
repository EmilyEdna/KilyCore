using KilyCore.EntityFrameWork.ModelEnum;
using System;

namespace KilyCore.DataEntity.ResponseMapper.System
{
    public class ResponseAdmin
    {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public string TrueName { get; set; }
        public string PassWord { get; set; }
        public string IdCard { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BankCard { get; set; }
        public string BankName { get; set; }
        public string TableName { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public string TypePath { get; set; }
        public AccountEnum AccountType { get; set; }
        /// <summary>
        /// 账号类型Id
        /// </summary>
        public Guid RoleAuthorType { get; set; }
        /// <summary>
        /// 账号类型名称
        /// </summary>
        public string AccountTypeName { get; set; }
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
    }
}
