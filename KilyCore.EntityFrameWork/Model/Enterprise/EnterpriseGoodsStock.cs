using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseGoodsStock
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/13 10:50:58
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 产品仓库表
    /// </summary>
    public class EnterpriseGoodsStock:EnterpriseBase
    {
        /// <summary>
        /// 入库批次号
        /// </summary>
        public virtual string GoodsBatchNo { get; set; }
        /// <summary>
        /// 产品表Id
        /// </summary>
        public virtual Guid GoodsId { get; set; }
        /// <summary>
        /// 仓库类型
        /// </summary>
        public virtual string StockType { get; set; }
        /// <summary>
        /// 入库数量
        /// </summary>
        public virtual string InStockNum { get; set; }
        /// <summary>
        /// 生产批次表Id
        /// </summary>
        public virtual Guid BatchId { get; set; }
        /// <summary>
        /// 质检单位
        /// </summary>
        public virtual string CheckUnit { get; set; }
        /// <summary>
        /// 质检员
        /// </summary>
        public virtual string CheckUser { get; set; }
        /// <summary>
        /// 质检报告
        /// </summary>
        public virtual string CheckResult { get; set; }
    }
}
