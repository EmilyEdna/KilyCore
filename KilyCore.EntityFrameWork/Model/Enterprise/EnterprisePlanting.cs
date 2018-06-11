using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 施养管理表
    /// </summary>
    public class EnterprisePlanting: EnterpriseBase
    {
        /// <summary>
        /// 批次号段
        /// </summary>
        public virtual string BatchNo { get; set; }
        /// <summary>
        /// 饲料名称
        /// </summary>
        public virtual string FeedName { get; set; }
        /// <summary>
        /// 饲料品牌
        /// </summary>
        public virtual string Brand { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public virtual string Supplier { get; set; }
        /// <summary>
        /// 施肥时间
        /// </summary>
        public virtual DateTime PlantTime { get; set; }
        /// <summary>
        /// 检测报告
        /// </summary>
        public virtual string CheckReport { get; set; }
        /// <summary>
        ///  1表示水肥管理2表示喂养管理
        /// </summary>
        public virtual int IsType { get; set; }
    }
}
