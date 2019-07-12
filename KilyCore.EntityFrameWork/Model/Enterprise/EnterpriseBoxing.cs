using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：KilyCore.EntityFrameWork.Model.Enterprise
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/12/21 14:55:12
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 装箱表
    /// </summary>
    public class EnterpriseBoxing: EnterpriseBase
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string GoodName { get; set; }
        /// <summary>
        /// 入库批次
        /// </summary>
        public virtual string StockBatchNo { get; set; }
        /// <summary>
        /// 生产批次
        /// </summary>
        public virtual string ProductionBatchNo { get; set; }
        /// <summary>
        /// 装箱批次
        /// </summary>
        public virtual string BoxBatchNo { get; set; }
        /// <summary>
        /// 箱码
        /// </summary>
        public virtual string BoxCode { get; set; }
        /// <summary>
        /// 装箱数量
        /// </summary>
        public virtual string BoxCount { get; set; }
        /// <summary>
        /// 物品码
        /// </summary>
        public virtual string ThingCode { get; set; }
        /// <summary>
        /// 装箱时间
        /// </summary>
        public virtual DateTime? BoxTime { get; set; }
        /// <summary>
        /// 箱码短码
        /// </summary>
        public virtual Int64 BoxCodeSort { get; set; }
        /// <summary>
        /// 发货使用过的码
        /// </summary>
        public virtual string SendTag { get; set; }
    }
}
