using KilyCore.Extension.ApplicationService.DependencyIdentity;
using KilyCore.Service.IServiceCore;
using System;

namespace KilyCore.API
{
    public class IocProviderServices
    {
        public static IocProviderServices Instance { get => new Lazy<IocProviderServices>().Value; }
        public IIocProviderService IocProviderService = EngineExtension.Context.Resolve<IIocProviderService>();
    }
}