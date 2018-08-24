using KilyCore.DataEntity.RequestMapper.Cook;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Cook;
using KilyCore.EntityFrameWork.Model.Cook;
using KilyCore.EntityFrameWork.ModelEnum;
using KilyCore.Extension.AutoMapperExtension;
using KilyCore.Repositories.BaseRepository;
using KilyCore.Service.ConstMessage;
using KilyCore.Service.IServiceCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                .Where(t => t.Account.Equals(LoginValidate.Account))
                .Where(t => t.PassWord.Equals(LoginValidate.PassWord))
                .Where(t => t.IsDelete == false);
            IQueryable<CookInfo> queryables = Kily.Set<CookInfo>();
            var data = queryable.GroupJoin(queryables, t => t.Id, x => x.CookId, (t, x) => new ResponseCookInfo()
            {
                Id = t.Id,
                CookId = t.Id,
                Sexlab = x.FirstOrDefault().Sexlab,
                Account = t.Account,
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
    }
}
