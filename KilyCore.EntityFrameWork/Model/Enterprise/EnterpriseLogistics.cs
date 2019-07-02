using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：EnterpriseLogistics
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/22 15:06:34
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 企业物流表
    /// </summary>
    public class EnterpriseLogistics : EnterpriseBase
    {
        /// <summary>
        /// 发货批次
        /// </summary>
        public virtual string BatchNo { get; set; }
        /// <summary>
        /// 物流单号
        /// </summary>
        public virtual string WayBill { get; set; }
        /// <summary>
        /// 包装编号
        /// </summary>
        public virtual string PackageNo { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string GoodsName { get; set; }
        /// <summary>
        /// 发货时间
        /// </summary>
        public virtual DateTime? SendTime { get; set; }
        /// <summary>
        /// 收货人公司Id
        /// </summary>
        public virtual Guid GainId { get; set; }
        /// <summary>
        /// 发货数量
        /// </summary>
        public virtual string SendGoodsNum { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public virtual string GainUser { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual string LinkPhone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 是否收货
        /// </summary>
        public virtual bool Flag { get; set; }
        /// <summary>
        /// 运输方式
        /// </summary>
        public virtual string TransportWay { get; set; }
        /// <summary>
        /// 交通工具
        /// </summary>
        public virtual string Traffic { get; set; }
        /// <summary>
        /// 发货地址
        /// </summary>
        public virtual string SendAddress { get; set; }
        /// <summary>
        /// 正确
        /// </summary>
        public virtual double Correct { get; set; }
        /// <summary>
        /// 错误
        /// </summary>
        public virtual double Error { get; set; }
        /// <summary>
        /// 收货时间
        /// </summary>
        public virtual DateTime? GetGoodTime{ get; set; }
        /// <summary>
        /// 发货方式
        /// </summary>
        public virtual int? SendType { get; set; }
        /// <summary>
        /// 追溯码
        /// </summary>
        public virtual string OneCode { get; set; }
        /// <summary>
        /// 箱码
        /// </summary>
        public virtual string BoxCode { get; set; }
    }
}
