using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.System
{
    public class RequestPreson
    {
        public Guid Id { get; set; }
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
        /// <summary>
        /// 服务年限
        /// </summary>
        public virtual string ServiceYear { get; set; }
        /// <summary>
        /// 银行信息
        /// </summary>
        public virtual string BankInfo { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? STime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime? ETime { get; set; }
        /// <summary>
        /// 服务区域
        /// </summary>
        public virtual string ServciePath { get; set; }
    }
}
