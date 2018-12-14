using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseCookInfo
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Cook
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/24 14:29:58
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Cook
{
    public class ResponseCookInfo
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 厨师会员表Id
        /// </summary>
        public Guid? CookId { get; set; }
        /// <summary>
        /// 开通时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string TrueName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int? Sexlab { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCardNo { get; set; }
        /// <summary>
        /// 所在区域
        /// </summary>
        public string TypePath { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 发证机关
        /// </summary>
        public string CardOffice { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpiredDay { get; set; }
        /// <summary>
        /// 身份证正反面
        /// </summary>
        public string IdCardPhoto { get; set; }
        /// <summary>
        /// 登记证
        /// </summary>
        public string BookInCard { get; set; }
        /// <summary>
        /// 培训合格证
        /// </summary>
        public string TrainCard { get; set; }
        /// <summary>
        /// 健康证
        /// </summary>
        public string HealthCard { get; set; }
        /// <summary>
        /// 是否会员
        /// </summary>
        public bool IsVip { get; set; }
        public string VipType { get => IsVip ? "是" : "否"; }
        public string Province
        {
            get
            {
                return !string.IsNullOrEmpty(TypePath) ? (TypePath.Split(',').Length >= 1 ? TypePath.Split(',')[0] : null) : null;
            }
        }
        public string City
        {
            get
            {
                return !string.IsNullOrEmpty(TypePath) ? (TypePath.Split(',').Length >= 2 ? TypePath.Split(',')[1] : null) : null;
            }
        }
        public string Area
        {
            get
            {
                return !string.IsNullOrEmpty(TypePath) ? (TypePath.Split(',').Length >= 3 ? TypePath.Split(',')[2] : null) : null;
            }
        }
        public string Town
        {
            get
            {
                return !string.IsNullOrEmpty(TypePath) ? (TypePath.Split(',').Length >= 4 ? (TypePath.Split(',')[3]) : null) : null;
            }
        }
        /// <summary>
        /// 所属角色
        /// </summary>
        public Guid? RoleId { get; set; }
        public string TableName { get; set; }
        public AuditEnum AuditType { get; set; }
        public string AuditTypeName { get; set; }
        public bool? IsEnable { get; set; }
        public string Stauts { get => IsEnable.HasValue ? ((bool)IsEnable ? "停用" : "启用") : ""; }
    }
}
