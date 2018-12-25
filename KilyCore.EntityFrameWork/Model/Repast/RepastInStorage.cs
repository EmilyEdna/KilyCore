using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastInStorage
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/7 11:27:44
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Repast
{
    /// <summary>
    /// 餐饮入库表
    /// </summary>
    public class RepastInStorage : RepastBase
    {
        /// <summary>
        /// 批次号
        /// </summary>
        public virtual string BatchNo { get; set; }
        /// <summary>
        /// 食材名称
        /// </summary>
        public virtual string IngredientName { get; set; }
        /// <summary>
        /// 物料类型
        /// </summary>
        public virtual string MaterType { get; set; }
        /// <summary>
        /// 入库数量
        /// </summary>
        public virtual int InStorageNum { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public virtual decimal? PrePrice { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public virtual decimal? ToPrice { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public virtual string Supplier { get; set; }
        /// <summary>
        /// 供应时间
        /// </summary>
        public virtual DateTime? SuppTime { get; set; }
        /// <summary>
        /// 保质期
        /// </summary>
        public virtual string ExpiredDay { get; set; }
        /// <summary>
        /// 质检报告
        /// </summary>
        public virtual string QualityReport { get; set; }
        /// <summary>
        /// 生产地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 溯源连接
        /// </summary>
        public virtual string SourceLink { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 采购负责人
        /// </summary>
        public virtual string BuyUser { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public virtual string Unit { get; set; }
    }
}
