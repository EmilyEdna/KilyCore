using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterprisePackCodeBind
    {
        public Guid Id { get; set; }
        public Guid? CompanyId { get; set; }
        /// <summary>
        /// 装包批次
        /// </summary>
        public string PacBatchNo { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string GoodName { get; set; }
        /// <summary>
        /// 装包规格
        /// </summary>
        public string PacSpec { get; set; }
        /// <summary>
        /// 装包时间
        /// </summary>
        public DateTime? PacTime { get; set; }
        /// <summary>
        /// 包码
        /// </summary>
        public string PackCode { get; set; }
        /// <summary>
        /// 装箱码
        /// </summary>
        public string ThingCode { get; set; }
        public Guid? TagId { get; set; }
    }
}
