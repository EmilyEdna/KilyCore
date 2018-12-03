using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestEnterpriseStockType
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Enterprise
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/18 10:50:49
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseStockType
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 企业Id
        /// </summary>
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string StockNo { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string StockName { get; set; }
        /// <summary>
        /// 贮藏方式
        /// </summary>
        public string SaveType { get; set; }
        /// <summary>
        /// 贮藏温度
        /// </summary>
        public string SaveTemp { get; set; }
        /// <summary>
        /// 贮藏湿度
        /// </summary>
        public string SaveH2 { get; set; }
        public string StockType { get; set; }
    }
}
