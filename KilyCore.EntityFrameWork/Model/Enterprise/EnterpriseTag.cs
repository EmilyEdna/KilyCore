using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

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
        public virtual string BacthNo { get; set; }
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
    }
}
