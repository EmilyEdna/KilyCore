using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastVideo
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/14 11:38:54
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Repast
{
    /// <summary>
    /// 实时监控
    /// </summary>
    public class RepastVideo : RepastBase
    {
        /// <summary>
        /// 监控区域
        /// </summary>
        public virtual string MonitorAddress { get; set; }
        /// <summary>
        /// 视频地址
        /// </summary>
        public virtual string VideoAddress { get; set; }
        /// <summary>
        /// 封面图
        /// </summary>
        public virtual string CoverPhoto { get; set; }
        /// <summary>
        /// 是否首页显示
        /// </summary>
        public virtual bool IsIndex { get; set; }
        public virtual string TypePath { get; set; }
    }
}
