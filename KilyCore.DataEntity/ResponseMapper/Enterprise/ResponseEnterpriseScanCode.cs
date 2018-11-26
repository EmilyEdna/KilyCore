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
        /// 抽查次数
        /// </summary>
        public int PotrolNum { get; set; }
        /// <summary>
        /// 通报次数
        /// </summary>
        public int BulletinNum { get; set; }
        /// <summary>
        /// 投诉举报
        /// </summary>
        public int Complain { get; set; }
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
        /// 生产时间
        /// </summary>
        public DateTime ProductTime { get; set; }
        /// <summary>
        /// 产品质检结果
        /// </summary>
        public string ProductCheckResult { get; set; }
        /// <summary>
        /// 产品质检报告
        /// </summary>
        public string ProductCheckReport { get; set; }
        /// <summary>
        /// 产品介绍
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 产品图片
        /// </summary>
        public string ImgUrl { get; set; }
        /// <summary>
        /// 产品说明书
        /// </summary>
        public string Explanation { get; set; }
        /// <summary>
        /// 生产批次
        /// </summary>
        public string BatchNo { get; set; }
        /// <summary>
        /// 生产设备
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 育苗名称
        /// </summary>
        public string GrowName { get; set; }
        /// <summary>
        /// 种植时间
        /// </summary>
        public DateTime PlantTime { get; set; }
        /// <summary>
        /// 育苗证书
        /// </summary>
        public string Paper { get; set; }
        /// <summary>
        /// 开始号段
        /// </summary>
        public long StarSerialNo { get; set; }
        /// <summary>
        /// 结束号段
        /// </summary>
        public long EndSerialNo { get; set; }
        public IList<ResponseEnterpriseScanCodeMaterial> Materials { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 企业地址
        /// </summary>
        public string CompanyAddress { get; set; }
        /// <summary>
        /// 经纬度
        /// </summary>
        public string LngAndLat { get; set; }
        /// <summary>
        /// 企业介绍
        /// </summary>
        public string Discription { get; set; }
        /// <summary>
        /// 网址
        /// </summary>
        public string NetAddress { get; set; }
        /// <summary>
        /// 物流单号
        /// </summary>
        public string WayBill { get; set; }
        /// <summary>
        /// 包装编号
        /// </summary>
        public string PackageNo { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 运输方式
        /// </summary>
        public string TransportWay { get; set; }
        /// <summary>
        /// 交通工具
        /// </summary>
        public string Traffic { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public string CompanyType { get; set; }
        public string TypePath { get; set; }
        public Guid? GrowNoteId { get; set; }
        public string OutStockBatchNo { get; set; }
        public ResponseEnterpriseRecover RecoverInfo { get; set; }
    }
    public class ResponseEnterpriseScanCodeMaterial
    {
        public Guid Id { get; set; }
        public string MaterName { get; set; }
        public string Supplier { get; set; }
        public string Standard { get; set; }
        public string MaterCheckResult { get; set; }
        public string MaterCheckReport { get; set; }
    }
}
