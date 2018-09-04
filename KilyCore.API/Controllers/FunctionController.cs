﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.DataEntity.RequestMapper.Function;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.API.Controllers
{
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
        public ObjectResultEx GetAreaPriceDetail(SimlpeParam<Guid> Param)
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
        public ObjectResultEx RemovePrice(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(FunctionService.RemovePrice(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetAddressList")]
        public ObjectResultEx GetAddressList(SimlpeParam<int> Param)
        {
            return ObjectResultEx.Instance(FunctionService.GetAddressList(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 纹理二维码
        /// <summary>
        /// 纹理二维码分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetTagPage")]
        public ObjectResultEx GetTagPage(PageParamList<RequestVeinTag> pageParam)
        {
            return ObjectResultEx.Instance(FunctionService.GetTagPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 录入标签
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RecordTag")]
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
        public ObjectResultEx AllotTag(RequestVeinTag Param)
        {
            return ObjectResultEx.Instance(FunctionService.AllotTag(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除二维码
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveTag")]
        public ObjectResultEx RemoveTag(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(FunctionService.RemoveTag(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 根据接收类型选取接受人
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetAcceptUser")]
        public ObjectResultEx GetAcceptUser(SimlpeParam<int> Param)
        {
            return ObjectResultEx.Instance(FunctionService.GetAcceptUser(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 纹理二维码批次号
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetTagBatchList")]
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
        public ObjectResultEx AcceptTag(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(FunctionService.AcceptTag(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
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
        public ObjectResultEx GetDicDetail(SimlpeParam<Guid> Param)
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
        /// <param name="Value"></param>
        /// <returns></returns>
        [HttpPost("RemoveDic")]
        public ObjectResultEx RemoveDic(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(FunctionService.RemoveDic(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
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
        /// <param name="Value"></param>
        /// <returns></returns>
        [HttpPost("IsEnable")]
        public ObjectResultEx IsEnable(SimlpeParam<Guid> Key, SimlpeParam<bool> Value)
        {
            return ObjectResultEx.Instance(FunctionService.IsEnable(Key.Id, Value.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 区域版本
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetAreaVersion")]
        public ObjectResultEx GetAreaVersion(SimlpeParam<String> Key, SimlpeParam<int> Value)
        {
            return ObjectResultEx.Instance(FunctionService.GetAreaVersion(Key.Id, Value.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取分配详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetAreaDicDetail")]
        public ObjectResultEx GetAreaDicDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(FunctionService.GetAreaDicDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 数据统计
        /// <summary>
        /// 饼状统计图
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetPieData")]
        public ObjectResultEx GetPieData()
        {
            return ObjectResultEx.Instance(FunctionService.GetPieData(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 柱状统计图
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetBarData")]
        public ObjectResultEx GetBarData()
        {
            return ObjectResultEx.Instance(FunctionService.GetBarData(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}