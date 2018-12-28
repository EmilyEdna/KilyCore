using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：KilyCore.EntityFrameWork.Model.Govt
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/12/28 9:52:20
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Govt
{
    /// <summary>
    /// 企业自查模板表
    /// </summary>
    public class GovtTemplate: GovtBase
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        public virtual string TemplateName { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        public virtual string CompanyType { get; set; }
        /// <summary>
        /// 模板所在管理区域
        /// </summary>
        public virtual string TypePath { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        public virtual string TemplateContent { get; set; }
    }
    /// <summary>
    /// 企业自查模板填写表
    /// </summary>
    public class GovtTemplateChild : GovtBase
    {
        /// <summary>
        /// 企业名称
        /// </summary>
        public virtual string CompanyName { get; set; }
        /// <summary>
        /// 检查人
        /// </summary>
        public virtual string CheckUser { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public virtual string TemplateName { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        public virtual string CompanyType { get; set; }
        /// <summary>
        /// 模板所在管理区域
        /// </summary>
        public virtual string TypePath { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        public virtual string TemplateContent { get; set; }
    }
}
