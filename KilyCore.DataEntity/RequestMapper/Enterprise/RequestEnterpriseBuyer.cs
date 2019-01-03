using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestEnterpriseBuyer
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/9 15:48:46
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseBuyer
    {
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 进货批次
        /// </summary>
        public string BatchNo { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodName { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Spec { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string Num { get; set; }
        /// <summary>
        /// 生成日期
        /// </summary>
        public DateTime? ProTime { get; set; }
        /// <summary>
        /// 保质期
        /// </summary>
        public string ExpiredDate { get; set; }
        /// <summary>
        /// 进货日期
        /// </summary>
        public DateTime? GetGoodsTime { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }
        /// <summary>
        /// 检测报告
        /// </summary>
        public string CheckReport { get; set; }
    }
}
