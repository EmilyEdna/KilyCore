using KilyCore.EntityFrameWork;
using KilyCore.Extension.ApplicationService.DependencyIdentity;
using System;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
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
