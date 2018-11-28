using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseGovtPatrolCategoryAttach
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/25 11:22:52
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Govt
{
    public class ResponseGovtPatrolCategoryAttach
    {
        public Guid Id { get; set; }
        public Guid? GovtId { get; set; }
        /// <summary>
        ///  执法类目表Id
        /// </summary>
        public Guid? PatralCategoryId { get; set; }
        /// <summary>
        /// 问题标题
        /// </summary>
        public string QuestionTitle { get; set; }
        /// <summary>
        /// 选择类型
        /// </summary>
        public ElementEnum SelectType { get; set; }
        /// <summary>
        /// 选择类型名称
        /// </summary>
        public string SelectTypeName { get; set; }
        /// <summary>
        /// 选项类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 分值
        /// </summary>
        public int? Score { get; set; }
        /// <summary>
        /// 答案
        /// </summary>
        public string Answer { get; set; }
        /// <summary>
        /// 答案分值
        /// </summary>
        public string AnswerScore { get; set; }
    }
}
