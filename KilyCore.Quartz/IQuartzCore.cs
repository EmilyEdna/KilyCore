using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点51分
/// </summary>
namespace KilyCore.Quartz
{
    public interface IQuartzCore
    {
        void StopResumeJob(QuartzMap quartz);
        void ResumeJob(QuartzMap quartz);
        void StopJob();
        Task<String> AddJob<T>(QuartzMap quartz) where T : IJob;
        Task<String> AddJob(QuartzMap quartz);
    }
}
