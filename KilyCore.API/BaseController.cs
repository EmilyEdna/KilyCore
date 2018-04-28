using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.Extension.ApplicationService.DependencyIdentity;
using KilyCore.Service.IServiceCore;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.API
{
    public class BaseController : Controller
    {
        public ISystemService SystemService = EngineExtension.Context.Resolve<ISystemService>();
        public IFinanceService FinanceService = EngineExtension.Context.Resolve<IFinanceService>();
        public IEnterpriseService EnterpriseService = EngineExtension.Context.Resolve<IEnterpriseService>();
        public IDiningService DiningService = EngineExtension.Context.Resolve<IDiningService>();
        public IFunctionService FunctionService = EngineExtension.Context.Resolve<IFunctionService>();

        public IEnterpriseWebService EnterpriseWebService = EngineExtension.Context.Resolve<IEnterpriseWebService>();
    }
}