using KilyCore.Configure;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.Service.IServiceCore
{
    public interface ITempService: IService
    {
        object GetAllUser(Guid CompanyId);
        object GetAllSupply(Guid CompanyId,int Type);
        object GetAllSample(Guid CompanyId);
        object RepastDuck(Guid CompanyId);
    }
}
