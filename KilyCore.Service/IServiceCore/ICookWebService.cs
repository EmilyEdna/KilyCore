using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Cook;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Cook;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ICookServiceWeb
* 类 描 述 ：
* 命名空间 ：KilyCore.Service.IServiceCore
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/24 15:07:29
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 厨师前台后台业务逻辑接口
    /// </summary>
    public interface ICookWebService : IService
    {
        #region 登录注册
        String RegistCookAccount(RequestCookInfo Param);
        ResponseCookInfo CookLogin(RequestValidate LoginValidate);
        #endregion

        #region 全局菜单
        IList<ResponseCookMenu> GetCookMenu();
        #endregion

        #region 账号管理
        PagedResult<ResponseCookInfo> GetCookVipPage(PageParamList<RequestCookInfo> pageParam);
        ResponseCookInfo GetCookVipDetail(Guid Id);
        String EditCookVip(RequestCookInfo Param);
        ResponseStayContract OpenService(RequestStayContract Param);
        #endregion

        #region 厨师信息
        ResponseCookInfo GetCookInfoDetail();
        String EditCookInfo(RequestCookInfo Param);
        #endregion

        #region 办宴报备
        PagedResult<ResponseCookBanquet> GetBanquetPage(PageParamList<RequestCookBanquet> pageParam);
        String EditBanquet(RequestCookBanquet Param);
        ResponseCookBanquet GetBanquetDetail(Guid Id);
        String RemoveBanquet(Guid Id);
        #endregion

        #region 帮厨管理
        PagedResult<ResponseCookHelper> GetHelperPage(PageParamList<RequestCookHelper> pageParam);
        String EditHelper(RequestCookHelper Param);
        String RemoveHelper(Guid Id);
        IList<ResponseCookHelper> GetHelperList();
        #endregion

        #region 食材信息
        PagedResult<ResponseCookFood> GetFoodPage(PageParamList<RequestCookFood> pageParam);
        String RemoveFood(Guid Id);
        String EditFood(RequestCookFood Param);
        IList<ResponseCookFood> GetFoodList(Guid Param);
        #endregion

        #region 数据统计
        Object GetDataCount();
        #endregion

        #region 微信支付
        String WxQueryPay(Guid Param);
        #endregion
    }
}
