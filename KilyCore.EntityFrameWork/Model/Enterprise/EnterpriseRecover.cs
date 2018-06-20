using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseRecover
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/20 10:14:22
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 产品召回表
    /// </summary>
    public class EnterpriseRecover : EnterpriseBase
    {
        /// <summary>
        /// 召回截至时间
        /// </summary>
        public virtual DateTime RecoverEndTime { get; set; }
        /// <summary>
        /// 召回的产品名称
        /// </summary>
        public virtual string RecoverGoodsName { get; set; }
        /// <summary>
        /// 召回原因
        /// </summary>
        public virtual string RecoverReason { get; set; }
        /// <summary>
        /// 处理方式
        /// </summary>
        public virtual string HandleWays { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public virtual DateTime? HandleTime { get; set; }
        /// <summary>
        /// 召回开始时间
        /// </summary>
        public virtual DateTime RecoverStarTime { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>
        public virtual string HandleUser { get; set; }
        /// <summary>
        /// 召回数量
        /// </summary>
        public virtual string RecoverNum { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        public virtual string States { get; set; }
    }
}
