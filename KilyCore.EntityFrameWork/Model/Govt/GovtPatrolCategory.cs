using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：GovtPatrolCategory
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/25 10:20:35
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Govt
{
    /// <summary>
    /// 执法类目表
    /// </summary>
    public class GovtPatrolCategory: GovtBase
    {
        /// <summary>
        /// 类目名称
        /// </summary>
        public virtual string CategoryName { get; set; }
        /// <summary>
        /// 所属类型
        /// </summary>
        public virtual string CategoryType { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        public virtual string CompanyType { get; set; }
        /// <summary>
        /// 评分项
        /// </summary>
        public virtual string Grade { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 模板
        /// </summary>
        public virtual string Template { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public virtual string TypePath { get; set; }
    }
}
