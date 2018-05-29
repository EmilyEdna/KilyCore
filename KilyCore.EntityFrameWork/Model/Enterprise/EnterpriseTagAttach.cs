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
    public class EnterpriseTagAttach : EnterpriseBase
    {
        /// <summary>
        /// 主表标签Id
        /// </summary>
        public virtual Guid TagId { get; set; }
        /// <summary>
        /// 产品表Id
        /// </summary>
        public virtual Guid ProductId { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string No { get; set; }
        /// <summary>
        /// 是否使用
        /// </summary>
        public virtual bool IsUse { get; set; }
    }
}
