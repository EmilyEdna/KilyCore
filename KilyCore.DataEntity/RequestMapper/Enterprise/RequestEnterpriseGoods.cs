using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestEnterpriseGoods
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/13 15:21:46
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseGoods
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }
        public Guid ProductSeriesId { get; set; }
        public string ExpiredDate { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        public AuditEnum AuditType { get; set; }
        public string Image { get; set; }
        public string Remark { get; set; }
        /// <summary>
        /// 条码编号
        /// </summary>
        public string LineCode { get; set; }
        /// <summary>
        /// 销售网址
        /// </summary>
        public string SellWebNet { get; set; }
        /// <summary>
        /// 批发价
        /// </summary>
        public string BatchPrice { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public string Price { get; set; }
    }
}
