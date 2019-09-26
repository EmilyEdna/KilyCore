using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Repast
{
    /// <summary>
    /// 委员会
    /// </summary>
    public class RepastOrg: RepastBase
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string TrueName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public virtual string Worker { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public virtual string IdCardNo { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string LinkPhone { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public virtual string IdCard { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public virtual string IsWork { get; set; }
    }
}
