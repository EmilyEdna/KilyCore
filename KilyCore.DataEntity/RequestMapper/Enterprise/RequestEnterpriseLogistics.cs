﻿using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestEnterpriseLogistics
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/22 15:41:52
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseLogistics
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 物流单号
        /// </summary>
        public string WayBill { get; set; }
        /// <summary>
        /// 包装编号
        /// </summary>
        public string PackageNo { get; set; }
        /// <summary>
        /// 发货数量
        /// </summary>
        public  string SendGoodsNum { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime? SendTime { get; set; }
        /// <summary>
        /// 收货人公司Id
        /// </summary>
        public Guid GainId { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string GainUser { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string LinkPhone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 是否收货
        /// </summary>
        public bool Flag { get; set; }
        public bool SendType { get; set; }
    }
}