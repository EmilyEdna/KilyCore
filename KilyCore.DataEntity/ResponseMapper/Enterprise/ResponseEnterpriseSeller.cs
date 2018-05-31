using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseEnterpriseSeller
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/5/31 9:15:51
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseSeller
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string SupplierTypeName { get => SupplierType.HasValue ? (SupplierType == 1 ? "企业" : "个人") : "-"; }
        public string SellerTypeName { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 1表示企业2表示个人
        /// </summary>
        public int? SupplierType { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string DutyMan { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string LinkPhone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 社会代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        public string RunCard { get; set; }
        /// <summary>
        /// 经营许可
        /// </summary>
        public string OkayCard { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 生产许可
        /// </summary>
        public string ProductCard { get; set; }
        /// <summary>
        /// 厂商类型
        /// </summary>
        public SellerEnum SellerType { get; set; }
    }
}
