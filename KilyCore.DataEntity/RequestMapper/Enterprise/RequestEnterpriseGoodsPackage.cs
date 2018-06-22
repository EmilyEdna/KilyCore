using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestEnterpriseGoodsPackage
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/22 15:41:30
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseGoodsPackage
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 包装编号
        /// </summary>
        public string PackageNo { get; set; }
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
        /// 二维码开始号段
        /// </summary>
        public Int64 CodeStarSerialNo { get; set; }
        /// <summary>
        /// 二维码结束号段
        /// </summary>
        public Int64 CodeEndSerialNo { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string Manager { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
    }
}
