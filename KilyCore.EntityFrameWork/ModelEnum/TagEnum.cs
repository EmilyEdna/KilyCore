﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KilyCore.EntityFrameWork.ModelEnum
{
    /// <summary>
    /// 二维码类型
    /// </summary>
    public enum TagEnum
    {
        /// <summary>
        /// 一物一码
        /// </summary>
        [Description("一物一码")]
        OneThing=10,
        /// <summary>
        /// 一品一码
        /// </summary>
        [Description("一品一码")]
        OneBrand =20,
        /// <summary>
        /// 一企一码
        /// </summary>
        [Description("一企一码")]
        OneEnterprise =30
    }
}
