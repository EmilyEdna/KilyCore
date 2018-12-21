using KilyCore.EntityFrameWork.ModelEnum;
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
        public DateTime ProductTime { get; set; }
        public Guid? StockTypeId { get; set; }
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
        public string AuditTypeName { get; set; }
        public AuditEnum AuditType { get; set; }
        /// <summary>
        /// 保质期
        /// </summary>
        public string ExpiredDate { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Spec { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
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
        /// 是否绑定箱码
        /// </summary>
        public bool IsBindBoxCode { get; set; }
        public string StockMsg { get => InStockNum == 0 ? "已全部出库" : "有剩余"; }
    }
    public class ResponseEnterpriseGoodsStockAttach
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string GoodsName { get; set; }
        public string OutStockType { get; set; }
        public Int64 CodeStarSerialNo { get; set; }
        public Int64 CodeEndSerialNo { get; set; }
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
        public string CodeStarSerialNos { get; set; }
        public string CodeEndSerialNos { get; set; }
    }
}
