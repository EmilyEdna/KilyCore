using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.System
{
    public class RequestAdmin
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
        public bool OpenNet { get; set; }
        public string Chapter { get; set; }
        public string TypePath
        {
            get
            {
                if (!string.IsNullOrEmpty(Province) || !string.IsNullOrEmpty(City) || !string.IsNullOrEmpty(Area)||!string.IsNullOrEmpty(Town))
                    return Province + "," + City + "," + Area+","+Town;
                else return null;
            }
        }
        public AccountEnum AccountType { get; set; }
        public Guid RoleAuthorType { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Town { get; set; }
        public string AreaTree { get; set; }
    }
}
