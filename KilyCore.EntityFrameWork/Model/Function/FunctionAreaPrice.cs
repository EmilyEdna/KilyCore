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
    /// 区域价目表
    /// </summary>
    public class FunctionAreaPrice:BaseEntity
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string ProjectName { get; set; }
        /// <summary>
        /// 省份价格
        /// </summary>
        public virtual decimal? ProvincePrice { get; set; }
        /// <summary>
        /// 市价格
        /// </summary>
        public virtual decimal? CityPrice { get; set; }
        /// <summary>
        /// 区域价格
        /// </summary>
        public virtual decimal? AreaPrice { get; set; }
        /// <summary>
        /// 乡镇价格
        /// </summary>
        public virtual decimal? TownPrice { get; set; }
        /// <summary>
        /// 省份Id
        /// </summary>
        public virtual Guid? ProvinceId { get; set; } 
        /// <summary>
        /// 城市Id
        /// </summary>
        public virtual Guid? CityId { get; set; }
        /// <summary>
        /// 区县Id
        /// </summary>
        public virtual Guid? AreaId { get; set; }
        /// <summary>
        /// 乡镇Id
        /// </summary>
        public virtual Guid? TownId { get; set; }
    }
}
