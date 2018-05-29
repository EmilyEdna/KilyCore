using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.Model.Dining
{
    /// <summary>
    /// 商家认证审核附加表
    /// </summary>
    public class DiningIdentAttach:BaseEntity
    {
        /// <summary>
        /// 商家认证表主键
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
        /// 原料购买证明
        /// </summary>
        public virtual string ImgMaterialOrder { get; set; }
        /// <summary>
        /// 消毒证明
        /// </summary>
        public virtual string ImgDisinfection { get; set; }
        /// <summary>
        /// 原料储藏证明
        /// </summary>
        public virtual string ImgMaterialSave { get; set; }
        /// <summary>
        /// 废弃物处理证明
        /// </summary>
        public virtual string ImgAbandoned { get; set; }
        /// <summary>
        /// 留样证明
        /// </summary>
        public virtual string ImgSample { get; set; }
        /// <summary>
        /// 从业证明
        /// </summary>
        public virtual string ImgWorkingPerson { get; set; }
        /// <summary>
        /// 其他证明
        /// </summary>
        public virtual string ImgOther { get; set; }
    }
}
