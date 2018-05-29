using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Threading.Tasks;
using KilyCore.Configure;
using System.Linq;
using System.Collections.Generic;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点51分
/// </summary>
namespace KilyCore.Quartz
{
    /// <summary>
    /// 任务调度
    /// </summary>
    public class QuartzCore :IQuartzCore
    {
        private Task<IScheduler> instance;
        /// <summary>
        /// 初始化任务调度器
        /// </summary>
        public Task<IScheduler> Instance
        {
            get
            {
                if (instance != null)
                    return instance;
                else
                {
                    NameValueCollection props = new NameValueCollection { { "quartz.serializer.type", "binary" } };
                     instance = new StdSchedulerFactory(props).GetScheduler();
                    return instance;
                }
            }
        }
        /// <summary>
        /// 创建简单任务
        /// </summary>
        /// <param name="quartz"></param>
        /// <returns></returns>
        protected ITrigger CreateSimpleTrigger(QuartzMap quartz)
        {
            if (quartz.RunTimes >0)
            {
                return TriggerBuilder.Create().WithIdentity(quartz.JobName, quartz.JobGroup)
                     .StartAt(quartz.StartTime).EndAt(quartz.EndTime)
                     .WithSimpleSchedule(t => t.WithIntervalInSeconds(quartz.IntervalSecond)
                     .WithRepeatCount(quartz.RunTimes)).ForJob(quartz.JobName, quartz.JobGroup).Build();
            }
            else
            {
                //无限循环执行
                return TriggerBuilder.Create().WithIdentity(quartz.JobName, quartz.JobGroup)
                    .StartAt(quartz.StartTime).EndAt(quartz.EndTime)
                    .WithSimpleSchedule(t => t.WithIntervalInSeconds(quartz.IntervalSecond)
                    .RepeatForever()).ForJob(quartz.JobName, quartz.JobGroup).Build();
            }
        }
        /// <summary>
        /// 创建表达式任务
        /// </summary>
        /// <param name="quartz"></param>
        /// <returns></returns>
        protected ITrigger CreateCronTrigger(QuartzMap quartz)
        {
            return TriggerBuilder.Create().WithIdentity(quartz.JobName, quartz.JobGroup)
                 .StartAt(quartz.StartTime).EndAt(quartz.EndTime)
                 .WithCronSchedule(quartz.Cron).ForJob(quartz.JobName, quartz.JobGroup).Build();
        }
        /// <summary>
        /// 暂停指定任务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="quartz"></param>
        /// <returns></returns>
        public async void StopResumeJob(QuartzMap quartz)
        {
            var key = new JobKey(quartz.JobName, quartz.JobGroup);
            if (await Instance.Result.CheckExists(key))
                await Instance.Result.PauseJob(key);
        }
        /// <summary>
        /// 恢复运行已经暂停的指定任务
        /// </summary>
        /// <param name="quartz"></param>
        public async void ResumeJob(QuartzMap quartz)
        {
            try
            {
                //检查任务是否存在
                var key = new JobKey(quartz.JobName, quartz.JobGroup);
                if (await Instance.Result.CheckExists(key))
                    await Instance.Result.ResumeJob(key);
            }
            catch (Exception)
            {
                throw new Exception("恢复任务失败!");
            }
        }
        /// <summary>
        /// 停止任务
        /// </summary>
        public async void StopJob()
        {
            try
            {
                //判断调度是否已经关闭
                if (!Instance.Result.IsShutdown)
                    //等待任务运行完成
                    await Instance.Result.Shutdown();
            }
            catch (Exception)
            {
                throw new Exception("停止任务失败!");
            }
        }
        /// <summary>
        /// 添加任务指定实现IJob接口的类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="quartz"></param>
        /// <returns></returns>
        public async Task<String> AddJob<T>(QuartzMap quartz) where T : IJob
        {
            try
            {
                await Instance.Result.Start();
                JobKey key = new JobKey(quartz.JobName, quartz.JobGroup);
                //任务存在则先删除
                if (await Instance.Result.CheckExists(key))
                    await Instance.Result.DeleteJob(key);
                IJobDetail job = JobBuilder.CreateForAsync<T>().WithIdentity(quartz.JobName, quartz.JobGroup).Build();
                if (!string.IsNullOrEmpty(quartz.Cron) && CronExpression.IsValidExpression(quartz.Cron))
                    await Instance.Result.ScheduleJob(job, CreateCronTrigger(quartz));
                else
                    await Instance.Result.ScheduleJob(job, CreateSimpleTrigger(quartz));
                return "添加任务成功!";
            }
            catch (Exception)
            {
                throw new Exception("添加任务出错!");
            }
        }
        /// <summary>
        /// 添加任务反射程序集模式
        /// </summary>
        /// <param name="quartz"></param>
        /// <returns></returns>
        public async Task<String> AddJob(QuartzMap quartz)
        {
            try
            {
                await Instance.Result.Start();
                JobKey key = new JobKey(quartz.JobName, quartz.JobGroup);
                //任务存在则先删除
                if (await Instance.Result.CheckExists(key))
                    await Instance.Result.DeleteJob(key);
                IEnumerable<Type> JobType = Configer.Assembly.SelectMany(t => t.ExportedTypes.Where(x => x.GetInterfaces().Contains(typeof(IJob))));
                JobType.ToList().ForEach(async t =>
                {
                    IJobDetail job = new JobDetailImpl(quartz.JobName, quartz.JobGroup, t);
                    if (!string.IsNullOrEmpty(quartz.Cron) && CronExpression.IsValidExpression(quartz.Cron))
                        await Instance.Result.ScheduleJob(job, CreateCronTrigger(quartz));
                    else
                        await Instance.Result.ScheduleJob(job, CreateSimpleTrigger(quartz));
                });
                return "添加任务成功!";
            }
            catch (Exception)
            {
                throw new Exception("添加任务出错!");
            }
        }
    }
}

