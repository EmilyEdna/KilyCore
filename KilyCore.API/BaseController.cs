using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.Extension.ApplicationService.DependencyIdentity;
using KilyCore.Service.IServiceCore;
using Microsoft.AspNetCore.Mvc;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.API
{
    public class BaseController : Controller
    {
        public ISystemService SystemService = EngineExtension.Context.Resolve<ISystemService>();
        public IFinanceService FinanceService = EngineExtension.Context.Resolve<IFinanceService>();
        public IEnterpriseService EnterpriseService = EngineExtension.Context.Resolve<IEnterpriseService>();
        public IRepastService RepastService = EngineExtension.Context.Resolve<IRepastService>();
        public IFunctionService FunctionService = EngineExtension.Context.Resolve<IFunctionService>();
        public ICookService CookService = EngineExtension.Context.Resolve<ICookService>();
        public IGovtService GovtService = EngineExtension.Context.Resolve<IGovtService>();

        public IEnterpriseWebService EnterpriseWebService = EngineExtension.Context.Resolve<IEnterpriseWebService>();
        public IRepastWebService RepastWebService = EngineExtension.Context.Resolve<IRepastWebService>();
        public ICookWebService CookWebService = EngineExtension.Context.Resolve<ICookWebService>();
    }
}