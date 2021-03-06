﻿using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseEnterpriseFacilities
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/9 15:51:36
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseFacilities
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 车间名称
        /// </summary>
        public string WorkShopName { get; set; }
        /// <summary>
        /// 供水
        /// </summary>
        public string GetWater { get; set; }
        /// <summary>
        /// 排水
        /// </summary>
        public string WaterOut { get; set; }
        /// <summary>
        /// 光照
        /// </summary>
        public string Light { get; set; }
        /// <summary>
        /// 通风
        /// </summary>
        public string Wind { get; set; }
        /// <summary>
        /// 环境控制
        /// </summary>
        public string Environment { get; set; }
        /// <summary>
        /// 废物处理
        /// </summary>
        public string Waste { get; set; }
    }
    public class ResponseEnterpriseFacilitiesAttach
    {
        public Guid Id { get; set; }
        public string DisinfectionName { get; set; }
        public DateTime? CleanTime { get; set; }
        public string HandlerUser { get; set; }
    }
}
