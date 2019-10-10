using Newtonsoft.Json;
using System;
using System.Collections.Generic;

/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
namespace KilyCore.Service.QueryExtend
{
    /// <summary>
    /// 分页结果返回
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T>
    {
        public PagedResult()
        {
            this.List = new List<T>();
        }

        /// <summary>
        /// 总行数
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        [JsonProperty("rows")]
        public List<T> List { get; set; }

        /// <summary>
        /// 开始位置
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            get
            {
                return (int)Math.Ceiling(Total / (double)PageSize);
            }
        }

        public int CurrentPage
        {
            get
            {
                return (int)Math.Ceiling(Index / (double)PageSize) + 1;
            }
        }
    }
}