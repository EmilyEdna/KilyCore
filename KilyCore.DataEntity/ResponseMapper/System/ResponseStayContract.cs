using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.DataEntity.ResponseMapper.System
{
    public class ResponseStayContract
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 运营商Id
        /// </summary>
        public Guid? AdminId { get; set; }
        /// <summary>
        /// 入住企业的Id
        /// </summary>
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public string TypePath { get; set; }
        /// <summary>
        /// 入住企业名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 缴费票据
        /// </summary>
        public string PayTicket { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public PayEnum PayType { get; set; }
        public string PayTypeName { get; set; }
        /// <summary>
        /// 审核类型
        /// </summary>
        public AuditEnum AuditType { get; set; }
        /// <summary>
        /// 合同年限
        /// </summary>
        public string ContractYear { get; set; }
        /// <summary>
        /// 合同模式 1表示线上合同，2表示线下合同
        /// </summary>
        public int ContractType { get; set; }
        public string ContractTypeName { get => ContractType == 1 ? "线上合同" : "线下合同"; }
        /// <summary>
        /// 是否缴费
        /// </summary>
        public bool? IsPay { get; set; }
        /// <summary>
        /// 缴费金额
        /// </summary>
        public decimal? TotalPrice { get; set; }
        public string Payment { get => IsPay != null ? ((bool)IsPay ? "已缴费" : "未缴费") : "未缴费"; }
        /// <summary>
        /// 试用
        /// </summary>
        public string TryOut { get; set; }
        /// <summary>
        /// 审核
        /// </summary>
        public string AuditTypeName { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public SystemVersionEnum VersionType { get; set; }
        public string VersionTypeName { get; set; }
        /// <summary>
        /// 入住时间
        /// </summary>
        public DateTime? StayTime { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime EndTime { get; set; }
        public string TableName { get; set; }
        public decimal? ActualPrice { get; set; }
        public Object Record { get; set; }
        public string Lv
        {
            get
            {
                if (Record != null)
                    return "有升级";
                else
                    return "无";
            }
        }
    }
}
