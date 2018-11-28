using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseEnterpriseInferiorExprired
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/19 16:42:03
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseInferiorExprired
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 不合格数量
        /// </summary>
        public string InferNum { get; set; }
        /// <summary>
        /// 1：表示不合格处理类型。2：表示过期处理类型。
        /// </summary>
        public int InferiorExprired { get; set; }
        /// <summary>
        /// 被处理的物品名称
        /// </summary>
        public string InferName { get; set; }
        /// <summary>
        /// 被处理的物品Id
        /// </summary>
        public Guid InferId { get; set; }
        /// <summary>
        /// 被处理的物品类型
        /// </summary>
        public string InferType { get; set; }
        /// <summary>
        /// 处理自定义名称
        /// </summary>
        public string CustomName { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>
        public string HandleUser { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime HandleTime { get; set; }
        /// <summary>
        /// 处理原因
        /// </summary>
        public string HandleReason { get; set; }
        /// <summary>
        /// 处理方式
        /// </summary>
        public string HandleWays { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string BatchNo { get; set; }
    }
}
