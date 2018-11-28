using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：GovtRisk
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/19 11:22:04
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Govt
{
    /// <summary>
    /// 风险预警表
    /// </summary>
    public class GovtRisk: GovtBase
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public virtual string EventName { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public virtual string TypePath { get; set; }
        /// <summary>
        /// 危险等级
        /// </summary>
        public virtual string WaringLv { get; set; }
        /// <summary>
        /// 行业类型
        /// </summary>
        public virtual string TradeType { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public virtual DateTime? ReleaseTime { get; set; }
        /// <summary>
        /// 是否广播
        /// </summary>
        public virtual bool ReportPlay { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
