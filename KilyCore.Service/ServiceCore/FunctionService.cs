﻿using KilyCore.DataEntity.RequestMapper.Function;
using KilyCore.DataEntity.ResponseMapper.Function;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.Model.Enterprise;
using KilyCore.EntityFrameWork.Model.Function;
using KilyCore.EntityFrameWork.Model.Repast;
using KilyCore.EntityFrameWork.Model.System;
using KilyCore.EntityFrameWork.ModelEnum;
using KilyCore.Extension.AttributeExtension;
using KilyCore.Extension.AutoMapperExtension;
using KilyCore.Repositories.BaseRepository;
using KilyCore.Service.ConstMessage;
using KilyCore.Service.IServiceCore;
using KilyCore.Service.QueryExtend;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
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
                    TownPrice = t.TownPrice,
                    ProvinceId = t.ProvinceId,
                    CityId = t.CityId,
                    AreaId = t.AreaId,
                    TownId = t.TownId
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
                FunctionAreaPrice Entity = Kily.Set<FunctionAreaPrice>().Where(t => t.Id == Param.Id).FirstOrDefault();
                IList<String> Fields = new List<String>();
                if (UserInfo().AccountType == AccountEnum.Admin || UserInfo().AccountType == AccountEnum.Country)
                {
                    Entity.ProvinceId = Param.ProvinceId;
                    Entity.ProvincePrice = Param.ProvincePrice;
                    Fields.Add("ProvinceId");
                    Fields.Add("ProvincePrice");
                }
                if (UserInfo().AccountType == AccountEnum.Province)
                {
                    Entity.CityId = Param.CityId;
                    Entity.CityPrice = Param.CityPrice;
                    if (Entity.CityPrice > (Entity.ProvincePrice + Entity.ProvincePrice * (decimal)0.5))
                        return "当前价格高出标准价格50%";
                    Fields.Add("CityId");
                    Fields.Add("CityPrice");
                }
                if (UserInfo().AccountType == AccountEnum.City)
                {
                    Entity.AreaId = Param.AreaId;
                    Entity.AreaPrice = Param.AreaPrice;
                    if (Entity.AreaPrice > (Entity.CityPrice + Entity.CityPrice * (decimal)0.5))
                        return "当前价格高出标准价格50%";
                    Fields.Add("AreaId");
                    Fields.Add("AreaPrice");
                }
                if (UserInfo().AccountType == AccountEnum.Area)
                {
                    Entity.TownId = Param.TownId;
                    Entity.TownPrice = Param.TownPrice;
                    if (Entity.TownPrice > (Entity.AreaPrice + Entity.AreaPrice * (decimal)0.5))
                        return "当前价格高出标准价格50%";
                    Fields.Add("TownId");
                    Fields.Add("TownPrice");
                }
                if (UpdateField<FunctionAreaPrice>(Entity, null, Fields))
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

        #endregion 区域价目

        #region 纹理二维码

        /// <summary>
        /// 纹理二维码分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseVeinTag> GetTagPage(PageParamList<RequestVeinTag> pageParam)
        {
            IQueryable<FunctionVeinTag> queryable = Kily.Set<FunctionVeinTag>().AsNoTracking().OrderByDescending(t => t.CreateTime).Where(t => t.IsDelete == false);
            IQueryable<FunctionVeinTagAttach> queryables = Kily.Set<FunctionVeinTagAttach>().AsNoTracking().OrderByDescending(t => t.CreateTime).Where(t => t.IsDelete == false);
            if (UserInfo().AccountType <= AccountEnum.Country)
            {
                var data = queryable.Select(t => new ResponseVeinTag()
                {
                    Id = t.Id,
                    BatchNo = t.BatchNo,
                    StarSerialNo = t.StarSerialNo,
                    EndSerialNo = t.EndSerialNo,
                    TotalNo = t.TotalNo,
                    IsAccept = t.IsAccept,
                    SingleBatchNo = t.BatchNo,
                    AcceptUser = t.AcceptUser,
                    AllotNum = t.AllotNum,
                }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
                return data;
            }
            else
            {
                var data = queryables.Where(t => t.AcceptUser.Contains(UserInfo().Id.ToString())).Select(t => new ResponseVeinTag()
                {
                    Id = t.Id,
                    BatchNo = t.BatchNo,
                    StarSerialNo = t.StarSerialNo,
                    EndSerialNo = t.EndSerialNo,
                    TotalNo = t.TotalNo,
                    SingleBatchNo = t.SingleBatchNo,
                    IsAccept = t.IsAccept,
                    AcceptUser = t.AcceptUser,
                    AllotNum = t.AllotNum,
                }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
                return data;
            }
        }

        /// <summary>
        /// 查看分配企业标签
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseVeinTag> GetTagToCompanyPage(PageParamList<RequestVeinTag> pageParam)
        {
            var data = Kily.Set<EnterpriseVeinTag>()
               .Where(t => t.CompanyId == pageParam.QueryParam.Id)
               .OrderByDescending(t => t.CreateTime).Select(t => new ResponseVeinTag
               {
                   AcceptUserName = t.AcceptUserName,
                   StarSerialNo = t.StarSerialNo,
                   EndSerialNo = t.EndSerialNo,
                   TotalNo = t.TotalNo,
                   AllotType = t.AllotType,
                   IsAcceptName = t.IsAccept ? "已签收" : "未签收"
               })
               .ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 查看分配营运中心标签
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseVeinTag> GetTagToAdminPage(PageParamList<RequestVeinTag> pageParam)
        {
            var data = Kily.Set<FunctionVeinTagAttach>()
                .Where(t => t.AcceptUser == pageParam.QueryParam.Id.ToString())
                .OrderByDescending(t => t.CreateTime).Select(t => new ResponseVeinTag()
                {
                    AcceptUserName = t.AcceptUserName,
                    StarSerialNo = t.StarSerialNo,
                    EndSerialNo = t.EndSerialNo,
                    TotalNo = t.TotalNo,
                    AllotType = t.AllotType,
                    IsAcceptName = t.IsAccept ? "已签收" : "未签收"
                }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 录入标签
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string RecordTag(RequestVeinTag Param)
        {
            Param.TotalNo = (int)(Param.EndSerialNo - Param.StarSerialNo) + 1;
            FunctionVeinTag VeinTag = Param.MapToEntity<FunctionVeinTag>();
            return Insert<FunctionVeinTag>(VeinTag) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        /// <summary>
        /// 分配标签
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string AllotTag(RequestVeinTag Param)
        {
            //计算个数
            Param.TotalNo = (int)(Param.EndSerialNo - Param.StarSerialNo) + 1;
            EnterpriseVeinTag Tag = Param.MapToEntity<EnterpriseVeinTag>();
            Tag.AcceptNo = "A" + DateTime.Now.ToString("yyyyMMddHHmmss");
            Tag.CompanyId = Guid.Parse(Param.AcceptUser);
            FunctionVeinTagAttach Attach = Param.MapToEntity<FunctionVeinTagAttach>();
            //查询主表当权限等级为全国以上
            var Master = Kily.Set<FunctionVeinTag>().Where(t => t.IsDelete == false).Where(t => t.BatchNo == Param.BatchNo).FirstOrDefault();
            //查询字表当权限等级为全国以下
            var Customer = Kily.Set<FunctionVeinTagAttach>().Where(t => t.IsDelete == false).Where(t => t.SingleBatchNo == Param.BatchNo).FirstOrDefault();
            if (Customer != null)
                if (!Customer.IsAccept)
                    return "请先签收";
            //检测企业有没有分配记录
            var Sum = Kily.Set<EnterpriseVeinTag>().Where(t => t.IsDelete == false).Where(t => t.BatchNo == Param.BatchNo).AsNoTracking().Select(t => t.TotalNo).Sum();
            //检测运营商有没有分配记录
            var SumNo = Kily.Set<FunctionVeinTagAttach>().Where(t => t.IsDelete == false).Where(t => t.BatchNo == Param.BatchNo).AsNoTracking().Select(t => t.TotalNo).Sum();
            //检测运营商有没有在往下级分配记录
            var SumAttach = Customer != null ? Kily.Set<FunctionVeinTagAttach>().Where(t => t.IsDelete == false).Where(t => t.BatchNo == Customer.SingleBatchNo).Select(t => t.TotalNo).Sum() : 0;
            //判断输入的开始号段是否已经被分配了
            long NewStarNo = (UserInfo().AccountType <= AccountEnum.Country) ? (Master.StarSerialNo + Sum + SumNo) : (Customer.StarSerialNo + Sum + SumAttach);
            if (Param.StarSerialNo - NewStarNo < 0)
                return $"当前起始号段已经被分配，新起始号段从{NewStarNo}开始";
            if (UserInfo().AccountType <= AccountEnum.Country)
            {
                Master.AllotNum += Param.TotalNo;
                Master.AllotType = Param.AllotType;
                Master.AcceptUser = Param.AcceptUser;
                Master.AcceptUserName = Param.AcceptUserName;
                if (Master.AllotNum > Master.TotalNo)
                    return $"当前纹理二维码配额已用完";
                IList<String> Fields = new List<String> { "AllotNum", "AcceptUser", "AcceptUserName", "AllotType" };
                UpdateField<FunctionVeinTag>(Master, null, Fields);
                if (Param.AllotType == 1)
                    return Insert<EnterpriseVeinTag>(Tag) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
                else
                    return Insert<FunctionVeinTagAttach>(Attach) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
            else
            {
                Customer.AllotNum += Param.TotalNo;
                Customer.AllotType = Param.AllotType;
                Customer.AcceptUserName = Param.AcceptUserName;
                IList<String> Fields = new List<String> { "AllotNum", "AcceptUserName", "AllotType" };
                if (Customer.AllotNum > Customer.TotalNo)
                    return $"当前纹理二维码配额已用完";
                UpdateField<FunctionVeinTagAttach>(Customer, null, Fields);
                if (Param.AllotType == 1)
                    return Insert<EnterpriseVeinTag>(Tag) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
                else
                    return Insert<FunctionVeinTagAttach>(Attach) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
        }

        /// <summary>
        /// 删除二维码
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveTag(Guid Id)
        {
            if (Delete<FunctionVeinTag>(t => t.Id == Id))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }

        /// <summary>
        /// 根据接收类型选取接受人
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public IList<ResponseVienTagPreson> GetAcceptUser(int flag)
        {
            if (flag == 1)
            {
                IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>().Where(t => t.AuditType == AuditEnum.AuditSuccess).AsNoTracking();
                if (UserInfo().AccountType == AccountEnum.Admin || UserInfo().AccountType == AccountEnum.Country)
                    return queryable.Select(t => new ResponseVienTagPreson()
                    {
                        Id = t.Id,
                        Name = t.CompanyName
                    }).ToList();
                else
                    return queryable.Where(t => t.TypePath.Contains(UserInfo().Province)).Select(t => new ResponseVienTagPreson()
                    {
                        Id = t.Id,
                        Name = t.CompanyName
                    }).ToList();
            }
            else
            {
                IQueryable<SystemAdmin> queryable = Kily.Set<SystemAdmin>().Where(t => t.IsDelete == false).AsNoTracking();
                if (UserInfo().AccountType == AccountEnum.Province)
                    queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().Province)).Where(t => t.AccountType != AccountEnum.Admin && t.AccountType != AccountEnum.Country);
                if (UserInfo().AccountType == AccountEnum.City)
                    queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().City));
                if (UserInfo().AccountType == AccountEnum.Area)
                    queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().Area));
                if (UserInfo().AccountType == AccountEnum.Village)
                    queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().Town));
                return queryable.Select(t => new ResponseVienTagPreson()
                {
                    Id = t.Id,
                    Name = t.CompanyName
                }).ToList();
            }
        }

        /// <summary>
        /// 获取批次列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseVeinTag> GetTagBatchList()
        {
            IQueryable<FunctionVeinTag> queryable = Kily.Set<FunctionVeinTag>().AsNoTracking().OrderByDescending(t => t.CreateTime).Where(t => t.IsDelete == false);
            IQueryable<FunctionVeinTagAttach> queryables = Kily.Set<FunctionVeinTagAttach>().AsNoTracking().OrderByDescending(t => t.CreateTime).Where(t => t.IsDelete == false);
            if (UserInfo().AccountType == AccountEnum.Admin || UserInfo().AccountType == AccountEnum.Country)
                return queryable.Where(t => t.TotalNo - t.AllotNum > 0).Select(t => new ResponseVeinTag()
                {
                    BatchNo = t.BatchNo,
                    TotalNo = t.TotalNo - t.AllotNum
                }).ToList();
            return queryables.Where(t => t.TotalNo - t.AllotNum > 0).Where(t => t.AcceptUser.Contains(UserInfo().Id.ToString())).Select(t => new ResponseVeinTag()
            {
                SingleBatchNo = t.SingleBatchNo,
                TotalNo = t.TotalNo - t.AllotNum
            }).ToList();
        }

        /// <summary>
        /// 签收
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string AcceptTag(Guid Id)
        {
            FunctionVeinTagAttach VeinTag = Kily.Set<FunctionVeinTagAttach>().Where(t => t.Id == Id).FirstOrDefault();
            VeinTag.IsAccept = true;
            VeinTag.AcceptTime = DateTime.Now;
            if (UpdateField<FunctionVeinTagAttach>(VeinTag, "IsAccept"))
                return ServiceMessage.UPDATESUCCESS;
            else
                return ServiceMessage.UPDATEFAIL;
        }

        public bool IsVenTag(int Param)
        {
            var data = Kily.Set<EnterpriseTagAttach>().Where(t => t.StarSerialNo <= Param && t.EndSerialNo >= Param).FirstOrDefault();
            return data == null ? false : true;
        }

        #endregion 纹理二维码

        #region 系统字典

        /// <summary>
        /// 获取系统字典分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseDictionary> GetSysDicPage(PageParamList<RequestDictionary> pageParam)
        {
            IQueryable<FunctionDictionary> queryable = Kily.Set<FunctionDictionary>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime).AsNoTracking();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.DicName))
                queryable = queryable.Where(t => t.DicName.Contains(pageParam.QueryParam.DicName));
            var data = queryable.Select(t => new ResponseDictionary()
            {
                Id = t.Id,
                DicName = t.DicName,
                DicValue = t.DicValue
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 获取字典详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseDictionary GetDicDetail(Guid Id)
        {
            var data = Kily.Set<FunctionDictionary>().Where(t => t.Id == Id).Select(t => new ResponseDictionary()
            {
                Id = t.Id,
                DicName = t.DicName,
                DicValue = t.DicValue,
                DicDescript = t.DicDescript,
                AttachInfo = t.AttachInfo
            }).FirstOrDefault();
            return data;
        }

        /// <summary>
        /// 编辑系统字典
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string DicEdit(RequestDictionary Param)
        {
            FunctionDictionary Dic = Param.MapToEntity<FunctionDictionary>();
            if (Param.Id == Guid.Empty)
                return Insert<FunctionDictionary>(Dic) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            else
                return Update<FunctionDictionary, RequestDictionary>(Dic, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveDic(Guid Id)
        {
            return Remove<FunctionDictionary>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        #endregion 系统字典

        #region 区域码表

        /// <summary>
        /// 区域码表分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseAreaDictionary> GetAreaDicPage(PageParamList<RequestAreaDictionary> pageParam)
        {
            IQueryable<FunctionAreaDictionary> queryable = Kily.Set<FunctionAreaDictionary>().OrderByDescending(t => t.CreateTime).AsNoTracking();
            IQueryable<FunctionDisDictionary> queryables = Kily.Set<FunctionDisDictionary>().AsNoTracking();
            if (UserInfo().AccountType != AccountEnum.Admin && UserInfo().AccountType != AccountEnum.Country)
            {
                queryable = queryable.Where(t => t.ProvinceId.Contains(UserInfo().Province));
                queryables = queryables.Where(t => t.ProvinceId.Equals(UserInfo().Province));
            }
            var data = queryable.Join(Kily.Set<FunctionDictionary>(), t => t.DictionaryId, x => x.Id, (t, x) => new ResponseAreaDictionary()
            {
                DicName = x.DicName,
                DicValue = x.DicValue,
                Id = t.Id,
                AttachArea = string.Join("*", Kily.Set<SystemProvince>().Where(o => t.ProvinceId.Contains(o.Id.ToString())).Select(o => o.Name).ToArray()),
                ProvinceKeyValue = Kily.Set<SystemProvince>().Where(o => t.ProvinceId.Contains(o.Id.ToString())).ToDictionary(o => o.Id.ToString(), o => o.Name),
                DisArea = string.Join("*", queryables.Where(m => m.AreaDicId == t.Id).Select(m => m.ProvinceId).ToArray()),
                IsEnable = (queryables.Where(o => o.AreaDicId == t.Id).Select(o => o.IsEnable).FirstOrDefault() == null ? false : queryables.Where(o => o.AreaDicId == t.Id).Select(o => o.IsEnable).FirstOrDefault())
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 分配区域码表
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string RecordAreaDic(RequestAreaDictionary Param)
        {
            FunctionAreaDictionary dictionary = Param.MapToEntity<FunctionAreaDictionary>();
            if (Param.Id == Guid.Empty)
                return Insert<FunctionAreaDictionary>(dictionary) ? ServiceMessage.HANDLESUCCESS : ServiceMessage.HANDLEFAIL;
            else
                return Update<FunctionAreaDictionary, RequestAreaDictionary>(dictionary, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }

        /// <summary>
        /// 启用或禁用
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string IsEnable(RequestDisDictionary Param)
        {
            FunctionDisDictionary data = Kily.Set<FunctionDisDictionary>().Where(t => t.AreaDicId == Param.AreaDicId).AsNoTracking().FirstOrDefault();
            if (data == null)
            {
                FunctionDisDictionary dictionary = Param.MapToEntity<FunctionDisDictionary>();
                return Insert(dictionary) ? ServiceMessage.HANDLESUCCESS : ServiceMessage.HANDLEFAIL;
            }
            else
            {
                return Remove<FunctionDisDictionary>(t => t.AreaDicId == Param.AreaDicId && t.ProvinceId == Param.ProvinceId) ? ServiceMessage.HANDLESUCCESS : ServiceMessage.HANDLEFAIL;
            }
        }

        /// <summary>
        /// 获取版本码表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IList<ResponseAreaDictionary> GetAreaVersion(string TypePaths, int Param)
        {
            IQueryable<FunctionDictionary> queryable = Kily.Set<FunctionDictionary>().Where(t => t.DicName.Contains("版"));
            IQueryable<FunctionDisDictionary> queryables = Kily.Set<FunctionDisDictionary>();
            string TypePath = TypePaths.Split(',')[0];
            if (CompanyInfo() != null)
            {
                if ((CompanyEnum)Param == CompanyEnum.Plant)
                    queryable = queryable.Where(t => t.DicName.Contains("种植"));
                if ((CompanyEnum)Param == CompanyEnum.Culture)
                    queryable = queryable.Where(t => t.DicName.Contains("养殖"));
                if ((CompanyEnum)Param == CompanyEnum.Production)
                    queryable = queryable.Where(t => t.DicName.Contains("生产"));
                if ((CompanyEnum)Param == CompanyEnum.Circulation)
                    queryable = queryable.Where(t => t.DicName.Contains("流通"));
                if ((CompanyEnum)Param == CompanyEnum.Other)
                    queryable = queryable.Where(t => t.DicName.Contains("其他"));
                queryable = queryable.Where(t => !t.DicName.Contains("餐饮") && !t.DicName.Contains("单位"));
            }
            else
            {
                if ((MerchantEnum)Param == MerchantEnum.Normal)
                    queryable = queryable.Where(t => t.DicName.Contains("餐饮"));
                if ((MerchantEnum)Param == MerchantEnum.UnitCanteen)
                    queryable = queryable.Where(t => t.DicName.Contains("单位"));
                if ((MerchantEnum)Param != MerchantEnum.Normal && (MerchantEnum)Param != MerchantEnum.UnitCanteen)
                    queryable = queryable.Where(t => t.DicName.Contains("三小"));
                queryable = queryable.Where(t => !t.DicName.Contains("生产")
                  && !t.DicName.Contains("种植")
                  && !t.DicName.Contains("流通")
                  && !t.DicName.Contains("养殖")
                  && !t.DicName.Contains("其他"));
            }
            var data = Kily.Set<FunctionAreaDictionary>().Where(t => t.ProvinceId.Contains(TypePath))
                  .Join(queryable, t => t.DictionaryId, x => x.Id, (t, x) => new { t.Id, x })
                  .GroupJoin(queryables, m => m.Id, n => n.AreaDicId, (m, n) => new { m.x, n.FirstOrDefault().IsEnable })
                  .Select(t => new ResponseAreaDictionary()
                  {
                      Id = t.x.Id,
                      DicName = t.x.DicName,
                      DicValue = t.x.DicValue,
                      AttachInfo = t.x.AttachInfo,
                      DicDescript = t.x.DicDescript,
                      IsEnable = (t.IsEnable == null ? false : t.IsEnable)
                  }).ToList().Where(t => t.IsEnable == false).ToList();
            return data;
        }

        /// <summary>
        /// 获取功能描述
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GetVersionById(Guid Id)
        {
            return Kily.Set<FunctionDictionary>().Where(t => t.Id == Id).Select(t => t.DicDescript).FirstOrDefault();
        }

        /// <summary>
        /// 获取分配详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseAreaDic GetAreaDicDetail(Guid Id)
        {
            return Kily.Set<FunctionAreaDictionary>().Where(t => t.DictionaryId == Id).Select(t => new ResponseAreaDic()
            {
                Id = t.Id,
                ProvinceId = t.ProvinceId,
                ProvinceName = string.Join(",", Kily.Set<SystemProvince>().Where(x => t.ProvinceId.Contains(x.Id.ToString())).Select(x => x.Name).ToList()),
                DictionaryId = t.DictionaryId,
            }).FirstOrDefault();
        }

        #endregion 区域码表

        #region 数据统计

        /// <summary>
        /// 饼状统计图
        /// </summary>
        /// <returns></returns>
        public ResponseDataCount GetPieData()
        {
            //需要统计入住的企业、餐饮商家、乡村厨师
            //第一步判断权限
            //第二步判断所在区域
            //第三步根据类型分组
            //审核通过
            List<EnterpriseInfo> enterprises = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false).Where(t => !string.IsNullOrEmpty(t.TypePath)).ToList();
            List<RepastInfo> repasts = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false).Where(t => !string.IsNullOrEmpty(t.TypePath)).ToList();
            //不等于审核通过
            List<EnterpriseInfo> enterprise = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false).Where(t => !string.IsNullOrEmpty(t.TypePath)).ToList();
            List<RepastInfo> repast = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false).Where(t => !string.IsNullOrEmpty(t.TypePath)).ToList();
            IList<DataPie> InSideData = null;
            IList<DataPie> OutSideData = null;
            if (UserInfo().AccountType == AccountEnum.Province)
            {
                //企业
                enterprises = enterprises.Where(t => t.AuditType == AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Province)).ToList();
                //商家
                repasts = repasts.Where(t => t.AuditType == AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Province)).ToList();
                //企业
                enterprise = enterprise.Where(t => t.AuditType != AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Province)).ToList();
                //商家
                repast = repast.Where(t => t.AuditType != AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Province)).ToList();
            }
            else if (UserInfo().AccountType == AccountEnum.City)
            {
                //企业
                enterprises = enterprises.Where(t => t.AuditType == AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().City)).ToList();
                //商家
                repasts = repasts.Where(t => t.AuditType == AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Area)).ToList();
                //企业
                enterprise = enterprise.Where(t => t.AuditType != AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().City)).ToList();
                //商家
                repast = repast.Where(t => t.AuditType != AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Area)).ToList();
            }
            else if (UserInfo().AccountType == AccountEnum.Area)
            {
                //企业
                enterprises = enterprises.Where(t => t.AuditType == AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Area)).ToList();
                //商家
                repasts = repasts.Where(t => t.AuditType == AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Area)).ToList();
                //企业
                enterprise = enterprise.Where(t => t.AuditType != AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Area)).ToList();
                //商家
                repast = repast.Where(t => t.AuditType != AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Area)).ToList();
            }
            else if (UserInfo().AccountType == AccountEnum.Village)
            {
                //企业
                enterprise = enterprises.Where(t => t.AuditType != AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Town)).ToList();
                //商家
                repast = repasts.Where(t => t.AuditType != AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Town)).ToList();
                //企业
                enterprise = enterprise.Where(t => t.AuditType != AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Town)).ToList();
                //商家
                repast = repast.Where(t => t.AuditType != AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Town)).ToList();
            }
            else
            {
                //企业
                enterprises = enterprises.Where(t => t.AuditType == AuditEnum.AuditSuccess).ToList();
                //商家
                repasts = repasts.Where(t => t.AuditType == AuditEnum.AuditSuccess).ToList();
                //企业
                enterprise = enterprise.Where(t => t.AuditType != AuditEnum.AuditSuccess).ToList();
                //商家
                repast = repast.Where(t => t.AuditType != AuditEnum.AuditSuccess).ToList();
            }
            //外环
            OutSideData = enterprises.GroupBy(t => t.CompanyType).Select(t => new DataPie
            {
                value = t.Count(),
                name = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.Key)
            }).ToList();
            repasts.GroupBy(t => t.DiningType).Select(t => new DataPie
            {
                value = t.Count(),
                name = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.Key)
            }).ToList().ForEach(t => { OutSideData.Add(t); });
            //内环
            InSideData = enterprise.GroupBy(t => t.CompanyType).Select(t => new DataPie
            {
                value = t.Count(),
                name = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.Key)
            }).ToList();
            repast.GroupBy(t => t.DiningType).Select(t => new DataPie
            {
                value = t.Count(),
                name = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.Key)
            }).ToList().ForEach(t => { InSideData.Add(t); });
            List<String> title = new List<String>() { "种植企业", "养殖企业", "生产企业", "流通企业", "其他企业", "餐饮企业", "单位食堂", "小经营店", "小作坊", "小摊贩" };
            ResponseDataCount dataCount = new ResponseDataCount()
            {
                Type = true,
                DataTitle = title,
                InSideData = InSideData,
                OutSideData = OutSideData
            };
            return dataCount;
        }

        /// <summary>
        /// 柱状统计图
        /// </summary>
        /// <returns></returns>
        public ResponseDataCount GetBarData()
        {
            //统计周一至周日的合同数量
            //第一步判断权限
            //第二步判断所在区域
            //第三步根据类型分组
            List<SystemStayContract> contracts = Kily.Set<SystemStayContract>().Where(t => t.IsDelete == false).ToList();
            if (UserInfo().AccountType == AccountEnum.Province)
                contracts = contracts.Where(t => t.TypePath.Contains(UserInfo().Province)).ToList();
            else if (UserInfo().AccountType == AccountEnum.City)
                contracts = contracts.Where(t => t.TypePath.Contains(UserInfo().City)).ToList();
            else if (UserInfo().AccountType == AccountEnum.Area)
                contracts = contracts.Where(t => t.TypePath.Contains(UserInfo().Area)).ToList();
            else if (UserInfo().AccountType == AccountEnum.Village)
                contracts = contracts.Where(t => t.TypePath.Contains(UserInfo().Town)).ToList();
            DataBar bar1 = contracts.Select(t => new DataBar()
            {
                name = "版本类型",
                data = contracts.GroupBy(x => x.VersionType).Select(x => x.Count()).ToList()
            }).FirstOrDefault();
            DataBar bar2 = contracts.Select(t => new DataBar()
            {
                name = "企业合同",
                data = contracts.GroupBy(x => new { x.VersionType, x.EnterpriseOrMerchant }).Where(x => x.Key.EnterpriseOrMerchant == 1).Select(x => x.Count()).ToList()
            }).FirstOrDefault();
            DataBar bar3 = contracts.Select(t => new DataBar()
            {
                name = "商家合同",
                data = contracts.GroupBy(x => new { x.VersionType, x.EnterpriseOrMerchant }).Where(x => x.Key.EnterpriseOrMerchant == 2).Select(x => x.Count()).ToList()
            }).FirstOrDefault();
            DataBar bar4 = contracts.Select(t => new DataBar()
            {
                name = "未审核",
                data = contracts.GroupBy(x => new { x.AuditType, x.VersionType }).Where(x => x.Key.AuditType == AuditEnum.AduitFail).Select(x => x.Count()).ToList()
            }).FirstOrDefault();
            DataBar bar5 = contracts.Select(t => new DataBar()
            {
                name = "已审核",
                data = contracts.GroupBy(x => new { x.AuditType, x.VersionType }).Where(x => x.Key.AuditType == AuditEnum.AuditSuccess).Select(x => x.Count()).ToList()
            }).FirstOrDefault();
            List<String> title = new List<String>() { "版本类型", "企业合同", "商家合同", "未审核", "已审核" };
            ResponseDataCount dataCount = new ResponseDataCount
            {
                DataTitle = title,
                Type = false,
                BarData = new List<DataBar> { bar1, bar2, bar3, bar4, bar5 }
            };
            return dataCount;
        }

        /// <summary>
        /// 获取首页企业统计
        /// </summary>
        /// <returns></returns>
        public Object GetStatistics()
        {
            List<EnterpriseInfo> enterprises = Kily.Set<EnterpriseInfo>().Where(t => !string.IsNullOrEmpty(t.TypePath)).Where(t => t.IsDelete == false && t.AuditType == AuditEnum.AuditSuccess).ToList();
            List<RepastInfo> repasts = Kily.Set<RepastInfo>().Where(t => !string.IsNullOrEmpty(t.TypePath)).Where(t => t.IsDelete == false && t.AuditType == AuditEnum.AuditSuccess).ToList();
            List<ResponseProvince> Temp = null;
            List<int> PlantCount = new List<int>();
            List<int> ProCount = new List<int>();
            List<int> MoveCount = new List<int>();
            List<int> FoodCount = new List<int>();
            List<int> UnitCount = new List<int>();
            List<int> SmallCount = new List<int>();
            if (UserInfo().AccountType <= AccountEnum.Country)
                Temp = Kily.Set<SystemProvince>().Select(t => new ResponseProvince
                {
                    Id = t.Id,
                    ProvinceName = t.Name,
                }).ToList();
            else if (UserInfo().AccountType == AccountEnum.Province)
            {
                int Code = Kily.Set<SystemProvince>().Where(t => t.Id.ToString() == UserInfo().Province).Select(t => t.Code).FirstOrDefault();
                Temp = Kily.Set<SystemCity>().Where(t => t.ProvinceCode == Code).Select(t => new ResponseProvince
                {
                    Id = t.Id,
                    ProvinceName = t.Name,
                }).ToList();
            }
            else if (UserInfo().AccountType == AccountEnum.City)
            {
                int Code = Kily.Set<SystemCity>().Where(t => t.Id.ToString() == UserInfo().City).Select(t => t.Code).FirstOrDefault();
                Temp = Kily.Set<SystemArea>().Where(t => t.CityCode == Code).Select(t => new ResponseProvince
                {
                    Id = t.Id,
                    ProvinceName = t.Name,
                }).ToList();
            }
            else if (UserInfo().AccountType == AccountEnum.Area)
            {
                int Code = Kily.Set<SystemCity>().Where(t => t.Id.ToString() == UserInfo().Area).Select(t => t.Code).FirstOrDefault();
                Temp = Kily.Set<SystemTown>().Where(t => t.AreaCode == Code).Select(t => new ResponseProvince
                {
                    Id = t.Id,
                    ProvinceName = t.Name,
                }).ToList();
            }
            else
                return null;
            Temp.ForEach(x =>
            {
                PlantCount.Add(enterprises.Where(t => t.CompanyType <= CompanyEnum.Culture).Where(t => t.TypePath.Contains(x.Id.ToString())).Count());
                ProCount.Add(enterprises.Where(t => t.CompanyType == CompanyEnum.Production).Where(t => t.TypePath.Contains(x.Id.ToString())).Count());
                MoveCount.Add(enterprises.Where(t => t.CompanyType == CompanyEnum.Circulation).Where(t => t.TypePath.Contains(x.Id.ToString())).Count());
                FoodCount.Add(repasts.Where(t => t.DiningType == MerchantEnum.Normal).Where(t => t.TypePath.Contains(x.Id.ToString())).Count());
                UnitCount.Add(repasts.Where(t => t.DiningType == MerchantEnum.UnitCanteen).Where(t => t.TypePath.Contains(x.Id.ToString())).Count());
                SmallCount.Add(repasts.Where(t => t.DiningType > MerchantEnum.UnitCanteen).Where(t => t.TypePath.Contains(x.Id.ToString())).Count());
            });
            Object obj = new { Temp, PlantCount, ProCount, MoveCount, FoodCount, UnitCount, SmallCount };
            return obj;
        }

        /// <summary>
        /// 获取生成的二维码
        /// </summary>
        /// <returns></returns>
        public Object GetCreateTagList()
        {
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>().Where(t => !string.IsNullOrEmpty(t.TypePath)).Where(t => t.AuditType == AuditEnum.AuditSuccess);
            if (UserInfo().AccountType == AccountEnum.Province)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().Province));
            if (UserInfo().AccountType == AccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().City));
            IQueryable<EnterpriseTag> tags = Kily.Set<EnterpriseTag>();
            var data = queryable.ToList().Join(tags.ToList(), t => t.Id, x => x.CompanyId, (t, x) => new { t.CompanyType, x.TotalNo }).GroupBy(t => t.CompanyType).Select(t => new
            {
                Sum = t.Sum(x => x.TotalNo),
                CompanyType = t.Key
            }).ToList();
            return data;
        }

        #endregion 数据统计

        #region 系统消息

        /// <summary>
        /// 消息中心
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseSystemMessage> GetMsgPage(PageParamList<Object> pageParam)
        {
            IQueryable<SystemMessage> queryable = Kily.Set<SystemMessage>().OrderByDescending(t => t.CreateTime);
            if (UserInfo().AccountType == AccountEnum.Province)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().Province));
            if (UserInfo().AccountType == AccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().City));
            if (UserInfo().AccountType == AccountEnum.Area)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().Area));
            var data = queryable.Select(t => new ResponseSystemMessage()
            {
                Id = t.Id,
                MsgName = t.MsgName,
                MsgContent = t.MsgContent
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveMsg(Guid Id)
        {
            return Remove<SystemMessage>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }

        #endregion 系统消息

        #region 定时提醒合同

        /// <summary>
        /// 合同提醒
        /// </summary>
        /// <returns></returns>
        public String NotifyContract()
        {
            IQueryable<SystemStayContract> queryable = Kily.Set<SystemStayContract>().Where(t => t.EnterpriseOrMerchant == 1).Where(t => t.AuditType == AuditEnum.AuditSuccess);
            if (UserInfo().AccountType <= AccountEnum.Country)
                queryable = queryable.Where(t => t.EndTime.Month - DateTime.Now.Month <= 1).Where(t => t.EndTime.Month - DateTime.Now.Month > 0);
            if (UserInfo().AccountType == AccountEnum.Province)
                queryable = queryable.Where(t => t.EndTime.Month - DateTime.Now.Month <= 1).Where(t => t.EndTime.Month - DateTime.Now.Month > 0).Where(t => t.TypePath.Contains(UserInfo().Province));
            if (UserInfo().AccountType <= AccountEnum.City)
                queryable = queryable.Where(t => t.EndTime.Month - DateTime.Now.Month <= 1).Where(t => t.EndTime.Month - DateTime.Now.Month > 0).Where(t => t.TypePath.Contains(UserInfo().City));
            if (UserInfo().AccountType <= AccountEnum.Area)
                queryable = queryable.Where(t => t.EndTime.Month - DateTime.Now.Month <= 1).Where(t => t.EndTime.Month - DateTime.Now.Month > 0).Where(t => t.TypePath.Contains(UserInfo().Area));
            else
                queryable = queryable.Where(t => t.EndTime.Month - DateTime.Now.Month <= 1).Where(t => t.EndTime.Month - DateTime.Now.Month > 0).Where(t => t.TypePath.Contains(UserInfo().Town));
            return string.Join(",", queryable.Select(t => t.CompanyId).ToList());
        }

        /// <summary>
        /// 合同导出
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Object NofityCompany(string Id)
        {
            var data = Kily.Set<EnterpriseInfo>().Where(t => Id.Contains(t.Id.ToString())).Where(t => t.AuditType == AuditEnum.AuditSuccess)
                .Select(t => new
                {
                    t.CompanyName,
                    t.CompanyPhone,
                    t.SafeOffer,
                    t.CompanyAddress
                }).ToList();
            return data;
        }

        #endregion 定时提醒合同
    }
}