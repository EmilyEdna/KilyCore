using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.Extension.ApplicationService.DependencyIdentity
{
    public interface IEngine
    {
        IServiceProvider ServiceProvider(IServiceCollection Collection);
        T Resolve<T>();
    }
}
