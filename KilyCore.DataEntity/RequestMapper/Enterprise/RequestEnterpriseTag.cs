using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseTag
    {
        public Guid CompanyId { get; set; }
        public string BatchNo { get; set; }

        public Int64 StarSerialNo { get; set; }

        public Int64 EndSerialNo { get; set; }

        public int TotalNo { get; set; }

        public TagEnum TagType { get; set; }
    }
    public class RequestEnterpriseTagAttach
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string TagType { get; set; }
        public  Guid TagId { get; set; }
        public  Guid GoodsId { get; set; }
        public  string TagBatchNo { get; set; }
        public  Int64 StarSerialNo { get; set; }
        public  Int64 EndSerialNo { get; set; }
        public  int UseNum { get; set; }
    }
}
