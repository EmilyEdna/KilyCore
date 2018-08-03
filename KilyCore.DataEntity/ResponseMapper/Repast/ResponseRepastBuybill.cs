using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseRepastBuybill
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/3 11:21:56
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Repast
{
    public class ResponseRepastBuybill
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 物品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 进货数量
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
        /// 进货时间
        /// </summary>
        public DateTime? OrderTime { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkPhone { get; set; }
        /// <summary>
        /// 采购负责人
        /// </summary>
        public string Purchase { get; set; }
    }
}
