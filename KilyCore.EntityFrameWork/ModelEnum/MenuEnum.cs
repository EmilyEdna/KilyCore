using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KilyCore.EntityFrameWork.ModelEnum
{
    /// <summary>
    /// 菜单枚举
    /// </summary>
    public enum MenuEnum
    {
        /// <summary>
        /// 一级菜单
        /// </summary>
        [Description("一级菜单")]
        LevelOne = 10,
        /// <summary>
        /// 二级菜单
        /// </summary>
        [Description("二级菜单")]
        LevelTwo = 20
    }
}
