using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.DataEntity.RequestMapper.System
{
    public class RequestAdminAttach
    {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public  Guid AdminId { get; set; }
        public  DateTime? StartTime { get; set; }
        public  DateTime? EndTime { get; set; }
        public  bool? IsPay { get; set; }
        public  decimal Money { get; set; }
        public  string PayUser { get; set; }
    }
}
