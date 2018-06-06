using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseMaterialStock
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/6 17:22:32
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 原料仓库表
    /// </summary>
    public class EnterpriseMaterialStock : EnterpriseBase
    {
        /// <summary>
        /// 入库批次号
        /// </summary>
        public virtual string SerializNo { get; set; }
        /// <summary>
        /// 原料批次号
        /// </summary>
        public virtual string BacthNo { get; set; }
        /// <summary>
        /// 入库数量
        /// </summary>
        public virtual string SetStockNum { get; set; }
        public virtual DateTime? SetStockTime { get; set; }
    }
}
