using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.DataEntity.RequestMapper.Function
{
    public class RequestAreaDictionary
    {
        public Guid Id { get; set; }
        public string ProvinceId { get; set; }
        public Guid DictionaryId { get; set; }
    }
    public class RequestDisDictionary {
        public Guid? AreaDicId { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnable { get; set; }
        /// <summary>
        /// 省份Id
        /// </summary>
        public string ProvinceId { get; set; }
    }
}
