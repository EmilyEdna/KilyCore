using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 任务调度表
    /// </summary>
    public class SystemQuartz:BaseEntity
    {
        /// <summary>
        /// 任务分组
        /// </summary>
        public virtual string JobGroup { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public virtual string JobName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual  DateTime? EndTime { get; set; }
        /// <summary>
        /// 执行任务间隔时间单位秒
        /// </summary>
        public virtual int IntervalSecond { get; set; }
        /// <summary>
        /// 执行次数
        /// </summary>
        public virtual int RunTimes { get; set; }
        /// <summary>
        /// 时间表达式
        /// </summary>
        public virtual string Cron { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        public virtual JobEnum JobType { get; set; }
        /// <summary>
        /// 任务详情
        /// </summary>
        public virtual string JobDetail { get; set; }
    }
}
