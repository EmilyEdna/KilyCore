using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.Extension.ApplicationService.DependencyIdentity
{
    public interface IEngine
    {
        IServiceProvider ServiceProvider(IServiceCollection Collection);
        T Resolve<T>();
    }
}
