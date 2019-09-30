using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：KilyCore.DataEntity.ResponseMapper.Govt
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/12/28 10:05:07
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Govt
{
    public class ResponseGovtTemplate
    {
        public Guid Id { get; set; }
        public Guid? GovtId { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        public string CompanyType { get; set; }
        /// <summary>
        /// 模板所在管理区域
        /// </summary>
        public string TypePath { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string TemplateContent { get; set; }
    }
    public class ResponseGovtTemplateChild
    {
        public Guid Id { get; set; }
        public Guid? GovtId { get; set; }
        /// <summary>
        /// 检查人
        /// </summary>
        public string CheckUser { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        public string CompanyType { get; set; }
        /// <summary>
        /// 模板所在管理区域
        /// </summary>
        public string TypePath { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string TemplateContent { get; set; }
        /// <summary>
        /// 自查日期
        /// </summary>
        public string CreateTime { set; get; }
    }
}
