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
        /// 开始号段
        /// </summary>
        public virtual Int64 StarSerialNo { get; set; }
        /// <summary>
        /// 结束号段
        /// </summary>
        public virtual Int64 EndSerialNo { get; set; }
        /// <summary>
        /// 标签类型 1:表示纹理二维码。2:表示普通二维码。
        /// </summary>
        public virtual string TagType { get; set; }
    }
}
