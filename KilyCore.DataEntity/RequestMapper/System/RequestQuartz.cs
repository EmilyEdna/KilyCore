using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.System
{
    public class RequestQuartz
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
        public int IntervalSecond { get; set; }
        /// <summary>
        /// 执行次数
        /// </summary>
        public int RunTimes { get; set; }
        /// <summary>
        /// 时间表达式
        /// </summary>
        public string Cron { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        public JobEnum JobType { get; set; }
        /// <summary>
        /// 任务详情
        /// </summary>
        public string JobDetail { get; set; }
    }
}
