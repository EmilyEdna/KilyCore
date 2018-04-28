using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 流通企业认证附加表
    /// </summary>
    public class EnterpriseCirculationIdentAttach:BaseEntity
    {
        /// <summary>
        /// 企业认证表主键
        /// </summary>
        public Guid IdentId { get; set; }
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
        /// 药品GSP检测
        /// </summary>
        public virtual string ImgDrugs { get; set; }
        /// <summary>
        /// 流通检测
        /// </summary>
        public virtual string ImgCirculation { get; set; }
        /// <summary>
        /// 卫生检测
        /// </summary>
        public virtual string ImgHygiene { get; set; }
        /// <summary>
        /// 进出口许可证
        /// </summary>
        public virtual string ImgImportExport { get; set; }
        /// <summary>
        /// 其他认证
        /// </summary>
        public virtual string ImgOther { get; set; }
    }
}
