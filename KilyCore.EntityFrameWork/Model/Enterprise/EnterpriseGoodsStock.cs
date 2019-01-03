using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseGoodsStock
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/13 10:50:58
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 产品仓库表
    /// </summary>
    public class EnterpriseGoodsStock:EnterpriseBase
    {
        /// <summary>
        /// 入库批次号
        /// </summary>
        public virtual string GoodsBatchNo { get; set; }
        /// <summary>
        /// 产品表Id
        /// </summary>
        public virtual Guid GoodsId { get; set; }
        /// <summary>
        /// 仓库类型
        /// </summary>
        public virtual string StockType { get; set; }
        /// <summary>
        /// 入库数量
        /// </summary>
        public virtual int InStockNum { get; set; }
        /// <summary>
        /// 生产批次表Id--生产企业
        /// </summary>
        public virtual Guid? BatchId { get; set; }
        /// <summary>
        /// 成长日记表Id--种养殖企业
        /// </summary>
        public virtual Guid? GrowNoteId { get; set; }
        /// <summary>
        /// 进货表Id--流通企业
        /// </summary>
        public virtual Guid? BuyId { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public virtual string Manager { get; set; }
        /// <summary>
        /// 入库时间
        /// </summary>
        public virtual DateTime ProductTime { get; set; }
        /// <summary>
        /// 产品质检表Id
        /// </summary>
        public virtual Guid CheckGoodsId { get; set; }
        /// <summary>
        /// 产品图片
        /// </summary>
        public virtual string ImgUrl { get; set; }
        /// <summary>
        /// 说明书
        /// </summary>
        public virtual string Explanation { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 仓库类型Id
        /// </summary>
        public virtual Guid? StockTypeId { get; set; }
        /// <summary>
        /// 是否绑定箱码
        /// </summary>
        public virtual bool IsBindBoxCode { get; set; }
    }
}
