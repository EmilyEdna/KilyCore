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
    /// 权限等级
    /// </summary>
    public enum RoleEnum
    {
        /// <summary>
        /// 超级管理员
        /// </summary>
        [Description("超级管理员")]
        LV1=10,
        /// <summary>
        /// 全国管理员
        /// </summary>
        [Description("全国管理员")]
        LV2 =20,
        /// <summary>
        /// 省份管理员
        /// </summary>
        [Description("省份管理员")]
        LV3 =30,
        /// <summary>
        /// 城市管理员
        /// </summary>
        [Description("城市管理员")]
        LV4 =40,
        /// <summary>
        /// 区域管理员
        /// </summary>
        [Description("区域管理员")]
        LV5 =50,
        /// <summary>
        /// 乡镇管理员
        /// </summary>
        [Description("乡镇管理员")]
        LV6 =60
    }
}
