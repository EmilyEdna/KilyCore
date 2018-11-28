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
    /// 区域表
    /// </summary>
    public class SystemArea : BaseEntity
    {
        /// <summary>
        /// 区名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 区代码
        /// </summary>
        public virtual int Code { get; set; }
        /// <summary>
        /// 省代码
        /// </summary>
        public virtual int ProvinceCode { get; set; }
        /// <summary>
        /// 市代码
        /// </summary>
        public virtual int CityCode { get; set; }
    }
}
