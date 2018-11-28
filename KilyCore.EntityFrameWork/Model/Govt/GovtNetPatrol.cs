using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：GovtPatrol
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/21 15:17:33
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Govt
{
    /// <summary>
    /// 网上巡查表
    /// </summary>
    public class GovtNetPatrol : GovtBase
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public virtual Guid? CompanyId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public virtual string CompanyName { get; set; }
        /// <summary>
        /// 所属行业
        /// </summary>
        public virtual string TradeType { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public virtual string TypePath { get; set; }
        /// <summary>
        /// 抽查次数
        /// </summary>
        public virtual int PotrolNum { get; set; }
        /// <summary>
        /// 通报次数
        /// </summary>
        public virtual int BulletinNum { get; set; }
        /// <summary>
        /// 合格率
        /// </summary>
        public virtual string QualifiedNum { get; set; }
    }
}
