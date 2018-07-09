using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseFacilities
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/9 15:14:28
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 企业设施表
    /// </summary>
    public class EnterpriseFacilities:EnterpriseBase
    {
        /// <summary>
        /// 车间名称
        /// </summary>
        public virtual string WorkShopName { get; set; }
        /// <summary>
        /// 供水
        /// </summary>
        public virtual string GetWater { get; set; }
        /// <summary>
        /// 排水
        /// </summary>
        public virtual string WaterOut { get; set; }
        /// <summary>
        /// 光照
        /// </summary>
        public virtual string Light { get; set; }
        /// <summary>
        /// 通风
        /// </summary>
        public virtual string Wind { get; set; }
        /// <summary>
        /// 环境控制
        /// </summary>
        public virtual string Environment { get; set; }
        /// <summary>
        /// 废物处理
        /// </summary>
        public virtual string Waste { get; set; }
    }
    /// <summary>
    /// 企业设施附加表
    /// </summary>
    public class EnterpriseFacilitiesAttach : EnterpriseBase
    {
        /// <summary>
        /// 企业设施表Id
        /// </summary>
        public virtual Guid FacId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string DisinfectionName { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public virtual DateTime? CleanTime { get; set; } 
    }
}
