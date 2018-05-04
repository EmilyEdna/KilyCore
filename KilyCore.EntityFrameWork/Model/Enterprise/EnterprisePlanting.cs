using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 施养管理表
    /// </summary>
    public class EnterprisePlanting:BaseEntity
    {
        /// <summary>
        /// 所属企业Id
        /// </summary>
        public virtual Guid CompanyId { get; set; }
        /// <summary>
        /// 饲料名称
        /// </summary>
        public virtual string FeedName { get; set; }
        /// <summary>
        /// 饲料品牌
        /// </summary>
        public virtual string Brand { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public virtual string Supplier { get; set; }
        /// <summary>
        /// 施肥时间
        /// </summary>
        public virtual DateTime PlantTime { get; set; }
        /// <summary>
        /// 检测报告
        /// </summary>
        public virtual string CheckReport { get; set; }
    }
}
