using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.Model.Repast
{
    /// <summary>
    /// 餐饮认证表
    /// </summary>
    public class RepastIdent: RepastBase
    {
        /// <summary>
        /// 认证编号
        /// </summary>
        public virtual string IdentNo { get; set; }
        /// <summary>
        /// 商家名称
        /// </summary>
        public virtual string MerchantName { get; set; }
        /// <summary>
        /// 认证星级
        /// </summary>
        public virtual IdentEnum IdentStar { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public virtual AuditEnum AuditType { get; set; }
        /// <summary>
        /// 商家类型
        /// </summary>
        public virtual MerchantEnum DiningType { get; set; }
        /// <summary>
        /// 认证年限
        /// </summary>
        public virtual int IdentYear { get; set; }
        /// <summary>
        /// 认证开始时间
        /// </summary>
        public virtual DateTime IdentStartTime { get; set; }
        /// <summary>
        /// 认证截至时间
        /// </summary>
        public virtual DateTime IdentEndTime { get; set; }
        /// <summary>
        /// 信用代码
        /// </summary>
        public virtual string CommunityCode { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        public virtual string Representative { get; set; }
        /// <summary>
        /// 法人身份证
        /// </summary>
        public virtual string RepresentativeCard { get; set; }
        /// <summary>
        /// 报送人
        /// </summary>
        public virtual string SendPerson { get; set; }
        /// <summary>
        /// 报送人身份证
        /// </summary>
        public virtual string SendCard { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public virtual string LinkPhone { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 法人身份证
        /// </summary>
        public virtual string ImgCard { get; set; }
        /// <summary>
        /// 申请表
        /// </summary>
        public virtual string ImgApply { get; set; }
        /// <summary>
        /// 调查表
        /// </summary>
        public virtual string ImgResearch { get; set; }
        /// <summary>
        /// 认证协议
        /// </summary>
        public virtual string ImgAgreement { get; set; }
        /// <summary>
        /// 其他证书
        /// </summary>
        public virtual string ImgOther { get; set; }
    }
}
