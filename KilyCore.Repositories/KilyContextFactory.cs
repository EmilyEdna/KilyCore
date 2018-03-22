using KilyCore.EntityFrameWork;
using KilyCore.Extension.ApplicationService.DependencyIdentity;
using System;

namespace KilyCore.Repositories
{
    public class KilyContextFactory
    {
        public static KilyContext GetContext()
        {
            if (EngineExtension.Context.Resolve<IKilyContext>() == null)
                return new KilyContext();
            else
                return (KilyContext)EngineExtension.Context.Resolve<IKilyContext>();
        }
    }
}
