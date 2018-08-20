using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KilyCore.WEB.Model
{
    public class MaterialStockIn
    {
        public List<MaterialStockInData> data { get; set; }
    }
    public class MaterialStockInData
    {
        [JsonProperty("No")]
        public int 编号 { get; set; }
        [JsonProperty("SerializNo")]
        public string 入库批次 { get; set; }
        [JsonProperty("MaterName")]
        public string 原料名称 { get; set; }
        [JsonProperty("StockType")]
        public string 入库类型 { get; set; }
        [JsonProperty("SetStockNum")]
        public string 入库数量 { get; set; }
        [JsonProperty("SetStockTime")]
        public string 入库时间 { get; set; }
        [JsonProperty("Supplier")]
        public string 供应商 { get; set; }
        [JsonProperty("SetStockUser")]
        public string 负责人 { get; set; }
    }
}
