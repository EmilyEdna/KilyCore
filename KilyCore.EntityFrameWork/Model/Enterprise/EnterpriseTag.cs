using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
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
    /// 企业物码表
    /// </summary>
    public class EnterpriseTag: EnterpriseBase
    {
        /// <summary>
        /// 批次
        /// </summary>
        public virtual string BatchNo { get; set; }
        /// <summary>
        /// 开始号段
        /// </summary>
        public virtual Int64 StarSerialNo { get; set; }
        /// <summary>
        /// 结束号段
        /// </summary>
        public virtual Int64 EndSerialNo { get; set; }
        /// <summary>
        /// 号段区间数量
        /// </summary>
        public virtual int TotalNo { get; set; }
        /// <summary>
        /// 二维码类型
        /// </summary>
        public virtual TagEnum TagType { get; set; }
        /// <summary>
        /// 是否使用的申请
        /// </summary>
        public virtual bool IsApplay { get; set; }
        /// <summary>
        /// 使用数量
        /// </summary>
        public virtual int UseNum { get; set; }
        /// <summary>
        /// 是否生成过空白标签
        /// </summary>
        public virtual bool? IsCreate { get; set; }
        /// <summary>
        /// 前缀识别
        /// </summary>
        public virtual string CodeDiscern { get; set; }
    }
}
