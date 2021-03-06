﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.ModelEnum
{
    /// <summary>
    /// 企业类型
    /// </summary>
    public enum CompanyEnum
    {
        /// <summary>
        /// 种植企业
        /// </summary>
        [Description("种植企业")]
        Plant = 10,
        /// <summary>
        /// 养殖企业
        /// </summary>
        [Description("养殖企业")]
        Culture =20,
        /// <summary>
        /// 生产企业
        /// </summary>
        [Description("生产企业")]
        Production = 30,
        /// <summary>
        /// 流通企业
        /// </summary>
        [Description("流通企业")]
        Circulation = 40,
        /// <summary>
        /// 其他企业
        /// </summary>
        [Description("其他企业")]
        Other=50
    }
}
