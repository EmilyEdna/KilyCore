﻿using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseRepastVideo
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/14 11:52:25
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Repast
{
    public class ResponseRepastVideo
    {
        public Guid Id { get; set; }
        public Guid InfoId { get; set; }
        /// <summary>
        /// 监控区域
        /// </summary>
        public string MonitorAddress { get; set; }
        /// <summary>
        /// 视频地址
        /// </summary>
        public string VideoAddress { get; set; }
        /// <summary>
        /// 封面图
        /// </summary>
        public string CoverPhoto { get; set; }
    }
}
