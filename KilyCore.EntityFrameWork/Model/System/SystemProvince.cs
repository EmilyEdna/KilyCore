using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 省份表
    /// </summary>
    public class SystemProvince : BaseEntity
    {
        /// <summary>
        /// 省份名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 省代码
        /// </summary>
        public virtual int Code { get; set; }
    }
}
