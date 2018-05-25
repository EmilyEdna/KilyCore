using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Function;
using KilyCore.DataEntity.ResponseMapper.Function;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 实用功能业务逻辑接口
    /// </summary>
    public interface IFunctionService: IService
    {
        #region 区域价目
        PagedResult<ResponseAreaPrice> GetAreaPricePage(PageParamList<RequestAreaPrice> pageParam);
        ResponseAreaPrice GetAreaPriceDetail(Guid Id);
        String EditPrice(RequestAreaPrice Param);
        String RemovePrice(Guid Id);
        IList<ResponseAreaPrice> GetAddressList(int AccountType);
        #endregion
        #region 纹理二维码
        PagedResult<ResponseVeinTag> GetTagPage(PageParamList<RequestVeinTag> pageParam);
        String RecordAllotTag(RequestVeinTag Param);
        String RemoveTag(Guid Id);
        IList<ResponseVienTagPreson> GetAcceptUser(int flag);
        String AcceptTag(Guid Id);
        #endregion
        #region 系统码表
        PagedResult<ResponseDictionary> GetSysDicPage(PageParamList<RequestDictionary> pageParam);
        ResponseDictionary GetDicDetail(Guid Id);
        String DicEdit(RequestDictionary Param);
        String RemoveDic(Guid Id);
        #endregion
        #region 区域码表
        PagedResult<ResponseAreaDictionary> GetAreaDicPage(PageParamList<RequestAreaDictionary> pageParam);
        String RecordAreaDic(RequestAreaDictionary Param);
        String IsEnable(Guid Id, bool Param);
        #endregion
    }
}
