using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 环境检测表
    /// </summary>
    public class EnterpriseEnvironment : BaseEntity
    {
        /// <summary>
        /// 所属企业Id
        /// </summary>
        public virtual Guid CompanyId { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public virtual DateTime? RecordTime { get; set; }
        /// <summary>
        /// 空气温度
        /// </summary>
        public virtual string AirEnv { get; set; }
        /// <summary>
        /// 土壤温度
        /// </summary>
        public virtual string SoilEnv { get; set; }
        /// <summary>
        /// 空气湿度
        /// </summary>
        public virtual string AirHdy { get; set; }
        /// <summary>
        /// 土壤湿度
        /// </summary>
        public virtual string SoilHdy { get; set; }
        /// <summary>
        /// 光照
        /// </summary>
        public virtual string Light { get; set; }
    }
    /// <summary>
    /// 环境检测附加表
    /// </summary>
    public class EnterpriseEnvironmentAttach : BaseEntity
    {
        /// <summary>
        /// 环境主表Id
        /// </summary>
        public virtual Guid EnvId { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public virtual DateTime? RecordTime { get; set; }
        /// <summary>
        /// 土壤报告
        /// </summary>
        public virtual string SoilReport { get; set; }
        /// <summary>
        /// 空气报告
        /// </summary>
        public virtual string AirReport { get; set; }
        /// <summary>
        /// 水质报告
        /// </summary>
        public virtual string WaterReport { get; set; }
        /// <summary>
        /// 金属报告
        /// </summary>
        public virtual string MetalReport { get; set; }
    }
}
