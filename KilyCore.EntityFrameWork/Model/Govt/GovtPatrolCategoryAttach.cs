using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：GovtPatrolCategoryAttach
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/25 10:30:36
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Govt
{
    /// <summary>
    /// 执法类目问题库表
    /// </summary>
    public class GovtPatrolCategoryAttach: GovtBase
    {
        /// <summary>
        ///  执法类目表Id
        /// </summary>
        public virtual Guid? PatralCategoryId { get; set; }
        /// <summary>
        /// 问题标题
        /// </summary>
        public virtual string QuestionTitle { get; set; }
        /// <summary>
        /// 选择类型
        /// </summary>
        public virtual ElementEnum SelectType { get; set; }
        /// <summary>
        /// 选项类型
        /// </summary>
        public virtual string Type { get; set; }
        /// <summary>
        /// 分值
        /// </summary>
        public virtual int? Score { get; set; }
        /// <summary>
        /// 答案
        /// </summary>
        public virtual string Answer { get; set; }
        /// <summary>
        /// 答案分值
        /// </summary>
        public virtual string AnswerScore { get; set; }
    }
}
