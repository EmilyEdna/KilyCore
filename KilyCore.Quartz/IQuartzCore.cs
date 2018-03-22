using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
