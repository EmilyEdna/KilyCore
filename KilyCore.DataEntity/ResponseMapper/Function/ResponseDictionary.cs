using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.Function
{
    public class ResponseDictionary
    {
        public Guid Id { get; set; }
        public string DicName { get; set; }
        public string DicValue { get; set; }
        public string DicDescript { get; set; }
        public string States { get => IsEnable ? "启用" : "禁用"; }
        public bool IsEnable { get; set; }
    }
}
