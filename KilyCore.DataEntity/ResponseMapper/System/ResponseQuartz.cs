﻿using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.DataEntity.ResponseMapper.System
{
    public class ResponseQuartz
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 任务分组
        /// </summary>
        public string JobGroup { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 执行任务间隔时间单位秒
        /// </summary>
        public int? IntervalSecond { get; set; }
        /// <summary>
        /// 执行次数
        /// </summary>
        public string RunTimes { get; set; }
        /// <summary>
        /// 时间表达式
        /// </summary>
        public string Cron { get; set; }
        /// <summary>
        /// 任务详情
        /// </summary>
        public string JobDetail { get; set; }
        public string JobState { get; set; }
    }
}
