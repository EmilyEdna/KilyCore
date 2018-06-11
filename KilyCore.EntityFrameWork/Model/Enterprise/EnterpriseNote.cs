using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 成长日记表
    /// </summary>
    public class EnterpriseNote: EnterpriseBase
    {
        /// <summary>
        /// 日记名称
        /// </summary>
        public virtual string NoteName { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public virtual string BatchNo { get; set; }
        /// <summary>
        /// 播种时间
        /// </summary>
        public virtual DateTime? SowingTime { get; set; } 
        /// <summary>
        /// 收获时间
        /// </summary>
        public virtual DateTime? ResultTime { get; set; }
    }
}
