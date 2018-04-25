using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KilyCore.EntityFrameWork.ModelEnum
{
    /// <summary>
    /// 系统版本
    /// </summary>
    public enum SystemVersionEnum
    {
        [Description("无版本")]
        Normal=0,
        [Description("体验版")]
        Test =10,
        [Description("基础版")]
        Base=20,
        [Description("升级版")]
        Level=30,
        [Description("旗舰版")]
        Enterprise=40,
        [Description("定制版")]
        DIY=50
    }
}
