using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseDrug
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string DrugName { get; set; }
        public string Brand { get; set; }
        public string Supplier { get; set; }
        public DateTime PlantTime { get; set; }
        public string CheckReport { get; set; }
        public string BatchNo { get; set; }
        public int IsType { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string SupplierName { get; set; }
    }
}
