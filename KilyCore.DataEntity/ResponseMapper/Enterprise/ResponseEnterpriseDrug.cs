﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
    public class ResponseEnterpriseDrug
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string DrugName { get; set; }
        public string Brand { get; set; }
        public string Supplier { get; set; }
        public DateTime PlantTime { get; set; }
        public string CheckReport { get; set; }
        public string BatchNo { get; set; }
        public  int IsType { get; set; }
        /// <summary>
        /// 生产商
        /// </summary>
        public string Producter { get; set; }
    }
}
