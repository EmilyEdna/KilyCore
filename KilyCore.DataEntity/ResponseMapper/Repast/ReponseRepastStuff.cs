using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ReponseRepastStuff
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/7 11:19:44
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Repast
{
    public class ReponseRepastStuff
    {
        public Guid Id { get; set; }
        public Guid InfoId { get; set; }
        /// <summary>
        /// 原料名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 原料类型
        /// </summary>
        public string MaterialType { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }
        /// <summary>
        /// 供应时间
        /// </summary>
        public DateTime? SuppTime { get; set; }
        /// <summary>
        /// 保质期
        /// </summary>
        public DateTime? ExpiredTime { get; set; }
        /// <summary>
        /// 质检报告
        /// </summary>
        public string QualityReport { get; set; }
        /// <summary>
        /// 资质证书
        /// </summary>
        public string Aptitude { get; set; }
        /// <summary>
        /// 执行标准
        /// </summary>
        public string Standard { get; set; }
        /// <summary>
        /// 生产地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 溯源连接
        /// </summary>
        public string SourceLink { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
