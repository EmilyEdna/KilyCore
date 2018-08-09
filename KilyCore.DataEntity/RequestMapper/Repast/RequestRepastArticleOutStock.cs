using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestRepastArticleOutStock
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/9 11:56:16
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Repast
{
    public class RequestRepastArticleOutStock
    {
        public Guid Id { get; set; }
        public Guid InfoId { get; set; }
        /// <summary>
        /// 入库的Id
        /// </summary>
        public Guid InStockId { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }
        /// <summary>
        /// 食材名称
        /// </summary>
        public string ArticleName { get; set; }
        /// <summary>
        /// 出库数量
        /// </summary>
        public int OutStockNum { get; set; }
        /// <summary>
        /// 出库时间
        /// </summary>
        public DateTime? OutStockTime { get; set; }
        /// <summary>
        /// 出库负责人
        /// </summary>
        public string OutUser { get; set; }
    }
}
