using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.Model.Base
{
    /// <summary>
    /// 企业公共表字段
    /// </summary>
    public class EnterpriseBase :BaseEntity
    {
        /// <summary>
        /// 企业Id
        /// </summary>
        public virtual Guid CompanyId { get; set; }
    }
}
