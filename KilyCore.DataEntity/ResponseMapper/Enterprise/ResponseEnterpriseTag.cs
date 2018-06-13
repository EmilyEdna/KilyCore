﻿using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseTag
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string BatchNo { get; set; }
        public Int64 StarSerialNo { get; set; }
        public Int64 EndSerialNo { get; set; }
        public int TotalNo { get; set; }
        public TagEnum TagType { get; set; }
        public string TagTypeName { get; set; }
        public virtual int? UseNum { get; set; }
    }
}
