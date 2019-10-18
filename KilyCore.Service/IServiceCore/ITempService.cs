using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using System;

namespace KilyCore.Service.IServiceCore
{
    public interface ITempService : IService
    {
        object GetAllUser(Guid CompanyId);

        object GetAllSupply(Guid CompanyId, int Type);

        object GetAllSample(Guid CompanyId);

        object RepastDuck(Guid CompanyId);

        object WeChatRegist(RequestEnterprise Param);

        object GetInviteCode(string Area);

        object RepastThing(Guid id);

        object RepastWeek(Guid id);

        object RepastMarket(Guid id);

        object RepastCheck(Guid id);
        object RepastFoods(Guid id);

        object RepastSelfCheck(Guid id);

        object GetBuybillPage(Guid CompanyId, string SDate, string EDate);

        object RepastProduct(Guid CompanyId);

        object GetProductPage(string Category, int PageIndex, int PageSize);

        object GetProductDetail(Guid ID);
    }
}