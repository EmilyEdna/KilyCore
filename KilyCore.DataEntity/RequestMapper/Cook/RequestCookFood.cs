using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：CookFood
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Cook
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/29 11:12:57
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Cook
{
    public class RequestCookFood
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 厨师会员表Id
        /// </summary>
        public Guid? CookId { get; set; }
        /// <summary>
        /// 食材名称
        /// </summary>
        public string FoodName { get; set; }
        /// <summary>
        /// 食材类型
        /// </summary>
        public string FoodType { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }
        /// <summary>
        /// 采购时间
        /// </summary>
        public DateTime? BuyTime { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 生产地址
        /// </summary>
        public string ProductionAddress { get; set; }
        /// <summary>
        /// 采购负责人
        /// </summary>
        public string BuyUser { get; set; }
        /// <summary>
        /// 质检报告
        /// </summary>
        public string Report { get; set; }
        /// <summary>
        /// 是否使用
        /// </summary>
        public bool? IsUse { get; set; }
    }
}
