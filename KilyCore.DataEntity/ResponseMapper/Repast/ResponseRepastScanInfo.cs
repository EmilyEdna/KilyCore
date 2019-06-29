using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseRepastScanInfo
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/11/2 9:56:00
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Repast
{
    public class ResponseRepastScanInfo
    {
        public Guid Id { get; set; }
        public Guid? InfoId { get; set; }
        /// <summary>
        /// 记录名称
        /// </summary>
        public string RecordName { get; set; }
        /// <summary>
        /// <summary>
        /// 菜品Ids
        /// </summary>
        public string DishIds { get; set; }
        /// <summary>
        /// 原料Ids
        /// </summary>
        public string StuffIds { get; set; }
        /// <summary>
        /// 视频Ids
        /// </summary>
        public string VideoIds { get; set; }
        /// <summary>
        /// 用户Ids
        /// </summary>
        public string UserIds { get; set; }
        /// <summary>
        /// 废物处理Ids
        /// </summary>
        public string DuckIds { get; set; }
        /// <summary>
        /// 抽样Ids
        /// </summary>
        public string DrawIds { get; set; }
        /// <summary>
        /// 消毒剂Ids
        /// </summary>
        public string DisinfectIds { get; set; }
        /// <summary>
        /// 留样Ids
        /// </summary>
        public string SampleIds { get; set; }
        /// <summary>
        /// 添加剂Ids
        /// </summary>
        public string AdditiveIds { get; set; }
        /// <summary>
        /// 上架时间
        /// </summary>
        public DateTime? ShowTime { get; set; }
        public bool? IsDelete { get; set; }
        public string Stutas => IsDelete == false ? "下架" : "上架";

    }
    public class ResponseRepastScanInfos
    {
        public List<ResponseRepastInfoScan> InfoScan { get; set; }
        public List<ResponseRepastDishScan> DishScan { get; set; }
        public List<ResponseRepastStuffScan> StuffScan { get; set; }
        public List<ResponseRepastVedioScan> VedioScan { get; set; }
        public List<ResponseRepastUserScan> UserScan { get; set; }
        public List<ResponseRepastDuckScan> DuckScan { get; set; }
        public List<ResponseRepastDrawScan> DrawScan { get; set; }
        public List<ResponseRepastSampleScan> SampleScan { get; set; }
        public List<ResponseRepastDisinfectScan> DisinfectScan { get; set; }
        public List<ResponseRepastAdditiveScan> AdditiveScan { get; set; }
    }
    public class ResponseRepastInfoScan
    {
        public string 商家名称 { get; set; }
        public Guid 商家Id { get; set; }
        public string 营业执照 { get; set; }
        public string 商家地址 { get; set; }
        public string 商家电话 { get; set; }
        public string 商家形象 { get; set; }
        public string 商家介绍 { get; set; }
    }
    public class ResponseRepastDishScan
    {
        public string 菜品名称 { get; set; }
        public string 菜品类型 { get; set; }
        public string 配料 { get; set; }
        public string 主料 { get; set; }
        public string 调料 { get; set; }
        public string 菜品介绍 { get; set; }
    }
    public class ResponseRepastStuffScan
    {
        public string 原料名称 { get; set; }
        public string 供应商 { get; set; }
        public string 质检报告 { get; set; }
        public string 保质期 { get; set; }
        public string 生产地址 { get; set; }
        public string 采购人 { get; set; }
        public DateTime? 供应时间 { get; set; }
    }
    public class ResponseRepastVedioScan
    {
        public string 监控区域 { get; set; }
        public string 视频连接 { get; set; }
    }
    public class ResponseRepastUserScan
    {
        public string 人员姓名 { get; set; }
        public string 人员电话 { get; set; }
        public string 健康证 { get; set; }
    }
    public class ResponseRepastDuckScan
    {
        public string 处理方式 { get; set; }
        public DateTime? 处理时间 { get; set; }
        public string 处理人 { get; set; }
        public string 相关图片 { get; set; }
    }
    public class ResponseRepastDrawScan
    {
        public string 抽样单位 { get; set; }
        public DateTime? 抽样时间 { get; set; }
        public string 抽样负责人 { get; set; }
        public string 负责人电话 { get; set; }
    }
    public class ResponseRepastSampleScan
    {
        public string 留样菜品 { get; set; }
        public DateTime? 留样时间 { get; set; }
        public string 操作人 { get; set; }
        public string 操作人电话 { get; set; }
        public string 留样图片 { get; set; }
    }
    public class ResponseRepastDisinfectScan
    {
        public string 消毒剂名称 { get; set; }
        public DateTime? 消毒时间 { get; set; }
        public string 使用人 { get; set; }
        public string 使用计量 { get; set; }
        public string 生产商 { get; set; }
    }
    public class ResponseRepastAdditiveScan
    {
        public string 添加剂名称 { get; set; }
        public string 产物 { get; set; }
        public string 使用人 { get; set; }
        public string 使用计量 { get; set; }
        public string 生产商 { get; set; }
    }
}
