using KilyCore.DataEntity.RequestMapper.Dining;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Dining;
using KilyCore.EntityFrameWork.Model.Dining;
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

namespace KilyCore.Service.ServiceCore
{
    public class DiningService : Repository, IDiningService
    {
        #region 商家中心
        /// <summary>
        /// 启用账号
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string EnableAccount(Guid Id)
        {
            DiningInfo info = Kily.Set<DiningInfo>().Where(t => t.Id == Id).FirstOrDefault();
            info.IsEnable = true;
            if (UpdateField<DiningInfo>(info, "IsEnable"))
                return ServiceMessage.HANDLESUCCESS;
            else
                return ServiceMessage.HANDLEFAIL;
        }
        /// <summary>
        /// 商家分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseMerchant> GetMerchantPage(PageParamList<RequestMerchant> pageParam)
        {
            IQueryable<DiningInfo> queryable = Kily.Set<DiningInfo>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.MerchantName))
                queryable = queryable.Where(t => t.MerchantName.Contains(pageParam.QueryParam.MerchantName));
            if (!string.IsNullOrEmpty(pageParam.QueryParam.AreaTree))
                queryable = queryable.Where(t => t.TypePath.Contains(pageParam.QueryParam.AreaTree));
            if (UserInfo().AccountType == AccountEnum.Province)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().Province));
            if (UserInfo().AccountType == AccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().City));
            if (UserInfo().AccountType == AccountEnum.Area)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().Area));
            if (UserInfo().AccountType == AccountEnum.Village)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().Town));
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseMerchant()
            {
                Id = t.Id,
                CommunityCode = t.CommunityCode,
                Account = t.Account,
                PassWord = t.PassWord,
                MerchantName = t.MerchantName,
                DiningTypeName = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType),
                DingRoleId = t.DingRoleId,
                Phone = t.Phone,
                Email = t.Email,
                TypePath = t.TypePath,
                IsEnable = t.IsEnable
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 获取商家详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseMerchant GetMerchantDetail(Guid Id)
        {
            IQueryable<DiningInfo> queryable = Kily.Set<DiningInfo>().Where(t => t.Id == Id);
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseMerchant()
            {
                Id = t.Id,
                CommunityCode = t.CommunityCode,
                Account = t.Account,
                PassWord = t.PassWord,
                MerchantName = t.MerchantName,
                DiningTypeName = AttrExtension.GetSingleDescription<MerchantEnum, DescriptionAttribute>(t.DiningType),
                DingRoleId = t.DingRoleId,
                Phone = t.Phone,
                Email = t.Email,
                TypePath = t.TypePath,
                IsEnable = t.IsEnable
            }).FirstOrDefault();
            return data;
        }
        #endregion
        #region 资料审核
        /// <summary>
        /// 审核商家资料
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string AuditMerchant(RequestAudit Param)
        {
            Param.AuditName = UserInfo().TrueName;
            SystemAudit Audit = Param.MapToEntity<SystemAudit>();
            if (Insert<SystemAudit>(Audit))
            {
                DiningInfo Info = Kily.Set<DiningInfo>().Where(t => t.IsDelete == false).Where(t => t.Id == Param.TableId).FirstOrDefault();
                Info.AuditType = Param.AuditType;
                if (UpdateField<DiningInfo>(Info, "AuditType"))
                    return ServiceMessage.HANDLESUCCESS;
                else
                    return ServiceMessage.HANDLEFAIL;
            }
            else
                return ServiceMessage.INSERTFAIL;
        }
        #endregion
    }
}
