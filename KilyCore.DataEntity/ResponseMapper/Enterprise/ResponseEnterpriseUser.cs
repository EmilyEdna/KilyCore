﻿using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseUser
    {
        /// <summary>
        /// 企业Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 公司用户Id
        /// </summary>
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        public CompanyEnum CompanyType { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public string CompanyTypeName { get; set; }
        /// <summary>
        /// 系统版本
        /// </summary>
        public SystemVersionEnum Version { get; set; }
        /// <summary>
        /// 版本名称
        /// </summary>
        public string VersionName { get; set; }
        /// <summary>
        /// 姓名
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
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 集团账户类型
        /// </summary>
        public Guid? RoleAuthorType { get; set; }
        public string TypePath { get; set; }
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
        public string CompanyAccount => Account;
        public string TableName { get; set; }
        public string LngAndLat { get; set; }
        public string CodeStar { get; set; }
    }
}
