using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KilyCore.WEB.Model
{
    public class ExportModelBase
    {
        public List<FileText> data { get; set; }
    }
    public class FileText
    {
        [JsonProperty("No")]
        public int 编号 { get; set; }
        [JsonProperty("FileTitle")]
        public string 标题 { get; set; }
    }
}
