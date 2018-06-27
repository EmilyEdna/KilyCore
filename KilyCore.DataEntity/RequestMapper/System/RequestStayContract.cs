using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.System
{
    public class RequestStayContract
    {
        public  Guid Id { get; set; }
        /// <summary>
        /// 运营商Id
        /// </summary>
        public  Guid? AdminId { get; set; }
        /// <summary>
        /// 入住企业的Id
        /// </summary>
        public  Guid CompanyId { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public  string TypePath { get; set; }
        /// <summary>
        /// 入住企业名称
        /// </summary>
        public  string CompanyName { get; set; }
        /// <summary>
        /// 缴费票据
        /// </summary>
        public  string PayTicket { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public  PayEnum PayType { get; set; }
        /// <summary>
        /// 审核类型
        /// </summary>
        public  AuditEnum AuditType { get; set; }
        /// <summary>
        /// 合同年限
        /// </summary>
        public  string ContractYear { get; set; }
        /// <summary>
        /// 合同模式 1表示线上合同，2表示线下合同
        /// </summary>
        public  int ContractType { get; set; }
        /// <summary>
        /// 是否缴费
        /// </summary>
        public  bool? IsPay { get; set; }
        /// <summary>
        /// 试用
        /// </summary>
        public  string TryOut { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public  SystemVersionEnum VersionType { get; set; }
    }
}
