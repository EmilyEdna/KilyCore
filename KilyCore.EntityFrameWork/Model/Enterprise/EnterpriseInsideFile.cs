﻿using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseInsideFile
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/4 14:08:09
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 内部文件表
    /// </summary>
    public class EnterpriseInsideFile : EnterpriseBase
    {
        /// <summary>
        /// 文件标题
        /// </summary>
        public virtual string FileTitle { get; set; }

        /// <summary>
        /// 文件内容
        /// </summary>
        public virtual string FileContent { get; set; }
    }
}