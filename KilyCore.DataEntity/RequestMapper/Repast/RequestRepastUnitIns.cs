﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.Repast
{
    public class RequestRepastUnitIns
    {
        public Guid Id { get; set; }
        public Guid InfoId { get; set; }
        /// <summary>
        /// 制度名称
        /// </summary>
        public string InsName { get; set; }
        /// <summary>
        /// 制度内容
        /// </summary>
        public string InsContent { get; set; }
        /// <summary>
        /// 制度时间
        /// </summary>
        public DateTime? InsTime { get; set; }
    }
    public class RequestRepastUnitInsRecord
    {
        public Guid Id { get; set; }
        public Guid InfoId { get; set; }
        /// <summary>
        /// 陪餐主题
        /// </summary>
        public string InsTheme { get; set; }
        /// <summary>
        /// 陪餐负责人
        /// </summary>
        public string InsUser { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string InsContent { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? InsTime { get; set; }
    }
}
