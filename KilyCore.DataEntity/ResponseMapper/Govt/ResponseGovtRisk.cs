using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseGovtRisk
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/19 11:46:21
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Govt
{
    public class ResponseGovtRisk
    {
        public Guid Id { get; set; }
        public Guid? GovtId { get; set; }
        /// <summary>
        /// 事件名称
        /// </summary>
        public string EventName { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public string TypePath { get; set; }
        public string Province
        {
            get
            {
                return !string.IsNullOrEmpty(TypePath) ? (TypePath.Split(',').Length >= 1 ? TypePath.Split(',')[0] : null) : null;
            }
        }
        public string City
        {
            get
            {
                return !string.IsNullOrEmpty(TypePath) ? (TypePath.Split(',').Length >= 2 ? TypePath.Split(',')[1] : null) : null;
            }
        }
        public string Area
        {
            get
            {
                return !string.IsNullOrEmpty(TypePath) ? (TypePath.Split(',').Length >= 3 ? TypePath.Split(',')[2] : null) : null;
            }
        }
        public string Town
        {
            get
            {
                return !string.IsNullOrEmpty(TypePath) ? (TypePath.Split(',').Length >= 4 ? (TypePath.Split(',')[3]) : null) : null;
            }
        }
        /// <summary>
        /// 危险等级
        /// </summary>
        public string WaringLv { get; set; }
        /// <summary>
        /// 行业类型
        /// </summary>
        public string TradeType { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? ReleaseTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Desc { set; get; }
        /// <summary>
        /// 是否广播
        /// </summary>
        public bool ReportPlay { get; set; }
        public string Report => ReportPlay ? "已广播" : "未广播";
    }
}
