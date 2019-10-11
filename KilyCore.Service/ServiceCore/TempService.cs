using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.Repast;
using KilyCore.EntityFrameWork.Model.Enterprise;
using KilyCore.EntityFrameWork.Model.Govt;
using KilyCore.EntityFrameWork.Model.Repast;
using KilyCore.EntityFrameWork.Model.System;
using KilyCore.Extension.AutoMapperExtension;
using KilyCore.Extension.ValidateExtension;
using KilyCore.Repositories.BaseRepository;
using KilyCore.Service.ConstMessage;
using KilyCore.Service.IServiceCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KilyCore.Service.ServiceCore
{
    public class TempService : Repository, ITempService
    {
        #region 弃用-中间系统

        /// <summary>
        /// 查询所有人员
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [Obsolete]
        public object GetAllUser(Guid CompanyId)
        {
            var Euser = Kily.Set<EnterpriseUser>().Where(t => t.CompanyId == CompanyId && t.IsDelete == false).Select(t => new
            {
                PersonName = t.TrueName,
                JobStatus = "在职",
                TimeLength = "",
                HealthImg = ""
            }).ToList();
            var Ruser = Kily.Set<RepastInfoUser>().Where(t => t.InfoId == CompanyId && t.IsDelete == false).Select(t => new
            {
                PersonName = t.TrueName,
                JobStatus = "在职",
                TimeLength = t.ExpiredTime.Value.ToString("yyyy年MM月dd日"),
                HealthImg = t.HealthCard
            }).ToList();
            Euser.AddRange(Ruser);
            return Euser;
        }

        /// <summary>
        /// 获取所有供应商
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [Obsolete]
        public object GetAllSupply(Guid CompanyId, int type)
        {
            //var Types = (SellerEnum)type;
            var Esupper = Kily.Set<EnterpriseSeller>().Where(t => t.SellerType.GetHashCode() == type && t.IsDelete == false)
                 .Where(t => t.CompanyId == CompanyId)
                 .Select(t => new
                 {
                     CompanyName = t.SupplierName,
                     CompanyCode = t.Code,
                     CompanyUser = t.DutyMan,
                     CompanyTel = t.LinkPhone,
                     CompanyAddress = t.Address,
                     CompanyFace = t.OkayCard
                 }).ToList();
            var Rsupper = Kily.Set<RepastSupplier>().Where(t => t.InfoId == CompanyId).Select(t => new
            {
                CompanyName = t.SupplierName,
                CompanyCode = "",
                CompanyUser = t.SupplierUser,
                CompanyTel = t.LinkPhone,
                CompanyAddress = t.Address,
                CompanyFace = t.RunCard
            }).ToList();
            Esupper.AddRange(Rsupper);
            return Esupper;
        }

        /// <summary>
        /// 获取留样
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [Obsolete]
        public object GetAllSample(Guid CompanyId)
        {
            return Kily.Set<RepastSample>().Where(t => t.InfoId == CompanyId && t.IsDelete == false).OrderByDescending(o => o.SampleTime).Select(t => new
            {
                FoodsName = t.DishName,
                SavePerson = t.OperatUser,
                SaveTime = t.SampleTime.Value.ToString("yyyy年MM月dd日"),
                ReportImg = t.SampleImg,
                t.Remark
            }).Take(12).ToList();
        }

        /// <summary>
        /// 废物处理
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [Obsolete]
        public object RepastDuck(Guid CompanyId)
        {
            return Kily.Set<RepastDuck>().Where(t => t.InfoId == CompanyId && t.IsDelete == false).OrderByDescending(o => o.HandleTime).Select(t => new
            {
                BadType = t.HandleWays,
                BadPhone = t.Phone,
                BadTime = t.HandleTime.Value.ToString("yyyy年MM月dd日"),
                ReportImg = t.HandleImg,
                BadRemark = t.Remark,
                BadPerson = t.HandleUser
            }).Take(12).ToList();
        }

        /// <summary>
        /// 食材供应
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [Obsolete]
        public object RepastThing(Guid CompanyId)
        {
            return Kily.Set<RepastBillTicket>().Where(t => t.InfoId == CompanyId && t.IsDelete == false).OrderByDescending(o => o.UpTime).Select(t => new
            {
                ThingName = t.Theme,
                Remark = t.Content.Replace("/upload/", "http://system.cfda.vip/upload/"),
                BuyTime = t.UpTime.Value
            }).Take(9).ToList();
        }

        /// <summary>
        /// 周菜谱
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [Obsolete]
        public object RepastWeek(Guid CompanyId)
        {
            return Kily.Set<RepastFoodMenu>().Where(t => t.InfoId == CompanyId && t.IsDelete == false).OrderByDescending(o => o.UpTime).Select(t => new
            {
                Title = t.FoodMenuName,
                Content = t.Content.Replace("/upload/", "http://system.cfda.vip/upload/"),
                DateTime = t.UpTime.Value
            }).Take(9).ToList();
        }

        /// <summary>
        /// 抽检信息
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [Obsolete]
        public object RepastCheck(Guid CompanyId)
        {
            return Kily.Set<RepastDraw>().Where(t => t.InfoId == CompanyId && t.IsDelete == false).OrderByDescending(o => o.DrawTime).Select(t => new
            {
                Title = t.DrawUnit,
                Person = t.DrawUser,
                Remark = t.Remark.Replace("/upload/", "http://system.cfda.vip/upload/").Replace("/editor/", "http://system.cfda.vip/editor/"),
                DateTime = t.DrawTime.Value
            }).Take(9).ToList();
        }
        /// <summary>
        /// 主营菜品
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [Obsolete]
        public object RepastFoods(Guid CompanyId)
        {
            return Kily.Set<RepastDish>().Where(t => t.InfoId == CompanyId && t.IsDelete == false).OrderByDescending(o => o.DishName).Select(t => new
            {
                Id = t.Id,
                DishName = t.DishName,
                DishType = t.DishType,
                CookingTime = t.CookingTime,
                CookingType = t.CookingType,
                MainBatch=t.MainBatch,
                Batching=t.Batching,
                Seasoning=t.Seasoning,
                FoodImg=""

            }).Take(20).ToList();
        }

        /// <summary>
        /// 陪餐打卡
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [Obsolete]
        public object RepastMarket(Guid CompanyId)
        {
            return Kily.Set<RepastUnitInsRecord>().Where(t => t.InfoId == CompanyId && t.IsDelete == false).OrderByDescending(o => o.InsTime).Select(t => new
            {
                Title = t.InsTheme,
                Person = t.InsUser,
                Remark = t.InsContent.Replace("/upload/", "http://system.cfda.vip/upload/").Replace("/editor/", "http://system.cfda.vip/editor/"),
                DateTime = t.InsTime.Value
            }).Take(9).ToList();
        }

        /// <summary>
        /// 商家自查
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [Obsolete]
        public object RepastSelfCheck(Guid CompanyId)
        {
            return Kily.Set<GovtTemplateChild>().Where(t => t.Id == CompanyId && t.IsDelete == false).OrderByDescending(o => o.CreateTime).Select(t => new
            {
                Title = t.TemplateName,
                Person = t.UpdateUser,
                Remark = t.TemplateContent.Replace("/upload/", "http://system.cfda.vip/upload/").Replace("/editor/", "http://system.cfda.vip/editor/"),
                DateTime = t.CreateTime.Value
            }).Take(9).ToList();
        }

        /// <summary>
        /// 产品信息
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [Obsolete]
        public object RepastProduct(Guid CompanyId)
        {
            return Kily.Set<RepastArticleInStock>().Where(t => t.InfoId == CompanyId)
                   .Join(Kily.Set<RepastTypeName>(), t => t.NameId, x => x.Id, (t, x) => new { t, x }).Select(t => new ResponseRepastTypeName
                   {
                       TypeNames = t.x.TypeNames,
                       Spec = t.x.Spec,
                       ProImg = t.x.ProImg,
                       Types = t.x.Types,
                       Remark = t.t.Remark
                   }).ToList();
        }
        /// <summary>
        /// 进货台账
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public object GetBuybillPage(Guid CompanyId,string SDate, string EDate)
        {
            IQueryable<RepastBuybill> queryable = Kily.Set<RepastBuybill>().Where(t => t.IsDelete == false).Where(o=>o.OrderTime>=DateTime.Parse(SDate)&&o.OrderTime<= DateTime.Parse(EDate));
            queryable = queryable.Where(t => t.InfoId == CompanyId);
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseRepastBuybill()
            {
                Id = t.Id,
                GoodsName = t.GoodsName,
                GoodsNum = t.GoodsNum,
                LinkPhone = t.LinkPhone,
                Purchase = t.Purchase,
                Unit = t.Unit,
                NoExp = t.NoExp,
                ProTime = t.ProTime,
                ToPay = t.ToPay,
                Supplier = t.Supplier,
                OrderTime = t.OrderTime,
                UnPay = t.UnPay
            }).ToList();
            return data;
        }

        #endregion 弃用-中间系统

        #region 推广活动

        /// <summary>
        /// 微信推广注册
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public object WeChatRegist(RequestEnterprise Param)
        {
            Param.InviteCode = Encoding.Default.GetString(Convert.FromBase64String(Param.InviteCode));
            EnterpriseInviteCode InviteCode = Kily.Set<EnterpriseInviteCode>().Where(t => t.InviteCode.Equals(Param.InviteCode)).FirstOrDefault();
            InviteCode.UseCompany = Param.CompanyName;
            InviteCode.UseCompanyType = Param.CompanyType;
            InviteCode.UsePhone = Param.CompanyPhone;
            InviteCode.UseTime = DateTime.Now;
            List<string> Fields = new List<string> { "UseCompany", "UseCompanyType", "UsePhone", "UseTime" };
            UpdateField(InviteCode, null, Fields);
            EnterpriseInfo Info = Param.MapToEntity<EnterpriseInfo>();
            return Insert(Info) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }

        /// <summary>
        /// 获取邀请码
        /// </summary>
        /// <param name="Area"></param>
        /// <returns></returns>
        public object GetInviteCode(string Area)
        {
            List<EnterpriseInviteCode> InviteCodes = Kily.Set<EnterpriseInviteCode>().Where(t => t.UseTypePath.Equals(Area))
                .Where(t => t.IsDelete == false)
                .Where(t => t.EffectiveSt <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
                .Where(t => t.EffectiveEt >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))).ToList();
            EnterpriseInviteCode InviteCode = InviteCodes[(new Random()).Next(InviteCodes.Count)];
            InviteCode.IsDelete = true;
            UpdateField<EnterpriseInviteCode>(InviteCode, "IsDelete");
            return Convert.ToBase64String(Encoding.Default.GetBytes(InviteCode.InviteCode));
        }

        #endregion 推广活动

        #region 生成活动码

        public string CreateInviteCode()
        {
            EnterpriseInviteCode code = new EnterpriseInviteCode();
            var Province = Kily.Set<SystemProvince>().ToList();
            foreach (var item in Province)
            {
                code.InviteCode = $"{item.Code}-{DateTime.Now.Millisecond}-{ValidateCode.CreateValidateCode()}";
                code.UseTypePath = item.Id.ToString();
                code.EffectiveSt = DateTime.Parse("2019-09-16");
                code.EffectiveEt = DateTime.Parse("2019-10-15");
                Insert(code);
            }
            return ServiceMessage.INSERTSUCCESS;
        }

        #endregion 生成活动码
    }
}