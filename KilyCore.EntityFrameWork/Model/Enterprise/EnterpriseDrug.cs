using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 施养管理表
    /// </summary>
    public class EnterpriseDrug : BaseEntity
    {
        /// <summary>
        /// 所属企业Id
        /// </summary>
        public virtual Guid CompanyId { get; set; }
        /// <summary>
        /// 药品名称
        /// </summary>
        public virtual string DrugName { get; set; }
        /// <summary>
        /// 药品品牌
        /// </summary>
        public virtual string Brand { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public virtual string Supplier { get; set; }
        /// <summary>
        /// 施药时间
        /// </summary>
        public virtual DateTime PlantTime { get; set; }
        /// <summary>
        /// 检测报告
        /// </summary>
        public virtual string CheckReport { get; set; }
    }
}
