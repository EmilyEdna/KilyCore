﻿using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseEnterpriseGoods
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/13 15:22:43
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseGoods
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? ProductSeriesId { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }
        /// <summary>
        /// 批发价
        /// </summary>
        public string BatchPrice { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 条码编号
        /// </summary>
        public virtual string LineCode { get; set; }
        /// <summary>
        /// 销售网址
        /// </summary>
        public virtual string SellWebNet { get; set; }
        /// <summary>
        /// 产品图片
        /// </summary>
        public string ProductImg { set; get; }
        public string ProductSeriesName { get; set; }
        public string ExpiredDate { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Specs { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string CompanyName { set; get; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        public string Remark { get; set; }
    }
}
