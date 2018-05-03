using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 养殖企业认证附加表
    /// </summary>
    public class EnterpriseCultureIdentAttach : BaseEntity
    {
        /// <summary>
        /// 企业认证表主键
        /// </summary>
        public virtual Guid IdentId { get; set; }
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
        /// 合格证
        /// </summary>
        public virtual string ImgQualified { get; set; }
        /// <summary>
        /// 水质检测
        /// </summary>
        public virtual string ImgWater { get; set; }
        /// <summary>
        /// 土壤检测
        /// </summary>
        public virtual string ImgSoil { get; set; }
        /// <summary>
        /// 金属检测
        /// </summary>
        public virtual string ImgMetal { get; set; }
        /// <summary>
        /// 其他检测
        /// </summary>
        public virtual string ImgOther { get; set; }
    }
}
