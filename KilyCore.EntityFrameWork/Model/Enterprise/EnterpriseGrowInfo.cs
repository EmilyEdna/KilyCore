using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 育苗信息表
    /// </summary>
    public class EnterpriseGrowInfo : EnterpriseBase
    {
        /// <summary>
        /// 批次号
        /// </summary>
        public virtual string BacthNo { get; set; }
        /// <summary>
        /// 育苗名称
        /// </summary>
        public virtual string GrowName { get; set; }
        /// <summary>
        /// 采购时间
        /// </summary>
        public virtual DateTime BuyTime { get; set; }
        /// <summary>
        /// 采购数量
        /// </summary>
        public virtual string BuyNum { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public virtual string Unit { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual  string Remark { get; set; }
    }
}
