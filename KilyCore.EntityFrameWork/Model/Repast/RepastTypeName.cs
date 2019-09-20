using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastTypeName
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/10 16:22:20
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Repast
{
    /// <summary>
    /// 原料物品名称管理表
    /// </summary>
    public class RepastTypeName: RepastBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string TypeNames { get; set; }
        /// <summary>
        /// 1代表原料2代表物品3代表产品
        /// </summary>
        public virtual int Types { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public virtual string Spec { get; set; }
        /// <summary>
        /// 产品图片
        /// </summary>
        public virtual string ProImg { get; set; }
    }
}
