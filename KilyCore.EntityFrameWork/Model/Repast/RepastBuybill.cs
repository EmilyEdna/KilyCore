using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastBuybill
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/3 9:46:56
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Repast
{
    /// <summary>
    /// 进货台账表
    /// </summary>
    public class RepastBuybill: RepastBase
    {
        /// <summary>
        /// 物品名称
        /// </summary>
        public virtual string GoodsName { get; set; }
        /// <summary>
        /// 进货数量
        /// </summary>
        public virtual string GoodsNum { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public virtual string UnPay { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public virtual string ToPay { get; set; }
        /// <summary>
        /// 进货时间
        /// </summary>
        public virtual DateTime? OrderTime { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public virtual string Supplier { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public virtual string Unit { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string LinkPhone { get; set; }
        /// <summary>
        /// 采购负责人
        /// </summary>
        public virtual string Purchase { get; set; }
    }
}
