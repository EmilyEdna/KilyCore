using KilyCore.EntityFrameWork.Model.Enterprise;
using KilyCore.EntityFrameWork.Model.Repast;
using KilyCore.EntityFrameWork.Model.System;
using KilyCore.Repositories.BaseRepository;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点51分
/// </summary>
namespace KilyCore.Quartz.Job
{
    public class QuartzJob : Repository, IJob
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            switch (context.JobDetail.Key.Group)
            {
                case "账户":
                    await CheckAccount();
                    break;
                case "订单":
                    await CheckOrderExpire();
                    break;
                default:
                    await Task.CompletedTask;
                    break;
            }
        }
        /// <summary>
        /// 账户检测
        /// </summary>
        /// <returns></returns>
        public async Task CheckAccount()
        {
            Kily.Set<SystemAdminAttach>().Where(t => t.EndTime <= DateTime.Now).Select(t => t.AdminId).ToList().ForEach(t =>
             {
                 Delete<SystemAdmin>(x => x.Id == t);
             });
            IQueryable<SystemStayContract> queryable = Kily.Set<SystemStayContract>().Where(t => t.EndTime <= DateTime.Now).OrderByDescending(t => t.CreateTime);
            queryable.Where(t => t.EnterpriseOrMerchant == 1).Select(t => t.CompanyId).ToList().ForEach(t =>
            {
                Delete<EnterpriseInfo>(x => x.Id == t);
                Delete<EnterpriseUser>(x => x.CompanyId == t);
            });
            queryable.Where(t => t.EnterpriseOrMerchant == 2).Select(t => t.CompanyId).ToList().ForEach(t =>
            {
                Delete<RepastInfo>(x => x.Id == t);
                Delete<RepastInfoUser>(x => x.InfoId == t);
            });
            await Task.CompletedTask;
        }
        /// <summary>
        /// 检测订单是否过期
        /// </summary>
        /// <returns></returns>
        public async Task CheckOrderExpire()
        {
            Kily.Set<SystemOrder>().Where(t => t.ExpireTime > DateTime.Now).ToList().ForEach(t =>
            {
                t.IsExpire = true;
                UpdateField(t, "IsExpire");
            });
            await Task.CompletedTask;
        }
    }
}
