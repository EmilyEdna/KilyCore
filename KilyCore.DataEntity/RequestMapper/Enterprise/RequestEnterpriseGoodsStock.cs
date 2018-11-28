using KilyCore.EntityFrameWork.ModelEnum;
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
        public Guid? StockTypeId { get; set; }
        public string GoodsBatchNo { get; set; }
        public Guid GoodsId { get; set; }
        public string GoodsName { get; set; }
        public string StockType { get; set; }
        public int InStockNum { get; set; }
        public Guid BatchId { get; set; }
        public string Manager { get; set; }
        public DateTime ProductTime { get; set; }
        public Guid CheckGoodsId { get; set; }
        /// <summary>
        /// 产品图片
        /// </summary>
        public string ImgUrl { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        public string Explanation { get; set; }
        /// <summary>
        /// 成长日记表Id--种养殖企业
        /// </summary>
        public Guid? GrowNoteId { get; set; }
        /// <summary>
        /// 进货表Id--流通企业
        /// </summary>
        public Guid? BuyId { get; set; }
        public string TempPath { get; set; }
    }
    public class RequestEnterpriseGoodsStockAttach
    {
        public string GoodsName { get; set; }
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string OutStockType { get; set; }
        public string GoodsBatchNo { get; set; }
        public Guid StockId { get; set; }
        public string OutStockUser { get; set; }
        public string Seller { get; set; }
        public int OutStockNum { get; set; }
        public DateTime OutStockTime { get; set; }
        public Int64 CodeStarSerialNo { get; set; }
        public Int64 CodeEndSerialNo { get; set; }
    }
}
