using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.Repast
{
    public class ResponseRepastOrg
    {
        public Guid Id { get; set; }
        public Guid? InfoId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public  string TrueName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public  string Address { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public  string Worker { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public  string IdCardNo { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public  string LinkPhone { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public  string IdCard { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public  string IsWork { get; set; }
    }
}
