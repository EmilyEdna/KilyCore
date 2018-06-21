using KilyCore.EntityFrameWork.Model.System;
using KilyCore.EntityFrameWork.ModelEnum;
using KilyCore.Extension.AutoMapperExtension;
using KilyCore.Quartz;
using KilyCore.Repositories.BaseRepository;
using KilyCore.Service.IServiceCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：IocProviderService
* 类 描 述 ：
* 命名空间 ：KilyCore.Service.ServiceCore
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/21 14:49:58
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.Service.ServiceCore
{
    public class IocProviderService: Repository,IIocProviderService
    {
        /// <summary>
        /// 重启服务器后自动添加数据库中已启动的任务到Quartz中
        /// </summary>
        /// <returns></returns>
        public string RestartQuartz()
        {
            IList<SystemQuartz> queryable = Kily.Set<SystemQuartz>().Where(t => t.IsDelete == false && t.JobType == JobEnum.Run).ToList();
            List<QuartzMap> quartz = queryable.MapToList<SystemQuartz, QuartzMap>();
            string msg = string.Empty;
            try
            {
                quartz.ForEach(t =>
                {
                    msg = QuartzCoreFactory.QuartzCore().AddJob(t).Result;
                });
                return msg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
