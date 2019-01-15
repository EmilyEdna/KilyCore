using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 入住合同
    /// </summary>
    public class SystemStayContract : BaseEntity
    {
        /// <summary>
        /// 运营商Id
        /// </summary>
        public virtual Guid? AdminId { get; set; }
        /// <summary>
        /// 入住企业或商家的Id
        /// </summary>
        public virtual Guid CompanyId { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public virtual string TypePath { get; set; }
        /// <summary>
        /// 入住企业名称
        /// </summary>
        public virtual string CompanyName { get; set; }
        /// <summary>
        /// 缴费票据
        /// </summary>
        public virtual string PayTicket { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public virtual PayEnum PayType { get; set; }
        /// <summary>
        /// 审核类型
        /// </summary>
        public virtual AuditEnum AuditType { get; set; }
        /// <summary>
        /// 合同年限
        /// </summary>
        public virtual string ContractYear { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public virtual DateTime EndTime { get; set; }
        /// <summary>
        /// 合同模式 1表示线上合同，2表示线下合同
        /// </summary>
        public virtual int ContractType { get; set; }
        /// <summary>
        /// 是否缴费
        /// </summary>
        public virtual bool? IsPay { get; set; }
        /// <summary>
        /// 试用
        /// </summary>
        public virtual string TryOut { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public virtual SystemVersionEnum VersionType { get; set; }
        /// <summary>
        /// 企业或商家 1=》企业；2=》商家
        /// </summary>
        public virtual int EnterpriseOrMerchant { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public virtual decimal? TotalPrice { get; set; }
        /// <summary>
        /// 实际金额
        /// </summary>
        public virtual decimal? ActualPrice { get; set; }
        /// <summary>
        /// 试用开始日期
        /// </summary>
        public virtual DateTime? TryStarDate { get; set; }
        /// <summary>
        /// 试用结束日期
        /// </summary>
        public virtual DateTime? TryEndDate { get; set; }
    }
}
