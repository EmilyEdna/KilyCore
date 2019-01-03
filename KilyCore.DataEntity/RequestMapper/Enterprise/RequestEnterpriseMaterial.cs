using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestEnterpriseMaterial
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Enterprise
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/5 16:54:42
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseMaterial
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string BatchNo { get; set; }
        public string MaterName { get; set; }
        public string ExpiredDay { get; set; }
        public string MaterType { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        public string Standard { get; set; }
        public string Supplier { get; set; }
        public string Address { get; set; }
        public  int MaterNum { get; set; }
        public string PackageType { get; set; }
        /// <summary>
        /// 采购时间
        /// </summary>
        public  DateTime? BuyTime { get; set; }
        /// <summary>
        /// 原料生产时间
        /// </summary>
        public  DateTime? MaterCreateTime { get; set; }
    }
}
