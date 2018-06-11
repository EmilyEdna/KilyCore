using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    /// <summary>
    /// 作者：刘泽华
    /// 时间：2018年5月29日11点13分
    /// </summary>
    public class ResponseEnterpriseGrowInfo
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string BatchNo { get; set; }
        public string GrowName { get; set; }
        public DateTime PlantTime { get; set; }
        public string BuyNum { get; set; }
        public string Unit { get; set; }
        public string Remark { get; set; }
        public string Paper { get; set; }
    }
}
