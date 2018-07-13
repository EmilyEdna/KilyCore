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
    }
    public class ResponseEnterpriseScanCodeMaterial {
        public Guid Id { get; set; }
        public string MaterName { get; set; }
        public string Supplier { get; set; }
        public string Standard { get; set; }
        public string MaterCheckResult { get; set; }
        public string MaterCheckReport { get; set; }
    }
}
