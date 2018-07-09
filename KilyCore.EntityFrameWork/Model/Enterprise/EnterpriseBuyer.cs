using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseBuyer
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/9 14:25:55
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 进货表
    /// </summary>
    public class EnterpriseBuyer:EnterpriseBase
    {
        /// <summary>
        /// 进货批次
        /// </summary>
        public virtual string BatchNo { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public virtual string GoodName { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public virtual string Spec { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public virtual string Num { get; set; }
        /// <summary>
        /// 生成日期
        /// </summary>
        public virtual DateTime? ProTime { get; set; }
        /// <summary>
        /// 保质期
        /// </summary>
        public virtual string ExpiredDate { get; set; }
        /// <summary>
        /// 进货日期
        /// </summary>
        public virtual DateTime? GetGoodsTime { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public virtual string Supplier { get; set; }
    }
}
