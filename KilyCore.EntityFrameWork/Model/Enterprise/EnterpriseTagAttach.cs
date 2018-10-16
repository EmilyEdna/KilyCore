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
    /// 二维码绑定表
    /// </summary>
    public class EnterpriseTagAttach : EnterpriseBase
    {
        /// <summary>
        /// 主表标签Id
        /// </summary>
        public virtual Guid TagId { get; set; }
        /// <summary>
        /// 产品表Id
        /// </summary>
        public virtual Guid GoodsId { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string TagBatchNo { get; set; }
        /// <summary>
        /// 使用数量
        /// </summary>
        public virtual int UseNum { get; set; }
        /// <summary>
        /// 开始号段
        /// </summary>
        public virtual Int64 StarSerialNo { get; set; }
        /// <summary>
        /// 结束号段
        /// </summary>
        public virtual Int64 EndSerialNo { get; set; }
        /// <summary>
        /// 标签类型 1:表示纹理二维码。2:表示一物一码。3:表示一品一码。
        /// </summary>
        public virtual string TagType { get; set; }
        /// <summary>
        /// 入库批次
        /// </summary>
        public virtual string StockNo { get; set; }
    }
}
