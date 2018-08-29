using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：CookFood
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Cook
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/29 10:53:00
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Cook
{
    /// <summary>
    /// 食材采购表
    /// </summary>
    public class CookFood : CookBase
    {
        /// <summary>
        /// 食材名称
        /// </summary>
        public virtual string FoodName { get; set; }
        /// <summary>
        /// 食材类型
        /// </summary>
        public virtual string FoodType { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public virtual string Number { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public virtual string Supplier { get; set; }
        /// <summary>
        /// 采购时间
        /// </summary>
        public virtual DateTime? BuyTime { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 生产地址
        /// </summary>
        public virtual string ProductionAddress { get; set; }
        /// <summary>
        /// 采购负责人
        /// </summary>
        public virtual string BuyUser { get; set; }
        /// <summary>
        /// 质检报告
        /// </summary>
        public virtual string Report { get; set; }
    }
}
