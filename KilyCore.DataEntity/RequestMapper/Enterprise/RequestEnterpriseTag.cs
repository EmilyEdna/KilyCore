using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseTag
    {
        public string BacthNo { get; set; }

        public Int64 StarSerialNo { get; set; }

        public Int64 EndSerialNo { get; set; }

        public int TotalNo { get; set; }

        public TagEnum TagType { get; set; }
    }
}
