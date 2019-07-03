using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.Phone
{
    public class BaseInfo
    {
        public string 二维码 { get; set; }
        public string 开始 { get; set; }
        public string 结束 { get; set; }
        public string 产品名称 { get; set; }
        public string 产品类型 { get; set; }
        public string 保质期 { get; set; }
        public string 规格 { get; set; }
        public string 产品系列 { get; set; }
        public string 入库批次 { get; set; }
        public string 生产时间 { get; set; }
        public string 成长档案 { get; set; }
        public string 产品图片 { get; set; }
        public string 产品介绍 { get; set; }
        public string 出库批次 { get; set; }
        public string 出库时间 { get; set; }
        public string 企业 { get; set; }
        public string 企业名称 { get; set; }
        public string 企业类型名称 { get; set; }
        public string 企业类型 { get; set; }
        public string 企业地址 { get; set; }
        public string 企业电话 { get; set; }
        public string 投诉电话 { get; set; }
        public string 主要产品 { get; set; }
        public string 主要介绍 { get; set; }
        public string 企业形象 { get; set; }
        public string 安全员 { get; set; }
        public string 安全等级 { get; set; }
        public string 企业等级 { get; set; }
        public string 证件到期 { get; set; }
        public string 经纬度 { get; set; }
        public string 企业介绍 { get; set; }
        public string 营业执照 { get; set; }
        public string 荣誉证书 { get; set; }
        public string 工商代码 { get; set; }
        public string 企业网站 { get; set; }
        public string 所在区域 { get; set; }
        public string 仓库名称 { get; set; }
        public string 贮藏方式 { get; set; }
        public string 贮藏温度 { get; set; }
        public string 贮藏湿度 { get; set; }
        public string 报告名称 { get; set; }
        public string 质检单位 { get; set; }
        public string 质检人 { get; set; }
        public string 质检结果 { get; set; }
        public string 质检报告 { get; set; }
        public string 装车编号 { get; set; }
        public string 发货绑定码 { get; set; }
        public string 发货批次 { get; set; }
        public string 运单号 { get; set; }
        public string 发货时间 { get; set; }
        public string 收货人 { get; set; }
        public string 收货地址 { get; set; }
        public string 发货地址 { get; set; }
        public string 交通工具 { get; set; }
        public string 运输方式 { get; set; }
        public bool 收货标志 { get; set; }
        public long 结束整型 => string.IsNullOrEmpty(结束) ? 0 : Convert.ToInt64(结束);
        public long 开始整型 => string.IsNullOrEmpty(开始) ? 0 : Convert.ToInt64(开始);
        public long 发货绑定码整型 => string.IsNullOrEmpty(发货绑定码) ? 0 : Convert.ToInt64(发货绑定码);
        #region 生产企业
        public string 设备名称 { get; set; }
        public string 设备供应商 { get; set; }
        public string 车间名称 { get; set; }
        public string 环境信息 { get; set; }
        public string 召回开始时间 { get; set; }
        public string 召回截至时间 { get; set; }
        public List<Material> Materials { get; set; }
        #endregion
        #region 种养企业
        public List<Plant> Plants { get; set; }
        public List<Drug> Drugs { get; set; }
        public List<Environ> Environs { get; set; }
        #endregion
        #region 政府抽查
        public int 抽查次数 { get; set; }
        public int 通报次数 { get; set; }
        public string 合格率 { get; set; }
        public int 投诉次数 { get; set; }
        #endregion
        public int 扫码次数 { get; set; }
    }
    public class Material
    {
        public string 原料 { get; set; }
        public string 原料名称 { get; set; }
        public string 原料保质期 { get; set; }
        public string 原料供应商 { get; set; }
        public string 采购时间 { get; set; }
        public string 原料生产时间 { get; set; }
        public string 质检单位 { get; set; }
        public string 质检人 { get; set; }
        public string 质检报告 { get; set; }
        public string 质检结果 { get; set; }
    }
    public class Plant
    {
        public string FeedName { get; set; }
        public DateTime? PlantTime { get; set; }
        public string Producter { get; set; }
    }
    public class Drug
    {
        public string DrugName { get; set; }
        public DateTime? PlantTime { get; set; }
        public string Producter { get; set; }
    }
    public class Environ
    {
        public string AirEnv { get; set; }
        public string AirHdy { get; set; }
        public string SoilEnv { get; set; }
        public string SoilHdy { get; set; }
        public string Light { get; set; }
        public string CO2 { get; set; }
        public decimal Lights => string.IsNullOrEmpty(Light) ? 0 : (Convert.ToDecimal(Light) / 100);
        public decimal CO2s => string.IsNullOrEmpty(CO2) ? 0 : (Convert.ToDecimal(CO2) / 100);
    }
}
