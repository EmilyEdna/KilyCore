using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseEnterpriseScanCode
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/13 14:51:29
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseScanCode
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProductType { get; set; }
        /// <summary>
        /// 保质期
        /// </summary>
        public string ExpiredDate { get; set; }
        /// <summary>
        /// 生产批次
        /// </summary>
        public string BatchNo { get; set; }
        /// <summary>
        /// 开始号段
        /// </summary>
        public long StarSerialNo { get; set; }
        /// <summary>
        /// 结束号段
        /// </summary>
        public long EndSerialNo { get; set; }
        /// <summary>
        /// 开始号段
        /// </summary>
        public string StarSerialNos { get; set; }
        /// <summary>
        /// 结束号段
        /// </summary>
        public string EndSerialNos { get; set; }
        public bool? IsCreate { get; set; }
    }
    public class ResponseEnterpriseScanCodeContent
    {
        /// <summary>
        /// 基础信息
        /// </summary>
        public List<ResponseEnterpriseScanCodeBaseInfos> BaseInfo { get; set; }
        /// <summary>
        ///关键点
        /// </summary>
        public List<ResponseEnterpriseScanCodeTarget> TargetInfo { get; set; }
        /// <summary>
        /// 原料一类
        /// </summary>
        public List<ResponseEnterpriseScanCodeMater> MaterInfo { get; set; }
        /// <summary>
        /// 车间
        /// </summary>
        public List<ResponseEnterpriseScanCodeFacility> FacilityInfo { get; set; }
        /// <summary>
        /// 种养殖质检报告
        /// </summary>
        public List<ResponseEnterpriseScanCodeNote> CheckNote { get; set; }
        /// <summary>
        /// 成长阶段
        /// </summary>
        public List<ResponseEnterpriseScanCodeAgeLV> AgeLVInfo { get; set; }
        /// <summary>
        /// 环境检测
        /// </summary>
        public List<ResponseEnterpriseScanCodeEnv> EnvInfo { get; set; }
        /// <summary>
        /// 施肥或者喂养
        /// </summary>
        public List<ResponseEnterpriseScanCodePlant> PlantInfo { get; set; }
        /// <summary>
        /// 药品
        /// </summary>
        public List<ResponseEnterpriseScanCodeDrugOne> DrugOneInfo { get; set; }
        /// <summary>
        /// 疫苗
        /// </summary>
        public List<ResponseEnterpriseScanCodeDrugTwo> DrugTwoInfo { get; set; }
        /// <summary>
        /// 投诉
        /// </summary>
        public List<ResponseEnterpriseScanCodeComplain> ComplainInfo { get; set; }
        /// <summary>
        /// 召回
        /// </summary>
        public List<ResponseEnterpriseScanCodeRecover> RecoverInfo { get; set; }
    }
    public class ResponseEnterpriseScanCodeNote
    {
        public string 成长档案质检报告 { get; set; }
        public string 批次编号 { get; set; }
    }
    public class ResponseEnterpriseScanCodeBaseInfos
    {
        public Guid? 二维码Id { get; set; }
        public Guid? 成长Id { get; set; }
        public Guid? 企业Id { get; set; }
        public Guid? 设施Id { get; set; }
        public string 产品名称 { get; set; }
        public string 产品保质期 { get; set; }
        public string 入库负责人 { get; set; }
        public string 入库批次 { get; set; }
        public string 产品图片 { get; set; }
        public string 产品介绍 { get; set; }
        public string 出库负责人 { get; set; }
        public string 出库批次 { get; set; }
        public string 生产商或发布企业 { get; set; }
        public string 生产地址 { get; set; }
        public int 企业类型 { get; set; }
        public string 企业类型名称 { get; set; }
        public string 所属区域 { get; set; }
        public DateTime? 生产时间 { get; set; }
        public string 生产批次 { get; set; }
        public string 生产负责人 { get; set; }
        public string 原料列表 { get; set; }
        public string 设备名称 { get; set; }
        public string 设备生产商 { get; set; }
        public string 设备负责人 { get; set; }
        public string 储藏方式 { get; set; }
        public string 储藏温度 { get; set; }
        public string 储藏湿度 { get; set; }
        public string 进货批次 { get; set; }
        public string 进货名称 { get; set; }
        public string 供应商 { get; set; }
        public string 检测报告 { get; set; }
        public string 进货规格 { get; set; }
        public string 进货产地 { get; set; }
        public string 产品质检单位 { get; set; }
        public string 产品质检人 { get; set; }
        public string 产品质检结果 { get; set; }
        public string 产品质检报告 { get; set; }
        public string 发货批次 { get; set; }
        public DateTime? 发货时间 { get; set; }
        public string 经销商 { get; set; }
        public string 运输方式 { get; set; }
        public string 交通工具 { get; set; }
        public int? 认证星级 { get; set; }
    }
    public class ResponseEnterpriseScanCodeTarget
    {
        public string 关键点名称 { get; set; }
        public string 关键点阙值 { get; set; }
        public string 关键点单位 { get; set; }
        public string 结果 { get; set; }
    }
    public class ResponseEnterpriseScanCodeMater
    {
        public Guid? 原料Id { get; set; }
        public string 原料名称 { get; set; }
        public string 原料保质期 { get; set; }
        public string 原料供应商 { get; set; }
        public DateTime? 采购时间 { get; set; }
        public DateTime? 原料生产时间 { get; set; }
        public string 质检单位 { get; set; }
        public string 质检人 { get; set; }
        public string 质检报告 { get; set; }
        public string 质检结果 { get; set; }
    }
    public class ResponseEnterpriseScanCodeFacility
    {
        public string 车间名称 { get; set; }
        public string 环境信息 { get; set; }
    }
    public class ResponseEnterpriseScanCodeAgeLV
    {
        public string 阶段名称 { get; set; }
        public string 阶段图片 { get; set; }
    }
    public class ResponseEnterpriseScanCodeEnv
    {
        public string 土壤报告 { get; set; }
        public string 空气报告 { get; set; }
        public string 水质报告 { get; set; }
        public string 金属报告 { get; set; }
    }
    public class ResponseEnterpriseScanCodePlant
    {
        public string 肥料名称 { get; set; }
        public DateTime? 施肥时间 { get; set; }
        public string 肥料生产商 { get; set; }
    }
    public class ResponseEnterpriseScanCodeDrugOne
    {
        public string 药品名称 { get; set; }
        public DateTime? 施药时间 { get; set; }
        public string 农药生产商 { get; set; }
    }
    public class ResponseEnterpriseScanCodeDrugTwo
    {
        public string 疫苗名称 { get; set; }
        public DateTime? 接种时间 { get; set; }
        public string 疫苗生产商 { get; set; }
    }
    public class ResponseEnterpriseScanCodeGovt
    {
        public int? 抽查次数 { get; set; }
        public int? 通报次数 { get; set; }
        public string 合格率 { get; set; }
        public DateTime? 通报时间 { get; set; }
    }
    public class ResponseEnterpriseScanCodeComplain
    {
        public int? 投诉次数 { get; set; }
    }
    public class ResponseEnterpriseScanCodeRecover
    {
        public DateTime? 召回开始时间 { get; set; }
        public DateTime? 召回截至时间 { get; set; }
    }
}
