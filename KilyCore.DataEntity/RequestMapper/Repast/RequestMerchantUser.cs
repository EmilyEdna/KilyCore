using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestRepastInfoUser
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Repast
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/16 11:49:01
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Repast
{
    public class RequestMerchantUser
    {
        public Guid Id { get; set; }
        public Guid? InfoId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string TrueName { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 健康证
        /// </summary>
        public string HealthCard { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 系统版本
        /// </summary>
        public SystemVersionEnum VersionType { get; set; }
        /// <summary>
        /// 商家类型
        /// </summary>
        public MerchantEnum DiningType { get; set; }
        /// <summary>
        /// 餐饮角色ID
        /// </summary>
        public Guid? DingRoleId { get; set; }
        /// <summary>
        /// 商家名称
        /// </summary>
        public string MerchantName { get; set; }
        /// <summary>
        /// 健康证到期时间
        /// </summary>
        public DateTime? ExpiredTime { get; set; }
    }
}
