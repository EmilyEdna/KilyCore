using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：GoodsPackage
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/22 14:46:37
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 装车管理表
    /// </summary>
    public class EnterpriseGoodsPackage : EnterpriseBase
    {
        /// <summary>
        /// 包装编号
        /// </summary>
        public virtual string PackageNo { get; set; }
        /// <summary>
        /// 产品出库表编号
        /// </summary>
        public virtual string ProductOutStockNo { get; set; }
        /// <summary>
        /// 打包时间
        /// </summary>
        public virtual DateTime? PackageTime { get; set; }
        /// <summary>
        /// 打包数量
        /// </summary>
        public virtual int PackageNum { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public virtual string Manager { get; set; }
        /// <summary>
        /// 箱码
        /// </summary>
        public virtual string BoxCode { get; set; }
    }
}
