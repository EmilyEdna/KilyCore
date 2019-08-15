using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 线下人员表
    /// </summary>
    public class SystemPersonOff:BaseEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string  UserNo { get; set; }
        /// <summary>
        /// 用户名-账号
        /// </summary>
        public virtual string  UserName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string  TrueName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public virtual string  PassWord { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public virtual string  Email { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual string  Phone { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual string  Gender { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public virtual string  TypePath { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public virtual string  Address { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public virtual string  IdCard { get; set; }
        /// <summary>
        /// 身份证图片
        /// </summary>
        public virtual string  IdCardImg { get; set; }
        /// <summary>
        /// 个人支付账户
        /// </summary>
        public virtual string  PayInfo { get; set; }
        /// <summary>
        /// 个人形象
        /// </summary>
        public virtual string  UserImg { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public virtual string  Status { get; set; }
    }
}
