using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    /// <summary>
    /// 作者：刘泽华
    /// 时间：2018年5月29日11点13分
    /// </summary>
    public class ResponseEnterpriseEnvironment
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 所属企业Id
        /// </summary>
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? RecordTime { get; set; }
        /// <summary>
        /// 空气温度
        /// </summary>
        public string AirEnv { get; set; }
        /// <summary>
        /// 土壤温度
        /// </summary>
        public string SoilEnv { get; set; }
        /// <summary>
        /// 空气湿度
        /// </summary>
        public string AirHdy { get; set; }
        /// <summary>
        /// 土壤湿度
        /// </summary>
        public string SoilHdy { get; set; }
        /// <summary>
        /// 光照
        /// </summary>
        public string Light { get; set; }
        public string BatchNo { get; set; }
        /// <summary>
        /// CO2浓度
        /// </summary>
        public string CO2 { get; set; }
    }
    public class ResponseEnterpriseEnvironmentAttach
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 批次号段
        /// </summary>
        public string BatchNo { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? RecordTime { get; set; }
        /// <summary>
        /// 土壤报告
        /// </summary>
        public string SoilReport { get; set; }
        /// <summary>
        /// 空气报告
        /// </summary>
        public string AirReport { get; set; }
        /// <summary>
        /// 水质报告
        /// </summary>
        public string WaterReport { get; set; }
        /// <summary>
        /// 金属报告
        /// </summary>
        public string MetalReport { get; set; }
    }
}
