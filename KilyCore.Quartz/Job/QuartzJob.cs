using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KilyCore.Quartz.Job
{
    public class QuartzJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
             await Task.CompletedTask;
        }
    }
}
