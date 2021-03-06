﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.System
{
    public class ResponseSystemNetService
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 网点名称
        /// </summary>
        public string ServiceNetName { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 信誉代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkPhone { get; set; }
        /// <summary>
        /// 法人
        /// </summary>
        public string Off { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 营业执照副本
        /// </summary>
        public string IdImage { get; set; }
        /// <summary>
        /// 服务年限
        /// </summary>
        public string ServiceYear { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? STime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? ETime { get; set; }
        /// <summary>
        /// 服务区域
        /// </summary>
        public string ServciePath { get; set; }
        public string Status { get; set; }
    }
}
