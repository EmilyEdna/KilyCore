using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点51分
/// </summary>
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
