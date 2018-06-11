using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月30日15点42分
/// </summary>
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 成长流程表
    /// </summary>
    public class EnterpriseAgeUp : EnterpriseBase
    {
        /// <summary>
        /// 批次
        /// </summary>
        public virtual string BatchNo { get; set; }
        /// <summary>
        /// 阶段名称
        /// </summary>
        public virtual string LvName { get; set; }
        /// <summary>
        /// 阶段图片
        /// </summary>
        public virtual string LvImg { get; set; }
    }
}
