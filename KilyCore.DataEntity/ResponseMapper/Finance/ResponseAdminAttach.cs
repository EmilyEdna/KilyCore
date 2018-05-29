using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.DataEntity.ResponseMapper.Finance
{
    public class ResponseAdminAttach
    {
        public Guid Id { get; set; }
        public string TrueName { get; set; }
        public string IdCard { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string AccountTypeName { get; set; }
        public AccountEnum AccountType { get; set; }
        public Guid AdminId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool? IsPay { get; set; }
        public decimal Money { get; set; }
        public string PayUser { get; set; }
        public bool? IsDelete { get; set; }
        public string UseOrStop
        {
            get
            {
                if (IsDelete.HasValue)
                    if ((bool)IsDelete)
                        return "停用中";
                    else
                        return "启用中";
                return null;
            }
        }
        public string PayOrNoPay
        {
            get
            {
                if (IsPay.HasValue)
                    if ((bool)IsPay)
                        return "已支付";
                    else
                        return "未支付";
                return null;
            }
        }
    }
}
