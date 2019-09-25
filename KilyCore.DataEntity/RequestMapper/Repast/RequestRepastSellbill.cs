using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestRepastSellbill
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/3 11:18:45
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Repast
{
    public class RequestRepastSellbill
    {
        public Guid Id { get; set; }
        public Guid InfoId { get; set; }
        /// <summary>
        /// 物品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 销售数量
        /// </summary>
        public string GoodsNum { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public string UnPay { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public string ToPay { get; set; }
        /// <summary>
        /// 销售时间
        /// </summary>
        public DateTime? SellTime { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string Manager { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public string NoExp { get; set; }
        /// <summary>
        /// 生成日期
        /// </summary>
        public DateTime? ProTime { get; set; }
    }
    public class RequestBillTicket {
        public Guid? InfoId { get; set; }
        public string Theme { get; set; }
        public DateTime? UpTime { get; set; }
        public string Content { get; set; }
    }
}
