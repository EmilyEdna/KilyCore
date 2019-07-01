using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseCheckGoods
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/19 15:27:43
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 产品质检表
    /// </summary>
    public class EnterpriseCheckGoods:EnterpriseBase
    {
        /// <summary>
        /// 检测自定义名称
        /// </summary>
        public virtual string CheckName { get; set; }
        /// <summary>
        /// 产品表Id
        /// </summary>
        public virtual Guid? GoodsId { get; set; }
        /// <summary>
        /// 进货表Id
        /// </summary>
        public virtual Guid? BuyerId { get; set; }
        /// <summary>
        /// 质检单位
        /// </summary>
        public virtual string CheckUint { get; set; }
        /// <summary>
        /// 质检人员
        /// </summary>
        public virtual string CheckUser { get; set; }
        /// <summary>
        /// 质检结果
        /// </summary>
        public virtual string CheckResult { get; set; }
        /// <summary>
        /// 质检报告
        /// </summary>
        public virtual string CheckReport { get; set; }
    }
}
