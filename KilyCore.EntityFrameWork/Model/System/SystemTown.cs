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
    public class SystemTown:BaseEntity
    {
        /// <summary>
        /// 乡代码
        /// </summary>
        public virtual  int Code { get; set; }
        /// <summary>
        /// 乡名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 省份代码
        /// </summary>
        public virtual int ProvinceCode { get; set; }
        /// <summary>
        /// 城市代码
        /// </summary>
        public virtual int CityCode { get; set; }
        /// <summary>
        /// 区域代码
        /// </summary>
        public virtual int AreaCode { get; set; }
    }
}
