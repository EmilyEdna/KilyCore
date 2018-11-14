using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/11/14 11:10:55
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 扫描记录表
    /// </summary>
    public class EnterpriseScanCodeInfo : EnterpriseBase
    {
        /// <summary>
        /// 标签号段
        /// </summary>
        public virtual string ScanCode { get; set; }
        /// <summary>
        /// 扫描地址
        /// </summary>
        public virtual string ScanAddress { get; set; }
        /// <summary>
        /// 打包批次
        /// </summary>
        public virtual string ScanPackageNo { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public virtual string ScanGoodsName { get; set; }
        /// <summary>
        /// Ip地址
        /// </summary>
        public virtual string ScanIP { get; set; }
        /// <summary>
        /// 扫描次数
        /// </summary>
        public virtual int ScanNum { get; set; }
    }
}
