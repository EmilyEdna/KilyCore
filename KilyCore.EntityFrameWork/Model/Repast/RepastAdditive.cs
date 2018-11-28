using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastAdditive
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/10/10 12:26:27
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Repast
{
    /// <summary>
    /// 添加剂表
    /// </summary>
    public class RepastAdditive : RepastBase
    {
        /// <summary>
        /// 添加剂名称
        /// </summary>
        public virtual string AdditiveName { get; set; }
        /// <summary>
        /// 生产商
        /// </summary>
        public virtual string SupplierName { get; set; }
        /// <summary>
        /// 使用时间
        /// </summary>
        public virtual DateTime? UseTime { get; set; }
        /// <summary>
        /// 生产时间
        /// </summary>
        public virtual DateTime? SupplierTime { get; set; }
        /// <summary>
        /// 使用计量
        /// </summary>
        public virtual string Metering { get; set; }
        /// <summary>
        /// 使用人
        /// </summary>
        public virtual string UsePerson { get; set; }
        /// <summary>
        /// 制作的食品
        /// </summary>
        public virtual string ProFood { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public virtual string Function { get; set; }
    }
}
