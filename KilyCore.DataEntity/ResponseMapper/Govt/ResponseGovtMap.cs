using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Govt
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/12/6 11:07:07
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Govt
{
    public class ResponseGovtMap
    {
        public String CityName { get; set; }
        public int City { get; set; }
        public IList<ResponseGovtRanking> DataList { get; set; }
        public int All { get; set; }
    }
    public class ResponseGovtRanking
    {
        [JsonProperty("name")]
        public string AreaName { get; set; }
        [JsonProperty("value")]
        public int TotalCount { get; set; }

    }
}
