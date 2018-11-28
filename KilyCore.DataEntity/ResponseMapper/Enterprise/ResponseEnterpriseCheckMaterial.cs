﻿using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseEnterpriseCheckMaterial
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/19 15:36:55
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseCheckMaterial
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 检测自定义名称
        /// </summary>
        public string CheckName { get; set; }
        /// <summary>
        /// 原料名称
        /// </summary>
        public string MaterName { get; set; }
        /// <summary>
        /// 质检单位
        /// </summary>
        public string CheckUint { get; set; }
        /// <summary>
        /// 质检人员
        /// </summary>
        public string CheckUser { get; set; }
        /// <summary>
        /// 质检结果
        /// </summary>
        public string CheckResult { get; set; }
        /// <summary>
        /// 质检报告
        /// </summary>
        public string CheckReport { get; set; }
        public string CheckReprotName{get=>string.IsNullOrEmpty(CheckReport) ? "未上传" : "已上传";}
    }
}
