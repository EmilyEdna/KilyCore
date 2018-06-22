using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseLogistics
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/22 15:06:34
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    public class EnterpriseLogistics:EnterpriseBase
    {
        /// <summary>
        /// 物流单号
        /// </summary>
        public virtual string WayBill { get; set; }
        /// <summary>
        /// 包装编号
        /// </summary>
        public virtual string PackageNo { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string GoodsName { get; set; }
        /// <summary>
        /// 发货时间
        /// </summary>
        public virtual DateTime? SendTime { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public virtual string Manager { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual string LinkPhone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 是否收货
        /// </summary>
        public virtual bool Flag { get; set; }
    }
}
