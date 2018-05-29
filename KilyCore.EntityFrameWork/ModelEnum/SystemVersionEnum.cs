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
    /// 系统版本
    /// </summary>
    public enum SystemVersionEnum
    {
        /// <summary>
        /// 无版本
        /// </summary>
        [Description("无版本")]
        Normal=0,
        /// <summary>
        /// 体验版
        /// </summary>
        [Description("体验版")]
        Test =10,
        /// <summary>
        /// 基础班
        /// </summary>
        [Description("基础版")]
        Base=20,
        /// <summary>
        /// 升级版
        /// </summary>
        [Description("升级版")]
        Level=30,
        /// <summary>
        /// 旗舰版
        /// </summary>
        [Description("旗舰版")]
        Enterprise=40,
        /// <summary>
        /// 定制版
        /// </summary>
        [Description("定制版")]
        DIY=50
    }
}
