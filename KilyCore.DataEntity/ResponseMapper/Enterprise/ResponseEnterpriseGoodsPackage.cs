using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseEnterpriseGoodsPackage
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/22 15:48:24
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseGoodsPackage
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 包装编号
        /// </summary>
        public string PackageNo { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 产品出库表编号
        /// </summary>
        public string ProductOutStockNo { get; set; }
        /// <summary>
        /// 打包时间
        /// </summary>
        public DateTime? PackageTime { get; set; }
        /// <summary>
        /// 打包数量
        /// </summary>
        public int PackageNum { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string Manager { get; set; }
        /// <summary>
        /// 箱码
        /// </summary>
        public string BoxCode { get; set; }
        /// <summary>
        /// 是否发货
        /// </summary>
        public bool IsSend { get; set; }
    }
}
