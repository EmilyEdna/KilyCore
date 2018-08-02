using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RepastSupplier
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.Repast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/2 15:28:03
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.Model.Repast
{
    /// <summary>
    /// 餐饮企业供应商表
    /// </summary>
    public class RepastSupplier: RepastBase
    {
        /// <summary>
        /// 经营类型1表示企业，2表示个人
        /// </summary>
        public virtual int SupplierType { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string SupplierNo { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public virtual string SupplierName { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        public virtual string SupplierUser { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string LinkPhone { get; set; }
        /// <summary>
        /// 所在地
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 卫生许可证
        /// </summary>
        public virtual string HealthCard { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        public virtual string RunCard { get; set; }
    }
}
