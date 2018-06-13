using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestEnterpriseGoodsStock
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/13 15:22:02
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseGoodsStock
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string GoodsBatchNo { get; set; }
        public Guid GoodsId { get; set; }
        public string GoodsName { get; set; }
        public string StockType { get; set; }
        public string InStockNum { get; set; }
        public Guid BatchId { get; set; }
        public string CheckUnit { get; set; }
        public string CheckUser { get; set; }
        public string CheckResult { get; set; }
    }
    public class RequestEnterpriseGoodsStockAttach
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string OutStockType { get; set; }
        public string GoodsBatchNo { get; set; }
        public Guid StockId { get; set; }
        public string OutStockUser { get; set; }
        public string Seller { get; set; }
    }
}
