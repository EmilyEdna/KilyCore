using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.System
{
    public class RequestValidate
    {
        public string Account { get; set; }
        public string PassWord { get; set; }
        public string ValidateCode { get; set; }
        public bool IsApp { get; set; }
    }
}
