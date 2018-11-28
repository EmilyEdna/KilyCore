using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseMaterialStockAttach
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/7 10:39:36
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 原料出库表
    /// </summary>
    public class EnterpriseMaterialStockAttach: EnterpriseBase
    {
        /// <summary>
        /// 入库表Id
        /// </summary>
        public virtual Guid MaterialStockId { get; set; }
        /// <summary>
        /// 出库批次号
        /// </summary>
        public virtual string SerializNo { get; set; }
        /// <summary>
        /// 出库数量
        /// </summary>
        public virtual int OutStockNum { get; set; }
        /// <summary>
        /// 出库时间
        /// </summary>
        public virtual DateTime? OutStockTime { get; set; }
        /// <summary>
        /// 出库负责人
        /// </summary>
        public virtual string OutStockUser { get; set; }
        /// <summary>
        /// 仓库类型
        /// </summary>
        public virtual string StockType { get; set; }
    }
}
