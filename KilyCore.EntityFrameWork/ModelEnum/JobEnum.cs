using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KilyCore.EntityFrameWork.ModelEnum
{
    /// <summary>
    /// 任务调度枚举
    /// </summary>
    public enum JobEnum
    {
        /// <summary>
        /// 运行
        /// </summary>
        [Description("运行")]
        Run=10,
        /// <summary>
        /// 暂停
        /// </summary>
        [Description("暂停")]
        Pause =20,
        /// <summary>
        /// 停止
        /// </summary>
        [Description("停止")]
        Stop =30
    }
}
