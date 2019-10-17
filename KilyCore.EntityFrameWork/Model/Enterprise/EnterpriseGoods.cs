using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseGoods
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/13 10:08:50
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 企业产品表
    /// </summary>
    public class EnterpriseGoods:EnterpriseBase
    {
        /// <summary>
        /// 产品类型
        /// </summary>
        public virtual string ProductType { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string ProductName { get; set; }
        /// <summary>
        /// 产品系列
        /// </summary>
        public virtual Guid? ProductSeriesId { get; set; }
        /// <summary>
        /// 保质期
        /// </summary>
        public virtual string ExpiredDate { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public virtual string Spec { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public virtual string Unit { get; set; }
        /// <summary>
        /// 审核类型
        /// </summary>
        public virtual AuditEnum AuditType { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public virtual string Image { get; set; }
        /// <summary>
        /// 条码编号
        /// </summary>
        public virtual string LineCode { get; set; }
        /// <summary>
        /// 销售网址
        /// </summary>
        public virtual string SellWebNet { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 批发价
        /// </summary>
        public virtual string BatchPrice { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public virtual string Price { get; set; }
    }
}
