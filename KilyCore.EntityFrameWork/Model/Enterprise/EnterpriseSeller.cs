using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月30日15点42分
/// </summary>
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 厂商表
    /// </summary>
    public class EnterpriseSeller : BaseEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string No { get; set; }
        /// <summary>
        /// 1表示企业2表示个人
        /// </summary>
        public virtual int? SupplierType { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public virtual string SupplierName { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public virtual string DutyMan { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual string LinkPhone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 社会代码
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        public virtual string RunCard { get; set; }
        /// <summary>
        /// 经营许可
        /// </summary>
        public virtual string OkayCard { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public virtual string IdCard { get; set; }
        /// <summary>
        /// 生产许可
        /// </summary>
        public virtual string ProductCard { get; set; }
        /// <summary>
        /// 厂商类型
        /// </summary>
        public virtual SellerEnum SellerType { get; set; }
    }
}
