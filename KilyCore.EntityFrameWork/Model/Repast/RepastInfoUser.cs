using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastInfoUser
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Repast
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/16 11:46:04
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Repast
{
    /// <summary>
    /// 餐饮用户表
    /// </summary>
    public class RepastInfoUser: RepastBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string TrueName { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public virtual string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public virtual string PassWord { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public virtual string IdCard { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public virtual string TypePath { get; set; }
        /// <summary>
        /// 健康证到期时间
        /// </summary>
        public virtual DateTime? ExpiredTime { get; set; }
        /// <summary>
        /// 系统版本
        /// </summary>
        public virtual SystemVersionEnum VersionType { get; set; }
        /// <summary>
        /// 商家类型
        /// </summary>
        public virtual MerchantEnum DiningType { get; set; }
        /// <summary>
        /// 餐饮角色ID
        /// </summary>
        public virtual Guid? DingRoleId { get; set; }
        /// <summary>
        /// 商家名称
        /// </summary>
        public virtual string MerchantName { get; set; }
        /// <summary>
        /// 健康证
        /// </summary>
        public virtual string HealthCard { get; set; }
    }
}
