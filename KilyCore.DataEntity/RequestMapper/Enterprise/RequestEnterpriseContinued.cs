using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestEnterpriseContinued
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/3 15:26:28
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseContinued
    {
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 续费年限
        /// </summary>
        public string ContinuedYear { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public PayEnum PayType { get; set; }
        /// <summary>
        /// 是否付款
        /// </summary>
        public bool? IsPay { get; set; }
        /// <summary>
        /// 票据
        /// </summary>
        public string PayTicket { get; set; }
    }
    public class RequestEnterpriseUpLevel
    {
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 版本类型
        /// </summary>
        public SystemVersionEnum VersionType { get; set; }
        /// <summary>
        /// 续费年限
        /// </summary>
        public string ContinuedYear { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public PayEnum PayType { get; set; }
        /// <summary>
        /// 票据
        /// </summary>
        public string PayTicket { get; set; }
        /// <summary>
        /// 是否付款
        /// </summary>
        public bool? IsPay { get; set; }
    }
}
