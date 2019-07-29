﻿using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.EntityFrameWork.Model.Enterprise;
using KilyCore.EntityFrameWork.Model.Repast;
using KilyCore.EntityFrameWork.ModelEnum;
using KilyCore.Extension.AutoMapperExtension;
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
        /// <summary>
        /// 查询所有人员
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public object GetAllUser(Guid CompanyId)
        {
            var Euser = Kily.Set<EnterpriseUser>().Where(t => t.CompanyId == CompanyId).Select(t => new
            {
                PersonName = t.TrueName,
                JobStatus = "在职",
                TimeLength = "",
                HealthImg = ""
            }).ToList();
            var Ruser = Kily.Set<RepastInfoUser>().Where(t => t.InfoId == CompanyId).Select(t => new
            {
                PersonName = t.TrueName,
                JobStatus = "在职",
                TimeLength = "",
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
        public object GetAllSupply(Guid CompanyId, int type)
        {
            var Types = (SellerEnum)type;
            var Esupper = Kily.Set<EnterpriseSeller>().Where(t => t.SellerType == Types)
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
                CompanyUser = "",
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
        public object GetAllSample(Guid CompanyId)
        {
            return Kily.Set<RepastSample>().Where(t => t.InfoId == CompanyId).Select(t => new
            {
                FoodsName = t.DishName,
                SavePerson = t.OperatUser,
                SaveTime = t.SampleTime.Value.ToString("yyyy年MM月dd日"),
                ReportImg = t.SampleImg,
                t.Remark
            }).ToList();
        }
        /// <summary>
        /// 废物处理
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public object RepastDuck(Guid CompanyId)
        {
            return Kily.Set<RepastDuck>().Where(t => t.InfoId == CompanyId).Select(t => new
            {
                BadType = t.HandleWays,
                BadPhone = t.Phone,
                BadTime = t.HandleTime.Value.ToString("yyyy年MM月dd日"),
                ReportImg = t.HandleImg,
                BadRemark = t.Remark,
                BadPerson = t.HandleUser
            }).ToList();
        }
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
    }
}
