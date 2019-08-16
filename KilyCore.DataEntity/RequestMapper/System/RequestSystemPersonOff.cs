using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.System
{
    public class RequestSystemPersonOff
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string UserNo { get; set; }
        /// <summary>
        /// 用户名-账号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string TrueName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string TypePath { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 身份证图片
        /// </summary>
        public string IdCardImg { get; set; }
        /// <summary>
        /// 个人支付账户
        /// </summary>
        public string PayInfo { get; set; }
        /// <summary>
        /// 个人形象
        /// </summary>
        public string UserImg { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 账户类型
        /// </summary>
        public string PayType { get; set; }
    }
}
