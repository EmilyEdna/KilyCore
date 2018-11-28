using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.ModelEnum
{
    /// <summary>
    /// 账号枚举
    /// </summary>
   public  enum AccountEnum
    {
        /// <summary>
        /// 超级管理员
        /// </summary>
        [Description("超级管理员")]
        Admin = 10,
        /// <summary>
        /// 全国管理员
        /// </summary>
        [Description("全国管理员")]
        Country = 20,
        /// <summary>
        /// 省份管理员
        /// </summary>
        [Description("省份管理员")]
        Province = 30,
        /// <summary>
        /// 城市管理员
        /// </summary>
        [Description("城市管理员")]
        City = 40,
        /// <summary>
        /// 区域管理员
        /// </summary>
        [Description("区域管理员")]
        Area = 50,
        /// <summary>
        /// 乡镇管理员
        /// </summary>
        [Description("乡镇管理员")]
        Village = 60
    }
}
