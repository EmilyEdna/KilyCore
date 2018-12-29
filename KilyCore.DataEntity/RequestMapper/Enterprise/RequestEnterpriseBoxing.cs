using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：KilyCore.DataEntity.RequestMapper.Enterprise
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Enterprise
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/12/21 15:37:23
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseBoxing
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public  string GoodName { get; set; }
        /// <summary>
        /// 出入库批次
        /// </summary>
        public  string StockBatchNo { get; set; }
        /// <summary>
        /// 生产批次
        /// </summary>
        public  string ProductionBatchNo { get; set; }
        /// <summary>
        /// 装箱批次
        /// </summary>
        public  string BoxBatchNo { get; set; }
        /// <summary>
        /// 箱码
        /// </summary>
        public  string BoxCode { get; set; }
        /// <summary>
        /// 装箱数量
        /// </summary>
        public  string BoxCount { get; set; }
        /// <summary>
        /// 物品码
        /// </summary>
        public  string ThingCode { get; set; }
        /// <summary>
        /// 装箱时间
        /// </summary>
        public  DateTime? BoxTime { get; set; }
        /// <summary>
        /// 箱码短码
        /// </summary>
        public Int64 BoxCodeSort { get; set; }
        public String StarCode { get; set; }
        public String EndCode { get; set; }
    }
}
