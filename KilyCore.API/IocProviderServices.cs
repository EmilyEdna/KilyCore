using KilyCore.Extension.ApplicationService.DependencyIdentity;
using KilyCore.Service.IServiceCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KilyCore.API
{
    public class IocProviderServices
    {
        public static IocProviderServices Instance { get => new Lazy<IocProviderServices>().Value; }
        public IIocProviderService IocProviderService = EngineExtension.Context.Resolve<IIocProviderService>();
    }
}
