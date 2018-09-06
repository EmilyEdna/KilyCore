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
* 类 名 称 ：GovtService
* 类 描 述 ：
* 命名空间 ：KilyCore.Service.ServiceCore
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/6 14:37:52
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.Service.ServiceCore
{
    public class GovtService : Repository, IGovtService
    {
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
                     Id=t.Id,
                     GovtId=t.GovtId,
                     TableName=t.GetType().Name,
                     Account=t.Account,
                     Phone=t.Phone,
                     AccountType=t.AccountType,
                     TrueName=t.TrueName,
                     TypePath=t.TypePath,
                     AccountTypeName=AttrExtension.GetSingleDescription<GovtAccountEnum, DescriptionAttribute>(t.AccountType),
                     DepartName=t.DepartName,
                     PassWord=t.PassWord,
                     DepartId=t.DepartId,
                     Email=t.Email
                 }).AsNoTracking().FirstOrDefault();
            return data;
        }
        #endregion
    }
}
