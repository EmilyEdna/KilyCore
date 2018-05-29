using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.Model.Function
{
    /// <summary>
    /// 系统码表
    /// </summary>
    public class FunctionDictionary : BaseEntity
    {
        /// <summary>
        /// 码表名称
        /// </summary>
        public virtual string DicName { get; set; }
        /// <summary>
        /// 码表值
        /// </summary>
        public virtual string DicValue { get; set; }
        /// <summary>
        /// 码表介绍
        /// </summary>
        public virtual string DicDescript { get; set; }
        /// <summary>
        /// 附加信息
        /// </summary>
        public virtual string AttachInfo { get; set; }
    }
}