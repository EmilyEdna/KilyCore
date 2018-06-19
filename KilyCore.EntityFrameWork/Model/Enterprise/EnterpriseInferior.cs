using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseInferior
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/19 16:17:12
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 不合格过期处理表
    /// </summary>
    public class EnterpriseInferiorExprired:EnterpriseBase
    {
        /// <summary>
        /// 1：表示不合格处理类型。2：表示过期处理类型。
        /// </summary>
        public virtual int InferiorExprired { get; set; }
        /// <summary>
        /// 被处理的物品名称
        /// </summary>
        public virtual string InferName { get; set; }
        /// <summary>
        /// 被处理的物品Id
        /// </summary>
        public virtual Guid InferId { get; set; }
        /// <summary>
        /// 被处理的物品类型
        /// </summary>
        public virtual string InferType { get; set; }
        /// <summary>
        /// 处理自定义名称
        /// </summary>
        public virtual string CustomName { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>
        public virtual string HandleUser { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public virtual DateTime HandleTime { get; set; }
        /// <summary>
        /// 处理原因
        /// </summary>
        public virtual string HandleReason { get; set; }
        /// <summary>
        /// 处理方式
        /// </summary>
        public virtual string HandleWays { get; set; }
    }
}
