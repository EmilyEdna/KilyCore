using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseEnterpriseGoodsStock
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/13 15:22:55
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseGoodsStock
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string GoodsBatchNo { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 产品表Id
        /// </summary>
        public Guid GoodsId { get; set; }
        public string StockType { get; set; }
        public int InStockNum { get; set; }
        /// <summary>
        /// 生产批次
        /// </summary>
        public string ProBatch { get; set; }
        public DateTime  ProductTime { get; set; }
        public string MaterialId { get; set; }
        public string MaterialName
        {
            get
            {
                if (!string.IsNullOrEmpty(MaterialId))
                    return String.Join(",", MaterialList.Where(t => MaterialId.Contains(t.Id.ToString())).Select(t => t.MaterName).ToArray());
                else
                    return null;
            }
        }
        public List<ResponseEnterpriseMaterial> MaterialList { get; set; }
        public string Manager { get; set; }
        public Guid CheckGoodsId { get; set; }
    }
    public class ResponseEnterpriseGoodsStockAttach
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string GoodsName { get; set; }
        public string OutStockType { get; set; }
        public string GoodsBatchNo { get; set; }
        /// <summary>
        /// 入库批次
        /// </summary>
        public string StockBatch { get; set; }
        public string OutStockUser { get; set; }
        public string Seller { get; set; }
        public int OutStockNum { get; set; }
        public DateTime OutStockTime { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public int StockEx { get; set; }
    }
}
