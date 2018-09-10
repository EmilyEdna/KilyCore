using KilyCore.DataEntity.RequestMapper.Govt;
using KilyCore.DataEntity.ResponseMapper.Govt;
using KilyCore.EntityFrameWork.Model.Govt;
using KilyCore.EntityFrameWork.ModelEnum;
using KilyCore.Extension.AttributeExtension;
using KilyCore.Repositories.BaseRepository;
using KilyCore.Service.IServiceCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：GovtWebService
* 类 描 述 ：
* 命名空间 ：KilyCore.Service.ServiceCore
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/7 11:20:01
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.Service.ServiceCore
{
    public class GovtWebService : Repository, IGovtWebService
    {
        #region 获取全局集团菜单
        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <returns></returns>
        public IList<ResponseGovtMenu> GetGovtMenu()
        {
            IQueryable<GovtMenu> queryable = Kily.Set<GovtMenu>().Where(t => t.Level == MenuEnum.LevelOne).Where(t => t.IsDelete == false).AsNoTracking().AsQueryable().OrderBy(t => t.CreateTime);
            IQueryable<GovtRoleAuthor> queryables = Kily.Set<GovtRoleAuthor>().Where(t => t.IsDelete == false);
            if (GovtInfo().AccountType <= GovtAccountEnum.Area)
                queryables = queryables.Where(t => !t.AuthorName.Contains("乡镇"));
            else
                queryables = queryables.Where(t => t.AuthorName.Contains("乡镇"));
            GovtRoleAuthor Author= queryables.FirstOrDefault();
            var data = queryable.OrderBy(t => t.CreateTime).Select(t => new ResponseGovtMenu()
            {
                Id = t.Id,
                MenuId = t.MenuId,
                ParentId = t.ParentId,
                MenuAddress = t.MenuAddress,
                MenuName = t.MenuName,
                HasChildrenNode = t.HasChildrenNode,
                MenuIcon = t.MenuIcon,
                MenuChildren = Kily.Set<GovtMenu>()
              .Where(x => x.ParentId == t.MenuId)
              .Where(x => x.Level != MenuEnum.LevelOne)
              .Where(x => x.IsDelete == false)
              .Where(x => Author.AuthorMenuPath.Contains(x.Id.ToString()))
              .OrderBy(x => x.CreateTime).Select(x => new ResponseGovtMenu()
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

        #region 登录
        /// <summary>
        /// 监管登录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public ResponseGovtInfo GovtLogin(RequestGovtInfo Param)
        {
            var data = Kily.Set<GovtInfo>().Where(t => t.Account.Equals(Param.Account) || t.Phone.Equals(Param.Account))
                 .Where(t => t.PassWord.Equals(Param.PassWord)).Select(t => new ResponseGovtInfo()
                 {
                     Id = t.Id,
                     GovtId = t.GovtId,
                     TableName = typeof(ResponseGovtInfo).Name,
                     Account = t.Account,
                     Phone = t.Phone,
                     AccountType = t.AccountType,
                     TrueName = t.TrueName,
                     TypePath = t.TypePath,
                     AccountTypeName = AttrExtension.GetSingleDescription<GovtAccountEnum, DescriptionAttribute>(t.AccountType),
                     DepartName = t.DepartName,
                     PassWord = t.PassWord,
                     DepartId = t.DepartId,
                     Email = t.Email,
                     Flag=t.UpdateUser
                 }).AsNoTracking().FirstOrDefault();
            return data;
        }
        #endregion
    }
}
