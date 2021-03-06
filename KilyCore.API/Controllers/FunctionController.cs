﻿using KilyCore.DataEntity.RequestMapper.Function;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.API.Controllers
{
    /// <summary>
    /// 系统功能接口
    /// </summary>
    [Route("api/[controller]")]
    public class FunctionController : BaseController
    {
        #region 区域价目

        /// <summary>
        /// 获取价目分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetAreaPricePage")]
        public ObjectResultEx GetAreaPricePage(PageParamList<RequestAreaPrice> pageParam)
        {
            return ObjectResultEx.Instance(FunctionService.GetAreaPricePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取区域价目详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetAreaPriceDetail")]
        public ObjectResultEx GetAreaPriceDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(FunctionService.GetAreaPriceDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 编辑区域价目
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditPrice")]
        public ObjectResultEx EditPrice(RequestAreaPrice Param)
        {
            return ObjectResultEx.Instance(FunctionService.EditPrice(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 删除价目
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemovePrice")]
        public ObjectResultEx RemovePrice(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(FunctionService.RemovePrice(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetAddressList")]
        public ObjectResultEx GetAddressList(SimpleParam<int> Param)
        {
            return ObjectResultEx.Instance(FunctionService.GetAddressList(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 区域价目

        #region 纹理二维码

        /// <summary>
        /// 纹理二维码分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetTagPage")]
        [AllowAnonymous]
        public ObjectResultEx GetTagPage(PageParamList<RequestVeinTag> pageParam)
        {
            return ObjectResultEx.Instance(FunctionService.GetTagPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 查看分配企业标签
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetTagToCompanyPage")]
        [AllowAnonymous]
        public ObjectResultEx GetTagToCompanyPage(PageParamList<RequestVeinTag> pageParam)
        {
            return ObjectResultEx.Instance(FunctionService.GetTagToCompanyPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 查看分配营运中心标签
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetTagToAdminPage")]
        [AllowAnonymous]
        public ObjectResultEx GetTagToAdminPage(PageParamList<RequestVeinTag> pageParam)
        {
            return ObjectResultEx.Instance(FunctionService.GetTagToAdminPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 录入标签
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RecordTag")]
        [AllowAnonymous]
        public ObjectResultEx RecordAllotTag(RequestVeinTag Param)
        {
            return ObjectResultEx.Instance(FunctionService.RecordTag(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 分配标签
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AllotTag")]
        [AllowAnonymous]
        public ObjectResultEx AllotTag(RequestVeinTag Param)
        {
            return ObjectResultEx.Instance(FunctionService.AllotTag(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 删除二维码
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveTag")]
        [AllowAnonymous]
        public ObjectResultEx RemoveTag(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(FunctionService.RemoveTag(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 根据接收类型选取接受人
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetAcceptUser")]
        [AllowAnonymous]
        public ObjectResultEx GetAcceptUser(SimpleParam<int> Param)
        {
            return ObjectResultEx.Instance(FunctionService.GetAcceptUser(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 纹理二维码批次号
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetTagBatchList")]
        [AllowAnonymous]
        public ObjectResultEx GetTagBatchList()
        {
            return ObjectResultEx.Instance(FunctionService.GetTagBatchList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 签收标签
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AcceptTag")]
        [AllowAnonymous]
        public ObjectResultEx AcceptTag(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(FunctionService.AcceptTag(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 判断是否存在纹理标签
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpGet("IsVenTag")]
        [AllowAnonymous]
        public ObjectResultEx IsVenTag(SimpleParam<int> Param)
        {
            return ObjectResultEx.Instance(FunctionService.IsVenTag(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 纹理二维码

        #region 系统码表

        /// <summary>
        /// 系统码表分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetSysDicPage")]
        public ObjectResultEx GetSysDicPage(PageParamList<RequestDictionary> pageParam)
        {
            return ObjectResultEx.Instance(FunctionService.GetSysDicPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 码表详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetDicDetail")]
        public ObjectResultEx GetDicDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(FunctionService.GetDicDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 编辑码表
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("DicEdit")]
        public ObjectResultEx DicEdit(RequestDictionary Param)
        {
            return ObjectResultEx.Instance(FunctionService.DicEdit(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveDic")]
        public ObjectResultEx RemoveDic(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(FunctionService.RemoveDic(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 系统码表

        #region 区域码表

        /// <summary>
        /// 区域码表分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetAreaDicPage")]
        public ObjectResultEx GetAreaDicPage(PageParamList<RequestAreaDictionary> pageParam)
        {
            return ObjectResultEx.Instance(FunctionService.GetAreaDicPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 分配区域码表
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RecordAreaDic")]
        public ObjectResultEx RecordAreaDic(RequestAreaDictionary Param)
        {
            return ObjectResultEx.Instance(FunctionService.RecordAreaDic(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 启用字典
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("IsEnable")]
        public ObjectResultEx IsEnable(RequestDisDictionary Param)
        {
            return ObjectResultEx.Instance(FunctionService.IsEnable(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 区域版本
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        [HttpPost("GetAreaVersion")]
        public ObjectResultEx GetAreaVersion(SimpleParam<String> Key, SimpleParam<int> Value)
        {
            return ObjectResultEx.Instance(FunctionService.GetAreaVersion(Key.Id, Value.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取功能描述
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetVersionById")]
        public ObjectResultEx GetVersionById(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(FunctionService.GetVersionById(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取分配详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetAreaDicDetail")]
        public ObjectResultEx GetAreaDicDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(FunctionService.GetAreaDicDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 区域码表

        #region 数据统计

        /// <summary>
        /// 饼状统计图
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetPieData")]
        [AllowAnonymous]
        public ObjectResultEx GetPieData()
        {
            return ObjectResultEx.Instance(FunctionService.GetPieData(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 柱状统计图
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetBarData")]
        [AllowAnonymous]
        public ObjectResultEx GetBarData()
        {
            return ObjectResultEx.Instance(FunctionService.GetBarData(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取首页企业统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetStatistics")]
        [AllowAnonymous]
        public ObjectResultEx GetStatistics()
        {
            return ObjectResultEx.Instance(FunctionService.GetStatistics(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 获取生成的二维码
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCreateTagList")]
        [AllowAnonymous]
        public ObjectResultEx GetCreateTagList()
        {
            return ObjectResultEx.Instance(FunctionService.GetCreateTagList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 数据统计

        #region 系统消息

        /// <summary>
        /// 消息中心
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetMsgPage")]
        public ObjectResultEx GetMsgPage(PageParamList<Object> pageParam)
        {
            return ObjectResultEx.Instance(FunctionService.GetMsgPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveMsg")]
        public ObjectResultEx RemoveMsg(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(FunctionService.RemoveMsg(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 系统消息

        #region 定时提醒合同

        /// <summary>
        /// 合同提醒
        /// </summary>
        /// <returns></returns>
        [HttpGet("NotifyContract")]
        public ObjectResultEx NotifyContract()
        {
            return ObjectResultEx.Instance(FunctionService.NotifyContract(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        /// <summary>
        /// 合同导出
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        [HttpGet("NofityCompany")]
        public ObjectResultEx NofityCompany(SimpleParam<string> Key)
        {
            return ObjectResultEx.Instance(FunctionService.NofityCompany(Key.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 定时提醒合同
    }
}