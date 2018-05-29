﻿using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.DataEntity.RequestMapper.Dining
{
   public class RequestDiningIdent
    {
        /// <summary>
        /// 认证编号
        /// </summary>
        public string IdentNo { get; set; }
        public Guid Id { get; set; }
        /// <summary>
        /// 商家名称
        /// </summary>
        public string MerchantName { get; set; }
        /// <summary>
        /// 认证星级
        /// </summary>
        public IdentEnum IdentStar { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public AuditEnum AuditType { get; set; }
        /// <summary>
        /// 商家类型
        /// </summary>
        public MerchantEnum? DiningType { get; set; }
        /// <summary>
        /// 认证年限
        /// </summary>
        public int IdentYear { get; set; }
        /// <summary>
        /// 信用代码
        /// </summary>
        public string CommunityCode { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        public string Representative { get; set; }
        /// <summary>
        /// 法人身份证
        /// </summary>
        public string RepresentativeCard { get; set; }
        /// <summary>
        /// 报送人
        /// </summary>
        public string SendPerson { get; set; }
        /// <summary>
        /// 报送人身份证
        /// </summary>
        public string SendCard { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string LinkPhone { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 区域树查询
        /// </summary>
        public string AreaTree { get; set; }
    }
}
