using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastStuff
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/7 10:08:20
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Repast
{
    /// <summary>
    /// 原料溯源表
    /// </summary>
    public class RepastStuff : RepastBase
    {
        /// <summary>
        /// 原料名称
        /// </summary>
        public virtual string MaterialName { get; set; }
        /// <summary>
        /// 原料类型
        /// </summary>
        public virtual string MaterialType { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public virtual string Supplier { get; set; }
        /// <summary>
        /// 供应时间
        /// </summary>
        public virtual DateTime? SuppTime { get; set; }
        /// <summary>
        /// 保质期
        /// </summary>
        public virtual string ExpiredDay { get; set; }
        /// <summary>
        /// 质检报告
        /// </summary>
        public virtual string QualityReport { get; set; }
        /// <summary>
        /// 资质证书
        /// </summary>
        public virtual string Aptitude { get; set; }
        /// <summary>
        /// 执行标准
        /// </summary>
        public virtual string Standard { get; set; }
        /// <summary>
        /// 生产地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 溯源连接
        /// </summary>
        public virtual string SourceLink { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 采购负责人
        /// </summary>
        public virtual string BuyUser { get; set; }
    }
}
