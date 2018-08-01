﻿using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseRepastContinued
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Repast
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/8/1 11:24:07
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Repast
{
    public class ResponseRepastContinued
    {
        public Guid Id { get; set; }
        public Guid InfoId { get; set; }
        /// <summary>
        /// 续费年限
        /// </summary>
        public string ContinuedYear { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public string PayTypeName { get; set; }
        /// <summary>
        /// 是否付款
        /// </summary>
        public bool? IsPay { get; set; }
        public string Payment { get => (IsPay.HasValue) ? ((bool)IsPay ? "已付款" : "未付款") : "/"; }
        /// <summary>
        /// 票据
        /// </summary>
        public string PayTicket { get; set; }
        public string AuditTypeName { get; set; }
    }
    public class ResponseRepastUpLevel
    {
        public Guid Id { get; set; }
        public Guid InfoId { get; set; }
        /// <summary>
        /// 版本类型
        /// </summary>
        public string VersionTypeName { get; set; }
        /// <summary>
        /// 续费年限
        /// </summary>
        public string ContinuedYear { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public string PayTypeName { get; set; }
        /// <summary>
        /// 票据
        /// </summary>
        public string PayTicket { get; set; }
        /// <summary>
        /// 是否付款
        /// </summary>
        public bool? IsPay { get; set; }
        public string Payment { get => (IsPay.HasValue) ? ((bool)IsPay ? "已付款" : "未付款") : "/"; }
        public string AuditTypeName { get; set; }
    }
}
