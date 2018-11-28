using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Enterprise
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/11/14 14:27:18
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseScanCodeInfo
    {
        public Guid Id { get; set; }
        public Guid? CompanyId { get; set; }
        /// <summary>
        /// 标签号段
        /// </summary>
        public string ScanCode { get; set; }
        /// <summary>
        /// 扫描地址
        /// </summary>
        public string ScanAddress { get; set; }
        /// <summary>
        /// 打包批次
        /// </summary>
        public string ScanPackageNo { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ScanGoodsName { get; set; }
        /// <summary>
        /// Ip地址
        /// </summary>
        public string ScanIP { get; set; }
        /// <summary>
        /// 扫描次数
        /// </summary>
        public int ScanNum { get; set; }
    }
}
