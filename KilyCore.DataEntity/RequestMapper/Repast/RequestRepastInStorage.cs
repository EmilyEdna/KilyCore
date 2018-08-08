using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastInStorageMap
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/7 14:09:38
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Repast
{
    public class RequestRepastInStorage
    {
        public Guid Id { get; set; }
        public Guid InfoId { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }
        /// <summary>
        /// 食材名称
        /// </summary>
        public string IngredientName { get; set; }
        /// <summary>
        /// 入口数量
        /// </summary>
        public int InStorageNum { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal? PrePrice { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal? ToPrice { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }
        /// <summary>
        /// 供应时间
        /// </summary>
        public DateTime? SuppTime { get; set; }
        /// <summary>
        /// 保质期
        /// </summary>
        public string ExpiredDay { get; set; }
        /// <summary>
        /// 质检报告
        /// </summary>
        public string QualityReport { get; set; }
        /// <summary>
        /// 生产地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 溯源连接
        /// </summary>
        public string SourceLink { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 采购负责人
        /// </summary>
        public string BuyUser { get; set; }
    }
}
