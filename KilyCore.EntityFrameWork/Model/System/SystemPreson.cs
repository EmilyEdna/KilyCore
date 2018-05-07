using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 人员信息表
    /// </summary>
    public class SystemPreson : BaseEntity
    {

        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string TrueName { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public virtual string WorkNum { get; set; }
        /// <summary>
        /// 所属种类
        /// </summary>
        public virtual string Type { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string LinkPhone { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public virtual string IdCard { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public virtual string HeadImage { get; set; }
    }
}
