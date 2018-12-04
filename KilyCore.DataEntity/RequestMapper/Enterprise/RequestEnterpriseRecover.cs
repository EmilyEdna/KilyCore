using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestEnterpriseRecover
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/20 10:36:16
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseRecover
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 召回截至时间
        /// </summary>
        public DateTime RecoverEndTime { get; set; }
        /// <summary>
        /// 召回的产品名称
        /// </summary>
        public string RecoverGoodsName { get; set; }
        /// <summary>
        /// 召回原因
        /// </summary>
        public string RecoverReason { get; set; }
        /// <summary>
        /// 处理方式
        /// </summary>
        public string HandleWays { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? HandleTime { get; set; }
        /// <summary>
        /// 召回开始时间
        /// </summary>
        public DateTime RecoverStarTime { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>
        public string HandleUser { get; set; }
        /// <summary>
        /// 召回数量
        /// </summary>
        public string RecoverNum { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        public string States { get; set; }
        /// <summary>
        /// 手机验证码
        /// </summary>
        public string Code { get; set; }
    }
}
