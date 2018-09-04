using KilyCore.DataEntity.RequestMapper.Function;
using KilyCore.DataEntity.ResponseMapper.Function;
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
using System.Text;
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
                    Fields.Add("CityId");
                    Fields.Add("CityPrice");
                }
                if (UserInfo().AccountType == AccountEnum.City)
                {
                    Entity.AreaId = Param.AreaId;
                    Entity.AreaPrice = Param.AreaPrice;
                    Fields.Add("AreaId");
                    Fields.Add("AreaPrice");
                }
                if (UserInfo().AccountType == AccountEnum.Area)
                {
                    Entity.TownId = Param.TownId;
                    Entity.TownPrice = Param.TownPrice;
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
        #endregion
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
                    AcceptUserName = t.AcceptUserName,
                    AllotType = t.AllotType,
                    AllotNum = t.AllotNum,
                    IsAcceptName = t.IsAccept ? "已签收" : "未签收"
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
                    AcceptUserName = t.AcceptUserName,
                    AllotType = t.AllotType,
                    AllotNum = t.AllotNum,
                    IsAcceptName = t.IsAccept ? "已签收" : "未签收"
                }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
                return data;
            }
        }
        /// <summary>
        /// 录入标签
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string RecordTag(RequestVeinTag Param)
        {
            Param.TotalNo = (int)(Param.EndSerialNo - Param.StarSerialNo);
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
            Param.TotalNo = (int)(Param.EndSerialNo - Param.StarSerialNo);
            EnterpriseVeinTag Tag = Param.MapToEntity<EnterpriseVeinTag>();
            Tag.CompanyId = Guid.Parse(Param.AcceptUser);
            FunctionVeinTagAttach Attach = Param.MapToEntity<FunctionVeinTagAttach>();
            //查询主表当权限等级为全国以上
            var Master = Kily.Set<FunctionVeinTag>().Where(t => t.IsDelete == false).Where(t => t.BatchNo == Param.BatchNo).FirstOrDefault();
            //查询字表当权限等级为全国以下
            var Customer = Kily.Set<FunctionVeinTagAttach>().Where(t => t.IsDelete == false).Where(t => t.SingleBatchNo == Param.BatchNo).FirstOrDefault();
            if (Customer != null)
                if (Customer.IsAccept)
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
                if (Master.AllotNum > Master.TotalNo)
                    return $"当前纹理二维码配额已用完";
                UpdateField<FunctionVeinTag>(Master, "AllotNum");
                if (Param.AllotType == 1)
                    return Insert<EnterpriseVeinTag>(Tag) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
                else
                    return Insert<FunctionVeinTagAttach>(Attach) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
            else
            {
                Customer.AllotNum += Param.TotalNo;
                if (Customer.AllotNum > Customer.TotalNo)
                    return $"当前纹理二维码配额已用完";
                UpdateField<FunctionVeinTagAttach>(Customer, "AllotNum");
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
                    Name = t.TrueName
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
                return queryable.Select(t => new ResponseVeinTag()
                {
                    BatchNo = t.BatchNo,
                }).ToList();
            return queryables.Where(t => t.AcceptUser.Contains(UserInfo().Id.ToString())).Select(t => new ResponseVeinTag()
            {
                SingleBatchNo = t.SingleBatchNo,
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
        #endregion
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
        #endregion
        #region 区域码表
        /// <summary>
        /// 区域码表分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseAreaDictionary> GetAreaDicPage(PageParamList<RequestAreaDictionary> pageParam)
        {
            IQueryable<FunctionAreaDictionary> queryable = Kily.Set<FunctionAreaDictionary>().AsNoTracking();
            if (UserInfo().AccountType != AccountEnum.Admin && UserInfo().AccountType != AccountEnum.Country)
                queryable = queryable.Where(t => t.ProvinceId.Contains(UserInfo().Province));
            var data = queryable.Join(Kily.Set<FunctionDictionary>(), t => t.DictionaryId, x => x.Id, (t, x) => new ResponseAreaDictionary()
            {
                DicName = x.DicName,
                DicValue = x.DicValue,
                Id = t.Id,
                IsEnable = t.IsDelete
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
            return Insert<FunctionAreaDictionary>(dictionary) ? ServiceMessage.HANDLESUCCESS : ServiceMessage.HANDLEFAIL;
        }
        /// <summary>
        /// 启用或禁用
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string IsEnable(Guid Id, bool Param)
        {
            FunctionAreaDictionary dictionary = Kily.Set<FunctionAreaDictionary>().Where(t => t.Id == Id).FirstOrDefault();
            dictionary.IsDelete = Param;
            return UpdateField<FunctionAreaDictionary>(dictionary, "IsDelete") ? ServiceMessage.HANDLESUCCESS : ServiceMessage.HANDLEFAIL;
        }
        /// <summary>
        /// 获取版本码表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IList<ResponseAreaDictionary> GetAreaVersion(string TypePath, int Param)
        {
            IQueryable<FunctionDictionary> queryable = Kily.Set<FunctionDictionary>().Where(t => t.DicName.Contains("版"));
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
            }
            else
            {
                if ((MerchantEnum)Param == MerchantEnum.Normal)
                    queryable = queryable.Where(t => t.DicName.Contains("餐饮"));
                if ((MerchantEnum)Param == MerchantEnum.UnitCanteen)
                    queryable = queryable.Where(t => t.DicName.Contains("单位"));
            }
            var data = Kily.Set<FunctionAreaDictionary>().Where(t => t.ProvinceId.Contains(TypePath)).Where(t => t.IsDelete == false)
                  .Join(queryable, t => t.DictionaryId, x => x.Id, (t, x) => new ResponseAreaDictionary()
                  {
                      DicName = x.DicName,
                      DicValue = x.DicValue,
                      DicDescript = x.DicDescript,
                      IsEnable = t.IsDelete,
                      AttachInfo = x.AttachInfo
                  }).ToList();
            return data;
        }
        #endregion
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
            IQueryable<EnterpriseInfo> enterprises = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false);
            IQueryable<RepastInfo> repasts = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false);
            //不等于审核通过
            IQueryable<EnterpriseInfo> enterprise = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false);
            IQueryable<RepastInfo> repast = Kily.Set<RepastInfo>().Where(t => t.IsDelete == false);
            IList<DataPie> InSideData = null;
            IList<DataPie> OutSideData = null;
            if (UserInfo().AccountType == AccountEnum.Province)
            {
                //企业
                enterprises = enterprises.Where(t => t.AuditType == AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Province));
                //商家
                repasts = repasts.Where(t => t.AuditType == AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Province));
                //企业
                enterprise = enterprise.Where(t => t.AuditType != AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Province));
                //商家
                repast = repast.Where(t => t.AuditType != AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Province));
            }
            else if (UserInfo().AccountType == AccountEnum.City)
            {
                //企业
                enterprises = enterprises.Where(t => t.AuditType == AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().City));
                //商家
                repasts = repasts.Where(t => t.AuditType == AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Area));
                //企业
                enterprise = enterprise.Where(t => t.AuditType != AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().City));
                //商家
                repast = repast.Where(t => t.AuditType != AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Area));
            }
            else if (UserInfo().AccountType == AccountEnum.Area)
            {
                //企业
                enterprises = enterprises.Where(t => t.AuditType == AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Area));
                //商家
                repasts = repasts.Where(t => t.AuditType == AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Area));
                //企业
                enterprise = enterprise.Where(t => t.AuditType != AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Area));
                //商家
                repast = repast.Where(t => t.AuditType != AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Area));
            }
            else if (UserInfo().AccountType == AccountEnum.Village)
            {
                //企业
                enterprise = enterprises.Where(t => t.AuditType != AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Town));
                //商家
                repast = repasts.Where(t => t.AuditType != AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Town));
                //企业
                enterprise = enterprise.Where(t => t.AuditType != AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Town));
                //商家
                repast = repast.Where(t => t.AuditType != AuditEnum.AuditSuccess)
                    .Where(t => t.TypePath.Contains(UserInfo().Town));
            }
            else
            {
                //企业
                enterprises = enterprises.Where(t => t.AuditType == AuditEnum.AuditSuccess);
                //商家
                repasts = repasts.Where(t => t.AuditType == AuditEnum.AuditSuccess);
                //企业
                enterprise = enterprise.Where(t => t.AuditType != AuditEnum.AuditSuccess);
                //商家
                repast = repast.Where(t => t.AuditType != AuditEnum.AuditSuccess);
            }
            //外环
            OutSideData = enterprises.GroupBy(t => t.CompanyType).Select(t => new DataPie
            {
                value = t.Count(),
                name = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.Key)
            }).AsNoTracking().ToList();
            repasts.GroupBy(t => t.DiningType).Select(t => new DataPie
            {
                value = t.Count(),
                name = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.Key)
            }).AsNoTracking().ToList().ForEach(t => { OutSideData.Add(t); });
            //内环
            InSideData = enterprise.GroupBy(t => t.CompanyType).Select(t => new DataPie
            {
                value = t.Count(),
                name = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.Key)
            }).AsNoTracking().ToList();
            repast.GroupBy(t => t.DiningType).Select(t => new DataPie
            {
                value = t.Count(),
                name = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.Key)
            }).AsNoTracking().ToList().ForEach(t => { InSideData.Add(t); });
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
            IQueryable<SystemStayContract> contracts = Kily.Set<SystemStayContract>().Where(t => t.IsDelete == false);
            if (UserInfo().AccountType == AccountEnum.Province)
                contracts = contracts.Where(t => t.TypePath.Contains(UserInfo().Province));
            else if (UserInfo().AccountType == AccountEnum.City)
                contracts = contracts.Where(t => t.TypePath.Contains(UserInfo().City));
            else if (UserInfo().AccountType == AccountEnum.Area)
                contracts = contracts.Where(t => t.TypePath.Contains(UserInfo().Area));
            else if (UserInfo().AccountType == AccountEnum.Village)
                contracts = contracts.Where(t => t.TypePath.Contains(UserInfo().Town));
            DataBar bar1 = contracts.Select(t => new DataBar()
            {
                name = "版本类型",
                data = contracts.GroupBy(x => x.VersionType).Select(x => x.Count()).ToList()
            }).AsNoTracking().FirstOrDefault();
            DataBar bar2 = contracts.Select(t => new DataBar()
            {
                name = "企业合同",
                data = contracts.GroupBy(x => new { x.VersionType, x.EnterpriseOrMerchant }).Where(x => x.Key.EnterpriseOrMerchant == 1).Select(x => x.Count()).ToList()
            }).AsNoTracking().FirstOrDefault();
            DataBar bar3 = contracts.Select(t => new DataBar()
            {
                name = "商家合同",
                data = contracts.GroupBy(x => new { x.VersionType, x.EnterpriseOrMerchant }).Where(x => x.Key.EnterpriseOrMerchant == 2).Select(x => x.Count()).ToList()
            }).AsNoTracking().FirstOrDefault();
            DataBar bar4 = contracts.Select(t => new DataBar()
            {
                name = "未审核",
                data = contracts.GroupBy(x => new { x.AuditType, x.VersionType }).Where(x => x.Key.AuditType == AuditEnum.AduitFail).Select(x => x.Count()).ToList()
            }).AsNoTracking().FirstOrDefault();
            DataBar bar5 = contracts.Select(t => new DataBar()
            {
                name = "已审核",
                data = contracts.GroupBy(x => new { x.AuditType, x.VersionType }).Where(x => x.Key.AuditType == AuditEnum.AuditSuccess).Select(x => x.Count()).ToList()
            }).AsNoTracking().FirstOrDefault();
            List<String> title = new List<String>() { "版本类型", "企业合同", "商家合同", "未审核", "已审核" };
            ResponseDataCount dataCount = new ResponseDataCount
            {
                DataTitle = title,
                Type = false,
                BarData = new List<DataBar> { bar1, bar2, bar3, bar4, bar5 }
            };
            return dataCount;
        }
        #endregion
    }
}
