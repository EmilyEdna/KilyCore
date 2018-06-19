using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseEnterpriseMaterialStock
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/7 9:55:46
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseMaterialStock
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string SerializNo { get; set; }
        public string BatchNo { get; set; }
        public string MaterName { get; set; }
        public int SetStockNum { get; set; }
        public DateTime? SetStockTime { get; set; }
        public DateTime? ProductTime { get; set; }
        public string SetStockUser { get; set; }
        public string StockType { get; set; }
        public Guid CheckMaterialId { get; set; }
    }
    public class ResponseEnterpriseMaterialStockAttach {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string BatchNo { get; set; }
        public Guid MaterialStockId { get; set; }
        public string SerializNo { get; set; }
        public int OutStockNum { get; set; }
        public DateTime? OutStockTime { get; set; }
        public string OutStockUser { get; set; }
        public string MaterName { get; set; }
        public string StockType { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public int StockEx { get; set; }
    }
}
