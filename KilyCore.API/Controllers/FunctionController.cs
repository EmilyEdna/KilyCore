using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.DataEntity.RequestMapper.Function;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}