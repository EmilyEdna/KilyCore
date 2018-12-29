using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseGoodsStockAttach
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/13 14:30:11
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 产品出库表
    /// </summary>
    public class EnterpriseGoodsStockAttach: EnterpriseBase
    {
        /// <summary>
        /// 出库类型
        /// </summary>
        public virtual string OutStockType { get; set; }
        /// <summary>
        /// 出库批次号
        /// </summary>
        public virtual string GoodsBatchNo { get; set; }
        /// <summary>
        /// 产品入库表Id
        /// </summary>
        public virtual Guid StockId { get; set; }
        /// <summary>
        /// 出库负责人
        /// </summary>
        public virtual string OutStockUser { get; set; }
        /// <summary>
        /// 出库数量
        /// </summary>
        public virtual int OutStockNum { get; set; }
        /// <summary>
        /// 出库时间
        /// </summary>
        public virtual DateTime OutStockTime { get; set; }
        /// <summary>
        /// 装箱码
        /// </summary>
        public virtual string BoxCodeNo { get; set; }
        /// <summary>
        /// 装箱数量
        /// </summary>
        public virtual string BoxCount { get; set; }
    }
}
