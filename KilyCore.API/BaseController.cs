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
        public ICompanyService CompanyService = EngineExtension.Context.Resolve<ICompanyService>();
        public IFinanceService FinanceService = EngineExtension.Context.Resolve<IFinanceService>();
    }
}