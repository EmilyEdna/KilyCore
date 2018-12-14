using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Cook;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Cook;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.Model.Cook;
using KilyCore.EntityFrameWork.Model.System;
using KilyCore.EntityFrameWork.ModelEnum;
using KilyCore.Extension.AttributeExtension;
using KilyCore.Extension.AutoMapperExtension;
using KilyCore.Extension.PayCore.AliPay;
using KilyCore.Extension.PayCore.WxPay;
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

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：CookServiceWeb
* 类 描 述 ：
* 命名空间 ：KilyCore.Service.ServiceCore
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/24 15:08:18
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.Service.ServiceCore
{
    public class CookServiceWeb : Repository, ICookWebService
    {
        #region 登录注册
        /// <summary>
        /// 厨师注册
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string RegistCookAccount(RequestCookInfo Param)
        {
            Param.IsVip = false;
            CookVip vip = Param.MapToEntity<CookVip>();
            CookRoleAuthor author = Kily.Set<CookRoleAuthor>().Where(t => t.IsDelete == false).Where(t => t.AuthorName.Contains("基本")).FirstOrDefault();
            vip.RoleId = author.Id;
            return Insert<CookVip>(vip) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 厨师登录
        /// </summary>
        /// <param name="LoginValidate"></param>
        /// <returns></returns>
        public ResponseCookInfo CookLogin(RequestValidate LoginValidate)
        {
            IQueryable<CookVip> queryable = Kily.Set<CookVip>()
                .Where(t => t.Account.Equals(LoginValidate.Account) || t.Phone.Equals(LoginValidate.Account))
                .Where(t => t.PassWord.Equals(LoginValidate.PassWord))
                .Where(t => t.IsDelete == false);
            IQueryable<CookInfo> queryables = Kily.Set<CookInfo>();
            var data = queryable.GroupJoin(queryables, t => t.Id, x => x.CookId, (t, x) => new ResponseCookInfo()
            {
                Id = t.Id,
                CookId = t.Id,
                Sexlab = x.FirstOrDefault().Sexlab,
                Account = t.Account,
                PassWord = t.PassWord,
                Address = x.FirstOrDefault().Address,
                Birthday = x.FirstOrDefault().Birthday,
                CardOffice = x.FirstOrDefault().CardOffice,
                ExpiredDay = x.FirstOrDefault().ExpiredDay,
                IdCardNo = x.FirstOrDefault().IdCardNo,
                Phone = t.Phone,
                TrueName = x.FirstOrDefault().TrueName,
                Nation = x.FirstOrDefault().Nation,
                TypePath = x.FirstOrDefault().TypePath,
                StartTime = t.StartTime,
                EndTime = t.EndTime,
                RoleId = t.RoleId,
                TableName = typeof(ResponseCookInfo).Name
            }).AsNoTracking().FirstOrDefault();
            return data;
        }
        #endregion

        #region 获取全局集团菜单
        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <returns></returns>
        public IList<ResponseCookMenu> GetCookMenu()
        {
            IQueryable<CookMenu> queryable = Kily.Set<CookMenu>().Where(t => t.Level == MenuEnum.LevelOne).Where(t => t.IsDelete == false).AsNoTracking().AsQueryable().OrderBy(t => t.CreateTime);
            CookRoleAuthor AuthorWeb = Kily.Set<CookRoleAuthor>().Where(t => t.IsDelete == false).
                    Where(t => t.Id == CookInfo().RoleId).AsNoTracking().FirstOrDefault();
            queryable = queryable.Where(t => AuthorWeb.AuthorMenuPath.Contains(t.Id.ToString())).AsNoTracking();
            var data = queryable.OrderBy(t => t.CreateTime).Select(t => new ResponseCookMenu()
            {
                Id = t.Id,
                MenuId = t.MenuId,
                ParentId = t.ParentId,
                MenuAddress = t.MenuAddress,
                MenuName = t.MenuName,
                HasChildrenNode = t.HasChildrenNode,
                MenuIcon = t.MenuIcon,
                MenuChildren = Kily.Set<CookMenu>()
              .Where(x => x.ParentId == t.MenuId)
              .Where(x => x.Level != MenuEnum.LevelOne)
              .Where(x => x.IsDelete == false)
              .Where(x => AuthorWeb.AuthorMenuPath.Contains(x.Id.ToString()))
              .OrderBy(x => x.CreateTime).Select(x => new ResponseCookMenu()
              {
                  Id = x.Id,
                  MenuId = x.MenuId,
                  ParentId = x.ParentId,
                  MenuAddress = x.MenuAddress,
                  MenuName = x.MenuName,
                  HasChildrenNode = x.HasChildrenNode,
                  MenuIcon = x.MenuIcon
              }).ToList()
            }).ToList();
            return data;

        }
        #endregion

        #region 账号管理
        /// <summary>
        /// 账号分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseCookInfo> GetCookVipPage(PageParamList<RequestCookInfo> pageParam)
        {
            var data = Kily.Set<CookVip>().Where(t => t.Id == CookInfo().Id).Select(t => new ResponseCookInfo()
            {
                Id = t.Id,
                Account = t.Account,
                IsVip = t.IsVip,
                StartTime = t.StartTime,
                EndTime = t.EndTime,
                Phone = t.Phone,
                Photo = t.Photo,
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 账号详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseCookInfo GetCookVipDetail(Guid Id)
        {
            var data = Kily.Set<CookVip>().Select(t => new ResponseCookInfo()
            {
                Id = t.Id,
                Account = t.Account,
                IsVip = t.IsVip,
                PassWord = t.PassWord,
                StartTime = t.StartTime,
                EndTime = t.EndTime,
                Phone = t.Phone,
                Photo = t.Photo,
                RoleId = t.RoleId
            }).FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 修改账号
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditCookVip(RequestCookInfo Param)
        {
            CookVip vip = Param.MapToEntity<CookVip>();
            if (Param.Id != Guid.Empty)
                return Update(vip, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            else
                return Insert(vip) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 开通服务
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="PayType"></param>
        /// <returns></returns>
        public ResponseStayContract OpenService(RequestStayContract Param)
        {
            RequestAliPayModel AliPayModel = new RequestAliPayModel
            {
                OrderTitle = "厨师系统会员服务",
                Money = ConfigMoney.Cook * Convert.ToInt32(Param.ContractYear)
            };
            RequestWxPayModel WxPayModel = AliPayModel.MapToEntity<RequestWxPayModel>();
            if (Param.PayType == PayEnum.Alipay)
            {
                Insert(new SystemPayInfo()
                {
                    GoodsId = Param.Id,
                    MerchantId = Param.Id,
                    PayType = Param.PayType,
                    TradeNo = AliPayCore.Instance.GetTradeNo(),
                    TagNum = Convert.ToInt32(Param.ContractYear)
                });
                return new ResponseStayContract() { PayInfoMsg = AliPayCore.Instance.WebPay(AliPayModel) };
            }
            else
            {
                Insert(new SystemPayInfo()
                {
                    GoodsId = Param.Id,
                    MerchantId = Param.Id,
                    PayType = Param.PayType,
                    TradeNo = WxPayCore.Instance.GetTradeNo(),
                    TagNum = Convert.ToInt32(Param.ContractYear)
                });
                return new ResponseStayContract() { PayInfoMsg = WxPayCore.Instance.WebPay(WxPayModel) };
            }
        }
        #endregion

        #region 厨师信息
        /// <summary>
        /// 厨师详情
        /// </summary>
        /// <returns></returns>
        public ResponseCookInfo GetCookInfoDetail()
        {
            var data = Kily.Set<CookInfo>().Where(t => t.CookId == CookInfo().Id).Select(t => new ResponseCookInfo()
            {
                Id = t.Id,
                CookId = t.CookId,
                TrueName = t.TrueName,
                Sexlab = t.Sexlab,
                Nation = t.Nation,
                Birthday = t.Birthday,
                IdCardNo = t.IdCardNo,
                TypePath = t.TypePath,
                Address = t.Address,
                CardOffice = t.CardOffice,
                ExpiredDay = t.ExpiredDay,
                IdCardPhoto = t.IdCardPhoto,
                BookInCard = t.BookInCard,
                HealthCard = t.HealthCard,
                TrainCard = t.TrainCard,
                AuditTypeName = AttrExtension.GetSingleDescription<DescriptionAttribute>(t.AuditType)
            }).AsNoTracking().FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 编辑厨师信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditCookInfo(RequestCookInfo Param)
        {
            CookInfo info = Param.MapToEntity<CookInfo>();
            if (Param.Id != Guid.Empty)
                return Update(info, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
            else
                return Insert(info) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        #endregion

        #region 办宴报备
        /// <summary>
        /// 报备列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseCookBanquet> GetBanquetPage(PageParamList<RequestCookBanquet> pageParam)
        {
            IQueryable<CookBanquet> queryable = Kily.Set<CookBanquet>().Where(t => t.CookId == CookInfo().Id).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.HoldName))
                queryable = queryable.Where(t => t.HoldName.Contains(pageParam.QueryParam.HoldName));
            var data = queryable.Select(t => new ResponseCookBanquet()
            {
                Id = t.Id,
                HoldTime = t.HoldTime,
                CreateTime = t.CreateTime,
                HoldName = t.HoldName,
                Address = t.Address,
                Stauts = t.Stauts
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 新增报备记录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditBanquet(RequestCookBanquet Param)
        {
            CookBanquet banquet = Param.MapToEntity<CookBanquet>();
            banquet.Ingredients.Split(",").ToList().ForEach(x =>
            {
                var Name = x.Split("_")[0];
                Delete<CookFood>(t => t.FoodName.Equals(Name), "IsUse", true);
            });
            return Insert(banquet) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 查看详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseCookBanquet GetBanquetDetail(Guid Id)
        {
            var data = Kily.Set<CookBanquet>().Where(t => t.Id == Id).Select(t => new ResponseCookBanquet()
            {
                Id = t.Id,
                HoldName = t.HoldName,
                Phone = t.Phone,
                TypePath = t.TypePath,
                Address = t.Address,
                HoldDay = t.HoldDay,
                HoldTime = t.HoldTime,
                HoldType = t.HoldType,
                Helper = t.Helper,
                Water = t.Water,
                Disinfection = t.Disinfection,
                Facility = t.Facility,
                Poisonous = t.Poisonous,
                Processing = t.Processing,
                Ingredients = t.Ingredients,
                CookBook = t.CookBook,
                Stauts = t.Stauts,
                ResultImg = t.ResultImg
            }).AsNoTracking().FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 删除报备信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveBanquet(Guid Id)
        {
            return Remove<CookBanquet>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        #endregion

        #region 帮厨管理
        /// <summary>
        /// 帮厨分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseCookHelper> GetHelperPage(PageParamList<RequestCookHelper> pageParam)
        {
            IQueryable<CookHelper> queryable = Kily.Set<CookHelper>().Where(t => t.CookId == CookInfo().Id).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.HelperName))
                queryable = queryable.Where(t => t.HelperName.Contains(pageParam.QueryParam.HelperName));
            var data = queryable.Select(t => new ResponseCookHelper()
            {
                Id = t.Id,
                HelperName = t.HelperName,
                TypePath = t.TypePath,
                ExpiredDate = t.ExpiredDate,
                HealthCard = t.HealthCard,
                Phone = t.Phone
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 编辑帮厨
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditHelper(RequestCookHelper Param)
        {
            CookHelper helper = Param.MapToEntity<CookHelper>();
            return Insert(helper) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 删除帮厨
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveHelper(Guid Id)
        {
            return Remove<CookHelper>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 帮厨列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseCookHelper> GetHelperList()
        {
            var data = Kily.Set<CookHelper>().Where(t => t.CookId == CookInfo().Id).OrderByDescending(t => t.CreateTime).Select(t => new ResponseCookHelper()
            {
                Id = t.Id,
                HelperName = t.HelperName,
            }).ToList();
            return data;
        }
        #endregion

        #region 食材信息
        /// <summary>
        /// 食材分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseCookFood> GetFoodPage(PageParamList<RequestCookFood> pageParam)
        {
            IQueryable<CookFood> queryable = Kily.Set<CookFood>().Where(t => t.CookId == CookInfo().Id).OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.FoodName))
                queryable = queryable.Where(t => t.FoodName.Contains(pageParam.QueryParam.FoodName));
            var data = queryable.Select(t => new ResponseCookFood()
            {
                Id = t.Id,
                FoodType = t.FoodType,
                FoodName = t.FoodName,
                Number = t.Number,
                Supplier = t.Supplier,
                ProductionAddress = t.ProductionAddress,
                BuyTime = t.BuyTime,
                Report = t.Report,
                BuyUser = t.BuyUser,
                Phone = t.Phone
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 删除食材
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveFood(Guid Id)
        {
            return Remove<CookFood>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 编辑食材
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditFood(RequestCookFood Param)
        {
            CookFood food = Param.MapToEntity<CookFood>();
            return Insert(food) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 食材列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseCookFood> GetFoodList(Guid Param)
        {
            IQueryable<CookFood> queryable = Kily.Set<CookFood>().Where(t => t.CookId == CookInfo().Id).OrderByDescending(t => t.CreateTime);
            if (Param != Guid.Empty)//传入详情
                queryable = queryable.Where(t => t.IsUse == true);
            else
                queryable = queryable.Where(t => t.IsUse == false);
            var data = queryable.Select(t => new ResponseCookFood()
            {
                Id = t.Id,
                BuyTime = t.BuyTime,
                FoodName = t.FoodName
            }).ToList();
            return data;
        }
        #endregion

        #region 数据统计
        /// <summary>
        /// 数据统计
        /// </summary>
        /// <returns></returns>
        public Object GetDataCount()
        {
            IQueryable<CookBanquet> queryable = Kily.Set<CookBanquet>().Where(t => t.CookId == CookInfo().Id).AsNoTracking();
            IQueryable<CookHelper> queryables = Kily.Set<CookHelper>().Where(t => t.CookId == CookInfo().Id).AsNoTracking();
            int Banquet = queryable.Select(t => t.Id).Count();
            int Helper = queryables.Select(t => t.Id).Count();
            return new { Banquet, Helper };
        }
        #endregion

        #region 微信支付
        /// <summary>
        /// 查询微信支付
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string WxQueryPay(Guid Param)
        {
            SystemPayInfo PayInfo = Kily.Set<SystemPayInfo>().Where(t => t.GoodsId == Param)
                .Where(t => t.PayType == PayEnum.WxPay)
                .AsNoTracking().FirstOrDefault();
            if (PayInfo != null)
            {
                String ResultCode = WxPayCore.Instance.QueryWxPay(PayInfo.TradeNo);
                if (string.IsNullOrEmpty(ResultCode))
                    return null;
                if (ResultCode.Equals("SUCCESS"))
                {
                    CookVip Info = Kily.Set<CookVip>().Where(t => t.Id == Param).FirstOrDefault();
                    PayInfo.PayDes = "SUCCESS";
                    IList<string> Fields = new List<string> { "IsVip", "StartTime", "EndTime" };
                    Info.IsVip = true;
                    Info.StartTime = DateTime.Now;
                    Info.EndTime = DateTime.Now.AddYears(Convert.ToInt32(PayInfo.TagNum));
                    if (string.IsNullOrEmpty(PayInfo.PayDes))
                    {
                        UpdateField(PayInfo, "PayDes");
                        UpdateField(Info, null, Fields);
                    }
                    return "http://main.cfdacx.com/StaticHtml/WxNotify.html";
                }
                else
                    return null;
            }
            else
                return null;
        }
        #endregion
    }
}
