using KilyCore.DataEntity.AttributeMapper;
using KilyCore.DataEntity.RequestMapper.Function;
using KilyCore.DataEntity.ResponseMapper.Function;
using KilyCore.EntityFrameWork.Model.Function;
using KilyCore.EntityFrameWork.Model.System;
using KilyCore.EntityFrameWork.ModelEnum;
using KilyCore.Extension.AutoMapperExtension;
using KilyCore.Repositories.BaseRepository;
using KilyCore.Service.ConstMessage;
using KilyCore.Service.IServiceCore;
using KilyCore.Service.QueryExtend;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KilyCore.Service.ServiceCore
{
    public class FunctionService : Repository, IFunctionService
    {
        #region 区域价目
        /// <summary>
        /// 区域价目分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseAreaPrice> GetAreaPricePage(PageParamList<RequestAreaPrice> pageParam)
        {
            IQueryable<FunctionAreaPrice> queryable = Kily.Set<FunctionAreaPrice>()
                .Where(t => t.IsDelete == false)
                .OrderByDescending(t => t.CreateTime).AsNoTracking();
            IQueryable<ResponseAreaPrice> queryables = null;
            if (!string.IsNullOrEmpty(pageParam.QueryParam.ProjectName))
                queryable = queryable.Where(t => t.ProjectName.Contains(pageParam.QueryParam.ProjectName));
            if (UserInfo().AccountType == AccountEnum.Admin || UserInfo().AccountType == AccountEnum.Country)
            {
                queryables = queryable.Select(t => new ResponseAreaPrice()
                {
                    Id = t.Id,
                    ProjectName = t.ProjectName,
                    ProvincePrice = t.ProvincePrice,
                    CityPrice = t.CityPrice,
                    AreaPrice = t.AreaPrice,
                    TownPrice = t.TownPrice
                });
            }
            if (UserInfo().AccountType == AccountEnum.Province)
            {
                queryable = queryable.Where(t => UserInfo().TypePath.Contains(t.ProvinceId.ToString()));
                queryables = queryable.Select(t => new ResponseAreaPrice()
                {
                    Id = t.Id,
                    ProjectName = t.ProjectName,
                    ProvincePrice = t.ProvincePrice,
                    CityPrice = t.CityPrice
                });
            }
            if (UserInfo().AccountType == AccountEnum.City)
            {
                queryable = queryable.Where(t => UserInfo().TypePath.Contains(t.CityId.ToString()));
                queryables = queryable.Select(t => new ResponseAreaPrice()
                {
                    Id = t.Id,
                    ProjectName = t.ProjectName,
                    ProvincePrice = t.ProvincePrice,
                    CityPrice = t.CityPrice,
                    AreaPrice = t.AreaPrice
                });
            }
            if (UserInfo().AccountType == AccountEnum.Area)
            {
                queryable = queryable.Where(t => UserInfo().TypePath.Contains(t.AreaId.ToString()));
                queryables = queryable.Select(t => new ResponseAreaPrice()
                {
                    Id = t.Id,
                    ProjectName = t.ProjectName,
                    CityPrice = t.CityPrice,
                    AreaPrice = t.AreaPrice,
                    TownPrice = t.TownPrice
                });
            }
            if (UserInfo().AccountType == AccountEnum.Village)
            {
                queryable = queryable.Where(t => UserInfo().TypePath.Contains(t.TownId.ToString()));
                queryables = queryable.Select(t => new ResponseAreaPrice()
                {
                    Id = t.Id,
                    ProjectName = t.ProjectName,
                    AreaPrice = t.AreaPrice,
                    TownPrice = t.TownPrice
                });
            }
            var data = queryables.ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 获取价目详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseAreaPrice GetAreaPriceDetail(Guid Id)
        {
            var data = Kily.Set<FunctionAreaPrice>().AsNoTracking()
                .Where(t => t.Id == Id)
                .Select(t => new ResponseAreaPrice()
                {
                    Id = t.Id,
                    ProjectName = t.ProjectName,
                    ProvincePrice = t.ProvincePrice,
                    CityPrice = t.CityPrice,
                    AreaPrice = t.AreaPrice,
                    TownPrice = t.TownPrice
                }).FirstOrDefault();
            return data;
        }
        /// <summary>
        ///  编辑区域价目
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditPrice(RequestAreaPrice Param)
        {
            FunctionAreaPrice AreaPrice = Param.MapToEntity<FunctionAreaPrice>();
            if (Param.Id != Guid.Empty)
            {
                if (Update<FunctionAreaPrice, RequestAreaPrice>(AreaPrice, Param, PropertyCollection<RequestAreaPrice, MapperAttribute>(Param, Updates.Ignore)))
                    return ServiceMessage.UPDATESUCCESS;
                else
                    return ServiceMessage.UPDATEFAIL;
            }
            else
            {
                if (Insert<FunctionAreaPrice>(AreaPrice))
                    return ServiceMessage.INSERTSUCCESS;
                else
                    return ServiceMessage.INSERTFAIL;
            }
        }
        /// <summary>
        /// 删除价目表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemovePrice(Guid Id)
        {
            if (Delete<FunctionAreaPrice>(ExpressionExtension.GetExpression<FunctionAreaPrice>("Id", Id, ExpressionEnum.Equals)))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <param name="AccountType"></param>
        /// <returns></returns>
        public IList<ResponseAreaPrice> GetAddressList(int AccountType)
        {
            IList<ResponseAreaPrice> queryable = null;
            Guid ProvinceId = Guid.Parse(UserInfo().Province);
            Guid CityId = Guid.Parse(UserInfo().City);
            Guid AreaId = Guid.Parse(UserInfo().Area);
            var ProvinceCode = Kily.Set<SystemProvince>().Where(x => x.Id == ProvinceId).Select(x => x.Code).FirstOrDefault();
            var CityCode = Kily.Set<SystemCity>().Where(x => x.Id == CityId).Select(x => x.Code).FirstOrDefault();
            var AreaCode = Kily.Set<SystemArea>().Where(x => x.Id == AreaId).Select(x => x.Code).FirstOrDefault();
            if (AccountType == 10 || AccountType == 20)
                queryable = Kily.Set<SystemProvince>().AsNoTracking().Select(t => new ResponseAreaPrice()
                {
                    Id = t.Id,
                    ProjectName = t.Name
                }).ToList();
            if (AccountType == 30)
                queryable = Kily.Set<SystemCity>().Where(t => t.ProvinceCode == ProvinceCode).Select(t => new ResponseAreaPrice()
                {
                    Id = t.Id,
                    ProjectName = t.Name
                }).ToList();
            if (AccountType == 40)
                queryable = Kily.Set<SystemArea>().Where(t => t.CityCode == CityCode).Select(t => new ResponseAreaPrice()
                {
                    Id = t.Id,
                    ProjectName = t.Name
                }).ToList();
            if (AccountType == 50)
                queryable = Kily.Set<SystemTown>().Where(t => t.AreaCode == AreaCode).Select(t => new ResponseAreaPrice()
                {
                    Id = t.Id,
                    ProjectName = t.Name
                }).ToList();
            return queryable;
        }
        #endregion
    }
}
