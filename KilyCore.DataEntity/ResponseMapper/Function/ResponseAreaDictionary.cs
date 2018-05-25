using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.Function
{
    public class ResponseAreaDictionary
    {
        public Guid Id { get; set; }
        public string  DicName { get; set; }
        public string DicValue { get; set; }
        public bool? IsEnable { get; set; }
        public string States { get => (bool)IsEnable ? "禁用中" : "启用中"; }
    }
}
