using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastSellbill
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/3 10:40:59
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Repast
{
    /// <summary>
    /// 销售台账表
    /// </summary>
    public class RepastSellbill: RepastBase
    {
        /// <summary>
        /// 物品名称
        /// </summary>
        public virtual string GoodsName { get; set; }
        /// <summary>
        /// 销售数量
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
        /// 销售时间
        /// </summary>
        public virtual DateTime? SellTime { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public virtual string Manager { get; set; }
    }
}
